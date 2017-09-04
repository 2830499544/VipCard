using Chain.BLL;
using Chain.Common.DEncrypt;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class PubFunction
{
	private enum ActionID
	{
		MemAdd = 1,
		MemEdit,
		MemDelete
	}

	public static bool ISCheckKey = false;

	private static CurrentParameter _parameter;

	private static object SynObject = new object();

	public static Dictionary<int, DataTable> _grouAuthority;

	private static object SynGroupObject = new object();

	private static Random r = new Random();

	public static bool IEbrowser = false;

	public static CurrentParameter curParameter
	{
		get
		{
			if (PubFunction._parameter == null)
			{
				lock (PubFunction.SynObject)
				{
					if (PubFunction._parameter == null)
					{
						PubFunction pub = new PubFunction();
						PubFunction._parameter = pub.LoadSysParameter();
					}
				}
			}
			return PubFunction._parameter;
		}
		set
		{
			PubFunction._parameter = null;
		}
	}

	public static int ProductDecimalNum
	{
		get
		{
			return PubFunction.curParameter.ProductDecimalNum;
		}
	}

	public static string ipAdress
	{
		get
		{
			string result;
			if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
			{
				result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[]
				{
					','
				})[0];
			}
			else
			{
				result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
			}
			return result;
		}
	}

	public static DataTable GetGroupAuthority(int GroupID)
	{
		if (PubFunction._grouAuthority == null)
		{
			PubFunction._grouAuthority = new Dictionary<int, DataTable>();
		}
		if (!PubFunction._grouAuthority.ContainsKey(GroupID))
		{
			lock (PubFunction.SynObject)
			{
				if (!PubFunction._grouAuthority.ContainsKey(GroupID))
				{
					PubFunction._grouAuthority.Add(GroupID, PubFunction.GetUserAuthority(GroupID));
				}
			}
		}
		return PubFunction._grouAuthority[GroupID];
	}

	public static void UpdateGroupAuthority(int GroupID)
	{
		PubFunction._grouAuthority.Remove(GroupID);
	}

	public static string TimeEndDay(string timeEnd)
	{
		string result;
		if (timeEnd != "")
		{
			result = DateTime.Parse(timeEnd).AddDays(1.0).ToShortDateString();
		}
		else
		{
			result = timeEnd;
		}
		return result;
	}

	public static void DeleteUpLoad(string strPath)
	{
		try
		{
			if (Directory.Exists(strPath))
			{
				string[] strFiles = Directory.GetFiles(strPath);
				string[] array = strFiles;
				for (int i = 0; i < array.Length; i++)
				{
					string filepath = array[i];
					FileInfo fi = new FileInfo(filepath);
					if (fi.CreationTime <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00"))
					{
						fi.Delete();
					}
				}
			}
		}
		catch
		{
		}
	}

	public static void Get_MenuCaption(int MenuID, ref Label lblTitle, ref Image imgTitle)
	{
		try
		{
			Chain.BLL.SysModule bll = new Chain.BLL.SysModule();
			Chain.Model.SysModule module = bll.GetModel(MenuID);
			lblTitle.Text = module.ModuleCaption;
			imgTitle.ImageUrl = "../" + module.ModuleIcoPath;
		}
		catch
		{
		}
	}

	public static DataTable DoubleAuotesEscape(DataTable dt)
	{
		if (dt != null && dt.Rows.Count > 0)
		{
			for (int rowItem = 0; rowItem < dt.Rows.Count; rowItem++)
			{
				for (int cellItem = 0; cellItem < dt.Columns.Count; cellItem++)
				{
					if (dt.Rows[rowItem][cellItem] != null)
					{
						dt.Rows[rowItem][cellItem] = dt.Rows[rowItem][cellItem].ToString().Replace("\"", "\\\"").Replace("\r\n", "").Replace("\n", "");
					}
				}
			}
		}
		return dt;
	}

	public static void Get_PrintTitle(ref Label litPrintTitle, ref Label litPrintFoot, int ShopID)
	{
		if (PubFunction.curParameter.bolAccordPrint)
		{
			litPrintTitle.Text = PubFunction.curParameter.strPrintTitle;
			litPrintFoot.Text = PubFunction.curParameter.strPrintFootNote;
		}
		else
		{
			Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(ShopID);
			litPrintTitle.Text = modelShop.ShopPrintTitle;
			litPrintFoot.Text = modelShop.ShopPrintFoot;
		}
	}

	public static void AgainPrint(ref Label lblPrintTitle, ref Label lblPrintFoot, int _UserShopID)
	{
		if (PubFunction.curParameter.bolAccordPrint)
		{
			lblPrintTitle.Text = PubFunction.curParameter.strPrintTitle;
			lblPrintFoot.Text = PubFunction.curParameter.strPrintFootNote;
		}
		else
		{
			Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(_UserShopID);
			lblPrintTitle.Text = modelShop.ShopPrintTitle;
			lblPrintFoot.Text = modelShop.ShopPrintFoot;
		}
	}

	public static Chain.Model.SysUser CheckUserLogin(string UserAccount, string Pwd)
	{
		Chain.BLL.SysUser bll = new Chain.BLL.SysUser();
		return bll.CheckUserLogin(UserAccount, DESEncrypt.Encrypt(Pwd));
	}

	public static void SysUpdateMemIsPast()
	{
		Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
		bllMem.SysUpdateMemIsPast();
	}

	public static void SysEdition(int type)
	{
		Chain.BLL.SysModule bllModule = new Chain.BLL.SysModule();
		int intType;
		if (type != 1)
		{
			intType = 0;
		}
		else
		{
			intType = 1;
		}
		bllModule.UpdateIsDataInit(intType);
	}

	public static void SysEnableGoods()
	{
		Chain.BLL.SysModule bllModule = new Chain.BLL.SysModule();
		bllModule.SetEnableGoods(PubFunction.curParameter.EnableGoods);
	}

	public CurrentParameter LoadSysParameter()
	{
		CurrentParameter parameter = new CurrentParameter();
		Chain.Model.SysParameter modelParameter = new Chain.Model.SysParameter();
		Chain.BLL.SysParameter bllParameter = new Chain.BLL.SysParameter();
		modelParameter = bllParameter.GetModel(1);
		parameter.strMchid = modelParameter.MchId;
        parameter.AlipayAppId = modelParameter.AlipayAppid;
		parameter.strMchKey = modelParameter.Api;
		parameter.dclGiveMemMoneyRate = modelParameter.GiveMemMoneyRate;
		parameter.bolPwd = modelParameter.Pwd;
		parameter.RegNullPwd = modelParameter.RegNullPwd;
		parameter.bolMoneyAndPoint = modelParameter.MoneyAndPoint;
		parameter.bolAutoLevel = modelParameter.AutoLevel;
		parameter.bolDegradeLevel = modelParameter.DegradeLevel;
		parameter.bolPastTime = modelParameter.PastTime;
		parameter.PointDiscountPercent = modelParameter.PointDiscountPercent;
		parameter.PointUsePercent = modelParameter.PointUsePercent;
		parameter.intRecommendPoint = modelParameter.RecommendPoint;
		parameter.DrawMoneyPercent = modelParameter.DrawMoneyPercent;
		parameter.intPointPeriod = modelParameter.PointPeriod;
		parameter.strExpensePrefix = modelParameter.ExpensePrefix;
		parameter.strGoodsExpensePrefix = modelParameter.GoodsExpensePrefix;
		parameter.strMemCountPrefix = modelParameter.MemCountPrefix;
		parameter.strMemRechargePrefix = modelParameter.MemRechargePrefix;
		parameter.strGoodsInPrefix = modelParameter.GoodsInPrefix;
		parameter.strGoodsAllotPrefix = modelParameter.GoodsAllotPrefix;
		parameter.strMemDrawMoneyPrefix = modelParameter.MemDrawMoneyPrefix;
		parameter.strMemPointChangePrefix = modelParameter.MemPointChangePrefix;
		parameter.strGiftExchangePrefix = modelParameter.GiftExchangePrefix;
		parameter.strTimeExpensePrefix = modelParameter.TimeExpensePrefix;
		parameter.bolAutoPrint = modelParameter.AutoPrint;
		parameter.bolAccordPrint = modelParameter.AccordPrint;
		parameter.strPrintTitle = modelParameter.PrintTitle;
		parameter.strPrintFootNote = modelParameter.PrintFootNote;
		parameter.bolSms = modelParameter.Sms;
		parameter.bolMoneySms = modelParameter.MoneySms;
		parameter.bolIsSmsShopName = modelParameter.IsSmsShopName;
		parameter.strSmsShopName = modelParameter.SmsShopName;
		parameter.strNotificationSMS = modelParameter.SmsSeries;
		parameter.strNotificationSMSPwd = modelParameter.SmsSerialPwd;
		parameter.bolMarketingSMS = modelParameter.MarketingSMS;
		parameter.strMarketingSmsSeries = modelParameter.MarketingSmsSeries;
		parameter.strMarketingSmsSerialPwd = modelParameter.MarketingSmsSerialPwd;
		parameter.bolTel = modelParameter.Tel;
		parameter.bolTelNoMember = modelParameter.TelNoMember;
		parameter.bolStaff = modelParameter.IsStaff;
		parameter.bolStaffType = modelParameter.StaffType;
		parameter.chkPointLevel = modelParameter.PointLevel;
		parameter.bolMMS = modelParameter.MMS;
		parameter.strMMSSeries = modelParameter.MMSSeries;
		parameter.strMMSSerialPwd = modelParameter.MMSSerialPwd;
		parameter.bolIsPayCard = modelParameter.IsPayCard;
		parameter.bolIsPayCash = modelParameter.IsPayCash;
		parameter.bolIsPayBink = modelParameter.IsPayBink;
		parameter.bolIsPayCoupon = modelParameter.IsPayCoupon;
		parameter.bolIsPayPoint = modelParameter.IsPayPoint;
		parameter.EmailAdress = modelParameter.EmailAdress;
		parameter.EmailPwd = modelParameter.EmailPwd;
		parameter.EmailSMTP = modelParameter.EmailSMTP;
		parameter.intStockCount = modelParameter.StockCount;
		parameter.strUnitList = modelParameter.UnitList;
		parameter.bolWeiXinSMSVcode = modelParameter.WeiXinSMSVcode;
		parameter.bolAutoSendSMSByMemRegister = modelParameter.IsAutoSendSMSByMemRegister;
		parameter.bolAutoSendMMSByMemRegister = modelParameter.IsAutoSendMMSByMemRegister;
		parameter.bolAutoSendSMSByMemRecharge = modelParameter.IsAutoSendSMSByMemRecharge;
		parameter.bolAutoSendSMSByMemWithdraw = modelParameter.IsAutoSendSMSByMemWithdraw;
		parameter.bolAutoSendSMSByMemGiftExchange = modelParameter.IsAutoSendSMSByMemGiftExchange;
		parameter.bolAutoSendSMSByMemPointChange = modelParameter.IsAutoSendSMSByMemPointChange;
		parameter.bolAutoSendSMSByCommodityConsumption = modelParameter.IsAutoSendSMSByCommodityConsumption;
		parameter.bolAutoSendSMSByFastConsumption = modelParameter.IsAutoSendSMSByFastConsumption;
		parameter.bolAutoSendSMSByMemRedTimes = modelParameter.IsAutoSendSMSByMemRedTimes;
		parameter.bolAutoSendSMSByTimingConsumption = modelParameter.IsAutoSendSMSByTimingConsumption;
		parameter.SellerAccount = modelParameter.SellerAccount;
		parameter.PartnerID = modelParameter.PartnerID;
		parameter.PartnerKey = modelParameter.PartnerKey;
		parameter.IsEditPwdNeedOldPwd = modelParameter.IsEditPwdNeedOldPwd;
		parameter.bolWeiXinType = modelParameter.WeiXinType;
		parameter.bolWeiXinVerified = modelParameter.WeiXinVerified;
		parameter.strWeiXinToken = modelParameter.WeiXinToken;
		parameter.strWeiXinShopName = modelParameter.WeiXinShopName;
		parameter.strWeiXinSalutatory = modelParameter.WeiXinSalutatory;
		parameter.strWeiXinAppID = modelParameter.WeiXinAppID;
		parameter.strWeiXinAppSecret = modelParameter.WeiXinAppSecret;
		parameter.intSignInPoint = modelParameter.SignInPoint;
		parameter.bolIsMemRegisterStaff = modelParameter.IsMemRegisterStaff;
		parameter.StorageTimingPrefix = modelParameter.StorageTimingPrefix;
		parameter.IsMustSlotCard = modelParameter.IsMustSlotCard;
		parameter.IsAutoSendSMSByStorageTiming = modelParameter.IsAutoSendSMSByStorageTiming;
		parameter.EnterpriseEmailPort = modelParameter.EnterpriseEmailPort;
		parameter.EnterpriseEmailDisplayName = modelParameter.EnterpriseEmailDisplayName;
		parameter.EnterpriseEmailEnableSSL = modelParameter.EnterpriseEmailEnableSSL;
		parameter.EnterpriseEmailUseDefaultCredentials = modelParameter.EnterpriseEmailUseDefaultCredentials;
		parameter.bolIsEmail = modelParameter.IsEmail;
		parameter.bolIsEmailNotice = modelParameter.IsEmailNotice;
		parameter.strMemCountExpensePrefix = modelParameter.MemCountExpensePrefix;
		parameter.strWeiXinEncodingAESKey = modelParameter.WeiXinEncodingAESKey;
		parameter.bolIsAutoSendSMSByMemPast = modelParameter.IsAutoSendSMSByMemPast;
		parameter.intAutoSendSMSByMemPastForDay = modelParameter.AutoSendSMSByMemPastForDay;
		parameter.bolIsAutoSendSMSByMemBirthday = modelParameter.IsAutoSendSMSByMemBirthday;
		parameter.intAutoSendSMSByMemBirthdayForDay = modelParameter.AutoSendSMSByMemBirthdayForDay;
		parameter.bolIsStartWeiXin = modelParameter.IsStartWeiXin;
		parameter.bolIsStartTimingProject = modelParameter.IsStartTimingProject;
		parameter.bolIsStartMemCount = modelParameter.IsStartMemCount;
		parameter.bolSenseiccard = modelParameter.Senseiccard;
		parameter.bolContacticcard = modelParameter.Contacticcard;
		parameter.strEmailUserName = modelParameter.EmailUserName;
		parameter.AllianceRebatePercent = modelParameter.AllianceRebatePercent;
		parameter.CardShopRebatePercent = modelParameter.CardShopRebatePercent;
		parameter.PointDrawPercent = modelParameter.PointDrawPercent;

        parameter.AlipayPrivateKey = modelParameter.AlipayPrivateKey;
        parameter.AlipayPublicKey = modelParameter.AlipayPublicKey;

		if (parameter.UsingUnion)
		{
			parameter.bolIsSendCard = modelParameter.IsSendCard;
			parameter.bolShopSmsManage = modelParameter.ShopSmsManage;
			parameter.bolShopPointManage = modelParameter.ShopPointManage;
			parameter.bolIsSettlement = modelParameter.ShopSettlement;
		}
		else
		{
			parameter.bolIsSendCard = false;
			parameter.bolShopSmsManage = false;
			parameter.bolShopPointManage = false;
			parameter.bolIsSettlement = false;
		}
		parameter.PointNumStr = modelParameter.PointNumStr;
		parameter.PrintPreview = modelParameter.PrintPreview;
		parameter.PrintPaperType = modelParameter.PrintPaperType;
		parameter.AutoBackupDB = modelParameter.AutoBackupDB;
		parameter.AutoBackupDay = modelParameter.AutoBackupDay;
		if (parameter.strDoMain != modelParameter.SystemDomain)
		{
			modelParameter.SystemDomain = parameter.strDoMain;
			bllParameter.Update(modelParameter);
		}
		return parameter;
	}

	public static DataTable GetUserAuthority(int GroupID)
	{
		Chain.BLL.SysGroup bll = new Chain.BLL.SysGroup();
		return bll.GetGroupAuthority(GroupID).Tables[0];
	}

	public static int GoodsCodeToGoodsID(string goodsCode)
	{
		DataTable dt = new Chain.BLL.Goods().GetList(" GoodsCode='" + goodsCode + "'").Tables[0];
		return int.Parse(dt.Rows[0][0].ToString());
	}

	public static bool GetPageVisit(DataTable db, int PageID)
	{
		DataRow[] dr = db.Select("ActionControl='page' and ModuleID=" + PageID);
		bool result;
		if (dr.Length > 0)
		{
			int parentModelID = Convert.ToInt32(dr[0]["ModuleParentID"]);
			DataRow[] pdr = db.Select(" ModuleID=" + parentModelID);
			if (pdr.Length > 0)
			{
				if (Convert.ToBoolean(pdr[0]["ActionValue"]) && Convert.ToBoolean(dr[0]["ActionValue"]))
				{
					result = true;
					return result;
				}
			}
		}
		result = false;
		return result;
	}

	public static bool GetControlVisit(DataTable db, string strControl, int PageID)
	{
		DataRow[] dr = db.Select(string.Concat(new object[]
		{
			"ActionControl='",
			strControl,
			"' and ModuleID=",
			PageID
		}));
		return dr.Length > 0 && Convert.ToBoolean(dr[0]["ActionValue"]);
	}

	public static bool GetControlVisit(DataTable db, int ActionID, int PageID)
	{
		DataRow[] dr = db.Select(string.Concat(new object[]
		{
			"ActionID=",
			ActionID,
			" and ModuleID=",
			PageID
		}));
		return dr.Length > 0 && Convert.ToBoolean(dr[0]["ActionValue"]);
	}

	public static int GetSmsAmount(string smsContent)
	{
		int length = smsContent.Length;
		double d = (double)length / 65.0;
		return int.Parse(Math.Ceiling(d).ToString());
	}

	public static string GetMemSex(bool bolMemSex)
	{
		string strMemSex;
		if (bolMemSex)
		{
			strMemSex = "男";
		}
		else
		{
			strMemSex = "女";
		}
		return strMemSex;
	}

	public static string GetDataTimeString()
	{
		return DateTime.Now.ToString("hhmmssffff");
	}

	public static bool SetMemSex(int intMemSex)
	{
		return intMemSex == 0;
	}

	public static string GetMemState(int intMemState)
	{
		string strMemState;
		if (intMemState == 0)
		{
			strMemState = "正常";
		}
		else if (intMemState == 1)
		{
			strMemState = "锁定";
		}
		else
		{
			strMemState = "挂失";
		}
		return strMemState;
	}

	public static void BindProvinceSelect(HtmlSelect select)
	{
		Chain.BLL.SysArea bllArea = new Chain.BLL.SysArea();
		DataTable dtArea = bllArea.GetList("PID=0").Tables[0];
		select.Items.Add(new ListItem("=== 请选择 ===", ""));
		foreach (DataRow dr in dtArea.Rows)
		{
			select.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
		}
	}

	public static string SysAreaName(int areaID)
	{
		Chain.Model.SysArea modelArea = new Chain.BLL.SysArea().GetModel(areaID);
		return modelArea.Name;
	}

	public static void BindSysArea(HtmlSelect select, int pid)
	{
		Chain.BLL.SysArea bllArea = new Chain.BLL.SysArea();
		DataTable dtArea = bllArea.GetList(" PID=" + pid).Tables[0];
		select.Items.Add(new ListItem("=== 请选择 ===", ""));
		foreach (DataRow dr in dtArea.Rows)
		{
			select.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
		}
	}

	public static void BindSysAreaMobile(HtmlSelect select, int pid)
	{
		Chain.BLL.SysArea bllArea = new Chain.BLL.SysArea();
		DataTable dtArea = bllArea.GetList(" PID=" + pid).Tables[0];
		select.Items.Clear();
		select.Items.Add(new ListItem("请选择", ""));
		foreach (DataRow dr in dtArea.Rows)
		{
			select.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
		}
	}

	public static void BindSysAreaNew(HtmlSelect select, int pid)
	{
		Chain.BLL.SysArea bllArea = new Chain.BLL.SysArea();
		DataTable dtArea = bllArea.GetList(" PID=" + pid).Tables[0];
		select.Items.Clear();
		select.Items.Add(new ListItem("请选择", ""));
		foreach (DataRow dr in dtArea.Rows)
		{
			select.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
		}
	}

	public static string ShopIDToName(int shopID)
	{
		Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(shopID);
		string result;
		if (modelShop != null)
		{
			result = modelShop.ShopName;
		}
		else
		{
			result = "";
		}
		return result;
	}

	public static string UserIDTOName(int userID)
	{
		Chain.Model.SysUser modelUser = new Chain.BLL.SysUser().GetModel(userID);
		string result;
		if (modelUser != null)
		{
			result = modelUser.UserName;
		}
		else
		{
			result = "";
		}
		return result;
	}

	public static string GroupIDToName(int group)
	{
		Chain.Model.SysGroup modelGroup = new Chain.BLL.SysGroup().GetModel(group);
		string result;
		if (modelGroup != null)
		{
			result = modelGroup.GroupName;
		}
		else
		{
			result = "";
		}
		return result;
	}

	public static DataTable GetShopAuthority(int ShopID)
	{
		Chain.BLL.SysShopAuthority shopAuthority = new Chain.BLL.SysShopAuthority();
		return shopAuthority.GetShopAuthority(ShopID).Tables[0];
	}

	public static void BindShopSelectByShopType(int intShopID, HtmlSelect select, bool bolAddEmpty, int shoptype)
	{
		string strShopAuth = "ShopID>0";
		DataTable dtShopAuth = PubFunction.GetShopAuthority(intShopID);
		if (dtShopAuth.Rows.Count > 0)
		{
			object obj = strShopAuth;
			strShopAuth = string.Concat(new object[]
			{
				obj,
				" and ShopID in (",
				dtShopAuth.Rows[0]["ShopAuthorityData"].ToString(),
				") and ShopState='0' and ShopType=",
				shoptype
			});
		}
		Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
		DataTable dtShop = bllShop.GetList(strShopAuth).Tables[0];
		select.Items.Clear();
		if (bolAddEmpty)
		{
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
		}
		foreach (DataRow dr in dtShop.Rows)
		{
			select.Items.Add(new ListItem(dr["ShopName"].ToString(), dr["ShopID"].ToString()));
		}
	}

	public static void BindShopSelectNew(int intShopID, HtmlSelect select, bool bolAddEmpty, string ShopType)
	{
		string strShopAuth = "ShopID>0";
		DataTable dtShopAuth = PubFunction.GetShopAuthority(intShopID);
		if (dtShopAuth.Rows.Count > 0)
		{
			string text = strShopAuth;
			strShopAuth = string.Concat(new string[]
			{
				text,
				" and ShopID in (",
				dtShopAuth.Rows[0]["ShopAuthorityData"].ToString(),
				") and ShopState='false' and ShopType=",
				ShopType
			});
		}
		Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
		DataTable dtShop = bllShop.GetList(strShopAuth).Tables[0];
		select.Items.Clear();
		if (bolAddEmpty)
		{
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
		}
		foreach (DataRow dr in dtShop.Rows)
		{
			select.Items.Add(new ListItem(dr["ShopName"].ToString(), dr["ShopID"].ToString()));
		}
	}

	public static void BindShopListSelect(int intShopID, int FatherShopID, HtmlSelect select, bool bolAddEmpty)
	{
		string strShopAuth = " ShopID>0 and ShopType=3 ";
		Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
		Chain.Model.SysShop modelShop = bllShop.GetModel(intShopID);
		if (modelShop.ShopType == 1)
		{
			object obj = strShopAuth;
			strShopAuth = string.Concat(new object[]
			{
				obj,
				" and (( IsMain=0 and  FatherShopID=",
				FatherShopID,
				") or ( IsMain=1 ))"
			});
		}
		if (modelShop.ShopType == 2)
		{
			object obj = strShopAuth;
			strShopAuth = string.Concat(new object[]
			{
				obj,
				" and (( IsMain=0 and  FatherShopID=",
				FatherShopID,
				") )"
			});
		}
		DataTable dtShopAuth = PubFunction.GetShopAuthority(intShopID);
		if (dtShopAuth.Rows.Count > 0)
		{
			strShopAuth = strShopAuth + " and ShopID in (" + dtShopAuth.Rows[0]["ShopAuthorityData"].ToString() + ") and ShopState='false'";
		}
		DataTable dtShop = bllShop.GetList(strShopAuth).Tables[0];
		select.Items.Clear();
		if (bolAddEmpty)
		{
			select.Items.Add(new ListItem("=== 请选择 ===", ""));
		}
		foreach (DataRow dr in dtShop.Rows)
		{
			select.Items.Add(new ListItem(dr["ShopName"].ToString(), dr["ShopID"].ToString()));
		}
	}

	public static void BindAllianceListSelect(int intShopID, HtmlSelect select, bool bolAddEmpty)
	{
		string strShopAuth = "ShopID>0 and ShopType=2 ";
		Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
		Chain.Model.SysShop modelShop = bllShop.GetModel(intShopID);
		if (modelShop.ShopType == 3)
		{
			strShopAuth = strShopAuth + " and ShopID=" + modelShop.FatherShopID;
		}
		else
		{
			DataTable dtShopAuth = PubFunction.GetShopAuthority(intShopID);
			if (dtShopAuth.Rows.Count > 0)
			{
				strShopAuth = strShopAuth + " and ShopID in (" + dtShopAuth.Rows[0]["ShopAuthorityData"].ToString() + ") and ShopState='false'";
			}
		}
		DataTable dtShop = bllShop.GetList(strShopAuth).Tables[0];
		select.Items.Clear();
		if (bolAddEmpty)
		{
			select.Items.Add(new ListItem("=== 请选择 ===", ""));
		}
		foreach (DataRow dr in dtShop.Rows)
		{
			select.Items.Add(new ListItem(dr["ShopName"].ToString(), dr["ShopID"].ToString()));
		}
	}

	public static void BindShopSelect(int intShopID, HtmlSelect select, bool bolAddEmpty)
	{
		string strShopAuth = "ShopID>0";
		DataTable dtShopAuth = PubFunction.GetShopAuthority(intShopID);
		if (dtShopAuth.Rows.Count > 0)
		{
			strShopAuth = strShopAuth + " and ShopID in (" + dtShopAuth.Rows[0]["ShopAuthorityData"].ToString() + ") and ShopState='false' ";
		}
		Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
		DataTable dtShop = bllShop.GetList(strShopAuth).Tables[0];
		select.Items.Clear();
		if (bolAddEmpty)
		{
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
		}
		foreach (DataRow dr in dtShop.Rows)
		{
			select.Items.Add(new ListItem(dr["ShopName"].ToString(), dr["ShopID"].ToString()));
		}
	}

	public static void BindShopSelect(int intShopID, HtmlSelect select, int intTragetShopID, bool boolIsDisabled)
	{
		string strShopAuth = "ShopID>0";
		DataTable dtShopAuth = PubFunction.GetShopAuthority(intShopID);
		if (dtShopAuth.Rows.Count > 0)
		{
			strShopAuth = strShopAuth + " and ShopID in (" + dtShopAuth.Rows[0]["ShopAuthorityData"].ToString() + ")";
		}
		Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
		DataTable dtShop = bllShop.GetList(strShopAuth).Tables[0];
		select.Items.Clear();
		if (intTragetShopID == 0)
		{
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
		}
		foreach (DataRow dr in dtShop.Rows)
		{
			ListItem lt = new ListItem(dr["ShopName"].ToString(), dr["ShopID"].ToString());
			if (Convert.ToInt32(dr["ShopID"]) == intTragetShopID)
			{
				lt.Selected = true;
			}
			select.Items.Add(lt);
		}
		select.Disabled = boolIsDisabled;
	}

	public static void BindCouponSelect(HtmlSelect select, bool bolAddEmpty)
	{
		select.Items.Clear();
		if (bolAddEmpty)
		{
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
		}
		Chain.BLL.Coupon bllCoupon = new Chain.BLL.Coupon();
		DataTable dtCoupon = bllCoupon.GetList("").Tables[0];
		foreach (DataRow dr in dtCoupon.Rows)
		{
			if (dr["CouponEffective"].ToString() == "0")
			{
				select.Items.Add(new ListItem(dr["CouponTitle"].ToString(), dr["ID"].ToString()));
			}
			else if (dr["CouponStart"].ToString() != "" && dr["CouponEnd"].ToString() != "")
			{
				DateTime dttStart = DateTime.Parse(dr["CouponStart"].ToString());
				DateTime dttEnd = DateTime.Parse(dr["CouponEnd"].ToString());
				if (dttStart < DateTime.Now && dttEnd > DateTime.Now)
				{
					select.Items.Add(new ListItem(dr["CouponTitle"].ToString(), dr["ID"].ToString()));
				}
			}
			else
			{
				select.Items.Add(new ListItem(dr["CouponTitle"].ToString(), dr["ID"].ToString()));
			}
		}
	}

	public static void BindStaff(int intShopID, HtmlSelect select, bool bolAddEmpty)
	{
		string strShopAuth = " SysShop.ShopID = StaffClass.ClassShopID and StaffClass.ClassID=Staff.StaffClassID ";
		strShopAuth = strShopAuth + " and ClassShopID=" + intShopID;
		Chain.BLL.Staff bllStaff = new Chain.BLL.Staff();
		DataTable dtStaff = bllStaff.GetList(strShopAuth).Tables[0];
		DataView dvStaff = dtStaff.DefaultView;
		dvStaff.Sort = " StaffClassID ASC";
		dtStaff = dvStaff.ToTable();
		select.Items.Clear();
		if (bolAddEmpty)
		{
			ListItem item = new ListItem("=== 请选择 ===", "");
			item.Attributes.Add("ClassPercent", "0");
			select.Items.Add(item);
		}
		foreach (DataRow dr in dtStaff.Rows)
		{
			Chain.BLL.StaffClass bllClass = new Chain.BLL.StaffClass();
			Chain.Model.StaffClass modelClass = new Chain.Model.StaffClass();
			modelClass = bllClass.GetModel(int.Parse(dr["StaffClassID"].ToString()));
			ListItem item = new ListItem(modelClass.ClassName + "--" + dr["StaffName"].ToString(), dr["StaffID"].ToString());
			item.Attributes.Add("ClassPercent", modelClass.ClassPercent.ToString());
			select.Items.Add(item);
		}
	}

	public static void BindStaffClass(int intShopID, HtmlSelect select, bool bolAddEmpty)
	{
		string strShopAuth = " SysShop.ShopID = StaffClass.ClassShopID ";
		DataTable dtShopAuth = PubFunction.GetShopAuthority(intShopID);
		if (dtShopAuth.Rows.Count > 0)
		{
			strShopAuth = strShopAuth + " and ClassShopID in (" + dtShopAuth.Rows[0]["ShopAuthorityData"].ToString() + ")";
		}
		Chain.BLL.StaffClass bllClass = new Chain.BLL.StaffClass();
		DataTable dtClass = bllClass.GetList(strShopAuth).Tables[0];
		DataView dvClass = dtClass.DefaultView;
		dvClass.Sort = " ClassShopID ASC ";
		dtClass = dvClass.ToTable();
		select.Items.Clear();
		if (bolAddEmpty)
		{
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
		}
		foreach (DataRow dr in dtClass.Rows)
		{
			Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
			Chain.Model.SysShop modelShop = new Chain.Model.SysShop();
			modelShop = bllShop.GetModel(int.Parse(dr["ClassShopID"].ToString()));
			select.Items.Add(new ListItem(modelShop.ShopName + "--" + dr["ClassName"].ToString(), dr["ClassID"].ToString()));
		}
	}

	public static void BindAuthoritySelelctByGroupType(int GroupID, HtmlSelect select, bool hasEmpty, int groupType)
	{
		string strSql = string.Format(" (IsPublic = '1' and GroupType= " + groupType + ") ", new object[0]);
		object obj = strSql;
		strSql = string.Concat(new object[]
		{
			obj,
			" and (ParentIDStr like '%/",
			groupType,
			"/%' or GroupID = ",
			groupType,
			")"
		});
		DataTable dtGroup = new Chain.BLL.SysGroup().GetList(strSql).Tables[0];
		if (hasEmpty)
		{
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
		}
		foreach (DataRow dr in dtGroup.Rows)
		{
			select.Items.Add(new ListItem(dr["GroupName"].ToString(), dr["GroupID"].ToString()));
		}
	}

	public static void BindAuthoritySelelct(int ShopID, int GroupID, HtmlSelect select, bool hasEmpty)
	{
		string strSql = string.Format(" (IsPublic = 'true' or CreateUserID in (select UserID from sysUser where UserShopID={0})) ", ShopID);
		object obj = strSql;
		strSql = string.Concat(new object[]
		{
			obj,
			" and (ParentIDStr like '%/",
			GroupID,
			"/%' or GroupID = ",
			GroupID,
			")"
		});
		DataTable dtGroup = new Chain.BLL.SysGroup().GetList(strSql).Tables[0];
		if (hasEmpty)
		{
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
		}
		foreach (DataRow dr in dtGroup.Rows)
		{
			select.Items.Add(new ListItem(dr["GroupName"].ToString(), dr["GroupID"].ToString()));
		}
	}

	public static string GetShopAuthority(int intShopID, string strTableShopName, string SqlWhere)
	{
		string strAuthorityData = "";
		Chain.BLL.SysShopAuthority bllShopAuthority = new Chain.BLL.SysShopAuthority();
		DataTable dtShopAuthority = bllShopAuthority.GetShopAuthority(intShopID).Tables[0];
		if (PubFunction.curParameter.dataAuthority == 0)
		{
			foreach (DataRow drShopAuthority in dtShopAuthority.Rows)
			{
				if (drShopAuthority["ShopAuthorityShopID"].ToString() == intShopID.ToString())
				{
					strAuthorityData = drShopAuthority["ShopAuthorityData"].ToString();
				}
				if (SqlWhere.Length > 0)
				{
					string text = SqlWhere;
					SqlWhere = string.Concat(new string[]
					{
						text,
						" and ",
						strTableShopName,
						" in (",
						strAuthorityData,
						")"
					});
				}
			}
		}
		else if (intShopID > 1)
		{
			object obj = SqlWhere;
			SqlWhere = string.Concat(new object[]
			{
				obj,
				" and ",
				strTableShopName,
				" =",
				intShopID
			});
		}
		else
		{
			SqlWhere = SqlWhere + " and " + strTableShopName + " >0 ";
		}
		return SqlWhere;
	}

	public static string GetMemListShopAuthority(int intShopID, string strTableShopName, string SqlWhere)
	{
		string strAuthorityData = "";
		Chain.BLL.SysShopAuthority bllShopAuthority = new Chain.BLL.SysShopAuthority();
		DataTable dtShopAuthority = bllShopAuthority.GetShopAuthority(intShopID).Tables[0];
		if (intShopID > 1)
		{
			foreach (DataRow drShopAuthority in dtShopAuthority.Rows)
			{
				if (drShopAuthority["ShopAuthorityShopID"].ToString() == intShopID.ToString())
				{
					strAuthorityData = drShopAuthority["ShopAuthorityData"].ToString();
				}
				if (SqlWhere.Length > 0)
				{
					string text = SqlWhere;
					SqlWhere = string.Concat(new string[]
					{
						text,
						" and ",
						strTableShopName,
						" in (",
						strAuthorityData,
						")"
					});
				}
			}
		}
		else
		{
			SqlWhere = SqlWhere + " and " + strTableShopName + " >0 ";
		}
		return SqlWhere;
	}

	public static string GetGoodsAuthority(int intShopID, string strTableShopName, string SqlWhere)
	{
		return "";
	}

	public static void BindUserSelect(int intShopID, HtmlSelect select, bool bolAddEmpty, bool bolAllUser)
	{
		if (bolAddEmpty)
		{
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
		}
		Chain.BLL.SysUser bllUser = new Chain.BLL.SysUser();
		DataTable dtUser;
		if (bolAllUser)
		{
			dtUser = bllUser.GetList("").Tables[0];
		}
		else
		{
			dtUser = bllUser.GetList(PubFunction.GetShopAuthority(intShopID, "UserShopID", "1=1")).Tables[0];
		}
		foreach (DataRow dr in dtUser.Rows)
		{
			select.Items.Add(new ListItem(dr["UserName"].ToString(), dr["UserID"].ToString()));
		}
	}

	public static void BindMemLevelSelect(HtmlSelect select, bool bolAddEmpty)
	{
		select.Items.Clear();
		if (bolAddEmpty)
		{
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
		}
		DataTable dtblLevel = new Chain.BLL.MemLevel().GetList("").Tables[0];
		foreach (DataRow drowLevel in dtblLevel.Rows)
		{
			double LevelPointPercent = double.Parse(drowLevel["LevelPointPercent"].ToString());
			double LevelDiscountPercent = double.Parse(drowLevel["LevelDiscountPercent"].ToString());
			select.Items.Add(new ListItem(drowLevel["LevelName"].ToString(), drowLevel["LevelID"].ToString()));
		}
	}

	public static string LevelIDToName(int levelID)
	{
		Chain.Model.MemLevel modelLevel = new Chain.BLL.MemLevel().GetModel(levelID);
		return modelLevel.LevelName;
	}

	public static void BindSmsTemplate(HtmlSelect select)
	{
		select.Items.Clear();
		select.Items.Add(new ListItem("===== 请选择 =====", ""));
		DataTable dtSmsTemplate = new Chain.BLL.SmsTemplate().GetList("TemplateID>9").Tables[0];
		foreach (DataRow drSmsTemplate in dtSmsTemplate.Rows)
		{
			select.Items.Add(new ListItem(drSmsTemplate["TemplateName"].ToString(), drSmsTemplate["TemplateContent"].ToString()));
		}
	}

	public static string StateToName(int state)
	{
		string result;
		if (state == 0)
		{
			result = "正常";
		}
		else if (state == 1)
		{
			result = "锁定";
		}
		else if (state == 2)
		{
			result = "挂失";
		}
		else
		{
			result = "";
		}
		return result;
	}

	public static string SexToName(bool sex)
	{
		string result;
		if (sex)
		{
			result = "男";
		}
		else
		{
			result = "女";
		}
		return result;
	}

	public static void BindShopArea(HtmlSelect select, bool hasEmpty)
	{
		DataTable dtShop = new Chain.BLL.SysArea().GetList("").Tables[0];
		if (hasEmpty)
		{
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
		}
		foreach (DataRow dr in dtShop.Rows)
		{
			select.Items.Add(new ListItem(dr["AreaName"].ToString(), dr["AreaID"].ToString()));
		}
	}

	public static void SaveSysLog(int userID, int actionID, string type, string detail, int shopID, DateTime createTime, string ipAdress)
	{
		Chain.Model.SysLog modelLog = new Chain.Model.SysLog();
		modelLog.LogUserID = userID;
		modelLog.LogActionID = actionID;
		modelLog.LogType = type;
		modelLog.LogDetail = detail;
		modelLog.LogShopID = shopID;
		modelLog.LogCreateTime = createTime;
		modelLog.LogIPAdress = ipAdress;
		new Chain.BLL.SysLog().Add(modelLog);
	}

	public static string RemoveSpace(string Field)
	{
		string strField = Field.Replace("\n", "");
		return strField.Replace(" ", "");
	}

	public static void BindGoodsClass(HtmlSelect select, int ShopID)
	{
		select.Items.Clear();
		select.Items.Add(new ListItem("===== 请选择 =====", ""));
		DataTable dtGoodsClass = new Chain.BLL.GoodsClass().GetListByShopID(ShopID).Tables[0];
		int index = 1;
		PubFunction.CreateGoodsClassItem(ref select, ref index, dtGoodsClass, "0");
	}

	public static void CreateGoodsClassItem(ref HtmlSelect select, ref int index, DataTable dt, string pid)
	{
		if (pid != "0")
		{
			index++;
		}
		DataRow[] drs = dt.Select(" ParentID=" + pid);
		DataRow[] array = drs;
		for (int j = 0; j < array.Length; j++)
		{
			DataRow dr = array[j];
			string blank = "";
			for (int i = 1; i < index; i++)
			{
				blank += "----";
			}
			ListItem item = new ListItem(blank + dr["ClassName"].ToString(), dr["ClassID"].ToString());
			if (dt.Select("ParentID=" + dr["ClassID"].ToString()).Length > 0)
			{
				item.Attributes.Add("parent", "true");
			}
			select.Items.Add(item);
			PubFunction.CreateGoodsClassItem(ref select, ref index, dt, dr["ClassID"].ToString());
			index--;
		}
	}

	public static string GetClassID(int id)
	{
		string strClass = "";
		DataTable dtGoodsClass = new Chain.BLL.GoodsClass().GetList("").Tables[0];
		DataRow[] drs = dtGoodsClass.Select(" ParentID=" + id);
		strClass += id;
		if (drs.Length > 0)
		{
			DataRow[] array = drs;
			for (int i = 0; i < array.Length; i++)
			{
				DataRow dr = array[i];
				strClass = strClass + "," + int.Parse(dr["ClassID"].ToString());
			}
		}
		return strClass;
	}

	public static void BindGiftClass(HtmlSelect select)
	{
		select.Items.Clear();
		select.Items.Add(new ListItem("===== 请选择 =====", ""));
		DataTable dtGiftClass = new Chain.BLL.GiftClass().GetList("").Tables[0];
		int index = 1;
		PubFunction.CreateGiftClassItem(ref select, ref index, dtGiftClass, "0");
	}

	public static string GetPointNum(string yemian)
	{
		string strNumber = PubFunction.curParameter.PointNumStr;
		string[] Number = strNumber.Split(new char[]
		{
			'$'
		});
		string ss = "";
		switch (yemian)
		{
		case "HYCZ":
			ss = Number[0];
			break;
		case "JFBD":
			ss = Number[1];
			break;
		case "JFDH":
			ss = Number[2];
			break;
		case "SPRK":
			ss = Number[3];
			break;
		case "SPXF":
			ss = Number[4];
			break;
		case "JCXF":
			ss = Number[5];
			break;
		case "KSXF":
			ss = Number[6];
			break;
		case "HYCC":
			ss = Number[7];
			break;
		case "HYCS":
			ss = Number[8];
			break;
		case "XFJL":
			ss = Number[9];
			break;
		case "HYCZBB":
			ss = Number[10];
			break;
		case "JFBDBB":
			ss = Number[11];
			break;
		case "JFDHBB":
			ss = Number[12];
			break;
		case "CKRKML":
			ss = Number[13];
			break;
		case "ZHTX":
			ss = Number[14];
			break;
		}
		return ss;
	}

	public static void CreateGiftClassItem(ref HtmlSelect select, ref int index, DataTable dt, string pid)
	{
		if (pid != "0")
		{
			index++;
		}
		DataRow[] drs = dt.Select(" GiftParentID=" + pid);
		DataRow[] array = drs;
		for (int j = 0; j < array.Length; j++)
		{
			DataRow dr = array[j];
			string blank = "";
			for (int i = 1; i < index; i++)
			{
				blank += "----";
			}
			ListItem item = new ListItem(blank + dr["GiftClassName"].ToString(), dr["GiftClassID"].ToString());
			if (dt.Select("GiftParentID=" + dr["GiftClassID"].ToString()).Length > 0)
			{
				item.Attributes.Add("parent", "true");
			}
			select.Items.Add(item);
			PubFunction.CreateGiftClassItem(ref select, ref index, dt, dr["GiftClassID"].ToString());
			index--;
		}
	}

	public static decimal GetSingleStaffMoney()
	{
		return 0m;
	}

	public static void BindCustomFieldSelect(int ShopID, HtmlSelect select, bool hasEmpty, int intCustomType)
	{
		if (hasEmpty)
		{
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
		}
		Chain.BLL.MemCustomField bllCustom = new Chain.BLL.MemCustomField();
		DataTable dtCustom = new DataTable();
		if (intCustomType == 1)
		{
			dtCustom = bllCustom.GetList(" CustomType=1 and CustomFieldShopID=" + ShopID).Tables[0];
		}
		else
		{
			dtCustom = bllCustom.GetList(" CustomType=2 and CustomFieldShopID=" + ShopID).Tables[0];
		}
		foreach (DataRow dr in dtCustom.Rows)
		{
			select.Items.Add(new ListItem(dr["CustomFieldName"].ToString(), dr["CustomField"].ToString()));
		}
	}

	public static void BindCustomField(int ShopID, HtmlSelect select, bool hasEmpty, int intCustomType)
	{
		if (hasEmpty)
		{
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
		}
		Chain.BLL.MemCustomField bllCustom = new Chain.BLL.MemCustomField();
		DataTable dtCustom = new DataTable();
		if (intCustomType == 1)
		{
			dtCustom = bllCustom.GetList(" CustomType=1 ").Tables[0];
		}
		else
		{
			dtCustom = bllCustom.GetList(" CustomType=2 ").Tables[0];
		}
		foreach (DataRow dr in dtCustom.Rows)
		{
			ListItem item = new ListItem(dr["CustomFieldName"].ToString(), dr["CustomField"].ToString());
			item.Attributes.Add("CustomFieldType", dr["CustomFieldType"].ToString());
			item.Attributes.Add("CustomFieldInfo", dr["CustomFieldInfo"].ToString());
			select.Items.Add(item);
		}
	}

	public static void BindAddCustomFields(HtmlContainerControl container, string type)
	{
		Chain.BLL.MemCustomField bllCustom = new Chain.BLL.MemCustomField();
		DataRow[] drs = bllCustom.CustomGetList("CustomType=" + ((type == "Mem") ? "1" : "2"));
		string strPre = (type == "Mem") ? "Mem_Custom_" : "Goods_Custom_";
		int index = 0;
		StringBuilder sbHtml = new StringBuilder();
		if (drs.Length <= 0)
		{
			sbHtml.Append("<tr>");
			sbHtml.Append("<td style='text-align:left;padding:2px;' colspan='4'>您可以根据需要在‘系统管理’---> ‘自定义属性设置’ 增加自定义属性。</td>");
			sbHtml.Append("</tr>");
		}
		StringBuilder sbControl = new StringBuilder("<script>");
		DataRow[] array = drs;
		for (int j = 0; j < array.Length; j++)
		{
			DataRow dr = array[j];
			if (index % 2 == 0)
			{
				sbHtml.Append("<tr>\n");
			}
			sbHtml.Append("<td class='tableStyle_left'>");
			if (!Convert.ToBoolean(dr["CustomFieldIsNull"].ToString()))
			{
				sbHtml.Append("<font color=\"#ff4800\"><strong>*</strong></font>");
			}
			sbHtml.Append(string.Concat(new object[]
			{
				"<span id='",
				strPre,
				"T_",
				dr["CustomField"],
				"'>",
				dr["CustomFieldName"],
				"</span>：</td>\n"
			}));
			sbHtml.Append("<td class='tableStyle_right' style='width: 270px'>");
			if (dr["CustomFieldType"].ToString() == "text")
			{
				sbHtml.Append(string.Concat(new object[]
				{
					"<input type='text' id='",
					strPre,
					dr["CustomField"],
					"' name='",
					strPre,
					dr["CustomField"],
					"' isNull='",
					dr["CustomFieldIsNull"],
					"' class='border_radius'/>"
				}));
			}
			else if (dr["CustomFieldType"].ToString() == "select")
			{
				string[] strInfo = dr["CustomFieldInfo"].ToString().Split("|".ToCharArray());
				sbHtml.Append(string.Concat(new object[]
				{
					"<select id='",
					strPre,
					dr["CustomField"],
					"' name='",
					strPre,
					dr["CustomField"],
					"' class='selectWidth' isNull='",
					dr["CustomFieldIsNull"],
					"' isSelect='true' >"
				}));
				sbHtml.Append("<option value='无'>===== 请选择 =====</option>");
				for (int i = 0; i < strInfo.Length; i++)
				{
					sbHtml.Append(string.Concat(new string[]
					{
						"<option value='",
						strInfo[i],
						"'>",
						strInfo[i],
						"</option>"
					}));
				}
				sbHtml.Append("</select>");
			}
			else if (dr["CustomFieldType"].ToString() == "date")
			{
				sbHtml.Append(string.Concat(new object[]
				{
					"<input type='text' id='",
					strPre,
					dr["CustomField"],
					"' class='Wdate border_radius' name='",
					strPre,
					dr["CustomField"],
					"' isNull='",
					dr["CustomFieldIsNull"],
					"' onfocus=\"WdatePicker({ skin: 'ext', isShowClear: false, readOnly: true });\"/>"
				}));
			}
			sbHtml.Append("</td>\n");
			if (index % 2 == 1)
			{
				sbHtml.Append("</tr>\n");
			}
			index++;
		}
		if (drs.Length != 0)
		{
			if (drs.Length % 2 == 1)
			{
				sbHtml.Append("<td class='tableStyle_left'></td><td class='tableStyle_right' style='width: 270px'></td></tr>\n");
			}
		}
		sbControl.Append("</script>");
		container.InnerHtml = sbHtml.ToString() + sbControl.ToString();
	}

	public static void BindEditCustomFields(HtmlContainerControl container, string type, DataRow row)
	{
		Chain.BLL.MemCustomField bllCustom = new Chain.BLL.MemCustomField();
		DataRow[] drs = bllCustom.CustomGetList("CustomType=" + ((type == "Mem") ? "1" : "2"));
		string strPre = (type == "Mem") ? "Mem_Custom_" : "Goods_Custom_";
		int index = 0;
		StringBuilder sbHtml = new StringBuilder();
		if (drs.Length <= 0)
		{
			sbHtml.Append("<tr>");
			sbHtml.Append("<td style='text-align:left;padding:2px;' colspan='4'>您可以根据需要在‘系统管理’---> ‘自定义属性设置’ 增加自定义属性。</td>");
			sbHtml.Append("</tr>");
		}
		StringBuilder sbControl = new StringBuilder("<script>");
		DataRow[] array = drs;
		for (int j = 0; j < array.Length; j++)
		{
			DataRow dr = array[j];
			if (index % 2 == 0)
			{
				sbHtml.Append("<tr>\n");
			}
			sbHtml.Append("<td class='tableStyle_left'>");
			if (!Convert.ToBoolean(dr["CustomFieldIsNull"].ToString()))
			{
				sbHtml.Append("<font color=\"#ff4800\"><strong>*</strong></font>");
			}
			sbHtml.Append(string.Concat(new object[]
			{
				"<span id='",
				strPre,
				"T_",
				dr["CustomField"],
				"'>",
				dr["CustomFieldName"],
				"</span>：</td>\n"
			}));
			sbHtml.Append("<td class='tableStyle_right' style='width: 270px'>");
			if (dr["CustomFieldType"].ToString() == "text")
			{
				sbHtml.Append(string.Concat(new object[]
				{
					"<input type='text' id='",
					strPre,
					dr["CustomField"],
					"' name='",
					strPre,
					dr["CustomField"],
					"' isNull='",
					dr["CustomFieldIsNull"],
					"' class='border_radius'  value='",
					row[dr["CustomField"].ToString()].ToString(),
					"'/>"
				}));
			}
			else if (dr["CustomFieldType"].ToString() == "select")
			{
				string[] strInfo = dr["CustomFieldInfo"].ToString().Split("|".ToCharArray());
				sbHtml.Append(string.Concat(new object[]
				{
					"<select id='",
					strPre,
					dr["CustomField"],
					"' name='",
					strPre,
					dr["CustomField"],
					"' class='selectWidth' isNull='",
					dr["CustomFieldIsNull"],
					"'isSelect='true' >"
				}));
				sbHtml.Append("<option value='无'>===== 请选择 =====</option>");
				for (int i = 0; i < strInfo.Length; i++)
				{
					if (strInfo[i] == row[dr["CustomField"].ToString()].ToString())
					{
						sbHtml.Append(string.Concat(new string[]
						{
							"<option value='",
							strInfo[i],
							"' selected>",
							strInfo[i],
							"</option>"
						}));
					}
					else
					{
						sbHtml.Append(string.Concat(new string[]
						{
							"<option value='",
							strInfo[i],
							"'>",
							strInfo[i],
							"</option>"
						}));
					}
				}
				sbHtml.Append("</select>");
			}
			else if (dr["CustomFieldType"].ToString() == "date")
			{
				sbHtml.Append(string.Concat(new object[]
				{
					"<input type='text' id='",
					strPre,
					dr["CustomField"],
					"' class='Wdate border_radius' name='",
					strPre,
					dr["CustomField"],
					"' isNull='",
					dr["CustomFieldIsNull"],
					"' value='",
					row[dr["CustomField"].ToString()].ToString(),
					"' onfocus=\"WdatePicker({ skin: 'ext', isShowClear: false, readOnly: true });\"/>"
				}));
			}
			sbHtml.Append("</td>\n");
			if (index % 2 == 1)
			{
				sbHtml.Append("</tr>\n");
			}
			index++;
		}
		if (drs.Length != 0)
		{
			if (drs.Length % 2 == 1)
			{
				sbHtml.Append("<td class='tableStyle_left'></td><td class='tableStyle_right' style='width: 270px'></td></tr>\n");
			}
		}
		sbControl.Append("</script>");
		container.InnerHtml = sbHtml.ToString() + sbControl.ToString();
	}

	public static void BindMemInfoCustomFields(HtmlContainerControl container, string type, string pre)
	{
		Chain.BLL.MemCustomField bllCustom = new Chain.BLL.MemCustomField();
		DataRow[] drs = bllCustom.CustomGetList("CustomType=" + ((type == "Mem") ? "1" : "2"));
		int index = 0;
		StringBuilder sbHtml = new StringBuilder();
		DataRow[] array = drs;
		for (int i = 0; i < array.Length; i++)
		{
			DataRow dr = array[i];
			if (index % 2 == 0)
			{
				sbHtml.Append("<tr>\n");
			}
			sbHtml.Append(string.Concat(new object[]
			{
				"<td class='tableStyle_left' id='",
				pre,
				"T_",
				dr["CustomField"],
				"'>",
				dr["CustomFieldName"],
				"：</td>\n"
			}));
			sbHtml.Append(string.Concat(new object[]
			{
				"<td class='tableStyle_right'><span id='",
				pre,
				dr["CustomField"],
				"'></span></td>\n"
			}));
			if (index % 2 == 1)
			{
				sbHtml.Append("</tr>\n");
			}
			index++;
		}
		if (drs.Length != 0)
		{
			if (sbHtml.ToString().Substring(sbHtml.Length - 5, 5) != "</tr>")
			{
				sbHtml.Append("<td class='tableStyle_left'></td><td  class='tableStyle_right'></td></tr>\n");
			}
		}
		container.InnerHtml = sbHtml.ToString();
	}

	public static string UpdateMemLevel(Chain.Model.Mem memInfo)
	{
		string str = "";
		if (PubFunction.curParameter.bolAutoLevel)
		{
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			List<Chain.Model.MemLevel> listLevel = new Chain.BLL.MemLevel().GetModelList("  SysShopMemLevel.ShopID=" + memInfo.MemShopID);
			Chain.Model.MemLevel level = listLevel.Find((Chain.Model.MemLevel p) => p.LevelID == memInfo.MemLevelID);
			if (!level.LevellLock)
			{
				listLevel.Sort((Chain.Model.MemLevel p1, Chain.Model.MemLevel p2) => Comparer<int>.Default.Compare(p1.LevelPoint, p2.LevelPoint));
				if (memInfo.MemPoint > 0)
				{
					Chain.Model.MemLevel computelevel = listLevel.FindLast((Chain.Model.MemLevel p) => p.LevelPoint <= memInfo.MemPoint);
					int oldLevel = listLevel.FindIndex(0, (Chain.Model.MemLevel p) => p.LevelID == memInfo.MemLevelID);
					int newLevel = listLevel.FindIndex(0, (Chain.Model.MemLevel p) => p.LevelID == computelevel.LevelID);
					if (newLevel != oldLevel)
					{
						if (newLevel > oldLevel)
						{
							bllMem.UpdateLevel(memInfo.MemID, computelevel.LevelID);
							str = string.Concat(new string[]
							{
								"  该会员已经从[",
								level.LevelName,
								"]升级为[",
								computelevel.LevelName,
								"]"
							});
							PubFunction.SaveSysLog(memInfo.MemUserID, 4, "会员自动升级", string.Concat(new string[]
							{
								"会员升级,会员卡号：[",
								memInfo.MemCard,
								"],姓名：[",
								memInfo.MemName,
								"]"
							}), memInfo.MemUserID, DateTime.Now, PubFunction.ipAdress);
							if (memInfo.MemMobile != "")
							{
								if (PubFunction.curParameter.bolMoneySms)
								{
									string strSendContent = SMSInfo.GetSendContent(6, new SmsTemplateParameter
									{
										strCardID = memInfo.MemCard,
										strName = memInfo.MemName,
										dclTempMoney = 0m,
										dclMoney = memInfo.MemMoney,
										intTempPoint = 0,
										intPoint = memInfo.MemPoint,
										OldLevelID = level.LevelID,
										NewLevelID = computelevel.LevelID
									}, memInfo.MemShopID);
									SMSInfo.Send_GXSMS(false, memInfo.MemMobile, strSendContent, "");
									Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
									modelSms.SmsMemID = memInfo.MemID;
									modelSms.SmsMobile = memInfo.MemMobile;
									modelSms.SmsContent = strSendContent;
									modelSms.SmsTime = DateTime.Now;
									modelSms.SmsShopID = memInfo.MemShopID;
									modelSms.SmsUserID = memInfo.MemUserID;
									modelSms.SmsAmount = PubFunction.GetSmsAmount(strSendContent);
									modelSms.SmsAllAmount = modelSms.SmsAmount;
									Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
									bllSms.Add(modelSms);
									PubFunction.SaveSysLog(memInfo.MemUserID, 4, "自动发送会员等级短信", string.Concat(new string[]
									{
										"会员等级变动,会员卡号：[",
										memInfo.MemCard,
										"],姓名：[",
										memInfo.MemName,
										"]"
									}), memInfo.MemUserID, DateTime.Now, PubFunction.ipAdress);
								}
							}
						}
						else if (PubFunction.curParameter.bolDegradeLevel)
						{
							bllMem.UpdateLevel(memInfo.MemID, computelevel.LevelID);
							str = string.Concat(new string[]
							{
								"  该会员已经从[",
								level.LevelName,
								"]降级为[",
								computelevel.LevelName,
								"]"
							});
							PubFunction.SaveSysLog(memInfo.MemUserID, 4, "会员自动降级", string.Concat(new string[]
							{
								"会员降级,会员卡号：[",
								memInfo.MemCard,
								"],姓名：[",
								memInfo.MemName,
								"]"
							}), memInfo.MemUserID, DateTime.Now, PubFunction.ipAdress);
							if (memInfo.MemMobile != "")
							{
								if (PubFunction.curParameter.bolMoneySms)
								{
									string strSendContent = SMSInfo.GetSendContent(6, new SmsTemplateParameter
									{
										strCardID = memInfo.MemCard,
										strName = memInfo.MemName,
										dclTempMoney = 0m,
										dclMoney = memInfo.MemMoney,
										intTempPoint = 0,
										intPoint = memInfo.MemPoint,
										OldLevelID = level.LevelID,
										NewLevelID = computelevel.LevelID
									}, memInfo.MemShopID);
									SMSInfo.Send_GXSMS(false, memInfo.MemMobile, strSendContent, "");
									Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
									modelSms.SmsMemID = memInfo.MemID;
									modelSms.SmsMobile = memInfo.MemMobile;
									modelSms.SmsContent = strSendContent;
									modelSms.SmsTime = DateTime.Now;
									modelSms.SmsShopID = memInfo.MemShopID;
									modelSms.SmsUserID = memInfo.MemUserID;
									modelSms.SmsAmount = PubFunction.GetSmsAmount(strSendContent);
									modelSms.SmsAllAmount = modelSms.SmsAmount;
									Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
									bllSms.Add(modelSms);
									PubFunction.SaveSysLog(memInfo.MemUserID, 4, "自动发送会员等级短信", string.Concat(new string[]
									{
										"会员等级变动,会员卡号：[",
										memInfo.MemCard,
										"],姓名：[",
										memInfo.MemName,
										"]"
									}), memInfo.MemUserID, DateTime.Now, PubFunction.ipAdress);
								}
							}
						}
					}
				}
			}
		}
		return str;
	}

	public static string CreateRandomNumber(int length)
	{
		string strLetters = "1234567890";
		StringBuilder s = new StringBuilder();
		for (int i = 0; i < length; i++)
		{
			s.Append(strLetters.Substring(PubFunction.r.Next(0, strLetters.Length - 1), 1));
		}
		return DateTime.Now.ToString("HHmmssffff") + s.ToString();
	}

	public static void TimeExpenseEnd(string strOrderAccount, int uid, DateTime date, string strOrderRemark)
	{
		Chain.BLL.OrderTime bllOrderTime = new Chain.BLL.OrderTime();
		Chain.Model.OrderTime modelOrderTime = new Chain.Model.OrderTime();
		DataTable dtTiemExpense = bllOrderTime.GetList(string.Format("OrderTimeCode = '{0}'", strOrderAccount)).Tables[0];
		if (dtTiemExpense.Rows.Count > 0)
		{
			modelOrderTime = bllOrderTime.GetModel(int.Parse(dtTiemExpense.Rows[0]["OrderTimeID"].ToString()));
			modelOrderTime.OrderState = true;
			modelOrderTime.OrderOutTime = date;
			modelOrderTime.OrderEndUserID = uid;
			modelOrderTime.OrderRemark = strOrderRemark;
			bllOrderTime.Update(modelOrderTime);
		}
	}

	public static void SetPageNavigation(int MenuID, ref Literal lblTitle)
	{
		StringBuilder strHtml = new StringBuilder();
		strHtml.Append("<a href='/StartPage.aspx'>主页</a>&nbsp;&nbsp;>");
		Chain.BLL.SysModule bllSysModule = new Chain.BLL.SysModule();
		Chain.Model.SysModule mdSysModule = bllSysModule.GetModel(MenuID);
		if (mdSysModule.ModuleParentID > 0)
		{
			strHtml.Append(PubFunction.BindFatherHtml(mdSysModule.ModuleParentID, ""));
		}
		strHtml.Append(string.Format("&nbsp;&nbsp;{0}", mdSysModule.ModuleCaption));
		lblTitle.Text = strHtml.ToString();
	}

	public static string BindFatherHtml(int fatherID, string strhtml)
	{
		Chain.BLL.SysModule bllSysModule = new Chain.BLL.SysModule();
		Chain.Model.SysModule mdSysModule = bllSysModule.GetModel(fatherID);
		if (mdSysModule.ModuleParentID > 0)
		{
			strhtml += PubFunction.BindFatherHtml(mdSysModule.ModuleParentID, strhtml);
		}
		if (!string.IsNullOrEmpty(mdSysModule.ModuleLink))
		{
			strhtml += string.Format("&nbsp;&nbsp;<a href='{0}'>{1}</a>&nbsp;&nbsp;>", "/" + mdSysModule.ModuleLink, mdSysModule.ModuleCaption);
		}
		else
		{
			strhtml += string.Format("&nbsp;&nbsp;{0}&nbsp;&nbsp;>", mdSysModule.ModuleCaption);
		}
		return strhtml;
	}

	public static void SubTractCont(int mytime, int memid, int projectid)
	{
		Chain.BLL.MemStorageTiming bllMemStorageTiming = new Chain.BLL.MemStorageTiming();
		DataTable dtMemStorageTiming = bllMemStorageTiming.GetList(string.Format("StorageResidueTime > 0 AND StorageTimingMemID = {0} AND StorageTimingProjectID = {1}", memid, projectid)).Tables[0];
		foreach (DataRow drMemStorageTiming in dtMemStorageTiming.Rows)
		{
			int owntime = Convert.ToInt32(drMemStorageTiming["StorageResidueTime"]);
			if (owntime >= mytime)
			{
				bllMemStorageTiming.UpdateStorageResidueTime(Convert.ToInt32(drMemStorageTiming["StorageTimingID"]), owntime - mytime);
				mytime = 0;
			}
			else
			{
				bllMemStorageTiming.UpdateStorageResidueTime(Convert.ToInt32(drMemStorageTiming["StorageTimingID"]), 0);
				mytime -= owntime;
			}
			if (mytime == 0)
			{
				break;
			}
		}
	}

	public static bool IsCanRegisterCard(int ShopID, string MemCard, string MemCardNumber)
	{
		bool result = false;
		bool result2;
		try
		{
			long intCard = Convert.ToInt64(MemCard);
			long lenCard = (long)MemCard.Length;
			Chain.BLL.SysShopBuyCard bllSysShopBuyCard = new Chain.BLL.SysShopBuyCard();
			string strCondition = " CONVERT(BIGINT,StartCardNumber) < = '{0}' AND CONVERT(BIGINT,EndCardNumber) > = '{1}' AND\r\n                           LEN(StartCardNumber) < = '{2}' AND LEN(EndCardNumber) > = '{3}' AND BuyCardShopid = '{4}'";
			string StrWhere = string.Format(strCondition, new object[]
			{
				intCard,
				intCard,
				lenCard,
				lenCard,
				ShopID
			});
			DataTable dt = bllSysShopBuyCard.GetList(StrWhere).Tables[0];
			if (dt.Rows.Count > 0)
			{
				result2 = true;
				return result2;
			}
			if (!string.IsNullOrEmpty(MemCardNumber))
			{
				long intCardNumber = Convert.ToInt64(MemCardNumber);
				long lenCardNumber = (long)MemCardNumber.Length;
				StrWhere = string.Format(strCondition, new object[]
				{
					intCardNumber,
					intCardNumber,
					lenCardNumber,
					lenCardNumber,
					ShopID
				});
				dt = bllSysShopBuyCard.GetList(StrWhere).Tables[0];
				if (dt.Rows.Count > 0)
				{
					result2 = true;
					return result2;
				}
			}
		}
		catch
		{
		}
		result2 = result;
		return result2;
	}

	public static bool IsCanBuyCard(string StartCardNumber, string EndCardNumber, string IsAllianceProgram, int shopID)
	{
		bool result = true;
		long intStart = Convert.ToInt64(StartCardNumber);
		long intEnd = Convert.ToInt64(EndCardNumber);
		Chain.BLL.SysShopBuyCard bllSysShopBuyCard = new Chain.BLL.SysShopBuyCard();
		string strWhere = " LEN(StartCardNumber) = '{0}' AND ((CONVERT(BIGINT,StartCardNumber) < ='{1}' AND CONVERT(BIGINT,EndCardNumber) >='{2}')\r\n                           OR (CONVERT(BIGINT,StartCardNumber) < ='{3}' AND CONVERT(BIGINT,EndCardNumber) >='{4}'))";
		strWhere = string.Format(strWhere, new object[]
		{
			StartCardNumber.Length,
			intStart,
			intStart,
			intEnd,
			intEnd
		});
		if (IsAllianceProgram == "False")
		{
			strWhere += string.Format(" AND BuyCardShopid Not IN (SELECT FatherShopID FROM dbo.SysShop WHERE ShopID={0})", shopID);
		}
		DataTable dtSysShopBuyCard = bllSysShopBuyCard.GetList(strWhere).Tables[0];
		if (dtSysShopBuyCard.Rows.Count > 0)
		{
			result = false;
		}
		return result;
	}

	public static bool isInAllianceBuyCard(string StartCardNumber, string EndCardNumber, int ShopID)
	{
		Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
		long intStart = Convert.ToInt64(StartCardNumber);
		long intEnd = Convert.ToInt64(EndCardNumber);
		string strWhere = " LEN(StartCardNumber) = '{0}' AND ((CONVERT(BIGINT,StartCardNumber) < ='{1}' AND CONVERT(BIGINT,EndCardNumber) >='{2}')\r\n                           OR (CONVERT(BIGINT,StartCardNumber) < ='{3}' AND CONVERT(BIGINT,EndCardNumber) >='{4}'))";
		strWhere = string.Format(strWhere, new object[]
		{
			StartCardNumber.Length,
			intStart,
			intStart,
			intEnd,
			intEnd
		});
		DataTable dt = bllSysShop.GetAllianceByCard(strWhere, ShopID);
		return dt.Rows.Count > 0;
	}

	public static string GetProperties<T>(T t)
	{
		string tStr = string.Empty;
		string result;
		if (t == null)
		{
			result = tStr;
		}
		else
		{
			PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			if (properties.Length <= 0)
			{
				result = tStr;
			}
			else
			{
				PropertyInfo[] array = properties;
				for (int i = 0; i < array.Length; i++)
				{
					PropertyInfo item = array[i];
					string name = item.Name;
					object value = item.GetValue(t, null);
					if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
					{
						if (string.IsNullOrEmpty(tStr))
						{
							tStr += string.Format("{0}:'{1}'", name, value);
						}
						else
						{
							tStr += string.Format(",{0}:'{1}'", name, value);
						}
					}
					else
					{
						PubFunction.GetProperties<object>(value);
					}
				}
				result = tStr;
			}
		}
		return result;
	}

	public static bool IsCanSendSms(int ShopID, int MemMobileCount)
	{
		bool result;
		if (!PubFunction.curParameter.UsingUnion)
		{
			result = true;
		}
		else
		{
			Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
			Chain.Model.SysShop mdSysShop = bllSysShop.GetModel(ShopID);
			if (ShopID > 1)
			{
				if (PubFunction.curParameter.IsSmsSame)
				{
					if (mdSysShop.SmsCount < MemMobileCount && mdSysShop.SmsType == 1)
					{
						result = false;
						return result;
					}
				}
			}
			result = true;
		}
		return result;
	}

	public static void SetShopSms(int UserID, int ShopID, int MemMobileCount, int Type)
	{
		if (ShopID > 1)
		{
			Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
			Chain.Model.SysShop mdSysShop = bllSysShop.GetModel(ShopID);
			if (PubFunction.curParameter.IsSmsSame)
			{
				if (mdSysShop.SmsCount >= MemMobileCount || mdSysShop.SmsType != 1)
				{
					mdSysShop.SmsCount -= MemMobileCount;
					bllSysShop.Update(mdSysShop);
					Chain.BLL.SysShopCmsLog bllSysShopCmsLog = new Chain.BLL.SysShopCmsLog();
					bllSysShopCmsLog.Add(new Chain.Model.SysShopCmsLog
					{
						Count = -1 * MemMobileCount,
						CreateTime = DateTime.Now,
						OutShopID = ShopID,
						ShopCmsAccount = "DX" + DateTime.Now.ToString("yyMMddHHmmssffff"),
						ShopCmsType = Type,
						ShopID = ShopID,
						UserID = UserID,
						Remark = mdSysShop.ShopName + "商家使用短信"
					});
				}
			}
		}
	}

	public static void SetShopSmsNew(int UserID, int ShopID, int UserShopID, int MemMobileCount, int Type, string Remark)
	{
		Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
		Chain.Model.SysShop mdSysShop = bllSysShop.GetModel(ShopID);
		if (ShopID == 1 || mdSysShop.SmsCount >= MemMobileCount || mdSysShop.SmsType != 1)
		{
			mdSysShop.SmsCount -= MemMobileCount;
			bllSysShop.Update(mdSysShop);
			Chain.BLL.SysShopCmsLog bllSysShopCmsLog = new Chain.BLL.SysShopCmsLog();
			bllSysShopCmsLog.Add(new Chain.Model.SysShopCmsLog
			{
				Count = -1 * MemMobileCount,
				CreateTime = DateTime.Now,
				OutShopID = ShopID,
				ShopCmsAccount = "DX" + DateTime.Now.ToString("yyMMddHHmmssffff"),
				ShopCmsType = Type,
				ShopID = UserShopID,
				UserID = UserID,
				Remark = Remark
			});
		}
	}

	public static bool IsShopPoint(int ShopID, ref int Point)
	{
		bool result;
		if (!PubFunction.curParameter.UsingUnion || !PubFunction.curParameter.bolShopPointManage)
		{
			result = true;
		}
		else
		{
			if (ShopID > 1 && Point != 0)
			{
				Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
				Chain.Model.SysShop mdSysShop = new Chain.Model.SysShop();
				mdSysShop = bllSysShop.GetModel(ShopID);
				if (PubFunction.curParameter.isPointSame)
				{
					if (mdSysShop.PointCount < Point)
					{
						if (mdSysShop.PointType == 1)
						{
							result = false;
							return result;
						}
						if (mdSysShop.PointType == 2)
						{
							Point = 0;
						}
					}
				}
			}
			result = true;
		}
		return result;
	}

	public static void SetShopPoint(int UserID, int ShopID, int outShopID, int Point, string Remark, int Type)
	{
		if (Point != 0)
		{
			Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
			Chain.Model.SysShop mdSysShop = new Chain.Model.SysShop();
			mdSysShop = bllSysShop.GetModel(outShopID);
			if (PubFunction.curParameter.isPointSame)
			{
				if (mdSysShop.PointType != 1 || Point <= mdSysShop.PointCount)
				{
					mdSysShop.PointCount -= Point;
					bllSysShop.Update(mdSysShop);
					Chain.BLL.SysShopPointLog bllSysShopPointLog = new Chain.BLL.SysShopPointLog();
					bllSysShopPointLog.Add(new Chain.Model.SysShopPointLog
					{
						Count = -1 * Point,
						CreateTime = DateTime.Now,
						OutShopID = outShopID,
						ShopID = ShopID,
						ShopPointAccount = "JF" + DateTime.Now.ToString("yyMMddHHmmssffff"),
						ShopPointType = Type,
						UserID = UserID,
						Remark = Remark
					});
				}
			}
		}
	}

	public static void SetShopPoint(int UserID, int ShopID, int Point, string Remark, int Type)
	{
		if (Point != 0)
		{
			Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
			Chain.Model.SysShop mdSysShop = new Chain.Model.SysShop();
			mdSysShop = bllSysShop.GetModel(ShopID);
			if (PubFunction.curParameter.isPointSame)
			{
				if (mdSysShop.PointType != 1 || Point <= mdSysShop.PointCount)
				{
					mdSysShop.PointCount -= Point;
					bllSysShop.Update(mdSysShop);
					Chain.BLL.SysShopPointLog bllSysShopPointLog = new Chain.BLL.SysShopPointLog();
					bllSysShopPointLog.Add(new Chain.Model.SysShopPointLog
					{
						Count = -1 * Point,
						CreateTime = DateTime.Now,
						OutShopID = ShopID,
						ShopID = ShopID,
						ShopPointAccount = "JF" + DateTime.Now.ToString("yyMMddHHmmssffff"),
						ShopPointType = Type,
						UserID = UserID,
						Remark = Remark
					});
				}
			}
		}
	}

	public static void AddShopPoint(int UserID, int ShopID, int Point, string Remark)
	{
		if (Point != 0)
		{
			if (ShopID > 1)
			{
				if (PubFunction.curParameter.isPointSame)
				{
					Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
					Chain.Model.SysShop mdSysShop = new Chain.Model.SysShop();
					mdSysShop = bllSysShop.GetModel(ShopID);
					mdSysShop.PointCount += Point;
					bllSysShop.Update(mdSysShop);
					Chain.BLL.SysShopPointLog bllSysShopPointLog = new Chain.BLL.SysShopPointLog();
					bllSysShopPointLog.Add(new Chain.Model.SysShopPointLog
					{
						Count = Point,
						Remark = Remark,
						CreateTime = DateTime.Now,
						OutShopID = ShopID,
						ShopID = ShopID,
						ShopPointAccount = "JF" + DateTime.Now.ToString("yyMMddHHmmssffff"),
						ShopPointType = 3,
						UserID = UserID
					});
				}
			}
		}
	}
}
