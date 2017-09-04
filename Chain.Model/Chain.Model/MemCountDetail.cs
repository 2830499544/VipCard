using System;

namespace Chain.Model
{
	[Serializable]
	public class MemCountDetail
	{
		private int _countdetailid;

		private int _countdetailcountid;

		private int _countdetailgoodsid;

		private int _countdetailmemid;

		private int _countdetailtotalnumber;

		private int _countdetailnumber;

		private decimal _countdetaildiscountmoney;

		private int _countdetailpoint;

		private DateTime _countcreatetime;

		public int CountDetailID
		{
			get
			{
				return this._countdetailid;
			}
			set
			{
				this._countdetailid = value;
			}
		}

		public int CountDetailCountID
		{
			get
			{
				return this._countdetailcountid;
			}
			set
			{
				this._countdetailcountid = value;
			}
		}

		public int CountDetailGoodsID
		{
			get
			{
				return this._countdetailgoodsid;
			}
			set
			{
				this._countdetailgoodsid = value;
			}
		}

		public int CountDetailMemID
		{
			get
			{
				return this._countdetailmemid;
			}
			set
			{
				this._countdetailmemid = value;
			}
		}

		public int CountDetailTotalNumber
		{
			get
			{
				return this._countdetailtotalnumber;
			}
			set
			{
				this._countdetailtotalnumber = value;
			}
		}

		public int CountDetailNumber
		{
			get
			{
				return this._countdetailnumber;
			}
			set
			{
				this._countdetailnumber = value;
			}
		}

		public decimal CountDetailDiscountMoney
		{
			get
			{
				return this._countdetaildiscountmoney;
			}
			set
			{
				this._countdetaildiscountmoney = value;
			}
		}

		public int CountDetailPoint
		{
			get
			{
				return this._countdetailpoint;
			}
			set
			{
				this._countdetailpoint = value;
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
	}
}
