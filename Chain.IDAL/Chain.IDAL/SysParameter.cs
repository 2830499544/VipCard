using Chain.DBUtility;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysParameter
	{
		public bool Exists(int ParameterID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysParameter");
			strSql.Append(" where ParameterID=@ParameterID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ParameterID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ParameterID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysParameter model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysParameter(");
            strSql.Append("Pwd,MoneyAndPoint,AutoLevel,DegradeLevel,PastTime,RecommendPoint,PointPeriod,ExpensePrefix,GoodsExpensePrefix,TimeExpensePrefix,MemCountPrefix,MemRechargePrefix,GoodsInPrefix,GoodsAllotPrefix,MemDrawMoneyPrefix,MemPointChangePrefix,GiftExchangePrefix,AutoPrint,AccordPrint,PrintTitle,PrintFootNote,Sms,MoneySms,IsSmsShopName,SmsShopName,SmsSeries,SmsSerialPwd,DrawMoneyPercent,Tel,TelNoMember,IsStaff,StaffType,PointLevel,MMS,MMSSeries,MMSSerialPwd,IsPayCard,IsPayCash,IsPayBink,IsPayCoupon,RegNullPwd,EmailAdress,EmailPwd,EmailSMTP,StockCount,UnitList,WeiXinSMSVcode,IsAutoSendSMSByMemRegister,IsAutoSendMMSByMemRegister,IsAutoSendSMSByMemRecharge,IsAutoSendSMSByMemWithdraw,IsAutoSendSMSByMemGiftExchange,IsAutoSendSMSByMemPointChange,IsAutoSendSMSByCommodityConsumption,IsAutoSendSMSByFastConsumption,IsAutoSendSMSByMemRedTimes,IsAutoSendSMSByTimingConsumption,SellerAccount,PartnerID,PartnerKey,IsEditPwdNeedOldPwd,WeiXinType,WeiXinVerified,WeiXinToken,WeiXinEncodingAESKey,WeiXinShopName,WeiXinSalutatory,WeiXinAppID,WeiXinAppSecret,SignInPoint,IsMemRegisterStaff,IsMustSlotCard,StorageTimingPrefix,IsAutoSendSMSByStorageTiming,EnterpriseEmailPort,EnterpriseEmailDisplayName,EnterpriseEmailEnableSSL,EnterpriseEmailUseDefaultCredentials,IsEmail,IsEmailNotice,MemCountExpensePrefix,IsAutoSendSMSByMemPast,AutoSendSMSByMemPastForDay,IsAutoSendSMSByMemBirthday,AutoSendSMSByMemBirthdayForDay,IsStartWeiXin,IsStartTimingProject,IsStartMemCount,MarketingSMS,MarketingSmsSeries,MarketingSmsSerialPwd,Senseiccard,Contacticcard,EmailUserName,PointNumStr,PrintPreview,PrintPaperType,IsSendCard,ShopSmsManage,ShopPointManage,ShopSettlement,AutoBackupDB,AutoBackupDay,SystemDomain,AlipayPrivateKey,AlipayPublicKey,alipayAppid)");
			strSql.Append(" values (");
            strSql.Append("@Pwd,@MoneyAndPoint,@AutoLevel,@DegradeLevel,@PastTime,@RecommendPoint,@PointPeriod,@ExpensePrefix,@GoodsExpensePrefix,@TimeExpensePrefix,@MemCountPrefix,@MemRechargePrefix,@GoodsInPrefix,@GoodsAllotPrefix,@MemDrawMoneyPrefix,@MemPointChangePrefix,@GiftExchangePrefix,@AutoPrint,@AccordPrint,@PrintTitle,@PrintFootNote,@Sms,@MoneySms,@IsSmsShopName,@SmsShopName,@SmsSeries,@SmsSerialPwd,@DrawMoneyPercent,@Tel,@TelNoMember,@IsStaff,@StaffType,@PointLevel,@MMS,@MMSSeries,@MMSSerialPwd,@IsPayCard,@IsPayCash,@IsPayBink,@IsPayCoupon,@RegNullPwd,@EmailAdress,@EmailPwd,@EmailSMTP,@StockCount,@UnitList,@WeiXinSMSVcode,@IsAutoSendSMSByMemRegister,@IsAutoSendMMSByMemRegister,@IsAutoSendSMSByMemRecharge,@IsAutoSendSMSByMemWithdraw,@IsAutoSendSMSByMemGiftExchange,@IsAutoSendSMSByMemPointChange,@IsAutoSendSMSByCommodityConsumption,@IsAutoSendSMSByFastConsumption,@IsAutoSendSMSByMemRedTimes,@IsAutoSendSMSByTimingConsumption,@SellerAccount,@PartnerID,@PartnerKey,@IsEditPwdNeedOldPwd,@WeiXinType,@WeiXinVerified,@WeiXinToken,@WeiXinEncodingAESKey,@WeiXinShopName,@WeiXinSalutatory,@WeiXinAppID,@WeiXinAppSecret,@SignInPoint,@IsMemRegisterStaff,@IsMustSlotCard,@StorageTimingPrefix,@IsAutoSendSMSByStorageTiming,@EnterpriseEmailPort,@EnterpriseEmailDisplayName,@EnterpriseEmailEnableSSL,@EnterpriseEmailUseDefaultCredentials,@IsEmail,@IsEmailNotice,@MemCountExpensePrefix,@IsAutoSendSMSByMemPast,@AutoSendSMSByMemPastForDay,@IsAutoSendSMSByMemBirthday,@AutoSendSMSByMemBirthdayForDay,@IsStartWeiXin,@IsStartTimingProject,@IsStartMemCount,@MarketingSMS,@MarketingSmsSeries,@MarketingSmsSerialPwd,@Senseiccard,@Contacticcard,@EmailUserName,@PointNumStr,@PrintPreview,@PrintPaperType,@IsSendCard,@ShopSmsManage,@ShopPointManage,@ShopSettlement,@AutoBackupDB,@AutoBackupDay,@SystemDomain,@AlipayPrivateKey,@AlipayPublicKey,@alipayAppid)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@Pwd", SqlDbType.Bit, 1),
				new SqlParameter("@MoneyAndPoint", SqlDbType.Bit, 1),
				new SqlParameter("@AutoLevel", SqlDbType.Bit, 1),
				new SqlParameter("@DegradeLevel", SqlDbType.Bit, 1),
				new SqlParameter("@PastTime", SqlDbType.Bit, 1),
				new SqlParameter("@RecommendPoint", SqlDbType.Int, 4),
				new SqlParameter("@PointPeriod", SqlDbType.Int, 4),
				new SqlParameter("@ExpensePrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@GoodsExpensePrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@TimeExpensePrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@MemCountPrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@MemRechargePrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@GoodsInPrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@GoodsAllotPrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@MemDrawMoneyPrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@MemPointChangePrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@GiftExchangePrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@AutoPrint", SqlDbType.Bit, 1),
				new SqlParameter("@AccordPrint", SqlDbType.Bit, 1),
				new SqlParameter("@PrintTitle", SqlDbType.NVarChar, 100),
				new SqlParameter("@PrintFootNote", SqlDbType.NVarChar, 100),
				new SqlParameter("@Sms", SqlDbType.Bit, 1),
				new SqlParameter("@MoneySms", SqlDbType.Bit, 1),
				new SqlParameter("@IsSmsShopName", SqlDbType.Bit, 1),
				new SqlParameter("@SmsShopName", SqlDbType.NVarChar, 100),
				new SqlParameter("@SmsSeries", SqlDbType.VarChar, 50),
				new SqlParameter("@SmsSerialPwd", SqlDbType.VarChar, 50),
				new SqlParameter("@DrawMoneyPercent", SqlDbType.Decimal, 9),
				new SqlParameter("@Tel", SqlDbType.Bit, 1),
				new SqlParameter("@TelNoMember", SqlDbType.Bit, 1),
				new SqlParameter("@IsStaff", SqlDbType.Bit, 1),
				new SqlParameter("@StaffType", SqlDbType.Bit, 1),
				new SqlParameter("@PointLevel", SqlDbType.Bit, 1),
				new SqlParameter("@MMS", SqlDbType.Bit, 1),
				new SqlParameter("@MMSSeries", SqlDbType.VarChar, 50),
				new SqlParameter("@MMSSerialPwd", SqlDbType.VarChar, 50),
				new SqlParameter("@IsPayCard", SqlDbType.Bit, 1),
				new SqlParameter("@IsPayCash", SqlDbType.Bit, 1),
				new SqlParameter("@IsPayBink", SqlDbType.Bit, 1),
				new SqlParameter("@IsPayCoupon", SqlDbType.Bit, 1),
				new SqlParameter("@RegNullPwd", SqlDbType.Bit, 1),
				new SqlParameter("@EmailAdress", SqlDbType.NVarChar, 100),
				new SqlParameter("@EmailPwd", SqlDbType.NVarChar, 100),
				new SqlParameter("@EmailSMTP", SqlDbType.NVarChar, 100),
				new SqlParameter("@StockCount", SqlDbType.Int, 4),
				new SqlParameter("@UnitList", SqlDbType.NVarChar, 200),
				new SqlParameter("@WeiXinSMSVcode", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemRegister", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendMMSByMemRegister", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemRecharge", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemWithdraw", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemGiftExchange", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemPointChange", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByCommodityConsumption", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByFastConsumption", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemRedTimes", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByTimingConsumption", SqlDbType.Bit, 1),
				new SqlParameter("@SellerAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@PartnerID", SqlDbType.VarChar, 50),
				new SqlParameter("@PartnerKey", SqlDbType.VarChar, 50),
				new SqlParameter("@IsEditPwdNeedOldPwd", SqlDbType.Bit, 1),
				new SqlParameter("@WeiXinType", SqlDbType.Bit, 1),
				new SqlParameter("@WeiXinVerified", SqlDbType.Bit, 1),
				new SqlParameter("@WeiXinToken", SqlDbType.NVarChar, 100),
				new SqlParameter("@WeiXinEncodingAESKey", SqlDbType.NVarChar, 100),
				new SqlParameter("@WeiXinShopName", SqlDbType.NVarChar, 100),
				new SqlParameter("@WeiXinSalutatory", SqlDbType.NVarChar, 1000),
				new SqlParameter("@WeiXinAppID", SqlDbType.NVarChar, 1000),
				new SqlParameter("@WeiXinAppSecret", SqlDbType.NVarChar, 1000),
				new SqlParameter("@SignInPoint", SqlDbType.Int, 4),
				new SqlParameter("@IsMemRegisterStaff", SqlDbType.Bit, 1),
				new SqlParameter("@IsMustSlotCard", SqlDbType.Bit, 1),
				new SqlParameter("@StorageTimingPrefix", SqlDbType.NVarChar, 50),
				new SqlParameter("@IsAutoSendSMSByStorageTiming", SqlDbType.Bit, 1),
				new SqlParameter("@EnterpriseEmailPort", SqlDbType.Int, 4),
				new SqlParameter("@EnterpriseEmailDisplayName", SqlDbType.NVarChar, 100),
				new SqlParameter("@EnterpriseEmailEnableSSL", SqlDbType.Bit, 1),
				new SqlParameter("@EnterpriseEmailUseDefaultCredentials", SqlDbType.Bit, 1),
				new SqlParameter("@IsEmail", SqlDbType.Bit, 1),
				new SqlParameter("@IsEmailNotice", SqlDbType.Bit, 1),
				new SqlParameter("@MemCountExpensePrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@IsAutoSendSMSByMemPast", SqlDbType.Bit, 1),
				new SqlParameter("@AutoSendSMSByMemPastForDay", SqlDbType.Int, 4),
				new SqlParameter("@IsAutoSendSMSByMemBirthday", SqlDbType.Bit, 1),
				new SqlParameter("@AutoSendSMSByMemBirthdayForDay", SqlDbType.Int, 4),
				new SqlParameter("@IsStartWeiXin", SqlDbType.Bit, 1),
				new SqlParameter("@IsStartTimingProject", SqlDbType.Bit, 1),
				new SqlParameter("@IsStartMemCount", SqlDbType.Bit, 1),
				new SqlParameter("@MarketingSMS", SqlDbType.Bit, 1),
				new SqlParameter("@MarketingSmsSeries", SqlDbType.VarChar, 50),
				new SqlParameter("@MarketingSmsSerialPwd", SqlDbType.VarChar, 50),
				new SqlParameter("@Senseiccard", SqlDbType.Bit, 1),
				new SqlParameter("@Contacticcard", SqlDbType.Bit, 1),
				new SqlParameter("@EmailUserName", SqlDbType.NVarChar, 50),
				new SqlParameter("@PointNumStr", SqlDbType.VarChar, 100),
				new SqlParameter("@PrintPreview", SqlDbType.Int, 4),
				new SqlParameter("@PrintPaperType", SqlDbType.Int, 4),
				new SqlParameter("@IsSendCard", SqlDbType.Bit, 1),
				new SqlParameter("@ShopSmsManage", SqlDbType.Bit, 1),
				new SqlParameter("@ShopPointManage", SqlDbType.Bit, 1),
				new SqlParameter("@ShopSettlement", SqlDbType.Bit, 1),
				new SqlParameter("@AutoBackupDB", SqlDbType.Bit, 1),
				new SqlParameter("@AutoBackupDay", SqlDbType.Int, 4),
				new SqlParameter("@SystemDomain", SqlDbType.VarChar, 100),
				new SqlParameter("@AlipayPrivateKey", SqlDbType.VarChar, 2000),
				new SqlParameter("@AlipayPublicKey", SqlDbType.VarChar, 2000),
				new SqlParameter("@alipayAppid", SqlDbType.VarChar, 20)
			};
			parameters[0].Value = model.Pwd;
			parameters[1].Value = model.MoneyAndPoint;
			parameters[2].Value = model.AutoLevel;
			parameters[3].Value = model.DegradeLevel;
			parameters[4].Value = model.PastTime;
			parameters[5].Value = model.RecommendPoint;
			parameters[6].Value = model.PointPeriod;
			parameters[7].Value = model.ExpensePrefix;
			parameters[8].Value = model.GoodsExpensePrefix;
			parameters[9].Value = model.TimeExpensePrefix;
			parameters[10].Value = model.MemCountPrefix;
			parameters[11].Value = model.MemRechargePrefix;
			parameters[12].Value = model.GoodsInPrefix;
			parameters[13].Value = model.GoodsAllotPrefix;
			parameters[14].Value = model.MemDrawMoneyPrefix;
			parameters[15].Value = model.MemPointChangePrefix;
			parameters[16].Value = model.GiftExchangePrefix;
			parameters[17].Value = model.AutoPrint;
			parameters[18].Value = model.AccordPrint;
			parameters[19].Value = model.PrintTitle;
			parameters[20].Value = model.PrintFootNote;
			parameters[21].Value = model.Sms;
			parameters[22].Value = model.MoneySms;
			parameters[23].Value = model.IsSmsShopName;
			parameters[24].Value = model.SmsShopName;
			parameters[25].Value = model.SmsSeries;
			parameters[26].Value = model.SmsSerialPwd;
			parameters[27].Value = model.DrawMoneyPercent;
			parameters[28].Value = model.Tel;
			parameters[29].Value = model.TelNoMember;
			parameters[30].Value = model.IsStaff;
			parameters[31].Value = model.StaffType;
			parameters[32].Value = model.PointLevel;
			parameters[33].Value = model.MMS;
			parameters[34].Value = model.MMSSeries;
			parameters[35].Value = model.MMSSerialPwd;
			parameters[36].Value = model.IsPayCard;
			parameters[37].Value = model.IsPayCash;
			parameters[38].Value = model.IsPayBink;
			parameters[39].Value = model.IsPayCoupon;
			parameters[40].Value = model.RegNullPwd;
			parameters[41].Value = model.EmailAdress;
			parameters[42].Value = model.EmailPwd;
			parameters[43].Value = model.EmailSMTP;
			parameters[44].Value = model.StockCount;
			parameters[45].Value = model.UnitList;
			parameters[46].Value = model.WeiXinSMSVcode;
			parameters[47].Value = model.IsAutoSendSMSByMemRegister;
			parameters[48].Value = model.IsAutoSendMMSByMemRegister;
			parameters[49].Value = model.IsAutoSendSMSByMemRecharge;
			parameters[50].Value = model.IsAutoSendSMSByMemWithdraw;
			parameters[51].Value = model.IsAutoSendSMSByMemGiftExchange;
			parameters[52].Value = model.IsAutoSendSMSByMemPointChange;
			parameters[53].Value = model.IsAutoSendSMSByCommodityConsumption;
			parameters[54].Value = model.IsAutoSendSMSByFastConsumption;
			parameters[55].Value = model.IsAutoSendSMSByMemRedTimes;
			parameters[56].Value = model.IsAutoSendSMSByTimingConsumption;
			parameters[57].Value = model.SellerAccount;
			parameters[58].Value = model.PartnerID;
			parameters[59].Value = model.PartnerKey;
			parameters[60].Value = model.IsEditPwdNeedOldPwd;
			parameters[61].Value = model.WeiXinType;
			parameters[62].Value = model.WeiXinVerified;
			parameters[63].Value = model.WeiXinToken;
			parameters[64].Value = model.WeiXinEncodingAESKey;
			parameters[65].Value = model.WeiXinShopName;
			parameters[66].Value = model.WeiXinSalutatory;
			parameters[67].Value = model.WeiXinAppID;
			parameters[68].Value = model.WeiXinAppSecret;
			parameters[69].Value = model.SignInPoint;
			parameters[70].Value = model.IsMemRegisterStaff;
			parameters[71].Value = model.IsMustSlotCard;
			parameters[72].Value = model.StorageTimingPrefix;
			parameters[73].Value = model.IsAutoSendSMSByStorageTiming;
			parameters[74].Value = model.EnterpriseEmailPort;
			parameters[75].Value = model.EnterpriseEmailDisplayName;
			parameters[76].Value = model.EnterpriseEmailEnableSSL;
			parameters[77].Value = model.EnterpriseEmailUseDefaultCredentials;
			parameters[78].Value = model.IsEmail;
			parameters[79].Value = model.IsEmailNotice;
			parameters[80].Value = model.MemCountExpensePrefix;
			parameters[81].Value = model.IsAutoSendSMSByMemPast;
			parameters[82].Value = model.AutoSendSMSByMemPastForDay;
			parameters[83].Value = model.IsAutoSendSMSByMemBirthday;
			parameters[84].Value = model.AutoSendSMSByMemBirthdayForDay;
			parameters[85].Value = model.IsStartWeiXin;
			parameters[86].Value = model.IsStartTimingProject;
			parameters[87].Value = model.IsStartMemCount;
			parameters[88].Value = model.MarketingSMS;
			parameters[89].Value = model.MarketingSmsSeries;
			parameters[90].Value = model.MarketingSmsSerialPwd;
			parameters[91].Value = model.Senseiccard;
			parameters[92].Value = model.Contacticcard;
			parameters[93].Value = model.EmailUserName;
			parameters[94].Value = model.PointNumStr;
			parameters[95].Value = model.PrintPreview;
			parameters[96].Value = model.PrintPaperType;
			parameters[97].Value = model.IsSendCard;
			parameters[98].Value = model.ShopSmsManage;
			parameters[99].Value = model.ShopPointManage;
			parameters[100].Value = model.ShopSettlement;
			parameters[101].Value = model.AutoBackupDB;
			parameters[102].Value = model.AutoBackupDay;
			parameters[103].Value = model.SystemDomain;
            parameters[104].Value = model.AlipayPrivateKey;
            parameters[105].Value = model.AlipayPublicKey;
            parameters[106].Value = model.AlipayAppid;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
			int result;
			if (obj == null)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(obj);
			}
			return result;
		}

		public bool Update(Chain.Model.SysParameter model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysParameter set ");
			strSql.Append("GiveMemMoneyRate=@GiveMemMoneyRate,");
			strSql.Append("IsPayPoint=@IsPayPoint,");
			strSql.Append("IsPayWeiXin=@IsPayWeiXin,");
			strSql.Append("PointDiscountPercent=@PointDiscountPercent,");
			strSql.Append("PointUsePercent=@PointUsePercent,");
			strSql.Append("Pwd=@Pwd,");
			strSql.Append("MoneyAndPoint=@MoneyAndPoint,");
			strSql.Append("AutoLevel=@AutoLevel,");
			strSql.Append("DegradeLevel=@DegradeLevel,");
			strSql.Append("PastTime=@PastTime,");
			strSql.Append("RecommendPoint=@RecommendPoint,");
			strSql.Append("PointPeriod=@PointPeriod,");
			strSql.Append("ExpensePrefix=@ExpensePrefix,");
			strSql.Append("GoodsExpensePrefix=@GoodsExpensePrefix,");
			strSql.Append("TimeExpensePrefix=@TimeExpensePrefix,");
			strSql.Append("MemCountPrefix=@MemCountPrefix,");
			strSql.Append("MemRechargePrefix=@MemRechargePrefix,");
			strSql.Append("GoodsInPrefix=@GoodsInPrefix,");
			strSql.Append("GoodsAllotPrefix=@GoodsAllotPrefix,");
			strSql.Append("MemDrawMoneyPrefix=@MemDrawMoneyPrefix,");
			strSql.Append("MemPointChangePrefix=@MemPointChangePrefix,");
			strSql.Append("GiftExchangePrefix=@GiftExchangePrefix,");
			strSql.Append("AutoPrint=@AutoPrint,");
			strSql.Append("AccordPrint=@AccordPrint,");
			strSql.Append("PrintTitle=@PrintTitle,");
			strSql.Append("PrintFootNote=@PrintFootNote,");
			strSql.Append("Sms=@Sms,");
			strSql.Append("MoneySms=@MoneySms,");
			strSql.Append("IsSmsShopName=@IsSmsShopName,");
			strSql.Append("SmsShopName=@SmsShopName,");
			strSql.Append("SmsSeries=@SmsSeries,");
			strSql.Append("SmsSerialPwd=@SmsSerialPwd,");
			strSql.Append("DrawMoneyPercent=@DrawMoneyPercent,");
			strSql.Append("Tel=@Tel,");
			strSql.Append("TelNoMember=@TelNoMember,");
			strSql.Append("IsStaff=@IsStaff,");
			strSql.Append("StaffType=@StaffType,");
			strSql.Append("PointLevel=@PointLevel,");
			strSql.Append("MMS=@MMS,");
			strSql.Append("MMSSeries=@MMSSeries,");
			strSql.Append("MMSSerialPwd=@MMSSerialPwd,");
			strSql.Append("IsPayCard=@IsPayCard,");
			strSql.Append("IsPayCash=@IsPayCash,");
			strSql.Append("IsPayBink=@IsPayBink,");
			strSql.Append("IsPayCoupon=@IsPayCoupon,");
			strSql.Append("RegNullPwd=@RegNullPwd,");
			strSql.Append("EmailAdress=@EmailAdress,");
			strSql.Append("EmailPwd=@EmailPwd,");
			strSql.Append("EmailSMTP=@EmailSMTP,");
			strSql.Append("StockCount=@StockCount,");
			strSql.Append("UnitList=@UnitList,");
			strSql.Append("WeiXinSMSVcode=@WeiXinSMSVcode,");
			strSql.Append("IsAutoSendSMSByMemRegister=@IsAutoSendSMSByMemRegister,");
			strSql.Append("IsAutoSendMMSByMemRegister=@IsAutoSendMMSByMemRegister,");
			strSql.Append("IsAutoSendSMSByMemRecharge=@IsAutoSendSMSByMemRecharge,");
			strSql.Append("IsAutoSendSMSByMemWithdraw=@IsAutoSendSMSByMemWithdraw,");
			strSql.Append("IsAutoSendSMSByMemGiftExchange=@IsAutoSendSMSByMemGiftExchange,");
			strSql.Append("IsAutoSendSMSByMemPointChange=@IsAutoSendSMSByMemPointChange,");
			strSql.Append("IsAutoSendSMSByCommodityConsumption=@IsAutoSendSMSByCommodityConsumption,");
			strSql.Append("IsAutoSendSMSByFastConsumption=@IsAutoSendSMSByFastConsumption,");
			strSql.Append("IsAutoSendSMSByMemRedTimes=@IsAutoSendSMSByMemRedTimes,");
			strSql.Append("IsAutoSendSMSByTimingConsumption=@IsAutoSendSMSByTimingConsumption,");
			strSql.Append("SellerAccount=@SellerAccount,");
			strSql.Append("PartnerID=@PartnerID,");
			strSql.Append("PartnerKey=@PartnerKey,");
			strSql.Append("IsEditPwdNeedOldPwd=@IsEditPwdNeedOldPwd,");
			strSql.Append("WeiXinType=@WeiXinType,");
			strSql.Append("WeiXinVerified=@WeiXinVerified,");
			strSql.Append("WeiXinToken=@WeiXinToken,");
			strSql.Append("WeiXinEncodingAESKey=@WeiXinEncodingAESKey,");
			strSql.Append("WeiXinShopName=@WeiXinShopName,");
			strSql.Append("WeiXinSalutatory=@WeiXinSalutatory,");
			strSql.Append("WeiXinAppID=@WeiXinAppID,");
			strSql.Append("WeiXinAppSecret=@WeiXinAppSecret,");
			strSql.Append("SignInPoint=@SignInPoint,");
			strSql.Append("IsMemRegisterStaff=@IsMemRegisterStaff,");
			strSql.Append("IsMustSlotCard=@IsMustSlotCard,");
			strSql.Append("StorageTimingPrefix=@StorageTimingPrefix,");
			strSql.Append("IsAutoSendSMSByStorageTiming=@IsAutoSendSMSByStorageTiming,");
			strSql.Append("EnterpriseEmailPort=@EnterpriseEmailPort,");
			strSql.Append("EnterpriseEmailDisplayName=@EnterpriseEmailDisplayName,");
			strSql.Append("EnterpriseEmailEnableSSL=@EnterpriseEmailEnableSSL,");
			strSql.Append("EnterpriseEmailUseDefaultCredentials=@EnterpriseEmailUseDefaultCredentials,");
			strSql.Append("IsEmail=@IsEmail,");
			strSql.Append("IsEmailNotice=@IsEmailNotice,");
			strSql.Append("MemCountExpensePrefix=@MemCountExpensePrefix,");
			strSql.Append("IsAutoSendSMSByMemPast=@IsAutoSendSMSByMemPast,");
			strSql.Append("AutoSendSMSByMemPastForDay=@AutoSendSMSByMemPastForDay,");
			strSql.Append("IsAutoSendSMSByMemBirthday=@IsAutoSendSMSByMemBirthday,");
			strSql.Append("AutoSendSMSByMemBirthdayForDay=@AutoSendSMSByMemBirthdayForDay,");
			strSql.Append("IsStartWeiXin=@IsStartWeiXin,");
			strSql.Append("IsStartTimingProject=@IsStartTimingProject,");
			strSql.Append("IsStartMemCount=@IsStartMemCount,");
			strSql.Append("MarketingSMS=@MarketingSMS,");
			strSql.Append("MarketingSmsSeries=@MarketingSmsSeries,");
			strSql.Append("MarketingSmsSerialPwd=@MarketingSmsSerialPwd,");
			strSql.Append("Senseiccard=@Senseiccard,");
			strSql.Append("Contacticcard=@Contacticcard,");
			strSql.Append("EmailUserName=@EmailUserName,");
			strSql.Append("PointNumStr=@PointNumStr,");
			strSql.Append("PrintPreview=@PrintPreview,");
			strSql.Append("PrintPaperType=@PrintPaperType,");
			strSql.Append("IsSendCard=@IsSendCard,");
			strSql.Append("ShopSmsManage=@ShopSmsManage,");
			strSql.Append("ShopPointManage=@ShopPointManage,");
			strSql.Append("ShopSettlement=@ShopSettlement,");
			strSql.Append("AutoBackupDB=@AutoBackupDB,");
			strSql.Append("AutoBackupDay=@AutoBackupDay,");
			strSql.Append("SystemDomain=@SystemDomain,");
			strSql.Append("PointDrawPercent=@PointDrawPercent,");
			strSql.Append("AllianceRebatePercent=@AllianceRebatePercent,");
			strSql.Append("CardShopRebatePercent=@CardShopRebatePercent,");
			strSql.Append("MchId=@MchId,");
			strSql.Append("Api=@Api,");
			strSql.Append("Pay=@Pay,");
			strSql.Append("Xiane=@Xiane,");
            strSql.Append("AlipayPrivateKey=@AlipayPrivateKey,");
            strSql.Append("AlipayPublicKey=@AlipayPublicKey,");
            strSql.Append("VIPDescribe=@VIPDescribe,");
            strSql.Append("alipayAppid=@alipayAppid");
            strSql.Append(" where ParameterID=@ParameterID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@Pwd", SqlDbType.Bit, 1),
				new SqlParameter("@MoneyAndPoint", SqlDbType.Bit, 1),
				new SqlParameter("@AutoLevel", SqlDbType.Bit, 1),
				new SqlParameter("@DegradeLevel", SqlDbType.Bit, 1),
				new SqlParameter("@PastTime", SqlDbType.Bit, 1),
				new SqlParameter("@RecommendPoint", SqlDbType.Int, 4),
				new SqlParameter("@PointPeriod", SqlDbType.Int, 4),
				new SqlParameter("@ExpensePrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@GoodsExpensePrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@TimeExpensePrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@MemCountPrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@MemRechargePrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@GoodsInPrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@GoodsAllotPrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@MemDrawMoneyPrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@MemPointChangePrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@GiftExchangePrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@AutoPrint", SqlDbType.Bit, 1),
				new SqlParameter("@AccordPrint", SqlDbType.Bit, 1),
				new SqlParameter("@PrintTitle", SqlDbType.NVarChar, 100),
				new SqlParameter("@PrintFootNote", SqlDbType.NVarChar, 100),
				new SqlParameter("@Sms", SqlDbType.Bit, 1),
				new SqlParameter("@MoneySms", SqlDbType.Bit, 1),
				new SqlParameter("@IsSmsShopName", SqlDbType.Bit, 1),
				new SqlParameter("@SmsShopName", SqlDbType.NVarChar, 100),
				new SqlParameter("@SmsSeries", SqlDbType.VarChar, 50),
				new SqlParameter("@SmsSerialPwd", SqlDbType.VarChar, 50),
				new SqlParameter("@DrawMoneyPercent", SqlDbType.Decimal, 9),
				new SqlParameter("@Tel", SqlDbType.Bit, 1),
				new SqlParameter("@TelNoMember", SqlDbType.Bit, 1),
				new SqlParameter("@IsStaff", SqlDbType.Bit, 1),
				new SqlParameter("@StaffType", SqlDbType.Bit, 1),
				new SqlParameter("@PointLevel", SqlDbType.Bit, 1),
				new SqlParameter("@MMS", SqlDbType.Bit, 1),
				new SqlParameter("@MMSSeries", SqlDbType.VarChar, 50),
				new SqlParameter("@MMSSerialPwd", SqlDbType.VarChar, 50),
				new SqlParameter("@IsPayCard", SqlDbType.Bit, 1),
				new SqlParameter("@IsPayCash", SqlDbType.Bit, 1),
				new SqlParameter("@IsPayBink", SqlDbType.Bit, 1),
				new SqlParameter("@IsPayCoupon", SqlDbType.Bit, 1),
				new SqlParameter("@RegNullPwd", SqlDbType.Bit, 1),
				new SqlParameter("@EmailAdress", SqlDbType.NVarChar, 100),
				new SqlParameter("@EmailPwd", SqlDbType.NVarChar, 100),
				new SqlParameter("@EmailSMTP", SqlDbType.NVarChar, 100),
				new SqlParameter("@StockCount", SqlDbType.Int, 4),
				new SqlParameter("@UnitList", SqlDbType.NVarChar, 200),
				new SqlParameter("@WeiXinSMSVcode", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemRegister", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendMMSByMemRegister", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemRecharge", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemWithdraw", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemGiftExchange", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemPointChange", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByCommodityConsumption", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByFastConsumption", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemRedTimes", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByTimingConsumption", SqlDbType.Bit, 1),
				new SqlParameter("@SellerAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@PartnerID", SqlDbType.VarChar, 50),
				new SqlParameter("@PartnerKey", SqlDbType.VarChar, 50),
				new SqlParameter("@IsEditPwdNeedOldPwd", SqlDbType.Bit, 1),
				new SqlParameter("@WeiXinType", SqlDbType.Bit, 1),
				new SqlParameter("@WeiXinVerified", SqlDbType.Bit, 1),
				new SqlParameter("@WeiXinToken", SqlDbType.NVarChar, 100),
				new SqlParameter("@WeiXinEncodingAESKey", SqlDbType.NVarChar, 100),
				new SqlParameter("@WeiXinShopName", SqlDbType.NVarChar, 100),
				new SqlParameter("@WeiXinSalutatory", SqlDbType.NVarChar, 1000),
				new SqlParameter("@WeiXinAppID", SqlDbType.NVarChar, 1000),
				new SqlParameter("@WeiXinAppSecret", SqlDbType.NVarChar, 1000),
				new SqlParameter("@SignInPoint", SqlDbType.Int, 4),
				new SqlParameter("@IsMemRegisterStaff", SqlDbType.Bit, 1),
				new SqlParameter("@IsMustSlotCard", SqlDbType.Bit, 1),
				new SqlParameter("@StorageTimingPrefix", SqlDbType.NVarChar, 50),
				new SqlParameter("@IsAutoSendSMSByStorageTiming", SqlDbType.Bit, 1),
				new SqlParameter("@EnterpriseEmailPort", SqlDbType.Int, 4),
				new SqlParameter("@EnterpriseEmailDisplayName", SqlDbType.NVarChar, 100),
				new SqlParameter("@EnterpriseEmailEnableSSL", SqlDbType.Bit, 1),
				new SqlParameter("@EnterpriseEmailUseDefaultCredentials", SqlDbType.Bit, 1),
				new SqlParameter("@IsEmail", SqlDbType.Bit, 1),
				new SqlParameter("@IsEmailNotice", SqlDbType.Bit, 1),
				new SqlParameter("@MemCountExpensePrefix", SqlDbType.VarChar, 50),
				new SqlParameter("@IsAutoSendSMSByMemPast", SqlDbType.Bit, 1),
				new SqlParameter("@AutoSendSMSByMemPastForDay", SqlDbType.Int, 4),
				new SqlParameter("@IsAutoSendSMSByMemBirthday", SqlDbType.Bit, 1),
				new SqlParameter("@AutoSendSMSByMemBirthdayForDay", SqlDbType.Int, 4),
				new SqlParameter("@IsStartWeiXin", SqlDbType.Bit, 1),
				new SqlParameter("@IsStartTimingProject", SqlDbType.Bit, 1),
				new SqlParameter("@IsStartMemCount", SqlDbType.Bit, 1),
				new SqlParameter("@MarketingSMS", SqlDbType.Bit, 1),
				new SqlParameter("@MarketingSmsSeries", SqlDbType.VarChar, 50),
				new SqlParameter("@MarketingSmsSerialPwd", SqlDbType.VarChar, 50),
				new SqlParameter("@Senseiccard", SqlDbType.Bit, 1),
				new SqlParameter("@Contacticcard", SqlDbType.Bit, 1),
				new SqlParameter("@EmailUserName", SqlDbType.NVarChar, 50),
				new SqlParameter("@PointNumStr", SqlDbType.VarChar, 100),
				new SqlParameter("@PrintPreview", SqlDbType.Int, 4),
				new SqlParameter("@PrintPaperType", SqlDbType.Int, 4),
				new SqlParameter("@IsSendCard", SqlDbType.Bit, 1),
				new SqlParameter("@ShopSmsManage", SqlDbType.Bit, 1),
				new SqlParameter("@ShopPointManage", SqlDbType.Bit, 1),
				new SqlParameter("@ShopSettlement", SqlDbType.Bit, 1),
				new SqlParameter("@AutoBackupDB", SqlDbType.Bit, 1),
				new SqlParameter("@AutoBackupDay", SqlDbType.Int, 4),
				new SqlParameter("@SystemDomain", SqlDbType.VarChar, 100),
				new SqlParameter("@PointDrawPercent", SqlDbType.Decimal),
				new SqlParameter("@AllianceRebatePercent", SqlDbType.Decimal),
				new SqlParameter("@CardShopRebatePercent", SqlDbType.Decimal),
				new SqlParameter("@MchId", SqlDbType.NVarChar, 100),
				new SqlParameter("@Api", SqlDbType.NVarChar, 50),
				new SqlParameter("@Pay", SqlDbType.Int),
				new SqlParameter("@Xiane", SqlDbType.Money),
				new SqlParameter("@ParameterID", SqlDbType.Int, 4),
				new SqlParameter("@VIPDescribe", SqlDbType.NVarChar),
				new SqlParameter("@PointDiscountPercent", SqlDbType.Decimal),
				new SqlParameter("@PointUsePercent", SqlDbType.Decimal),
				new SqlParameter("@IsPayWeiXin", SqlDbType.Bit, 1),
				new SqlParameter("@IsPayPoint", SqlDbType.Bit, 1),
				new SqlParameter("@AlipayPrivateKey", SqlDbType.VarChar,2000),
				new SqlParameter("@AlipayPublicKey", SqlDbType.VarChar,2000),
				new SqlParameter("@GiveMemMoneyRate", SqlDbType.Decimal),
				new SqlParameter("@alipayAppid", SqlDbType.VarChar,20)
			};
			parameters[0].Value = model.Pwd;
			parameters[1].Value = model.MoneyAndPoint;
			parameters[2].Value = model.AutoLevel;
			parameters[3].Value = model.DegradeLevel;
			parameters[4].Value = model.PastTime;
			parameters[5].Value = model.RecommendPoint;
			parameters[6].Value = model.PointPeriod;
			parameters[7].Value = model.ExpensePrefix;
			parameters[8].Value = model.GoodsExpensePrefix;
			parameters[9].Value = model.TimeExpensePrefix;
			parameters[10].Value = model.MemCountPrefix;
			parameters[11].Value = model.MemRechargePrefix;
			parameters[12].Value = model.GoodsInPrefix;
			parameters[13].Value = model.GoodsAllotPrefix;
			parameters[14].Value = model.MemDrawMoneyPrefix;
			parameters[15].Value = model.MemPointChangePrefix;
			parameters[16].Value = model.GiftExchangePrefix;
			parameters[17].Value = model.AutoPrint;
			parameters[18].Value = model.AccordPrint;
			parameters[19].Value = model.PrintTitle;
			parameters[20].Value = model.PrintFootNote;
			parameters[21].Value = model.Sms;
			parameters[22].Value = model.MoneySms;
			parameters[23].Value = model.IsSmsShopName;
			parameters[24].Value = model.SmsShopName;
			parameters[25].Value = model.SmsSeries;
			parameters[26].Value = model.SmsSerialPwd;
			parameters[27].Value = model.DrawMoneyPercent;
			parameters[28].Value = model.Tel;
			parameters[29].Value = model.TelNoMember;
			parameters[30].Value = model.IsStaff;
			parameters[31].Value = model.StaffType;
			parameters[32].Value = model.PointLevel;
			parameters[33].Value = model.MMS;
			parameters[34].Value = model.MMSSeries;
			parameters[35].Value = model.MMSSerialPwd;
			parameters[36].Value = model.IsPayCard;
			parameters[37].Value = model.IsPayCash;
			parameters[38].Value = model.IsPayBink;
			parameters[39].Value = model.IsPayCoupon;
			parameters[40].Value = model.RegNullPwd;
			parameters[41].Value = model.EmailAdress;
			parameters[42].Value = model.EmailPwd;
			parameters[43].Value = model.EmailSMTP;
			parameters[44].Value = model.StockCount;
			parameters[45].Value = model.UnitList;
			parameters[46].Value = model.WeiXinSMSVcode;
			parameters[47].Value = model.IsAutoSendSMSByMemRegister;
			parameters[48].Value = model.IsAutoSendMMSByMemRegister;
			parameters[49].Value = model.IsAutoSendSMSByMemRecharge;
			parameters[50].Value = model.IsAutoSendSMSByMemWithdraw;
			parameters[51].Value = model.IsAutoSendSMSByMemGiftExchange;
			parameters[52].Value = model.IsAutoSendSMSByMemPointChange;
			parameters[53].Value = model.IsAutoSendSMSByCommodityConsumption;
			parameters[54].Value = model.IsAutoSendSMSByFastConsumption;
			parameters[55].Value = model.IsAutoSendSMSByMemRedTimes;
			parameters[56].Value = model.IsAutoSendSMSByTimingConsumption;
			parameters[57].Value = model.SellerAccount;
			parameters[58].Value = model.PartnerID;
			parameters[59].Value = model.PartnerKey;
			parameters[60].Value = model.IsEditPwdNeedOldPwd;
			parameters[61].Value = model.WeiXinType;
			parameters[62].Value = model.WeiXinVerified;
			parameters[63].Value = model.WeiXinToken;
			parameters[64].Value = model.WeiXinEncodingAESKey;
			parameters[65].Value = model.WeiXinShopName;
			parameters[66].Value = model.WeiXinSalutatory;
			parameters[67].Value = model.WeiXinAppID;
			parameters[68].Value = model.WeiXinAppSecret;
			parameters[69].Value = model.SignInPoint;
			parameters[70].Value = model.IsMemRegisterStaff;
			parameters[71].Value = model.IsMustSlotCard;
			parameters[72].Value = model.StorageTimingPrefix;
			parameters[73].Value = model.IsAutoSendSMSByStorageTiming;
			parameters[74].Value = model.EnterpriseEmailPort;
			parameters[75].Value = model.EnterpriseEmailDisplayName;
			parameters[76].Value = model.EnterpriseEmailEnableSSL;
			parameters[77].Value = model.EnterpriseEmailUseDefaultCredentials;
			parameters[78].Value = model.IsEmail;
			parameters[79].Value = model.IsEmailNotice;
			parameters[80].Value = model.MemCountExpensePrefix;
			parameters[81].Value = model.IsAutoSendSMSByMemPast;
			parameters[82].Value = model.AutoSendSMSByMemPastForDay;
			parameters[83].Value = model.IsAutoSendSMSByMemBirthday;
			parameters[84].Value = model.AutoSendSMSByMemBirthdayForDay;
			parameters[85].Value = model.IsStartWeiXin;
			parameters[86].Value = model.IsStartTimingProject;
			parameters[87].Value = model.IsStartMemCount;
			parameters[88].Value = model.MarketingSMS;
			parameters[89].Value = model.MarketingSmsSeries;
			parameters[90].Value = model.MarketingSmsSerialPwd;
			parameters[91].Value = model.Senseiccard;
			parameters[92].Value = model.Contacticcard;
			parameters[93].Value = model.EmailUserName;
			parameters[94].Value = model.PointNumStr;
			parameters[95].Value = model.PrintPreview;
			parameters[96].Value = model.PrintPaperType;
			parameters[97].Value = model.IsSendCard;
			parameters[98].Value = model.ShopSmsManage;
			parameters[99].Value = model.ShopPointManage;
			parameters[100].Value = model.ShopSettlement;
			parameters[101].Value = model.AutoBackupDB;
			parameters[102].Value = model.AutoBackupDay;
			parameters[103].Value = model.SystemDomain;
			parameters[104].Value = model.PointDrawPercent;
			parameters[105].Value = model.AllianceRebatePercent;
			parameters[106].Value = model.CardShopRebatePercent;
			parameters[107].Value = model.MchId;
			parameters[108].Value = model.Api;
			parameters[109].Value = model.Pay;
			parameters[110].Value = model.Xiane;
			parameters[111].Value = model.ParameterID;
			parameters[112].Value = model.VIPDescribe;
			parameters[113].Value = model.PointDiscountPercent;
			parameters[114].Value = model.PointUsePercent;
			parameters[115].Value = model.IsPayWeiXin;
			parameters[116].Value = model.IsPayPoint;
            parameters[117].Value = model.AlipayPrivateKey;
            parameters[118].Value = model.AlipayPublicKey;
            parameters[119].Value = model.GiveMemMoneyRate;
            parameters[120].Value = model.AlipayAppid;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool UpdateVIPDescript(string vipDescript, int parameterID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysParameter set ");
			strSql.Append("VIPDescribe=@VIPDescribe");
			strSql.Append(" where ParameterID=@ParameterID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ParameterID", SqlDbType.Int, 4),
				new SqlParameter("@VIPDescribe", SqlDbType.NVarChar)
			};
			parameters[0].Value = parameterID;
			parameters[1].Value = vipDescript;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ParameterID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysParameter ");
			strSql.Append(" where ParameterID=@ParameterID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ParameterID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ParameterID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string ParameterIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysParameter ");
			strSql.Append(" where ParameterID in (" + ParameterIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysParameter GetModel(int ParameterID)
		{
			StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 GiveMemMoneyRate,IsPayPoint,IsPayWeiXin,PointDiscountPercent,PointUsePercent,ParameterID,Pwd,MoneyAndPoint,AutoLevel,DegradeLevel,PastTime,RecommendPoint,PointPeriod,ExpensePrefix,GoodsExpensePrefix,TimeExpensePrefix,MemCountPrefix,MemRechargePrefix,GoodsInPrefix,GoodsAllotPrefix,MemDrawMoneyPrefix,MemPointChangePrefix,GiftExchangePrefix,AutoPrint,AccordPrint,PrintTitle,PrintFootNote,Sms,MoneySms,IsSmsShopName,SmsShopName,SmsSeries,SmsSerialPwd,DrawMoneyPercent,Tel,TelNoMember,IsStaff,StaffType,PointLevel,MMS,MMSSeries,MMSSerialPwd,IsPayCard,IsPayCash,IsPayBink,IsPayCoupon,RegNullPwd,EmailAdress,EmailPwd,EmailSMTP,StockCount,UnitList,WeiXinSMSVcode,IsAutoSendSMSByMemRegister,IsAutoSendMMSByMemRegister,IsAutoSendSMSByMemRecharge,IsAutoSendSMSByMemWithdraw,IsAutoSendSMSByMemGiftExchange,IsAutoSendSMSByMemPointChange,IsAutoSendSMSByCommodityConsumption,IsAutoSendSMSByFastConsumption,IsAutoSendSMSByMemRedTimes,IsAutoSendSMSByTimingConsumption,SellerAccount,PartnerID,PartnerKey,IsEditPwdNeedOldPwd,WeiXinType,WeiXinVerified,WeiXinToken,WeiXinEncodingAESKey,WeiXinShopName,WeiXinSalutatory,WeiXinAppID,WeiXinAppSecret,SignInPoint,IsMemRegisterStaff,IsMustSlotCard,StorageTimingPrefix,IsAutoSendSMSByStorageTiming,EnterpriseEmailPort,EnterpriseEmailDisplayName,EnterpriseEmailEnableSSL,EnterpriseEmailUseDefaultCredentials,IsEmail,IsEmailNotice,MemCountExpensePrefix,IsAutoSendSMSByMemPast,AutoSendSMSByMemPastForDay,IsAutoSendSMSByMemBirthday,AutoSendSMSByMemBirthdayForDay,IsStartWeiXin,IsStartTimingProject,IsStartMemCount,MarketingSMS,MarketingSmsSeries,MarketingSmsSerialPwd,Senseiccard,Contacticcard,EmailUserName,PointNumStr,PrintPreview,PrintPaperType,IsSendCard,ShopSmsManage,ShopPointManage,ShopSettlement,AutoBackupDB,AutoBackupDay,SystemDomain,PointDrawPercent,AllianceRebatePercent,CardShopRebatePercent,PointUsePercent,MchId,Api,Pay,Xiane,VIPDescribe,PointDiscountPercent,IsPayWeiXin,IsPayPoint,AlipayPublicKey,AlipayPrivateKey,alipayAppid from SysParameter ");
			strSql.Append(" where ParameterID=@ParameterID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ParameterID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ParameterID;
			Chain.Model.SysParameter model = new Chain.Model.SysParameter();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysParameter result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["GiveMemMoneyRate"] != null && ds.Tables[0].Rows[0]["GiveMemMoneyRate"].ToString() != "")
				{
					model.GiveMemMoneyRate = decimal.Parse(ds.Tables[0].Rows[0]["GiveMemMoneyRate"].ToString());
				}
				if (ds.Tables[0].Rows[0]["IsPayPoint"] != null && ds.Tables[0].Rows[0]["IsPayPoint"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsPayPoint"].ToString() == "1" || ds.Tables[0].Rows[0]["IsPayPoint"].ToString().ToLower() == "IsPayPoint")
					{
						model.IsPayPoint = true;
					}
					else
					{
						model.IsPayPoint = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsPayWeiXin"] != null && ds.Tables[0].Rows[0]["IsPayWeiXin"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsPayWeiXin"].ToString() == "1" || ds.Tables[0].Rows[0]["Pwd"].ToString().ToLower() == "IsPayWeiXin")
					{
						model.IsPayWeiXin = true;
					}
					else
					{
						model.IsPayWeiXin = false;
					}
				}
				if (ds.Tables[0].Rows[0]["CardShopRebatePercent"] != null && ds.Tables[0].Rows[0]["CardShopRebatePercent"].ToString() != "")
				{
					model.CardShopRebatePercent = decimal.Parse(ds.Tables[0].Rows[0]["CardShopRebatePercent"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AllianceRebatePercent"] != null && ds.Tables[0].Rows[0]["AllianceRebatePercent"].ToString() != "")
				{
					model.AllianceRebatePercent = decimal.Parse(ds.Tables[0].Rows[0]["AllianceRebatePercent"].ToString());
				}

                if (ds.Tables[0].Rows[0]["AlipayPublicKey"] != null && ds.Tables[0].Rows[0]["AlipayPublicKey"].ToString() != "")
                {
                    model.AlipayPublicKey = ds.Tables[0].Rows[0]["AlipayPublicKey"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AlipayPrivateKey"] != null && ds.Tables[0].Rows[0]["AlipayPrivateKey"].ToString() != "")
                {
                    model.AlipayPrivateKey = ds.Tables[0].Rows[0]["AlipayPrivateKey"].ToString();
                }


				if (ds.Tables[0].Rows[0]["PointDrawPercent"] != null && ds.Tables[0].Rows[0]["PointDrawPercent"].ToString() != "")
				{
					model.PointDrawPercent = decimal.Parse(ds.Tables[0].Rows[0]["PointDrawPercent"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PointDiscountPercent"] != null && ds.Tables[0].Rows[0]["PointDiscountPercent"].ToString() != "")
				{
					model.PointDiscountPercent = decimal.Parse(ds.Tables[0].Rows[0]["PointDiscountPercent"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PointUsePercent"] != null && ds.Tables[0].Rows[0]["PointUsePercent"].ToString() != "")
				{
					model.PointUsePercent = decimal.Parse(ds.Tables[0].Rows[0]["PointUsePercent"].ToString());
				}
				if (ds.Tables[0].Rows[0]["VIPDescribe"] != null && ds.Tables[0].Rows[0]["VIPDescribe"].ToString() != "")
				{
					model.VIPDescribe = ds.Tables[0].Rows[0]["VIPDescribe"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Xiane"] != null && ds.Tables[0].Rows[0]["Xiane"].ToString() != "")
				{
					model.Xiane = decimal.Parse(ds.Tables[0].Rows[0]["Xiane"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MchId"] != null && ds.Tables[0].Rows[0]["MchId"].ToString() != "")
				{
					model.MchId = ds.Tables[0].Rows[0]["MchId"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Api"] != null && ds.Tables[0].Rows[0]["Api"].ToString() != "")
				{
					model.Api = ds.Tables[0].Rows[0]["Api"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Pay"] != null && ds.Tables[0].Rows[0]["Pay"].ToString() != "")
				{
					model.Pay = int.Parse(ds.Tables[0].Rows[0]["Pay"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ParameterID"] != null && ds.Tables[0].Rows[0]["ParameterID"].ToString() != "")
				{
					model.ParameterID = int.Parse(ds.Tables[0].Rows[0]["ParameterID"].ToString());
				}

                if (ds.Tables[0].Rows[0]["alipayAppid"] != null && ds.Tables[0].Rows[0]["alipayAppid"].ToString() != "")
                {
                    model.AlipayAppid = ds.Tables[0].Rows[0]["alipayAppid"].ToString();
                }


				if (ds.Tables[0].Rows[0]["Pwd"] != null && ds.Tables[0].Rows[0]["Pwd"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["Pwd"].ToString() == "1" || ds.Tables[0].Rows[0]["Pwd"].ToString().ToLower() == "true")
					{
						model.Pwd = true;
					}
					else
					{
						model.Pwd = false;
					}
				}
				if (ds.Tables[0].Rows[0]["MoneyAndPoint"] != null && ds.Tables[0].Rows[0]["MoneyAndPoint"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["MoneyAndPoint"].ToString() == "1" || ds.Tables[0].Rows[0]["MoneyAndPoint"].ToString().ToLower() == "true")
					{
						model.MoneyAndPoint = true;
					}
					else
					{
						model.MoneyAndPoint = false;
					}
				}
				if (ds.Tables[0].Rows[0]["AutoLevel"] != null && ds.Tables[0].Rows[0]["AutoLevel"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["AutoLevel"].ToString() == "1" || ds.Tables[0].Rows[0]["AutoLevel"].ToString().ToLower() == "true")
					{
						model.AutoLevel = true;
					}
					else
					{
						model.AutoLevel = false;
					}
				}
				if (ds.Tables[0].Rows[0]["DegradeLevel"] != null && ds.Tables[0].Rows[0]["DegradeLevel"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["DegradeLevel"].ToString() == "1" || ds.Tables[0].Rows[0]["DegradeLevel"].ToString().ToLower() == "true")
					{
						model.DegradeLevel = true;
					}
					else
					{
						model.DegradeLevel = false;
					}
				}
				if (ds.Tables[0].Rows[0]["PastTime"] != null && ds.Tables[0].Rows[0]["PastTime"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["PastTime"].ToString() == "1" || ds.Tables[0].Rows[0]["PastTime"].ToString().ToLower() == "true")
					{
						model.PastTime = true;
					}
					else
					{
						model.PastTime = false;
					}
				}
				if (ds.Tables[0].Rows[0]["RecommendPoint"] != null && ds.Tables[0].Rows[0]["RecommendPoint"].ToString() != "")
				{
					model.RecommendPoint = int.Parse(ds.Tables[0].Rows[0]["RecommendPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PointPeriod"] != null && ds.Tables[0].Rows[0]["PointPeriod"].ToString() != "")
				{
					model.PointPeriod = int.Parse(ds.Tables[0].Rows[0]["PointPeriod"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExpensePrefix"] != null && ds.Tables[0].Rows[0]["ExpensePrefix"].ToString() != "")
				{
					model.ExpensePrefix = ds.Tables[0].Rows[0]["ExpensePrefix"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GoodsExpensePrefix"] != null && ds.Tables[0].Rows[0]["GoodsExpensePrefix"].ToString() != "")
				{
					model.GoodsExpensePrefix = ds.Tables[0].Rows[0]["GoodsExpensePrefix"].ToString();
				}
				if (ds.Tables[0].Rows[0]["TimeExpensePrefix"] != null && ds.Tables[0].Rows[0]["TimeExpensePrefix"].ToString() != "")
				{
					model.TimeExpensePrefix = ds.Tables[0].Rows[0]["TimeExpensePrefix"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MemCountPrefix"] != null && ds.Tables[0].Rows[0]["MemCountPrefix"].ToString() != "")
				{
					model.MemCountPrefix = ds.Tables[0].Rows[0]["MemCountPrefix"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MemRechargePrefix"] != null && ds.Tables[0].Rows[0]["MemRechargePrefix"].ToString() != "")
				{
					model.MemRechargePrefix = ds.Tables[0].Rows[0]["MemRechargePrefix"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GoodsInPrefix"] != null && ds.Tables[0].Rows[0]["GoodsInPrefix"].ToString() != "")
				{
					model.GoodsInPrefix = ds.Tables[0].Rows[0]["GoodsInPrefix"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GoodsAllotPrefix"] != null && ds.Tables[0].Rows[0]["GoodsAllotPrefix"].ToString() != "")
				{
					model.GoodsAllotPrefix = ds.Tables[0].Rows[0]["GoodsAllotPrefix"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MemDrawMoneyPrefix"] != null && ds.Tables[0].Rows[0]["MemDrawMoneyPrefix"].ToString() != "")
				{
					model.MemDrawMoneyPrefix = ds.Tables[0].Rows[0]["MemDrawMoneyPrefix"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MemPointChangePrefix"] != null && ds.Tables[0].Rows[0]["MemPointChangePrefix"].ToString() != "")
				{
					model.MemPointChangePrefix = ds.Tables[0].Rows[0]["MemPointChangePrefix"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GiftExchangePrefix"] != null && ds.Tables[0].Rows[0]["GiftExchangePrefix"].ToString() != "")
				{
					model.GiftExchangePrefix = ds.Tables[0].Rows[0]["GiftExchangePrefix"].ToString();
				}
				if (ds.Tables[0].Rows[0]["AutoPrint"] != null && ds.Tables[0].Rows[0]["AutoPrint"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["AutoPrint"].ToString() == "1" || ds.Tables[0].Rows[0]["AutoPrint"].ToString().ToLower() == "true")
					{
						model.AutoPrint = true;
					}
					else
					{
						model.AutoPrint = false;
					}
				}
				if (ds.Tables[0].Rows[0]["AccordPrint"] != null && ds.Tables[0].Rows[0]["AccordPrint"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["AccordPrint"].ToString() == "1" || ds.Tables[0].Rows[0]["AccordPrint"].ToString().ToLower() == "true")
					{
						model.AccordPrint = true;
					}
					else
					{
						model.AccordPrint = false;
					}
				}
				if (ds.Tables[0].Rows[0]["PrintTitle"] != null && ds.Tables[0].Rows[0]["PrintTitle"].ToString() != "")
				{
					model.PrintTitle = ds.Tables[0].Rows[0]["PrintTitle"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PrintFootNote"] != null && ds.Tables[0].Rows[0]["PrintFootNote"].ToString() != "")
				{
					model.PrintFootNote = ds.Tables[0].Rows[0]["PrintFootNote"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Sms"] != null && ds.Tables[0].Rows[0]["Sms"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["Sms"].ToString() == "1" || ds.Tables[0].Rows[0]["Sms"].ToString().ToLower() == "true")
					{
						model.Sms = true;
					}
					else
					{
						model.Sms = false;
					}
				}
				if (ds.Tables[0].Rows[0]["MoneySms"] != null && ds.Tables[0].Rows[0]["MoneySms"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["MoneySms"].ToString() == "1" || ds.Tables[0].Rows[0]["MoneySms"].ToString().ToLower() == "true")
					{
						model.MoneySms = true;
					}
					else
					{
						model.MoneySms = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsSmsShopName"] != null && ds.Tables[0].Rows[0]["IsSmsShopName"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsSmsShopName"].ToString() == "1" || ds.Tables[0].Rows[0]["IsSmsShopName"].ToString().ToLower() == "true")
					{
						model.IsSmsShopName = true;
					}
					else
					{
						model.IsSmsShopName = false;
					}
				}
				if (ds.Tables[0].Rows[0]["SmsShopName"] != null && ds.Tables[0].Rows[0]["SmsShopName"].ToString() != "")
				{
					model.SmsShopName = ds.Tables[0].Rows[0]["SmsShopName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SmsSeries"] != null && ds.Tables[0].Rows[0]["SmsSeries"].ToString() != "")
				{
					model.SmsSeries = ds.Tables[0].Rows[0]["SmsSeries"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SmsSerialPwd"] != null && ds.Tables[0].Rows[0]["SmsSerialPwd"].ToString() != "")
				{
					model.SmsSerialPwd = ds.Tables[0].Rows[0]["SmsSerialPwd"].ToString();
				}
				if (ds.Tables[0].Rows[0]["DrawMoneyPercent"] != null && ds.Tables[0].Rows[0]["DrawMoneyPercent"].ToString() != "")
				{
					model.DrawMoneyPercent = decimal.Parse(ds.Tables[0].Rows[0]["DrawMoneyPercent"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Tel"] != null && ds.Tables[0].Rows[0]["Tel"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["Tel"].ToString() == "1" || ds.Tables[0].Rows[0]["Tel"].ToString().ToLower() == "true")
					{
						model.Tel = true;
					}
					else
					{
						model.Tel = false;
					}
				}
				if (ds.Tables[0].Rows[0]["TelNoMember"] != null && ds.Tables[0].Rows[0]["TelNoMember"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["TelNoMember"].ToString() == "1" || ds.Tables[0].Rows[0]["TelNoMember"].ToString().ToLower() == "true")
					{
						model.TelNoMember = true;
					}
					else
					{
						model.TelNoMember = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsStaff"] != null && ds.Tables[0].Rows[0]["IsStaff"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsStaff"].ToString() == "1" || ds.Tables[0].Rows[0]["IsStaff"].ToString().ToLower() == "true")
					{
						model.IsStaff = true;
					}
					else
					{
						model.IsStaff = false;
					}
				}
				if (ds.Tables[0].Rows[0]["StaffType"] != null && ds.Tables[0].Rows[0]["StaffType"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["StaffType"].ToString() == "1" || ds.Tables[0].Rows[0]["StaffType"].ToString().ToLower() == "true")
					{
						model.StaffType = true;
					}
					else
					{
						model.StaffType = false;
					}
				}
				if (ds.Tables[0].Rows[0]["PointLevel"] != null && ds.Tables[0].Rows[0]["PointLevel"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["PointLevel"].ToString() == "1" || ds.Tables[0].Rows[0]["PointLevel"].ToString().ToLower() == "true")
					{
						model.PointLevel = true;
					}
					else
					{
						model.PointLevel = false;
					}
				}
				if (ds.Tables[0].Rows[0]["MMS"] != null && ds.Tables[0].Rows[0]["MMS"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["MMS"].ToString() == "1" || ds.Tables[0].Rows[0]["MMS"].ToString().ToLower() == "true")
					{
						model.MMS = true;
					}
					else
					{
						model.MMS = false;
					}
				}
				if (ds.Tables[0].Rows[0]["MMSSeries"] != null && ds.Tables[0].Rows[0]["MMSSeries"].ToString() != "")
				{
					model.MMSSeries = ds.Tables[0].Rows[0]["MMSSeries"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MMSSerialPwd"] != null && ds.Tables[0].Rows[0]["MMSSerialPwd"].ToString() != "")
				{
					model.MMSSerialPwd = ds.Tables[0].Rows[0]["MMSSerialPwd"].ToString();
				}
				if (ds.Tables[0].Rows[0]["IsPayCard"] != null && ds.Tables[0].Rows[0]["IsPayCard"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsPayCard"].ToString() == "1" || ds.Tables[0].Rows[0]["IsPayCard"].ToString().ToLower() == "true")
					{
						model.IsPayCard = true;
					}
					else
					{
						model.IsPayCard = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsPayCash"] != null && ds.Tables[0].Rows[0]["IsPayCash"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsPayCash"].ToString() == "1" || ds.Tables[0].Rows[0]["IsPayCash"].ToString().ToLower() == "true")
					{
						model.IsPayCash = true;
					}
					else
					{
						model.IsPayCash = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsPayBink"] != null && ds.Tables[0].Rows[0]["IsPayBink"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsPayBink"].ToString() == "1" || ds.Tables[0].Rows[0]["IsPayBink"].ToString().ToLower() == "true")
					{
						model.IsPayBink = true;
					}
					else
					{
						model.IsPayBink = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsPayWeiXin"] != null && ds.Tables[0].Rows[0]["IsPayWeiXin"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsPayWeiXin"].ToString() == "1" || ds.Tables[0].Rows[0]["IsPayWeiXin"].ToString().ToLower() == "true")
					{
						model.IsPayWeiXin = true;
					}
					else
					{
						model.IsPayWeiXin = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsPayPoint"] != null && ds.Tables[0].Rows[0]["IsPayPoint"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsPayPoint"].ToString() == "1" || ds.Tables[0].Rows[0]["IsPayPoint"].ToString().ToLower() == "true")
					{
						model.IsPayPoint = true;
					}
					else
					{
						model.IsPayPoint = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsPayCoupon"] != null && ds.Tables[0].Rows[0]["IsPayCoupon"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsPayCoupon"].ToString() == "1" || ds.Tables[0].Rows[0]["IsPayCoupon"].ToString().ToLower() == "true")
					{
						model.IsPayCoupon = true;
					}
					else
					{
						model.IsPayCoupon = false;
					}
				}
				if (ds.Tables[0].Rows[0]["RegNullPwd"] != null && ds.Tables[0].Rows[0]["RegNullPwd"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["RegNullPwd"].ToString() == "1" || ds.Tables[0].Rows[0]["RegNullPwd"].ToString().ToLower() == "true")
					{
						model.RegNullPwd = true;
					}
					else
					{
						model.RegNullPwd = false;
					}
				}
				if (ds.Tables[0].Rows[0]["EmailAdress"] != null && ds.Tables[0].Rows[0]["EmailAdress"].ToString() != "")
				{
					model.EmailAdress = ds.Tables[0].Rows[0]["EmailAdress"].ToString();
				}
				if (ds.Tables[0].Rows[0]["EmailPwd"] != null && ds.Tables[0].Rows[0]["EmailPwd"].ToString() != "")
				{
					model.EmailPwd = ds.Tables[0].Rows[0]["EmailPwd"].ToString();
				}
				if (ds.Tables[0].Rows[0]["EmailSMTP"] != null && ds.Tables[0].Rows[0]["EmailSMTP"].ToString() != "")
				{
					model.EmailSMTP = ds.Tables[0].Rows[0]["EmailSMTP"].ToString();
				}
				if (ds.Tables[0].Rows[0]["StockCount"] != null && ds.Tables[0].Rows[0]["StockCount"].ToString() != "")
				{
					model.StockCount = int.Parse(ds.Tables[0].Rows[0]["StockCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["UnitList"] != null && ds.Tables[0].Rows[0]["UnitList"].ToString() != "")
				{
					model.UnitList = ds.Tables[0].Rows[0]["UnitList"].ToString();
				}
				if (ds.Tables[0].Rows[0]["WeiXinSMSVcode"] != null && ds.Tables[0].Rows[0]["WeiXinSMSVcode"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["WeiXinSMSVcode"].ToString() == "1" || ds.Tables[0].Rows[0]["WeiXinSMSVcode"].ToString().ToLower() == "true")
					{
						model.WeiXinSMSVcode = true;
					}
					else
					{
						model.WeiXinSMSVcode = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsAutoSendSMSByMemRegister"] != null && ds.Tables[0].Rows[0]["IsAutoSendSMSByMemRegister"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsAutoSendSMSByMemRegister"].ToString() == "1" || ds.Tables[0].Rows[0]["IsAutoSendSMSByMemRegister"].ToString().ToLower() == "true")
					{
						model.IsAutoSendSMSByMemRegister = true;
					}
					else
					{
						model.IsAutoSendSMSByMemRegister = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsAutoSendMMSByMemRegister"] != null && ds.Tables[0].Rows[0]["IsAutoSendMMSByMemRegister"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsAutoSendMMSByMemRegister"].ToString() == "1" || ds.Tables[0].Rows[0]["IsAutoSendMMSByMemRegister"].ToString().ToLower() == "true")
					{
						model.IsAutoSendMMSByMemRegister = true;
					}
					else
					{
						model.IsAutoSendMMSByMemRegister = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsAutoSendSMSByMemRecharge"] != null && ds.Tables[0].Rows[0]["IsAutoSendSMSByMemRecharge"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsAutoSendSMSByMemRecharge"].ToString() == "1" || ds.Tables[0].Rows[0]["IsAutoSendSMSByMemRecharge"].ToString().ToLower() == "true")
					{
						model.IsAutoSendSMSByMemRecharge = true;
					}
					else
					{
						model.IsAutoSendSMSByMemRecharge = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsAutoSendSMSByMemWithdraw"] != null && ds.Tables[0].Rows[0]["IsAutoSendSMSByMemWithdraw"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsAutoSendSMSByMemWithdraw"].ToString() == "1" || ds.Tables[0].Rows[0]["IsAutoSendSMSByMemWithdraw"].ToString().ToLower() == "true")
					{
						model.IsAutoSendSMSByMemWithdraw = true;
					}
					else
					{
						model.IsAutoSendSMSByMemWithdraw = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsAutoSendSMSByMemGiftExchange"] != null && ds.Tables[0].Rows[0]["IsAutoSendSMSByMemGiftExchange"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsAutoSendSMSByMemGiftExchange"].ToString() == "1" || ds.Tables[0].Rows[0]["IsAutoSendSMSByMemGiftExchange"].ToString().ToLower() == "true")
					{
						model.IsAutoSendSMSByMemGiftExchange = true;
					}
					else
					{
						model.IsAutoSendSMSByMemGiftExchange = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsAutoSendSMSByMemPointChange"] != null && ds.Tables[0].Rows[0]["IsAutoSendSMSByMemPointChange"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsAutoSendSMSByMemPointChange"].ToString() == "1" || ds.Tables[0].Rows[0]["IsAutoSendSMSByMemPointChange"].ToString().ToLower() == "true")
					{
						model.IsAutoSendSMSByMemPointChange = true;
					}
					else
					{
						model.IsAutoSendSMSByMemPointChange = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsAutoSendSMSByCommodityConsumption"] != null && ds.Tables[0].Rows[0]["IsAutoSendSMSByCommodityConsumption"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsAutoSendSMSByCommodityConsumption"].ToString() == "1" || ds.Tables[0].Rows[0]["IsAutoSendSMSByCommodityConsumption"].ToString().ToLower() == "true")
					{
						model.IsAutoSendSMSByCommodityConsumption = true;
					}
					else
					{
						model.IsAutoSendSMSByCommodityConsumption = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsAutoSendSMSByFastConsumption"] != null && ds.Tables[0].Rows[0]["IsAutoSendSMSByFastConsumption"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsAutoSendSMSByFastConsumption"].ToString() == "1" || ds.Tables[0].Rows[0]["IsAutoSendSMSByFastConsumption"].ToString().ToLower() == "true")
					{
						model.IsAutoSendSMSByFastConsumption = true;
					}
					else
					{
						model.IsAutoSendSMSByFastConsumption = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsAutoSendSMSByMemRedTimes"] != null && ds.Tables[0].Rows[0]["IsAutoSendSMSByMemRedTimes"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsAutoSendSMSByMemRedTimes"].ToString() == "1" || ds.Tables[0].Rows[0]["IsAutoSendSMSByMemRedTimes"].ToString().ToLower() == "true")
					{
						model.IsAutoSendSMSByMemRedTimes = true;
					}
					else
					{
						model.IsAutoSendSMSByMemRedTimes = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsAutoSendSMSByTimingConsumption"] != null && ds.Tables[0].Rows[0]["IsAutoSendSMSByTimingConsumption"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsAutoSendSMSByTimingConsumption"].ToString() == "1" || ds.Tables[0].Rows[0]["IsAutoSendSMSByTimingConsumption"].ToString().ToLower() == "true")
					{
						model.IsAutoSendSMSByTimingConsumption = true;
					}
					else
					{
						model.IsAutoSendSMSByTimingConsumption = false;
					}
				}
				if (ds.Tables[0].Rows[0]["SellerAccount"] != null && ds.Tables[0].Rows[0]["SellerAccount"].ToString() != "")
				{
					model.SellerAccount = ds.Tables[0].Rows[0]["SellerAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PartnerID"] != null && ds.Tables[0].Rows[0]["PartnerID"].ToString() != "")
				{
					model.PartnerID = ds.Tables[0].Rows[0]["PartnerID"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PartnerKey"] != null && ds.Tables[0].Rows[0]["PartnerKey"].ToString() != "")
				{
					model.PartnerKey = ds.Tables[0].Rows[0]["PartnerKey"].ToString();
				}
				if (ds.Tables[0].Rows[0]["IsEditPwdNeedOldPwd"] != null && ds.Tables[0].Rows[0]["IsEditPwdNeedOldPwd"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsEditPwdNeedOldPwd"].ToString() == "1" || ds.Tables[0].Rows[0]["IsEditPwdNeedOldPwd"].ToString().ToLower() == "true")
					{
						model.IsEditPwdNeedOldPwd = true;
					}
					else
					{
						model.IsEditPwdNeedOldPwd = false;
					}
				}
				if (ds.Tables[0].Rows[0]["WeiXinType"] != null && ds.Tables[0].Rows[0]["WeiXinType"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["WeiXinType"].ToString() == "1" || ds.Tables[0].Rows[0]["WeiXinType"].ToString().ToLower() == "true")
					{
						model.WeiXinType = true;
					}
					else
					{
						model.WeiXinType = false;
					}
				}
				if (ds.Tables[0].Rows[0]["WeiXinVerified"] != null && ds.Tables[0].Rows[0]["WeiXinVerified"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["WeiXinVerified"].ToString() == "1" || ds.Tables[0].Rows[0]["WeiXinVerified"].ToString().ToLower() == "true")
					{
						model.WeiXinVerified = true;
					}
					else
					{
						model.WeiXinVerified = false;
					}
				}
				if (ds.Tables[0].Rows[0]["WeiXinToken"] != null && ds.Tables[0].Rows[0]["WeiXinToken"].ToString() != "")
				{
					model.WeiXinToken = ds.Tables[0].Rows[0]["WeiXinToken"].ToString();
				}
				if (ds.Tables[0].Rows[0]["WeiXinEncodingAESKey"] != null && ds.Tables[0].Rows[0]["WeiXinEncodingAESKey"].ToString() != "")
				{
					model.WeiXinEncodingAESKey = ds.Tables[0].Rows[0]["WeiXinEncodingAESKey"].ToString();
				}
				if (ds.Tables[0].Rows[0]["WeiXinShopName"] != null && ds.Tables[0].Rows[0]["WeiXinShopName"].ToString() != "")
				{
					model.WeiXinShopName = ds.Tables[0].Rows[0]["WeiXinShopName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["WeiXinSalutatory"] != null && ds.Tables[0].Rows[0]["WeiXinSalutatory"].ToString() != "")
				{
					model.WeiXinSalutatory = ds.Tables[0].Rows[0]["WeiXinSalutatory"].ToString();
				}
				if (ds.Tables[0].Rows[0]["WeiXinAppID"] != null && ds.Tables[0].Rows[0]["WeiXinAppID"].ToString() != "")
				{
					model.WeiXinAppID = ds.Tables[0].Rows[0]["WeiXinAppID"].ToString();
				}
				if (ds.Tables[0].Rows[0]["WeiXinAppSecret"] != null && ds.Tables[0].Rows[0]["WeiXinAppSecret"].ToString() != "")
				{
					model.WeiXinAppSecret = ds.Tables[0].Rows[0]["WeiXinAppSecret"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SignInPoint"] != null && ds.Tables[0].Rows[0]["SignInPoint"].ToString() != "")
				{
					model.SignInPoint = int.Parse(ds.Tables[0].Rows[0]["SignInPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["IsMemRegisterStaff"] != null && ds.Tables[0].Rows[0]["IsMemRegisterStaff"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsMemRegisterStaff"].ToString() == "1" || ds.Tables[0].Rows[0]["IsMemRegisterStaff"].ToString().ToLower() == "true")
					{
						model.IsMemRegisterStaff = true;
					}
					else
					{
						model.IsMemRegisterStaff = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsMustSlotCard"] != null && ds.Tables[0].Rows[0]["IsMustSlotCard"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsMustSlotCard"].ToString() == "1" || ds.Tables[0].Rows[0]["IsMustSlotCard"].ToString().ToLower() == "true")
					{
						model.IsMustSlotCard = true;
					}
					else
					{
						model.IsMustSlotCard = false;
					}
				}
				if (ds.Tables[0].Rows[0]["StorageTimingPrefix"] != null && ds.Tables[0].Rows[0]["StorageTimingPrefix"].ToString() != "")
				{
					model.StorageTimingPrefix = ds.Tables[0].Rows[0]["StorageTimingPrefix"].ToString();
				}
				if (ds.Tables[0].Rows[0]["IsAutoSendSMSByStorageTiming"] != null && ds.Tables[0].Rows[0]["IsAutoSendSMSByStorageTiming"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsAutoSendSMSByStorageTiming"].ToString() == "1" || ds.Tables[0].Rows[0]["IsAutoSendSMSByStorageTiming"].ToString().ToLower() == "true")
					{
						model.IsAutoSendSMSByStorageTiming = true;
					}
					else
					{
						model.IsAutoSendSMSByStorageTiming = false;
					}
				}
				if (ds.Tables[0].Rows[0]["EnterpriseEmailPort"] != null && ds.Tables[0].Rows[0]["EnterpriseEmailPort"].ToString() != "")
				{
					model.EnterpriseEmailPort = int.Parse(ds.Tables[0].Rows[0]["EnterpriseEmailPort"].ToString());
				}
				if (ds.Tables[0].Rows[0]["EnterpriseEmailDisplayName"] != null && ds.Tables[0].Rows[0]["EnterpriseEmailDisplayName"].ToString() != "")
				{
					model.EnterpriseEmailDisplayName = ds.Tables[0].Rows[0]["EnterpriseEmailDisplayName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["EnterpriseEmailEnableSSL"] != null && ds.Tables[0].Rows[0]["EnterpriseEmailEnableSSL"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["EnterpriseEmailEnableSSL"].ToString() == "1" || ds.Tables[0].Rows[0]["EnterpriseEmailEnableSSL"].ToString().ToLower() == "true")
					{
						model.EnterpriseEmailEnableSSL = true;
					}
					else
					{
						model.EnterpriseEmailEnableSSL = false;
					}
				}
				if (ds.Tables[0].Rows[0]["EnterpriseEmailUseDefaultCredentials"] != null && ds.Tables[0].Rows[0]["EnterpriseEmailUseDefaultCredentials"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["EnterpriseEmailUseDefaultCredentials"].ToString() == "1" || ds.Tables[0].Rows[0]["EnterpriseEmailUseDefaultCredentials"].ToString().ToLower() == "true")
					{
						model.EnterpriseEmailUseDefaultCredentials = true;
					}
					else
					{
						model.EnterpriseEmailUseDefaultCredentials = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsEmail"] != null && ds.Tables[0].Rows[0]["IsEmail"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsEmail"].ToString() == "1" || ds.Tables[0].Rows[0]["IsEmail"].ToString().ToLower() == "true")
					{
						model.IsEmail = true;
					}
					else
					{
						model.IsEmail = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsEmailNotice"] != null && ds.Tables[0].Rows[0]["IsEmailNotice"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsEmailNotice"].ToString() == "1" || ds.Tables[0].Rows[0]["IsEmailNotice"].ToString().ToLower() == "true")
					{
						model.IsEmailNotice = true;
					}
					else
					{
						model.IsEmailNotice = false;
					}
				}
				if (ds.Tables[0].Rows[0]["MemCountExpensePrefix"] != null && ds.Tables[0].Rows[0]["MemCountExpensePrefix"].ToString() != "")
				{
					model.MemCountExpensePrefix = ds.Tables[0].Rows[0]["MemCountExpensePrefix"].ToString();
				}
				if (ds.Tables[0].Rows[0]["IsAutoSendSMSByMemPast"] != null && ds.Tables[0].Rows[0]["IsAutoSendSMSByMemPast"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsAutoSendSMSByMemPast"].ToString() == "1" || ds.Tables[0].Rows[0]["IsAutoSendSMSByMemPast"].ToString().ToLower() == "true")
					{
						model.IsAutoSendSMSByMemPast = true;
					}
					else
					{
						model.IsAutoSendSMSByMemPast = false;
					}
				}
				if (ds.Tables[0].Rows[0]["AutoSendSMSByMemPastForDay"] != null && ds.Tables[0].Rows[0]["AutoSendSMSByMemPastForDay"].ToString() != "")
				{
					model.AutoSendSMSByMemPastForDay = int.Parse(ds.Tables[0].Rows[0]["AutoSendSMSByMemPastForDay"].ToString());
				}
				if (ds.Tables[0].Rows[0]["IsAutoSendSMSByMemBirthday"] != null && ds.Tables[0].Rows[0]["IsAutoSendSMSByMemBirthday"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsAutoSendSMSByMemBirthday"].ToString() == "1" || ds.Tables[0].Rows[0]["IsAutoSendSMSByMemBirthday"].ToString().ToLower() == "true")
					{
						model.IsAutoSendSMSByMemBirthday = true;
					}
					else
					{
						model.IsAutoSendSMSByMemBirthday = false;
					}
				}
				if (ds.Tables[0].Rows[0]["AutoSendSMSByMemBirthdayForDay"] != null && ds.Tables[0].Rows[0]["AutoSendSMSByMemBirthdayForDay"].ToString() != "")
				{
					model.AutoSendSMSByMemBirthdayForDay = int.Parse(ds.Tables[0].Rows[0]["AutoSendSMSByMemBirthdayForDay"].ToString());
				}
				if (ds.Tables[0].Rows[0]["IsStartWeiXin"] != null && ds.Tables[0].Rows[0]["IsStartWeiXin"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsStartWeiXin"].ToString() == "1" || ds.Tables[0].Rows[0]["IsStartWeiXin"].ToString().ToLower() == "true")
					{
						model.IsStartWeiXin = true;
					}
					else
					{
						model.IsStartWeiXin = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsStartTimingProject"] != null && ds.Tables[0].Rows[0]["IsStartTimingProject"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsStartTimingProject"].ToString() == "1" || ds.Tables[0].Rows[0]["IsStartTimingProject"].ToString().ToLower() == "true")
					{
						model.IsStartTimingProject = true;
					}
					else
					{
						model.IsStartTimingProject = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsStartMemCount"] != null && ds.Tables[0].Rows[0]["IsStartMemCount"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsStartMemCount"].ToString() == "1" || ds.Tables[0].Rows[0]["IsStartMemCount"].ToString().ToLower() == "true")
					{
						model.IsStartMemCount = true;
					}
					else
					{
						model.IsStartMemCount = false;
					}
				}
				if (ds.Tables[0].Rows[0]["MarketingSMS"] != null && ds.Tables[0].Rows[0]["MarketingSMS"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["MarketingSMS"].ToString() == "1" || ds.Tables[0].Rows[0]["MarketingSMS"].ToString().ToLower() == "true")
					{
						model.MarketingSMS = true;
					}
					else
					{
						model.MarketingSMS = false;
					}
				}
				if (ds.Tables[0].Rows[0]["MarketingSmsSeries"] != null && ds.Tables[0].Rows[0]["MarketingSmsSeries"].ToString() != "")
				{
					model.MarketingSmsSeries = ds.Tables[0].Rows[0]["MarketingSmsSeries"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MarketingSmsSerialPwd"] != null && ds.Tables[0].Rows[0]["MarketingSmsSerialPwd"].ToString() != "")
				{
					model.MarketingSmsSerialPwd = ds.Tables[0].Rows[0]["MarketingSmsSerialPwd"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Senseiccard"] != null && ds.Tables[0].Rows[0]["Senseiccard"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["Senseiccard"].ToString() == "1" || ds.Tables[0].Rows[0]["Senseiccard"].ToString().ToLower() == "true")
					{
						model.Senseiccard = true;
					}
					else
					{
						model.Senseiccard = false;
					}
				}
				if (ds.Tables[0].Rows[0]["Contacticcard"] != null && ds.Tables[0].Rows[0]["Contacticcard"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["Contacticcard"].ToString() == "1" || ds.Tables[0].Rows[0]["Contacticcard"].ToString().ToLower() == "true")
					{
						model.Contacticcard = true;
					}
					else
					{
						model.Contacticcard = false;
					}
				}
				if (ds.Tables[0].Rows[0]["EmailUserName"] != null && ds.Tables[0].Rows[0]["EmailUserName"].ToString() != "")
				{
					model.EmailUserName = ds.Tables[0].Rows[0]["EmailUserName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PointNumStr"] != null && ds.Tables[0].Rows[0]["PointNumStr"].ToString() != "")
				{
					model.PointNumStr = ds.Tables[0].Rows[0]["PointNumStr"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PrintPreview"] != null && ds.Tables[0].Rows[0]["PrintPreview"].ToString() != "")
				{
					model.PrintPreview = int.Parse(ds.Tables[0].Rows[0]["PrintPreview"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PrintPaperType"] != null && ds.Tables[0].Rows[0]["PrintPaperType"].ToString() != "")
				{
					model.PrintPaperType = int.Parse(ds.Tables[0].Rows[0]["PrintPaperType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["IsSendCard"] != null && ds.Tables[0].Rows[0]["IsSendCard"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsSendCard"].ToString() == "1" || ds.Tables[0].Rows[0]["IsSendCard"].ToString().ToLower() == "true")
					{
						model.IsSendCard = true;
					}
					else
					{
						model.IsSendCard = false;
					}
				}
				if (ds.Tables[0].Rows[0]["ShopSmsManage"] != null && ds.Tables[0].Rows[0]["ShopSmsManage"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["ShopSmsManage"].ToString() == "1" || ds.Tables[0].Rows[0]["ShopSmsManage"].ToString().ToLower() == "true")
					{
						model.ShopSmsManage = true;
					}
					else
					{
						model.ShopSmsManage = false;
					}
				}
				if (ds.Tables[0].Rows[0]["ShopPointManage"] != null && ds.Tables[0].Rows[0]["ShopPointManage"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["ShopPointManage"].ToString() == "1" || ds.Tables[0].Rows[0]["ShopPointManage"].ToString().ToLower() == "true")
					{
						model.ShopPointManage = true;
					}
					else
					{
						model.ShopPointManage = false;
					}
				}
				if (ds.Tables[0].Rows[0]["ShopSettlement"] != null && ds.Tables[0].Rows[0]["ShopSettlement"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["ShopSettlement"].ToString() == "1" || ds.Tables[0].Rows[0]["ShopSettlement"].ToString().ToLower() == "true")
					{
						model.ShopSettlement = true;
					}
					else
					{
						model.ShopSettlement = false;
					}
				}
				if (ds.Tables[0].Rows[0]["AutoBackupDB"] != null && ds.Tables[0].Rows[0]["AutoBackupDB"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["AutoBackupDB"].ToString() == "1" || ds.Tables[0].Rows[0]["AutoBackupDB"].ToString().ToLower() == "true")
					{
						model.AutoBackupDB = true;
					}
					else
					{
						model.AutoBackupDB = false;
					}
				}
				if (ds.Tables[0].Rows[0]["AutoBackupDay"] != null && ds.Tables[0].Rows[0]["AutoBackupDay"].ToString() != "")
				{
					model.AutoBackupDay = int.Parse(ds.Tables[0].Rows[0]["AutoBackupDay"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SystemDomain"] != null && ds.Tables[0].Rows[0]["SystemDomain"].ToString() != "")
				{
					model.SystemDomain = ds.Tables[0].Rows[0]["SystemDomain"].ToString();
				}
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ParameterID,Pwd,MoneyAndPoint,AutoLevel,DegradeLevel,PastTime,RecommendPoint,PointPeriod,ExpensePrefix,GoodsExpensePrefix,TimeExpensePrefix,MemCountPrefix,MemRechargePrefix,GoodsInPrefix,GoodsAllotPrefix,MemDrawMoneyPrefix,MemPointChangePrefix,GiftExchangePrefix,AutoPrint,AccordPrint,PrintTitle,PrintFootNote,Sms,MoneySms,IsSmsShopName,SmsShopName,SmsSeries,SmsSerialPwd,DrawMoneyPercent,Tel,TelNoMember,IsStaff,StaffType,PointLevel,MMS,MMSSeries,MMSSerialPwd,IsPayCard,IsPayCash,IsPayBink,IsPayCoupon,RegNullPwd,EmailAdress,EmailPwd,EmailSMTP,StockCount,UnitList,WeiXinSMSVcode,IsAutoSendSMSByMemRegister,IsAutoSendMMSByMemRegister,IsAutoSendSMSByMemRecharge,IsAutoSendSMSByMemWithdraw,IsAutoSendSMSByMemGiftExchange,IsAutoSendSMSByMemPointChange,IsAutoSendSMSByCommodityConsumption,IsAutoSendSMSByFastConsumption,IsAutoSendSMSByMemRedTimes,IsAutoSendSMSByTimingConsumption,SellerAccount,PartnerID,PartnerKey,IsEditPwdNeedOldPwd,WeiXinType,WeiXinVerified,WeiXinToken,WeiXinEncodingAESKey,WeiXinShopName,WeiXinSalutatory,WeiXinAppID,WeiXinAppSecret,SignInPoint,IsMemRegisterStaff,IsMustSlotCard,StorageTimingPrefix,IsAutoSendSMSByStorageTiming,EnterpriseEmailPort,EnterpriseEmailDisplayName,EnterpriseEmailEnableSSL,EnterpriseEmailUseDefaultCredentials,IsEmail,IsEmailNotice,MemCountExpensePrefix,IsAutoSendSMSByMemPast,AutoSendSMSByMemPastForDay,IsAutoSendSMSByMemBirthday,AutoSendSMSByMemBirthdayForDay,IsStartWeiXin,IsStartTimingProject,IsStartMemCount,MarketingSMS,MarketingSmsSeries,MarketingSmsSerialPwd,Senseiccard,Contacticcard,EmailUserName,PointNumStr,PrintPreview,PrintPaperType,IsSendCard,ShopSmsManage,ShopPointManage,ShopSettlement,AutoBackupDB,AutoBackupDay,SystemDomain ");
			strSql.Append(" FROM SysParameter ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ");
			if (Top > 0)
			{
				strSql.Append(" top " + Top.ToString());
			}
			strSql.Append(" ParameterID,Pwd,MoneyAndPoint,AutoLevel,DegradeLevel,PastTime,RecommendPoint,PointPeriod,ExpensePrefix,GoodsExpensePrefix,TimeExpensePrefix,MemCountPrefix,MemRechargePrefix,GoodsInPrefix,GoodsAllotPrefix,MemDrawMoneyPrefix,MemPointChangePrefix,GiftExchangePrefix,AutoPrint,AccordPrint,PrintTitle,PrintFootNote,Sms,MoneySms,IsSmsShopName,SmsShopName,SmsSeries,SmsSerialPwd,DrawMoneyPercent,Tel,TelNoMember,IsStaff,StaffType,PointLevel,MMS,MMSSeries,MMSSerialPwd,IsPayCard,IsPayCash,IsPayBink,IsPayCoupon,RegNullPwd,EmailAdress,EmailPwd,EmailSMTP,StockCount,UnitList,WeiXinSMSVcode,IsAutoSendSMSByMemRegister,IsAutoSendMMSByMemRegister,IsAutoSendSMSByMemRecharge,IsAutoSendSMSByMemWithdraw,IsAutoSendSMSByMemGiftExchange,IsAutoSendSMSByMemPointChange,IsAutoSendSMSByCommodityConsumption,IsAutoSendSMSByFastConsumption,IsAutoSendSMSByMemRedTimes,IsAutoSendSMSByTimingConsumption,SellerAccount,PartnerID,PartnerKey,IsEditPwdNeedOldPwd,WeiXinType,WeiXinVerified,WeiXinToken,WeiXinEncodingAESKey,WeiXinShopName,WeiXinSalutatory,WeiXinAppID,WeiXinAppSecret,SignInPoint,IsMemRegisterStaff,IsMustSlotCard,StorageTimingPrefix,IsAutoSendSMSByStorageTiming,EnterpriseEmailPort,EnterpriseEmailDisplayName,EnterpriseEmailEnableSSL,EnterpriseEmailUseDefaultCredentials,IsEmail,IsEmailNotice,MemCountExpensePrefix,IsAutoSendSMSByMemPast,AutoSendSMSByMemPastForDay,IsAutoSendSMSByMemBirthday,AutoSendSMSByMemBirthdayForDay,IsStartWeiXin,IsStartTimingProject,IsStartMemCount,MarketingSMS,MarketingSmsSeries,MarketingSmsSerialPwd,Senseiccard,Contacticcard,EmailUserName,PointNumStr,PrintPreview,PrintPaperType,IsSendCard,ShopSmsManage,ShopPointManage,ShopSettlement,AutoBackupDB,AutoBackupDay,SystemDomain ");
			strSql.Append(" FROM SysParameter ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) FROM SysParameter ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			int result;
			if (obj == null)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(obj);
			}
			return result;
		}

		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby);
			}
			else
			{
				strSql.Append("order by T.ParameterID desc");
			}
			strSql.Append(")AS Row, T.*  from SysParameter T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ParameterID", "SysParameter");
		}

		public bool UpdateExtraParameter(Chain.Model.SysParameter model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysParameter set ");
			strSql.Append("Sms=@Sms,");
			strSql.Append("MoneySms=@MoneySms,");
			strSql.Append("IsSmsShopName=@IsSmsShopName,");
			strSql.Append("SmsShopName=@SmsShopName,");
			strSql.Append("SmsSeries=@SmsSeries,");
			strSql.Append("SmsSerialPwd=@SmsSerialPwd,");
			strSql.Append("Tel=@Tel,");
			strSql.Append("TelNoMember=@TelNoMember,");
			strSql.Append("MMS=@MMS,");
			strSql.Append("MMSSeries=@MMSSeries,");
			strSql.Append("MMSSerialPwd=@MMSSerialPwd,");
			strSql.Append("StockCount=@StockCount,");
			strSql.Append("UnitList=@UnitList,");
			strSql.Append("IsAutoSendSMSByMemRegister=@IsAutoSendSMSByMemRegister,");
			strSql.Append("IsAutoSendMMSByMemRegister=@IsAutoSendMMSByMemRegister,");
			strSql.Append("IsAutoSendSMSByMemRecharge=@IsAutoSendSMSByMemRecharge,");
			strSql.Append("IsAutoSendSMSByMemWithdraw=@IsAutoSendSMSByMemWithdraw,");
			strSql.Append("IsAutoSendSMSByMemGiftExchange=@IsAutoSendSMSByMemGiftExchange,");
			strSql.Append("IsAutoSendSMSByMemPointChange=@IsAutoSendSMSByMemPointChange,");
			strSql.Append("IsAutoSendSMSByCommodityConsumption=@IsAutoSendSMSByCommodityConsumption,");
			strSql.Append("IsAutoSendSMSByFastConsumption=@IsAutoSendSMSByFastConsumption,");
			strSql.Append("IsAutoSendSMSByMemRedTimes=@IsAutoSendSMSByMemRedTimes,");
			strSql.Append("IsAutoSendSMSByTimingConsumption=@IsAutoSendSMSByTimingConsumption,");
			strSql.Append("IsAutoSendSMSByStorageTiming=@IsAutoSendSMSByStorageTiming,");
			strSql.Append("MarketingSMS=@MarketingSMS,");
			strSql.Append("MarketingSmsSeries=@MarketingSmsSeries,");
			strSql.Append("MarketingSmsSerialPwd=@MarketingSmsSerialPwd");
			strSql.Append(" where ParameterID=@ParameterID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@Sms", SqlDbType.Bit, 1),
				new SqlParameter("@MoneySms", SqlDbType.Bit, 1),
				new SqlParameter("@IsSmsShopName", SqlDbType.Bit, 1),
				new SqlParameter("@SmsShopName", SqlDbType.NVarChar, 100),
				new SqlParameter("@SmsSeries", SqlDbType.VarChar, 50),
				new SqlParameter("@SmsSerialPwd", SqlDbType.VarChar, 50),
				new SqlParameter("@Tel", SqlDbType.Bit, 1),
				new SqlParameter("@TelNoMember", SqlDbType.Bit, 1),
				new SqlParameter("@MMS", SqlDbType.Bit),
				new SqlParameter("@MMSSeries", SqlDbType.VarChar, 50),
				new SqlParameter("@MMSSerialPwd", SqlDbType.VarChar, 50),
				new SqlParameter("@StockCount", SqlDbType.Int, 4),
				new SqlParameter("@UnitList", SqlDbType.NVarChar, 200),
				new SqlParameter("@IsAutoSendSMSByMemRegister", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendMMSByMemRegister", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemRecharge", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemWithdraw", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemGiftExchange", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemPointChange", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByCommodityConsumption", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByFastConsumption", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByMemRedTimes", SqlDbType.Bit, 1),
				new SqlParameter("@IsAutoSendSMSByTimingConsumption", SqlDbType.Bit, 1),
				new SqlParameter("@ParameterID", SqlDbType.Int, 4),
				new SqlParameter("@IsAutoSendSMSByStorageTiming", SqlDbType.Bit),
				new SqlParameter("@MarketingSMS", SqlDbType.Bit),
				new SqlParameter("@MarketingSmsSeries", SqlDbType.VarChar, 50),
				new SqlParameter("@MarketingSmsSerialPwd", SqlDbType.VarChar, 50)
			};
			parameters[0].Value = model.Sms;
			parameters[1].Value = model.MoneySms;
			parameters[2].Value = model.IsSmsShopName;
			parameters[3].Value = model.SmsShopName;
			parameters[4].Value = model.SmsSeries;
			parameters[5].Value = model.SmsSerialPwd;
			parameters[6].Value = model.Tel;
			parameters[7].Value = model.TelNoMember;
			parameters[8].Value = model.MMS;
			parameters[9].Value = model.MMSSeries;
			parameters[10].Value = model.MMSSerialPwd;
			parameters[11].Value = model.StockCount;
			parameters[12].Value = model.UnitList;
			parameters[13].Value = model.IsAutoSendSMSByMemRegister;
			parameters[14].Value = model.IsAutoSendMMSByMemRegister;
			parameters[15].Value = model.IsAutoSendSMSByMemRecharge;
			parameters[16].Value = model.IsAutoSendSMSByMemWithdraw;
			parameters[17].Value = model.IsAutoSendSMSByMemGiftExchange;
			parameters[18].Value = model.IsAutoSendSMSByMemPointChange;
			parameters[19].Value = model.IsAutoSendSMSByCommodityConsumption;
			parameters[20].Value = model.IsAutoSendSMSByFastConsumption;
			parameters[21].Value = model.IsAutoSendSMSByMemRedTimes;
			parameters[22].Value = model.IsAutoSendSMSByTimingConsumption;
			parameters[23].Value = model.ParameterID;
			parameters[24].Value = model.IsAutoSendSMSByStorageTiming;
			parameters[25].Value = model.MarketingSMS;
			parameters[26].Value = model.MarketingSmsSeries;
			parameters[27].Value = model.MarketingSmsSerialPwd;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public int DataBakUp(string FileName)
		{
			int result;
			if (DbHelperSQL.DataBaseBackup(FileName))
			{
				result = 1;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public int ReductionDataBakUp(string FileName)
		{
			int result;
			if (DbHelperSQL.DataBaseRecover(FileName))
			{
				try
				{
					this.GetMaxId();
				}
				catch
				{
				}
				result = 1;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public string GetDataBaseName()
		{
			return DbHelperSQL.GetDataBaseName();
		}

        public bool SwitchingMode(List<bool> Status, List<string> ModuleIDs)
        {
            StringBuilder sbSql = new StringBuilder();
            for (int i = 0; i < Status.Count; i++)
            {

                sbSql.AppendFormat("update SysModule set ModuleVisible='{0}' where ModuleID in ({1});", Status[i].ToString(), ModuleIDs[i]);
                sbSql.AppendFormat("update SysGroupAuthority set ActionValue='{0}' where  ModuleID in ({1});", Status[i].ToString(), ModuleIDs[i]);

                //if (Status[i])
                //{
                //    sbSql.AppendFormat("update SysGroupAuthority set ActionValue='{0}' where  ModuleID in ({1}) and GroupID=1 ;", Status[i].ToString(), ModuleIDs[i]);
                //}
                //else
                //{
                //    sbSql.AppendFormat("update SysGroupAuthority set ActionValue='{0}' where  ModuleID in ({1});", Status[i].ToString(), ModuleIDs[i]);
                //}
            }
            int rows = DbHelperSQL.ExecuteSql(sbSql.ToString());
            return rows > 0;
        }
	}
}
