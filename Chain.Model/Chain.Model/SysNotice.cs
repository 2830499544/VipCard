using System;

namespace Chain.Model
{
	[Serializable]
	public class SysNotice
	{
		private int _sysnoticeid;

		private string _sysnoticecode;

		private string _sysnotiecename;

		private string _sysnoticetitle;

		private string _sysnoticedetail;

		private DateTime _sysnoticetime;

		public int SysNoticeID
		{
			get
			{
				return this._sysnoticeid;
			}
			set
			{
				this._sysnoticeid = value;
			}
		}

		public string SysNoticeCode
		{
			get
			{
				return this._sysnoticecode;
			}
			set
			{
				this._sysnoticecode = value;
			}
		}

		public string SysNotieceName
		{
			get
			{
				return this._sysnotiecename;
			}
			set
			{
				this._sysnotiecename = value;
			}
		}

		public string SysNoticeTitle
		{
			get
			{
				return this._sysnoticetitle;
			}
			set
			{
				this._sysnoticetitle = value;
			}
		}

		public string SysNoticeDetail
		{
			get
			{
				return this._sysnoticedetail;
			}
			set
			{
				this._sysnoticedetail = value;
			}
		}

		public DateTime SysNoticeTime
		{
			get
			{
				return this._sysnoticetime;
			}
			set
			{
				this._sysnoticetime = value;
			}
		}
	}
}
