using System;

namespace Chain.Model
{
	[Serializable]
	public class GoodsLog
	{
		private int _id;

		private string _goodsaccount;

		private int _type;

		private int _goodsid;

		private decimal _totalprice;

		private int _goodsnumber;

		private string _remark;

		private DateTime _createtime;

		private int _shopid;

		private int _userid;

		private int _changeshopid;

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

		public string GoodsAccount
		{
			get
			{
				return this._goodsaccount;
			}
			set
			{
				this._goodsaccount = value;
			}
		}

		public int Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
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

		public decimal TotalPrice
		{
			get
			{
				return this._totalprice;
			}
			set
			{
				this._totalprice = value;
			}
		}

		public int GoodsNumber
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

		public string Remark
		{
			get
			{
				return this._remark;
			}
			set
			{
				this._remark = value;
			}
		}

		public DateTime CreateTime
		{
			get
			{
				return this._createtime;
			}
			set
			{
				this._createtime = value;
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

		public int UserID
		{
			get
			{
				return this._userid;
			}
			set
			{
				this._userid = value;
			}
		}

		public int ChangeShopID
		{
			get
			{
				return this._changeshopid;
			}
			set
			{
				this._changeshopid = value;
			}
		}
	}
}
