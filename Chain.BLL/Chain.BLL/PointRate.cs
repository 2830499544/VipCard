using Chain.IDAL;
using Chain.Model;
using System;
using System.Data;

namespace Chain.BLL
{
	public class PointRate
	{
		private readonly Chain.IDAL.PointRate dal = new Chain.IDAL.PointRate();

		public int Update(Chain.Model.PointRate model)
		{
			return this.dal.Update(model);
		}

		public DataRow GetDataRow()
		{
			return this.dal.GetDataRow();
		}

		public Chain.Model.PointRate GetPointRate()
		{
			return this.dal.GetPointRate();
		}

		public int GetPointRateNumber(string strWhere)
		{
			return this.dal.GetPointRateNumber(strWhere);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, string strTime, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strTime, strWhere);
		}

		public DataSet GetMyTeamList(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetMyTeamList(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetMemDetailByMemCard(int MemID, string strWhere)
		{
			return this.dal.GetMemDetailByMemCard(MemID, strWhere);
		}

		public DataSet GetMemDetailByMemID(int MemID, string strWhere)
		{
			return this.dal.GetMemDetailByMemID(MemID, strWhere);
		}

		public DataSet GetPointLog(string strWhere)
		{
			return this.dal.GetPointLog(strWhere);
		}
	}
}
