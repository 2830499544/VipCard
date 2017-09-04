using Chain.DAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class ProductCenter
	{
		private readonly Chain.DAL.ProductCenter dal = new Chain.DAL.ProductCenter();

		public bool Exists(int ProductID)
		{
			return this.dal.Exists(ProductID);
		}

		public int Add(Chain.Model.ProductCenter model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.ProductCenter model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ProductID)
		{
			return this.dal.Delete(ProductID);
		}

		public bool DeleteList(string ProductIDlist)
		{
			return this.dal.DeleteList(ProductIDlist);
		}

		public Chain.Model.ProductCenter GetModel(int ProductID)
		{
			return this.dal.GetModel(ProductID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.ProductCenter> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.ProductCenter> DataTableToList(DataTable dt)
		{
			List<Chain.Model.ProductCenter> modelList = new List<Chain.Model.ProductCenter>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.ProductCenter model = new Chain.Model.ProductCenter();
					if (dt.Rows[i]["ProductID"] != null && dt.Rows[i]["ProductID"].ToString() != "")
					{
						model.ProductID = int.Parse(dt.Rows[i]["ProductID"].ToString());
					}
					if (dt.Rows[i]["ProductName"] != null && dt.Rows[i]["ProductName"].ToString() != "")
					{
						model.ProductName = dt.Rows[i]["ProductName"].ToString();
					}
					if (dt.Rows[i]["ProductPhoto"] != null && dt.Rows[i]["ProductPhoto"].ToString() != "")
					{
						model.ProductPhoto = dt.Rows[i]["ProductPhoto"].ToString();
					}
					if (dt.Rows[i]["ProductDesc"] != null && dt.Rows[i]["ProductDesc"].ToString() != "")
					{
						model.ProductDesc = dt.Rows[i]["ProductDesc"].ToString();
					}
					if (dt.Rows[i]["ProductCreateTime"] != null && dt.Rows[i]["ProductCreateTime"].ToString() != "")
					{
						model.ProductCreateTime = new DateTime?(DateTime.Parse(dt.Rows[i]["ProductCreateTime"].ToString()));
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

		public DataSet GetProductCenterInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetProductCenterInfo(PageSize, PageIndex, out resCount, strWhere);
		}
	}
}
