using System;

namespace Chain.Model
{
	[Serializable]
	public class OrderLog
	{
		private int _orderid;

		private string _orderaccount;

		private int _ordertype;

		private int _ordermemid;

		private decimal _ordertotalmoney;

		private decimal _orderdiscountmoney;

		private bool _orderiscard;

		private decimal _orderpaycard;

		private bool _orderiscash;

		private decimal _orderpaycash;

		private bool _orderisbink;

		private decimal _orderpaybink;

		private decimal _orderpaycoupon;

		private int _orderpoint;

		private string _orderremark;

		private int _orderpaytype;

		private int _ordershopid;

		private DateTime _ordercreatetime;

		private int _orderuserid;

		private string _oldaccount;

		private decimal _ordercardbalance;

		private int _ordercardpoint;

		private int usePoint;

		private decimal usePointAmount;

		public int UsePoint
		{
			get
			{
				return this.usePoint;
			}
			set
			{
				this.usePoint = value;
			}
		}

		public decimal UsePointAmount
		{
			get
			{
				return this.usePointAmount;
			}
			set
			{
				this.usePointAmount = value;
			}
		}

		public int OrderID
		{
			get
			{
				return this._orderid;
			}
			set
			{
				this._orderid = value;
			}
		}

		public string OrderAccount
		{
			get
			{
				return this._orderaccount;
			}
			set
			{
				this._orderaccount = value;
			}
		}

		public int OrderType
		{
			get
			{
				return this._ordertype;
			}
			set
			{
				this._ordertype = value;
			}
		}

		public int OrderMemID
		{
			get
			{
				return this._ordermemid;
			}
			set
			{
				this._ordermemid = value;
			}
		}

		public decimal OrderTotalMoney
		{
			get
			{
				return this._ordertotalmoney;
			}
			set
			{
				this._ordertotalmoney = value;
			}
		}

		public decimal OrderDiscountMoney
		{
			get
			{
				return this._orderdiscountmoney;
			}
			set
			{
				this._orderdiscountmoney = value;
			}
		}

		public bool OrderIsCard
		{
			get
			{
				return this._orderiscard;
			}
			set
			{
				this._orderiscard = value;
			}
		}

		public decimal OrderPayCard
		{
			get
			{
				return this._orderpaycard;
			}
			set
			{
				this._orderpaycard = value;
			}
		}

		public bool OrderIsCash
		{
			get
			{
				return this._orderiscash;
			}
			set
			{
				this._orderiscash = value;
			}
		}

		public decimal OrderPayCash
		{
			get
			{
				return this._orderpaycash;
			}
			set
			{
				this._orderpaycash = value;
			}
		}

		public bool OrderIsBink
		{
			get
			{
				return this._orderisbink;
			}
			set
			{
				this._orderisbink = value;
			}
		}

		public decimal OrderPayBink
		{
			get
			{
				return this._orderpaybink;
			}
			set
			{
				this._orderpaybink = value;
			}
		}

		public decimal OrderPayCoupon
		{
			get
			{
				return this._orderpaycoupon;
			}
			set
			{
				this._orderpaycoupon = value;
			}
		}

		public int OrderPoint
		{
			get
			{
				return this._orderpoint;
			}
			set
			{
				this._orderpoint = value;
			}
		}

		public string OrderRemark
		{
			get
			{
				return this._orderremark;
			}
			set
			{
				this._orderremark = value;
			}
		}

		public int OrderPayType
		{
			get
			{
				return this._orderpaytype;
			}
			set
			{
				this._orderpaytype = value;
			}
		}

		public int OrderShopID
		{
			get
			{
				return this._ordershopid;
			}
			set
			{
				this._ordershopid = value;
			}
		}

		public DateTime OrderCreateTime
		{
			get
			{
				return this._ordercreatetime;
			}
			set
			{
				this._ordercreatetime = value;
			}
		}

		public int OrderUserID
		{
			get
			{
				return this._orderuserid;
			}
			set
			{
				this._orderuserid = value;
			}
		}

		public string OldAccount
		{
			get
			{
				return this._oldaccount;
			}
			set
			{
				this._oldaccount = value;
			}
		}

		public decimal OrderCardBalance
		{
			get
			{
				return this._ordercardbalance;
			}
			set
			{
				this._ordercardbalance = value;
			}
		}

		public int OrderCardPoint
		{
			get
			{
				return this._ordercardpoint;
			}
			set
			{
				this._ordercardpoint = value;
			}
		}
	}
}
