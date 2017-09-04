using System;

namespace Chain.Model
{
	[Serializable]
	public class SysParameter
	{
		private decimal giveMemMoneyRate;

		private int _parameterid;

		private bool _pwd;

		private bool _moneyandpoint;

		private bool _autolevel;

		private bool _degradelevel;

		private bool _pasttime;

		private int _recommendpoint;

		private int _pointperiod;

		private string _expenseprefix;

		private string _goodsexpenseprefix;

		private string _timeexpenseprefix;

		private string _memcountprefix;

		private string _memrechargeprefix;

		private string _goodsinprefix;

		private string _goodsallotprefix;

		private string _memdrawmoneyprefix;

		private string _mempointchangeprefix;

		private string _giftexchangeprefix;

		private bool _autoprint;

		private bool _accordprint;

		private string _printtitle;

		private string _printfootnote;

		private bool _sms;

		private bool _moneysms;

		private bool _issmsshopname;

		private string _smsshopname;

		private string _smsseries;

		private string _smsserialpwd;

		private decimal _drawmoneypercent;

		private bool _tel;

		private bool _telnomember;

		private bool _isstaff;

		private bool _stafftype;

		private bool _pointlevel;

		private bool _mms;

		private string _mmsseries;

		private string _mmsserialpwd;

		private bool _ispaycard;

		private bool _ispaycash;

		private bool _ispaybink;

		private bool _ispaycoupon;

		private bool _regnullpwd;

		private string _emailadress;

		private string _emailpwd;

		private string _emailsmtp;

		private int _stockcount;

		private string _unitlist;

		private bool _weixinsmsvcode;

		private bool _isautosendsmsbymemregister;

		private bool _isautosendmmsbymemregister;

		private bool _isautosendsmsbymemrecharge;

		private bool _isautosendsmsbymemwithdraw;

		private bool _isautosendsmsbymemgiftexchange;

		private bool _isautosendsmsbymempointchange;

		private bool _isautosendsmsbycommodityconsumption;

		private bool _isautosendsmsbyfastconsumption;

		private bool _isautosendsmsbymemredtimes;

		private bool _isautosendsmsbytimingconsumption;

		private string _selleraccount;

		private string _partnerid;

		private string _partnerkey;

		private bool _iseditpwdneedoldpwd;

		private bool _weixintype;

		private bool _weixinverified;

		private string _weixintoken;

		private string _weixinencodingaeskey;

		private string _weixinshopname;

		private string _weixinsalutatory;

		private string _weixinappid;

		private string _weixinappsecret;

		private int _signinpoint;

		private bool _ismemregisterstaff;

		private bool _ismustslotcard;

		private string _storagetimingprefix;

		private bool _isautosendsmsbystoragetiming;

		private int _enterpriseemailport;

		private string _enterpriseemaildisplayname;

		private bool _enterpriseemailenablessl;

		private bool _enterpriseemailusedefaultcredentials;

		private bool _isemail;

		private bool _isemailnotice;

		private string _memcountexpenseprefix;

		private bool _isautosendsmsbymempast;

		private int _autosendsmsbymempastforday;

		private bool _isautosendsmsbymembirthday;

		private int _autosendsmsbymembirthdayforday;

		private bool _isstartweixin;

		private bool _isstarttimingproject;

		private bool _isstartmemcount;

		private bool _marketingsms;

		private string _marketingsmsseries;

		private string _marketingsmsserialpwd;

		private bool _senseiccard;

		private bool _contacticcard;

		private string _emailusername;

		private string _pointnumstr;

		private int _printpreview;

		private int _printpapertype;

		private bool _issendcard = false;

		private bool _shopsmsmanage = false;

		private bool _shoppointmanage = false;

		private bool _shopsettlement = false;

		private bool _autobackupdb;

		private int _autobackupday;

		private string _systemdomain;

		private decimal pointUsePercent;

		private bool isPayWeiXin;

		private decimal pointDiscountPercent;

		private bool isPayPoint;

		private decimal _xiane;

