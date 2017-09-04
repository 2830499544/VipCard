using System;

namespace Chain.Model
{
	[Serializable]
	public class MicroWebsiteOrderLogDetail
	{
		private int _microorderlogdetailid;

		private int _microorderid;

		private int _microgoodsid;

		private decimal _microorderdetailprice;

		private int _microorderdetailpoint;

		private decimal _microorderdetaildiscountprice;

		private int _microorderdetailnumber;

		public int MicroOrderLogDetailID
		{
			get
			{
				return this._microorderlogdetailid;
			}
			set
			{
				this._microorderlogdetailid = value;
			}
		}

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

		public int MicroGoodsID
		{
			get
			{
				return this._microgoodsid;
			}
			set
			{
				this._microgoodsid = value;
			}
		}

		public decimal MicroOrderDetailPrice
		{
			get
			{
				return this._microorderdetailprice;
			}
			set
			{
				this._microorderdetailprice = value;
			}
		}

		public int MicroOrderDetailPoint
		{
			get
			{
				return this._microorderdetailpoint;
			}
			set
			{
				this._microorderdetailpoint = value;
			}
		}

		public decimal MicroOrderDetailDiscountPrice
		{
			get
			{
				return this._microorderdetaildiscountprice;
			}
			set
			{
				this._microorderdetaildiscountprice = value;
			}
		}

		public int MicroOrderDetailNumber
		{
			get
			{
				return this._microorderdetailnumber;
			}
			set
			{
				this._microorderdetailnumber = value;
			}
		}
	}
}
