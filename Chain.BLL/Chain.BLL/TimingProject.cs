using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class TimingProject
	{
		private readonly Chain.IDAL.TimingProject dal = new Chain.IDAL.TimingProject();

		public bool Exists(int ProjectID)
		{
			return this.dal.Exists(ProjectID);
		}

		public bool ExistsRules(int RulesID)
		{
			return this.dal.ExistsRules(RulesID);
		}

		public bool ExistsCategory(int CategoryID)
		{
			return this.dal.ExistsCategory(CategoryID);
		}

		public int Add(Chain.Model.TimingProject model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.TimingProject model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ProjectID)
		{
			OrderTime bllOrderTime = new OrderTime();
			bool result;
			if (!bllOrderTime.ExistsProject(ProjectID))
			{
				MemStorageTiming bllMemStorageTiming = new MemStorageTiming();
				result = (!bllMemStorageTiming.ExistsProject(ProjectID) && this.dal.Delete(ProjectID));
			}
			else
			{
				result = false;
			}
			return result;
		}

		public bool DeleteList(string ProjectIDlist)
		{
			return this.dal.DeleteList(ProjectIDlist);
		}

		public Chain.Model.TimingProject GetModel(int ProjectID)
		{
			return this.dal.GetModel(ProjectID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.TimingProject> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.TimingProject> DataTableToList(DataTable dt)
		{
			List<Chain.Model.TimingProject> modelList = new List<Chain.Model.TimingProject>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.TimingProject model = new Chain.Model.TimingProject();
					if (dt.Rows[i]["ProjectID"] != null && dt.Rows[i]["ProjectID"].ToString() != "")
					{
						model.ProjectID = int.Parse(dt.Rows[i]["ProjectID"].ToString());
					}
					if (dt.Rows[i]["ProjectName"] != null && dt.Rows[i]["ProjectName"].ToString() != "")
					{
						model.ProjectName = dt.Rows[i]["ProjectName"].ToString();
					}
					if (dt.Rows[i]["ProjectCategoryID"] != null && dt.Rows[i]["ProjectCategoryID"].ToString() != "")
					{
						model.ProjectCategoryID = int.Parse(dt.Rows[i]["ProjectCategoryID"].ToString());
					}
					if (dt.Rows[i]["ProjectRulesID"] != null && dt.Rows[i]["ProjectRulesID"].ToString() != "")
					{
						model.ProjectRulesID = int.Parse(dt.Rows[i]["ProjectRulesID"].ToString());
					}
					if (dt.Rows[i]["ProjectAddTime"] != null && dt.Rows[i]["ProjectAddTime"].ToString() != "")
					{
						model.ProjectAddTime = DateTime.Parse(dt.Rows[i]["ProjectAddTime"].ToString());
					}
					if (dt.Rows[i]["ProjectShopID"] != null && dt.Rows[i]["ProjectShopID"].ToString() != "")
					{
						model.ProjectShopID = int.Parse(dt.Rows[i]["ProjectShopID"].ToString());
					}
					if (dt.Rows[i]["ProjectUserID"] != null && dt.Rows[i]["ProjectUserID"].ToString() != "")
					{
						model.ProjectUserID = int.Parse(dt.Rows[i]["ProjectUserID"].ToString());
					}
					if (dt.Rows[i]["ProjectRemark"] != null && dt.Rows[i]["ProjectRemark"].ToString() != "")
					{
						model.ProjectRemark = dt.Rows[i]["ProjectRemark"].ToString();
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

		public DataSet GetListSP(int MemID, int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(MemID, PageSize, PageIndex, out resCount, strWhere);
		}
	}
}