		private string _mchid;

		private string _api;

		private int _pay;

		private decimal pointDrawPercent;

		private decimal allianceRebatePercent;

		private decimal cardShopRebatePercent;

        

//        txtAlipayPrivateKey
//txtAlipayPublicKey
        private string _AlipayPrivateKey;
        private string _AlipayPublicKey;
        string _alipayappid;

        public string AlipayAppid
        {
            get
            {
                return _alipayappid;
            }
            set
            {
                _alipayappid = value;
            }
        }

        public string AlipayPrivateKey
        {
            get
            {
                return _AlipayPrivateKey;
            }
            set
            {
                _AlipayPrivateKey = value;
            }
        }


        public string AlipayPublicKey
        {
            get
            {
                return _AlipayPublicKey;
            }
            set
            {
                _AlipayPublicKey = value;
            }
        }

		public decimal GiveMemMoneyRate
		{
			get
			{
				return this.giveMemMoneyRate;
			}
			set
			{
				this.giveMemMoneyRate = value;
			}
		}

		public bool IsPayWeiXin
		{
			get
			{
				return this.isPayWeiXin;
			}
			set
			{
				this.isPayWeiXin = value;
			}
		}

		public bool IsPayPoint
		{
			get
			{
				return this.isPayPoint;
			}
			set
			{
				this.isPayPoint = value;
			}
		}

		public decimal PointDiscountPercent
		{
			get
			{
				return this.pointDiscountPercent;
			}
			set
			{
				this.pointDiscountPercent = value;
			}
		}

		public string VIPDescribe
		{
			get;
			set;
		}

		public decimal PointUsePercent
		{
			get
			{
				return this.pointUsePercent;
			}
			set
			{
				this.pointUsePercent = value;
			}
		}

		public decimal Xiane
		{
			get
			{
				return this._xiane;
			}
			set
			{
				this._xiane = value;
			}
		}

		public string MchId
		{
			get
			{
				return this._mchid;
			}
			set
			{
				this._mchid = value;
			}
		}

		public string Api
		{
			get
			{
				return this._api;
			}
			set
			{
				this._api = value;
			}
		}

		public int Pay
		{
			get
			{
				return this._pay;
			}
			set
			{
				this._pay = value;
			}
		}

		public decimal PointDrawPercent
		{
			get
			{
				return this.pointDrawPercent;
			}
			set
			{
				this.pointDrawPercent = value;
			}
		}

		public decimal AllianceRebatePercent
		{
			get
			{
				return this.allianceRebatePercent;
			}
			set
			{
				this.allianceRebatePercent = value;
			}
		}

		public decimal CardShopRebatePercent
		{
			get
			{
				return this.cardShopRebatePercent;
			}
			set
			{
				this.cardShopRebatePercent = value;
			}
		}

		public int ParameterID
		{
			get
			{
				return this._parameterid;
			}
			set
			{
				this._parameterid = value;
			}
		}

		public bool Pwd
		{
			get
			{
				return this._pwd;
			}
			set
			{
				this._pwd = value;
			}
		}

		public bool MoneyAndPoint
		{
			get
			{
				return this._moneyandpoint;
			}
			set
			{
				this._moneyandpoint = value;
			}
		}

		public bool AutoLevel
		{
			get
			{
				return this._autolevel;
			}
			set
			{
				this._autolevel = value;
			}
		}

		public bool DegradeLevel
		{
			get
			{
				return this._degradelevel;
			}
			set
			{
				this._degradelevel = value;
			}
		}

		public bool PastTime
		{
			get
			{
				return this._pasttime;
			}
			set
			{
				this._pasttime = value;
			}
		}

		public int RecommendPoint
		{
			get
			{
				return this._recommendpoint;
			}
			set
			{
				this._recommendpoint = value;
			}
		}

		public int PointPeriod
		{
			get
			{
				return this._pointperiod;
			}
			set
			{
				this._pointperiod = value;
			}
		}

		public string ExpensePrefix
		{
			get
			{
				return this._expenseprefix;
			}
			set
			{
				this._expenseprefix = value;
			}
		}

