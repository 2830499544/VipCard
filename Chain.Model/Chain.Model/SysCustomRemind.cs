using System;

namespace Chain.Model
{
	[Serializable]
	public class SysCustomRemind
	{
		private int _customremindid;

		private string _customremindtitle;

		private string _customreminddetail;

		private string _customreminder;

		private DateTime _customremindtime;

		private DateTime _customremindcreatetime;

		private int _customremindshopid;

		private int _customreminduserid;

		public int CustomRemindID
		{
			get
			{
				return this._customremindid;
			}
			set
			{
				this._customremindid = value;
			}
		}

		public string CustomRemindTitle
		{
			get
			{
				return this._customremindtitle;
			}
			set
			{
				this._customremindtitle = value;
			}
		}

		public string CustomRemindDetail
		{
			get
			{
				return this._customreminddetail;
			}
			set
			{
				this._customreminddetail = value;
			}
		}

		public string CustomReminder
		{
			get
			{
				return this._customreminder;
			}
			set
			{
				this._customreminder = value;
			}
		}

		public DateTime CustomRemindTime
		{
			get
			{
				return this._customremindtime;
			}
			set
			{
				this._customremindtime = value;
			}
		}

		public DateTime CustomRemindCreateTime
		{
			get
			{
				return this._customremindcreatetime;
			}
			set
			{
				this._customremindcreatetime = value;
			}
		}

		public int CustomRemindShopID
		{
			get
			{
				return this._customremindshopid;
			}
			set
			{
				this._customremindshopid = value;
			}
		}

		public int CustomRemindUserID
		{
			get
			{
				return this._customreminduserid;
			}
			set
			{
				this._customreminduserid = value;
			}
		}
	}
}
