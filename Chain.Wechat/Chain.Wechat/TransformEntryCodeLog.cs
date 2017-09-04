using System;

namespace Chain.Wechat
{
	public class TransformEntryCodeLog : ILog
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

		public string MemCard
		{
			get;
			set;
		}

		public TransformEntryCodeLog(int wrningCount, string randomCode, string memcard)
		{
			base.LogType = "TransformEntryCodeLog";
			base.CreateTime = DateTime.Now;
			this.WrningCount = wrningCount;
			this.RandomCode = randomCode;
			this.MemCard = memcard;
		}
	}
}
