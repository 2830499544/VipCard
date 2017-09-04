using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysRotateCount
	{
		private readonly Chain.IDAL.SysRotateCount dal = new Chain.IDAL.SysRotateCount();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ClassID)
		{
			return this.dal.Exists(ClassID);
		}

		public bool Exists(int intClassID, string strClassName, int ShopID)
		{
			return this.dal.Exists(intClassID, strClassName, ShopID);
		}

		public int GetDaysByCoinAmount(decimal coinAmount)
		{
			return this.dal.GetDaysByCoinAmount(coinAmount);
		}

		public int Add(Chain.Model.SysRotateCount model)
		{
			return this.dal.Add(model);
		}

		public int Update(Chain.Model.SysRotateCount model)
		{
			return this.dal.Update(model);
		}

		public int Delete(int CoinID)
		{
			return this.dal.Delete(CoinID);
		}

		public bool DeleteList(string ClassIDlist)
		{
			return this.dal.DeleteList(ClassIDlist);
		}

		public Chain.Model.SysRotateCount GetModelbyRotateID(int RotateID)
		{
			return this.dal.GetModelbyRotateID(RotateID);
		}

		public Chain.Model.SysRotateCount GetModel(int ClassID)
		{
			return this.dal.GetModel(ClassID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysRotateCount> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysRotateCount> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysRotateCount> modelList = new List<Chain.Model.SysRotateCount>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysRotateCount model = new Chain.Model.SysRotateCount();
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

		public decimal GetMemStorageTimingCostAmount(string starttime, string endtime, int MemID)
		{
			return this.dal.GetMemStorageTimingCostAmount(starttime, endtime, MemID);
		}

		public decimal GetMemCountCostAmount(string starttime, string endtime, int MemID)
		{
			return this.dal.GetMemCountCostAmount(starttime, endtime, MemID);
		}

		public decimal GetMemOrderLogCostAmount(string starttime, string endtime, int MemID)
		{
			return this.dal.GetMemOrderLogCostAmount(starttime, endtime, MemID);
		}
	}
}
