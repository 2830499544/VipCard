using System;

namespace Chain.Model
{
	[Serializable]
	public class MemCount
	{
		private int _countid;

		private int _countmemid;

		private string _countaccount;

		private decimal _counttotalmoney;

		private decimal _countdiscountmoney;

		private bool _countiscard;

		private decimal _countpaycard;

		private bool _countiscash;

		private decimal _countpaycash;

		private bool _countisbink;

		private decimal _countpaybink;

		private decimal _countpaycoupon;

		private int _countpaytype;

		private int _countpoint;

		private string _countremark;

		private int _countshopid;

		private DateTime _countcreatetime;

		private int _countuserid;

		public int CountID
		{
			get
			{
				return this._countid;
			}
			set
			{
				this._countid = value;
			}
		}

		public int CountMemID
		{
			get
			{
				return this._countmemid;
			}
			set
			{
				this._countmemid = value;
			}
		}

		public string CountAccount
		{
			get
			{
				return this._countaccount;
			}
			set
			{
				this._countaccount = value;
			}
		}

		public decimal CountTotalMoney
		{
			get
			{
				return this._counttotalmoney;
			}
			set
			{
				this._counttotalmoney = value;
			}
		}

		public decimal CountDiscountMoney
		{
			get
			{
				return this._countdiscountmoney;
			}
			set
			{
				this._countdiscountmoney = value;
			}
		}

		public bool CountIsCard
		{
			get
			{
				return this._countiscard;
			}
			set
			{
				this._countiscard = value;
			}
		}

		public decimal CountPayCard
		{
			get
			{
				return this._countpaycard;
			}
			set
			{
				this._countpaycard = value;
			}
		}

		public bool CountIsCash
		{
			get
			{
				return this._countiscash;
			}
			set
			{
				this._countiscash = value;
			}
		}

		public decimal CountPayCash
		{
			get
			{
				return this._countpaycash;
			}
			set
			{
				this._countpaycash = value;
			}
		}

		public bool CountIsBink
		{
			get
			{
				return this._countisbink;
			}
			set
			{
				this._countisbink = value;
			}
		}

		public decimal CountPayBink
		{
			get
			{
				return this._countpaybink;
			}
			set
			{
				this._countpaybink = value;
			}
		}

		public decimal CountPayCoupon
		{
			get
			{
				return this._countpaycoupon;
			}
			set
			{
				this._countpaycoupon = value;
			}
		}

		public int CountPayType
		{
			get
			{
				return this._countpaytype;
			}
			set
			{
				this._countpaytype = value;
			}
		}

		public int CountPoint
		{
			get
			{
				return this._countpoint;
			}
			set
			{
				this._countpoint = value;
			}
		}

		public string CountRemark
		{
			get
			{
				return this._countremark;
			}
			set
			{
				this._countremark = value;
			}
		}

		public int CountShopID
		{
			get
			{
				return this._countshopid;
			}
			set
			{
				this._countshopid = value;
			}
		}

		public DateTime CountCreateTime
		{
			get
			{
				return this._countcreatetime;
			}
			set
			{
				this._countcreatetime = value;
			}
		}

		public int CountUserID
		{
			get
			{
				return this._countuserid;
			}
			set
			{
				this._countuserid = value;
			}
		}
	}
}
