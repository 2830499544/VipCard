using Chain.DAL;
using Chain.Model;
using System;
using System.Data;

namespace Chain.BLL
{
	public class SubMem
	{
		private readonly Chain.DAL.SubMem dal = new Chain.DAL.SubMem();

		public int Add(Chain.Model.SubMem model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SubMem model)
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

		public bool IsHasMemCard(string MemCard,int MemShopID)
		{
			int result = new Mem().Exists(0, MemCard, MemCard, "", MemShopID);
			return result == -1 || result == -2 || this.dal.IsHasMemCard(MemCard);
		}

		public Chain.Model.SubMem GetModel(int ID)
		{
			return this.dal.GetModel(ID);
		}
	}
}
