using Chain.BLL;
using Chain.Model;
using ChainStock.WebReference;
using System;
using System.Text;

public class SMSInfo
{
	private static SmsService SMS = new SmsService();

	private static int intNotificationSMS
	{
		get
		{
			return PubFunction.curParameter.intNotificationSMS;
		}
	}

	private static string strNotificationSMS
	{
		get
		{
			return PubFunction.curParameter.strNotificationSMS;
		}
	}

	private static string strNotificationSMSPwd
	{
		get
		{
			return PubFunction.curParameter.strNotificationSMSPwd;
		}
	}

	private static int intMarketingSMS
	{
		get
		{
			return PubFunction.curParameter.intMarketingSMS;
		}
	}

	private static string strMarketingSmsSeries
	{
		get
		{
			return PubFunction.curParameter.strMarketingSmsSeries;
		}
	}

	private static string strMarketingSmsSerialPwd
	{
		get
		{
			return PubFunction.curParameter.strMarketingSmsSerialPwd;
		}
	}

	public static bool Send_GXSMS(bool SmsType, string MobileList, string Content, string Stime)
	{
		long startTime = DateTime.Now.Ticks;
		string result;
		if (SmsType)
		{
			result = SMSInfo.SMS.SendMessage(SMSInfo.intMarketingSMS, SMSInfo.strMarketingSmsSeries, SMSInfo.strMarketingSmsSerialPwd, MobileList, Content, Stime);
		}
		else
		{
			result = SMSInfo.SMS.SendMessage(SMSInfo.intNotificationSMS, SMSInfo.strNotificationSMS, SMSInfo.strNotificationSMSPwd, MobileList, Content, Stime);
		}
		long endTime = DateTime.Now.Ticks;
		TimeSpan sendSpan = new TimeSpan(endTime - startTime);
		return result.Length > 10;
	}

	public static bool Send_SMS(bool SmsType, string MobileList, string Content, string Stime)
	{
		long startTime = DateTime.Now.Ticks;
		string result;
		if (SmsType)
		{
			result = SMSInfo.SMS.SendMessage(SMSInfo.intMarketingSMS, SMSInfo.strMarketingSmsSeries, SMSInfo.strMarketingSmsSerialPwd, MobileList, Content, Stime);
		}
		else
		{
			result = SMSInfo.SMS.SendMessage(SMSInfo.intNotificationSMS, SMSInfo.strNotificationSMS, SMSInfo.strNotificationSMSPwd, MobileList, Content, Stime);
		}
		long endTime = DateTime.Now.Ticks;
		TimeSpan sendSpan = new TimeSpan(endTime - startTime);
		return result.Length > 10;
	}

	public static string GetBalance(bool smsType)
	{
		string strBalance = "0";
		try
		{
			if (smsType)
			{
				strBalance = SMSInfo.SMS.GetBalance(SMSInfo.intMarketingSMS, SMSInfo.strMarketingSmsSeries, SMSInfo.strMarketingSmsSerialPwd);
			}
			else
			{
				strBalance = SMSInfo.SMS.GetBalance(SMSInfo.intNotificationSMS, SMSInfo.strNotificationSMS, SMSInfo.strNotificationSMSPwd);
			}
		}
		catch
		{
		}
		return strBalance;
	}

	public static string GetSendContent(int intTemplateType, SmsTemplateParameter smsTemplateParameter, int intShopID)
	{
		Chain.BLL.SmsTemplate bllTemplate = new Chain.BLL.SmsTemplate();
		Chain.Model.SmsTemplate modelTemplate = new Chain.Model.SmsTemplate();
		modelTemplate = bllTemplate.GetModel(intTemplateType);
		StringBuilder strTemplateContent = new StringBuilder(modelTemplate.TemplateContent);
		strTemplateContent.Replace("{CardID}", smsTemplateParameter.strCardID);
		strTemplateContent.Replace("{LCardID}", smsTemplateParameter.strCardID.Substring(smsTemplateParameter.strCardID.Length - 3));
		strTemplateContent.Replace("{Name}", smsTemplateParameter.strName);
		strTemplateContent.Replace("{TempMoney}", smsTemplateParameter.dclTempMoney.ToString("0.00"));
		strTemplateContent.Replace("{Money}", smsTemplateParameter.dclMoney.ToString("0.00"));
		strTemplateContent.Replace("{Time}", DateTime.Now.ToString("yy年MM月dd日HH时mm分"));
		strTemplateContent.Replace("{TempPoint}", smsTemplateParameter.intTempPoint.ToString());
		strTemplateContent.Replace("{Point}", smsTemplateParameter.intPoint.ToString());
		Chain.BLL.MemLevel bllMemLevel = new Chain.BLL.MemLevel();
		Chain.Model.MemLevel modelMemLevel = new Chain.Model.MemLevel();
		modelMemLevel = bllMemLevel.GetModel(smsTemplateParameter.OldLevelID);
		strTemplateContent.Replace("{OldLevel}", modelMemLevel.LevelName);
		modelMemLevel = bllMemLevel.GetModel(smsTemplateParameter.NewLevelID);
		strTemplateContent.Replace("{NewLevel}", modelMemLevel.LevelName);
		strTemplateContent.Replace("{MemBirthday}", string.Format("{0}月{1}日", Convert.ToInt32(smsTemplateParameter.MemBirthday.ToString("MM")), Convert.ToInt32(smsTemplateParameter.MemBirthday.ToString("dd"))));
		strTemplateContent.Replace("{MemPastTime}", smsTemplateParameter.MemPastTime.ToString("yyyy-MM-dd"));
		strTemplateContent.Replace("{TempGoodsItem}", smsTemplateParameter.CountItemsString);
		if (modelTemplate.TemplateID < 13)
		{
			if (PubFunction.curParameter.bolIsSmsShopName)
			{
				if (PubFunction.curParameter.strSmsShopName != "")
				{
					strTemplateContent.Append("【" + PubFunction.curParameter.strSmsShopName + "】");
				}
			}
			else
			{
				Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
				Chain.Model.SysShop modelShop = new Chain.Model.SysShop();
				modelShop = bllShop.GetModel(intShopID);
				if (modelShop.ShopSmsName != "")
				{
					strTemplateContent.Append("【" + modelShop.ShopSmsName + "】");
				}
			}
		}
		return strTemplateContent.ToString();
	}
}
