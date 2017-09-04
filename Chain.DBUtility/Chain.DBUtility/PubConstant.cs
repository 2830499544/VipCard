using Chain.Common;
using Chain.Common.DEncrypt;
using System;

namespace Chain.DBUtility
{
	public class PubConstant
	{
		public static string ConnectionString
		{
			get
			{
				string text = ConfigHelper.GetValue("ConnString");
				string value = ConfigHelper.GetValue("ConnStringE");
				if (value == "true" && text != "")
				{
					text = DESEncrypt.Decrypt(text);
				}
				return text;
			}
		}

		public static string DBName
		{
			get
			{
				int num = PubConstant.ConnectionString.IndexOf("database=") + 9;
				int length = PubConstant.ConnectionString.IndexOf(";", num) - num;
				return PubConstant.ConnectionString.Substring(num, length);
			}
		}

		public static string ServerIP
		{
			get
			{
				int num = PubConstant.ConnectionString.IndexOf("server=") + 7;
				int length = PubConstant.ConnectionString.IndexOf(";", num) - num;
				return PubConstant.ConnectionString.Substring(num, length);
			}
		}

		public static string Login
		{
			get
			{
				int num = PubConstant.ConnectionString.IndexOf("uid=") + 4;
				int length = PubConstant.ConnectionString.IndexOf(";", num) - num;
				return PubConstant.ConnectionString.Substring(num, length);
			}
		}

		public static string Pwd
		{
			get
			{
				int num = PubConstant.ConnectionString.IndexOf("pwd=") + 4;
				int length = PubConstant.ConnectionString.IndexOf(";", num) - num;
				return PubConstant.ConnectionString.Substring(num, length);
			}
		}

		public static string DoMain
		{
			get
			{
				return DESEncrypt.Decrypt(ConfigHelper.GetValue("SystemDomain"));
			}
		}
	}
}
