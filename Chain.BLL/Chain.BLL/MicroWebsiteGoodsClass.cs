using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MicroWebsiteGoodsClass
	{
		private readonly Chain.IDAL.MicroWebsiteGoodsClass dal = new Chain.IDAL.MicroWebsiteGoodsClass();

		public DataSet GetGoodsClassInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetGoodsClassInfo(PageSize, PageIndex, out resCount, strWhere);
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int MicroGoodsClassID)
		{
			return this.dal.Exists(MicroGoodsClassID);
		}

		public int Add(Chain.Model.MicroWebsiteGoodsClass model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.MicroWebsiteGoodsClass model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int MicroGoodsClassID)
		{
			return this.dal.Delete(MicroGoodsClassID);
		}

		public bool DeleteList(string MicroGoodsClassIDlist)
		{
			return this.dal.DeleteList(MicroGoodsClassIDlist);
		}

		public Chain.Model.MicroWebsiteGoodsClass GetModel(int MicroGoodsClassID)
		{
			return this.dal.GetModel(MicroGoodsClassID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.MicroWebsiteGoodsClass> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MicroWebsiteGoodsClass> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MicroWebsiteGoodsClass> modelList = new List<Chain.Model.MicroWebsiteGoodsClass>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MicroWebsiteGoodsClass model = new Chain.Model.MicroWebsiteGoodsClass();
					if (dt.Rows[i]["MicroGoodsClassID"] != null && dt.Rows[i]["MicroGoodsClassID"].ToString() != "")
					{
						model.MicroGoodsClassID = int.Parse(dt.Rows[i]["MicroGoodsClassID"].ToString());
					}
					if (dt.Rows[i]["MicroGoodsClassName"] != null && dt.Rows[i]["MicroGoodsClassName"].ToString() != "")
					{
						model.MicroGoodsClassName = dt.Rows[i]["MicroGoodsClassName"].ToString();
					}
					if (dt.Rows[i]["MicroGoodsClassRemark"] != null && dt.Rows[i]["MicroGoodsClassRemark"].ToString() != "")
					{
						model.MicroGoodsClassRemark = dt.Rows[i]["MicroGoodsClassRemark"].ToString();
					}
					if (dt.Rows[i]["MicroGoodsClassShopID"] != null && dt.Rows[i]["MicroGoodsClassShopID"].ToString() != "")
					{
						model.MicroGoodsClassShopID = int.Parse(dt.Rows[i]["MicroGoodsClassShopID"].ToString());
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
