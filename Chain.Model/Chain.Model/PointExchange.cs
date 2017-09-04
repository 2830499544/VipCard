using System;

namespace Chain.Model
{
	[Serializable]
	public class PointExchange
	{
		private int _exchangeid;

		private int _exchangememid;

		private int _exchangegiftid;

		private int _exchangenumber;

		private int _exchangetotalpoint;

		private int _exchangeshopid;

		private DateTime _exchangetime;

		private int _exchangeuserid;

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

		public int ExchangeMemID
		{
			get
			{
				return this._exchangememid;
			}
			set
			{
				this._exchangememid = value;
			}
		}

		public int ExchangeGiftID
		{
			get
			{
				return this._exchangegiftid;
			}
			set
			{
				this._exchangegiftid = value;
			}
		}

		public int ExchangeNumber
		{
			get
			{
				return this._exchangenumber;
			}
			set
			{
				this._exchangenumber = value;
			}
		}

		public int ExchangeTotalPoint
		{
			get
			{
				return this._exchangetotalpoint;
			}
			set
			{
				this._exchangetotalpoint = value;
			}
		}

		public int ExchangeShopID
		{
			get
			{
				return this._exchangeshopid;
			}
			set
			{
				this._exchangeshopid = value;
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
	}
}
