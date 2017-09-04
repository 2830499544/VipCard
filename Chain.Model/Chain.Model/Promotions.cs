using System;

namespace Chain.Model
{
	[Serializable]
	public class Promotions
	{
		private int _promotionsid;

		private string _promotionstitle;

		private DateTime _promotionsstart;

		private DateTime _promotionsend;

		private int _promotionsmemlevel;

		private int _promotionstype;

		private DateTime _promotionstime;

		private string promotionsDesc;

		private string promotionsRemark;

		private string shopList;

		private string promotionsPhoto;

		private int createUserID;

		public string PromotionsDesc
		{
			get
			{
				return this.promotionsDesc;
			}
			set
			{
				this.promotionsDesc = value;
			}
		}

		public string PromotionsRemark
		{
			get
			{
				return this.promotionsRemark;
			}
			set
			{
				this.promotionsRemark = value;
			}
		}

		public string ShopList
		{
			get
			{
				return this.shopList;
			}
			set
			{
				this.shopList = value;
			}
		}

		public string PromotionsPhoto
		{
			get
			{
				return this.promotionsPhoto;
			}
			set
			{
				this.promotionsPhoto = value;
			}
		}

		public int CreateUserID
		{
			get
			{
				return this.createUserID;
			}
			set
			{
				this.createUserID = value;
			}
		}

		public int PromotionsID
		{
			get
			{
				return this._promotionsid;
			}
			set
			{
				this._promotionsid = value;
			}
		}

		public string PromotionsTitle
		{
			get
			{
				return this._promotionstitle;
			}
			set
			{
				this._promotionstitle = value;
			}
		}

		public DateTime PromotionsStart
		{
			get
			{
				return this._promotionsstart;
			}
			set
			{
				this._promotionsstart = value;
			}
		}

		public DateTime PromotionsEnd
		{
			get
			{
				return this._promotionsend;
			}
			set
			{
				this._promotionsend = value;
			}
		}

		public int PromotionsMemLevel
		{
			get
			{
				return this._promotionsmemlevel;
			}
			set
			{
				this._promotionsmemlevel = value;
			}
		}

		public int PromotionsType
		{
			get
			{
				return this._promotionstype;
			}
			set
			{
				this._promotionstype = value;
			}
		}

		public DateTime PromotionsTime
		{
			get
			{
				return this._promotionstime;
			}
			set
			{
				this._promotionstime = value;
			}
		}
	}
}
