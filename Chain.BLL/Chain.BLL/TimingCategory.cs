using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class TimingCategory
	{
		private readonly Chain.IDAL.TimingCategory dal = new Chain.IDAL.TimingCategory();

		public bool Exists(int CategoryID)
		{
			return this.dal.Exists(CategoryID);
		}

		public int Add(Chain.Model.TimingCategory model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.TimingCategory model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int CategoryID)
		{
			TimingProject bllTimingProject = new TimingProject();
			return !bllTimingProject.ExistsCategory(CategoryID) && this.dal.Delete(CategoryID);
		}

		public bool DeleteList(string CategoryIDlist)
		{
			return this.dal.DeleteList(CategoryIDlist);
		}

		public Chain.Model.TimingCategory GetModel(int CategoryID)
		{
			return this.dal.GetModel(CategoryID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetNifo(string strWhere)
		{
			return this.dal.GetNifo(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.TimingCategory> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.TimingCategory> DataTableToList(DataTable dt)
		{
			List<Chain.Model.TimingCategory> modelList = new List<Chain.Model.TimingCategory>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.TimingCategory model = new Chain.Model.TimingCategory();
					if (dt.Rows[i]["CategoryID"] != null && dt.Rows[i]["CategoryID"].ToString() != "")
					{
						model.CategoryID = int.Parse(dt.Rows[i]["CategoryID"].ToString());
					}
					if (dt.Rows[i]["CategoryName"] != null && dt.Rows[i]["CategoryName"].ToString() != "")
					{
						model.CategoryName = dt.Rows[i]["CategoryName"].ToString();
					}
					if (dt.Rows[i]["CategoryFatherID"] != null && dt.Rows[i]["CategoryFatherID"].ToString() != "")
					{
						model.CategoryFatherID = int.Parse(dt.Rows[i]["CategoryFatherID"].ToString());
					}
					if (dt.Rows[i]["CategoryShopID"] != null && dt.Rows[i]["CategoryShopID"].ToString() != "")
					{
						model.CategoryShopID = int.Parse(dt.Rows[i]["CategoryShopID"].ToString());
					}
					if (dt.Rows[i]["CategoryUserID"] != null && dt.Rows[i]["CategoryUserID"].ToString() != "")
					{
						model.CategoryUserID = int.Parse(dt.Rows[i]["CategoryUserID"].ToString());
					}
					if (dt.Rows[i]["CategoryrRemark"] != null && dt.Rows[i]["CategoryrRemark"].ToString() != "")
					{
						model.CategoryrRemark = dt.Rows[i]["CategoryrRemark"].ToString();
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
	}
}
