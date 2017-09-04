using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Tencent
{
	public class WXBizMsgCrypt
	{
		public enum WXBizMsgCryptErrorCode
		{
			WXBizMsgCrypt_OK,
			WXBizMsgCrypt_ValidateSignature_Error = -40001,
			WXBizMsgCrypt_ParseXml_Error = -40002,
			WXBizMsgCrypt_ComputeSignature_Error = -40003,
			WXBizMsgCrypt_IllegalAesKey = -40004,
			WXBizMsgCrypt_ValidateAppid_Error = -40005,
			WXBizMsgCrypt_EncryptAES_Error = -40006,
			WXBizMsgCrypt_DecryptAES_Error = -40007,
			WXBizMsgCrypt_IllegalBuffer = -40008,
			WXBizMsgCrypt_EncodeBase64_Error = -40009,
			WXBizMsgCrypt_DecodeBase64_Error = -40010
		}

		public class DictionarySort : IComparer
		{
			public int Compare(object oLeft, object oRight)
			{
				string sLeft = oLeft as string;
				string sRight = oRight as string;
				int iLeftLength = sLeft.Length;
				int iRightLength = sRight.Length;
				int index = 0;
				int result;
				while (index < iLeftLength && index < iRightLength)
				{
					if (sLeft[index] < sRight[index])
					{
						result = -1;
					}
					else
					{
						if (sLeft[index] <= sRight[index])
						{
							index++;
							continue;
						}
						result = 1;
					}
					return result;
				}
				result = iLeftLength - iRightLength;
				return result;
			}
		}

		private string m_sToken;

		private string m_sEncodingAESKey;

		private string m_sAppID;

		public WXBizMsgCrypt(string sToken, string sEncodingAESKey, string sAppID)
		{
			this.m_sToken = sToken;
			this.m_sAppID = sAppID;
			this.m_sEncodingAESKey = sEncodingAESKey;
		}

		public int DecryptMsg(string sMsgSignature, string sTimeStamp, string sNonce, string sPostData, ref string sMsg)
		{
			int result;
			if (this.m_sEncodingAESKey.Length != 43)
			{
				result = -40004;
			}
			else
			{
				XmlDocument doc = new XmlDocument();
				string sEncryptMsg;
				try
				{
					doc.LoadXml(sPostData);
					XmlNode root = doc.FirstChild;
					sEncryptMsg = root["Encrypt"].InnerText;
				}
				catch (Exception)
				{
					result = -40002;
					return result;
				}
				int ret = WXBizMsgCrypt.VerifySignature(this.m_sToken, sTimeStamp, sNonce, sEncryptMsg, sMsgSignature);
				if (ret != 0)
				{
					result = ret;
				}
				else
				{
					string cpid = "";
					try
					{
						sMsg = Cryptography.AES_decrypt(sEncryptMsg, this.m_sEncodingAESKey, ref cpid);
					}
					catch (FormatException)
					{
						result = -40010;
						return result;
					}
					catch (Exception)
					{
						result = -40007;
						return result;
					}
					if (cpid != this.m_sAppID)
					{
						result = -40005;
					}
					else
					{
						result = 0;
					}
				}
			}
			return result;
		}

		public int EncryptMsg(string sReplyMsg, string sTimeStamp, string sNonce, ref string sEncryptMsg)
		{
			int result;
			if (this.m_sEncodingAESKey.Length != 43)
			{
				result = -40004;
			}
			else
			{
				string raw = "";
				try
				{
					raw = Cryptography.AES_encrypt(sReplyMsg, this.m_sEncodingAESKey, this.m_sAppID);
				}
				catch (Exception)
				{
					result = -40006;
					return result;
				}
				string MsgSigature = "";
				int ret = WXBizMsgCrypt.GenarateSinature(this.m_sToken, sTimeStamp, sNonce, raw, ref MsgSigature);
				if (0 != ret)
				{
					result = ret;
				}
				else
				{
					sEncryptMsg = "";
					string EncryptLabelHead = "<Encrypt><![CDATA[";
					string EncryptLabelTail = "]]></Encrypt>";
					string MsgSigLabelHead = "<MsgSignature><![CDATA[";
					string MsgSigLabelTail = "]]></MsgSignature>";
					string TimeStampLabelHead = "<TimeStamp><![CDATA[";
					string TimeStampLabelTail = "]]></TimeStamp>";
					string NonceLabelHead = "<Nonce><![CDATA[";
					string NonceLabelTail = "]]></Nonce>";
					sEncryptMsg = string.Concat(new string[]
					{
						sEncryptMsg,
						"<xml>",
						EncryptLabelHead,
						raw,
						EncryptLabelTail
					});
					sEncryptMsg = sEncryptMsg + MsgSigLabelHead + MsgSigature + MsgSigLabelTail;
					sEncryptMsg = sEncryptMsg + TimeStampLabelHead + sTimeStamp + TimeStampLabelTail;
					sEncryptMsg = sEncryptMsg + NonceLabelHead + sNonce + NonceLabelTail;
					sEncryptMsg += "</xml>";
					result = 0;
				}
			}
			return result;
		}

		public static int VerifySignature(string sToken, string sTimeStamp, string sNonce, string sMsgEncrypt, string sSigture)
		{
			string hash = "";
			int ret = WXBizMsgCrypt.GenarateSinature(sToken, sTimeStamp, sNonce, sMsgEncrypt, ref hash);
			int result;
			if (ret != 0)
			{
				result = ret;
			}
			else if (hash == sSigture)
			{
				result = 0;
			}
			else
			{
				result = -40001;
			}
			return result;
		}

		public static int VerifySignature(string sToken, string sTimeStamp, string sNonce, string sSigture)
		{
			return WXBizMsgCrypt.VerifySignature(sToken, sTimeStamp, sNonce, "", sSigture);
		}

		public static int GenarateSinature(string sToken, string sTimeStamp, string sNonce, string sMsgEncrypt, ref string sMsgSignature)
		{
			ArrayList AL = new ArrayList();
			AL.Add(sToken);
			AL.Add(sTimeStamp);
			AL.Add(sNonce);
			AL.Add(sMsgEncrypt);
			AL.Sort(new WXBizMsgCrypt.DictionarySort());
			string raw = "";
			for (int i = 0; i < AL.Count; i++)
			{
				raw += AL[i];
			}
			string hash = "";
			int result;
			try
			{
				SHA1 sha = new SHA1CryptoServiceProvider();
				ASCIIEncoding enc = new ASCIIEncoding();
				byte[] dataToHash = enc.GetBytes(raw);
				byte[] dataHashed = sha.ComputeHash(dataToHash);
				hash = BitConverter.ToString(dataHashed).Replace("-", "");
				hash = hash.ToLower();
			}
			catch (Exception)
			{
				result = -40003;
				return result;
			}
			sMsgSignature = hash;
			result = 0;
			return result;
		}
	}
}
