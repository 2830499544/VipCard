using System;

namespace Chain.Model
{
	[Serializable]
	public class GoodsLogDetail
	{
		private int _goodslogdetailid;

		private int _goodsid;

		private decimal _goodsinprice;

		private decimal _goodsoutprice;

		private decimal _goodsnumber = 0m;

		private int _goodslogid;

		public int GoodsLogDetailID
		{
			get
			{
				return this._goodslogdetailid;
			}
			set
			{
				this._goodslogdetailid = value;
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

		public decimal GoodsInPrice
		{
			get
			{
				return this._goodsinprice;
			}
			set
			{
				this._goodsinprice = value;
			}
		}

		public decimal GoodsOutPrice
		{
			get
			{
				return this._goodsoutprice;
			}
			set
			{
				this._goodsoutprice = value;
			}
		}

		public decimal GoodsNumber
		{
			get
			{
				return this._goodsnumber;
			}
			set
			{
				this._goodsnumber = value;
			}
		}

		public int GoodsLogID
		{
			get
			{
				return this._goodslogid;
			}
			set
			{
				this._goodslogid = value;
			}
		}
	}
}
