using System;

namespace Chain.Wechat
{
	public class TransformEntryPwdLog : ILog
	{
		public int WrningCount
		{
			get;
			set;
		}

		public string MemCard
		{
			get;
			set;
		}

		public TransformEntryPwdLog(int wrningCount, string memcard)
		{
			base.LogType = "TransformEntryPwdLog";
			base.CreateTime = DateTime.Now;
			this.WrningCount = wrningCount;
			this.MemCard = memcard;
		}
	}
}
