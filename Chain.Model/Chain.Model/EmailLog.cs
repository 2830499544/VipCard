using System;

namespace Chain.Model
{
	[Serializable]
	public class EmailLog
	{
		private int _emailid;

		private string _emailadress;

		private string _emailtitle;

		private string _emailcontent;

		private int _emailstate;

		private DateTime _emailsendtime;

		private int _emailshopid;

		private int _emailuserid;

		private int _emailcount;

		public int EmailID
		{
			get
			{
				return this._emailid;
			}
			set
			{
				this._emailid = value;
			}
		}

		public string EmailAdress
		{
			get
			{
				return this._emailadress;
			}
			set
			{
				this._emailadress = value;
			}
		}

		public string EmailTitle
		{
			get
			{
				return this._emailtitle;
			}
			set
			{
				this._emailtitle = value;
			}
		}

		public string EmailContent
		{
			get
			{
				return this._emailcontent;
			}
			set
			{
				this._emailcontent = value;
			}
		}

		public int EmailState
		{
			get
			{
				return this._emailstate;
			}
			set
			{
				this._emailstate = value;
			}
		}

		public DateTime EmailSendTime
		{
			get
			{
				return this._emailsendtime;
			}
			set
			{
				this._emailsendtime = value;
			}
		}

		public int EmailShopID
		{
			get
			{
				return this._emailshopid;
			}
			set
			{
				this._emailshopid = value;
			}
		}

		public int EmailUserID
		{
			get
			{
				return this._emailuserid;
			}
			set
			{
				this._emailuserid = value;
			}
		}

		public int EmailCount
		{
			get
			{
				return this._emailcount;
			}
			set
			{
				this._emailcount = value;
			}
		}
	}
}
