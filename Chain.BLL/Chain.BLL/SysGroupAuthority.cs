using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysGroupAuthority
	{
		private readonly Chain.IDAL.SysGroupAuthority dal = new Chain.IDAL.SysGroupAuthority();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int GAID)
		{
			return this.dal.Exists(GAID);
		}

		public int Add(Chain.Model.SysGroupAuthority model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SysGroupAuthority model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int GAID)
		{
			return this.dal.Delete(GAID);
		}

		public bool DeleteList(string GAIDlist)
		{
			return this.dal.DeleteList(GAIDlist);
		}

		public bool DeleteList(int GroupID)
		{
			return this.dal.DeleteList(GroupID);
		}

		public Chain.Model.SysGroupAuthority GetModel(int GAID)
		{
			return this.dal.GetModel(GAID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysGroupAuthority> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysGroupAuthority> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysGroupAuthority> modelList = new List<Chain.Model.SysGroupAuthority>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysGroupAuthority model = this.dal.DataRowToModel(dt.Rows[i]);
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

		public bool UpdateOrEndowPower(Chain.Model.SysGroupAuthority sysGroupAuthorityModel)
		{
			DataTable dt = this.dal.GetList(sysGroupAuthorityModel.GroupID.Value, sysGroupAuthorityModel.ModuleID.Value, sysGroupAuthorityModel.ActionID.Value);
			bool result;
			if (dt.Rows.Count > 0)
			{
				Chain.Model.SysGroupAuthority model = this.dal.DataRowToModel(dt.Rows[0]);
				model.ActionValue = sysGroupAuthorityModel.ActionValue;
				result = this.dal.Update(model);
			}
			else
			{
				int flag = this.Add(sysGroupAuthorityModel);
				result = (flag > 0);
			}
			return result;
		}

		public bool isPowerReg(int groupID, int moduleID, int actionID)
		{
			DataTable dt = this.dal.GetList(groupID, moduleID, actionID);
			return dt.Rows.Count > 0 && this.dal.DataRowToModel(dt.Rows[0]).ActionValue;
		}

		public bool CheckChildGroup(int GroupID)
		{
			return this.dal.CheckChildGroup(GroupID);
		}

		public int getModuleID(string AbsolutePath)
		{
			Chain.IDAL.SysGroupAuthority Spa = new Chain.IDAL.SysGroupAuthority();
			return Spa.getModuleID(AbsolutePath);
		}
	}
}
