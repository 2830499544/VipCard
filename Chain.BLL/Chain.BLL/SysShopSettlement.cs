using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysShopSettlement
	{
		private readonly Chain.IDAL.SysShopSettlement dal = new Chain.IDAL.SysShopSettlement();

		public bool Exists(int ID)
		{
			return this.dal.Exists(ID);
		}

		public int Add(Chain.Model.SysShopSettlement model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SysShopSettlement model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID)
		{
			return this.dal.Delete(ID);
		}

		public bool DeleteList(string IDlist)
		{
			return this.dal.DeleteList(IDlist);
		}

		public void UpDateSettlement()
		{
			this.dal.UpDateSettlement();
		}

		public Chain.Model.SysShopSettlement GetModel(int ID)
		{
			return this.dal.GetModel(ID);
		}

		public string GetEndTime(int OutShopID)
		{
			return this.dal.GetEndTime(OutShopID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysShopSettlement> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysShopSettlement> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysShopSettlement> modelList = new List<Chain.Model.SysShopSettlement>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysShopSettlement model = new Chain.Model.SysShopSettlement();
					if (dt.Rows[i]["ID"] != null && dt.Rows[i]["ID"].ToString() != "")
					{
						model.ID = int.Parse(dt.Rows[i]["ID"].ToString());
					}
					if (dt.Rows[i]["StartTime"] != null && dt.Rows[i]["StartTime"].ToString() != "")
					{
						model.StartTime = DateTime.Parse(dt.Rows[i]["StartTime"].ToString());
					}
					if (dt.Rows[i]["EndTime"] != null && dt.Rows[i]["EndTime"].ToString() != "")
					{
						model.EndTime = DateTime.Parse(dt.Rows[i]["EndTime"].ToString());
					}
					if (dt.Rows[i]["RechargeMoney"] != null && dt.Rows[i]["RechargeMoney"].ToString() != "")
					{
						model.RechargeMoney = decimal.Parse(dt.Rows[i]["RechargeMoney"].ToString());
					}
					if (dt.Rows[i]["DrawMoney"] != null && dt.Rows[i]["DrawMoney"].ToString() != "")
					{
						model.DrawMoney = decimal.Parse(dt.Rows[i]["DrawMoney"].ToString());
					}
					if (dt.Rows[i]["PayCard"] != null && dt.Rows[i]["PayCard"].ToString() != "")
					{
						model.PayCard = decimal.Parse(dt.Rows[i]["PayCard"].ToString());
					}
					if (dt.Rows[i]["IsFinish"] != null && dt.Rows[i]["IsFinish"].ToString() != "")
					{
						if (dt.Rows[i]["IsFinish"].ToString() == "1" || dt.Rows[i]["IsFinish"].ToString().ToLower() == "true")
						{
							model.IsFinish = true;
						}
						else
						{
							model.IsFinish = false;
						}
					}
					if (dt.Rows[i]["FinishTime"] != null && dt.Rows[i]["FinishTime"].ToString() != "")
					{
						model.FinishTime = DateTime.Parse(dt.Rows[i]["FinishTime"].ToString());
					}
					if (dt.Rows[i]["Remark"] != null && dt.Rows[i]["Remark"].ToString() != "")
					{
						model.Remark = dt.Rows[i]["Remark"].ToString();
					}
					if (dt.Rows[i]["OutShopID"] != null && dt.Rows[i]["OutShopID"].ToString() != "")
					{
						model.OutShopID = int.Parse(dt.Rows[i]["OutShopID"].ToString());
					}
					if (dt.Rows[i]["UserID"] != null && dt.Rows[i]["UserID"].ToString() != "")
					{
						model.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
					}
					if (dt.Rows[i]["AllExpenseMoney"] != null && dt.Rows[i]["AllExpenseMoney"].ToString() != "")
					{
						model.AllExpenseMoney = Convert.ToDecimal(dt.Rows[i]["AllExpenseMoney"].ToString());
					}
					if (dt.Rows[i]["ProportionMoney"] != null && dt.Rows[i]["ProportionMoney"].ToString() != "")
					{
						model.ProportionMoney = Convert.ToDecimal(dt.Rows[i]["ProportionMoney"].ToString());
					}
					if (dt.Rows[i]["Proportion"] != null && dt.Rows[i]["Proportion"].ToString() != "")
					{
						model.Proportion = Convert.ToDecimal(dt.Rows[i]["Proportion"].ToString());
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

		public DataSet GetListSP(int PageSize, int PageIndex, string join, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, join, out resCount, strWhere);
		}
	}
}
