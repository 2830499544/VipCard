using System;

namespace Chain.Model
{
	[Serializable]
	public class SysLog
	{
		private int _logid;

		private int _loguserid;

		private int _logactionid;

		private string _logtype;

		private string _logdetail;

		private int _logshopid;

		private DateTime _logcreatetime;

		private string _logipadress;

		public int LogID
		{
			get
			{
				return this._logid;
			}
			set
			{
				this._logid = value;
			}
		}

		public int LogUserID
		{
			get
			{
				return this._loguserid;
			}
			set
			{
				this._loguserid = value;
			}
		}

		public int LogActionID
		{
			get
			{
				return this._logactionid;
			}
			set
			{
				this._logactionid = value;
			}
		}

		public string LogType
		{
			get
			{
				return this._logtype;
			}
			set
			{
				this._logtype = value;
			}
		}

		public string LogDetail
		{
			get
			{
				return this._logdetail;
			}
			set
			{
				this._logdetail = value;
			}
		}

		public int LogShopID
		{
			get
			{
				return this._logshopid;
			}
			set
			{
				this._logshopid = value;
			}
		}

		public DateTime LogCreateTime
		{
			get
			{
				return this._logcreatetime;
			}
			set
			{
				this._logcreatetime = value;
			}
		}

		public string LogIPAdress
		{
			get
			{
				return this._logipadress;
			}
			set
			{
				this._logipadress = value;
			}
		}
	}
}