		public string GoodsExpensePrefix
		{
			get
			{
				return this._goodsexpenseprefix;
			}
			set
			{
				this._goodsexpenseprefix = value;
			}
		}

		public string TimeExpensePrefix
		{
			get
			{
				return this._timeexpenseprefix;
			}
			set
			{
				this._timeexpenseprefix = value;
			}
		}

		public string MemCountPrefix
		{
			get
			{
				return this._memcountprefix;
			}
			set
			{
				this._memcountprefix = value;
			}
		}

		public string MemRechargePrefix
		{
			get
			{
				return this._memrechargeprefix;
			}
			set
			{
				this._memrechargeprefix = value;
			}
		}

		public string GoodsInPrefix
		{
			get
			{
				return this._goodsinprefix;
			}
			set
			{
				this._goodsinprefix = value;
			}
		}

		public string GoodsAllotPrefix
		{
			get
			{
				return this._goodsallotprefix;
			}
			set
			{
				this._goodsallotprefix = value;
			}
		}

		public string MemDrawMoneyPrefix
		{
			get
			{
				return this._memdrawmoneyprefix;
			}
			set
			{
				this._memdrawmoneyprefix = value;
			}
		}

		public string MemPointChangePrefix
		{
			get
			{
				return this._mempointchangeprefix;
			}
			set
			{
				this._mempointchangeprefix = value;
			}
		}

		public string GiftExchangePrefix
		{
			get
			{
				return this._giftexchangeprefix;
			}
			set
			{
				this._giftexchangeprefix = value;
			}
		}

		public bool AutoPrint
		{
			get
			{
				return this._autoprint;
			}
			set
			{
				this._autoprint = value;
			}
		}

		public bool AccordPrint
		{
			get
			{
				return this._accordprint;
			}
			set
			{
				this._accordprint = value;
			}
		}

		public string PrintTitle
		{
			get
			{
				return this._printtitle;
			}
			set
			{
				this._printtitle = value;
			}
		}

		public string PrintFootNote
		{
			get
			{
				return this._printfootnote;
			}
			set
			{
				this._printfootnote = value;
			}
		}

		public bool Sms
		{
			get
			{
				return this._sms;
			}
			set
			{
				this._sms = value;
			}
		}

		public bool MoneySms
		{
			get
			{
				return this._moneysms;
			}
			set
			{
				this._moneysms = value;
			}
		}

		public bool IsSmsShopName
		{
			get
			{
				return this._issmsshopname;
			}
			set
			{
				this._issmsshopname = value;
			}
		}

		public string SmsShopName
		{
			get
			{
				return this._smsshopname;
			}
			set
			{
				this._smsshopname = value;
			}
		}

		public string SmsSeries
		{
			get
			{
				return this._smsseries;
			}
			set
			{
				this._smsseries = value;
			}
		}

		public string SmsSerialPwd
		{
			get
			{
				return this._smsserialpwd;
			}
			set
			{
				this._smsserialpwd = value;
			}
		}

		public decimal DrawMoneyPercent
		{
			get
			{
				return this._drawmoneypercent;
			}
			set
			{
				this._drawmoneypercent = value;
			}
		}

		public bool Tel
		{
			get
			{
				return this._tel;
			}
			set
			{
				this._tel = value;
			}
		}

		public bool TelNoMember
		{
			get
			{
				return this._telnomember;
			}
			set
			{
				this._telnomember = value;
			}
		}

		public bool IsStaff
		{
			get
			{
				return this._isstaff;
			}
			set
			{
				this._isstaff = value;
			}
		}

		public bool StaffType
		{
			get
			{
				return this._stafftype;
			}
			set
			{
				this._stafftype = value;
			}
		}

		public bool PointLevel
		{
			get
			{
				return this._pointlevel;
			}
			set
			{
				this._pointlevel = value;
			}
		}

		public bool MMS
		{
			get
			{
				return this._mms;
			}
			set
			{
				this._mms = value;
			}
		}

