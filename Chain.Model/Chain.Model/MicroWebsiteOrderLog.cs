using System;

namespace Chain.Model
{
	[Serializable]
	public class MicroWebsiteOrderLog
	{
		private int _microorderid;

		private string _microorderaccount;

		private int _microordertype;

		private int _microordermemid;

		private decimal _microordertotalmoney;

		private decimal _microorderdiscountmoney;

		private bool _microorderiscard;

		private decimal _microorderpaycard;

		private bool _microorderiscash;

		private decimal _microorderpaycash;

		private bool _microorderisbink;

		private decimal _microorderpaybink;

		private decimal _microorderpaycoupon;

		private int _microorderpoint;

		private string _microorderremark;

		private int _microordershopid;

		private int _microorderuserid;

		private DateTime _microordercreatetime;

		private string _microoldaccount;

		private decimal _microordercardbalance;

		private int _microordercardpoint;

		private string _microordername;

		private string _microordermobile;

		private string _microorderadress;

		private int _microorderstatus;

		private DateTime _microorderpasscreatetime;

		private string _microordermark;

		private DateTime _microorderpaycreatetime;

		public int MicroOrderID
		{
			get
			{
				return this._microorderid;
			}
			set
			{
				this._microorderid = value;
			}
		}

		public string MicroOrderAccount
		{
			get
			{
				return this._microorderaccount;
			}
			set
			{
				this._microorderaccount = value;
			}
		}

		public int MicroOrderType
		{
			get
			{
				return this._microordertype;
			}
			set
			{
				this._microordertype = value;
			}
		}

		public int MicroOrderMemID
		{
			get
			{
				return this._microordermemid;
			}
			set
			{
				this._microordermemid = value;
			}
		}

		public decimal MicroOrderTotalMoney
		{
			get
			{
				return this._microordertotalmoney;
			}
			set
			{
				this._microordertotalmoney = value;
			}
		}

		public decimal MicroOrderDiscountMoney
		{
			get
			{
				return this._microorderdiscountmoney;
			}
			set
			{
				this._microorderdiscountmoney = value;
			}
		}

		public bool MicroOrderIsCard
		{
			get
			{
				return this._microorderiscard;
			}
			set
			{
				this._microorderiscard = value;
			}
		}

		public decimal MicroOrderPayCard
		{
			get
			{
				return this._microorderpaycard;
			}
			set
			{
				this._microorderpaycard = value;
			}
		}

		public bool MicroOrderIsCash
		{
			get
			{
				return this._microorderiscash;
			}
			set
			{
				this._microorderiscash = value;
			}
		}

		public decimal MicroOrderPayCash
		{
			get
			{
				return this._microorderpaycash;
			}
			set
			{
				this._microorderpaycash = value;
			}
		}

		public bool MicroOrderIsBink
		{
			get
			{
				return this._microorderisbink;
			}
			set
			{
				this._microorderisbink = value;
			}
		}

		public decimal MicroOrderPayBink
		{
			get
			{
				return this._microorderpaybink;
			}
			set
			{
				this._microorderpaybink = value;
			}
		}

		public decimal MicroOrderPayCoupon
		{
			get
			{
				return this._microorderpaycoupon;
			}
			set
			{
				this._microorderpaycoupon = value;
			}
		}

		public int MicroOrderPoint
		{
			get
			{
				return this._microorderpoint;
			}
			set
			{
				this._microorderpoint = value;
			}
		}

		public string MicroOrderRemark
		{
			get
			{
				return this._microorderremark;
			}
			set
			{
				this._microorderremark = value;
			}
		}

		public int MicroOrderShopID
		{
			get
			{
				return this._microordershopid;
			}
			set
			{
				this._microordershopid = value;
			}
		}

		public int MicroOrderUserID
		{
			get
			{
				return this._microorderuserid;
			}
			set
			{
				this._microorderuserid = value;
			}
		}

		public DateTime MicroOrderCreateTime
		{
			get
			{
				return this._microordercreatetime;
			}
			set
			{
				this._microordercreatetime = value;
			}
		}

		public string MicroOldAccount
		{
			get
			{
				return this._microoldaccount;
			}
			set
			{
				this._microoldaccount = value;
			}
		}

		public decimal MicroOrderCardBalance
		{
			get
			{
				return this._microordercardbalance;
			}
			set
			{
				this._microordercardbalance = value;
			}
		}

		public int MicroOrderCardPoint
		{
			get
			{
				return this._microordercardpoint;
			}
			set
			{
				this._microordercardpoint = value;
			}
		}

		public string MicroOrderName
		{
			get
			{
				return this._microordername;
			}
			set
			{
				this._microordername = value;
			}
		}

		public string MicroOrderMobile
		{
			get
			{
				return this._microordermobile;
			}
			set
			{
				this._microordermobile = value;
			}
		}

		public string MicroOrderAdress
		{
			get
			{
				return this._microorderadress;
			}
			set
			{
				this._microorderadress = value;
			}
		}

		public int MicroOrderStatus
		{
			get
			{
				return this._microorderstatus;
			}
			set
			{
				this._microorderstatus = value;
			}
		}

		public DateTime MicroOrderPassCreateTime
		{
			get
			{
				return this._microorderpasscreatetime;
			}
			set
			{
				this._microorderpasscreatetime = value;
			}
		}

		public string MicroOrderMark
		{
			get
			{
				return this._microordermark;
			}
			set
			{
				this._microordermark = value;
			}
		}

		public DateTime MicroOrderPayCreateTime
		{
			get
			{
				return this._microorderpaycreatetime;
			}
			set
			{
				this._microorderpaycreatetime = value;
			}
		}
	}
}
