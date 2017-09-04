using System;

namespace Chain.Model
{
	[Serializable]
	public class Goods
	{
		private int _goodsid;

		private string _goodscode;

		private int _goodsclassid;

		private string _name;

		private string _namecode;

		private string _unit;

		private int _goodsnumber;

		private decimal _salepercet;

		private int _goodssalenumber;

		private decimal _price;

		private int _commissiontype;

		private decimal _commissionnumber;

		private int _point;

		private decimal _minpercent;

		private int _goodstype;

		private decimal _goodsbidprice;

		private string _goodsremark;

		private string _goodspicture;

		private DateTime _goodscreatetime;

		private int _createshopid;

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

		public string GoodsCode
		{
			get
			{
				return this._goodscode;
			}
			set
			{
				this._goodscode = value;
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

		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		public string NameCode
		{
			get
			{
				return this._namecode;
			}
			set
			{
				this._namecode = value;
			}
		}

		public string Unit
		{
			get
			{
				return this._unit;
			}
			set
			{
				this._unit = value;
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

		public decimal SalePercet
		{
			get
			{
				return this._salepercet;
			}
			set
			{
				this._salepercet = value;
			}
		}

		public int GoodsSaleNumber
		{
			get
			{
				return this._goodssalenumber;
			}
			set
			{
				this._goodssalenumber = value;
			}
		}

		public decimal Price
		{
			get
			{
				return this._price;
			}
			set
			{
				this._price = value;
			}
		}

		public int CommissionType
		{
			get
			{
				return this._commissiontype;
			}
			set
			{
				this._commissiontype = value;
			}
		}

		public decimal CommissionNumber
		{
			get
			{
				return this._commissionnumber;
			}
			set
			{
				this._commissionnumber = value;
			}
		}

		public int Point
		{
			get
			{
				return this._point;
			}
			set
			{
				this._point = value;
			}
		}

		public decimal MinPercent
		{
			get
			{
				return this._minpercent;
			}
			set
			{
				this._minpercent = value;
			}
		}

		public int GoodsType
		{
			get
			{
				return this._goodstype;
			}
			set
			{
				this._goodstype = value;
			}
		}

		public decimal GoodsBidPrice
		{
			get
			{
				return this._goodsbidprice;
			}
			set
			{
				this._goodsbidprice = value;
			}
		}

		public string GoodsRemark
		{
			get
			{
				return this._goodsremark;
			}
			set
			{
				this._goodsremark = value;
			}
		}

		public string GoodsPicture
		{
			get
			{
				return this._goodspicture;
			}
			set
			{
				this._goodspicture = value;
			}
		}

		public DateTime GoodsCreateTime
		{
			get
			{
				return this._goodscreatetime;
			}
			set
			{
				this._goodscreatetime = value;
			}
		}

		public int CreateShopID
		{
			get
			{
				return this._createshopid;
			}
			set
			{
				this._createshopid = value;
			}
		}
	}
}
