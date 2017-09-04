using Chain.IDAL;
using Chain.Model;
using System;
using System.Data;

namespace Chain.BLL
{
	public class ReturnPointLog
	{
		private readonly Chain.IDAL.ReturnPointLog dal = new Chain.IDAL.ReturnPointLog();

		public int Add(Chain.Model.ReturnPointLog model)
		{
			return this.dal.Add(model);
		}

		public Chain.Model.ReturnPointLog GetModelByOrderAccount(string OrderAccount)
		{
			return this.dal.GetModelByOrderAccount(OrderAccount);
		}

		public int GetRecordCount(string strWhere)
		{
			return this.dal.GetRecordCount(strWhere);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}
	}
}
