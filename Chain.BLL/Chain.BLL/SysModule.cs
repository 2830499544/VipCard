using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysModule
	{
		private readonly Chain.IDAL.SysModule dal = new Chain.IDAL.SysModule();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ModuleID)
		{
			return this.dal.Exists(ModuleID);
		}

		public int Add(Chain.Model.SysModule model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SysModule model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ModuleID)
		{
			return this.dal.Delete(ModuleID);
		}

		public bool DeleteList(string ModuleIDlist)
		{
			return this.dal.DeleteList(ModuleIDlist);
		}

		public Chain.Model.SysModule GetModel(int ModuleID)
		{
			return this.dal.GetModel(ModuleID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysModule> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysModule> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysModule> modelList = new List<Chain.Model.SysModule>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysModule model = this.dal.DataRowToModel(dt.Rows[i]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		public DataSet GetAllList()
		{
			return this.GetList("");
		}

		public DataSet GetAllList(string strSql)
		{
			return this.GetList(strSql);
		}

		public int GetRecordCount(string strWhere)
		{
			return this.dal.GetRecordCount(strWhere);
		}

		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
		}

		public int UpdateIsDataInit(int intType)
		{
			return this.dal.UpdateIsDataInit(intType);
		}

		public void SetEnableGoods(bool enableGoods)
		{
			string strStartGood = "66,67,69,76,87,91,113,114,118";
			string strStartGoodBar = "60";
			List<bool> ststus = new List<bool>
			{
				enableGoods,
				enableGoods
			};
			List<string> moduleIDs = new List<string>
			{
				strStartGoodBar,
				strStartGood
			};
			SysParameter bllUpdateParameter = new SysParameter();
			bllUpdateParameter.SwitchingMode(ststus, moduleIDs);
		}

		public List<Chain.Model.SysModule> GetModelList(int moduleParentID)
		{
			return this.DataTableToList(this.dal.GetList(moduleParentID));
		}
	}
}
