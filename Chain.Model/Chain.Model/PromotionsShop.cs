using System;

namespace Chain.Model
{
	[Serializable]
	public class PromotionsShop
	{
		private int shopid;

		private int promotionsID;

		public int ShopID
		{
			get
			{
				return this.shopid;
			}
			set
			{
				this.shopid = value;
			}
		}

		public int PromotionsID
		{
			get
			{
				return this.promotionsID;
			}
			set
			{
				this.promotionsID = value;
			}
		}
	}
}
