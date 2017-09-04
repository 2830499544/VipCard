using Chain.IDAL;
using Chain.Model;
using System;
using System.Data;

namespace Chain.BLL
{
	public class WeiXinMoneyMem
	{
		private readonly Chain.IDAL.WeiXinMoneyMem dal = new Chain.IDAL.WeiXinMoneyMem();

		public int Add(Chain.Model.WeiXinMoneyMem model)
		{
			return this.dal.Add(model);
		}

		public bool Delete(int MoneyID)
		{
			return this.dal.Delete(MoneyID);
		}

		public Chain.Model.WeiXinMoneyMem GetModel(int MoneyID)
		{
			return this.dal.GetModel(MoneyID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, string[] strWhere, out int resCount)
		{
			return this.dal.GetListSP(PageSize, PageIndex, strWhere, out resCount);
		}

		public int GetRecordCount(string strWhere)
		{
			return this.dal.GetRecordCount(strWhere);
		}
	}
}
