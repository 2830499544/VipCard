using Chain.IDAL;
using Chain.Model;
using System;
using System.Data;

namespace Chain.BLL
{
	public class WeiXinMoney
	{
		private readonly Chain.IDAL.WeiXinMoney dal = new Chain.IDAL.WeiXinMoney();

		public string GetMoneyIDByMoneyRegion(string MoneyRegion)
		{
			return this.dal.GetMoneyIDByMoneyRegion(MoneyRegion);
		}

		public int Add(Chain.Model.WeiXinMoney model)
		{
			return this.dal.Add(model);
		}

		public bool UpdateGiveMoney(int MoneyID, decimal GiveMoney)
		{
			return this.dal.UpdateGiveMoney(MoneyID, GiveMoney);
		}

		public int Update(Chain.Model.WeiXinMoney model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int MoneyID)
		{
			return this.dal.Delete(MoneyID);
		}

		public Chain.Model.WeiXinMoney GetModel(int MoneyID)
		{
			return this.dal.GetModel(MoneyID);
		}

		public Chain.Model.WeiXinMoney GetModelByWhere(string strWhere)
		{
			return this.dal.GetModelByWhere(strWhere);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public int GetRecordCount(string strWhere)
		{
			return this.dal.GetRecordCount(strWhere);
		}
	}
}
