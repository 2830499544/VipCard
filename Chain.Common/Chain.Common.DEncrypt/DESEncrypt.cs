using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Chain.Common.DEncrypt
{
	public class DESEncrypt
	{
		private static string sKey = "szYiJiaSoft";

		public static string Encrypt(string Text)
		{
			return DESEncrypt.Encrypt(Text, DESEncrypt.sKey);
		}

		public static string Encrypt(string Text, string sKey)
		{
			DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
			byte[] bytes = Encoding.Default.GetBytes(Text);
			dESCryptoServiceProvider.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
			dESCryptoServiceProvider.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(bytes, 0, bytes.Length);
			cryptoStream.FlushFinalBlock();
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = memoryStream.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				byte b = array[i];
				stringBuilder.AppendFormat("{0:X2}", b);
			}
			return stringBuilder.ToString();
		}

		public static string Decrypt(string Text)
		{
			return DESEncrypt.Decrypt(Text, DESEncrypt.sKey);
		}

		public static string Decrypt(string Text, string sKey)
		{
			string result = "";
			try
			{
				DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
				int num = Text.Length / 2;
				byte[] array = new byte[num];
				for (int i = 0; i < num; i++)
				{
					int num2 = Convert.ToInt32(Text.Substring(i * 2, 2), 16);
					array[i] = (byte)num2;
				}
				dESCryptoServiceProvider.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
				dESCryptoServiceProvider.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
				MemoryStream memoryStream = new MemoryStream();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
				cryptoStream.Write(array, 0, array.Length);
				cryptoStream.FlushFinalBlock();
				result = Encoding.Default.GetString(memoryStream.ToArray());
			}
			catch
			{
			}
			return result;
		}

		public static string MD5(string ConvertString)
		{
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			string text = BitConverter.ToString(mD5CryptoServiceProvider.ComputeHash(Encoding.Default.GetBytes(ConvertString)), 4, 8);
			return text.Replace("-", "");
		}

		public static string MD5_SMS(string source)
		{
			string result;
			try
			{
				MD5 mD = new MD5CryptoServiceProvider();
				byte[] value = mD.ComputeHash(Encoding.UTF8.GetBytes(source));
				string text = BitConverter.ToString(value).Replace("-", "");
				result = text;
			}
			catch (Exception)
			{
				result = "0";
			}
			return result;
		}
	}
}
