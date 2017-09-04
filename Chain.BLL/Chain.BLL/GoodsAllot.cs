using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class GoodsAllot
	{
		private readonly Chain.IDAL.GoodsAllot dal = new Chain.IDAL.GoodsAllot();

		public bool Exists(int AllotID)
		{
			return this.dal.Exists(AllotID);
		}

		public int Add(Chain.Model.GoodsAllot model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.GoodsAllot model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int AllotID)
		{
			return this.dal.Delete(AllotID);
		}

		public bool DeleteList(string AllotIDlist)
		{
			return this.dal.DeleteList(AllotIDlist);
		}

		public Chain.Model.GoodsAllot GetModel(int AllotID)
		{
			return this.dal.GetModel(AllotID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.GoodsAllot> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.GoodsAllot> DataTableToList(DataTable dt)
		{
			List<Chain.Model.GoodsAllot> modelList = new List<Chain.Model.GoodsAllot>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.GoodsAllot model = new Chain.Model.GoodsAllot();
					if (dt.Rows[i]["AllotID"] != null && dt.Rows[i]["AllotID"].ToString() != "")
					{
						model.AllotID = int.Parse(dt.Rows[i]["AllotID"].ToString());
					}
					if (dt.Rows[i]["AllotAccount"] != null && dt.Rows[i]["AllotAccount"].ToString() != "")
					{
						model.AllotAccount = dt.Rows[i]["AllotAccount"].ToString();
					}
					if (dt.Rows[i]["AllotOutShopID"] != null && dt.Rows[i]["AllotOutShopID"].ToString() != "")
					{
						model.AllotOutShopID = int.Parse(dt.Rows[i]["AllotOutShopID"].ToString());
					}
					if (dt.Rows[i]["AllotInShopID"] != null && dt.Rows[i]["AllotInShopID"].ToString() != "")
					{
						model.AllotInShopID = int.Parse(dt.Rows[i]["AllotInShopID"].ToString());
					}
					if (dt.Rows[i]["AllotCreateTime"] != null && dt.Rows[i]["AllotCreateTime"].ToString() != "")
					{
						model.AllotCreateTime = DateTime.Parse(dt.Rows[i]["AllotCreateTime"].ToString());
					}
					if (dt.Rows[i]["AllotTotalNumber"] != null && dt.Rows[i]["AllotTotalNumber"].ToString() != "")
					{
						model.AllotTotalNumber = decimal.Parse(dt.Rows[i]["AllotTotalNumber"].ToString());
					}
					if (dt.Rows[i]["AllotUserID"] != null && dt.Rows[i]["AllotUserID"].ToString() != "")
					{
						model.AllotUserID = int.Parse(dt.Rows[i]["AllotUserID"].ToString());
					}
					if (dt.Rows[i]["AllotRemark"] != null && dt.Rows[i]["AllotRemark"].ToString() != "")
					{
						model.AllotRemark = dt.Rows[i]["AllotRemark"].ToString();
					}
					if (dt.Rows[i]["AllotState"] != null && dt.Rows[i]["AllotState"].ToString() != "")
					{
						model.Allotstate = int.Parse(dt.Rows[i]["AllotState"].ToString());
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
