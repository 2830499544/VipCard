using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class Mem
	{
		private readonly Chain.IDAL.Mem dal = new Chain.IDAL.Mem();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public int GetMemIDByMobile(string mobile)
		{
			return this.dal.GetMemIDByMobile(mobile);
		}

		public string GetWeiXinMemCardbyMemID(int MemID)
		{
			return this.dal.GetWeiXinMemCardbyMemID(MemID);
		}

		public bool Exists(int MemID)
		{
			return this.dal.Exists(MemID);
		}

		public bool ExistsMemCard(string MemCard)
		{
			return this.dal.ExistsMemCard(MemCard);
		}

		public int Exists(int MemID, string MemCard, string MemMobile, string MemCardNumber,int MemShopID)
		{
			return this.dal.Exists(MemID, MemCard, MemMobile, MemCardNumber, MemShopID);
		}

        /// <summary>
        /// 联盟商卡判断
        /// </summary>
        /// <param name="FatherID"></param>
        /// <param name="MemCard"></param>
        /// <param name="MemMobile"></param>
        /// <param name="MemCardNumber"></param>
        /// <param name="MemShopID"></param>
        /// <returns></returns>
        public int Exists_Alliance(int FatherID, string MemCard, string MemMobile, string MemCardNumber)
        {
            return this.dal.Exists_Alliance(FatherID, MemCard, MemMobile, MemCardNumber);
        }


        public DataSet GetItemAll(int MemID)
		{
			return this.dal.GetItemAll(MemID);
		}

		public int Add(Chain.Model.Mem model)
		{
            Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(model.MemShopID);
            int exists;
            //0是独立商，非0是联盟商
            if (modelShop.FatherShopID == 0)
                exists = this.Exists(model.MemID, model.MemCard, model.MemMobile, model.MemCardNumber, model.MemShopID);
            else
                exists = Exists_Alliance(modelShop.FatherShopID, model.MemCard, model.MemMobile, model.MemCardNumber);
			int result;
			if (exists != 1)
			{
				result = exists;
			}
			else
			{
				result = this.dal.Add(model);
			}
			return result;
		}

		public int MemRegister(string CardID, string LevelID, int userid, int shopid, string phone, string name)
		{
			return this.dal.MemRegister(CardID, LevelID, userid, shopid, phone, name);
		}

		public int AddCustomField(string MemCard, Hashtable customhash)
		{
			return this.dal.AddCustomField(MemCard, customhash);
		}

		public int Update(Chain.Model.Mem model)
		{
            Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(model.MemShopID);
            int exists = 0;
            //0是独立商，非0是联盟商
            if (modelShop.FatherShopID == 0)
                exists = this.Exists(model.MemID, model.MemCard, model.MemMobile, model.MemCardNumber, model.MemShopID);
            else
                exists = Exists_Alliance(modelShop.FatherShopID, model.MemCard, model.MemMobile, model.MemCardNumber);
            int result;
			if (exists != 1)
			{
				result = exists;
			}
			else
			{
				result = this.dal.Update(model);
			}
			return result;
		}

		public int UpdateMemSelf(Chain.Model.Mem model)
		{
			int exists = this.Exists(model.MemID, model.MemCard, model.MemMobile, "", model.MemShopID);
			int result;
			if (exists != 1)
			{
				result = exists;
			}
			else
			{
				result = this.dal.UpdateMemSelf(model);
			}
			return result;
		}

		public int ExpenseUpdateMem(int memID, decimal dclMemMoney, decimal memConsumeMoney, int point, DateTime dtime, int count)
		{
			return this.dal.ExpenseUpdateMem(memID, dclMemMoney, memConsumeMoney, point, dtime, count);
		}

		public int MemCountUpdateMem(int memID, decimal dclMemMoney, int point)
		{
			return this.dal.MemCountUpdateMem(memID, dclMemMoney, point);
		}

		public bool Delete(int MemID)
		{
			return this.dal.Delete(MemID);
		}

		public DataSet IsCanDelMem(int MemID)
		{
			return this.dal.IsCanDelMem(MemID);
		}

		public bool DeleteList(string MemIDlist)
		{
			return this.dal.DeleteList(MemIDlist);
		}

		public Chain.Model.Mem GetModel(int MemID)
		{
			return this.dal.GetModel(MemID);
		}

		public Chain.Model.Mem GetModelByMemCard(string MemCard)
		{
			return this.dal.GetModelByMemCard(MemCard);
		}

		public DataTable GetMemAdressBymencard(string MemCard)
		{
			return this.dal.GetMemAdressBymencard(MemCard);
		}

		public Chain.Model.Mem GetMemInfoByMobile(string Mobile)
		{
			return this.dal.GetMemInfoByMobile(Mobile);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.Mem> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.Mem> DataTableToList(DataTable dt)
		{
			List<Chain.Model.Mem> modelList = new List<Chain.Model.Mem>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.Mem model = this.dal.DataRowToModel(dt.Rows[i]);
					if (model != null)
					{
						modelList.Add(model);
					}
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

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetPointRankList(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetPointRankList(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetMemExpense(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetMemExpense(PageSize, PageIndex, out resCount, strWhere);
		}

		public int DrawMoney(int memID, decimal money)
		{
			return this.dal.DrawMoney(memID, money);
		}

		public int ExistPwd(int MemID, string oldPwd)
		{
			return this.dal.ExistPwd(MemID, oldPwd);
		}

		public int UpdateMemPwd(int memID, string newPwd, string oldPwd)
		{
			int result;
			if (oldPwd != "E62A9E6C1892C6BB")
			{
				int exist = this.ExistPwd(memID, oldPwd);
				if (exist != 1)
				{
					result = exist;
				}
				else
				{
					result = this.dal.UpdateMemPwd(memID, newPwd);
				}
			}
			else
			{
				result = this.dal.UpdateMemPwd(memID, newPwd);
			}
			return result;
		}

		public int UpdateMemPwd(bool isOldPwd, int memID, string newPwd, string oldPwd)
		{
			int result;
			if (isOldPwd)
			{
				int exist = this.ExistPwd(memID, oldPwd);
				if (exist != 1)
				{
					result = exist;
				}
				else
				{
					result = this.dal.UpdateMemPwd(memID, newPwd);
				}
			}
			else
			{
				result = this.dal.UpdateMemPwd(memID, newPwd);
			}
			return result;
		}

		public int UpdateMemPwd(int intMemID, string newPwd)
		{
			return this.dal.UpdateMemPwd(intMemID, newPwd);
		}

		public int UpdateMemState(int memID, int state)
		{
			return this.dal.UpdateMemState(memID, state);
		}

		public int UpdateMoney(int memID, decimal money)
		{
			return this.dal.UpdateMoney(money, memID);
		}

		public int UpdatePoint(int memID, int point)
		{
			return this.dal.UpdatePoint(memID, point);
		}

		public bool ChangeCard(Chain.Model.Mem modelMem, string newMemCard, string newPwd)
		{
			return this.dal.ChangeCard(modelMem, newMemCard, newPwd);
		}

		public DataSet GetMemConsumeMoney(string strWhere)
		{
			return this.dal.GetMemConsumeMoney(strWhere);
		}

		public void UpdateLevel(int memID, int levelID)
		{
			this.dal.UpdateLevel(memID, levelID);
		}

		public int UpdateMemPastTime(int memID, DateTime pastTime)
		{
			return this.dal.UpdateMemPastTime(memID, pastTime);
		}

		public int SysUpdateMemIsPast()
		{
			return this.dal.SysUpdateMemIsPast();
		}

		public DataSet GetBirthdayList(int day, int shopID, int count)
		{
			return this.dal.GetBirthdayList(day, shopID, count);
		}

		public DataSet GetBirthdayList(int day, int shopID)
		{
			return this.dal.GetBirthdayList(day, shopID);
		}

		public DataSet GetMemPastTime(string strSql, int shopID, int count)
		{
			return this.dal.GetMemPastTime(strSql, shopID, count);
		}

		public DataSet GetMemPastTime(string strSql, int shopID)
		{
			return this.dal.GetMemPastTime(strSql, shopID);
		}

		public DataSet GetMemPointReset(string strSql, int type, int count)
		{
			return this.dal.GetMemPointReset(strSql, type, count);
		}

		public DataSet GetMemPointReset(string strSql, int type)
		{
			return this.dal.GetMemPointReset(strSql, type);
		}

		public bool ClearMemberPoint(int MemID)
		{
			return this.dal.ClearMemberPoint(MemID);
		}

		public bool ExeclDataInput(ArrayList sqlArray)
		{
			return this.dal.ExeclDataInput(sqlArray);
		}

		public DataSet GetDataByTime(DateTime starttime, DateTime endtime, string strwhere)
		{
			return this.dal.GetDataByTime(starttime, endtime, strwhere);
		}

		public DataSet GetMemExpense(int PageSize, int PageIndex, out int resCount, string strTime, params string[] strWhere)
		{
			return this.dal.GetMemExpense(PageSize, PageIndex, out resCount, strTime, strWhere);
		}

		public DataSet GetMemExpenseToWeiXin(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetMemExpenseToWeiXin(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetMemExpenseDetail(string strWhere)
		{
			return this.dal.GetMemExpenseDetail(strWhere);
		}

		public Chain.Model.Mem CheckMemPwd(string strAccount, string strPassword)
		{
			DataTable db = this.dal.CheckMemPwd(strAccount, strPassword);
			Chain.Model.Mem result;
			if (db.Rows.Count > 0)
			{
				result = this.DataTableToList(db)[0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public int SecuritySettings(int intMemID, string strEmail, string strQuestion, string strAnswer)
		{
			return this.dal.SecuritySettings(intMemID, strEmail, strQuestion, strAnswer);
		}

		public int updateMemSyaArea(string strProvince, string strCity, string strCounty, string strVillage, int memId)
		{
			return this.dal.updateMemSyaArea(strProvince, strCity, strCounty, strVillage, memId);
		}

		public Chain.Model.Mem GetMemByWeiXinCard(string weiXinCard)
		{
			return this.dal.GetMemByWeiXinCard(weiXinCard);
		}

		public DataSet WeiXinLogin(string WeiXinCard, string PassWord)
		{
			return this.dal.WeiXinLogin(WeiXinCard, PassWord);
		}

		public int WeiXinRegister(string MemName, string PassWord, string telnumber, string referrerMemId, string memWeiXinCard, int MemShopID)
		{
			return this.dal.WeiXinRegister(MemName, PassWord, telnumber, referrerMemId, memWeiXinCard, MemShopID);
		}

		public Chain.Model.Mem GetModelByMemMobile(string Mobile)
		{
			return this.dal.GetModelByMemMobile(Mobile);
		}

		public DataSet GetMemBirthday(int day)
		{
			return this.dal.GetMemBirthday(day);
		}

		public DataSet GetMemPast(int day)
		{
			return this.dal.GetMemPast(day);
		}

		public int GetMemCount(string strWhere)
		{
			return this.dal.GetMemCount(strWhere);
		}

		public int AddMemFirst(Chain.Model.Mem model)
		{
			int exists = this.Exists(model.MemID, model.MemCard, model.MemMobile, model.MemCardNumber, model.MemShopID);
			int result;
			if (exists != 1)
			{
				result = exists;
			}
			else
			{
				result = this.dal.AddMemFirst(model);
			}
			return result;
		}

		public static string GetMemBirthday(DateTime birthday)
		{
			string result;
			if (birthday.Year == 1900 && birthday.Month == 1 && birthday.Day == 1)
			{
				result = "";
			}
			else
			{
				result = birthday.ToString("yyyy-MM-dd");
			}
			return result;
		}

		public DataTable GetModelDetail(params string[] strWhere)
		{
			return this.dal.GetModelDetail(strWhere);
		}

		public Chain.Model.Mem GetModel(string memCard)
		{
			return this.dal.GetModel(memCard);
		}

		public DataTable getMemRecommend(object memid)
		{
			return this.dal.getMemRecommend(memid);
		}

		public DataTable getMemRecommendList(int MemID)
		{
			return this.dal.GetMemRecommendList(MemID);
		}

		public DataSet GetListS(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListS(PageSize, PageIndex, strWhere, out resCount);
		}

		public DataTable getMemPayList(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.getMemPayList(PageSize, PageIndex, strWhere, out resCount);
		}

		public Chain.Model.Mem GetMemWeiXinCardModel(string strMemWeiXinCard, string strAttributes)
		{
			return this.dal.GetMemWeiXinCardModel(strMemWeiXinCard, strAttributes);
		}

		public int WeiXinRegister(string MemCard, string MemName, string PassWord, string telnumber, string referrerMemId, string memWeiXinCard, int MemShopID, string hidtf)
		{
			return this.dal.WeiXinRegister(MemCard, MemName, PassWord, telnumber, referrerMemId, memWeiXinCard, MemShopID, hidtf);
		}

		public int UpdateMemWeiXinCard(int memID, string openid)
		{
			return this.dal.UpdateMemWeiXinCard(memID, openid);
		}

		public DataSet GetMemBillList(string strWhere)
		{
			return this.dal.GetMemBillList(strWhere);
		}

		public int GetMemIDByWhere(string strwhere)
		{
			return this.dal.GetMemIDByWhere(strwhere);
		}
	}
}
