using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysError
	{
		private readonly Chain.IDAL.SysError dal = new Chain.IDAL.SysError();

		public bool Exists(int ID)
		{
			return this.dal.Exists(ID);
		}

		public int Add(Chain.Model.SysError model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SysError model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID)
		{
			return this.dal.Delete(ID);
		}

		public bool DeleteList(string IDlist)
		{
			return this.dal.DeleteList(IDlist);
		}

		public Chain.Model.SysError GetModel(int ID)
		{
			return this.dal.GetModel(ID);
		}

		public DataSet GetErrorContent(string strWhere)
		{
			return this.dal.GetErrorContent(strWhere);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysError> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysError> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysError> modelList = new List<Chain.Model.SysError>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysError model = new Chain.Model.SysError();
					if (dt.Rows[i]["ID"] != null && dt.Rows[i]["ID"].ToString() != "")
					{
						model.ID = int.Parse(dt.Rows[i]["ID"].ToString());
					}
					if (dt.Rows[i]["ErrorContent"] != null && dt.Rows[i]["ErrorContent"].ToString() != "")
					{
						model.ErrorContent = dt.Rows[i]["ErrorContent"].ToString();
					}
					if (dt.Rows[i]["ErrorTime"] != null && dt.Rows[i]["ErrorTime"].ToString() != "")
					{
						model.ErrorTime = DateTime.Parse(dt.Rows[i]["ErrorTime"].ToString());
					}
					if (dt.Rows[i]["Ipaddress"] != null && dt.Rows[i]["Ipaddress"].ToString() != "")
					{
						model.Ipaddress = dt.Rows[i]["Ipaddress"].ToString();
					}
					if (dt.Rows[i]["ErrorType"] != null && dt.Rows[i]["ErrorType"].ToString() != "")
					{
						model.ErrorType = dt.Rows[i]["ErrorType"].ToString();
					}
					if (dt.Rows[i]["UserID"] != null && dt.Rows[i]["UserID"].ToString() != "")
					{
						model.UserID = Convert.ToInt32(dt.Rows[i]["UserID"]);
					}
					if (dt.Rows[i]["ShopID"] != null && dt.Rows[i]["ShopID"].ToString() != "")
					{
						model.ShopID = Convert.ToInt32(dt.Rows[i]["ShopID"]);
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

		public int CleadSysError(int strDay)
		{
			return this.dal.CleadSysError(strDay);
		}

		public static void Add(string Content, string IP, int UserID, int ShopID)
		{
			Chain.Model.SysError model = new Chain.Model.SysError();
			model.ErrorTime = DateTime.Now;
			model.ErrorContent = Content;
			model.Ipaddress = IP;
			model.UserID = UserID;
			model.ShopID = ShopID;
			new SysError().Add(model);
		}

		public static void Add(Exception Err, string IP)
		{
			string content = string.Format("Error Message:{0}\r\nStack Trace:{1}", Err.Message, Err.StackTrace);
			SysError.Add(content, IP, 0, 0);
		}
	}
}