		public string MMSSeries
		{
			get
			{
				return this._mmsseries;
			}
			set
			{
				this._mmsseries = value;
			}
		}

		public string MMSSerialPwd
		{
			get
			{
				return this._mmsserialpwd;
			}
			set
			{
				this._mmsserialpwd = value;
			}
		}

		public bool IsPayCard
		{
			get
			{
				return this._ispaycard;
			}
			set
			{
				this._ispaycard = value;
			}
		}

		public bool IsPayCash
		{
			get
			{
				return this._ispaycash;
			}
			set
			{
				this._ispaycash = value;
			}
		}

		public bool IsPayBink
		{
			get
			{
				return this._ispaybink;
			}
			set
			{
				this._ispaybink = value;
			}
		}

		public bool IsPayCoupon
		{
			get
			{
				return this._ispaycoupon;
			}
			set
			{
				this._ispaycoupon = value;
			}
		}

		public bool RegNullPwd
		{
			get
			{
				return this._regnullpwd;
			}
			set
			{
				this._regnullpwd = value;
			}
		}

		public string EmailAdress
		{
			get
			{
				return this._emailadress;
			}
			set
			{
				this._emailadress = value;
			}
		}

		public string EmailPwd
		{
			get
			{
				return this._emailpwd;
			}
			set
			{
				this._emailpwd = value;
			}
		}

		public string EmailSMTP
		{
			get
			{
				return this._emailsmtp;
			}
			set
			{
				this._emailsmtp = value;
			}
		}

		public int StockCount
		{
			get
			{
				return this._stockcount;
			}
			set
			{
				this._stockcount = value;
			}
		}

		public string UnitList
		{
			get
			{
				return this._unitlist;
			}
			set
			{
				this._unitlist = value;
			}
		}

		public bool WeiXinSMSVcode
		{
			get
			{
				return this._weixinsmsvcode;
			}
			set
			{
				this._weixinsmsvcode = value;
			}
		}

		public bool IsAutoSendSMSByMemRegister
		{
			get
			{
				return this._isautosendsmsbymemregister;
			}
			set
			{
				this._isautosendsmsbymemregister = value;
			}
		}

		public bool IsAutoSendMMSByMemRegister
		{
			get
			{
				return this._isautosendmmsbymemregister;
			}
			set
			{
				this._isautosendmmsbymemregister = value;
			}
		}

		public bool IsAutoSendSMSByMemRecharge
		{
			get
			{
				return this._isautosendsmsbymemrecharge;
			}
			set
			{
				this._isautosendsmsbymemrecharge = value;
			}
		}

		public bool IsAutoSendSMSByMemWithdraw
		{
			get
			{
				return this._isautosendsmsbymemwithdraw;
			}
			set
			{
				this._isautosendsmsbymemwithdraw = value;
			}
		}

		public bool IsAutoSendSMSByMemGiftExchange
		{
			get
			{
				return this._isautosendsmsbymemgiftexchange;
			}
			set
			{
				this._isautosendsmsbymemgiftexchange = value;
			}
		}

		public bool IsAutoSendSMSByMemPointChange
		{
			get
			{
				return this._isautosendsmsbymempointchange;
			}
			set
			{
				this._isautosendsmsbymempointchange = value;
			}
		}

		public bool IsAutoSendSMSByCommodityConsumption
		{
			get
			{
				return this._isautosendsmsbycommodityconsumption;
			}
			set
			{
				this._isautosendsmsbycommodityconsumption = value;
			}
		}

		public bool IsAutoSendSMSByFastConsumption
		{
			get
			{
				return this._isautosendsmsbyfastconsumption;
			}
			set
			{
				this._isautosendsmsbyfastconsumption = value;
			}
		}

		public bool IsAutoSendSMSByMemRedTimes
		{
			get
			{
				return this._isautosendsmsbymemredtimes;
			}
			set
			{
				this._isautosendsmsbymemredtimes = value;
			}
		}

