using Chain.BLL;
using Chain.Common;
using Chain.Common.DEncrypt;
using Chain.DBUtility;
using Chain.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;
using Web.Common;

namespace ChainStock.Service
{
	[ToolboxItem(false), ScriptService, WebService(Namespace = "http://tempuri.org/"), WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	public class ChainStockSelf : WebService
	{
		private MySelfSoapHeader _mySoapHeader = null;

		public MySelfSoapHeader MySoapHeader
		{
			get
			{
				MySelfSoapHeader result;
				if (this._mySoapHeader == null)
				{
					result = new MySelfSoapHeader();
				}
				else
				{
					result = this._mySoapHeader;
				}
				return result;
			}
			set
			{
				this._mySoapHeader = value;
			}
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public int MemPwdReset(int intMemID, string strNewPwd)
		{
			int result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
					result = bllMem.UpdateMemPwd(intMemID, DESEncrypt.Encrypt(strNewPwd.Trim()));
				}
				else
				{
					result = 0;
				}
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public int SecuritySettings(int intMemID, string strEmail, string strQuestion, string strAnswer)
		{
			int result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
					result = bllMem.SecuritySettings(intMemID, strEmail, strQuestion, strAnswer);
				}
				else
				{
					result = 0;
				}
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetMemCard(string strMemCard)
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.Mem bll = new Chain.BLL.Mem();
					Chain.Model.Mem mem = bll.GetModelByMemCard(strMemCard);
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(mem);
					}
					else
					{
						result = JsonPlus.ToJson(mem);
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetMemAdressBymencard(string strMemCard)
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.Mem bll = new Chain.BLL.Mem();
					DataTable dt = bll.GetMemAdressBymencard(strMemCard);
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dt);
					}
					else
					{
						result = JsonPlus.ToJson(dt, "MemProvinceName,MemCityName,MemCountyName,MemVillageName,MemAddress");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetSysParameter()
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.SysParameter bllSysParameter = new Chain.BLL.SysParameter();
					Chain.Model.SysParameter modelSysParameter = bllSysParameter.GetModel(1);
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(modelSysParameter);
					}
					else
					{
						result = JsonPlus.ToJson(modelSysParameter);
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetWebSetting()
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					StringWriter sw = new StringWriter();
					JsonWriter writer = new JsonWriter(sw);
					writer.WriteStartObject();
					writer.WritePropertyName("EnableGoods");
					writer.WriteValue(PubFunction.curParameter.EnableGoods.ToString());
					writer.WriteEndObject();
					writer.Flush();
					string jsonText = sw.GetStringBuilder().ToString();
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(jsonText);
					}
					else
					{
						result = JsonPlus.ToJson(jsonText);
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetLevel()
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.MemLevel bll = new Chain.BLL.MemLevel();
					List<Chain.Model.MemLevel> listLevel = bll.GetLevelList("");
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(listLevel);
					}
					else
					{
						StringBuilder sb = new StringBuilder();
						sb.Append("[");
						string[] strList = new string[listLevel.Count];
						for (int i = 0; i < listLevel.Count; i++)
						{
							strList[i] = JsonPlus.ToJson(listLevel[i]);
							if (0 == i)
							{
								sb.AppendFormat("{0}\":{1}", "{\"" + i, strList[i] + "}");
							}
							else
							{
								sb.AppendFormat(",{0}\":{1}", "{\"" + i, strList[i] + "}");
							}
						}
						sb.Append("]");
						result = sb.ToString();
					}
				}
				else
				{
					result = null;
				}
			}
			catch (Exception e)
			{
				Chain.BLL.SysError.Add(e, PubFunction.ipAdress);
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetShopList()
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.SysShop bll = new Chain.BLL.SysShop();
					List<Chain.Model.SysShop> listShop = bll.GetModelList("");
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(listShop);
					}
					else
					{
						StringBuilder sb = new StringBuilder();
						sb.Append("{");
						string[] strShop = new string[listShop.Count];
						for (int i = 0; i < listShop.Count; i++)
						{
							strShop[i] = JsonPlus.ToJson(listShop[i]);
							if (i == 0)
							{
								sb.AppendFormat("{0}:[{1}]", i, strShop[i]);
							}
							else
							{
								sb.AppendFormat(",{0}:[{1}]", i, strShop[i]);
							}
						}
						sb.Append("}");
						result = sb.ToString();
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object CheckMemPwd(string strAccount, string strPassword)
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
					Chain.Model.Mem modelMem = bllMem.CheckMemPwd(strAccount.Trim(), DESEncrypt.Encrypt(strPassword.Trim()));
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(modelMem);
					}
					else
					{
						result = JsonPlus.ToJson(modelMem);
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public int ModifyMemPwd(int intMemID, string strOldPwd, string strNewPwd)
		{
			int result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
					result = bllMem.UpdateMemPwd(intMemID, DESEncrypt.Encrypt(strNewPwd.Trim()), DESEncrypt.Encrypt(strOldPwd.Trim()));
				}
				else
				{
					result = 0;
				}
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetMemList(string strMemCard)
		{
			object result;
			if (this.MySoapHeader.CheckUser())
			{
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				string strSql = string.Format(" MemCard='{0}' ", strMemCard);
				DataTable dtMem = bllMem.GetList(strSql).Tables[0];
				if (this.MySoapHeader.SerializeRequired)
				{
					result = ConvertSerialize.CompactFormatterSerialize(dtMem);
				}
				else
				{
					result = JsonPlus.ToJson(dtMem, "");
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public int UpdateMemInfo(int intMemID, string strMemMobile, string strMemBirthday, string strMemEmail, string strMemTelephone, string strMemIdentityCard, string strMemAddress)
		{
			int result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
					Chain.Model.Mem modelMem = new Chain.Model.Mem();
					modelMem = bllMem.GetModel(intMemID);
					modelMem.MemMobile = strMemMobile.Trim();
					modelMem.MemBirthday = DateTime.Parse(strMemBirthday);
					modelMem.MemEmail = strMemEmail.Trim();
					modelMem.MemAddress = strMemAddress.Trim();
					modelMem.MemTelePhone = strMemTelephone.Trim();
					modelMem.MemIdentityCard = strMemIdentityCard.Trim();
					string domain = PubConstant.DoMain;
					result = bllMem.UpdateMemSelf(modelMem);
				}
				else
				{
					result = 0;
				}
			}
			catch
			{
				result = 0;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object ResetMemInfo(int intMemID)
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
					string strWhere = string.Format(" MemID={0} ", intMemID);
					DataTable dtMem = bllMem.GetList(strWhere).Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dtMem);
					}
					else
					{
						result = JsonPlus.ToJson(dtMem, "");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetMemCustomField()
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.MemCustomField bllCustom = new Chain.BLL.MemCustomField();
					DataTable dtCustom = bllCustom.GetList("").Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dtCustom);
					}
					else
					{
						result = JsonPlus.ToJson(dtCustom, "");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetMemRechargeList(int intPageSize, int intPageIndex, string strWhere, out int intResCount)
		{
			intResCount = 0;
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.MemRecharge bllRecharge = new Chain.BLL.MemRecharge();
					DataTable dtMemRecharge = bllRecharge.GetListSP(intPageSize, intPageIndex, out intResCount, new string[]
					{
						strWhere
					}).Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dtMemRecharge);
					}
					else
					{
						result = JsonPlus.ToJson(dtMemRecharge, "");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetPointChangeList(int intPageSize, int intPageIndex, string strWhere, out int intResCount)
		{
			intResCount = 0;
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.PointLog bllPoint = new Chain.BLL.PointLog();
					DataTable dtPoint = bllPoint.GetListSP(intPageSize, intPageIndex, out intResCount, new string[]
					{
						strWhere
					}).Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dtPoint);
					}
					else
					{
						result = JsonPlus.ToJson(dtPoint, "");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetPointExchangeList(int intPageSize, int intPageIndex, string strWhere, out int intResCount)
		{
			intResCount = 0;
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.PointExchange bllPointExchange = new Chain.BLL.PointExchange();
					DataTable dtExchagne = bllPointExchange.GetListSP(intPageSize, intPageIndex, out intResCount, new string[]
					{
						strWhere
					}).Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dtExchagne);
					}
					else
					{
						result = JsonPlus.ToJson(dtExchagne, "");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetPointRateList(int intPageSize, int intPageIndex, string strWhere, out int intResCount)
		{
			intResCount = 0;
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.PointRate bllPointRate = new Chain.BLL.PointRate();
					DataTable dtPointRate = bllPointRate.GetMyTeamList(intPageSize, intPageIndex, out intResCount, new string[]
					{
						strWhere
					}).Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dtPointRate);
					}
					else
					{
						result = JsonPlus.ToJson(dtPointRate, "");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetShopDataTable(int intPageSize, int intPageIndex, string strWhere, out int intResCount)
		{
			intResCount = 0;
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
					DataTable dtShop = bllShop.GetListSP(intPageSize, intPageIndex, out intResCount, new string[]
					{
						string.IsNullOrEmpty(strWhere) ? "ShopID>0 and ShopState=0 " : ("ShopID>0 and ShopState=0 and " + strWhere)
					}).Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dtShop);
					}
					else
					{
						result = JsonPlus.ToJson(dtShop, "");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetGiftList(int intPageSize, int intPageIndex, bool IsAsc, string strWhere, out int intResCount)
		{
			intResCount = 0;
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.PointGift bllGift = new Chain.BLL.PointGift();
					DataTable dtGift = bllGift.GetListSP(intPageSize, intPageIndex, IsAsc, out intResCount, new string[]
					{
						strWhere
					}).Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dtGift);
					}
					else
					{
						result = JsonPlus.ToJson(dtGift, "");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetGiftListByGids(string strWhere)
		{
			object result;
			if (this.MySoapHeader.CheckUser())
			{
				Chain.BLL.PointGift bllGift = new Chain.BLL.PointGift();
				DataTable dtGift = bllGift.GetList(strWhere).Tables[0];
				if (this.MySoapHeader.SerializeRequired)
				{
					result = ConvertSerialize.CompactFormatterSerialize(dtGift);
				}
				else
				{
					result = JsonPlus.ToJson(dtGift, "");
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetHotGifts(int top, string strwhere, string filedOrder)
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.PointGift bllGift = new Chain.BLL.PointGift();
					DataTable dtGift = bllGift.GetList(top, strwhere, filedOrder).Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dtGift);
					}
					else
					{
						result = JsonPlus.ToJson(dtGift, "");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetGiftInfo(int giftID)
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.PointGift bllGift = new Chain.BLL.PointGift();
					Chain.Model.PointGift gift = bllGift.GetModel(giftID);
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(gift);
					}
					else
					{
						result = JsonPlus.ToJson(gift);
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetGiftClassInfo(int classID)
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.GiftClass bllClass = new Chain.BLL.GiftClass();
					Chain.Model.GiftClass ModelClass = bllClass.GetModel(classID);
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(ModelClass);
					}
					else
					{
						result = JsonPlus.ToJson(ModelClass);
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetGiftClassList()
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.GiftClass bllClass = new Chain.BLL.GiftClass();
					DataTable dtClass = bllClass.GetList("").Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dtClass);
					}
					else
					{
						result = JsonPlus.ToJson(dtClass, "");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public int SendMessage(int intMemID, string strReceiver, string strContent, int intUserID)
		{
			int intFlag = 0;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					if (PubFunction.curParameter.bolMoneySms)
					{
						if (int.Parse(SMSInfo.GetBalance(false).ToString()) > 0 && SMSInfo.Send_SMS(false, strReceiver, strContent, ""))
						{
							Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
							modelSms.SmsMemID = intMemID;
							modelSms.SmsMobile = strReceiver;
							modelSms.SmsContent = strContent;
							modelSms.SmsTime = DateTime.Now;
							modelSms.SmsUserID = intUserID;
							modelSms.SmsShopID = new Chain.BLL.SysUser().GetModel(intUserID).UserShopID;
							modelSms.SmsAmount = PubFunction.GetSmsAmount(strContent);
							modelSms.SmsAllAmount = modelSms.SmsAmount;
							Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
							bllSms.Add(modelSms);
							intFlag = 1;
						}
					}
				}
			}
			catch
			{
			}
			return intFlag;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public int SystemLog(int intUserID, int actionID, string strType, string strDetail, string strIpAddress)
		{
			int result;
			if (this.MySoapHeader.CheckUser())
			{
				int intShopID = new Chain.BLL.SysUser().GetModel(intUserID).UserShopID;
				Chain.Model.SysLog modelLog = new Chain.Model.SysLog();
				modelLog.LogUserID = intUserID;
				modelLog.LogActionID = actionID;
				modelLog.LogType = strType;
				modelLog.LogDetail = strDetail;
				modelLog.LogShopID = intShopID;
				modelLog.LogCreateTime = DateTime.Now;
				modelLog.LogIPAdress = strIpAddress;
				result = new Chain.BLL.SysLog().Add(modelLog);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public int OnlineMessage(int intMemID, string strContent)
		{
			int result;
			if (this.MySoapHeader.CheckUser())
			{
				Chain.BLL.Message bllMessage = new Chain.BLL.Message();
				result = bllMessage.Add(new Chain.Model.Message
				{
					MessageMemID = intMemID,
					MessageContent = strContent,
					MessageTime = DateTime.Now,
					MessageIsReply = 0
				});
			}
			else
			{
				result = 0;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetMessageList(int intPageSize, int intPageIndex, string strWhere, out int intResCount)
		{
			intResCount = 0;
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.Message bllMessage = new Chain.BLL.Message();
					DataTable dtMessage = bllMessage.GetMemMessageList(intPageSize, intPageIndex, out intResCount, new string[]
					{
						strWhere
					}).Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dtMessage);
					}
					else
					{
						result = JsonPlus.ToJson(dtMessage, "");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public int AddGiftExchangeInfo(Chain.Model.GiftExchange model)
		{
			int result;
			if (this.MySoapHeader.CheckUser())
			{
				model.ExchangeStatus = 1;
				int id = new Chain.BLL.GiftExchange().Add(model);
				result = id;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public int AddGiftExchangeDetailInfo(Chain.Model.GiftExchangeDetail model)
		{
			int result;
			if (this.MySoapHeader.CheckUser())
			{
				result = new Chain.BLL.GiftExchangeDetail().Add(model);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public bool CheckFileIsExist(string path)
		{
			return this.MySoapHeader.CheckUser() && File.Exists(base.Server.MapPath(path));
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetGiftExchangeDetailByExchangeID(int exchangeID)
		{
			object result;
			if (this.MySoapHeader.CheckUser())
			{
				DataTable dtGiftExchangeDetail = new Chain.BLL.GiftExchangeDetail().GetGiftExchangeDetailByExchangeID(exchangeID);
				if (this.MySoapHeader.SerializeRequired)
				{
					result = ConvertSerialize.CompactFormatterSerialize(dtGiftExchangeDetail);
				}
				else
				{
					result = JsonPlus.ToJson(dtGiftExchangeDetail, "");
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetGiftExchangeListSP(int PageSize, int PageIndex, out int resCount, string strWhere)
		{
			object result;
			if (this.MySoapHeader.CheckUser())
			{
				if (this.MySoapHeader.SerializeRequired)
				{
					result = ConvertSerialize.CompactFormatterSerialize(new Chain.BLL.GiftExchange().GetListGiftExchangeSP(PageSize, PageIndex, out resCount, new string[]
					{
						strWhere
					}).Tables[0]);
				}
				else
				{
					result = JsonPlus.ToJson(new Chain.BLL.GiftExchange().GetListGiftExchangeSP(PageSize, PageIndex, out resCount, new string[]
					{
						strWhere
					}).Tables[0], "");
				}
			}
			else
			{
				resCount = 1;
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetExchangeIDByGiftNameOrGiftCode(string giftNameOrGiftCodeStrWhere)
		{
			object result;
			if (this.MySoapHeader.CheckUser())
			{
				DataTable dtExchangeID = new Chain.BLL.GiftExchange().GetExchangeIDByGiftNameOrGiftCode(giftNameOrGiftCodeStrWhere);
				if (this.MySoapHeader.SerializeRequired)
				{
					result = ConvertSerialize.CompactFormatterSerialize(dtExchangeID);
				}
				else
				{
					result = JsonPlus.ToJson(dtExchangeID, "");
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetMemCountList(int intPageSize, int intPageIndex, string strWhere, out int intResCount)
		{
			intResCount = 0;
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.MemCount bllCount = new Chain.BLL.MemCount();
					DataTable dtPoint = bllCount.GetListSP(intPageSize, intPageIndex, out intResCount, new string[]
					{
						strWhere
					}).Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dtPoint);
					}
					else
					{
						result = JsonPlus.ToJson(dtPoint, "");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetMemCountDetailList(string strSql)
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.MemCountDetail bllMemCountDetail = new Chain.BLL.MemCountDetail();
					DataTable dtPoint = bllMemCountDetail.GetList(strSql).Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dtPoint);
					}
					else
					{
						result = JsonPlus.ToJson(dtPoint, "");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetMemExpenseList(int intPageSize, int intPageIndex, string strWhere, out int intResCount)
		{
			intResCount = 0;
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.OrderLog bllOrderLog = new Chain.BLL.OrderLog();
					DataTable dtExpense = bllOrderLog.GetListSP(intPageSize, intPageIndex, out intResCount, new string[]
					{
						strWhere
					}).Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dtExpense);
					}
					else
					{
						result = JsonPlus.ToJson(dtExpense, "");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetMemExpenseDetailList(string strSql)
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.OrderDetail bllDetail = new Chain.BLL.OrderDetail();
					DataTable dtOrderDetail = bllDetail.GetListSP(strSql).Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dtOrderDetail);
					}
					else
					{
						result = JsonPlus.ToJson(dtOrderDetail, "");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[WebMethod]
		public bool GiftShare()
		{
			return PubFunction.curParameter.bolGiftShare;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetMemRecharge(string strSql)
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.MemRecharge bllRecharge = new Chain.BLL.MemRecharge();
					DataTable dt = bllRecharge.GetList(strSql).Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dt);
					}
					else
					{
						result = JsonPlus.ToJson(dt, "");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetMem(int memID)
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.Mem bll = new Chain.BLL.Mem();
					Chain.Model.Mem mem = bll.GetModel(memID);
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(mem);
					}
					else
					{
						result = JsonPlus.ToJson(mem);
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public int UpdateRecharge(int rechargeID)
		{
			int result;
			if (this.MySoapHeader.CheckUser())
			{
				result = new Chain.BLL.MemRecharge().UpdateRecharge(rechargeID);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public int UpdateMemMoney(int memID, decimal dclMoney)
		{
			int result;
			if (this.MySoapHeader.CheckUser())
			{
				result = new Chain.BLL.Mem().UpdateMoney(memID, dclMoney);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public int MemRecharge(Chain.Model.MemRecharge model)
		{
			int result;
			if (this.MySoapHeader.CheckUser())
			{
				result = new Chain.BLL.MemRecharge().Add(model);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public object GetPointLog(string strWhere)
		{
			object result;
			try
			{
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.PointLog bllPL = new Chain.BLL.PointLog();
					DataTable dt = bllPL.GetList(1000, strWhere, "PointCreateTime desc").Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						result = ConvertSerialize.CompactFormatterSerialize(dt);
					}
					else
					{
						result = JsonPlus.ToJson(dt, "PointID,PointMemID,PointNumber,PointChangeType,PointRemark,PointShopID,PointCreateTime,PointUserID,PointOrderCode,PointGiveMemID");
					}
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public int AddEmail(Chain.Model.EmailLog model)
		{
			int result;
			if (this.MySoapHeader.CheckUser())
			{
				int id = new Chain.BLL.EmailLog().Add(model);
				result = id;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public void GetMemberList()
		{
			try
			{
				HttpContext.Current.Response.ContentType = "application/json;charset=utf-8";
				string jsonCallBackFunName = string.Empty;
				jsonCallBackFunName = HttpContext.Current.Request.Params["callback"].ToString();
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
					DataTable dtMem = bllMem.GetList("1=1").Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						HttpContext.Current.Response.Write(ConvertSerialize.CompactFormatterSerialize(dtMem));
					}
					else
					{
						string json = JsonPlus.ToJson(dtMem, "MemID,MemCard,MemPassword,MemName,MemSex,MemIdentityCard,MemMobile,MemPhoto,MemBirthdayType,MemBirthday,MemIsPast,MemPastTime,MemPoint,MemPointAutomatic,MemMoney,MemConsumeMoney,MemConsumeLastTime,MemConsumeCount,MemEmail,MemAddress,MemState,MemRecommendID,MemLevelID,MemShopID,MemCreateTime,MemUserID");
						HttpContext.Current.Response.Write(jsonCallBackFunName + "(" + json + ")");
					}
				}
			}
			catch
			{
			}
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public void GetGoodsList()
		{
			try
			{
				HttpContext.Current.Response.ContentType = "application/json;charset=utf-8";
				string jsonCallBackFunName = string.Empty;
				jsonCallBackFunName = HttpContext.Current.Request.Params["callback"].ToString();
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.Goods bllGoods = new Chain.BLL.Goods();
					DataTable dtGoods = bllGoods.GetList("1=1").Tables[0];
					if (this.MySoapHeader.SerializeRequired)
					{
						HttpContext.Current.Response.Write(ConvertSerialize.CompactFormatterSerialize(dtGoods));
					}
					else
					{
						string json = JsonPlus.ToJson(dtGoods, "GoodsID,GoodsCode,GoodsClassID,Name,NameCode,Unit,GoodsNumber,SalePercet,GoodsSaleNumber,Price,CommissionType,CommissionNumber,Point,MinPercent,GoodsType,GoodsBidPrice,GoodsRemark,GoodsPicture,GoodsCreateTime,CreateShopID,ClassName");
						HttpContext.Current.Response.Write(jsonCallBackFunName + "(" + json + ")");
					}
				}
			}
			catch
			{
			}
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public void GetModel()
		{
			try
			{
				HttpContext.Current.Response.ContentType = "application/json;charset=utf-8";
				string jsonCallBackFunName = string.Empty;
				jsonCallBackFunName = HttpContext.Current.Request.Params["callback"].ToString();
				string strWhere = string.Format(" MemCard='{0}' or MemName='{0}' or MemMobile='{0}'", HttpContext.Current.Request.Params["condition"].ToString());
				if (this.MySoapHeader.CheckUser())
				{
					Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
					DataTable dtMem = bllMem.GetModelDetail(new string[]
					{
						strWhere
					});
					if (this.MySoapHeader.SerializeRequired)
					{
						HttpContext.Current.Response.Write(ConvertSerialize.CompactFormatterSerialize(dtMem));
					}
					else
					{
						string json = JsonPlus.ToJson(dtMem.Rows[0], "MemID,MemCard,MemPassword,MemName,MemSex,MemIdentityCard,MemMobile,MemPhoto,MemBirthdayType,MemBirthday,MemIsPast,MemPastTime,MemPoint,MemPointAutomatic,MemMoney,MemConsumeMoney,MemConsumeLastTime,MemConsumeCount,MemEmail,MemAddress,MemState,MemRecommendID,MemLevelID,MemShopID,MemCreateTime,MemRemark,MemUserID,MemTelePhone,MemQRCode,MemProvince,MemCity,MemCounty,MemVillage,MemQuestion,MemAnswer,MemWeiXinCard,MemCardNumber,ShopName,LevelID,LevelName,LevelPoint,LevelDiscountPercent,LevelPointPercent,LevellLock,ClassDiscountPercent,ClassPointPercent,ConsumeMoney,UserName,MemProvinceName,MemCityName,MemCountyName,MemVillageName,MemCountNumber,ClassRechargePointRate");
						HttpContext.Current.Response.Write(jsonCallBackFunName + "(" + json + ")");
					}
				}
			}
			catch
			{
			}
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public void QuickExpense()
		{
			HttpContext.Current.Response.ContentType = "application/json;charset=utf-8";
			string jsonCallBackFunName = string.Empty;
			jsonCallBackFunName = HttpContext.Current.Request.Params["callback"].ToString();
			string memCard = HttpContext.Current.Request.Params["memcard"].ToString();
			string account = HttpContext.Current.Request.Params["account"].ToString();
			decimal money = Convert.ToDecimal(HttpContext.Current.Request.Params["money"].ToString());
			decimal discount = Convert.ToDecimal(HttpContext.Current.Request.Params["discount"].ToString());
			int point = Convert.ToInt32(HttpContext.Current.Request.Params["point"].ToString());
			string flag = "{\"tag\":\"\"}";
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			Chain.Model.Mem modelMem = bllMem.GetModel(memCard);
			string Remark = string.Concat(new object[]
			{
				"散客快速消费,订单号：[",
				account,
				"],消费金额：[",
				discount,
				"]"
			});
			if (PubFunction.IsShopPoint(modelMem.MemShopID, ref point))
			{
				try
				{
					Chain.Model.OrderLog mdOrderLog = new Chain.Model.OrderLog();
					Chain.Model.SysLog mdSysLog = new Chain.Model.SysLog();
					Chain.Model.PointLog mdPoint = new Chain.Model.PointLog();
					mdOrderLog.OrderMemID = modelMem.MemID;
					mdOrderLog.OrderType = 0;
					mdOrderLog.OrderPoint = point;
					mdOrderLog.OrderTotalMoney = money;
					mdOrderLog.OrderDiscountMoney = discount;
					mdOrderLog.OrderIsCard = false;
					mdOrderLog.OrderPayCard = 0m;
					mdOrderLog.OrderIsCash = true;
					mdOrderLog.OrderPayCash = discount;
					mdOrderLog.OrderIsBink = false;
					mdOrderLog.OrderPayBink = 0m;
					mdOrderLog.OrderPayCoupon = 0m;
					mdOrderLog.OrderAccount = account;
					mdOrderLog.OrderShopID = modelMem.MemShopID;
					mdOrderLog.OrderUserID = modelMem.MemUserID;
					mdOrderLog.OrderRemark = Remark;
					mdOrderLog.OrderCreateTime = DateTime.Now;
					mdOrderLog.OrderPayType = 0;
					mdOrderLog.OrderCardBalance = modelMem.MemMoney;
					mdOrderLog.OrderCardPoint = modelMem.MemPoint + point;
					Chain.BLL.OrderLog bllOrder = new Chain.BLL.OrderLog();
					int intSuccess = bllOrder.Add(mdOrderLog, account);
					if (intSuccess > 0)
					{
						decimal dclMemMoney = modelMem.MemMoney;
						modelMem.MemConsumeMoney += mdOrderLog.OrderDiscountMoney;
						modelMem.MemPoint += mdOrderLog.OrderPoint;
						modelMem.MemConsumeLastTime = DateTime.Now;
						modelMem.MemConsumeCount++;
						int mem = bllMem.ExpenseUpdateMem(modelMem.MemID, dclMemMoney, modelMem.MemConsumeMoney, modelMem.MemPoint, DateTime.Now, modelMem.MemConsumeCount);
						Chain.Model.MoneyChangeLog moneyChangeLogModel = new Chain.Model.MoneyChangeLog();
						moneyChangeLogModel.MoneyChangeMemID = modelMem.MemID;
						moneyChangeLogModel.MoneyChangeUserID = modelMem.MemUserID;
						moneyChangeLogModel.MoneyChangeType = 3;
						moneyChangeLogModel.MoneyChangeAccount = account;
						moneyChangeLogModel.MoneyChangeMoney = -discount;
						moneyChangeLogModel.MoneyChangeCash = -discount;
						moneyChangeLogModel.MoneyChangeBalance = 0m;
						moneyChangeLogModel.MoneyChangeUnionPay = 0m;
						moneyChangeLogModel.MemMoney = modelMem.MemMoney;
						moneyChangeLogModel.MoneyChangeCreateTime = DateTime.Now;
						moneyChangeLogModel.MoneyChangeGiveMoney = 0m;
						new Chain.BLL.MoneyChangeLog().Add(moneyChangeLogModel);
						mdPoint.PointMemID = modelMem.MemID;
						mdPoint.PointNumber = point;
						mdPoint.PointChangeType = 2;
						mdPoint.PointRemark = string.Concat(new object[]
						{
							"会员快速消费，消费金额：[",
							discount,
							"],获得积分：",
							point,
							"]"
						});
						mdPoint.PointShopID = modelMem.MemShopID;
						mdPoint.PointCreateTime = DateTime.Now;
						mdPoint.PointUserID = modelMem.MemUserID;
						mdPoint.PointOrderCode = account;
						new Chain.BLL.PointLog().Add(mdPoint);
						if (PubFunction.curParameter.bolShopPointManage)
						{
							PubFunction.SetShopPoint(modelMem.MemUserID, modelMem.MemShopID, point, "会员快速消费扣除商家积分", 2);
						}
						MEMPointUpdate.MEMPointRate(modelMem, mdOrderLog.OrderPoint, mdOrderLog.OrderAccount, 2, modelMem.MemUserID, 1);
						modelMem = new Chain.BLL.Mem().GetModel(modelMem.MemID);
						string strUpdateMemLevel = PubFunction.UpdateMemLevel(modelMem);
						flag = "{\"tag\":\"" + account + "\"}";
						PubFunction.SaveSysLog(modelMem.MemUserID, 4, "会员消费", Remark, modelMem.MemShopID, DateTime.Now, PubFunction.ipAdress);
					}
				}
				catch
				{
					flag = "{\"tag\":\"\"}";
				}
			}
			HttpContext.Current.Response.Write(jsonCallBackFunName + "(" + flag + ")");
		}

		[SoapHeader("MySoapHeader"), WebMethod(Description = "")]
		public void AddErrorLog(Chain.Model.SysError mdSysError)
		{
			if (this.MySoapHeader.CheckUser())
			{
				new Chain.BLL.SysError().Add(mdSysError);
			}
		}
	}
}
