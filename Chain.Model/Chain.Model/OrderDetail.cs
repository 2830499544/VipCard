using System;

namespace Chain.Model
{
	[Serializable]
	public class OrderDetail
	{
		private int _orderdetailid;

		private int _orderid;

		private int _goodsid;

		private decimal _orderdetailprice;

		private int _orderdetailpoint;

		private decimal _orderdetaildiscountprice;

		private decimal _orderdetailnumber;

		private int _orderdetailtype;

		public int OrderDetailID
		{
			get
			{
				return this._orderdetailid;
			}
			set
			{
				this._orderdetailid = value;
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

		public int GoodsID
		{
			get
			{
				return this._goodsid;
			}
			set
			{
				this._goodsid = value;
			}
		}

		public decimal OrderDetailPrice
		{
			get
			{
				return this._orderdetailprice;
			}
			set
			{
				this._orderdetailprice = value;
			}
		}

		public int OrderDetailPoint
		{
			get
			{
				return this._orderdetailpoint;
			}
			set
			{
				this._orderdetailpoint = value;
			}
		}

		public decimal OrderDetailDiscountPrice
		{
			get
			{
				return this._orderdetaildiscountprice;
			}
			set
			{
				this._orderdetaildiscountprice = value;
			}
		}

		public decimal OrderDetailNumber
		{
			get
			{
				return this._orderdetailnumber;
			}
			set
			{
				this._orderdetailnumber = value;
			}
		}

		public int OrderDetailType
		{
			get
			{
				return this._orderdetailtype;
			}
			set
			{
				this._orderdetailtype = value;
			}
		}
	}
}