		public bool IsAutoSendSMSByTimingConsumption
		{
			get
			{
				return this._isautosendsmsbytimingconsumption;
			}
			set
			{
				this._isautosendsmsbytimingconsumption = value;
			}
		}

		public string SellerAccount
		{
			get
			{
				return this._selleraccount;
			}
			set
			{
				this._selleraccount = value;
			}
		}

		public string PartnerID
		{
			get
			{
				return this._partnerid;
			}
			set
			{
				this._partnerid = value;
			}
		}

		public string PartnerKey
		{
			get
			{
				return this._partnerkey;
			}
			set
			{
				this._partnerkey = value;
			}
		}

		public bool IsEditPwdNeedOldPwd
		{
			get
			{
				return this._iseditpwdneedoldpwd;
			}
			set
			{
				this._iseditpwdneedoldpwd = value;
			}
		}

		public bool WeiXinType
		{
			get
			{
				return this._weixintype;
			}
			set
			{
				this._weixintype = value;
			}
		}

		public bool WeiXinVerified
		{
			get
			{
				return this._weixinverified;
			}
			set
			{
				this._weixinverified = value;
			}
		}

		public string WeiXinToken
		{
			get
			{
				return this._weixintoken;
			}
			set
			{
				this._weixintoken = value;
			}
		}

		public string WeiXinEncodingAESKey
		{
			get
			{
				return this._weixinencodingaeskey;
			}
			set
			{
				this._weixinencodingaeskey = value;
			}
		}

		public string WeiXinShopName
		{
			get
			{
				return this._weixinshopname;
			}
			set
			{
				this._weixinshopname = value;
			}
		}

		public string WeiXinSalutatory
		{
			get
			{
				return this._weixinsalutatory;
			}
			set
			{
				this._weixinsalutatory = value;
			}
		}

		public string WeiXinAppID
		{
			get
			{
				return this._weixinappid;
			}
			set
			{
				this._weixinappid = value;
			}
		}

		public string WeiXinAppSecret
		{
			get
			{
				return this._weixinappsecret;
			}
			set
			{
				this._weixinappsecret = value;
			}
		}

		public int SignInPoint
		{
			get
			{
				return this._signinpoint;
			}
			set
			{
				this._signinpoint = value;
			}
		}

		public bool IsMemRegisterStaff
		{
			get
			{
				return this._ismemregisterstaff;
			}
			set
			{
				this._ismemregisterstaff = value;
			}
		}

		public bool IsMustSlotCard
		{
			get
			{
				return this._ismustslotcard;
			}
			set
			{
				this._ismustslotcard = value;
			}
		}

		public string StorageTimingPrefix
		{
			get
			{
				return this._storagetimingprefix;
			}
			set
			{
				this._storagetimingprefix = value;
			}
		}

		public bool IsAutoSendSMSByStorageTiming
		{
			get
			{
				return this._isautosendsmsbystoragetiming;
			}
			set
			{
				this._isautosendsmsbystoragetiming = value;
			}
		}

		public int EnterpriseEmailPort
		{
			get
			{
				return this._enterpriseemailport;
			}
			set
			{
				this._enterpriseemailport = value;
			}
		}

		public string EnterpriseEmailDisplayName
		{
			get
			{
				return this._enterpriseemaildisplayname;
			}
			set
			{
				this._enterpriseemaildisplayname = value;
			}
		}

		public bool EnterpriseEmailEnableSSL
		{
			get
			{
				return this._enterpriseemailenablessl;
			}
			set
			{
				this._enterpriseemailenablessl = value;
			}
		}

		public bool EnterpriseEmailUseDefaultCredentials
		{
			get
			{
				return this._enterpriseemailusedefaultcredentials;
			}
			set
			{
				this._enterpriseemailusedefaultcredentials = value;
			}
		}

		public bool IsEmail
		{
			get
			{
				return this._isemail;
			}
			set
			{
				this._isemail = value;
			}
		}

		public bool IsEmailNotice
		{
			get
			{
				return this._isemailnotice;
			}
			set
			{
				this._isemailnotice = value;
			}
		}

