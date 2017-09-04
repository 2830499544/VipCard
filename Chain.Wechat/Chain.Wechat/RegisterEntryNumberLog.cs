using System;

namespace Chain.Wechat
{
	public class RegisterEntryNumberLog : ILog
	{
		public int WrningCount
		{
			get;
			set;
		}

		public RegisterEntryNumberLog(int wrningCount)
		{
			base.LogType = "RegisterEntryNumberLog";
			base.CreateTime = DateTime.Now;
			this.WrningCount = wrningCount;
		}
	}
}
