using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class ProductClass
	{
		private readonly Chain.IDAL.ProductClass dal = new Chain.IDAL.ProductClass();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ClassID)
		{
			return this.dal.Exists(ClassID);
		}

		public int Add(Chain.Model.ProductClass model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.ProductClass model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ClassID)
		{
			return this.dal.Delete(ClassID);
		}

		public bool DeleteList(string ClassIDlist)
		{
			return this.dal.DeleteList(ClassIDlist);
		}

		public Chain.Model.ProductClass GetModel(int ClassID)
		{
			return this.dal.GetModel(ClassID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.ProductClass> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public DataSet GetGoodsClassInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetGoodsClassInfo(PageSize, PageIndex, out resCount, strWhere);
		}

		public List<Chain.Model.ProductClass> DataTableToList(DataTable dt)
		{
			List<Chain.Model.ProductClass> modelList = new List<Chain.Model.ProductClass>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.ProductClass model = new Chain.Model.ProductClass();
					if (dt.Rows[i]["ClassID"] != null && dt.Rows[i]["ClassID"].ToString() != "")
					{
						model.ClassID = int.Parse(dt.Rows[i]["ClassID"].ToString());
					}
					if (dt.Rows[i]["ClassName"] != null && dt.Rows[i]["ClassName"].ToString() != "")
					{
						model.ClassName = dt.Rows[i]["ClassName"].ToString();
					}
					if (dt.Rows[i]["ClassRemark"] != null && dt.Rows[i]["ClassRemark"].ToString() != "")
					{
						model.ClassRemark = dt.Rows[i]["ClassRemark"].ToString();
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
