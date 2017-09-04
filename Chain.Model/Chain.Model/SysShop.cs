using System;

namespace Chain.Model
{
	[Serializable]
	public class SysShop
	{
		private int _shopid;

		private string _shopname;

		private string _shoptelephone;

		private string _shopcontactman;

		private int _shopareaid;

		private string _shopaddress;

		private string _shopremark;

		private DateTime _shopcreatetime;

		private bool _shopstate;

		private string _shopprinttitle;

		private string _shopprintfoot;

		private string _shopsmsname;

		private int _settlementinterval;

		private decimal _shopproportion = 0m;

		private int fathershopid;

		private bool isallianceprogram;

		private int pointcount;

		private int smscount;

		private int pointtype;

		private int smstype;

		private int shopType;

		private decimal totalRate;

		private string shopImageUrl;

		private int shopProvince;

		private int shopCity;

		private int shopCounty;

		private bool isMain;

		private bool isRecharge;

		private decimal rechargeMaxMoney;

		public int ShopType
		{
			get
			{
				return this.shopType;
			}
			set
			{
				this.shopType = value;
			}
		}

		public decimal TotalRate
		{
			get
			{
				return this.totalRate;
			}
			set
			{
				this.totalRate = value;
			}
		}

		public int PointCount
		{
			get
			{
				return this.pointcount;
			}
			set
			{
				this.pointcount = value;
			}
		}

		public int SmsCount
		{
			get
			{
				return this.smscount;
			}
			set
			{
				this.smscount = value;
			}
		}

		public int SmsType
		{
			get
			{
				return this.smstype;
			}
			set
			{
				this.smstype = value;
			}
		}

		public int PointType
		{
			get
			{
				return this.pointtype;
			}
			set
			{
				this.pointtype = value;
			}
		}

		public bool IsAllianceProgram
		{
			get
			{
				return this.isallianceprogram;
			}
			set
			{
				this.isallianceprogram = value;
			}
		}

		public int FatherShopID
		{
			get
			{
				return this.fathershopid;
			}
			set
			{
				this.fathershopid = value;
			}
		}

		public string ShopImageUrl
		{
			get
			{
				return this.shopImageUrl;
			}
			set
			{
				this.shopImageUrl = value;
			}
		}

		public int ShopProvince
		{
			get
			{
				return this.shopProvince;
			}
			set
			{
				this.shopProvince = value;
			}
		}

		public int ShopCity
		{
			get
			{
				return this.shopCity;
			}
			set
			{
				this.shopCity = value;
			}
		}

		public int ShopCounty
		{
			get
			{
				return this.shopCounty;
			}
			set
			{
				this.shopCounty = value;
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

		public bool IsMain
		{
			get
			{
				return this.isMain;
			}
			set
			{
				this.isMain = value;
			}
		}

		public string ShopName
		{
			get
			{
				return this._shopname;
			}
			set
			{
				this._shopname = value;
			}
		}

		public string ShopTelephone
		{
			get
			{
				return this._shoptelephone;
			}
			set
			{
				this._shoptelephone = value;
			}
		}

		public string ShopContactMan
		{
			get
			{
				return this._shopcontactman;
			}
			set
			{
				this._shopcontactman = value;
			}
		}

		public int ShopAreaID
		{
			get
			{
				return this._shopareaid;
			}
			set
			{
				this._shopareaid = value;
			}
		}

		public string ShopAddress
		{
			get
			{
				return this._shopaddress;
			}
			set
			{
				this._shopaddress = value;
			}
		}

		public string ShopRemark
		{
			get
			{
				return this._shopremark;
			}
			set
			{
				this._shopremark = value;
			}
		}

		public DateTime ShopCreateTime
		{
			get
			{
				return this._shopcreatetime;
			}
			set
			{
				this._shopcreatetime = value;
			}
		}

		public bool ShopState
		{
			get
			{
				return this._shopstate;
			}
			set
			{
				this._shopstate = value;
			}
		}

		public string ShopPrintTitle
		{
			get
			{
				return this._shopprinttitle;
			}
			set
			{
				this._shopprinttitle = value;
			}
		}

		public string ShopPrintFoot
		{
			get
			{
				return this._shopprintfoot;
			}
			set
			{
				this._shopprintfoot = value;
			}
		}

		public string ShopSmsName
		{
			get
			{
				return this._shopsmsname;
			}
			set
			{
				this._shopsmsname = value;
			}
		}

		public int SettlementInterval
		{
			get
			{
				return this._settlementinterval;
			}
			set
			{
				this._settlementinterval = value;
			}
		}

		public decimal ShopProportion
		{
			get
			{
				return this._shopproportion;
			}
			set
			{
				this._shopproportion = value;
			}
		}

		public decimal RechargeProportion
		{
			get;
			set;
		}

		public bool IsRecharge
		{
			get
			{
				return this.isRecharge;
			}
			set
			{
				this.isRecharge = value;
			}
		}

		public decimal RechargeMaxMoney
		{
			get
			{
				return this.rechargeMaxMoney;
			}
			set
			{
				this.rechargeMaxMoney = value;
			}
		}
	}
}
