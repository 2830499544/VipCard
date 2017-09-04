using Chain.Common.WebReference;
using System;

namespace Chain.Common
{
	public class SMSHelper
	{
		private static SmsService SMS = new SmsService();

		private int SmsType
		{
			get;
			set;
		}

		private string SN
		{
			get;
			set;
		}

		private string Pwd
		{
			get;
			set;
		}

		public SMSHelper(int smsType, string sn, string pwd)
		{
			this.SmsType = smsType;
			this.SN = sn;
			this.Pwd = pwd;
		}

		public bool Send_GXSMS(string MobileList, string Content, string Stime)
		{
			long ticks = DateTime.Now.Ticks;
			string text = SMSHelper.SMS.SendMessage(this.SmsType, this.SN, this.Pwd, MobileList, Content, Stime);
			long ticks2 = DateTime.Now.Ticks;
			new TimeSpan(ticks2 - ticks);
			return text.Length > 10;
		}

		public bool Send_SMS(string MobileList, string Content, string Stime)
		{
			long ticks = DateTime.Now.Ticks;
			string text = SMSHelper.SMS.SendMessage(this.SmsType, this.SN, this.Pwd, MobileList, Content, Stime);
			long ticks2 = DateTime.Now.Ticks;
			new TimeSpan(ticks2 - ticks);
			return text.Length > 10;
		}

		public int GetBalance()
		{
			int result = 0;
			try
			{
				result = Convert.ToInt32(SMSHelper.SMS.GetBalance(this.SmsType, this.SN, this.Pwd));
			}
			catch
			{
			}
			return result;
		}
	}
}
