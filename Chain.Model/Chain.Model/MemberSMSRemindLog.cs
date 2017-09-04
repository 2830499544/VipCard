using System;

namespace Chain.Model
{
	[Serializable]
	public class MemberSMSRemindLog
	{
		private int _membersmsremindid;

		private int _membersmsremindmemid;

		private string _membersmsremindmobile;

		private string _membersmsremindcontent;

		private int _membersmsremindshopid;

		private DateTime _membersmsremindtime;

		private int _membersmsreminduserid;

		private int _membersmsremindamount;

		private int _membersmsremindallamount;

		private int _membersmsremindtype;

		private DateTime _membersmsremindbirthday;

		public int MemberSMSRemindID
		{
			get
			{
				return this._membersmsremindid;
			}
			set
			{
				this._membersmsremindid = value;
			}
		}

		public int MemberSMSRemindMemID
		{
			get
			{
				return this._membersmsremindmemid;
			}
			set
			{
				this._membersmsremindmemid = value;
			}
		}

		public string MemberSMSRemindMobile
		{
			get
			{
				return this._membersmsremindmobile;
			}
			set
			{
				this._membersmsremindmobile = value;
			}
		}

		public string MemberSMSRemindContent
		{
			get
			{
				return this._membersmsremindcontent;
			}
			set
			{
				this._membersmsremindcontent = value;
			}
		}

		public int MemberSMSRemindShopID
		{
			get
			{
				return this._membersmsremindshopid;
			}
			set
			{
				this._membersmsremindshopid = value;
			}
		}

		public DateTime MemberSMSRemindTime
		{
			get
			{
				return this._membersmsremindtime;
			}
			set
			{
				this._membersmsremindtime = value;
			}
		}

		public int MemberSMSRemindUserID
		{
			get
			{
				return this._membersmsreminduserid;
			}
			set
			{
				this._membersmsreminduserid = value;
			}
		}

		public int MemberSMSRemindAmount
		{
			get
			{
				return this._membersmsremindamount;
			}
			set
			{
				this._membersmsremindamount = value;
			}
		}

		public int MemberSMSRemindAllAmount
		{
			get
			{
				return this._membersmsremindallamount;
			}
			set
			{
				this._membersmsremindallamount = value;
			}
		}

		public int MemberSMSRemindType
		{
			get
			{
				return this._membersmsremindtype;
			}
			set
			{
				this._membersmsremindtype = value;
			}
		}

		public DateTime MemberSMSRemindBirthday
		{
			get
			{
				return this._membersmsremindbirthday;
			}
			set
			{
				this._membersmsremindbirthday = value;
			}
		}
	}
}
