using System;

namespace Chain.Model
{
	[Serializable]
	public class SysShopAuthority
	{
		private int _shopauthorityid;

		private int? _shopauthorityshopid;

		private string _shopauthoritydata;

		public int ShopAuthorityID
		{
			get
			{
				return this._shopauthorityid;
			}
			set
			{
				this._shopauthorityid = value;
			}
		}

		public int? ShopAuthorityShopID
		{
			get
			{
				return this._shopauthorityshopid;
			}
			set
			{
				this._shopauthorityshopid = value;
			}
		}

		public string ShopAuthorityData
		{
			get
			{
				return this._shopauthoritydata;
			}
			set
			{
				this._shopauthoritydata = value;
			}
		}
	}
}
