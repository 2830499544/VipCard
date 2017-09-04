using System;

namespace Chain.Model
{
	[Serializable]
	public class SmsLog
	{
		private int _smsid;

		private int _smsmemid;

		private string _smsmobile;

		private string _smscontent;

		private int _smsshopid;

		private DateTime _smstime;

		private int _smsuserid;

		private int _smsamount;

		private int _smsallamount;

		public int SmsID
		{
			get
			{
				return this._smsid;
			}
			set
			{
				this._smsid = value;
			}
		}

		public int SmsMemID
		{
			get
			{
				return this._smsmemid;
			}
			set
			{
				this._smsmemid = value;
			}
		}

		public string SmsMobile
		{
			get
			{
				return this._smsmobile;
			}
			set
			{
				this._smsmobile = value;
			}
		}

		public string SmsContent
		{
			get
			{
				return this._smscontent;
			}
			set
			{
				this._smscontent = value;
			}
		}

		public int SmsShopID
		{
			get
			{
				return this._smsshopid;
			}
			set
			{
				this._smsshopid = value;
			}
		}

		public DateTime SmsTime
		{
			get
			{
				return this._smstime;
			}
			set
			{
				this._smstime = value;
			}
		}

		public int SmsUserID
		{
			get
			{
				return this._smsuserid;
			}
			set
			{
				this._smsuserid = value;
			}
		}

		public int SmsAmount
		{
			get
			{
				return this._smsamount;
			}
			set
			{
				this._smsamount = value;
			}
		}

		public int SmsAllAmount
		{
			get
			{
				return this._smsallamount;
			}
			set
			{
				this._smsallamount = value;
			}
		}
	}
}
