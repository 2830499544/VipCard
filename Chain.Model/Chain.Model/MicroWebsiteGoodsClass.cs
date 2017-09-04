using System;

namespace Chain.Model
{
	[Serializable]
	public class MicroWebsiteGoodsClass
	{
		private int _microgoodsclassid;

		private string _microgoodsclassname;

		private string _microgoodsclassremark;

		private int _microgoodsclassshopid;

		public int MicroGoodsClassID
		{
			get
			{
				return this._microgoodsclassid;
			}
			set
			{
				this._microgoodsclassid = value;
			}
		}

		public string MicroGoodsClassName
		{
			get
			{
				return this._microgoodsclassname;
			}
			set
			{
				this._microgoodsclassname = value;
			}
		}

		public string MicroGoodsClassRemark
		{
			get
			{
				return this._microgoodsclassremark;
			}
			set
			{
				this._microgoodsclassremark = value;
			}
		}

		public int MicroGoodsClassShopID
		{
			get
			{
				return this._microgoodsclassshopid;
			}
			set
			{
				this._microgoodsclassshopid = value;
			}
		}
	}
}