		public string MemCountExpensePrefix
		{
			get
			{
				return this._memcountexpenseprefix;
			}
			set
			{
				this._memcountexpenseprefix = value;
			}
		}

		public bool IsAutoSendSMSByMemPast
		{
			get
			{
				return this._isautosendsmsbymempast;
			}
			set
			{
				this._isautosendsmsbymempast = value;
			}
		}

		public int AutoSendSMSByMemPastForDay
		{
			get
			{
				return this._autosendsmsbymempastforday;
			}
			set
			{
				this._autosendsmsbymempastforday = value;
			}
		}

		public bool IsAutoSendSMSByMemBirthday
		{
			get
			{
				return this._isautosendsmsbymembirthday;
			}
			set
			{
				this._isautosendsmsbymembirthday = value;
			}
		}

		public int AutoSendSMSByMemBirthdayForDay
		{
			get
			{
				return this._autosendsmsbymembirthdayforday;
			}
			set
			{
				this._autosendsmsbymembirthdayforday = value;
			}
		}

		public bool IsStartWeiXin
		{
			get
			{
				return this._isstartweixin;
			}
			set
			{
				this._isstartweixin = value;
			}
		}

		public bool IsStartTimingProject
		{
			get
			{
				return this._isstarttimingproject;
			}
			set
			{
				this._isstarttimingproject = value;
			}
		}

		public bool IsStartMemCount
		{
			get
			{
				return this._isstartmemcount;
			}
			set
			{
				this._isstartmemcount = value;
			}
		}

		public bool MarketingSMS
		{
			get
			{
				return this._marketingsms;
			}
			set
			{
				this._marketingsms = value;
			}
		}

		public string MarketingSmsSeries
		{
			get
			{
				return this._marketingsmsseries;
			}
			set
			{
				this._marketingsmsseries = value;
			}
		}

		public string MarketingSmsSerialPwd
		{
			get
			{
				return this._marketingsmsserialpwd;
			}
			set
			{
				this._marketingsmsserialpwd = value;
			}
		}

		public bool Senseiccard
		{
			get
			{
				return this._senseiccard;
			}
			set
			{
				this._senseiccard = value;
			}
		}

		public bool Contacticcard
		{
			get
			{
				return this._contacticcard;
			}
			set
			{
				this._contacticcard = value;
			}
		}

		public string EmailUserName
		{
			get
			{
				return this._emailusername;
			}
			set
			{
				this._emailusername = value;
			}
		}

		public string PointNumStr
		{
			get
			{
				return this._pointnumstr;
			}
			set
			{
				this._pointnumstr = value;
			}
		}

		public int PrintPreview
		{
			get
			{
				return this._printpreview;
			}
			set
			{
				this._printpreview = value;
			}
		}

		public int PrintPaperType
		{
			get
			{
				return this._printpapertype;
			}
			set
			{
				this._printpapertype = value;
			}
		}

		public bool IsSendCard
		{
			get
			{
				return this._issendcard;
			}
			set
			{
				this._issendcard = value;
			}
		}

		public bool ShopSmsManage
		{
			get
			{
				return this._shopsmsmanage;
			}
			set
			{
				this._shopsmsmanage = value;
			}
		}

		public bool ShopPointManage
		{
			get
			{
				return this._shoppointmanage;
			}
			set
			{
				this._shoppointmanage = value;
			}
		}

		public bool ShopSettlement
		{
			get
			{
				return this._shopsettlement;
			}
			set
			{
				this._shopsettlement = value;
			}
		}

		public bool AutoBackupDB
		{
			get
			{
				return this._autobackupdb;
			}
			set
			{
				this._autobackupdb = value;
			}
		}

		public int AutoBackupDay
		{
			get
			{
				return this._autobackupday;
			}
			set
			{
				this._autobackupday = value;
			}
		}

		public string SystemDomain
		{
			get
			{
				return this._systemdomain;
			}
			set
			{
				this._systemdomain = value;
			}
		}
	}
}
