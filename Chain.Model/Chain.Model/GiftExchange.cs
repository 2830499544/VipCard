using System;

namespace Chain.Model
{
	[Serializable]
	public class GiftExchange
	{
		private int _exchangeid;

		private int _memid;

		private string _exchangetelephone;

		private string _exchangeaddress;

		private string _exchangeaccount;

		private int _exchangeallnumber;

		private int _exchangeallpoint;

		private DateTime _applicationtime;

		private string _applicationremark;

		private int _exchangestatus;

		private DateTime _exchangetime;

		private int _exchangeuserid;

		private string _exchangeremark;

		private int _exchangetype;

		private int _shopid;

		private string memname;

		private int addressID;

		public int ExchangeID
		{
			get
			{
				return this._exchangeid;
			}
			set
			{
				this._exchangeid = value;
			}
		}

		public int MemID
		{
			get
			{
				return this._memid;
			}
			set
			{
				this._memid = value;
			}
		}

		public string ExchangeTelePhone
		{
			get
			{
				return this._exchangetelephone;
			}
			set
			{
				this._exchangetelephone = value;
			}
		}

		public string ExchangeAddress
		{
			get
			{
				return this._exchangeaddress;
			}
			set
			{
				this._exchangeaddress = value;
			}
		}

		public string ExchangeAccount
		{
			get
			{
				return this._exchangeaccount;
			}
			set
			{
				this._exchangeaccount = value;
			}
		}

		public int ExchangeAllNumber
		{
			get
			{
				return this._exchangeallnumber;
			}
			set
			{
				this._exchangeallnumber = value;
			}
		}

		public int ExchangeAllPoint
		{
			get
			{
				return this._exchangeallpoint;
			}
			set
			{
				this._exchangeallpoint = value;
			}
		}

		public DateTime ApplicationTime
		{
			get
			{
				return this._applicationtime;
			}
			set
			{
				this._applicationtime = value;
			}
		}

		public string ApplicationRemark
		{
			get
			{
				return this._applicationremark;
			}
			set
			{
				this._applicationremark = value;
			}
		}

		public int ExchangeStatus
		{
			get
			{
				return this._exchangestatus;
			}
			set
			{
				this._exchangestatus = value;
			}
		}

		public DateTime ExchangeTime
		{
			get
			{
				return this._exchangetime;
			}
			set
			{
				this._exchangetime = value;
			}
		}

		public int ExchangeUserID
		{
			get
			{
				return this._exchangeuserid;
			}
			set
			{
				this._exchangeuserid = value;
			}
		}

		public string ExchangeRemark
		{
			get
			{
				return this._exchangeremark;
			}
			set
			{
				this._exchangeremark = value;
			}
		}

		public int ExchangeType
		{
			get
			{
				return this._exchangetype;
			}
			set
			{
				this._exchangetype = value;
			}
		}

		public string MemName
		{
			get
			{
				return this.memname;
			}
			set
			{
				this.memname = value;
			}
		}

		public int AddressID
		{
			get
			{
				return this.addressID;
			}
			set
			{
				this.addressID = value;
			}
		}

		public int ShopID
		{
			get
			{
				return this._shopid;
			}
			set
			{
				this._shopid = value;
			}
		}
	}
}
