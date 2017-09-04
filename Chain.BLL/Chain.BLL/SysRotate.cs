using Chain.IDAL;
using Chain.Model;
using System;
using System.Data;

namespace Chain.BLL
{
	public class SysRotate
	{
		private readonly Chain.IDAL.SysRotate dal = new Chain.IDAL.SysRotate();

		public int UpdateWinCount(Chain.Model.SysRotate model)
		{
			return this.dal.UpdateWinCount(model);
		}

		public bool Exists(int RotateID)
		{
			return this.dal.Exists(RotateID);
		}

		public int Add(Chain.Model.SysRotate model)
		{
			return this.dal.Add(model);
		}

		public int Update(Chain.Model.SysRotate model)
		{
			return this.dal.Update(model);
		}

		public int Delete(int RotateID)
		{
			int result;
			if (this.Exists(RotateID))
			{
				result = -2;
			}
			else
			{
				result = this.dal.Delete(RotateID);
			}
			return result;
		}

		public Chain.Model.SysRotate GetModelByDate()
		{
			return this.dal.GetModelByDate();
		}

		public string GetRotateIDByRotateRegion(string RotateRegion)
		{
			return this.dal.GetRotateIDByRotateRegion(RotateRegion);
		}

		public Chain.Model.SysRotate GetModel(int LogID)
		{
			return this.dal.GetModel(LogID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetAllList()
		{
			return this.GetList("");
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
