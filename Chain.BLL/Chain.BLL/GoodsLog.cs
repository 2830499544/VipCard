using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class GoodsLog
	{
		private readonly Chain.IDAL.GoodsLog dal = new Chain.IDAL.GoodsLog();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID)
		{
			return this.dal.Exists(ID);
		}

		public int Add(Chain.Model.GoodsLog model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.GoodsLog model)
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

		public Chain.Model.GoodsLog GetModel(int ID)
		{
			return this.dal.GetModel(ID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetListsss(string strWhere)
		{
			return this.dal.GetListsss(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.GoodsLog> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.GoodsLog> DataTableToList(DataTable dt)
		{
			List<Chain.Model.GoodsLog> modelList = new List<Chain.Model.GoodsLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.GoodsLog model = new Chain.Model.GoodsLog();
					if (dt.Rows[i]["ID"] != null && dt.Rows[i]["ID"].ToString() != "")
					{
						model.ID = int.Parse(dt.Rows[i]["ID"].ToString());
					}
					if (dt.Rows[i]["GoodsAccount"] != null && dt.Rows[i]["GoodsAccount"].ToString() != "")
					{
						model.GoodsAccount = dt.Rows[i]["GoodsAccount"].ToString();
					}
					if (dt.Rows[i]["Type"] != null && dt.Rows[i]["Type"].ToString() != "")
					{
						model.Type = int.Parse(dt.Rows[i]["Type"].ToString());
					}
					if (dt.Rows[i]["GoodsID"] != null && dt.Rows[i]["GoodsID"].ToString() != "")
					{
						model.GoodsID = int.Parse(dt.Rows[i]["GoodsID"].ToString());
					}
					if (dt.Rows[i]["TotalPrice"] != null && dt.Rows[i]["TotalPrice"].ToString() != "")
					{
						model.TotalPrice = decimal.Parse(dt.Rows[i]["TotalPrice"].ToString());
					}
					if (dt.Rows[i]["GoodsNumber"] != null && dt.Rows[i]["GoodsNumber"].ToString() != "")
					{
						model.GoodsNumber = int.Parse(dt.Rows[i]["GoodsNumber"].ToString());
					}
					if (dt.Rows[i]["Remark"] != null && dt.Rows[i]["Remark"].ToString() != "")
					{
						model.Remark = dt.Rows[i]["Remark"].ToString();
					}
					if (dt.Rows[i]["CreateTime"] != null && dt.Rows[i]["CreateTime"].ToString() != "")
					{
						model.CreateTime = DateTime.Parse(dt.Rows[i]["CreateTime"].ToString());
					}
					if (dt.Rows[i]["ShopID"] != null && dt.Rows[i]["ShopID"].ToString() != "")
					{
						model.ShopID = int.Parse(dt.Rows[i]["ShopID"].ToString());
					}
					if (dt.Rows[i]["UserID"] != null && dt.Rows[i]["UserID"].ToString() != "")
					{
						model.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
					}
					if (dt.Rows[i]["ChangeShopID"] != null && dt.Rows[i]["ChangeShopID"].ToString() != "")
					{
						model.ChangeShopID = int.Parse(dt.Rows[i]["ChangeShopID"].ToString());
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

		public bool ExeclDataInput(ArrayList sqlArray)
		{
			return this.dal.ExeclDataInput(sqlArray);
		}

		public int GetIDByGoodsAccount(string GoodsAccount)
		{
			return this.dal.GetIDByGoodsAccount(GoodsAccount);
		}
	}
}
