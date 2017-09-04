using System;

namespace Chain.Wechat
{
	public abstract class ILog
	{
		public string LogType
		{
			get;
			set;
		}

		public DateTime CreateTime
		{
			get;
			set;
		}
	}
}
