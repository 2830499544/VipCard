using Chain.DAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class WeiXinNews
	{
		private readonly Chain.DAL.WeiXinNews dal = new Chain.DAL.WeiXinNews();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int NewsID)
		{
			return this.dal.Exists(NewsID);
		}

		public int Add(Chain.Model.WeiXinNews model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.WeiXinNews model)
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

		public Chain.Model.WeiXinNews GetModel(int NewsID)
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

		public List<Chain.Model.WeiXinNews> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.WeiXinNews> DataTableToList(DataTable dt)
		{
			List<Chain.Model.WeiXinNews> modelList = new List<Chain.Model.WeiXinNews>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.WeiXinNews model = new Chain.Model.WeiXinNews();
					if (dt.Rows[i]["NewsID"] != null && dt.Rows[i]["NewsID"].ToString() != "")
					{
						model.NewsID = int.Parse(dt.Rows[i]["NewsID"].ToString());
					}
					if (dt.Rows[i]["NewsRuleID"] != null && dt.Rows[i]["NewsRuleID"].ToString() != "")
					{
						model.NewsRuleID = int.Parse(dt.Rows[i]["NewsRuleID"].ToString());
					}
					if (dt.Rows[i]["NewsTitle"] != null && dt.Rows[i]["NewsTitle"].ToString() != "")
					{
						model.NewsTitle = dt.Rows[i]["NewsTitle"].ToString();
					}
					if (dt.Rows[i]["NewsDesc"] != null && dt.Rows[i]["NewsDesc"].ToString() != "")
					{
						model.NewsDesc = dt.Rows[i]["NewsDesc"].ToString();
					}
					if (dt.Rows[i]["NewsUrlFirst"] != null && dt.Rows[i]["NewsUrlFirst"].ToString() != "")
					{
						model.NewsUrlFirst = dt.Rows[i]["NewsUrlFirst"].ToString();
					}
					if (dt.Rows[i]["NewsUrlSecond"] != null && dt.Rows[i]["NewsUrlSecond"].ToString() != "")
					{
						model.NewsUrlSecond = dt.Rows[i]["NewsUrlSecond"].ToString();
					}
					if (dt.Rows[i]["NewsLinkContent"] != null && dt.Rows[i]["NewsLinkContent"].ToString() != "")
					{
						model.NewsLinkContent = dt.Rows[i]["NewsLinkContent"].ToString();
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

		public DataTable GetParent()
		{
			return this.dal.GetParent();
		}

		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
		}
	}
}
