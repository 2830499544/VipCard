using Chain.BLL;
using Chain.Model;
using ChainStock.WebReference;
using System;
using System.IO;
using System.Net;
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
            result = SendSMS_New(SMSInfo.intMarketingSMS, SMSInfo.strMarketingSmsSeries, SMSInfo.strMarketingSmsSerialPwd, MobileList, Content, Stime);
		}
		else
		{
            result = SendSMS_New(SMSInfo.intNotificationSMS, SMSInfo.strNotificationSMS, SMSInfo.strNotificationSMSPwd, MobileList, Content, Stime);
		}
		long endTime = DateTime.Now.Ticks;
		TimeSpan sendSpan = new TimeSpan(endTime - startTime);
        return result.IndexOf("000/") >= 0;
//        return result == "000";
    }


    public static string SendSms(string purl,string str)
    {
        try
        {
            //string purl = "http://service.winic.org:8009/sys_port/gateway/index.asp?";

            byte[] data = System.Text.Encoding.GetEncoding("GB2312").GetBytes(str);
            // 准备请求 
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(purl);

            //设置超时
            req.Timeout = 30000;
            req.Method = "Post";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;
            Stream stream = req.GetRequestStream();
            // 发送数据 
            stream.Write(data, 0, data.Length);
            stream.Close();

            HttpWebResponse rep = (HttpWebResponse)req.GetResponse();
            Stream receiveStream = rep.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("GB2312");
            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, encode);

            Char[] read = new Char[256];
            int count = readStream.Read(read, 0, 256);
            StringBuilder sb = new StringBuilder("");
            while (count > 0)
            {
                String readstr = new String(read, 0, count);
                sb.Append(readstr);
                count = readStream.Read(read, 0, 256);
            }

            rep.Close();
            readStream.Close();

            //return true;
            return sb.ToString();

        }
        catch (Exception ex)
        {
            return "";
            // ForumExceptions.Log(ex);
        }
    }
    public static string SendSMS_New(int intSmsType, string username, string password, string mobile, string content, string stime)
    {
        string postdata = "id=" + username + "&pwd=" + password + "&to=" + mobile + "&content=" + content + "&time=" + stime;
        return SendSms("http://service.winic.org:8009/sys_port/gateway/index.asp?",postdata);
//        string backinfo = PostData("http://service.winic.org:8009/sys_port/gateway/index.asp?", "id=" + smsuid + "&pwd=" + smspwd + "&to=" + tos + "&content=" + msg + "&time=");
    }

    public static string GetSMSBalance(string username,string password)
    {
        string url = "http://service.winic.org/webservice/public/remoney.asp";
        string postdata = "uid=" + username + "&pwd=" + password;
        return SendSms(url, postdata);
    }

	public static bool Send_SMS(bool SmsType, string MobileList, string Content, string Stime)
	{
		long startTime = DateTime.Now.Ticks;
		string result;
		if (SmsType)
		{
            result = SendSMS_New(SMSInfo.intMarketingSMS, SMSInfo.strMarketingSmsSeries, SMSInfo.strMarketingSmsSerialPwd, MobileList, Content, Stime);
            //result = SMSInfo.SMS.SendMessage(SMSInfo.intMarketingSMS, SMSInfo.strMarketingSmsSeries, SMSInfo.strMarketingSmsSerialPwd, MobileList, Content, Stime);
		}
		else
		{
            result = SendSMS_New(SMSInfo.intNotificationSMS, SMSInfo.strNotificationSMS, SMSInfo.strNotificationSMSPwd, MobileList, Content, Stime);
            //result = SMSInfo.SMS.SendMessage(SMSInfo.intNotificationSMS, SMSInfo.strNotificationSMS, SMSInfo.strNotificationSMSPwd, MobileList, Content, Stime);
		}
		long endTime = DateTime.Now.Ticks;
		TimeSpan sendSpan = new TimeSpan(endTime - startTime);
        return result.IndexOf("000/")>=0;
	}

	public static string GetBalance(bool smsType)
	{
		string strBalance = "0";
		try
		{
			if (smsType)
			{
                strBalance = GetSMSBalance(SMSInfo.strMarketingSmsSeries, SMSInfo.strMarketingSmsSerialPwd);
				//strBalance = SMSInfo.SMS.GetBalance(SMSInfo.intMarketingSMS, SMSInfo.strMarketingSmsSeries, SMSInfo.strMarketingSmsSerialPwd);
			}
			else
			{
                strBalance = GetSMSBalance(SMSInfo.strNotificationSMS, SMSInfo.strNotificationSMSPwd);
                //strBalance = SMSInfo.SMS.GetBalance(SMSInfo.intNotificationSMS, SMSInfo.strNotificationSMS, SMSInfo.strNotificationSMS);
			}
		}
		catch
		{
		}

        float v = 0;
        if (strBalance != "")
            v = float.Parse(strBalance);
        return ((int)v).ToString();
		//return strBalance;
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
