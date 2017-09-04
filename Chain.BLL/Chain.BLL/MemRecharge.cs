using Chain.DAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MemRecharge
	{
		private readonly Chain.DAL.MemRecharge dal = new Chain.DAL.MemRecharge();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int RechargeID)
		{
			return this.dal.Exists(RechargeID);
		}

		public bool Exists(string orderaccount)
		{
			return this.dal.Exists(orderaccount);
		}

		public int Add(Chain.Model.MemRecharge model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.MemRecharge model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int RechargeID)
		{
			return this.dal.Delete(RechargeID);
		}

		public bool DeleteList(string RechargeIDlist)
		{
			return this.dal.DeleteList(RechargeIDlist);
		}

		public Chain.Model.MemRecharge GetModel(int RechargeID)
		{
			return this.dal.GetModel(RechargeID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.MemRecharge> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MemRecharge> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MemRecharge> modelList = new List<Chain.Model.MemRecharge>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MemRecharge model = new Chain.Model.MemRecharge();
					if (dt.Rows[i]["RechargeID"] != null && dt.Rows[i]["RechargeID"].ToString() != "")
					{
						model.RechargeID = int.Parse(dt.Rows[i]["RechargeID"].ToString());
					}
					if (dt.Rows[i]["RechargeMemID"] != null && dt.Rows[i]["RechargeMemID"].ToString() != "")
					{
						model.RechargeMemID = int.Parse(dt.Rows[i]["RechargeMemID"].ToString());
					}
					if (dt.Rows[i]["RechargeAccount"] != null && dt.Rows[i]["RechargeAccount"].ToString() != "")
					{
						model.RechargeAccount = dt.Rows[i]["RechargeAccount"].ToString();
					}
					if (dt.Rows[i]["RechargeType"] != null && dt.Rows[i]["RechargeType"].ToString() != "")
					{
						model.RechargeType = int.Parse(dt.Rows[i]["RechargeType"].ToString());
					}
					if (dt.Rows[i]["RechargeMoney"] != null && dt.Rows[i]["RechargeMoney"].ToString() != "")
					{
						model.RechargeMoney = decimal.Parse(dt.Rows[i]["RechargeMoney"].ToString());
					}
					if (dt.Rows[i]["RechargeRemark"] != null && dt.Rows[i]["RechargeRemark"].ToString() != "")
					{
						model.RechargeRemark = dt.Rows[i]["RechargeRemark"].ToString();
					}
					if (dt.Rows[i]["RechargeShopID"] != null && dt.Rows[i]["RechargeShopID"].ToString() != "")
					{
						model.RechargeShopID = int.Parse(dt.Rows[i]["RechargeShopID"].ToString());
					}
					if (dt.Rows[i]["RechargeCreateTime"] != null && dt.Rows[i]["RechargeCreateTime"].ToString() != "")
					{
						model.RechargeCreateTime = DateTime.Parse(dt.Rows[i]["RechargeCreateTime"].ToString());
					}
					if (dt.Rows[i]["RechargeUserID"] != null && dt.Rows[i]["RechargeUserID"].ToString() != "")
					{
						model.RechargeUserID = int.Parse(dt.Rows[i]["RechargeUserID"].ToString());
					}
					if (dt.Rows[i]["RechargeCardBalance"] != null && dt.Rows[i]["RechargeCardBalance"].ToString() != "")
					{
						model.RechargeCardBalance = decimal.Parse(dt.Rows[i]["RechargeCardBalance"].ToString());
					}
					if (dt.Rows[i]["RechargeIsApprove"] != null && dt.Rows[i]["RechargeIsApprove"].ToString() != "")
					{
						if (dt.Rows[i]["RechargeIsApprove"].ToString() == "1" || dt.Rows[i]["RechargeIsApprove"].ToString().ToLower() == "true")
						{
							model.RechargeIsApprove = true;
						}
						else
						{
							model.RechargeIsApprove = false;
						}
					}
					if (dt.Rows[i]["RechargePoint"] != null && dt.Rows[i]["RechargePoint"].ToString() != "")
					{
						model.RechargePoint = int.Parse(dt.Rows[i]["RechargePoint"].ToString());
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

		public decimal GetRechargeMoney(string strWhere)
		{
			return this.dal.GetRechargeMoney(strWhere);
		}

		public decimal GetGiveMoney(string strWhere)
		{
			return this.dal.GetGiveMoney(strWhere);
		}

		public DataSet GetRechargeByTime(DateTime starttime, DateTime endtime, string strwhere)
		{
			return this.dal.GetRechargeByTime(starttime, endtime, strwhere);
		}

		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetListSP1(int PageSize, int PageIndex, string IndexColumn, bool isAsc, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP1(PageSize, PageIndex, IndexColumn, isAsc, out resCount, strWhere);
		}

		public DataSet GetListSP2(int PageSize, int PageIndex, string IndexColumn, bool isAsc, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP2(PageSize, PageIndex, IndexColumn, isAsc, out resCount, strWhere);
		}

		public decimal GetRecMoney(string strWhere)
		{
			return this.dal.GetRecMoney(strWhere);
		}

		public DataTable AgainPrint(int rechargeID)
		{
			return this.dal.AgainPrint(rechargeID);
		}

		public int UpdateRecharge(int rechargeID)
		{
			return this.dal.UpdateRecharge(rechargeID);
		}
	}
}
