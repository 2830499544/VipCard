using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysModuleAction
	{
		private readonly Chain.IDAL.SysModuleAction dal = new Chain.IDAL.SysModuleAction();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ActionID)
		{
			return this.dal.Exists(ActionID);
		}

		public int Add(Chain.Model.SysModuleAction model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SysModuleAction model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ActionID)
		{
			return this.dal.Delete(ActionID);
		}

		public bool DeleteList(string ActionIDlist)
		{
			return this.dal.DeleteList(ActionIDlist);
		}

		public Chain.Model.SysModuleAction GetModel(int ActionID)
		{
			return this.dal.GetModel(ActionID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysModuleAction> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysModuleAction> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysModuleAction> modelList = new List<Chain.Model.SysModuleAction>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysModuleAction model = this.dal.DataRowToModel(dt.Rows[i]);
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

		public int GetRecordCount(string strWhere)
		{
			return this.dal.GetRecordCount(strWhere);
		}

		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
		}

		public DataSet GetGroupMenuAction(int PageID)
		{
			return this.dal.GetGroupMenuAction(PageID);
		}

		public Chain.Model.SysModuleAction GetModelByModuleIDAndControl(int actionModuleID, string actionControl)
		{
			return this.dal.GetModelByModuleIDAndControl(actionModuleID, actionControl);
		}

		public List<Chain.Model.SysModuleAction> GetList(int actionModuleID)
		{
			return this.DataTableToList(this.dal.GetGroupMenuActionNotPage(actionModuleID).Tables[0]);
		}
	}
}
