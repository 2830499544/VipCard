using System;

namespace Chain.Model
{
	[Serializable]
	public class GoodsClassAuthority
	{
		private int _classauthorityid;

		private int _classid;

		private int _shopid;

		public int ClassAuthorityID
		{
			get
			{
				return this._classauthorityid;
			}
			set
			{
				this._classauthorityid = value;
			}
		}

		public int ClassID
		{
			get
			{
				return this._classid;
			}
			set
			{
				this._classid = value;
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
	}
}
