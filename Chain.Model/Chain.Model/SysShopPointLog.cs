using System;

namespace Chain.Model
{
	[Serializable]
	public class SysShopPointLog
	{
		private int _id;

		private string _shoppointaccount;

		private int _shoppointtype;

		private int _count;

		private string _remark;

		private DateTime _createtime;

		private int _userid;

		private int _shopid;

		private int _outshopid;

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

		public string ShopPointAccount
		{
			get
			{
				return this._shoppointaccount;
			}
			set
			{
				this._shoppointaccount = value;
			}
		}

		public int ShopPointType
		{
			get
			{
				return this._shoppointtype;
			}
			set
			{
				this._shoppointtype = value;
			}
		}

		public int Count
		{
			get
			{
				return this._count;
			}
			set
			{
				this._count = value;
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

		public int OutShopID
		{
			get
			{
				return this._outshopid;
			}
			set
			{
				this._outshopid = value;
			}
		}
	}
}
