using Chain.BLL;
using Chain.Common;
using Chain.Common.DEncrypt;
using Chain.Model;
using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace ChainStock.Service
{
	public class PhoneService : IHttpHandler, IRequiresSessionState
	{
		private HttpRequest Request;

		private HttpResponse Response;

		private HttpSessionState Session;

		private HttpServerUtility Server;

		private HttpContext Context;

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			context.Response.Buffer = true;
			context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0);
			context.Response.AddHeader("pragma", "no-cache");
			context.Response.AddHeader("cache-control", "");
			context.Response.CacheControl = "no-cache";
			context.Response.ContentType = "application/json";
			this.Request = context.Request;
			this.Response = context.Response;
			this.Session = context.Session;
			this.Server = context.Server;
			this.Context = context;
			string method = this.Request["Method"].ToString();
			MethodInfo methodInfo = base.GetType().GetMethod(method);
			methodInfo.Invoke(this, null);
		}

		public void CheckUserLogin()
		{
			int flag = 0;
			Chain.Model.SysUser User = new Chain.Model.SysUser();
			string data;
			try
			{
				string username = this.Request["Account"].ToString();
				string password = this.Request["password"].ToString();
				User = PubFunction.CheckUserLogin(username.Trim(), password);
				if (User != null)
				{
					flag = 2;
					Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(User.UserShopID);
					if (!modelShop.ShopState)
					{
						if (!User.UserLock)
						{
							flag = 1;
							this.Session["UID"] = User.UserID;
							this.Session["UserAccount"] = User.UserAccount;
							this.Session["UserName"] = User.UserName;
							this.Session["UserGroupID"] = User.UserGroupID;
							this.Session["UserShopID"] = User.UserShopID;
							OnlineBiz.Add(User.UserID, this.Session.SessionID, this.Request.UserHostAddress, this.Request.UserAgent);
							PubFunction.SysUpdateMemIsPast();
							PubFunction.SaveSysLog(User.UserID, 4, "系统登录", string.Concat(new string[]
							{
								"系统登录,账号：[",
								User.UserAccount,
								"] 姓名：[",
								User.UserName,
								"]"
							}), User.UserShopID, DateTime.Now, PubFunction.ipAdress);
						}
					}
				}
			}
			catch (Exception er)
			{
				flag = 3;
				data = "'" + er.Message.ToString() + "'";
			}
			if (flag == 1)
			{
				string model = string.Format("UserID:'{0}',UserAccount:'{1}',UserName:'{2}',UserShopID:'{3}',UserGroupID:'{4}',UserLock:'{5}'", new object[]
				{
					User.UserID,
					User.UserAccount,
					User.UserName,
					User.UserShopID,
					User.UserGroupID,
					User.UserLock
				});
				data = "{" + model + "}";
			}
			else
			{
				data = "''";
			}
			this.ResponseWrite(flag == 1, flag, data);
		}

		public void MemAdd()
		{
			int flag = 0;
			string arg_53_0 = (this.Request["MemID"] != null && this.Request["MemID"] != "") ? this.Request["MemID"].ToString() : "";
			try
			{
				string strMemCard = (this.Request["MemCard"].ToString() != "") ? this.Request["MemCard"].ToString() : "";
				string strMemName = (this.Request["MemName"].ToString() != "") ? this.Request["MemName"].ToString() : "";
				int intMemSex = (this.Request["MemSex"].ToString() != "") ? int.Parse(this.Request["MemSex"].ToString()) : 0;
				string strMemBirthday = (this.Request["MemBirthday"].ToString() != "") ? this.Request["MemBirthday"].ToString() : "1900-1-1";
				string strMemMobile = (this.Request["MemMobile"].ToString() != "") ? this.Request["MemMobile"].ToString() : "";
				string strMemPassword = (this.Request["MemPassword"].ToString() != "") ? this.Request["MemPassword"].ToString() : "";
				string strMemRePassword = (this.Request["MemRePassword"].ToString() != "") ? this.Request["MemRePassword"].ToString() : "";
				int strMemRecommendCardInfoID = (this.Request["MemRecommendCardInfoID"].ToString() != "") ? int.Parse(this.Request["MemRecommendCardInfoID"].ToString()) : 0;
				string strMemCreateTime = string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
				string strPastTime = "2900-1-1";
				string strRemark = string.Concat(new string[]
				{
					"会员登记,会员卡号：[",
					strMemCard,
					"],姓名：[",
					strMemName,
					"],会员等级：[",
					PubFunction.LevelIDToName(0),
					"]"
				});
				int MemShopID = (this.Request["MemShopID"].ToString() != "") ? int.Parse(this.Request["MemShopID"].ToString()) : 1;
				int MemUserID = (this.Request["MemUserID"].ToString() != "") ? int.Parse(this.Request["MemUserID"].ToString()) : 1;
				if (strMemPassword != strMemRePassword)
				{
					flag = -4;
				}
				else
				{
					Chain.Model.Mem modelMem = new Chain.Model.Mem();
					modelMem.MemCard = strMemCard.Trim();
					modelMem.MemName = strMemName.Trim();
					modelMem.MemPassword = DESEncrypt.Encrypt("");
					modelMem.MemSex = PubFunction.SetMemSex(intMemSex);
					modelMem.MemIdentityCard = "";
					DateTime dteMemBirthday = default(DateTime);
					DateTime.TryParse(strMemBirthday, out dteMemBirthday);
					modelMem.MemBirthday = dteMemBirthday;
					modelMem.MemBirthdayType = true;
					modelMem.MemMobile = strMemMobile.Trim();
					modelMem.MemPoint = 0;
					modelMem.MemPointAutomatic = true;
					modelMem.MemMoney = 0m;
					modelMem.MemConsumeMoney = 0m;
					modelMem.MemEmail = "";
					modelMem.MemAddress = "";
					modelMem.MemState = 0;
					modelMem.MemLevelID = 0;
					modelMem.MemShopID = MemShopID;
					modelMem.MemRecommendID = strMemRecommendCardInfoID;
					DateTime dteMemPastTime = default(DateTime);
					DateTime.TryParse(strPastTime, out dteMemPastTime);
					modelMem.MemPastTime = dteMemPastTime;
					if (dteMemPastTime > DateTime.Now)
					{
						modelMem.MemIsPast = false;
					}
					modelMem.MemPhoto = "";
					DateTime dteMemCreateTime = default(DateTime);
					DateTime.TryParse(strMemCreateTime, out dteMemCreateTime);
					modelMem.MemCreateTime = dteMemCreateTime;
					modelMem.MemRemark = "";
					modelMem.MemUserID = MemUserID;
					modelMem.MemTelePhone = "";
					modelMem.MemQRCode = "";
					modelMem.MemProvince = "";
					modelMem.MemCity = "";
					modelMem.MemCounty = "";
					modelMem.MemVillage = "";
					modelMem.MemPassword = DESEncrypt.Encrypt(strMemPassword.Trim());
					Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
					if (flag >= 0)
					{
						flag = bllMem.Add(modelMem);
					}
					if (flag > 0)
					{
						DataRow[] drs = new Chain.BLL.MemCustomField().CustomGetList(" CustomType=1 ");
						Hashtable hash = new Hashtable();
						if (drs.Length > 0)
						{
							DataRow[] array = drs;
							for (int i = 0; i < array.Length; i++)
							{
								DataRow dr = array[i];
								hash.Add(dr["CustomField"].ToString(), (this.Request["Mem_Custom_" + dr["CustomField"].ToString()] != null) ? this.Request["Mem_Custom_" + dr["CustomField"].ToString()].ToString() : "");
							}
							bllMem.AddCustomField(modelMem.MemCard, hash);
						}
						this.MemRecommendPoint(modelMem, modelMem.MemShopID, modelMem.MemUserID);
						if (modelMem.MemMobile != "")
						{
							if (PubFunction.curParameter.bolSms && PubFunction.curParameter.bolAutoSendSMSByMemRegister)
							{
								if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
								{
									flag = -3;
								}
								else
								{
									string strSendContent = SMSInfo.GetSendContent(1, new SmsTemplateParameter
									{
										strCardID = modelMem.MemCard,
										strName = modelMem.MemName,
										dclMoney = 0m,
										dclTempMoney = 0m,
										intTempPoint = 0,
										intPoint = 0,
										OldLevelID = 0,
										NewLevelID = 0
									}, modelMem.MemShopID);
									SMSInfo.Send_GXSMS(false, modelMem.MemMobile, strSendContent, "");
									Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
									modelSms.SmsMemID = modelMem.MemID;
									modelSms.SmsMobile = modelMem.MemMobile;
									modelSms.SmsContent = strSendContent;
									modelSms.SmsTime = DateTime.Now;
									modelSms.SmsShopID = modelMem.MemShopID;
									modelSms.SmsUserID = modelMem.MemUserID;
									modelSms.SmsAmount = PubFunction.GetSmsAmount(strSendContent);
									modelSms.SmsAllAmount = modelSms.SmsAmount;
									Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
									bllSms.Add(modelSms);
								}
							}
						}
						PubFunction.SaveSysLog(0, 1, "会员登记", strRemark, 0, DateTime.Now, PubFunction.ipAdress);
					}
				}
			}
			catch (Exception er)
			{
				flag = 0;
				string error = er.Message;
			}
			this.ResponseWrite(flag > 0 || flag == -3, flag, "''");
		}

		public void MemRecommendPoint(Chain.Model.Mem modelMem, int ShopID, int Uid)
		{
			if (modelMem.MemRecommendID > 0 && PubFunction.curParameter.intRecommendPoint > 0)
			{
				int point = PubFunction.curParameter.intRecommendPoint;
				if (PubFunction.IsShopPoint(ShopID, ref point))
				{
					Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
					Chain.Model.Mem MemRecommend = bllMem.GetModel(modelMem.MemRecommendID);
					Chain.Model.PointLog modelPontLog = new Chain.Model.PointLog();
					Chain.BLL.PointLog bllPontLog = new Chain.BLL.PointLog();
					modelPontLog.PointMemID = modelMem.MemRecommendID;
					modelPontLog.PointNumber = PubFunction.curParameter.intRecommendPoint;
					modelPontLog.PointShopID = modelMem.MemShopID;
					modelPontLog.PointUserID = modelMem.MemUserID;
					modelPontLog.PointChangeType = 7;
					modelPontLog.PointCreateTime = DateTime.Now;
					modelPontLog.PointGiveMemID = modelMem.MemID;
					modelPontLog.PointRemark = string.Concat(new object[]
					{
						"推荐会员获得积分,推荐卡号:[",
						modelMem.MemCard,
						"] 姓名:[",
						modelMem.MemName,
						"] 获得积分：[",
						modelPontLog.PointNumber,
						"]"
					});
					bllPontLog.Add(modelPontLog);
					if (PubFunction.curParameter.bolShopPointManage)
					{
						PubFunction.SetShopPoint(Uid, ShopID, point, "会员添加,推荐会员获取积分,商家扣除积分", 2);
					}
					int intSuccess = bllMem.UpdatePoint(modelPontLog.PointMemID, modelPontLog.PointNumber);
					if (intSuccess > 0)
					{
						PubFunction.UpdateMemLevel(bllMem.GetModel(modelPontLog.PointMemID));
					}
				}
			}
		}

		public void PointChange()
		{
			int flag = 0;
			int intUserID = (this.Request["MemUserID"] != null) ? int.Parse(this.Request["MemUserID"].ToString()) : 0;
			int intUserShopID = (this.Request["UserShopID"] != null) ? int.Parse(this.Request["UserShopID"].ToString()) : 0;
			int intMemID = int.Parse(this.Request["memID"].ToString());
			int intPointNumber = (this.Request["PointChangeNumber"] != null) ? int.Parse(this.Request["PointChangeNumber"]) : 0;
			string strPointOrderCode = this.Request["PointChangeCode"].ToString();
			bool sendMSM = this.Request["sendSMS"] == "true";
			string result = "";
			try
			{
				Chain.Model.PointLog modelPontLog = new Chain.Model.PointLog();
				Chain.BLL.PointLog bllPontLog = new Chain.BLL.PointLog();
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = bllMem.GetModel(intMemID);
				int intLevelID = modelMem.MemLevelID;
				if (modelMem.MemState != 0)
				{
					flag = -1;
				}
				else
				{
					if (!PubFunction.IsShopPoint(intUserShopID, ref intPointNumber))
					{
						flag = -8;
						this.Context.Response.Write(flag);
						return;
					}
					string strRemark = (this.Request["PointChangeRemark"] != "") ? this.Request["PointChangeRemark"].ToString() : "无";
					string strType = this.Request["paytype"];
					modelPontLog.PointMemID = intMemID;
					modelPontLog.PointNumber = intPointNumber;
					modelPontLog.PointShopID = intUserShopID;
					modelPontLog.PointUserID = intUserID;
					modelPontLog.PointChangeType = 6;
					modelPontLog.PointCreateTime = DateTime.Now;
					modelPontLog.PointOrderCode = strPointOrderCode;
					if (strType == null)
					{
						flag = -4;
					}
					else
					{
						if (strType == "reduce")
						{
							if (modelMem.MemPoint > modelPontLog.PointNumber)
							{
								modelPontLog.PointNumber *= -1;
								modelPontLog.PointRemark = string.Concat(new object[]
								{
									"手动进行减去积分,积分变动：[",
									modelPontLog.PointNumber,
									"],备注：",
									strRemark
								});
								flag = bllPontLog.Add(modelPontLog);
							}
							else
							{
								flag = -3;
							}
						}
						else
						{
							modelPontLog.PointRemark = string.Concat(new object[]
							{
								"手动进行增加积分,积分变动：[",
								modelPontLog.PointNumber,
								"],备注：",
								strRemark
							});
							flag = bllPontLog.Add(modelPontLog);
						}
						if (flag > 0)
						{
							int intSuccess = new Chain.BLL.Mem().UpdatePoint(intMemID, modelPontLog.PointNumber);
							if (intSuccess > 0)
							{
								modelMem = new Chain.BLL.Mem().GetModel(intMemID);
								string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
								if (modelMem.MemMobile != "")
								{
									if (sendMSM)
									{
										if (Convert.ToInt32(SMSInfo.GetBalance(false)) > 0)
										{
											if (PubFunction.IsCanSendSms(intUserShopID, modelMem.MemMobile.Split(new char[]
											{
												','
											}).Length))
											{
												SmsTemplateParameter smsTemplateParameter = new SmsTemplateParameter();
												smsTemplateParameter.strCardID = modelMem.MemCard;
												smsTemplateParameter.strName = modelMem.MemName;
												smsTemplateParameter.intTempPoint = intPointNumber;
												smsTemplateParameter.intPoint = modelMem.MemPoint;
												smsTemplateParameter.dclTempMoney = 0m;
												smsTemplateParameter.dclMoney = modelMem.MemMoney;
												smsTemplateParameter.OldLevelID = intLevelID;
												modelMem = new Chain.BLL.Mem().GetModel(intMemID);
												smsTemplateParameter.NewLevelID = modelMem.MemLevelID;
												smsTemplateParameter.MemBirthday = modelMem.MemBirthday;
												smsTemplateParameter.MemPastTime = modelMem.MemPastTime;
												string strSendContent = SMSInfo.GetSendContent(7, smsTemplateParameter, intUserShopID);
												SMSInfo.Send_GXSMS(false, modelMem.MemMobile, strSendContent, "");
												Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
												modelSms.SmsMemID = modelMem.MemID;
												modelSms.SmsMobile = modelMem.MemMobile;
												modelSms.SmsContent = strSendContent;
												modelSms.SmsTime = DateTime.Now;
												modelSms.SmsShopID = intUserShopID;
												modelSms.SmsUserID = intUserID;
												modelSms.SmsAmount = PubFunction.GetSmsAmount(strSendContent);
												modelSms.SmsAllAmount = modelSms.SmsAmount;
												Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
												bllSms.Add(modelSms);
											}
											else
											{
												flag = -2;
											}
										}
									}
								}
								PubFunction.SaveSysLog(intUserID, 3, "积分变动", string.Concat(new object[]
								{
									"会员积分变动,会员卡号：[",
									modelMem.MemCard,
									"],会员姓名：[",
									modelMem.MemName,
									"],变动积分：[",
									modelPontLog.PointNumber,
									"],备注：",
									strRemark
								}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
							}
						}
					}
				}
			}
			catch (Exception e_567)
			{
				flag = -1;
			}
			this.ResponseWrite(flag > 0, flag, "[{" + result + "}]");
		}

		public void PointChangeCode()
		{
			string OrderCode = PubFunction.curParameter.strMemPointChangePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
			this.ResponseWrite(true, 1, "'" + OrderCode + "'");
		}

		public void GetMemByCard()
		{
			int flag = 0;
			string result = "''";
			string strMemCard = (this.Request["MemCard"] != null && this.Request["MemCard"] != "") ? this.Request["MemCard"].ToString().Trim() : "";
			try
			{
				StringBuilder strSql = new StringBuilder();
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				string strCustomField = "";
				DataTable dt = new Chain.BLL.MemCustomField().GetList(" CustomType=1").Tables[0];
				foreach (DataRow dr in dt.Rows)
				{
					strCustomField = strCustomField + dr["CustomField"].ToString() + ",";
				}
				strSql.Append(" 1=1 ");
				if (strMemCard != "")
				{
					strSql.AppendFormat(" and (MemCard='{0}' or MemName ='{0}' or  MemMobile='{0}' or  MemCardNumber='{0}')", strMemCard);
				}
				strSql.Append(" and Mem.MemShopID = SysShop.ShopID and Mem.MemLevelID = MemLevel.LevelID and Mem.MemUserID=SysUser.UserID");
				strSql.Append(" and Mem.MemShopID =SysShopMemLevel.ShopID and SysShopMemLevel.MemLevelID=MemLevel.LevelID ");
				strSql.Append(" and Mem.MemID>0");
				int counts = 0;
				DataTable dtMem = bllMem.GetListSP(20, 1, out counts, new string[]
				{
					strSql.ToString()
				}).Tables[0];
				if (dtMem != null && dtMem.Rows.Count > 0)
				{
					result = JsonPlus.ToJson(dtMem, "MemID,MemCard,MemName,MemSex,MemIdentityCard,MemMobile,MemPhoto,MemBirthday,MemIsPast,MemPastTime,MemPoint,MemMoney,MemConsumeMoney,MemConsumeLastTime,MemConsumeCount,MemEmail,MemAddress,MemState,MemRecommendID,MemCreateTime,MemRemark,MemLevelID,MemShopID,MemUserID,ShopName," + strCustomField + "LevelID,LevelName,LevelPoint,LevelDiscountPercent,LevelPointPercent,UserName,MemTelePhone,MemQRCode,MemProvinceName,MemCityName,MemCountyName,MemVillageName,ClassPointPercent");
					flag = 1;
				}
				else
				{
					flag = -1;
				}
			}
			catch
			{
				flag = 0;
			}
			this.ResponseWrite(flag > 0, flag, result);
		}

		public void GetSysParameter()
		{
			int flag = 0;
			string result = "''";
			try
			{
				StringBuilder strSql = new StringBuilder();
				Chain.BLL.SysParameter bll = new Chain.BLL.SysParameter();
				Chain.Model.SysParameter para = bll.GetModel(1);
				if (para != null)
				{
					result = this.getProperties<Chain.Model.SysParameter>(para);
					flag = 1;
				}
				else
				{
					flag = -1;
				}
			}
			catch
			{
				flag = 0;
			}
			this.ResponseWrite(flag > 0, flag, "[{" + result + "}]");
		}

		public void GetPageVisit()
		{
			int flag = 0;
			try
			{
				int UserGroupID = (this.Request["UserGroupID"] != null) ? int.Parse(this.Request["UserGroupID"].ToString()) : 0;
				int PageID = (this.Request["PageID"] != null) ? int.Parse(this.Request["PageID"].ToString()) : 0;
				DataTable dt = PubFunction.GetGroupAuthority(UserGroupID);
				if (PubFunction.GetPageVisit(dt, PageID))
				{
					flag = 1;
				}
				else
				{
					flag = -1;
				}
			}
			catch
			{
				flag = 0;
			}
			this.ResponseWrite(flag > 0, flag, "''");
		}

		public void CheckMemPwd()
		{
			string strAccount = (this.Request["strAccount"] != null && this.Request["strAccount"] != "") ? this.Request["strAccount"].ToString().Trim() : "";
			string strPassword = (this.Request["strPassword"] != null && this.Request["strPassword"] != "") ? this.Request["strPassword"].ToString().Trim() : "";
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			Chain.Model.Mem modelMem = bllMem.CheckMemPwd(strAccount.Trim(), DESEncrypt.Encrypt(strPassword.Trim()));
			int flag;
			if (modelMem != null)
			{
				flag = 1;
			}
			else
			{
				flag = -1;
			}
			this.ResponseWrite(true, flag, "''");
		}

		public void Expense()
		{
			int flag = -1;
			int intUserID = (this.Request["MemUserID"] != null) ? int.Parse(this.Request["MemUserID"].ToString()) : 0;
			int intUserShopID = (this.Request["UserShopID"] != null) ? int.Parse(this.Request["UserShopID"].ToString()) : 0;
			int intMemID = (this.Request["MemID"] != null) ? int.Parse(this.Request["MemID"].ToString()) : 0;
			int intPoint = (this.Request["txtExpensePoint"].ToString() != "") ? int.Parse(this.Request["txtExpensePoint"].ToString()) : 0;
			bool chkNoMember = this.Request["NoMember"].ToString() == "true";
			decimal dclMoney = (this.Request["txtExpenseNeedPay"].ToString() != "") ? decimal.Parse(this.Request["txtExpenseNeedPay"].ToString()) : 0m;
			decimal dclDiscountMoney = decimal.Parse(this.Request["txtExpenseDiscountPay"].ToString());
			decimal dclCardPayMoney = (this.Request["txtExpenseCardPay"] != "0") ? decimal.Parse(this.Request["txtExpenseCardPay"]) : 0m;
			decimal dclCashPayMoney = (this.Request["txtExpenseCashPay"] != "0") ? decimal.Parse(this.Request["txtExpenseCashPay"]) : 0m;
			decimal dclBinkPayMoney = 0m;
			decimal dclCouponPayMoney = 0m;
			string strOrderAccount = (this.Request["OrderCode"].ToString() != "") ? this.Request["OrderCode"].ToString() : "";
			string strRemark = this.Request["txtExpenseRemark"].ToString();
			bool bolIsCard = dclCardPayMoney > 0m;
			bool bolIsCash = dclCashPayMoney > 0m;
			bool bolIsBink = dclBinkPayMoney > 0m;
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			Chain.Model.Mem modelMem = bllMem.GetModel(intMemID);
			int intLevelID = modelMem.MemLevelID;
			string Remark;
			if (intMemID != 0)
			{
				Remark = string.Concat(new object[]
				{
					"会员快速消费,会员卡号：[",
					modelMem.MemCard,
					"],姓名：[",
					modelMem.MemName,
					"],订单号：[",
					strOrderAccount,
					"],消费金额：[",
					dclDiscountMoney,
					"],获得积分：[",
					intPoint,
					"],备注：",
					strRemark
				});
			}
			else
			{
				Remark = string.Concat(new object[]
				{
					"散客快速消费,订单号：[",
					strOrderAccount,
					"],消费金额：[",
					dclDiscountMoney,
					"],备注：",
					strRemark
				});
			}
			if (PubFunction.IsShopPoint(intUserShopID, ref intPoint) || chkNoMember)
			{
				try
				{
					Chain.Model.OrderLog mdOrderLog = new Chain.Model.OrderLog();
					Chain.Model.SysLog mdSysLog = new Chain.Model.SysLog();
					Chain.Model.PointLog mdPoint = new Chain.Model.PointLog();
					if (!chkNoMember)
					{
						mdOrderLog.OrderMemID = intMemID;
						mdOrderLog.OrderType = 0;
						mdOrderLog.OrderPoint = intPoint;
						mdOrderLog.OrderTotalMoney = dclMoney;
						mdOrderLog.OrderDiscountMoney = dclDiscountMoney;
						mdOrderLog.OrderIsCard = bolIsCard;
						mdOrderLog.OrderPayCard = dclCardPayMoney;
						mdOrderLog.OrderIsCash = bolIsCash;
						mdOrderLog.OrderPayCash = dclCashPayMoney;
						mdOrderLog.OrderIsBink = bolIsBink;
						mdOrderLog.OrderPayBink = dclBinkPayMoney;
						mdOrderLog.OrderPayCoupon = dclCouponPayMoney;
						mdOrderLog.OrderAccount = strOrderAccount;
						mdOrderLog.OrderShopID = intUserShopID;
						mdOrderLog.OrderUserID = intUserID;
						mdOrderLog.OrderRemark = strRemark;
						mdOrderLog.OrderCreateTime = DateTime.Now;
						mdOrderLog.OrderPayType = 0;
						mdOrderLog.OrderCardBalance = modelMem.MemMoney - dclCardPayMoney;
						mdOrderLog.OrderCardPoint = modelMem.MemPoint + intPoint;
						Chain.BLL.OrderLog bllOrder = new Chain.BLL.OrderLog();
						int intSuccess = bllOrder.Add(mdOrderLog, strOrderAccount);
						if (intSuccess > 0)
						{
							decimal dclMemMoney = modelMem.MemMoney - dclCardPayMoney;
							modelMem.MemConsumeMoney += mdOrderLog.OrderDiscountMoney;
							modelMem.MemPoint += mdOrderLog.OrderPoint;
							modelMem.MemConsumeLastTime = DateTime.Now;
							modelMem.MemConsumeCount++;
							int mem = bllMem.ExpenseUpdateMem(intMemID, dclMemMoney, modelMem.MemConsumeMoney, modelMem.MemPoint, DateTime.Now, modelMem.MemConsumeCount);
							Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
							moneyChangeLogModel.MoneyChangeMemID = intMemID;
							moneyChangeLogModel.MoneyChangeUserID = intUserID;
							moneyChangeLogModel.MoneyChangeType = 3;
							moneyChangeLogModel.MoneyChangeAccount = strOrderAccount;
							moneyChangeLogModel.MoneyChangeMoney = -(dclCardPayMoney + dclCashPayMoney + dclBinkPayMoney);
							moneyChangeLogModel.MoneyChangeCash = -dclCashPayMoney;
							moneyChangeLogModel.MoneyChangeBalance = -dclCardPayMoney;
							moneyChangeLogModel.MoneyChangeUnionPay = -dclBinkPayMoney;
							moneyChangeLogModel.MemMoney = modelMem.MemMoney - dclCardPayMoney;
							moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
							moneyChangeLogModel.MoneyChangeGiveMoney = 0m;
							new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
							mdPoint.PointMemID = intMemID;
							mdPoint.PointNumber = intPoint;
							mdPoint.PointChangeType = 2;
							mdPoint.PointRemark = string.Concat(new object[]
							{
								"会员快速消费，消费金额：[",
								dclDiscountMoney,
								"],获得积分：",
								intPoint,
								"]"
							});
							mdPoint.PointShopID = intUserShopID;
							mdPoint.PointCreateTime = DateTime.Now;
							mdPoint.PointUserID = intUserID;
							mdPoint.PointOrderCode = strOrderAccount;
							Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
							bllPoint.Add(mdPoint);
							MEMPointUpdate.MEMPointRate(modelMem, mdOrderLog.OrderPoint, mdOrderLog.OrderAccount, 2, intUserID, intUserShopID);
							modelMem = new Chain.BLL.Mem().GetModel(intMemID);
							string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
							flag = 1;
							if (modelMem.MemMobile != "")
							{
								if (PubFunction.curParameter.bolSms && PubFunction.curParameter.bolAutoSendSMSByFastConsumption)
								{
									if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
									{
										flag = -2;
									}
									else
									{
										SmsTemplateParameter smsTemplateParameter = new SmsTemplateParameter();
										smsTemplateParameter.strCardID = modelMem.MemCard;
										smsTemplateParameter.strName = modelMem.MemName;
										smsTemplateParameter.dclTempMoney = dclMoney;
										smsTemplateParameter.dclMoney = modelMem.MemMoney;
										smsTemplateParameter.intTempPoint = intPoint;
										smsTemplateParameter.intPoint = modelMem.MemPoint;
										smsTemplateParameter.OldLevelID = intLevelID;
										modelMem = new Chain.BLL.Mem().GetModel(intMemID);
										smsTemplateParameter.NewLevelID = modelMem.MemLevelID;
										string strSendContent = SMSInfo.GetSendContent(5, smsTemplateParameter, intUserShopID);
										SMSInfo.Send_GXSMS(false, modelMem.MemMobile, strSendContent, "");
										Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
										modelSms.SmsMemID = modelMem.MemID;
										modelSms.SmsMobile = modelMem.MemMobile;
										modelSms.SmsContent = strSendContent;
										modelSms.SmsTime = DateTime.Now;
										modelSms.SmsShopID = intUserShopID;
										modelSms.SmsUserID = intUserID;
										modelSms.SmsAmount = PubFunction.GetSmsAmount(strSendContent);
										modelSms.SmsAllAmount = modelSms.SmsAmount;
										Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
										bllSms.Add(modelSms);
										PubFunction.SaveSysLog(intUserID, 4, "会员消费-快速消费", string.Concat(new string[]
										{
											"快速消费发短信成功,会员卡号：[",
											modelMem.MemCard,
											"],姓名：[",
											modelMem.MemName,
											"]"
										}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
									}
								}
							}
							PubFunction.SaveSysLog(intUserID, 4, "会员消费", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
						}
					}
					else
					{
						mdOrderLog.OrderMemID = intMemID;
						mdOrderLog.OrderType = 0;
						mdOrderLog.OrderPoint = intPoint;
						mdOrderLog.OrderTotalMoney = dclMoney;
						mdOrderLog.OrderDiscountMoney = dclDiscountMoney;
						mdOrderLog.OrderIsCard = bolIsCard;
						mdOrderLog.OrderPayCard = dclCardPayMoney;
						mdOrderLog.OrderIsCash = bolIsCash;
						mdOrderLog.OrderPayCash = dclCashPayMoney;
						mdOrderLog.OrderIsBink = bolIsBink;
						mdOrderLog.OrderPayBink = dclBinkPayMoney;
						mdOrderLog.OrderPayCoupon = dclCouponPayMoney;
						mdOrderLog.OrderAccount = strOrderAccount;
						mdOrderLog.OrderShopID = intUserShopID;
						mdOrderLog.OrderUserID = intUserID;
						mdOrderLog.OrderRemark = strRemark;
						mdOrderLog.OrderCreateTime = DateTime.Now;
						mdOrderLog.OrderPayType = 0;
						Chain.BLL.OrderLog bllOrder = new Chain.BLL.OrderLog();
						int intSuccess = bllOrder.Add(mdOrderLog);
						if (intSuccess > 0)
						{
							PubFunction.SaveSysLog(intUserID, 4, "会员管理", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
							flag = 2;
						}
					}
				}
				catch (Exception e_99F)
				{
					flag = -1;
				}
			}
			else
			{
				flag = -6;
			}
			this.ResponseWrite(flag > 0 || flag == -2, flag, "''");
		}

		public void Deposit()
		{
			int flag = -1;
			int intUserID = int.Parse(this.Request["MemUserID"].ToString());
			int intUserShopID = int.Parse(this.Request["UserShopID"].ToString());
			int intMemID = (this.Request["MemID"].ToString() != "") ? int.Parse(this.Request["MemID"].ToString()) : 0;
			string rechargeAccount = this.Request["DepositCode"].ToString();
			decimal money = (this.Request["DepositMoney"].ToString() != "") ? decimal.Parse(this.Request["DepositMoney"]) : 0m;
			decimal giveMoney = (this.Request["GiveMoney"].ToString() != "") ? decimal.Parse(this.Request["GiveMoney"]) : 0m;
			string strRemark = (this.Request["DepositRemark"].ToString() != "") ? this.Request["DepositRemark"].ToString() : "无";
			DateTime createTime = DateTime.Parse(this.Request["DepositTime"]);
			bool isbank = this.Request["paytype"] == "union";
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			Chain.Model.Mem modelMem = bllMem.GetModel(intMemID);
			string Remark = string.Concat(new object[]
			{
				"会员账户充值,充值金额：[",
				money.ToString(),
				"],赠送：[",
				giveMoney,
				"],备注：",
				strRemark
			});
			try
			{
				decimal sumMoney = money + giveMoney;
				Chain.Model.MemRecharge mdRechange = new Chain.Model.MemRecharge();
				mdRechange.RechargeMemID = modelMem.MemID;
				mdRechange.RechargeAccount = rechargeAccount;
				mdRechange.RechargeMoney = money + giveMoney;
				mdRechange.RechargeShopID = intUserShopID;
				mdRechange.RechargeUserID = intUserID;
				mdRechange.RechargeCreateTime = createTime;
				mdRechange.RechargeIsApprove = true;
				mdRechange.RechargeRemark = strRemark;
				if (isbank)
				{
					mdRechange.RechargeType = 3;
				}
				else
				{
					mdRechange.RechargeType = 2;
				}
				mdRechange.RechargeGive = giveMoney;
				mdRechange.RechargeCardBalance = modelMem.MemMoney + sumMoney;
				flag = new Chain.BLL.MemRecharge().Add(mdRechange);
				bllMem.UpdateMoney(intMemID, sumMoney);
				Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
				moneyChangeLogModel.MoneyChangeMemID = intMemID;
				moneyChangeLogModel.MoneyChangeUserID = intUserID;
				moneyChangeLogModel.MoneyChangeType = 1;
				moneyChangeLogModel.MoneyChangeAccount = rechargeAccount;
				moneyChangeLogModel.MoneyChangeMoney = sumMoney;
				if (!isbank)
				{
					moneyChangeLogModel.MoneyChangeCash = money;
				}
				else
				{
					moneyChangeLogModel.MoneyChangeUnionPay = money;
				}
				moneyChangeLogModel.MemMoney = modelMem.MemMoney + sumMoney;
				moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
				moneyChangeLogModel.MoneyChangeGiveMoney = giveMoney;
				new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
				if (modelMem.MemMobile != "")
				{
					if (PubFunction.curParameter.bolSms && PubFunction.curParameter.bolAutoSendSMSByMemRecharge)
					{
						if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
						{
							flag = -2;
						}
						else
						{
							string strSendContent = SMSInfo.GetSendContent(2, new SmsTemplateParameter
							{
								strCardID = modelMem.MemCard,
								strName = modelMem.MemName,
								dclTempMoney = money + giveMoney,
								dclMoney = modelMem.MemMoney + money + giveMoney,
								intTempPoint = 0,
								intPoint = modelMem.MemPoint,
								OldLevelID = modelMem.MemLevelID,
								NewLevelID = modelMem.MemLevelID
							}, intUserShopID);
							SMSInfo.Send_GXSMS(false, modelMem.MemMobile, strSendContent, "");
							Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
							modelSms.SmsMemID = modelMem.MemID;
							modelSms.SmsMobile = modelMem.MemMobile;
							modelSms.SmsContent = strSendContent;
							modelSms.SmsTime = DateTime.Now;
							modelSms.SmsShopID = intUserShopID;
							modelSms.SmsUserID = intUserID;
							modelSms.SmsAmount = PubFunction.GetSmsAmount(strSendContent);
							modelSms.SmsAllAmount = modelSms.SmsAmount;
							Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
							bllSms.Add(modelSms);
						}
					}
				}
				PubFunction.SaveSysLog(intUserID, 3, "会员充值", Remark, intUserShopID, createTime, PubFunction.ipAdress);
			}
			catch
			{
				flag = -1;
			}
			this.ResponseWrite(flag > 0 || flag == -2, flag, "''");
		}

		public void ExpenseOrderCode()
		{
			string OrderCode = PubFunction.curParameter.strExpensePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
			this.ResponseWrite(true, 1, "'" + OrderCode + "'");
		}

		public void DepositCode()
		{
			string OrderCode = PubFunction.curParameter.strMemRechargePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
			this.ResponseWrite(true, 1, "'" + OrderCode + "'");
		}

		public void GoodsExpenseCode()
		{
			string OrderCode = PubFunction.curParameter.strGoodsExpensePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
			this.ResponseWrite(true, 1, "'" + OrderCode + "'");
		}

		public void GetShopById()
		{
			string result = "";
			string strShopID = (this.Request["id"] != null && this.Request["id"] != "") ? this.Request["id"].ToString().Trim() : "";
			int ShopId;
			if (int.TryParse(strShopID, out ShopId))
			{
				Chain.Model.SysShop model = new Chain.BLL.SysShop().GetModel(ShopId);
				if (model != null)
				{
					model.ShopRemark = model.ShopRemark.Replace("\n", "<br />");
					result = this.getProperties<Chain.Model.SysShop>(model);
				}
			}
			this.ResponseWrite("SysShops", "[{" + result + "}]", 1);
		}

		public void GetMemLevelById()
		{
			string result = "";
			string strLevelID = (this.Request["id"] != null && this.Request["id"] != "") ? this.Request["id"].ToString().Trim() : "";
			int LevelId;
			if (int.TryParse(strLevelID, out LevelId))
			{
				Chain.Model.MemLevel model = new Chain.BLL.MemLevel().GetModel(LevelId);
				if (model != null)
				{
					result = this.getProperties<Chain.Model.MemLevel>(model);
				}
			}
			this.ResponseWrite("MemLevels", "[{" + result + "}]", 1);
		}

		public void GetMemLevelList()
		{
			string result = "";
			DataTable dtMemLevel = new Chain.BLL.MemLevel().GetList("").Tables[0];
			if (dtMemLevel != null && dtMemLevel.Rows.Count > 0)
			{
				result = JsonPlus.ToJson(dtMemLevel, "LevelID,LevelName,LevelPoint,LevelDiscountPercent,LevelPointPercent,LevellLock");
			}
			this.ResponseWrite("MemLevels", result, dtMemLevel.Rows.Count);
		}

		public void GetOrderLogList()
		{
			string strShopID = (this.Request["ShopID"] != null && this.Request["ShopID"] != "") ? this.Request["ShopID"].ToString().Trim() : "";
			string strMemID = (this.Request["MemID"] != null && this.Request["MemID"] != "") ? this.Request["MemID"].ToString().Trim() : "";
			string strCurrentPage = (this.Request["page"] != null && this.Request["page"] != "") ? this.Request["page"].ToString().Trim() : "";
			string strPageSize = (this.Request["limit"] != null && this.Request["limit"] != "") ? this.Request["limit"].ToString().Trim() : "";
			string result = "{}";
			int shopId = 0;
			int currentPage = 0;
			int pageSize = 0;
			if (int.TryParse(strShopID, out shopId) && int.TryParse(strCurrentPage, out currentPage) && int.TryParse(strPageSize, out pageSize))
			{
				if (shopId > 0 && currentPage > 0)
				{
					string strSql = "";
					strSql += " OrderLog.OrderType <> 3 and OrderLog.OrderType <> 5 and OrderLog.OrderType <>7 ";
					strSql += " and OrderLog.OrderShopID = SysShop.ShopID and OrderLog.OrderMemID = Mem.MemID and OrderLog.OrderUserID = SysUser.UserID";
					strSql = strSql + " and Mem.MemID = " + strMemID;
					strSql = PubFunction.GetShopAuthority(shopId, "OrderShopID", strSql);
					Chain.BLL.OrderLog bllOrderLog = new Chain.BLL.OrderLog();
					int recordCount;
					DataTable dtExpenseHistory = bllOrderLog.GetListSP(pageSize, currentPage, out recordCount, new string[]
					{
						strSql
					}).Tables[0];
					if (dtExpenseHistory != null && dtExpenseHistory.Rows.Count > 0)
					{
						foreach (DataRow item in dtExpenseHistory.Rows)
						{
							item["OrderTotalMoney"] = decimal.Round(decimal.Parse(item["OrderTotalMoney"].ToString()), 2);
							item["OrderDiscountMoney"] = decimal.Round(decimal.Parse(item["OrderDiscountMoney"].ToString()), 2);
						}
						result = JsonPlus.ToJson(dtExpenseHistory, "OrderAccount,OrderMemId,OrderTotalMoney,OrderDiscountMoney,OrderPoint,OrderShopId,OrderCreateTime,OrderUserId,OrderCardBalance,OrderCardPoint,MemName,MemCard,ShopName,UserName");
					}
					this.ResponseWrite("OrderLogs", result, recordCount);
				}
			}
		}

		public void GetDepositList()
		{
			string strShopID = (this.Request["ShopID"] != null && this.Request["ShopID"] != "") ? this.Request["ShopID"].ToString().Trim() : "";
			string strMemID = (this.Request["MemID"] != null && this.Request["MemID"] != "") ? this.Request["MemID"].ToString().Trim() : "";
			string strCurrentPage = (this.Request["page"] != null && this.Request["page"] != "") ? this.Request["page"].ToString().Trim() : "";
			string strPageSize = (this.Request["limit"] != null && this.Request["limit"] != "") ? this.Request["limit"].ToString().Trim() : "";
			string result = "{}";
			int shopId = 0;
			int currentPage = 0;
			int pageSize = 0;
			if (int.TryParse(strShopID, out shopId) && int.TryParse(strCurrentPage, out currentPage) && int.TryParse(strPageSize, out pageSize))
			{
				if (shopId > 0 && currentPage > 0)
				{
					Chain.BLL.MemRecharge bllMemRecharge = new Chain.BLL.MemRecharge();
					string strSql = "";
					strSql += " MemRecharge.RechargeShopID = SysShop.ShopID and MemRecharge.RechargeMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and MemRecharge.RechargeUserID = SysUser.UserID";
					strSql = strSql + " and Mem.MemID = " + strMemID;
					strSql = PubFunction.GetShopAuthority(shopId, "RechargeShopID", strSql);
					int recordCount;
					DataTable db = bllMemRecharge.GetListSP(pageSize, currentPage, out recordCount, new string[]
					{
						strSql
					}).Tables[0];
					if (db != null && db.Rows.Count > 0)
					{
						foreach (DataRow item in db.Rows)
						{
							item["RechargeMoney"] = decimal.Round(decimal.Parse(item["RechargeMoney"].ToString()), 2);
							item["RechargeOrdMoney"] = decimal.Round(decimal.Parse(item["RechargeOrdMoney"].ToString()), 2);
							item["RechargeGive"] = decimal.Round(decimal.Parse(item["RechargeGive"].ToString()), 2);
						}
						result = JsonPlus.ToJson(db, "RechargeType,RechargeAccount,RechargeMoney,RechargeOrdMoney,RechargeGive,RechargeCardBalance,RechargeRemark,ShopName,RechargeCreateTime,UserName");
					}
					this.ResponseWrite("DepositLogs", result, recordCount);
				}
			}
		}

		public void GetGoodsClass()
		{
			string strShopID = (this.Request["ShopID"] != null && this.Request["ShopID"] != "") ? this.Request["ShopID"].ToString().Trim() : "";
			string result = "{}";
			Chain.BLL.GoodsClass gdClass = new Chain.BLL.GoodsClass();
			DataTable dtClass = gdClass.GetListByShopID(Convert.ToInt32(strShopID)).Tables[0];
			DataTable dtResult = new DataTable();
			gdClass.CreateTreeItem(dtClass, dtResult, 0, 0);
			if (dtResult != null && dtResult.Rows.Count > 0)
			{
				result = JsonPlus.ToJson(dtResult, "ClassID,ClassName,ParentID,ClassRemark,ShopID");
			}
			this.ResponseWrite("GoodsClassList", result, dtResult.Rows.Count);
		}

		public void GetGoodsList()
		{
			string result = "{}";
			int resCount = 0;
			Chain.BLL.Goods bllGoods = new Chain.BLL.Goods();
			try
			{
				int intSize = (this.Request["limit"].ToString() != "") ? int.Parse(this.Request["limit"].ToString()) : 0;
				int intIndex = (this.Request["page"].ToString() != "") ? int.Parse(this.Request["page"].ToString()) : 0;
				int intShopID = (this.Request["ShopID"] != "") ? int.Parse(this.Request["ShopID"].ToString()) : -1;
				bool bolGoodsExpense = true;
				bool isCheckStock = true;
				int intGoodsClass = (this.Request["ClassID"] == null || this.Request["ClassID"].ToString() == "") ? 0 : int.Parse(this.Request["ClassID"].ToString());
				int memLevelID = (this.Request["MemLevelID"] == null) ? -1 : int.Parse(this.Request["MemLevelID"]);
				string strGoodsIdList = (this.Request["GoodsIdList"] == null) ? "" : this.Request["GoodsIdList"].ToString().Trim().Trim(new char[]
				{
					','
				});
				StringBuilder sbWhere = new StringBuilder();
				sbWhere.Append(" 1=1 ");
				if (!bolGoodsExpense)
				{
					sbWhere.Append(" and Goods.GoodsType=0");
				}
				if (intGoodsClass != 0)
				{
					sbWhere.AppendFormat(" and Goods.GoodsClassID in ({0}) ", PubFunction.GetClassID(intGoodsClass));
				}
				if (isCheckStock)
				{
					sbWhere.Append("and ((GoodsNumber.Number>0 and Goods.GoodsType=0) or Goods.GoodsType=1) ");
				}
				DataTable dtGoods = bllGoods.GetGoodsStockList(intShopID, memLevelID, strGoodsIdList, intSize, intIndex, out resCount, new string[]
				{
					sbWhere.ToString()
				}).Tables[0];
				if (dtGoods != null)
				{
					result = JsonPlus.ToJson(dtGoods, "GoodsID,Name,Price,GoodsBidPrice,Number,GoodsCode,Point,MinPercent,SalePercet,GoodsType,NameCode,ShopName,ClassDiscountPercent,ClassPointPercent,GoodsDiscount,PointDiscount,DiscountPrice,DiscountPoint");
				}
			}
			catch
			{
			}
			this.ResponseWrite("Goodslist", result, resCount);
		}

		public void GetNoticeList()
		{
			string strCurrentPage = (this.Request["page"] != null && this.Request["page"] != "") ? this.Request["page"].ToString().Trim() : "";
			string strPageSize = (this.Request["limit"] != null && this.Request["limit"] != "") ? this.Request["limit"].ToString().Trim() : "";
			string result = "{}";
			int currentPage = 0;
			int pageSize = 0;
			if (int.TryParse(strCurrentPage, out currentPage) && int.TryParse(strPageSize, out pageSize))
			{
				if (currentPage > 0)
				{
					Chain.BLL.SysNotice bllNotice = new Chain.BLL.SysNotice();
					int recordCount;
					DataTable db = bllNotice.GetListSP(pageSize, currentPage, out recordCount, new string[]
					{
						""
					}).Tables[0];
					if (db != null && db.Rows.Count > 0)
					{
						foreach (DataRow item in db.Rows)
						{
							item["SysNoticeDetail"] = item["SysNoticeDetail"].ToString().Replace("\n", "<br />");
						}
						result = JsonPlus.ToJson(db, "SysNoticeID,SysNoticeCode,SysNotieceName,SysNoticeTitle,SysNoticeDetail,SysNoticeTime");
					}
					this.ResponseWrite("NoticeList", result, recordCount);
				}
			}
		}

		public void GoodsExpense()
		{
			int flag = 0;
			string Remark = "";
			try
			{
				int intUserID = (this.Request["intUserID"] == null && this.Request["intUserID"] == "") ? 0 : int.Parse(this.Request["intUserID"].ToString().Trim());
				int intUserShopID = (this.Request["intUserShopID"] == null && this.Request["intUserShopID"] == "") ? 0 : int.Parse(this.Request["intUserShopID"].ToString().Trim());
				int memID = (this.Request["memID"] == null && this.Request["memID"] == "") ? 0 : int.Parse(this.Request["memID"].ToString().Trim());
				int memLevelId = (this.Request["memLevelId"] == null && this.Request["memLevelId"] == "") ? -1 : int.Parse(this.Request["memLevelId"].ToString().Trim());
				int orderID = (this.Request["orderID"] == null || this.Request["orderID"].ToString() == "") ? 0 : int.Parse(this.Request["orderID"].ToString());
				string strOrderCode = this.Request["strOrderCode"].ToString();
				DateTime dtExTime = DateTime.Parse(this.Request["expensetime"].ToString());
				string strOrderType = this.Request["strOrderType"];
				decimal dclDiscountMoney = decimal.Parse(this.Request["dclDiscountMoney"]);
				decimal dclCardPayMoney = (this.Request["dclCardPayMoney"] != "0") ? decimal.Parse(this.Request["dclCardPayMoney"]) : 0m;
				decimal dclCashPayMoney = (this.Request["dclCashPayMoney"] != "0") ? decimal.Parse(this.Request["dclCashPayMoney"]) : 0m;
				decimal dclBinkPayMoney = 0m;
				decimal dclCouponPayMoney = 0m;
				decimal dclTotalMoney = decimal.Parse(this.Request["dclTotalMoney"]);
				int intPoint = int.Parse(this.Request["intPoint"]);
				string strRemark = "";
				strRemark = PubFunction.RemoveSpace(strRemark);
				int intCount = int.Parse(this.Request["intCount"]);
				string GoodsListId = this.Request["GoodsListId"];
				string GoodsListCount = this.Request["GoodsListCount"];
				bool bolIsCard = dclCardPayMoney > 0m;
				bool bolIsCash = dclCashPayMoney > 0m;
				bool bolIsBink = dclBinkPayMoney > 0m;
				bool bolIsEmptyBills = false;
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = bllMem.GetModel(memID);
				int intLevelID = modelMem.MemLevelID;
				if (memID != 0 && strOrderType != "EmptyBills")
				{
					Remark = string.Concat(new object[]
					{
						"会员商品消费,会员卡号：[",
						modelMem.MemCard,
						"],姓名：[",
						modelMem.MemName,
						"],订单号：[",
						strOrderCode,
						"],消费金额：[",
						dclDiscountMoney,
						"],获得积分：[",
						intPoint,
						"],备注：",
						strRemark
					});
				}
				else if (memID != 0 && strOrderType == "EmptyBills")
				{
					Remark = string.Concat(new string[]
					{
						"会员挂单,会员卡号：[",
						modelMem.MemCard,
						"],姓名：[",
						modelMem.MemName,
						"],订单号：[",
						strOrderCode,
						"],备注：",
						strRemark
					});
				}
				else
				{
					Remark = string.Concat(new object[]
					{
						"散客商品消费,订单号：[",
						strOrderCode,
						"],消费金额：[",
						dclDiscountMoney,
						"],备注：",
						strRemark
					});
				}
				if (PubFunction.IsShopPoint(intUserShopID, ref intPoint))
				{
					int intOrderLogAdd = 0;
					Chain.Model.OrderLog modelOrderLog = new Chain.Model.OrderLog();
					modelOrderLog.OrderAccount = strOrderCode;
					modelOrderLog.OrderMemID = memID;
					modelOrderLog.OrderType = 2;
					modelOrderLog.OrderTotalMoney = dclTotalMoney;
					modelOrderLog.OrderDiscountMoney = dclDiscountMoney;
					modelOrderLog.OrderIsCard = bolIsCard;
					modelOrderLog.OrderPayCard = dclCardPayMoney;
					modelOrderLog.OrderIsCash = bolIsCash;
					modelOrderLog.OrderPayCash = dclCashPayMoney;
					modelOrderLog.OrderIsBink = bolIsBink;
					modelOrderLog.OrderPayBink = dclBinkPayMoney;
					modelOrderLog.OrderPayCoupon = dclCouponPayMoney;
					modelOrderLog.OrderPoint = intPoint;
					modelOrderLog.OrderRemark = strRemark;
					modelOrderLog.OrderPayType = 0;
					modelOrderLog.OrderShopID = intUserShopID;
					modelOrderLog.OrderUserID = intUserID;
					modelOrderLog.OrderCreateTime = dtExTime;
					modelOrderLog.OldAccount = "";
					if (strOrderType == "EmptyBills")
					{
						modelOrderLog.OrderType = 3;
					}
					Chain.BLL.OrderLog bllOrderLog = new Chain.BLL.OrderLog();
					if (!bolIsEmptyBills)
					{
						intOrderLogAdd = bllOrderLog.Add(modelOrderLog, strOrderCode);
					}
					else
					{
						modelOrderLog.OrderID = orderID;
						intOrderLogAdd = bllOrderLog.Update(modelOrderLog);
					}
					if (intOrderLogAdd == 0)
					{
						flag = -3;
					}
					if (intOrderLogAdd > 0 && memID != 0 && strOrderType != "EmptyBills")
					{
						Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
						moneyChangeLogModel.MoneyChangeMemID = modelMem.MemID;
						moneyChangeLogModel.MoneyChangeUserID = intUserID;
						moneyChangeLogModel.MoneyChangeType = 12;
						moneyChangeLogModel.MoneyChangeAccount = strOrderCode;
						moneyChangeLogModel.MoneyChangeMoney = -(dclCardPayMoney + dclCashPayMoney + dclBinkPayMoney);
						moneyChangeLogModel.MoneyChangeBalance = -dclCardPayMoney;
						moneyChangeLogModel.MoneyChangeCash = -dclCashPayMoney;
						moneyChangeLogModel.MoneyChangeUnionPay = -dclBinkPayMoney;
						moneyChangeLogModel.MemMoney = modelMem.MemMoney - dclCardPayMoney;
						moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
						moneyChangeLogModel.MoneyChangeGiveMoney = 0m;
						new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
					}
					int intLog = 0;
					int goodLog = 0;
					if (intOrderLogAdd > 0)
					{
						Chain.Model.GoodsLog modelGoodsLog = new Chain.Model.GoodsLog();
						modelGoodsLog.GoodsAccount = strOrderCode;
						if (strOrderType != "EmptyBills")
						{
							modelGoodsLog.Type = 2;
							modelGoodsLog.Remark = "商品销售出库";
						}
						else
						{
							modelGoodsLog.Type = 3;
							modelGoodsLog.Remark = "商品挂单出库";
						}
						modelGoodsLog.TotalPrice = dclDiscountMoney;
						modelGoodsLog.CreateTime = dtExTime;
						modelGoodsLog.ShopID = intUserShopID;
						modelGoodsLog.UserID = intUserID;
						Chain.BLL.GoodsLog bllGoodsLog = new Chain.BLL.GoodsLog();
						if (!bolIsEmptyBills)
						{
							intLog = bllGoodsLog.Add(modelGoodsLog);
						}
						else
						{
							DataTable dtGoods = bllGoodsLog.GetList("GoodsAccount='" + strOrderCode + "'").Tables[0];
							if (dtGoods.Rows.Count > 0)
							{
								goodLog = int.Parse(dtGoods.Rows[0]["ID"].ToString());
								modelGoodsLog.ID = goodLog;
								if (bllGoodsLog.Update(modelGoodsLog))
								{
									intLog = goodLog;
								}
							}
						}
					}
					Chain.BLL.OrderDetail bllDetail = new Chain.BLL.OrderDetail();
					Chain.Model.GoodsNumber modelNumber = new Chain.Model.GoodsNumber();
					Chain.BLL.GoodsNumber bllNumber = new Chain.BLL.GoodsNumber();
					Chain.BLL.GoodsLogDetail bllGoodsDetail = new Chain.BLL.GoodsLogDetail();
					Chain.Model.GoodsLogDetail modelGoodsDetail = new Chain.Model.GoodsLogDetail();
					Chain.Model.OrderDetail modelDetail = new Chain.Model.OrderDetail();
					if (intOrderLogAdd > 0)
					{
						int intDetailAdd = 0;
						if (bolIsEmptyBills)
						{
							DataTable dtOrderDetail = bllDetail.GetList("OrderID=" + orderID).Tables[0];
							int intUpdateNumber = 0;
							for (int i = 0; i < dtOrderDetail.Rows.Count; i++)
							{
								modelNumber.GoodsID = int.Parse(dtOrderDetail.Rows[i]["GoodsID"].ToString());
								modelNumber.ShopID = intUserShopID;
								modelNumber.Number = int.Parse(dtOrderDetail.Rows[i]["OrderDetailNumber"].ToString());
								intUpdateNumber = bllNumber.UpdataGoodsNumber(modelNumber);
							}
							if (intUpdateNumber > 0)
							{
								bllDetail.DeleteDetail(orderID);
							}
							bllGoodsDetail.DeleteDetail(goodLog);
						}
						Chain.BLL.Goods bllGoods = new Chain.BLL.Goods();
						int RecordCount = 0;
						DataTable dt = bllGoods.GetGoodsStockList(intUserShopID, memLevelId, GoodsListId, 100, 1, out RecordCount, new string[]
						{
							""
						}).Tables[0];
						string[] idList = GoodsListId.Split(new char[]
						{
							','
						});
						string[] countList = GoodsListCount.Split(new char[]
						{
							','
						});
						if (dt == null || RecordCount != intCount || idList.Length != countList.Length)
						{
						}
						for (int j = 0; j < intCount; j++)
						{
							modelDetail.OrderID = intOrderLogAdd;
							modelDetail.GoodsID = int.Parse(dt.Rows[j]["GoodsID"].ToString());
							modelDetail.OrderDetailPrice = decimal.Parse(dt.Rows[j]["Price"].ToString());
							modelDetail.OrderDetailDiscountPrice = decimal.Parse(dt.Rows[j]["DiscountPrice"].ToString());
							for (int xx = 0; xx < idList.Length; xx++)
							{
								if (idList[xx] == dt.Rows[j]["GoodsID"].ToString())
								{
									modelDetail.OrderDetailNumber = decimal.Parse(countList[xx]);
									decimal point = decimal.Parse(dt.Rows[j]["Point"].ToString());
									if (point > 0m)
									{
										modelDetail.OrderDetailPoint = int.Parse(point.ToString());
									}
									else
									{
										point = decimal.Parse(dt.Rows[j]["DiscountPrice"].ToString()) * decimal.Parse(dt.Rows[j]["PointDiscount"].ToString());
										modelDetail.OrderDetailPoint = int.Parse(Math.Round(point, 0).ToString());
									}
								}
							}
							if (bolIsEmptyBills)
							{
								modelDetail.OrderID = orderID;
							}
							if (modelDetail.OrderDetailNumber > 0m)
							{
								modelDetail.OrderDetailType = 0;
							}
							else
							{
								modelDetail.OrderDetailType = 1;
							}
							intDetailAdd = bllDetail.Add(modelDetail);
							int intGoodsType = int.Parse(dt.Rows[j]["GoodsType"].ToString());
							if (intGoodsType == 0)
							{
								modelNumber.GoodsID = modelDetail.GoodsID;
								modelNumber.ShopID = intUserShopID;
								modelNumber.Number = int.Parse((modelDetail.OrderDetailNumber * -1m).ToString());
								bllNumber.UpdataGoodsNumber(modelNumber);
							}
							modelGoodsDetail.GoodsLogID = intLog;
							modelGoodsDetail.GoodsID = modelDetail.GoodsID;
							modelGoodsDetail.GoodsInPrice = modelDetail.OrderDetailPrice;
							modelGoodsDetail.GoodsInPrice = modelDetail.OrderDetailDiscountPrice / modelDetail.OrderDetailNumber;
							modelGoodsDetail.GoodsNumber = decimal.Parse(modelDetail.OrderDetailNumber.ToString()) * -1m;
							if (bolIsEmptyBills)
							{
								modelGoodsDetail.GoodsLogID = goodLog;
							}
							bllGoodsDetail.Add(modelGoodsDetail);
							Chain.BLL.MemCountDetail bllCountDetail = new Chain.BLL.MemCountDetail();
							if (modelDetail.OrderDetailNumber < 0m)
							{
								DataTable dtCount = bllCountDetail.GetList(-1, string.Concat(new object[]
								{
									" CountDetailMemID=",
									memID,
									" and CountDetailGoodsID=",
									modelDetail.GoodsID,
									" and CountDetailNumber>0"
								}), "CountCreateTime ASC").Tables[0];
								int orderCount = Math.Abs(int.Parse(modelDetail.OrderDetailNumber.ToString()));
								foreach (DataRow drCount in dtCount.Rows)
								{
									if (orderCount != 0)
									{
										int detailNumber = int.Parse(drCount["CountDetailNumber"].ToString());
										if (detailNumber > orderCount)
										{
											bllCountDetail.UpdateCountDetailNumber(orderCount, int.Parse(drCount["CountDetailID"].ToString()));
											orderCount = 0;
										}
										else
										{
											bllCountDetail.UpdateCountDetailNumber(detailNumber, int.Parse(drCount["CountDetailID"].ToString()));
											orderCount -= detailNumber;
										}
									}
								}
							}
						}
						if (intDetailAdd > 0 && memID != 0)
						{
							if (strOrderType != "EmptyBills")
							{
								Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
								if (bllPoint.Add(new Chain.Model.PointLog
								{
									PointMemID = memID,
									PointNumber = intPoint,
									PointChangeType = 1,
									PointRemark = "会员商品消费成功，消费总额：[" + dclDiscountMoney + "]",
									PointShopID = intUserShopID,
									PointUserID = intUserID,
									PointCreateTime = dtExTime,
									PointOrderCode = strOrderCode
								}) > 0)
								{
									decimal dclMemMoney = modelMem.MemMoney - dclCardPayMoney;
									modelMem.MemConsumeMoney += dclDiscountMoney;
									modelMem.MemPoint += intPoint;
									modelMem.MemConsumeLastTime = dtExTime;
									modelMem.MemConsumeCount++;
									int mem = bllMem.ExpenseUpdateMem(memID, dclMemMoney, modelMem.MemConsumeMoney, modelMem.MemPoint, DateTime.Now, modelMem.MemConsumeCount);
									MEMPointUpdate.MEMPointRate(modelMem, modelOrderLog.OrderPoint, modelOrderLog.OrderAccount, 1, intUserID, intUserShopID);
									modelMem = new Chain.BLL.Mem().GetModel(memID);
									string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
									flag = 3;
									if (modelMem.MemMobile != "" && PubFunction.curParameter.bolSms && PubFunction.curParameter.bolAutoSendSMSByCommodityConsumption)
									{
										if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
										{
											flag = -2;
										}
										else
										{
											SmsTemplateParameter smsTemplateParameter = new SmsTemplateParameter();
											smsTemplateParameter.strCardID = modelMem.MemCard;
											smsTemplateParameter.strName = modelMem.MemName;
											smsTemplateParameter.dclTempMoney = dclDiscountMoney;
											smsTemplateParameter.dclMoney = modelMem.MemMoney;
											smsTemplateParameter.intTempPoint = intPoint;
											smsTemplateParameter.intPoint = modelMem.MemPoint;
											smsTemplateParameter.OldLevelID = intLevelID;
											modelMem = new Chain.BLL.Mem().GetModel(memID);
											smsTemplateParameter.NewLevelID = modelMem.MemLevelID;
											string strSendContent = SMSInfo.GetSendContent(5, smsTemplateParameter, intUserShopID);
											SMSInfo.Send_GXSMS(false, modelMem.MemMobile, strSendContent, "");
											Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
											modelSms.SmsMemID = modelMem.MemID;
											modelSms.SmsMobile = modelMem.MemMobile;
											modelSms.SmsContent = strSendContent;
											modelSms.SmsTime = DateTime.Now;
											modelSms.SmsShopID = intUserShopID;
											modelSms.SmsUserID = intUserID;
											modelSms.SmsAmount = PubFunction.GetSmsAmount(strSendContent);
											modelSms.SmsAllAmount = modelSms.SmsAmount;
											Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
											bllSms.Add(modelSms);
										}
									}
									PubFunction.SaveSysLog(intUserID, 4, "会员消费", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
								}
							}
							else
							{
								flag = 2;
								PubFunction.SaveSysLog(intUserID, 4, "会员消费", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
							}
						}
						else if (intDetailAdd > 0 && memID == 0)
						{
							flag = 1;
							PubFunction.SaveSysLog(intUserID, 4, "散客消费", Remark, intUserShopID, DateTime.Now, PubFunction.ipAdress);
						}
					}
				}
				else
				{
					flag = -6;
				}
			}
			catch
			{
				flag = -1;
			}
			this.ResponseWrite(flag >= 0 || flag == -2 || flag == -6, flag, "''");
		}

		public void GetRptTotal()
		{
			int flag = 0;
			string result = "";
			try
			{
				string strUserName = "";
				DateTime time = DateTime.Now;
				DateTime time2 = DateTime.Now;
				string checkRadion = (this.Request["checkRadion"] != null) ? this.Request["checkRadion"].ToString() : "";
				string arg_7B_0 = (this.Request["txttimeStart"] != null) ? this.Request["txttimeStart"].ToString() : "";
				string arg_AB_0 = (this.Request["txttimeEnd"] != null) ? this.Request["txttimeEnd"].ToString() : "";
				string shopID = (this.Request["ShopID"] != null) ? this.Request["ShopID"].ToString() : "";
				string userID = (this.Request["UserID"] != null) ? this.Request["UserID"].ToString() : "";
				int sysShopId = (this.Request["UserShopID"] != null) ? Convert.ToInt32(this.Request["UserShopID"].ToString()) : 0;
				int arg_16D_0 = (this.Request["UID"] != null) ? Convert.ToInt32(this.Request["UID"].ToString()) : 0;
				if (userID == "" || userID == null)
				{
					strUserName = "所有操作员";
				}
				else
				{
					strUserName = PubFunction.UserIDTOName(int.Parse(userID));
				}
				if (checkRadion == "1")
				{
					time = DateTime.Today;
					time2 = DateTime.Today.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
				}
				else if (checkRadion == "2")
				{
					time = DateTime.Today.AddDays(-1.0);
					time2 = DateTime.Today.AddDays(-1.0).AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
				}
				else if (checkRadion == "3")
				{
					time = DateTime.Today.AddDays(-7.0);
					time2 = DateTime.Today.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
				}
				else if (checkRadion == "4")
				{
					time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
					time2 = DateTime.Today.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
				}
				else if (checkRadion == "5")
				{
					time = DateTime.Today.AddDays(-30.0);
					time2 = DateTime.Today.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
				}
				string memWhere = " 1=1 ";
				string rechargeWhere = " 1=1 ";
				string orderWhere = " 1=1 ";
				string countWhere = " 1=1 ";
				string memstoragetiming = " 1=1";
				string drawmoneyWhere = "1=1";
				if (shopID != "")
				{
					memWhere = memWhere + " and MemShopID=" + shopID;
					rechargeWhere = rechargeWhere + " and RechargeShopID=" + shopID;
					orderWhere = orderWhere + " and OrderShopID=" + shopID;
					countWhere = countWhere + " and CountShopID=" + shopID;
					memstoragetiming = memstoragetiming + " and StorageTimingShopID = " + shopID;
					drawmoneyWhere = drawmoneyWhere + " and DrawMoneyShopID =" + shopID;
				}
				else
				{
					memWhere = PubFunction.GetShopAuthority(sysShopId, "MemShopID", memWhere);
					rechargeWhere = PubFunction.GetShopAuthority(sysShopId, "RechargeShopID", rechargeWhere);
					orderWhere = PubFunction.GetShopAuthority(sysShopId, "OrderShopID", orderWhere);
					countWhere = PubFunction.GetShopAuthority(sysShopId, "CountShopID", countWhere);
					memstoragetiming = PubFunction.GetShopAuthority(sysShopId, "StorageTimingShopID", memstoragetiming);
					drawmoneyWhere = PubFunction.GetShopAuthority(sysShopId, "DrawMoneyShopID", drawmoneyWhere);
				}
				if (userID != "")
				{
					memWhere = memWhere + ((memWhere != "") ? " and" : "") + " MemUserID=" + userID;
					rechargeWhere = rechargeWhere + ((rechargeWhere != "") ? " and" : "") + "  RechargeUserID=" + userID;
					orderWhere = orderWhere + ((orderWhere != "") ? " and" : "") + "  OrderUserID=" + userID;
					countWhere = countWhere + ((countWhere != "") ? " and" : "") + "  CountUserID=" + userID;
					memstoragetiming = memstoragetiming + " and StorageTimingUserID = " + userID;
					drawmoneyWhere = drawmoneyWhere + " and DrawMoneyUserID =" + userID;
				}
				if (time.ToString() != "" && time2.ToString() != "")
				{
					memWhere = memWhere + ((memWhere != "") ? " and" : "") + string.Format("  MemCreateTime between '{0}' and '{1}'", time, time2);
					rechargeWhere = rechargeWhere + ((rechargeWhere != "") ? " and" : "") + string.Format(" RechargeCreateTime between '{0}' and '{1}' ", time, time2);
					orderWhere = orderWhere + ((orderWhere != "") ? " and" : "") + string.Format(" OrderCreateTime between '{0}' and '{1}' ", time, time2);
					countWhere = countWhere + ((countWhere != "") ? " and" : "") + string.Format(" CountCreateTime between '{0}' and '{1}' ", time, time2);
					memstoragetiming += string.Format(" and StorageTimingCreateTime between '{0}' and '{1}'", time, time2);
					drawmoneyWhere += string.Format(" AND DrawMoneyCreateTime between '{0}' and '{1}' ", time, time2);
				}
				DataSet ds = new Chain.BLL.SysShop().GetTotalRptData(memWhere, rechargeWhere, orderWhere, countWhere, memstoragetiming, drawmoneyWhere);
				int intNumber = 0;
				StringBuilder sbMem = new StringBuilder();
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					intNumber += int.Parse(row["MemNumber"].ToString());
					sbMem.Append(PubFunction.LevelIDToName(int.Parse(row["LevelID"].ToString())) + "：" + row["MemNumber"].ToString() + "名 ");
				}
				decimal SRechargeMoney = 0m;
				DataRow[] drs = ds.Tables[1].Select(" RechargeType=1 ");
				if (drs.Length == 1)
				{
					SRechargeMoney += decimal.Parse(drs[0][0].ToString());
				}
				decimal FRechargeGiveMoney = 0m;
				for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
				{
					FRechargeGiveMoney += decimal.Parse(ds.Tables[1].Rows[i][1].ToString());
				}
				decimal FRechargeMoney = 0m;
				drs = ds.Tables[1].Select(" RechargeType=2 ");
				if (drs.Length == 1)
				{
					FRechargeMoney += decimal.Parse(drs[0][0].ToString());
				}
				decimal FRechargeBankMoney = 0m;
				drs = ds.Tables[1].Select(" RechargeType=3 ");
				if (drs.Length == 1)
				{
					FRechargeBankMoney += decimal.Parse(drs[0][0].ToString());
				}
				decimal FRechargeTotalMoney = SRechargeMoney + FRechargeGiveMoney + FRechargeMoney + FRechargeBankMoney;
				decimal payBink = 0m;
				decimal payCash = 0m;
				decimal payCard = 0m;
				decimal payCoupon = 0m;
				decimal zljg = 0m;
				decimal totalPay = 0m;
				foreach (DataRow dr in ds.Tables[2].Rows)
				{
					payCard += decimal.Parse(dr["OrderPayCard"].ToString());
					payCash += decimal.Parse(dr["OrderPayCash"].ToString());
					payBink += decimal.Parse(dr["OrderPayBink"].ToString());
					payCoupon += decimal.Parse(dr["OrderPayCoupon"].ToString());
					totalPay += decimal.Parse(dr["OrderDiscountMoney"].ToString());
					zljg += decimal.Parse(dr["OrderPayCard"].ToString()) + decimal.Parse(dr["OrderPayBink"].ToString()) + decimal.Parse(dr["OrderPayCash"].ToString()) + decimal.Parse(dr["OrderPayCoupon"].ToString()) - decimal.Parse(dr["OrderDiscountMoney"].ToString());
				}
				decimal countPayBink = 0m;
				decimal countPayCash = 0m;
				decimal countPayCard = 0m;
				decimal countPayCoupon = 0m;
				decimal jczl = 0m;
				decimal totalCountPay = 0m;
				foreach (DataRow dr in ds.Tables[3].Rows)
				{
					countPayCard += decimal.Parse(dr["CountPayCard"].ToString());
					countPayCash += decimal.Parse(dr["CountPayCash"].ToString());
					countPayBink += decimal.Parse(dr["CountPayBink"].ToString());
					countPayCoupon += decimal.Parse(dr["CountPayCoupon"].ToString());
					totalCountPay += decimal.Parse(dr["CountDiscountMoney"].ToString());
					jczl += decimal.Parse(dr["CountPayCard"].ToString()) + decimal.Parse(dr["CountPayBink"].ToString()) + decimal.Parse(dr["CountPayCash"].ToString()) + decimal.Parse(dr["CountPayCoupon"].ToString()) - decimal.Parse(dr["CountDiscountMoney"].ToString());
				}
				decimal expenseSumMoneys = payCash - zljg;
				decimal countSumMoneys = countPayCash - jczl;
				decimal allMoney = SRechargeMoney + FRechargeMoney + expenseSumMoneys + countSumMoneys - Convert.ToDecimal(ds.Tables[5].Rows[0]["AllDrawMoney"]);
				decimal totalPayBank = FRechargeBankMoney + payBink + countPayBink;
				decimal totalPayCard = payCard + countPayCard;
				decimal doWorkallMoney = SRechargeMoney + FRechargeMoney + expenseSumMoneys + countSumMoneys - Convert.ToDecimal(ds.Tables[5].Rows[0]["AllDrawMoney"]);
				result = string.Concat(new object[]
				{
					"{\"timeRadion\":\"",
					time.ToShortDateString(),
					" 至 ",
					time2.ToShortDateString(),
					"\",\"intNumber\":\"",
					intNumber,
					"名\",\"memSRechargeMoney\":\"",
					SRechargeMoney.ToString("#0.00"),
					"\",\"memFRechargeMoney\":\"",
					FRechargeMoney.ToString("#0.00"),
					"\",\"FRechargeTotalMoney\":\"",
					FRechargeTotalMoney.ToString("#0.00"),
					"\",\"expenseSumMoneys\":\"",
					expenseSumMoneys.ToString("#0.00"),
					"\",\"expenseBankSumMoneys\":\"",
					FRechargeBankMoney.ToString("#0.00"),
					"\",\"payCard\":\"",
					payCard.ToString("#0.00"),
					"\",\"payBink\":\"",
					payBink.ToString("#0.00"),
					"\",\"payCoupon\":\"",
					payCoupon.ToString("#0.00"),
					"\",\"FRechargeGiveMoney\":\"",
					FRechargeGiveMoney.ToString("#0.00"),
					"\",\"allMoney\":\"",
					allMoney.ToString("#0.00"),
					"\",\"MemDetial\":\"",
					sbMem,
					"\",\"strUserName\":\"",
					strUserName,
					"\",\"doWorkallMoney\":\"",
					doWorkallMoney,
					"\",\"countSumMoneys\":\"",
					countSumMoneys.ToString("#0.00"),
					"\",\"countPayCard\":\"",
					countPayCard.ToString("#0.00"),
					"\",\"countPayBink\":\"",
					countPayBink.ToString("#0.00"),
					"\",\"countpayCoupon\":\"",
					countPayCoupon.ToString("#0.00"),
					"\",\"totalPay\":\"",
					totalPay.ToString("#0.00"),
					"\",\"totalCountPay\":\"",
					totalCountPay.ToString("#0.00"),
					"\",\"totalPayBank\":\"",
					totalPayBank.ToString("#0.00"),
					"\",\"totalPayCard\":\"",
					totalPayCard.ToString("#0.00"),
					"\",\"AllDrawMoney\":\"",
					Convert.ToDecimal(ds.Tables[5].Rows[0]["AllDrawMoney"]).ToString("F2"),
					"\",\"AllDrawActualMoney\":\"",
					Convert.ToDecimal(ds.Tables[5].Rows[0]["AllDrawActualMoney"]).ToString("F2"),
					"\"}"
				});
				flag = 1;
			}
			catch
			{
				flag = -3;
			}
			this.ResponseWrite(flag > 0, flag, result);
		}

		public void GetGiftClass()
		{
			string result = "{}";
			Chain.BLL.GiftClass giftClass = new Chain.BLL.GiftClass();
			DataTable dtClass = giftClass.GetList("").Tables[0];
			DataTable dtResult = new DataTable();
			giftClass.CreateTreeItem(dtClass, dtResult, 0, 0);
			if (dtResult != null && dtResult.Rows.Count > 0)
			{
				result = JsonPlus.ToJson(dtResult, "GiftClassID,GiftClassName,GiftClassRemark,GiftParentID");
			}
			this.ResponseWrite("GiftClassList", result, dtResult.Rows.Count);
		}

		public void GetPointGiftList()
		{
			string result = "{}";
			int resCount = 0;
			Chain.BLL.Goods bllGoods = new Chain.BLL.Goods();
			try
			{
				int intSize = (this.Request["limit"].ToString() != "") ? int.Parse(this.Request["limit"].ToString()) : 0;
				int intIndex = (this.Request["page"].ToString() != "") ? int.Parse(this.Request["page"].ToString()) : 0;
				string key = (this.Request["key"] != null) ? this.Request["key"].ToString() : "";
				int intShopID = (this.Request["intShopID"] != null) ? Convert.ToInt32(this.Request["intShopID"].ToString()) : -1;
				int intClassID = (this.Request["ClassID"] != null) ? Convert.ToInt32(this.Request["ClassID"].ToString()) : -1;
				string GiftIdList = (this.Request["GiftIdList"] != null) ? this.Request["GiftIdList"].ToString() : "";
				StringBuilder sbWhere = new StringBuilder();
				sbWhere.Append("GiftStockNumber > 0 ");
				if (!PubFunction.curParameter.bolGiftShare && intShopID > 0)
				{
					sbWhere.Append("and GiftShopID=" + intShopID.ToString());
				}
				if (key != null && key != "")
				{
					sbWhere.AppendFormat(" and ((GiftCode like '{0}') or (GiftName like '{0}'))", key);
				}
				if (intClassID > 0)
				{
					sbWhere.AppendFormat(" and ((PointGift.GiftClassID = {0}) or (PointGift.GiftClassID in (select GiftClassID from GiftClass where GiftParentID = {1} )) )", intClassID, intClassID);
				}
				if (!string.IsNullOrEmpty(GiftIdList))
				{
					sbWhere.AppendFormat(" and PointGift.GiftID in ({0})", GiftIdList.Trim(new char[]
					{
						','
					}));
				}
				sbWhere.AppendFormat(" and PointGift.GiftShopID = SysShop.ShopID and GiftClass.GiftClassID = PointGift.GiftClassID", new object[0]);
				DataTable dtGift = new Chain.BLL.PointGift().GetListSP(intSize, intIndex, true, out resCount, new string[]
				{
					sbWhere.ToString()
				}).Tables[0];
				if (dtGift != null)
				{
					result = JsonPlus.ToJson(dtGift, "GiftID,GiftName,GiftCode,GiftClassID,GiftExchangePoint,GiftStockNumber,GiftExchangeNumber");
				}
			}
			catch
			{
			}
			this.ResponseWrite("GiftList", result, resCount);
		}

		public void GiftExchangeCode()
		{
			string OrderCode = PubFunction.curParameter.strGiftExchangePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
			this.ResponseWrite(true, 1, "'" + OrderCode + "'");
		}

		public void GiftExchange()
		{
			int flag = 0;
			int memID = int.Parse(this.Request["memID"].ToString());
			int sumPoint = int.Parse(this.Request["sumPoint"].ToString());
			int sumNumber = int.Parse(this.Request["sumNumber"].ToString());
			int giftcount = int.Parse(this.Request["giftcount"].ToString());
			int intUserID = int.Parse(this.Request["intUserID"].ToString());
			int intUserShopID = int.Parse(this.Request["intUserShopID"].ToString());
			string strAccount = this.Request["strOrderCode"].ToString();
			string GiftListId = this.Request["GiftListId"].ToString();
			string GiftListCount = this.Request["GiftListCount"].ToString();
			try
			{
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = bllMem.GetModel(memID);
				Chain.Model.PointLog modelPoint = new Chain.Model.PointLog();
				Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
				Chain.Model.GiftExchange modelGiftExchange = new Chain.Model.GiftExchange();
				Chain.BLL.GiftExchange bllGiftExchange = new Chain.BLL.GiftExchange();
				Chain.Model.GiftExchangeDetail modelDetail = new Chain.Model.GiftExchangeDetail();
				Chain.BLL.GiftExchangeDetail bllDetail = new Chain.BLL.GiftExchangeDetail();
				int levelID = modelMem.MemLevelID;
				if (modelMem.MemState != 0)
				{
					flag = -1;
				}
				else
				{
					modelGiftExchange.MemID = memID;
					modelGiftExchange.ExchangeTelePhone = modelMem.MemTelePhone;
					modelGiftExchange.ExchangeAddress = "";
					modelGiftExchange.ExchangeAccount = strAccount;
					modelGiftExchange.ExchangeAllNumber = sumNumber;
					modelGiftExchange.ExchangeAllPoint = sumPoint;
					modelGiftExchange.ApplicationTime = DateTime.Now;
					modelGiftExchange.ExchangeStatus = 2;
					modelGiftExchange.ExchangeTime = DateTime.Now;
					modelGiftExchange.ExchangeUserID = intUserID;
					modelGiftExchange.ExchangeType = 4;
					modelGiftExchange.ShopID = intUserShopID;
					int intSuccess = bllGiftExchange.MainSystemAdd(modelGiftExchange);
					int resCount = 0;
					StringBuilder sbWhere = new StringBuilder("");
					sbWhere.Append("PointGift.GiftShopID = SysShop.ShopID and GiftClass.GiftClassID = PointGift.GiftClassID");
					if (!PubFunction.curParameter.bolGiftShare && intUserShopID > 0)
					{
						sbWhere.AppendFormat(" and GiftShopID={0}", intUserShopID.ToString());
					}
					if (!string.IsNullOrEmpty(GiftListId))
					{
						sbWhere.AppendFormat(" and PointGift.GiftID in ({0})", GiftListId.Trim(new char[]
						{
							','
						}));
					}
					DataTable dtGift = new Chain.BLL.PointGift().GetListSP(100, 1, true, out resCount, new string[]
					{
						sbWhere.ToString()
					}).Tables[0];
					if (intSuccess > 0)
					{
						for (int i = 0; i < dtGift.Rows.Count; i++)
						{
							modelDetail.ExchangeID = intSuccess;
							modelDetail.ExchangeGiftID = int.Parse(dtGift.Rows[i]["GiftID"].ToString());
							modelDetail.ExchangeNumber = 0;
							for (int x = 0; x < GiftListId.Split(new char[]
							{
								','
							}).Length; x++)
							{
								if (GiftListId.Split(new char[]
								{
									','
								})[x] == dtGift.Rows[i]["GiftID"].ToString())
								{
									modelDetail.ExchangeNumber = int.Parse(GiftListCount.Split(new char[]
									{
										','
									})[x]);
								}
							}
							modelDetail.Giftname = dtGift.Rows[i]["Giftname"].ToString();
							int point = int.Parse(dtGift.Rows[i]["GiftExchangePoint"].ToString());
							modelDetail.ExchangePoint = point * modelDetail.ExchangeNumber;
							bllDetail.Add(modelDetail);
							new Chain.BLL.PointGift().UpdateGiftNumber(modelDetail.ExchangeGiftID, modelDetail.ExchangeNumber);
						}
						modelPoint.PointMemID = modelMem.MemID;
						modelPoint.PointNumber = sumPoint;
						modelPoint.PointChangeType = 4;
						modelPoint.PointRemark = "实体店兑换礼品成功，扣减积分[" + sumPoint + "]";
						modelPoint.PointShopID = intUserShopID;
						modelPoint.PointCreateTime = DateTime.Now;
						modelPoint.PointUserID = intUserID;
						modelPoint.PointOrderCode = strAccount;
						bllPoint.Add(modelPoint);
						sumPoint *= -1;
						bllMem.UpdatePoint(memID, sumPoint);
						modelMem = new Chain.BLL.Mem().GetModel(memID);
						string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
						flag = 1;
						if (modelMem.MemMobile != "")
						{
							if (PubFunction.curParameter.bolSms && PubFunction.curParameter.bolAutoSendSMSByMemGiftExchange)
							{
								if (Convert.ToInt32(SMSInfo.GetBalance(false)) <= 0)
								{
									flag = -2;
								}
								else
								{
									SmsTemplateParameter smsTemplateParameter = new SmsTemplateParameter();
									smsTemplateParameter.strCardID = modelMem.MemCard;
									smsTemplateParameter.strName = modelMem.MemName;
									smsTemplateParameter.dclTempMoney = 0m;
									smsTemplateParameter.dclMoney = modelMem.MemMoney;
									smsTemplateParameter.intTempPoint = -sumPoint;
									smsTemplateParameter.intPoint = modelMem.MemPoint;
									smsTemplateParameter.OldLevelID = levelID;
									modelMem = new Chain.BLL.Mem().GetModel(memID);
									smsTemplateParameter.NewLevelID = modelMem.MemLevelID;
									string strSendContent = SMSInfo.GetSendContent(4, smsTemplateParameter, intUserShopID);
									SMSInfo.Send_GXSMS(false, modelMem.MemMobile, strSendContent, "");
									Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
									modelSms.SmsMemID = modelMem.MemID;
									modelSms.SmsMobile = modelMem.MemMobile;
									modelSms.SmsContent = strSendContent;
									modelSms.SmsTime = DateTime.Now;
									modelSms.SmsShopID = intUserShopID;
									modelSms.SmsUserID = intUserID;
									modelSms.SmsAmount = PubFunction.GetSmsAmount(strSendContent);
									modelSms.SmsAllAmount = modelSms.SmsAmount;
									Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
									bllSms.Add(modelSms);
								}
							}
						}
						PubFunction.SaveSysLog(intUserID, 4, "礼品兑换", string.Concat(new object[]
						{
							"积分兑换,会员卡号：[",
							modelMem.MemCard,
							"],姓名：[",
							modelMem.MemName,
							"],礼品总数：[",
							sumNumber,
							"],总积分：[",
							sumNumber,
							"]"
						}), intUserShopID, DateTime.Now, PubFunction.ipAdress);
					}
				}
			}
			catch
			{
				flag = 0;
			}
			this.ResponseWrite(flag > 0 || flag == -2, flag, "''");
		}

		private void ResponseWrite(bool success, int flag, string data)
		{
			string strJson = "'success':{0},flag:{1},data:{2}";
			strJson = string.Format(strJson, success.ToString().ToLower(), flag, data);
			string cb = this.Request.Params.Get("callback");
			if (!string.IsNullOrEmpty(cb))
			{
				this.Response.Write(cb + "({" + strJson + "})");
			}
			else
			{
				this.Response.Write("{" + strJson + "}");
			}
		}

		private void ResponseWrite(string name, string data, int count)
		{
			string strJson = "success:true,recordCount:{0},{1}:{2}";
			strJson = string.Format(strJson, count, name, data);
			string cb = this.Request.Params.Get("callback");
			if (!string.IsNullOrEmpty(cb))
			{
				this.Response.Write(cb + "({" + strJson + "})");
			}
			else
			{
				this.Response.Write("{" + strJson + "}");
			}
		}

		private string getProperties<T>(T t)
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
							tStr += string.Format("{0}:'{1}',", name, value);
						}
						else
						{
							this.getProperties<object>(value);
						}
					}
					result = tStr;
				}
			}
			return result;
		}
	}
}
