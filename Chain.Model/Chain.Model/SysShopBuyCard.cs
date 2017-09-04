using System;

namespace Chain.Model
{
	[Serializable]
	public class SysShopBuyCard
	{
		private int _buycardid;

		private string _startcardnumber;

		private string _endcardnumber;

		private int _buycardshopid;

		private int _userid;

		private string _remark;

		private DateTime _buycardtime;

		private decimal _buycardmoney;

		private int buyType;

		public int BuyCardID
		{
			get
			{
				return this._buycardid;
			}
			set
			{
				this._buycardid = value;
			}
		}

		public string StartCardNumber
		{
			get
			{
				return this._startcardnumber;
			}
			set
			{
				this._startcardnumber = value;
			}
		}

		public string EndCardNumber
		{
			get
			{
				return this._endcardnumber;
			}
			set
			{
				this._endcardnumber = value;
			}
		}

		public int BuyCardShopid
		{
			get
			{
				return this._buycardshopid;
			}
			set
			{
				this._buycardshopid = value;
			}
		}

		public int BuyType
		{
			get
			{
				return this.buyType;
			}
			set
			{
				this.buyType = value;
			}
		}

		public int UserID
		{
			get
			{
				return this._userid;
			}
			set
			{
				this._userid = value;
			}
		}

		public string Remark
		{
			get
			{
				return this._remark;
			}
			set
			{
				this._remark = value;
			}
		}

		public DateTime BuyCardTime
		{
			get
			{
				return this._buycardtime;
			}
			set
			{
				this._buycardtime = value;
			}
		}

		public decimal BuyCardMoney
		{
			get
			{
				return this._buycardmoney;
			}
			set
			{
				this._buycardmoney = value;
			}
		}
	}
}
