using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class StaffMoney
	{
		private readonly Chain.IDAL.StaffMoney dal = new Chain.IDAL.StaffMoney();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int StaffMoneyID)
		{
			return this.dal.Exists(StaffMoneyID);
		}

		public int Add(Chain.Model.StaffMoney model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.StaffMoney model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int StaffMoneyID)
		{
			return this.dal.Delete(StaffMoneyID);
		}

		public bool DeleteList(string StaffMoneyIDlist)
		{
			return this.dal.DeleteList(StaffMoneyIDlist);
		}

		public Chain.Model.StaffMoney GetModel(int StaffMoneyID)
		{
			return this.dal.GetModel(StaffMoneyID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.StaffMoney> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.StaffMoney> DataTableToList(DataTable dt)
		{
			List<Chain.Model.StaffMoney> modelList = new List<Chain.Model.StaffMoney>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.StaffMoney model = new Chain.Model.StaffMoney();
					if (dt.Rows[i]["StaffMoneyID"] != null && dt.Rows[i]["StaffMoneyID"].ToString() != "")
					{
						model.StaffMoneyID = int.Parse(dt.Rows[i]["StaffMoneyID"].ToString());
					}
					if (dt.Rows[i]["StaffID"] != null && dt.Rows[i]["StaffID"].ToString() != "")
					{
						model.StaffID = int.Parse(dt.Rows[i]["StaffID"].ToString());
					}
					if (dt.Rows[i]["StaffTotalMoney"] != null && dt.Rows[i]["StaffTotalMoney"].ToString() != "")
					{
						model.StaffTotalMoney = decimal.Parse(dt.Rows[i]["StaffTotalMoney"].ToString());
					}
					if (dt.Rows[i]["StaffOrderCode"] != null && dt.Rows[i]["StaffOrderCode"].ToString() != "")
					{
						model.StaffOrderCode = dt.Rows[i]["StaffOrderCode"].ToString();
					}
					if (dt.Rows[i]["StaffMemID"] != null && dt.Rows[i]["StaffMemID"].ToString() != "")
					{
						model.StaffMemID = int.Parse(dt.Rows[i]["StaffMemID"].ToString());
					}
					if (dt.Rows[i]["StaffGoodsID"] != null && dt.Rows[i]["StaffGoodsID"].ToString() != "")
					{
						model.StaffGoodsID = int.Parse(dt.Rows[i]["StaffGoodsID"].ToString());
					}
					if (dt.Rows[i]["StaffShopID"] != null && dt.Rows[i]["StaffShopID"].ToString() != "")
					{
						model.StaffShopID = int.Parse(dt.Rows[i]["StaffShopID"].ToString());
					}
					if (dt.Rows[i]["StaffCreateTime"] != null && dt.Rows[i]["StaffCreateTime"].ToString() != "")
					{
						model.StaffCreateTime = DateTime.Parse(dt.Rows[i]["StaffCreateTime"].ToString());
					}
					if (dt.Rows[i]["StaffOrderDetailID"] != null && dt.Rows[i]["StaffOrderDetailID"].ToString() != "")
					{
						model.StaffOrderDetailID = int.Parse(dt.Rows[i]["StaffOrderDetailID"].ToString());
					}
					if (dt.Rows[i]["StaffType"] != null && dt.Rows[i]["StaffType"].ToString() != "")
					{
						model.StaffType = int.Parse(dt.Rows[i]["StaffType"].ToString());
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

		public decimal GetTotalStaffMoney(string strWhere)
		{
			return this.dal.GetTotalStaffMoney(strWhere);
		}

		public DataSet GetListSP(string strWhere)
		{
			return this.dal.GetListSP(strWhere);
		}

		public DataSet GetStaffMoneyByTime(DateTime starttime, DateTime endtime, string strwhere)
		{
			return this.dal.GetStaffMoneyByTime(starttime, endtime, strwhere);
		}

		public int DeleteStaff(string strStaffOrderCode)
		{
			return this.dal.DeleteStaff(strStaffOrderCode);
		}

		public int UpdateStaffMoney(string strOrderCode, int intGoodsID, decimal money)
		{
			return this.dal.UpdateStaffMoney(strOrderCode, intGoodsID, money);
		}

		public DataSet GetStaffMoney(string strWhere)
		{
			return this.dal.GetStaffMoney(strWhere);
		}

		public decimal GetStaffTotalMoney(string strSql)
		{
			return this.dal.GetStaffTotalMoney(strSql);
		}
	}
}
