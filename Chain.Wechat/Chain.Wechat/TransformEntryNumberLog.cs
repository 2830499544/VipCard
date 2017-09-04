using System;

namespace Chain.Wechat
{
	public class TransformEntryNumberLog : ILog
	{
		public int WrningCount
		{
			get;
			set;
		}

		public TransformEntryNumberLog(int wrningCount)
		{
			base.LogType = "TransformEntryNumberLog";
			base.CreateTime = DateTime.Now;
			this.WrningCount = wrningCount;
		}
	}
}
