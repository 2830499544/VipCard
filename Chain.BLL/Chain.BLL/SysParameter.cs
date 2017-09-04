using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysParameter
	{
		private readonly Chain.IDAL.SysParameter dal = new Chain.IDAL.SysParameter();

		public bool Exists(int ParameterID)
		{
			return this.dal.Exists(ParameterID);
		}

		public int Add(Chain.Model.SysParameter model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SysParameter model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ParameterID)
		{
			return this.dal.Delete(ParameterID);
		}

		public bool DeleteList(string ParameterIDlist)
		{
			return this.dal.DeleteList(ParameterIDlist);
		}

		public Chain.Model.SysParameter GetModel(int ParameterID)
		{
			return this.dal.GetModel(ParameterID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysParameter> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysParameter> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysParameter> modelList = new List<Chain.Model.SysParameter>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysParameter model = new Chain.Model.SysParameter();
					if (dt.Rows[i]["ParameterID"] != null && dt.Rows[i]["ParameterID"].ToString() != "")
					{
						model.ParameterID = int.Parse(dt.Rows[i]["ParameterID"].ToString());
					}
					if (dt.Rows[i]["Pwd"] != null && dt.Rows[i]["Pwd"].ToString() != "")
					{
						if (dt.Rows[i]["Pwd"].ToString() == "1" || dt.Rows[i]["Pwd"].ToString().ToLower() == "true")
						{
							model.Pwd = true;
						}
						else
						{
							model.Pwd = false;
						}
					}
					if (dt.Rows[i]["MoneyAndPoint"] != null && dt.Rows[i]["MoneyAndPoint"].ToString() != "")
					{
						if (dt.Rows[i]["MoneyAndPoint"].ToString() == "1" || dt.Rows[i]["MoneyAndPoint"].ToString().ToLower() == "true")
						{
							model.MoneyAndPoint = true;
						}
						else
						{
							model.MoneyAndPoint = false;
						}
					}
					if (dt.Rows[i]["AutoLevel"] != null && dt.Rows[i]["AutoLevel"].ToString() != "")
					{
						if (dt.Rows[i]["AutoLevel"].ToString() == "1" || dt.Rows[i]["AutoLevel"].ToString().ToLower() == "true")
						{
							model.AutoLevel = true;
						}
						else
						{
							model.AutoLevel = false;
						}
					}
					if (dt.Rows[i]["DegradeLevel"] != null && dt.Rows[i]["DegradeLevel"].ToString() != "")
					{
						if (dt.Rows[i]["DegradeLevel"].ToString() == "1" || dt.Rows[i]["DegradeLevel"].ToString().ToLower() == "true")
						{
							model.DegradeLevel = true;
						}
						else
						{
							model.DegradeLevel = false;
						}
					}
					if (dt.Rows[i]["PastTime"] != null && dt.Rows[i]["PastTime"].ToString() != "")
					{
						if (dt.Rows[i]["PastTime"].ToString() == "1" || dt.Rows[i]["PastTime"].ToString().ToLower() == "true")
						{
							model.PastTime = true;
						}
						else
						{
							model.PastTime = false;
						}
					}
					if (dt.Rows[i]["RecommendPoint"] != null && dt.Rows[i]["RecommendPoint"].ToString() != "")
					{
						model.RecommendPoint = int.Parse(dt.Rows[i]["RecommendPoint"].ToString());
					}
					if (dt.Rows[i]["PointPeriod"] != null && dt.Rows[i]["PointPeriod"].ToString() != "")
					{
						model.PointPeriod = int.Parse(dt.Rows[i]["PointPeriod"].ToString());
					}
					if (dt.Rows[i]["ExpensePrefix"] != null && dt.Rows[i]["ExpensePrefix"].ToString() != "")
					{
						model.ExpensePrefix = dt.Rows[i]["ExpensePrefix"].ToString();
					}
					if (dt.Rows[i]["GoodsExpensePrefix"] != null && dt.Rows[i]["GoodsExpensePrefix"].ToString() != "")
					{
						model.GoodsExpensePrefix = dt.Rows[i]["GoodsExpensePrefix"].ToString();
					}
					if (dt.Rows[i]["TimeExpensePrefix"] != null && dt.Rows[i]["TimeExpensePrefix"].ToString() != "")
					{
						model.TimeExpensePrefix = dt.Rows[i]["TimeExpensePrefix"].ToString();
					}
					if (dt.Rows[i]["MemCountPrefix"] != null && dt.Rows[i]["MemCountPrefix"].ToString() != "")
					{
						model.MemCountPrefix = dt.Rows[i]["MemCountPrefix"].ToString();
					}
					if (dt.Rows[i]["MemRechargePrefix"] != null && dt.Rows[i]["MemRechargePrefix"].ToString() != "")
					{
						model.MemRechargePrefix = dt.Rows[i]["MemRechargePrefix"].ToString();
					}
					if (dt.Rows[i]["GoodsInPrefix"] != null && dt.Rows[i]["GoodsInPrefix"].ToString() != "")
					{
						model.GoodsInPrefix = dt.Rows[i]["GoodsInPrefix"].ToString();
					}
					if (dt.Rows[i]["GoodsAllotPrefix"] != null && dt.Rows[i]["GoodsAllotPrefix"].ToString() != "")
					{
						model.GoodsAllotPrefix = dt.Rows[i]["GoodsAllotPrefix"].ToString();
					}
					if (dt.Rows[i]["MemDrawMoneyPrefix"] != null && dt.Rows[i]["MemDrawMoneyPrefix"].ToString() != "")
					{
						model.MemDrawMoneyPrefix = dt.Rows[i]["MemDrawMoneyPrefix"].ToString();
					}
					if (dt.Rows[i]["MemPointChangePrefix"] != null && dt.Rows[i]["MemPointChangePrefix"].ToString() != "")
					{
						model.MemPointChangePrefix = dt.Rows[i]["MemPointChangePrefix"].ToString();
					}
					if (dt.Rows[i]["GiftExchangePrefix"] != null && dt.Rows[i]["GiftExchangePrefix"].ToString() != "")
					{
						model.GiftExchangePrefix = dt.Rows[i]["GiftExchangePrefix"].ToString();
					}
					if (dt.Rows[i]["AutoPrint"] != null && dt.Rows[i]["AutoPrint"].ToString() != "")
					{
						if (dt.Rows[i]["AutoPrint"].ToString() == "1" || dt.Rows[i]["AutoPrint"].ToString().ToLower() == "true")
						{
							model.AutoPrint = true;
						}
						else
						{
							model.AutoPrint = false;
						}
					}
					if (dt.Rows[i]["AccordPrint"] != null && dt.Rows[i]["AccordPrint"].ToString() != "")
					{
						if (dt.Rows[i]["AccordPrint"].ToString() == "1" || dt.Rows[i]["AccordPrint"].ToString().ToLower() == "true")
						{
							model.AccordPrint = true;
						}
						else
						{
							model.AccordPrint = false;
						}
					}
					if (dt.Rows[i]["PrintTitle"] != null && dt.Rows[i]["PrintTitle"].ToString() != "")
					{
						model.PrintTitle = dt.Rows[i]["PrintTitle"].ToString();
					}
					if (dt.Rows[i]["PrintFootNote"] != null && dt.Rows[i]["PrintFootNote"].ToString() != "")
					{
						model.PrintFootNote = dt.Rows[i]["PrintFootNote"].ToString();
					}
					if (dt.Rows[i]["Sms"] != null && dt.Rows[i]["Sms"].ToString() != "")
					{
						if (dt.Rows[i]["Sms"].ToString() == "1" || dt.Rows[i]["Sms"].ToString().ToLower() == "true")
						{
							model.Sms = true;
						}
						else
						{
							model.Sms = false;
						}
					}
					if (dt.Rows[i]["MoneySms"] != null && dt.Rows[i]["MoneySms"].ToString() != "")
					{
						if (dt.Rows[i]["MoneySms"].ToString() == "1" || dt.Rows[i]["MoneySms"].ToString().ToLower() == "true")
						{
							model.MoneySms = true;
						}
						else
						{
							model.MoneySms = false;
						}
					}
					if (dt.Rows[i]["IsSmsShopName"] != null && dt.Rows[i]["IsSmsShopName"].ToString() != "")
					{
						if (dt.Rows[i]["IsSmsShopName"].ToString() == "1" || dt.Rows[i]["IsSmsShopName"].ToString().ToLower() == "true")
						{
							model.IsSmsShopName = true;
						}
						else
						{
							model.IsSmsShopName = false;
						}
					}
					if (dt.Rows[i]["SmsShopName"] != null && dt.Rows[i]["SmsShopName"].ToString() != "")
					{
						model.SmsShopName = dt.Rows[i]["SmsShopName"].ToString();
					}
					if (dt.Rows[i]["SmsSeries"] != null && dt.Rows[i]["SmsSeries"].ToString() != "")
					{
						model.SmsSeries = dt.Rows[i]["SmsSeries"].ToString();
					}
					if (dt.Rows[i]["SmsSerialPwd"] != null && dt.Rows[i]["SmsSerialPwd"].ToString() != "")
					{
						model.SmsSerialPwd = dt.Rows[i]["SmsSerialPwd"].ToString();
					}
					if (dt.Rows[i]["DrawMoneyPercent"] != null && dt.Rows[i]["DrawMoneyPercent"].ToString() != "")
					{
						model.DrawMoneyPercent = decimal.Parse(dt.Rows[i]["DrawMoneyPercent"].ToString());
					}
					if (dt.Rows[i]["Tel"] != null && dt.Rows[i]["Tel"].ToString() != "")
					{
						if (dt.Rows[i]["Tel"].ToString() == "1" || dt.Rows[i]["Tel"].ToString().ToLower() == "true")
						{
							model.Tel = true;
						}
						else
						{
							model.Tel = false;
						}
					}
					if (dt.Rows[i]["TelNoMember"] != null && dt.Rows[i]["TelNoMember"].ToString() != "")
					{
						if (dt.Rows[i]["TelNoMember"].ToString() == "1" || dt.Rows[i]["TelNoMember"].ToString().ToLower() == "true")
						{
							model.TelNoMember = true;
						}
						else
						{
							model.TelNoMember = false;
						}
					}
					if (dt.Rows[i]["IsStaff"] != null && dt.Rows[i]["IsStaff"].ToString() != "")
					{
						if (dt.Rows[i]["IsStaff"].ToString() == "1" || dt.Rows[i]["IsStaff"].ToString().ToLower() == "true")
						{
							model.IsStaff = true;
						}
						else
						{
							model.IsStaff = false;
						}
					}
					if (dt.Rows[i]["StaffType"] != null && dt.Rows[i]["StaffType"].ToString() != "")
					{
						if (dt.Rows[i]["StaffType"].ToString() == "1" || dt.Rows[i]["StaffType"].ToString().ToLower() == "true")
						{
							model.StaffType = true;
						}
						else
						{
							model.StaffType = false;
						}
					}
					if (dt.Rows[i]["PointLevel"] != null && dt.Rows[i]["PointLevel"].ToString() != "")
					{
						if (dt.Rows[i]["PointLevel"].ToString() == "1" || dt.Rows[i]["PointLevel"].ToString().ToLower() == "true")
						{
							model.PointLevel = true;
						}
						else
						{
							model.PointLevel = false;
						}
					}
					if (dt.Rows[i]["MMS"] != null && dt.Rows[i]["MMS"].ToString() != "")
					{
						if (dt.Rows[i]["MMS"].ToString() == "1" || dt.Rows[i]["MMS"].ToString().ToLower() == "true")
						{
							model.MMS = true;
						}
						else
						{
							model.MMS = false;
						}
					}
					if (dt.Rows[i]["MMSSeries"] != null && dt.Rows[i]["MMSSeries"].ToString() != "")
					{
						model.MMSSeries = dt.Rows[i]["MMSSeries"].ToString();
					}
					if (dt.Rows[i]["MMSSerialPwd"] != null && dt.Rows[i]["MMSSerialPwd"].ToString() != "")
					{
						model.MMSSerialPwd = dt.Rows[i]["MMSSerialPwd"].ToString();
					}
					if (dt.Rows[i]["IsPayCard"] != null && dt.Rows[i]["IsPayCard"].ToString() != "")
					{
						if (dt.Rows[i]["IsPayCard"].ToString() == "1" || dt.Rows[i]["IsPayCard"].ToString().ToLower() == "true")
						{
							model.IsPayCard = true;
						}
						else
						{
							model.IsPayCard = false;
						}
					}
					if (dt.Rows[i]["IsPayCash"] != null && dt.Rows[i]["IsPayCash"].ToString() != "")
					{
						if (dt.Rows[i]["IsPayCash"].ToString() == "1" || dt.Rows[i]["IsPayCash"].ToString().ToLower() == "true")
						{
							model.IsPayCash = true;
						}
						else
						{
							model.IsPayCash = false;
						}
					}
					if (dt.Rows[i]["IsPayBink"] != null && dt.Rows[i]["IsPayBink"].ToString() != "")
					{
						if (dt.Rows[i]["IsPayBink"].ToString() == "1" || dt.Rows[i]["IsPayBink"].ToString().ToLower() == "true")
						{
							model.IsPayBink = true;
						}
						else
						{
							model.IsPayBink = false;
						}
					}
					if (dt.Rows[i]["IsPayCoupon"] != null && dt.Rows[i]["IsPayCoupon"].ToString() != "")
					{
						if (dt.Rows[i]["IsPayCoupon"].ToString() == "1" || dt.Rows[i]["IsPayCoupon"].ToString().ToLower() == "true")
						{
							model.IsPayCoupon = true;
						}
						else
						{
							model.IsPayCoupon = false;
						}
					}
					if (dt.Rows[i]["RegNullPwd"] != null && dt.Rows[i]["RegNullPwd"].ToString() != "")
					{
						if (dt.Rows[i]["RegNullPwd"].ToString() == "1" || dt.Rows[i]["RegNullPwd"].ToString().ToLower() == "true")
						{
							model.RegNullPwd = true;
						}
						else
						{
							model.RegNullPwd = false;
						}
					}
					if (dt.Rows[i]["EmailAdress"] != null && dt.Rows[i]["EmailAdress"].ToString() != "")
					{
						model.EmailAdress = dt.Rows[i]["EmailAdress"].ToString();
					}
					if (dt.Rows[i]["EmailPwd"] != null && dt.Rows[i]["EmailPwd"].ToString() != "")
					{
						model.EmailPwd = dt.Rows[i]["EmailPwd"].ToString();
					}
					if (dt.Rows[i]["EmailSMTP"] != null && dt.Rows[i]["EmailSMTP"].ToString() != "")
					{
						model.EmailSMTP = dt.Rows[i]["EmailSMTP"].ToString();
					}
					if (dt.Rows[i]["StockCount"] != null && dt.Rows[i]["StockCount"].ToString() != "")
					{
						model.StockCount = int.Parse(dt.Rows[i]["StockCount"].ToString());
					}
					if (dt.Rows[i]["UnitList"] != null && dt.Rows[i]["UnitList"].ToString() != "")
					{
						model.UnitList = dt.Rows[i]["UnitList"].ToString();
					}
					if (dt.Rows[i]["WeiXinSMSVcode"] != null && dt.Rows[i]["WeiXinSMSVcode"].ToString() != "")
					{
						if (dt.Rows[i]["WeiXinSMSVcode"].ToString() == "1" || dt.Rows[i]["WeiXinSMSVcode"].ToString().ToLower() == "true")
						{
							model.WeiXinSMSVcode = true;
						}
						else
						{
							model.WeiXinSMSVcode = false;
						}
					}
					if (dt.Rows[i]["IsAutoSendSMSByMemRegister"] != null && dt.Rows[i]["IsAutoSendSMSByMemRegister"].ToString() != "")
					{
						if (dt.Rows[i]["IsAutoSendSMSByMemRegister"].ToString() == "1" || dt.Rows[i]["IsAutoSendSMSByMemRegister"].ToString().ToLower() == "true")
						{
							model.IsAutoSendSMSByMemRegister = true;
						}
						else
						{
							model.IsAutoSendSMSByMemRegister = false;
						}
					}
					if (dt.Rows[i]["IsAutoSendMMSByMemRegister"] != null && dt.Rows[i]["IsAutoSendMMSByMemRegister"].ToString() != "")
					{
						if (dt.Rows[i]["IsAutoSendMMSByMemRegister"].ToString() == "1" || dt.Rows[i]["IsAutoSendMMSByMemRegister"].ToString().ToLower() == "true")
						{
							model.IsAutoSendMMSByMemRegister = true;
						}
						else
						{
							model.IsAutoSendMMSByMemRegister = false;
						}
					}
					if (dt.Rows[i]["IsAutoSendSMSByMemRecharge"] != null && dt.Rows[i]["IsAutoSendSMSByMemRecharge"].ToString() != "")
					{
						if (dt.Rows[i]["IsAutoSendSMSByMemRecharge"].ToString() == "1" || dt.Rows[i]["IsAutoSendSMSByMemRecharge"].ToString().ToLower() == "true")
						{
							model.IsAutoSendSMSByMemRecharge = true;
						}
						else
						{
							model.IsAutoSendSMSByMemRecharge = false;
						}
					}
					if (dt.Rows[i]["IsAutoSendSMSByMemWithdraw"] != null && dt.Rows[i]["IsAutoSendSMSByMemWithdraw"].ToString() != "")
					{
						if (dt.Rows[i]["IsAutoSendSMSByMemWithdraw"].ToString() == "1" || dt.Rows[i]["IsAutoSendSMSByMemWithdraw"].ToString().ToLower() == "true")
						{
							model.IsAutoSendSMSByMemWithdraw = true;
						}
						else
						{
							model.IsAutoSendSMSByMemWithdraw = false;
						}
					}
					if (dt.Rows[i]["IsAutoSendSMSByMemGiftExchange"] != null && dt.Rows[i]["IsAutoSendSMSByMemGiftExchange"].ToString() != "")
					{
						if (dt.Rows[i]["IsAutoSendSMSByMemGiftExchange"].ToString() == "1" || dt.Rows[i]["IsAutoSendSMSByMemGiftExchange"].ToString().ToLower() == "true")
						{
							model.IsAutoSendSMSByMemGiftExchange = true;
						}
						else
						{
							model.IsAutoSendSMSByMemGiftExchange = false;
						}
					}
					if (dt.Rows[i]["IsAutoSendSMSByMemPointChange"] != null && dt.Rows[i]["IsAutoSendSMSByMemPointChange"].ToString() != "")
					{
						if (dt.Rows[i]["IsAutoSendSMSByMemPointChange"].ToString() == "1" || dt.Rows[i]["IsAutoSendSMSByMemPointChange"].ToString().ToLower() == "true")
						{
							model.IsAutoSendSMSByMemPointChange = true;
						}
						else
						{
							model.IsAutoSendSMSByMemPointChange = false;
						}
					}
					if (dt.Rows[i]["IsAutoSendSMSByCommodityConsumption"] != null && dt.Rows[i]["IsAutoSendSMSByCommodityConsumption"].ToString() != "")
					{
						if (dt.Rows[i]["IsAutoSendSMSByCommodityConsumption"].ToString() == "1" || dt.Rows[i]["IsAutoSendSMSByCommodityConsumption"].ToString().ToLower() == "true")
						{
							model.IsAutoSendSMSByCommodityConsumption = true;
						}
						else
						{
							model.IsAutoSendSMSByCommodityConsumption = false;
						}
					}
					if (dt.Rows[i]["IsAutoSendSMSByFastConsumption"] != null && dt.Rows[i]["IsAutoSendSMSByFastConsumption"].ToString() != "")
					{
						if (dt.Rows[i]["IsAutoSendSMSByFastConsumption"].ToString() == "1" || dt.Rows[i]["IsAutoSendSMSByFastConsumption"].ToString().ToLower() == "true")
						{
							model.IsAutoSendSMSByFastConsumption = true;
						}
						else
						{
							model.IsAutoSendSMSByFastConsumption = false;
						}
					}
					if (dt.Rows[i]["IsAutoSendSMSByMemRedTimes"] != null && dt.Rows[i]["IsAutoSendSMSByMemRedTimes"].ToString() != "")
					{
						if (dt.Rows[i]["IsAutoSendSMSByMemRedTimes"].ToString() == "1" || dt.Rows[i]["IsAutoSendSMSByMemRedTimes"].ToString().ToLower() == "true")
						{
							model.IsAutoSendSMSByMemRedTimes = true;
						}
						else
						{
							model.IsAutoSendSMSByMemRedTimes = false;
						}
					}
					if (dt.Rows[i]["IsAutoSendSMSByTimingConsumption"] != null && dt.Rows[i]["IsAutoSendSMSByTimingConsumption"].ToString() != "")
					{
						if (dt.Rows[i]["IsAutoSendSMSByTimingConsumption"].ToString() == "1" || dt.Rows[i]["IsAutoSendSMSByTimingConsumption"].ToString().ToLower() == "true")
						{
							model.IsAutoSendSMSByTimingConsumption = true;
						}
						else
						{
							model.IsAutoSendSMSByTimingConsumption = false;
						}
					}
					if (dt.Rows[i]["SellerAccount"] != null && dt.Rows[i]["SellerAccount"].ToString() != "")
					{
						model.SellerAccount = dt.Rows[i]["SellerAccount"].ToString();
					}
					if (dt.Rows[i]["PartnerID"] != null && dt.Rows[i]["PartnerID"].ToString() != "")
					{
						model.PartnerID = dt.Rows[i]["PartnerID"].ToString();
					}
					if (dt.Rows[i]["PartnerKey"] != null && dt.Rows[i]["PartnerKey"].ToString() != "")
					{
						model.PartnerKey = dt.Rows[i]["PartnerKey"].ToString();
					}
					if (dt.Rows[i]["IsEditPwdNeedOldPwd"] != null && dt.Rows[i]["IsEditPwdNeedOldPwd"].ToString() != "")
					{
						if (dt.Rows[i]["IsEditPwdNeedOldPwd"].ToString() == "1" || dt.Rows[i]["IsEditPwdNeedOldPwd"].ToString().ToLower() == "true")
						{
							model.IsEditPwdNeedOldPwd = true;
						}
						else
						{
							model.IsEditPwdNeedOldPwd = false;
						}
					}
					if (dt.Rows[i]["WeiXinType"] != null && dt.Rows[i]["WeiXinType"].ToString() != "")
					{
						if (dt.Rows[i]["WeiXinType"].ToString() == "1" || dt.Rows[i]["WeiXinType"].ToString().ToLower() == "true")
						{
							model.WeiXinType = true;
						}
						else
						{
							model.WeiXinType = false;
						}
					}
					if (dt.Rows[i]["WeiXinVerified"] != null && dt.Rows[i]["WeiXinVerified"].ToString() != "")
					{
						if (dt.Rows[i]["WeiXinVerified"].ToString() == "1" || dt.Rows[i]["WeiXinVerified"].ToString().ToLower() == "true")
						{
							model.WeiXinVerified = true;
						}
						else
						{
							model.WeiXinVerified = false;
						}
					}
					if (dt.Rows[i]["WeiXinToken"] != null && dt.Rows[i]["WeiXinToken"].ToString() != "")
					{
						model.WeiXinToken = dt.Rows[i]["WeiXinToken"].ToString();
					}
					if (dt.Rows[i]["WeiXinEncodingAESKey"] != null && dt.Rows[i]["WeiXinEncodingAESKey"].ToString() != "")
					{
						model.WeiXinEncodingAESKey = dt.Rows[i]["WeiXinEncodingAESKey"].ToString();
					}
					if (dt.Rows[i]["WeiXinShopName"] != null && dt.Rows[i]["WeiXinShopName"].ToString() != "")
					{
						model.WeiXinShopName = dt.Rows[i]["WeiXinShopName"].ToString();
					}
					if (dt.Rows[i]["WeiXinSalutatory"] != null && dt.Rows[i]["WeiXinSalutatory"].ToString() != "")
					{
						model.WeiXinSalutatory = dt.Rows[i]["WeiXinSalutatory"].ToString();
					}
					if (dt.Rows[i]["WeiXinAppID"] != null && dt.Rows[i]["WeiXinAppID"].ToString() != "")
					{
						model.WeiXinAppID = dt.Rows[i]["WeiXinAppID"].ToString();
					}
					if (dt.Rows[i]["WeiXinAppSecret"] != null && dt.Rows[i]["WeiXinAppSecret"].ToString() != "")
					{
						model.WeiXinAppSecret = dt.Rows[i]["WeiXinAppSecret"].ToString();
					}
					if (dt.Rows[i]["SignInPoint"] != null && dt.Rows[i]["SignInPoint"].ToString() != "")
					{
						model.SignInPoint = int.Parse(dt.Rows[i]["SignInPoint"].ToString());
					}
					if (dt.Rows[i]["IsMemRegisterStaff"] != null && dt.Rows[i]["IsMemRegisterStaff"].ToString() != "")
					{
						if (dt.Rows[i]["IsMemRegisterStaff"].ToString() == "1" || dt.Rows[i]["IsMemRegisterStaff"].ToString().ToLower() == "true")
						{
							model.IsMemRegisterStaff = true;
						}
						else
						{
							model.IsMemRegisterStaff = false;
						}
					}
					if (dt.Rows[i]["IsMustSlotCard"] != null && dt.Rows[i]["IsMustSlotCard"].ToString() != "")
					{
						if (dt.Rows[i]["IsMustSlotCard"].ToString() == "1" || dt.Rows[i]["IsMustSlotCard"].ToString().ToLower() == "true")
						{
							model.IsMustSlotCard = true;
						}
						else
						{
							model.IsMustSlotCard = false;
						}
					}
					if (dt.Rows[i]["StorageTimingPrefix"] != null && dt.Rows[i]["StorageTimingPrefix"].ToString() != "")
					{
						model.StorageTimingPrefix = dt.Rows[i]["StorageTimingPrefix"].ToString();
					}
					if (dt.Rows[i]["IsAutoSendSMSByStorageTiming"] != null && dt.Rows[i]["IsAutoSendSMSByStorageTiming"].ToString() != "")
					{
						if (dt.Rows[i]["IsAutoSendSMSByStorageTiming"].ToString() == "1" || dt.Rows[i]["IsAutoSendSMSByStorageTiming"].ToString().ToLower() == "true")
						{
							model.IsAutoSendSMSByStorageTiming = true;
						}
						else
						{
							model.IsAutoSendSMSByStorageTiming = false;
						}
					}
					if (dt.Rows[i]["EnterpriseEmailPort"] != null && dt.Rows[i]["EnterpriseEmailPort"].ToString() != "")
					{
						model.EnterpriseEmailPort = int.Parse(dt.Rows[i]["EnterpriseEmailPort"].ToString());
					}
					if (dt.Rows[i]["EnterpriseEmailDisplayName"] != null && dt.Rows[i]["EnterpriseEmailDisplayName"].ToString() != "")
					{
						model.EnterpriseEmailDisplayName = dt.Rows[i]["EnterpriseEmailDisplayName"].ToString();
					}
					if (dt.Rows[i]["EnterpriseEmailEnableSSL"] != null && dt.Rows[i]["EnterpriseEmailEnableSSL"].ToString() != "")
					{
						if (dt.Rows[i]["EnterpriseEmailEnableSSL"].ToString() == "1" || dt.Rows[i]["EnterpriseEmailEnableSSL"].ToString().ToLower() == "true")
						{
							model.EnterpriseEmailEnableSSL = true;
						}
						else
						{
							model.EnterpriseEmailEnableSSL = false;
						}
					}
					if (dt.Rows[i]["EnterpriseEmailUseDefaultCredentials"] != null && dt.Rows[i]["EnterpriseEmailUseDefaultCredentials"].ToString() != "")
					{
						if (dt.Rows[i]["EnterpriseEmailUseDefaultCredentials"].ToString() == "1" || dt.Rows[i]["EnterpriseEmailUseDefaultCredentials"].ToString().ToLower() == "true")
						{
							model.EnterpriseEmailUseDefaultCredentials = true;
						}
						else
						{
							model.EnterpriseEmailUseDefaultCredentials = false;
						}
					}
					if (dt.Rows[i]["IsEmail"] != null && dt.Rows[i]["IsEmail"].ToString() != "")
					{
						if (dt.Rows[i]["IsEmail"].ToString() == "1" || dt.Rows[i]["IsEmail"].ToString().ToLower() == "true")
						{
							model.IsEmail = true;
						}
						else
						{
							model.IsEmail = false;
						}
					}
					if (dt.Rows[i]["IsEmailNotice"] != null && dt.Rows[i]["IsEmailNotice"].ToString() != "")
					{
						if (dt.Rows[i]["IsEmailNotice"].ToString() == "1" || dt.Rows[i]["IsEmailNotice"].ToString().ToLower() == "true")
						{
							model.IsEmailNotice = true;
						}
						else
						{
							model.IsEmailNotice = false;
						}
					}
					if (dt.Rows[i]["MemCountExpensePrefix"] != null && dt.Rows[i]["MemCountExpensePrefix"].ToString() != "")
					{
						model.MemCountExpensePrefix = dt.Rows[i]["MemCountExpensePrefix"].ToString();
					}
					if (dt.Rows[i]["IsAutoSendSMSByMemPast"] != null && dt.Rows[i]["IsAutoSendSMSByMemPast"].ToString() != "")
					{
						if (dt.Rows[i]["IsAutoSendSMSByMemPast"].ToString() == "1" || dt.Rows[i]["IsAutoSendSMSByMemPast"].ToString().ToLower() == "true")
						{
							model.IsAutoSendSMSByMemPast = true;
						}
						else
						{
							model.IsAutoSendSMSByMemPast = false;
						}
					}
					if (dt.Rows[i]["AutoSendSMSByMemPastForDay"] != null && dt.Rows[i]["AutoSendSMSByMemPastForDay"].ToString() != "")
					{
						model.AutoSendSMSByMemPastForDay = int.Parse(dt.Rows[i]["AutoSendSMSByMemPastForDay"].ToString());
					}
					if (dt.Rows[i]["IsAutoSendSMSByMemBirthday"] != null && dt.Rows[i]["IsAutoSendSMSByMemBirthday"].ToString() != "")
					{
						if (dt.Rows[i]["IsAutoSendSMSByMemBirthday"].ToString() == "1" || dt.Rows[i]["IsAutoSendSMSByMemBirthday"].ToString().ToLower() == "true")
						{
							model.IsAutoSendSMSByMemBirthday = true;
						}
						else
						{
							model.IsAutoSendSMSByMemBirthday = false;
						}
					}
					if (dt.Rows[i]["AutoSendSMSByMemBirthdayForDay"] != null && dt.Rows[i]["AutoSendSMSByMemBirthdayForDay"].ToString() != "")
					{
						model.AutoSendSMSByMemBirthdayForDay = int.Parse(dt.Rows[i]["AutoSendSMSByMemBirthdayForDay"].ToString());
					}
					if (dt.Rows[i]["IsStartWeiXin"] != null && dt.Rows[i]["IsStartWeiXin"].ToString() != "")
					{
						if (dt.Rows[i]["IsStartWeiXin"].ToString() == "1" || dt.Rows[i]["IsStartWeiXin"].ToString().ToLower() == "true")
						{
							model.IsStartWeiXin = true;
						}
						else
						{
							model.IsStartWeiXin = false;
						}
					}
					if (dt.Rows[i]["IsStartTimingProject"] != null && dt.Rows[i]["IsStartTimingProject"].ToString() != "")
					{
						if (dt.Rows[i]["IsStartTimingProject"].ToString() == "1" || dt.Rows[i]["IsStartTimingProject"].ToString().ToLower() == "true")
						{
							model.IsStartTimingProject = true;
						}
						else
						{
							model.IsStartTimingProject = false;
						}
					}
					if (dt.Rows[i]["IsStartMemCount"] != null && dt.Rows[i]["IsStartMemCount"].ToString() != "")
					{
						if (dt.Rows[i]["IsStartMemCount"].ToString() == "1" || dt.Rows[i]["IsStartMemCount"].ToString().ToLower() == "true")
						{
							model.IsStartMemCount = true;
						}
						else
						{
							model.IsStartMemCount = false;
						}
					}
					if (dt.Rows[i]["MarketingSMS"] != null && dt.Rows[i]["MarketingSMS"].ToString() != "")
					{
						if (dt.Rows[i]["MarketingSMS"].ToString() == "1" || dt.Rows[i]["MarketingSMS"].ToString().ToLower() == "true")
						{
							model.MarketingSMS = true;
						}
						else
						{
							model.MarketingSMS = false;
						}
					}
					if (dt.Rows[i]["MarketingSmsSeries"] != null && dt.Rows[i]["MarketingSmsSeries"].ToString() != "")
					{
						model.MarketingSmsSeries = dt.Rows[i]["MarketingSmsSeries"].ToString();
					}
					if (dt.Rows[i]["MarketingSmsSerialPwd"] != null && dt.Rows[i]["MarketingSmsSerialPwd"].ToString() != "")
					{
						model.MarketingSmsSerialPwd = dt.Rows[i]["MarketingSmsSerialPwd"].ToString();
					}
					if (dt.Rows[i]["Senseiccard"] != null && dt.Rows[i]["Senseiccard"].ToString() != "")
					{
						if (dt.Rows[i]["Senseiccard"].ToString() == "1" || dt.Rows[i]["Senseiccard"].ToString().ToLower() == "true")
						{
							model.Senseiccard = true;
						}
						else
						{
							model.Senseiccard = false;
						}
					}
					if (dt.Rows[i]["Contacticcard"] != null && dt.Rows[i]["Contacticcard"].ToString() != "")
					{
						if (dt.Rows[i]["Contacticcard"].ToString() == "1" || dt.Rows[i]["Contacticcard"].ToString().ToLower() == "true")
						{
							model.Contacticcard = true;
						}
						else
						{
							model.Contacticcard = false;
						}
					}
					if (dt.Rows[i]["EmailUserName"] != null && dt.Rows[i]["EmailUserName"].ToString() != "")
					{
						model.EmailUserName = dt.Rows[i]["EmailUserName"].ToString();
					}
					if (dt.Rows[i]["PointNumStr"] != null && dt.Rows[i]["PointNumStr"].ToString() != "")
					{
						model.PointNumStr = dt.Rows[i]["PointNumStr"].ToString();
					}
					if (dt.Rows[i]["PrintPreview"] != null && dt.Rows[i]["PrintPreview"].ToString() != "")
					{
						model.PrintPreview = int.Parse(dt.Rows[i]["PrintPreview"].ToString());
					}
					if (dt.Rows[i]["PrintPaperType"] != null && dt.Rows[i]["PrintPaperType"].ToString() != "")
					{
						model.PrintPaperType = int.Parse(dt.Rows[i]["PrintPaperType"].ToString());
					}
					if (dt.Rows[i]["IsSendCard"] != null && dt.Rows[i]["IsSendCard"].ToString() != "")
					{
						if (dt.Rows[i]["IsSendCard"].ToString() == "1" || dt.Rows[i]["IsSendCard"].ToString().ToLower() == "true")
						{
							model.IsSendCard = true;
						}
						else
						{
							model.IsSendCard = false;
						}
					}
					if (dt.Rows[i]["ShopSmsManage"] != null && dt.Rows[i]["ShopSmsManage"].ToString() != "")
					{
						if (dt.Rows[i]["ShopSmsManage"].ToString() == "1" || dt.Rows[i]["ShopSmsManage"].ToString().ToLower() == "true")
						{
							model.ShopSmsManage = true;
						}
						else
						{
							model.ShopSmsManage = false;
						}
					}
					if (dt.Rows[i]["ShopPointManage"] != null && dt.Rows[i]["ShopPointManage"].ToString() != "")
					{
						if (dt.Rows[i]["ShopPointManage"].ToString() == "1" || dt.Rows[i]["ShopPointManage"].ToString().ToLower() == "true")
						{
							model.ShopPointManage = true;
						}
						else
						{
							model.ShopPointManage = false;
						}
					}
					if (dt.Rows[i]["ShopSettlement"] != null && dt.Rows[i]["ShopSettlement"].ToString() != "")
					{
						if (dt.Rows[i]["ShopSettlement"].ToString() == "1" || dt.Rows[i]["ShopSettlement"].ToString().ToLower() == "true")
						{
							model.ShopSettlement = true;
						}
						else
						{
							model.ShopSettlement = false;
						}
					}
					if (dt.Rows[i]["AutoBackupDB"] != null && dt.Rows[i]["AutoBackupDB"].ToString() != "")
					{
						if (dt.Rows[i]["AutoBackupDB"].ToString() == "1" || dt.Rows[i]["AutoBackupDB"].ToString().ToLower() == "true")
						{
							model.AutoBackupDB = true;
						}
						else
						{
							model.AutoBackupDB = false;
						}
					}
					if (dt.Rows[i]["AutoBackupDay"] != null && dt.Rows[i]["AutoBackupDay"].ToString() != "")
					{
						model.AutoBackupDay = int.Parse(dt.Rows[i]["AutoBackupDay"].ToString());
					}
					if (dt.Rows[i]["SystemDomain"] != null && dt.Rows[i]["SystemDomain"].ToString() != "")
					{
						model.SystemDomain = dt.Rows[i]["SystemDomain"].ToString();
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		public DataSet GetAllList()
		{
			return this.GetList("");
		}

		public int GetRecordCount(string strWhere)
		{
			return this.dal.GetRecordCount(strWhere);
		}

		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool UpdateExtraParameter(Chain.Model.SysParameter model)
		{
			return this.dal.UpdateExtraParameter(model);
		}

		public int DataBakUp(string FileName)
		{
			return this.dal.DataBakUp(FileName);
		}

		public int ReductionDataBakUp(string FileName)
		{
			return this.dal.ReductionDataBakUp(FileName);
		}

		public string GetDataBaseName()
		{
			return this.dal.GetDataBaseName();
		}

		public bool SwitchingMode(List<bool> Status, List<string> ModuleIDs)
		{
			return this.dal.SwitchingMode(Status, ModuleIDs);
		}
	}
}
