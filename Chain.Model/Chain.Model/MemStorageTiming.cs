using System;

namespace Chain.Model
{
	[Serializable]
	public class MemStorageTiming
	{
		private int _storagetimingid;

		private int _storagetimingmemid;

		private string _storagetimingaccount;

		private decimal _storagetimingtotalmoney;

		private decimal _storagetimingdiscountmoney;

		private bool _storagetimingiscard;

		private decimal _storagetimingpaycard;

		private bool _storagetimingiscash;

		private decimal _storagetimingpaycash;

		private bool _storagetimingisbink;

		private decimal _storagetimingpaybink;

		private decimal _storagetimingpaycoupon;

		private int _storagetimingpaytype;

		private int _storagetimingpoint;

		private string _storagetimingremark;

		private int _storagetimingshopid;

		private int _storagetiminguserid;

		private DateTime _storagetimingcreatetime;

		private int _storagetotaltime;

		private int _storageresiduetime;

		private int _storagetimingprojectid;

		public int StorageTimingID
		{
			get
			{
				return this._storagetimingid;
			}
			set
			{
				this._storagetimingid = value;
			}
		}

		public int StorageTimingMemID
		{
			get
			{
				return this._storagetimingmemid;
			}
			set
			{
				this._storagetimingmemid = value;
			}
		}

		public string StorageTimingAccount
		{
			get
			{
				return this._storagetimingaccount;
			}
			set
			{
				this._storagetimingaccount = value;
			}
		}

		public decimal StorageTimingTotalMoney
		{
			get
			{
				return this._storagetimingtotalmoney;
			}
			set
			{
				this._storagetimingtotalmoney = value;
			}
		}

		public decimal StorageTimingDiscountMoney
		{
			get
			{
				return this._storagetimingdiscountmoney;
			}
			set
			{
				this._storagetimingdiscountmoney = value;
			}
		}

		public bool StorageTimingIsCard
		{
			get
			{
				return this._storagetimingiscard;
			}
			set
			{
				this._storagetimingiscard = value;
			}
		}

		public decimal StorageTimingPayCard
		{
			get
			{
				return this._storagetimingpaycard;
			}
			set
			{
				this._storagetimingpaycard = value;
			}
		}

		public bool StorageTimingIsCash
		{
			get
			{
				return this._storagetimingiscash;
			}
			set
			{
				this._storagetimingiscash = value;
			}
		}

		public decimal StorageTimingPayCash
		{
			get
			{
				return this._storagetimingpaycash;
			}
			set
			{
				this._storagetimingpaycash = value;
			}
		}

		public bool StorageTimingIsBink
		{
			get
			{
				return this._storagetimingisbink;
			}
			set
			{
				this._storagetimingisbink = value;
			}
		}

		public decimal StorageTimingPayBink
		{
			get
			{
				return this._storagetimingpaybink;
			}
			set
			{
				this._storagetimingpaybink = value;
			}
		}

		public decimal StorageTimingPayCoupon
		{
			get
			{
				return this._storagetimingpaycoupon;
			}
			set
			{
				this._storagetimingpaycoupon = value;
			}
		}

		public int StorageTimingPayType
		{
			get
			{
				return this._storagetimingpaytype;
			}
			set
			{
				this._storagetimingpaytype = value;
			}
		}

		public int StorageTimingPoint
		{
			get
			{
				return this._storagetimingpoint;
			}
			set
			{
				this._storagetimingpoint = value;
			}
		}

		public string StorageTimingRemark
		{
			get
			{
				return this._storagetimingremark;
			}
			set
			{
				this._storagetimingremark = value;
			}
		}

		public int StorageTimingShopID
		{
			get
			{
				return this._storagetimingshopid;
			}
			set
			{
				this._storagetimingshopid = value;
			}
		}

		public int StorageTimingUserID
		{
			get
			{
				return this._storagetiminguserid;
			}
			set
			{
				this._storagetiminguserid = value;
			}
		}

		public DateTime StorageTimingCreateTime
		{
			get
			{
				return this._storagetimingcreatetime;
			}
			set
			{
				this._storagetimingcreatetime = value;
			}
		}

		public int StorageTotalTime
		{
			get
			{
				return this._storagetotaltime;
			}
			set
			{
				this._storagetotaltime = value;
			}
		}

		public int StorageResidueTime
		{
			get
			{
				return this._storageresiduetime;
			}
			set
			{
				this._storageresiduetime = value;
			}
		}

		public int StorageTimingProjectID
		{
			get
			{
				return this._storagetimingprojectid;
			}
			set
			{
				this._storagetimingprojectid = value;
			}
		}
	}
}
