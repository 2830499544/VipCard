using System;

namespace Chain.Model
{
	[Serializable]
	public class MicroWebsiteGoods
	{
		private int _microgoodsid;

		private string _microgoodscode;

		private int _microgoodsclassid;

		private string _microgoodsname;

		private decimal _microsalepercet;

		private decimal _microprice;

		private int _micropoint;

		private decimal _microgoodsbidprice;

		private string _microgoodsremark;

		private string _microgoodspicture;

		private DateTime _microgoodscreatetime;

		private int _microgoodsshopid;

		public int MicroGoodsID
		{
			get
			{
				return this._microgoodsid;
			}
			set
			{
				this._microgoodsid = value;
			}
		}

		public string MicroGoodsCode
		{
			get
			{
				return this._microgoodscode;
			}
			set
			{
				this._microgoodscode = value;
			}
		}

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

		public string MicroGoodsName
		{
			get
			{
				return this._microgoodsname;
			}
			set
			{
				this._microgoodsname = value;
			}
		}

		public decimal MicroSalePrice
		{
			get
			{
				return this._microsalepercet;
			}
			set
			{
				this._microsalepercet = value;
			}
		}

		public decimal MicroPrice
		{
			get
			{
				return this._microprice;
			}
			set
			{
				this._microprice = value;
			}
		}

		public int MicroPoint
		{
			get
			{
				return this._micropoint;
			}
			set
			{
				this._micropoint = value;
			}
		}

		public decimal MicroGoodsBidPrice
		{
			get
			{
				return this._microgoodsbidprice;
			}
			set
			{
				this._microgoodsbidprice = value;
			}
		}

		public string MicroGoodsRemark
		{
			get
			{
				return this._microgoodsremark;
			}
			set
			{
				this._microgoodsremark = value;
			}
		}

		public string MicroGoodsPicture
		{
			get
			{
				return this._microgoodspicture;
			}
			set
			{
				this._microgoodspicture = value;
			}
		}

		public DateTime MicroGoodsCreateTime
		{
			get
			{
				return this._microgoodscreatetime;
			}
			set
			{
				this._microgoodscreatetime = value;
			}
		}

		public int MicroGoodsShopID
		{
			get
			{
				return this._microgoodsshopid;
			}
			set
			{
				this._microgoodsshopid = value;
			}
		}
	}
}
