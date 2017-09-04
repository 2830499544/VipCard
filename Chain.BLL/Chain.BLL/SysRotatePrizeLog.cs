using Chain.IDAL;
using Chain.Model;
using System;
using System.Data;

namespace Chain.BLL
{
	public class SysRotatePrizeLog
	{
		private readonly Chain.IDAL.SysRotatePrizeLog dal = new Chain.IDAL.SysRotatePrizeLog();

		public bool Exists(int LogID)
		{
			return this.dal.Exists(LogID);
		}

		public bool ExistsPrizeCode(int RotateID, string PrizeCode)
		{
			return this.dal.ExistsPrizeCode(RotateID, PrizeCode);
		}

		public int Add(Chain.Model.SysRotatePrizeLog model)
		{
			return this.dal.Add(model);
		}

		public int Update(Chain.Model.SysRotatePrizeLog model)
		{
			return this.dal.Update(model);
		}

		public bool DeleteList(string LogIDlist)
		{
			return this.dal.DeleteList(LogIDlist);
		}

		public Chain.Model.SysRotatePrizeLog GetModel(int LogID)
		{
			return this.dal.GetModel(LogID);
		}

		public int GetMemRotateCount(int MemID, int RotateID)
		{
			return this.dal.GetMemRotateCount(MemID, RotateID);
		}

		public int GetRotateCount(int RotateID)
		{
			return this.dal.GetRotateCount(RotateID);
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

		public DataSet GetListSP(bool isasc, string order, int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(isasc, order, PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetListSPNew(int PageSize, int PageIndex, params string[] strWhere)
		{
			return this.dal.GetListSPNew(PageSize, PageIndex, strWhere);
		}

		public DataSet GetListNew(string strWhere)
		{
			return this.dal.GetListNew(strWhere);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}
	}
}
