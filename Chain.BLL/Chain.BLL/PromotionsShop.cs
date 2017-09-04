using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class PromotionsShop
	{
		private readonly Chain.IDAL.PromotionsShop dal = new Chain.IDAL.PromotionsShop();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ClassID)
		{
			return this.dal.Exists(ClassID);
		}

		public int Add(Chain.Model.PromotionsShop model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.PromotionsShop model)
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

		public Chain.Model.PromotionsShop GetModel(int ClassID)
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

		public List<Chain.Model.PromotionsShop> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public DataSet GetGoodsClassInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetGoodsClassInfo(PageSize, PageIndex, out resCount, strWhere);
		}

		public List<Chain.Model.PromotionsShop> DataTableToList(DataTable dt)
		{
			List<Chain.Model.PromotionsShop> modelList = new List<Chain.Model.PromotionsShop>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.PromotionsShop model = new Chain.Model.PromotionsShop();
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
