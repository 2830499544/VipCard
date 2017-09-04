using System;

namespace Chain.Wechat
{
	public class RegisterEntryCodeLog : ILog
	{
		public int WrningCount
		{
			get;
			set;
		}

		public string RandomCode
		{
			get;
			set;
		}

		public string MobileNumber
		{
			get;
			set;
		}

		public RegisterEntryCodeLog(int wrningCount, string randomCode, string mobileNumber)
		{
			base.LogType = "RegisterEntryCodeLog";
			base.CreateTime = DateTime.Now;
			this.WrningCount = wrningCount;
			this.RandomCode = randomCode;
			this.MobileNumber = mobileNumber;
		}
	}
}
