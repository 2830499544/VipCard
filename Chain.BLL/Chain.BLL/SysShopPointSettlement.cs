using Chain.IDAL;
using Chain.Model;
using System;
using System.Data;

namespace Chain.BLL
{
	public class SysShopPointSettlement
	{
		private readonly Chain.IDAL.SysShopPointSettlement dal = new Chain.IDAL.SysShopPointSettlement();

		public bool Exists(int ID)
		{
			return this.dal.Exists(ID);
		}

		public int Add(Chain.Model.SysShopPointSettlement model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SysShopPointSettlement model)
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

		public void UpDateSettlement()
		{
			this.dal.UpDateSettlement();
		}

		public Chain.Model.SysShopPointSettlement GetModel(int ID)
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

		public DataSet GetListSP(int PageSize, int PageIndex, string join, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, join, out resCount, strWhere);
		}
	}
}
