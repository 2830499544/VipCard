using System;

namespace Chain.Model
{
	[Serializable]
	public class CouponList
	{
		private int _cid;

		private int _couponid;

		private string _coupon;

		private bool _couponyf;

		private bool _couponsy;

		private int _couponmid;

		private DateTime _conponsendtime;

		private DateTime _conponusetime;

		private string _couponorderaccount;

		private int _coupontype = 0;

		private decimal _couponnumber;

		private int _couponeffective;

		private DateTime? _couponstart;

		private DateTime? _couponend;

		private int _coupondaynum;

		private decimal _couponminmoney;

		public int CID
		{
			get
			{
				return this._cid;
			}
			set
			{
				this._cid = value;
			}
		}

		public int CouPonID
		{
			get
			{
				return this._couponid;
			}
			set
			{
				this._couponid = value;
			}
		}

		public string CouPon
		{
			get
			{
				return this._coupon;
			}
			set
			{
				this._coupon = value;
			}
		}

		public bool CouPonYF
		{
			get
			{
				return this._couponyf;
			}
			set
			{
				this._couponyf = value;
			}
		}

		public bool CouPonSY
		{
			get
			{
				return this._couponsy;
			}
			set
			{
				this._couponsy = value;
			}
		}

		public int CouPonMID
		{
			get
			{
				return this._couponmid;
			}
			set
			{
				this._couponmid = value;
			}
		}

		public DateTime ConPonSendTime
		{
			get
			{
				return this._conponsendtime;
			}
			set
			{
				this._conponsendtime = value;
			}
		}

		public DateTime ConPonUseTime
		{
			get
			{
				return this._conponusetime;
			}
			set
			{
				this._conponusetime = value;
			}
		}

		public string CouPonOrderAccount
		{
			get
			{
				return this._couponorderaccount;
			}
			set
			{
				this._couponorderaccount = value;
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
	}
}
