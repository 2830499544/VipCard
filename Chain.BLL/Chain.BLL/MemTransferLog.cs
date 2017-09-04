using Chain.DAL;
using Chain.Model;
using System;
using System.Data;

namespace Chain.BLL
{
	public class MemTransferLog
	{
		private readonly Chain.DAL.MemTransferLog dal = new Chain.DAL.MemTransferLog();

		public int Add(Chain.Model.MemTransferLog model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.MemTransferLog model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID)
		{
			return this.dal.Delete(ID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetMoneyCount(string strSql)
		{
			return this.dal.GetMoneyCount(strSql);
		}
	}
}
