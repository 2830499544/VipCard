using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysLog
	{
		private readonly Chain.IDAL.SysLog dal = new Chain.IDAL.SysLog();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int LogID)
		{
			return this.dal.Exists(LogID);
		}

		public int Add(Chain.Model.SysLog model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SysLog model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int LogID)
		{
			return this.dal.Delete(LogID);
		}

		public bool DeleteList(string LogIDlist)
		{
			return this.dal.DeleteList(LogIDlist);
		}

		public Chain.Model.SysLog GetModel(int LogID)
		{
			return this.dal.GetModel(LogID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysLog> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysLog> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysLog> modelList = new List<Chain.Model.SysLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysLog model = new Chain.Model.SysLog();
					if (dt.Rows[i]["LogID"] != null && dt.Rows[i]["LogID"].ToString() != "")
					{
						model.LogID = int.Parse(dt.Rows[i]["LogID"].ToString());
					}
					if (dt.Rows[i]["LogUserID"] != null && dt.Rows[i]["LogUserID"].ToString() != "")
					{
						model.LogUserID = int.Parse(dt.Rows[i]["LogUserID"].ToString());
					}
					if (dt.Rows[i]["LogActionID"] != null && dt.Rows[i]["LogActionID"].ToString() != "")
					{
						model.LogActionID = int.Parse(dt.Rows[i]["LogActionID"].ToString());
					}
					if (dt.Rows[i]["LogType"] != null && dt.Rows[i]["LogType"].ToString() != "")
					{
						model.LogType = dt.Rows[i]["LogType"].ToString();
					}
					if (dt.Rows[i]["LogDetail"] != null && dt.Rows[i]["LogDetail"].ToString() != "")
					{
						model.LogDetail = dt.Rows[i]["LogDetail"].ToString();
					}
					if (dt.Rows[i]["LogShopID"] != null && dt.Rows[i]["LogShopID"].ToString() != "")
					{
						model.LogShopID = int.Parse(dt.Rows[i]["LogShopID"].ToString());
					}
					if (dt.Rows[i]["LogCreateTime"] != null && dt.Rows[i]["LogCreateTime"].ToString() != "")
					{
						model.LogCreateTime = DateTime.Parse(dt.Rows[i]["LogCreateTime"].ToString());
					}
					if (dt.Rows[i]["LogIPAdress"] != null && dt.Rows[i]["LogIPAdress"].ToString() != "")
					{
						model.LogIPAdress = dt.Rows[i]["LogIPAdress"].ToString();
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

		public DataSet GetActionList(string MemCard)
		{
			return this.dal.GetActionList(MemCard);
		}

		public int CleadSysLog(int strDay)
		{
			return this.dal.CleadSysLog(strDay);
		}

		public bool DataBaseInit(ArrayList arrSql)
		{
			return this.dal.DataBaseInit(arrSql);
		}
	}
}
