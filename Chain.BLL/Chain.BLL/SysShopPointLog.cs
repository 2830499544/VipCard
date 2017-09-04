using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysShopPointLog
	{
		private readonly Chain.IDAL.SysShopPointLog dal = new Chain.IDAL.SysShopPointLog();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID)
		{
			return this.dal.Exists(ID);
		}

		public bool Add(Chain.Model.SysShopPointLog model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SysShopPointLog model)
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

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public Chain.Model.SysShopPointLog GetModel(int ID)
		{
			return this.dal.GetModel(ID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysShopPointLog> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysShopPointLog> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysShopPointLog> modelList = new List<Chain.Model.SysShopPointLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysShopPointLog model = new Chain.Model.SysShopPointLog();
					if (dt.Rows[i]["ID"] != null && dt.Rows[i]["ID"].ToString() != "")
					{
						model.ID = int.Parse(dt.Rows[i]["ID"].ToString());
					}
					if (dt.Rows[i]["ShopPointAccount"] != null && dt.Rows[i]["ShopPointAccount"].ToString() != "")
					{
						model.ShopPointAccount = dt.Rows[i]["ShopPointAccount"].ToString();
					}
					if (dt.Rows[i]["ShopPointType"] != null && dt.Rows[i]["ShopPointType"].ToString() != "")
					{
						model.ShopPointType = int.Parse(dt.Rows[i]["ShopPointType"].ToString());
					}
					if (dt.Rows[i]["Count"] != null && dt.Rows[i]["Count"].ToString() != "")
					{
						model.Count = int.Parse(dt.Rows[i]["Count"].ToString());
					}
					if (dt.Rows[i]["Remark"] != null && dt.Rows[i]["Remark"].ToString() != "")
					{
						model.Remark = dt.Rows[i]["Remark"].ToString();
					}
					if (dt.Rows[i]["CreateTime"] != null && dt.Rows[i]["CreateTime"].ToString() != "")
					{
						model.CreateTime = DateTime.Parse(dt.Rows[i]["CreateTime"].ToString());
					}
					if (dt.Rows[i]["UserID"] != null && dt.Rows[i]["UserID"].ToString() != "")
					{
						model.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
					}
					if (dt.Rows[i]["ShopID"] != null && dt.Rows[i]["ShopID"].ToString() != "")
					{
						model.ShopID = int.Parse(dt.Rows[i]["ShopID"].ToString());
					}
					if (dt.Rows[i]["OutShopID"] != null && dt.Rows[i]["OutShopID"].ToString() != "")
					{
						model.OutShopID = int.Parse(dt.Rows[i]["OutShopID"].ToString());
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
