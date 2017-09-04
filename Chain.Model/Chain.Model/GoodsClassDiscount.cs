using System;

namespace Chain.Model
{
	[Serializable]
	public class GoodsClassDiscount
	{
		private int _classdiscountid;

		private int _goodsclassid;

		private int _memlevelid;

		private int _discountshopid;

		private decimal _classdiscountpercent;

		private decimal _classpointpercent;

		public int ClassDiscountID
		{
			get
			{
				return this._classdiscountid;
			}
			set
			{
				this._classdiscountid = value;
			}
		}

		public int GoodsClassID
		{
			get
			{
				return this._goodsclassid;
			}
			set
			{
				this._goodsclassid = value;
			}
		}

		public int MemLevelID
		{
			get
			{
				return this._memlevelid;
			}
			set
			{
				this._memlevelid = value;
			}
		}

		public int DiscountShopID
		{
			get
			{
				return this._discountshopid;
			}
			set
			{
				this._discountshopid = value;
			}
		}

		public decimal ClassDiscountPercent
		{
			get
			{
				return this._classdiscountpercent;
			}
			set
			{
				this._classdiscountpercent = value;
			}
		}

		public decimal ClassPointPercent
		{
			get
			{
				return this._classpointpercent;
			}
			set
			{
				this._classpointpercent = value;
			}
		}
	}
}
