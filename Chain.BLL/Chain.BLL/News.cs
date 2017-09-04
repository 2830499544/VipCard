using Chain.DAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class News
	{
		private readonly Chain.DAL.News dal = new Chain.DAL.News();

		public bool Exists(int NewsID)
		{
			return this.dal.Exists(NewsID);
		}

		public DataSet GetNewsInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetNewsInfo(PageSize, PageIndex, out resCount, strWhere);
		}

		public int Add(Chain.Model.News model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.News model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int NewsID)
		{
			return this.dal.Delete(NewsID);
		}

		public bool DeleteList(string NewsIDlist)
		{
			return this.dal.DeleteList(NewsIDlist);
		}

		public Chain.Model.News GetModel(int NewsID)
		{
			return this.dal.GetModel(NewsID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.News> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.News> DataTableToList(DataTable dt)
		{
			List<Chain.Model.News> modelList = new List<Chain.Model.News>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.News model = new Chain.Model.News();
					if (dt.Rows[i]["NewsID"] != null && dt.Rows[i]["NewsID"].ToString() != "")
					{
						model.NewsID = int.Parse(dt.Rows[i]["NewsID"].ToString());
					}
					if (dt.Rows[i]["NewsName"] != null && dt.Rows[i]["NewsName"].ToString() != "")
					{
						model.NewsName = dt.Rows[i]["NewsName"].ToString();
					}
					if (dt.Rows[i]["NewsPhoto"] != null && dt.Rows[i]["NewsPhoto"].ToString() != "")
					{
						model.NewsPhoto = dt.Rows[i]["NewsPhoto"].ToString();
					}
					if (dt.Rows[i]["NewsDesc"] != null && dt.Rows[i]["NewsDesc"].ToString() != "")
					{
						model.NewsDesc = dt.Rows[i]["NewsDesc"].ToString();
					}
					if (dt.Rows[i]["NewsCreateTime"] != null && dt.Rows[i]["NewsCreateTime"].ToString() != "")
					{
						model.NewsCreateTime = DateTime.Parse(dt.Rows[i]["NewsCreateTime"].ToString());
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
