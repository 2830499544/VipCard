using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class Timingrules
	{
		private readonly Chain.IDAL.Timingrules dal = new Chain.IDAL.Timingrules();

		public bool Exists(int RulesID)
		{
			return this.dal.Exists(RulesID);
		}

		public int Add(Chain.Model.Timingrules model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.Timingrules model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int RulesID)
		{
			TimingProject bllTimingProject = new TimingProject();
			bool result;
			if (!bllTimingProject.ExistsRules(RulesID))
			{
				OrderTime bllOrderTime = new OrderTime();
				result = (!bllOrderTime.ExistsRules(RulesID) && this.dal.Delete(RulesID));
			}
			else
			{
				result = false;
			}
			return result;
		}

		public bool DeleteList(string RulesIDlist)
		{
			return this.dal.DeleteList(RulesIDlist);
		}

		public Chain.Model.Timingrules GetModel(int RulesID)
		{
			return this.dal.GetModel(RulesID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.Timingrules> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.Timingrules> DataTableToList(DataTable dt)
		{
			List<Chain.Model.Timingrules> modelList = new List<Chain.Model.Timingrules>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.Timingrules model = new Chain.Model.Timingrules();
					if (dt.Rows[i]["RulesID"] != null && dt.Rows[i]["RulesID"].ToString() != "")
					{
						model.RulesID = int.Parse(dt.Rows[i]["RulesID"].ToString());
					}
					if (dt.Rows[i]["RulesName"] != null && dt.Rows[i]["RulesName"].ToString() != "")
					{
						model.RulesName = dt.Rows[i]["RulesName"].ToString();
					}
					if (dt.Rows[i]["RulesInterval"] != null && dt.Rows[i]["RulesInterval"].ToString() != "")
					{
						model.RulesInterval = int.Parse(dt.Rows[i]["RulesInterval"].ToString());
					}
					if (dt.Rows[i]["RulesUnitPrice"] != null && dt.Rows[i]["RulesUnitPrice"].ToString() != "")
					{
						model.RulesUnitPrice = decimal.Parse(dt.Rows[i]["RulesUnitPrice"].ToString());
					}
					if (dt.Rows[i]["RulesExceedTime"] != null && dt.Rows[i]["RulesExceedTime"].ToString() != "")
					{
						model.RulesExceedTime = int.Parse(dt.Rows[i]["RulesExceedTime"].ToString());
					}
					if (dt.Rows[i]["RulesAddTime"] != null && dt.Rows[i]["RulesAddTime"].ToString() != "")
					{
						model.RulesAddTime = DateTime.Parse(dt.Rows[i]["RulesAddTime"].ToString());
					}
					if (dt.Rows[i]["RulesShopID"] != null && dt.Rows[i]["RulesShopID"].ToString() != "")
					{
						model.RulesShopID = int.Parse(dt.Rows[i]["RulesShopID"].ToString());
					}
					if (dt.Rows[i]["RulesUserID"] != null && dt.Rows[i]["RulesUserID"].ToString() != "")
					{
						model.RulesUserID = int.Parse(dt.Rows[i]["RulesUserID"].ToString());
					}
					if (dt.Rows[i]["RulesRemark"] != null && dt.Rows[i]["RulesRemark"].ToString() != "")
					{
						model.RulesRemark = dt.Rows[i]["RulesRemark"].ToString();
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

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}
	}
}
