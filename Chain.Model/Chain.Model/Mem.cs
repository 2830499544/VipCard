using System;

namespace Chain.Model
{
	[Serializable]
	public class Mem
	{
		private int _memid;

		private string _memcard;

		private string _mempassword;

		private string _memname;

		private bool _memsex;

		private string _memidentitycard;

		private string _memmobile;

		private string _memphoto;

		private bool _membirthdaytype;

		private DateTime _membirthday;

		private bool _memispast;

		private DateTime _mempasttime;

		private int _mempoint;

		private bool _mempointautomatic;

		private decimal _memmoney;

		private decimal _memconsumemoney;

		private DateTime _memconsumelasttime;

		private int _memconsumecount;

		private string _mememail;

		private string _memaddress;

		private int _memstate;

		private int _memrecommendid;

		private int _memlevelid;

		private int _memshopid;

		private DateTime _memcreatetime;

		private string _memremark;

		private int _memuserid;

		private string _memtelephone;

		private string _memqrcode;

		private string _memprovince;

		private string _memcity;

		private string _memcounty;

		private string _memvillage;

		private string _memquestion;

		private string _memanswer;

		private string _memweixincard;

		private string _memcardnumber;

		private int _memattention;

		private string _memweixincards;

		public int MemAttention
		{
			get
			{
				return this._memattention;
			}
			set
			{
				this._memattention = value;
			}
		}

		public string MemWeiXinCards
		{
			get
			{
				return this._memweixincards;
			}
			set
			{
				this._memweixincards = value;
			}
		}

		public int MemID
		{
			get
			{
				return this._memid;
			}
			set
			{
				this._memid = value;
			}
		}

		public string MemCard
		{
			get
			{
				return this._memcard;
			}
			set
			{
				this._memcard = value;
			}
		}

		public string MemPassword
		{
			get
			{
				return this._mempassword;
			}
			set
			{
				this._mempassword = value;
			}
		}

		public string MemName
		{
			get
			{
				return this._memname;
			}
			set
			{
				this._memname = value;
			}
		}

		public bool MemSex
		{
			get
			{
				return this._memsex;
			}
			set
			{
				this._memsex = value;
			}
		}

		public string MemIdentityCard
		{
			get
			{
				return this._memidentitycard;
			}
			set
			{
				this._memidentitycard = value;
			}
		}

		public string MemMobile
		{
			get
			{
				return this._memmobile;
			}
			set
			{
				this._memmobile = value;
			}
		}

		public string MemPhoto
		{
			get
			{
				return this._memphoto;
			}
			set
			{
				this._memphoto = value;
			}
		}

		public bool MemBirthdayType
		{
			get
			{
				return this._membirthdaytype;
			}
			set
			{
				this._membirthdaytype = value;
			}
		}

		public DateTime MemBirthday
		{
			get
			{
				return this._membirthday;
			}
			set
			{
				this._membirthday = value;
			}
		}

		public bool MemIsPast
		{
			get
			{
				return this._memispast;
			}
			set
			{
				this._memispast = value;
			}
		}

		public DateTime MemPastTime
		{
			get
			{
				return this._mempasttime;
			}
			set
			{
				this._mempasttime = value;
			}
		}

		public int MemPoint
		{
			get
			{
				return this._mempoint;
			}
			set
			{
				this._mempoint = value;
			}
		}

		public bool MemPointAutomatic
		{
			get
			{
				return this._mempointautomatic;
			}
			set
			{
				this._mempointautomatic = value;
			}
		}

		public decimal MemMoney
		{
			get
			{
				return this._memmoney;
			}
			set
			{
				this._memmoney = value;
			}
		}

		public decimal MemConsumeMoney
		{
			get
			{
				return this._memconsumemoney;
			}
			set
			{
				this._memconsumemoney = value;
			}
		}

		public DateTime MemConsumeLastTime
		{
			get
			{
				return this._memconsumelasttime;
			}
			set
			{
				this._memconsumelasttime = value;
			}
		}

		public int MemConsumeCount
		{
			get
			{
				return this._memconsumecount;
			}
			set
			{
				this._memconsumecount = value;
			}
		}

		public string MemEmail
		{
			get
			{
				return this._mememail;
			}
			set
			{
				this._mememail = value;
			}
		}

		public string MemAddress
		{
			get
			{
				return this._memaddress;
			}
			set
			{
				this._memaddress = value;
			}
		}

		public int MemState
		{
			get
			{
				return this._memstate;
			}
			set
			{
				this._memstate = value;
			}
		}

		public int MemRecommendID
		{
			get
			{
				return this._memrecommendid;
			}
			set
			{
				this._memrecommendid = value;
			}
		}

		public int MemLevelID
		{
			get
			{
				return this._memlevelid;
			}
			set
			{
				this._memlevelid = value;
			}
		}

		public int MemShopID
		{
			get
			{
				return this._memshopid;
			}
			set
			{
				this._memshopid = value;
			}
		}

		public DateTime MemCreateTime
		{
			get
			{
				return this._memcreatetime;
			}
			set
			{
				this._memcreatetime = value;
			}
		}

		public string MemRemark
		{
			get
			{
				return this._memremark;
			}
			set
			{
				this._memremark = value;
			}
		}

		public int MemUserID
		{
			get
			{
				return this._memuserid;
			}
			set
			{
				this._memuserid = value;
			}
		}

		public string MemTelePhone
		{
			get
			{
				return this._memtelephone;
			}
			set
			{
				this._memtelephone = value;
			}
		}

		public string MemQRCode
		{
			get
			{
				return this._memqrcode;
			}
			set
			{
				this._memqrcode = value;
			}
		}

		public string MemProvince
		{
			get
			{
				return this._memprovince;
			}
			set
			{
				this._memprovince = value;
			}
		}

		public string MemCity
		{
			get
			{
				return this._memcity;
			}
			set
			{
				this._memcity = value;
			}
		}

		public string MemCounty
		{
			get
			{
				return this._memcounty;
			}
			set
			{
				this._memcounty = value;
			}
		}

		public string MemVillage
		{
			get
			{
				return this._memvillage;
			}
			set
			{
				this._memvillage = value;
			}
		}

		public string MemQuestion
		{
			get
			{
				return this._memquestion;
			}
			set
			{
				this._memquestion = value;
			}
		}

		public string MemAnswer
		{
			get
			{
				return this._memanswer;
			}
			set
			{
				this._memanswer = value;
			}
		}

		public string MemWeiXinCard
		{
			get
			{
				return this._memweixincard;
			}
			set
			{
				this._memweixincard = value;
			}
		}

		public string MemCardNumber
		{
			get
			{
				return this._memcardnumber;
			}
			set
			{
				this._memcardnumber = value;
			}
		}
	}
}
