using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysArea
	{
		private readonly Chain.IDAL.SysArea dal = new Chain.IDAL.SysArea();

		public string GetNameByID(int ID)
		{
			return this.dal.GetNameByID(ID);
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID)
		{
			return this.dal.Exists(ID);
		}

		public bool Exists(int PID, string Name)
		{
			return this.dal.Exists(PID, Name);
		}

		public bool Exists(int ID, int PID, string Name)
		{
			return this.dal.Exists(ID, PID, Name);
		}

		public int Add(Chain.Model.SysArea model)
		{
			int result;
			if (this.Exists(model.PID, model.Name))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Add(model);
			}
			return result;
		}

		public int Update(Chain.Model.SysArea model)
		{
			int result;
			if (this.Exists(model.ID, model.PID, model.Name))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Update(model);
			}
			return result;
		}

		public bool Delete(int ID)
		{
			return this.dal.Delete(ID);
		}

		public bool DeleteList(string IDlist)
		{
			return this.dal.DeleteList(IDlist);
		}

		public Chain.Model.SysArea GetModel(int ID)
		{
			return this.dal.GetModel(ID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysArea> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysArea> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysArea> modelList = new List<Chain.Model.SysArea>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysArea model = new Chain.Model.SysArea();
					if (dt.Rows[i]["ID"] != null && dt.Rows[i]["ID"].ToString() != "")
					{
						model.ID = int.Parse(dt.Rows[i]["ID"].ToString());
					}
					if (dt.Rows[i]["PID"] != null && dt.Rows[i]["PID"].ToString() != "")
					{
						model.PID = int.Parse(dt.Rows[i]["PID"].ToString());
					}
					if (dt.Rows[i]["Name"] != null && dt.Rows[i]["Name"].ToString() != "")
					{
						model.Name = dt.Rows[i]["Name"].ToString();
					}
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

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}
	}
}
