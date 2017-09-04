using Chain.IDAL;
using Chain.Model;
using System;
using System.Data;

namespace Chain.BLL
{
	public class Special
	{
		private readonly Chain.IDAL.Special dal = new Chain.IDAL.Special();

		public DataSet GetItemAll(int SpecialID)
		{
			return this.dal.GetItemAll(SpecialID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetListsp(int PageSiza, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListsp(PageSiza, PageIndex, out resCount, strWhere);
		}

		public int Update(Chain.Model.Special model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int SpecialID)
		{
			return this.dal.Delete(SpecialID);
		}

		public int Add(Chain.Model.Special model)
		{
			return this.dal.Add(model);
		}
	}
}
