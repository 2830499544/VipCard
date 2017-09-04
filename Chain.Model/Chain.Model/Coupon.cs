using System;

namespace Chain.Model
{
	[Serializable]
	public class Coupon
	{
		private int _id;

		private string _coupontitle;

		private int _coupontype = 0;

		private decimal _couponnumber;

		private int _couponpredictnu;

		private int _couponeffective;

		private DateTime? _couponstart;

		private DateTime? _couponend;

		private int _coupondaynum;

		private decimal _couponminmoney;

		private string _couponcontent;

		private int _CouponYF = 0;

		private int _CouponSY = 0;

		private int _CouponShopID;

		private int isGet;

		public int IsGet
		{
			get
			{
				return this.isGet;
			}
			set
			{
				this.isGet = value;
			}
		}

		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		public string CouponTitle
		{
			get
			{
				return this._coupontitle;
			}
			set
			{
				this._coupontitle = value;
			}
		}

		public int CouponType
		{
			get
			{
				return this._coupontype;
			}
			set
			{
				this._coupontype = value;
			}
		}

		public decimal CouponNumber
		{
			get
			{
				return this._couponnumber;
			}
			set
			{
				this._couponnumber = value;
			}
		}

		public int CouponPredictNu
		{
			get
			{
				return this._couponpredictnu;
			}
			set
			{
				this._couponpredictnu = value;
			}
		}

		public int CouponEffective
		{
			get
			{
				return this._couponeffective;
			}
			set
			{
				this._couponeffective = value;
			}
		}

		public DateTime? CouponStart
		{
			get
			{
				return this._couponstart;
			}
			set
			{
				this._couponstart = value;
			}
		}

		public DateTime? CouponEnd
		{
			get
			{
				return this._couponend;
			}
			set
			{
				this._couponend = value;
			}
		}

		public int CouponDayNum
		{
			get
			{
				return this._coupondaynum;
			}
			set
			{
				this._coupondaynum = value;
			}
		}

		public decimal CouponMinMoney
		{
			get
			{
				return this._couponminmoney;
			}
			set
			{
				this._couponminmoney = value;
			}
		}

		public string CouponContent
		{
			get
			{
				return this._couponcontent;
			}
			set
			{
				this._couponcontent = value;
			}
		}

		public int CouponYF
		{
			get
			{
				return this._CouponYF;
			}
			set
			{
				this._CouponYF = value;
			}
		}

		public int CouponSY
		{
			get
			{
				return this._CouponSY;
			}
			set
			{
				this._CouponSY = value;
			}
		}

		public int CouponShopID
		{
			get
			{
				return this._CouponShopID;
			}
			set
			{
				this._CouponShopID = value;
			}
		}
	}
}
