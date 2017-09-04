using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class Promotions
	{
		private readonly Chain.IDAL.Promotions dal = new Chain.IDAL.Promotions();

		public DataSet GetPromotionsInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetPromotionsInfo(PageSize, PageIndex, out resCount, strWhere);
		}

		public bool Exists(int PromotionsID)
		{
			return this.dal.Exists(PromotionsID);
		}

		public int Add(Chain.Model.Promotions model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.Promotions model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int PromotionsID)
		{
			return this.dal.Delete(PromotionsID);
		}

		public bool DeleteList(string PromotionsIDlist)
		{
			return this.dal.DeleteList(PromotionsIDlist);
		}

		public Chain.Model.Promotions GetModel(int PromotionsID)
		{
			return this.dal.GetModel(PromotionsID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.Promotions> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.Promotions> DataTableToList(DataTable dt)
		{
			List<Chain.Model.Promotions> modelList = new List<Chain.Model.Promotions>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.Promotions model = new Chain.Model.Promotions();
					if (dt.Rows[i]["PromotionsID"] != null && dt.Rows[i]["PromotionsID"].ToString() != "")
					{
						model.PromotionsID = int.Parse(dt.Rows[i]["PromotionsID"].ToString());
					}
					if (dt.Rows[i]["PromotionsTitle"] != null && dt.Rows[i]["PromotionsTitle"].ToString() != "")
					{
						model.PromotionsTitle = dt.Rows[i]["PromotionsTitle"].ToString();
					}
					if (dt.Rows[i]["PromotionsStart"] != null && dt.Rows[i]["PromotionsStart"].ToString() != "")
					{
						model.PromotionsStart = DateTime.Parse(dt.Rows[i]["PromotionsStart"].ToString());
					}
					if (dt.Rows[i]["PromotionsEnd"] != null && dt.Rows[i]["PromotionsEnd"].ToString() != "")
					{
						model.PromotionsEnd = DateTime.Parse(dt.Rows[i]["PromotionsEnd"].ToString());
					}
					if (dt.Rows[i]["PromotionsMemLevel"] != null && dt.Rows[i]["PromotionsMemLevel"].ToString() != "")
					{
						model.PromotionsMemLevel = int.Parse(dt.Rows[i]["PromotionsMemLevel"].ToString());
					}
					if (dt.Rows[i]["PromotionsType"] != null && dt.Rows[i]["PromotionsType"].ToString() != "")
					{
						model.PromotionsType = int.Parse(dt.Rows[i]["PromotionsType"].ToString());
					}
					if (dt.Rows[i]["PromotionsTime"] != null && dt.Rows[i]["PromotionsTime"].ToString() != "")
					{
						model.PromotionsTime = DateTime.Parse(dt.Rows[i]["PromotionsTime"].ToString());
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
