using Chain.BLL;
using Chain.Common;
using Chain.Common.DEncrypt;
using Chain.Model;
using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace Chain.Wechat
{
	public class TextRequest : IRequest
	{
		public string Content
		{
			get;
			set;
		}

		public string MsgId
		{
			get;
			set;
		}

		public TextRequest(XmlDocument input) : base(input)
		{
			XmlNode root = base.XmlDoc.FirstChild;
			this.Content = root["Content"].InnerText.Trim();
			this.MsgId = root["MsgId"].InnerText;
			base.RequestEvent += new IRequest.RequestEventHandler(this.TextReqeustEventHandler);
		}

		public string TextReqeustEventHandler()
		{
			string result;
			if (LogManager.ContainsKey(base.FromUserName))
			{
				ILog log = LogManager.Get(base.FromUserName);
				string logType = log.LogType;
				if (logType != null)
				{
					if (logType == "RegisterEntryNumberLog")
					{
						RegisterEntryNumberLog regLog = (RegisterEntryNumberLog)log;
						result = this.RegisterEntryPhone(regLog);
						return result;
					}
					if (logType == "RegisterEntryCodeLog")
					{
						RegisterEntryCodeLog codeLog = (RegisterEntryCodeLog)log;
						result = this.RegisterEntryCode(codeLog);
						return result;
					}
					if (logType == "TransformEntryNumberLog")
					{
						TransformEntryNumberLog transformNumberLog = (TransformEntryNumberLog)log;
						result = this.TransformEntryNumber(transformNumberLog);
						return result;
					}
					if (logType == "TransformEntryCodeLog")
					{
						TransformEntryCodeLog transformCodeLog = (TransformEntryCodeLog)log;
						result = this.TransformEntryCode(transformCodeLog);
						return result;
					}
					if (logType == "TransformEntryPwdLog")
					{
						TransformEntryPwdLog transformPwdLog = (TransformEntryPwdLog)log;
						result = this.TransformEntryPwd(transformPwdLog);
						return result;
					}
				}
				LogManager.Remove(base.FromUserName);
				result = string.Empty;
			}
			else if (this.Content == "会员")
			{
				Chain.Model.Mem mem = new Chain.BLL.Mem().GetMemByWeiXinCard(base.FromUserName);
				if (mem != null)
				{
					result = new BusinessLogic().GetMemCardResponse(this, mem, "");
				}
				else
				{
					RegisterEntryNumberLog log2 = new RegisterEntryNumberLog(0);
					LogManager.Add(base.FromUserName, log2);
					result = new TextResponse(this, "您已进入会员申请模式,请发送您的手机号码!").Result();
				}
			}
			else if (this.Content == "绑定")
			{
				Chain.Model.Mem mem = new Chain.BLL.Mem().GetMemByWeiXinCard(base.FromUserName);
				if (mem != null)
				{
					result = new TextResponse(this, "您已经绑定了微信会员!").Result();
				}
				else
				{
					TransformEntryNumberLog log3 = new TransformEntryNumberLog(0);
					LogManager.Add(base.FromUserName, log3);
					result = new TextResponse(this, "您已进入转微信会员模式,请发送您的卡号!").Result();
				}
			}
			else if (this.Content == "签到")
			{
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem mem = bllMem.GetMemByWeiXinCard(base.FromUserName);
				if (mem != null)
				{
					switch (new BusinessLogic().MemSign(mem))
					{
					case -3:
						result = new TextResponse(this, "您今日已经签到过，重复签到不会再次赠送积分!").Result();
						return result;
					case -2:
						result = new TextResponse(this, "对不起，签到送积分活动未开启!").Result();
						return result;
					case -1:
						result = new TextResponse(this, "您还未申请或绑定会员卡!").Result();
						return result;
					case 1:
						mem = bllMem.GetMemByWeiXinCard(base.FromUserName);
						result = new BusinessLogic().GetMemCardResponse(this, mem, "今日签到成功!");
						return result;
					}
					result = new TextResponse(this, "系统出现未知错误!").Result();
				}
				else
				{
					result = new TextResponse(this, "您还未申请或绑定会员卡!").Result();
				}
			}
			else
			{
				string responseText = new BusinessLogic().GetResponseByRule(this, this.Content);
				if (!string.IsNullOrEmpty(responseText))
				{
					result = responseText;
				}
				else
				{
					result = new TextResponse(this, new BusinessLogic().GetWelcomeText()).Result();
				}
			}
			return result;
		}

		private string TransformEntryPwd(TransformEntryPwdLog transformPwdLog)
		{
			transformPwdLog.WrningCount++;
			string pwd = (this.Content == "#") ? "" : ((this.Content == "＃") ? "" : this.Content);
			pwd = DESEncrypt.Encrypt(pwd);
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			Chain.Model.Mem mem = bllMem.CheckMemPwd(transformPwdLog.MemCard, pwd);
			string result;
			if (mem != null)
			{
				LogManager.Remove(base.FromUserName);
				mem.MemWeiXinCard = base.FromUserName;
				mem.MemWeiXinCards = base.FromUserName;
				mem.MemAttention = 1;
				bllMem.Update(mem);
				result = new BusinessLogic().GetMemCardResponse(this, mem, "绑定微信会员卡成功!");
			}
			else if (transformPwdLog.WrningCount >= 3)
			{
				LogManager.Remove(base.FromUserName);
				result = new TextResponse(this, "由于您的误操作次数过多,会员转微会员模式已退出!").Result();
			}
			else
			{
				LogManager.Add(base.FromUserName, transformPwdLog);
				result = new TextResponse(this, "您发送的密码不正确,请重新发送!").Result();
			}
			return result;
		}

		private string TransformEntryCode(TransformEntryCodeLog transformCodeLog)
		{
			transformCodeLog.WrningCount++;
			string result;
			if (transformCodeLog.RandomCode == this.Content)
			{
				LogManager.Remove(base.FromUserName);
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem mem = bllMem.GetModelByMemCard(transformCodeLog.MemCard);
				mem.MemWeiXinCard = base.FromUserName;
				bllMem.Update(mem);
				result = new BusinessLogic().GetMemCardResponse(this, mem, "绑定微信会员卡成功!");
			}
			else if (transformCodeLog.WrningCount >= 3)
			{
				LogManager.Remove(base.FromUserName);
				result = new TextResponse(this, "由于您的误操作次数过多,会员转微会员模式已退出!").Result();
			}
			else
			{
				LogManager.Add(base.FromUserName, transformCodeLog);
				result = new TextResponse(this, "您发送的验证码不正确,请重新发送!").Result();
			}
			return result;
		}

		private string TransformEntryNumber(TransformEntryNumberLog transformLog)
		{
			transformLog.WrningCount++;
			Chain.Model.Mem mem = new Chain.BLL.Mem().GetModelByMemCard(this.Content);
			string result;
			string randomCode;
			if (mem == null)
			{
				if (transformLog.WrningCount >= 3)
				{
					LogManager.Remove(base.FromUserName);
					result = new TextResponse(this, "由于您的误操作次数过多,会员转微会员模式已退出!").Result();
				}
				else
				{
					LogManager.Add(base.FromUserName, transformLog);
					result = new TextResponse(this, "没有查找到您输入的卡号,请重新输入一个正确的卡号!").Result();
				}
			}
			else if (this.SendRandomCode(base.FromUserName, mem.MemMobile, out randomCode))
			{
				TransformEntryCodeLog transformCodeLog = new TransformEntryCodeLog(0, randomCode, this.Content);
				LogManager.Add(base.FromUserName, transformCodeLog);
				result = new TextResponse(this, "温馨提示,已经向您的手机发送了验证码,请回复收到的验证码确认手机号码!").Result();
			}
			else
			{
				TransformEntryPwdLog transformPwdLog = new TransformEntryPwdLog(0, this.Content);
				LogManager.Add(base.FromUserName, transformPwdLog);
				result = new TextResponse(this, "温馨提示,已查找到对应的卡号,请回复会员卡密码!无密码请回复#").Result();
			}
			return result;
		}

		private string RegisterEntryCode(RegisterEntryCodeLog codeLog)
		{
			codeLog.WrningCount++;
			string result;
			if (codeLog.RandomCode == this.Content)
			{
				LogManager.Remove(base.FromUserName);
				result = this.TryRegisterMem(codeLog.MobileNumber, base.FromUserName);
			}
			else if (codeLog.WrningCount >= 3)
			{
				LogManager.Remove(base.FromUserName);
				result = new TextResponse(this, "由于您的误操作次数过多,会员申请模式已退出!").Result();
			}
			else
			{
				LogManager.Add(base.FromUserName, codeLog);
				result = new TextResponse(this, "您发送的验证码不正确,请重新发送!").Result();
			}
			return result;
		}

		private string RegisterEntryPhone(RegisterEntryNumberLog regLog)
		{
			regLog.WrningCount++;
			string result;
			if (!this.CheckMobileFormat(this.Content))
			{
				if (regLog.WrningCount >= 3)
				{
					LogManager.Remove(base.FromUserName);
					result = new TextResponse(this, "由于您的误操作次数过多,会员申请模式已退出!").Result();
				}
				else
				{
					LogManager.Add(base.FromUserName, regLog);
					result = new TextResponse(this, "您发送的手机号格式有误,请重新发送!").Result();
				}
			}
			else
			{
				Chain.Model.Mem mem = new Chain.BLL.Mem().GetModelByMemMobile(this.Content);
				string randomCode;
				if (mem != null)
				{
					if (regLog.WrningCount >= 3)
					{
						LogManager.Remove(base.FromUserName);
						result = new TextResponse(this, "由于您的误操作次数过多,会员申请模式已退出!").Result();
					}
					else
					{
						LogManager.Add(base.FromUserName, regLog);
						result = new TextResponse(this, "该手机号已被注册,请重新输入一个新的手机号!").Result();
					}
				}
				else if (this.SendRandomCode(base.FromUserName, this.Content, out randomCode))
				{
					RegisterEntryCodeLog codeLog = new RegisterEntryCodeLog(0, randomCode, this.Content);
					LogManager.Add(base.FromUserName, codeLog);
					result = new TextResponse(this, "温馨提示,已经向您的手机发送了验证码,请输入验证码确认手机号码!").Result();
				}
				else
				{
					LogManager.Remove(base.FromUserName);
					result = this.TryRegisterMem(this.Content, base.FromUserName);
				}
			}
			return result;
		}

		private string TryRegisterMem(string mobile, string openID)
		{
			int status = new BusinessLogic().MemRegister(mobile, openID);
			string result;
			if (status > 0)
			{
				Chain.Model.Mem mem = new Chain.BLL.Mem().GetModel(status);
				string show = "注册微信会员卡成功\r\n会员卡密码默认为手机后6位数\r\n请及时登陆会员自助平台进行修改！";
				result = new BusinessLogic().GetMemCardResponse(this, mem, show);
			}
			else if (status == -1)
			{
				result = new TextResponse(this, "会员卡号重复,会员办卡失败!").Result();
			}
			else if (status == -2)
			{
				result = new TextResponse(this, "手机号码重复,会员办卡失败!").Result();
			}
			else if (status == -6)
			{
				result = new TextResponse(this, "卡面号重复,会员办卡失败!").Result();
			}
			else
			{
				result = new TextResponse(this, "未知原因,办卡失败!").Result();
			}
			return result;
		}

		private bool SendRandomCode(string openId, string mobile, out string randomCode)
		{
			Chain.Model.SysParameter modelSysPara = new Chain.BLL.SysParameter().GetModel(1);
			bool result;
			if (modelSysPara != null && modelSysPara.WeiXinSMSVcode)
			{
				if (modelSysPara.Sms && this.CheckMobileFormat(mobile))
				{
					SMSHelper sms = new SMSHelper(4, modelSysPara.SmsSeries, modelSysPara.SmsSerialPwd);
					if (sms.GetBalance() > 0)
					{
						randomCode = this.GetRandomCode();
						string notify = string.Concat(new string[]
						{
							"温馨提示,欢迎您注册",
							modelSysPara.WeiXinShopName,
							"微信会员,您的",
							modelSysPara.WeiXinShopName,
							"微信会员短信验证码是：",
							randomCode
						});
						if (sms.Send_GXSMS(mobile, notify, ""))
						{
							Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
							modelSms.SmsMemID = 0;
							modelSms.SmsMobile = this.Content;
							modelSms.SmsContent = notify;
							modelSms.SmsTime = DateTime.Now;
							modelSms.SmsShopID = 1;
							modelSms.SmsUserID = 1;
							modelSms.SmsAmount = 1;
							modelSms.SmsAllAmount = modelSms.SmsAmount;
							new Chain.BLL.SmsLog().Add(modelSms);
							result = true;
							return result;
						}
					}
				}
			}
			randomCode = "";
			result = false;
			return result;
		}

		private bool CheckMobileFormat(string mobile)
		{
			return Regex.IsMatch(mobile, "^1[3-9]\\d{9}$");
		}

		private string GetRandomCode()
		{
			Random random = new Random();
			string result = string.Empty;
			for (int i = 0; i < 4; i++)
			{
				result += random.Next(0, 10);
			}
			return result;
		}
	}
}
