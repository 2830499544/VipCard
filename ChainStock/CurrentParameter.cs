using Chain.Common;
using Chain.Common.DEncrypt;
using System;

public class CurrentParameter
{
	public bool bolPwd = true;

	public bool bolMoneyAndPoint = true;

	public bool bolAutoLevel = true;

	public bool bolDegradeLevel = true;

	public bool bolPastTime = true;

	public int intRecommendPoint = 0;

	public int intPointPeriod = 0;

	public decimal PointUsePercent = 0m;

	public int intStockCount = 0;

	public string strExpensePrefix = "";

	public string strGoodsExpensePrefix = "";

	public string strTimeExpensePrefix = "";

	public string strMemCountPrefix = "";

	public string strMemRechargePrefix = "";

	public string strGoodsInPrefix = "";

	public string strGoodsAllotPrefix = "";

	public string strMemDrawMoneyPrefix = "";

	public string strMemPointChangePrefix = "";

	public string strGiftExchangePrefix = "";

	public bool bolAutoPrint = true;

	public bool bolAccordPrint = true;

	public string strPrintTitle = "";

	public decimal dclGiveMemMoneyRate = 0m;

	public string strMchKey = "";

	public string strMchid = "";

    public string AlipayAppId = "";

	public string strPrintFootNote = "";

	public bool bolSms = true;

	public bool bolMoneySms = true;

	public bool bolIsSmsShopName = true;

    public string StoreId = "";

	public string strSmsShopName = "";

	public string strNotificationSMS = "";

	public string strNotificationSMSPwd = "";

	public bool bolMarketingSMS = true;

	public string strMarketingSmsSeries = "";

	public string strMarketingSmsSerialPwd = "";

	public decimal DrawMoneyPercent = 1m;

	public decimal PointDrawPercent = 1m;

	public decimal AllianceRebatePercent = 0m;

	public decimal CardShopRebatePercent = 0m;

	public bool bolTel = true;

	public bool bolTelNoMember = true;

	public int intMarketingSMS = int.Parse(ConfigHelper.GetValue("MarketingSMS"));

	public int intNotificationSMS = int.Parse(ConfigHelper.GetValue("NotificationSMS"));

	public bool bolStaff = true;

	public bool bolStaffType = true;

	public int istry = Convert.ToInt32(DESEncrypt.Decrypt(ConfigHelper.GetValue("ISTry")));

	public int dataAuthority = 0;

	public bool bolGiftShare = Convert.ToBoolean(ConfigHelper.GetValue("GiftShare"));

	public string strDoMain = DESEncrypt.Decrypt(ConfigHelper.GetValue("SystemDomain"));

	public string SelfDoMain = DESEncrypt.Decrypt(ConfigHelper.GetValue("SelfDomain"));

	public bool EnableGoods = Convert.ToBoolean(DESEncrypt.Decrypt(ConfigHelper.GetValue("EnableGoods")));

	public string RegisterNumber = ConfigHelper.GetValue("RegisterNumber");

	public int RestrainOnlineNumber = Convert.ToInt32(DESEncrypt.Decrypt(ConfigHelper.GetValue("RestrainOnlineNumber")));

	public DateTime DateValidity = Convert.ToDateTime(DESEncrypt.Decrypt(ConfigHelper.GetValue("DateValidity")));

	public bool chkPointLevel = false;

	public bool bolMMS = true;

	public string strMMSSeries = "";

	public string strUnitList = "";

	public string strMMSSerialPwd = "";

	public bool bolIsPayCard = true;

	public bool bolIsPayCash = true;

	public bool bolIsPayBink = true;

	public bool bolIsPayCoupon = true;

	public bool RegNullPwd = true;

	public string EmailAdress = "";

	public string EmailPwd = "";

	public string EmailSMTP = "";

	public bool bolWeiXinSMSVcode = true;

	public bool bolAutoSendSMSByMemRegister = true;

	public bool bolAutoSendMMSByMemRegister = true;

	public bool bolAutoSendSMSByMemRecharge = true;

	public bool bolAutoSendSMSByMemWithdraw = true;

	public bool bolAutoSendSMSByMemGiftExchange = true;

	public bool bolAutoSendSMSByMemPointChange = true;

	public bool bolAutoSendSMSByCommodityConsumption = true;

	public bool bolAutoSendSMSByFastConsumption = true;

	public bool bolAutoSendSMSByMemRedTimes = true;

	public bool bolAutoSendSMSByTimingConsumption = true;

	public string SellerAccount = "";

	public string PartnerID = "";

    public string AlipayPrivateKey = "";
    public string AlipayPublicKey = "";

	public string PartnerKey = "";

	public bool IsEditPwdNeedOldPwd = true;

	public bool bolWeiXinType;

	public bool bolWeiXinVerified;

	public int intSignInPoint;

	public string strWeiXinToken = "";

	public string strWeiXinShopName = "";

	public string strWeiXinSalutatory = "";

	public string strWeiXinEncodingAESKey = "";

	public string strWeiXinAppID = "";

	public string strWeiXinAppSecret = "";

	public bool bolIsMemRegisterStaff = false;

	public bool IsMustSlotCard = false;

	public string StorageTimingPrefix = "";

	public bool IsAutoSendSMSByStorageTiming = false;

	public int EnterpriseEmailPort = 25;

	public string EnterpriseEmailDisplayName = "";

	public bool EnterpriseEmailEnableSSL = false;

	public bool EnterpriseEmailUseDefaultCredentials = false;

	public bool bolIsEmail = false;

	public bool bolIsEmailNotice = false;

	public decimal PointDiscountPercent = 0m;

	public string strMemCountExpensePrefix;

	public bool bolIsAutoSendSMSByMemPast = false;

	public int intAutoSendSMSByMemPastForDay = 0;

	public bool bolIsAutoSendSMSByMemBirthday = false;

	public int intAutoSendSMSByMemBirthdayForDay = 0;

	public bool bolIsStartWeiXin = false;

	public bool bolIsStartTimingProject = false;

	public bool bolIsStartMemCount = false;

	public bool bolIsPayWeiXin = true;

	public bool bolIsPayPoint = true;

	public string strEmailUserName = "";

	public string PointNumStr = "";

	public bool bolIsSendCard = false;

	public bool bolShopSmsManage = false;

	public bool bolShopPointManage = false;

	public bool bolIsSettlement = false;

	public bool UsingUnion = Convert.ToBoolean(DESEncrypt.Decrypt(ConfigHelper.GetValue("UsingUnion")));

	public int PrintPreview = 0;

	public int PrintPaperType = 0;

	public bool bolSenseiccard = false;

	public bool bolContacticcard = false;

	public bool isPointSame = true;

	public bool IsSmsSame = true;

	public bool AutoBackupDB = false;

	public int AutoBackupDay = 1;

	public int ProductDecimalNum = Convert.ToInt32(DESEncrypt.Decrypt(ConfigHelper.GetValue("ProductDN"))) % 4;
}
