using System;

namespace Chain.Model
{
	[Serializable]
	public class SysShopMemLevel
	{
		private int _shopmemlevelid;

		private int _shopid;

		private int _memlevelid;

		private decimal _classdiscountpercent;

		private decimal _classpointpercent;

		private decimal _classrechargepointrate;

		public int ShopMemLevelID
		{
			get
			{
				return this._shopmemlevelid;
			}
			set
			{
				this._shopmemlevelid = value;
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

		public decimal ClassRechargePointRate
		{
			get
			{
				return this._classrechargepointrate;
			}
			set
			{
				this._classrechargepointrate = value;
			}
		}
	}
}
