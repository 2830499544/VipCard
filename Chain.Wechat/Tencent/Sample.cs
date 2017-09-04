using System;
using System.Xml;

namespace Tencent
{
	internal class Sample
	{
		private void Main(string[] args)
		{
			string sToken = "QDG6eK";
			string sAppID = "wx5823bf96d3bd56c7";
			string sEncodingAESKey = "jWmYm7qr5nMoAUwZRjGtBxmz3KA1tkAj3ykkR6q2B2C";
			WXBizMsgCrypt wxcpt = new WXBizMsgCrypt(sToken, sEncodingAESKey, sAppID);
			string sReqMsgSig = "477715d11cdb4164915debcba66cb864d751f3e6";
			string sReqTimeStamp = "1409659813";
			string sReqNonce = "1372623149";
			string sReqData = "<xml><ToUserName><![CDATA[wx5823bf96d3bd56c7]]></ToUserName><Encrypt><![CDATA[RypEvHKD8QQKFhvQ6QleEB4J58tiPdvo+rtK1I9qca6aM/wvqnLSV5zEPeusUiX5L5X/0lWfrf0QADHHhGd3QczcdCUpj911L3vg3W/sYYvuJTs3TUUkSUXxaccAS0qhxchrRYt66wiSpGLYL42aM6A8dTT+6k4aSknmPj48kzJs8qLjvd4Xgpue06DOdnLxAUHzM6+kDZ+HMZfJYuR+LtwGc2hgf5gsijff0ekUNXZiqATP7PF5mZxZ3Izoun1s4zG4LUMnvw2r+KqCKIw+3IQH03v+BCA9nMELNqbSf6tiWSrXJB3LAVGUcallcrw8V2t9EL4EhzJWrQUax5wLVMNS0+rUPA3k22Ncx4XXZS9o0MBH27Bo6BpNelZpS+/uh9KsNlY6bHCmJU9p8g7m3fVKn28H3KDYA5Pl/T8Z1ptDAVe0lXdQ2YoyyH2uyPIGHBZZIs2pDBS8R07+qN+E7Q==]]></Encrypt></xml>";
			string sMsg = "";
			int ret = wxcpt.DecryptMsg(sReqMsgSig, sReqTimeStamp, sReqNonce, sReqData, ref sMsg);
			if (ret != 0)
			{
				Console.WriteLine("ERR: Decrypt fail, ret: " + ret);
			}
			else
			{
				Console.WriteLine(sMsg);
				string sRespData = "<xml><ToUserName><![CDATA[mycreate]]></ToUserName><FromUserName><![CDATA[wx582测试一下中文的情况，消息长度是按字节来算的396d3bd56c7]]></FromUserName><CreateTime>1348831860</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[this is a test]]></Content><MsgId>1234567890123456</MsgId></xml>";
				string sEncryptMsg = "";
				ret = wxcpt.EncryptMsg(sRespData, sReqTimeStamp, sReqNonce, ref sEncryptMsg);
				Console.WriteLine("sEncryptMsg");
				Console.WriteLine(sEncryptMsg);
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(sEncryptMsg);
				XmlNode root = doc.FirstChild;
				string sig = root["MsgSignature"].InnerText;
				string enc = root["Encrypt"].InnerText;
				string timestamp = root["TimeStamp"].InnerText;
				string nonce = root["Nonce"].InnerText;
				string stmp = "";
				ret = wxcpt.DecryptMsg(sig, timestamp, nonce, sEncryptMsg, ref stmp);
				Console.WriteLine("stemp");
				Console.WriteLine(stmp + ret);
				Console.ReadKey();
			}
		}
	}
}
