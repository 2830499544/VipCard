using System;

namespace Chain.Model
{
	[Serializable]
	public class GiftExchangeDetail
	{
		private int _exchangedetailid;

		private int _exchangeid;

		private int _exchangegiftid;

		private int _exchangenumber;

		private int _exchangepoint;

		private string _giftname;

		public int ExchangeDetailID
		{
			get
			{
				return this._exchangedetailid;
			}
			set
			{
				this._exchangedetailid = value;
			}
		}

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

		public int ExchangePoint
		{
			get
			{
				return this._exchangepoint;
			}
			set
			{
				this._exchangepoint = value;
			}
		}

		public string Giftname
		{
			get
			{
				return this._giftname;
			}
			set
			{
				this._giftname = value;
			}
		}
	}
}
