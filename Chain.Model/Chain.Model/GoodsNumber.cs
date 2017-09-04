using System;

namespace Chain.Model
{
	[Serializable]
	public class GoodsNumber
	{
		private int _id;

		private int _goodsid;

		private int _shopid;

		private decimal _number = 0m;

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

		public int ShopID
		{
			get
			{
				return this._shopid;
			}
			set
			{
				this._shopid = value;
			}
		}

		public decimal Number
		{
			get
			{
				return this._number;
			}
			set
			{
				this._number = value;
			}
		}
	}
}
