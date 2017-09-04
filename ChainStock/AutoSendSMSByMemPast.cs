using Chain.BLL;
using Chain.Model;
using Chain.Tasks;
using System;
using System.Data;
using System.Xml;

public class AutoSendSMSByMemPast : ITask
{
	public void Execute(XmlNode node)
	{
		if (PubFunction.curParameter.bolIsAutoSendSMSByMemPast)
		{
			if (PubFunction.curParameter.bolSms && Convert.ToInt32(SMSInfo.GetBalance(false)) > 0)
			{
				Chain.BLL.Mem MemBll = new Chain.BLL.Mem();
				Chain.BLL.MemberSMSRemindLog MemberSMSRemindLogBll = new Chain.BLL.MemberSMSRemindLog();
				DataTable dtMem = MemBll.GetMemPast(PubFunction.curParameter.intAutoSendSMSByMemPastForDay).Tables[0];
				DataTable dtLog = MemberSMSRemindLogBll.GetMemPasted().Tables[0];
				if (dtMem != null && dtMem.Rows.Count > 0)
				{
					for (int i = 0; i < dtMem.Rows.Count; i++)
					{
						if (dtLog.Select("MemberSMSRemindMemID=" + dtMem.Rows[i]["MemID"]).Length == 0)
						{
							if (Convert.ToInt32(SMSInfo.GetBalance(false)) > 0)
							{
								string strSendContent = SMSInfo.GetSendContent(11, new SmsTemplateParameter
								{
									strCardID = dtMem.Rows[i]["MemCard"].ToString(),
									strName = dtMem.Rows[i]["MemName"].ToString(),
									dclMoney = Convert.ToDecimal(dtMem.Rows[i]["MemMoney"]),
									dclTempMoney = 0m,
									intTempPoint = 0,
									intPoint = Convert.ToInt32(dtMem.Rows[i]["MemPoint"]),
									OldLevelID = Convert.ToInt32(dtMem.Rows[i]["MemLevelID"]),
									NewLevelID = Convert.ToInt32(dtMem.Rows[i]["MemLevelID"])
								}, 1);
								bool result = SMSInfo.Send_GXSMS(false, dtMem.Rows[i]["MemMobile"].ToString(), strSendContent, "");
								if (result)
								{
									Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
									modelSms.SmsMemID = Convert.ToInt32(dtMem.Rows[i]["MemID"]);
									modelSms.SmsMobile = dtMem.Rows[i]["MemMobile"].ToString();
									modelSms.SmsContent = strSendContent;
									modelSms.SmsTime = DateTime.Now;
									modelSms.SmsShopID = 1;
									modelSms.SmsUserID = 1;
									modelSms.SmsAmount = PubFunction.GetSmsAmount(strSendContent);
									modelSms.SmsAllAmount = modelSms.SmsAmount;
									Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
									bllSms.Add(modelSms);
									MemberSMSRemindLogBll.Add(new Chain.Model.MemberSMSRemindLog
									{
										MemberSMSRemindMemID = Convert.ToInt32(dtMem.Rows[i]["MemID"]),
										MemberSMSRemindMobile = dtMem.Rows[i]["MemMobile"].ToString(),
										MemberSMSRemindContent = strSendContent,
										MemberSMSRemindShopID = 1,
										MemberSMSRemindTime = DateTime.Now,
										MemberSMSRemindUserID = 1,
										MemberSMSRemindAmount = PubFunction.GetSmsAmount(strSendContent),
										MemberSMSRemindAllAmount = modelSms.SmsAmount,
										MemberSMSRemindType = 2,
										MemberSMSRemindBirthday = Convert.ToDateTime(dtMem.Rows[i]["MemPastTime"])
									});
								}
							}
						}
					}
				}
			}
		}
	}
}
