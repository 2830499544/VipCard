using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class PointLog
	{
		private readonly Chain.IDAL.PointLog dal = new Chain.IDAL.PointLog();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int PointID)
		{
			return this.dal.Exists(PointID);
		}

		public bool Exists(string account)
		{
			return this.dal.Exists(account);
		}

		public int Add(Chain.Model.PointLog model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.PointLog model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int PointID)
		{
			return this.dal.Delete(PointID);
		}

		public bool DeleteList(string PointIDlist)
		{
			return this.dal.DeleteList(PointIDlist);
		}

		public Chain.Model.PointLog GetModel(int PointID)
		{
			return this.dal.GetModel(PointID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.PointLog> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.PointLog> DataTableToList(DataTable dt)
		{
			List<Chain.Model.PointLog> modelList = new List<Chain.Model.PointLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.PointLog model = new Chain.Model.PointLog();
					if (dt.Rows[i]["PointID"] != null && dt.Rows[i]["PointID"].ToString() != "")
					{
						model.PointID = int.Parse(dt.Rows[i]["PointID"].ToString());
					}
					if (dt.Rows[i]["PointMemID"] != null && dt.Rows[i]["PointMemID"].ToString() != "")
					{
						model.PointMemID = int.Parse(dt.Rows[i]["PointMemID"].ToString());
					}
					if (dt.Rows[i]["PointNumber"] != null && dt.Rows[i]["PointNumber"].ToString() != "")
					{
						model.PointNumber = int.Parse(dt.Rows[i]["PointNumber"].ToString());
					}
					if (dt.Rows[i]["PointChangeType"] != null && dt.Rows[i]["PointChangeType"].ToString() != "")
					{
						model.PointChangeType = int.Parse(dt.Rows[i]["PointChangeType"].ToString());
					}
					if (dt.Rows[i]["PointRemark"] != null && dt.Rows[i]["PointRemark"].ToString() != "")
					{
						model.PointRemark = dt.Rows[i]["PointRemark"].ToString();
					}
					if (dt.Rows[i]["PointShopID"] != null && dt.Rows[i]["PointShopID"].ToString() != "")
					{
						model.PointShopID = int.Parse(dt.Rows[i]["PointShopID"].ToString());
					}
					if (dt.Rows[i]["PointCreateTime"] != null && dt.Rows[i]["PointCreateTime"].ToString() != "")
					{
						model.PointCreateTime = DateTime.Parse(dt.Rows[i]["PointCreateTime"].ToString());
					}
					if (dt.Rows[i]["PointUserID"] != null && dt.Rows[i]["PointUserID"].ToString() != "")
					{
						model.PointUserID = int.Parse(dt.Rows[i]["PointUserID"].ToString());
					}
					if (dt.Rows[i]["PointOrderCode"] != null && dt.Rows[i]["PointOrderCode"].ToString() != "")
					{
						model.PointOrderCode = dt.Rows[i]["PointOrderCode"].ToString();
					}
					if (dt.Rows[i]["PointGiveMemID"] != null && dt.Rows[i]["PointGiveMemID"].ToString() != "")
					{
						model.PointGiveMemID = Convert.ToInt32(dt.Rows[i]["PointGiveMemID"]);
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

		public int GetPointNumber(string strWhere)
		{
			return this.dal.GetPointNumber(strWhere);
		}

		public DataSet GetPointByTime(DateTime starttime, DateTime endtime, string strwhere)
		{
			return this.dal.GetPointByTime(starttime, endtime, strwhere);
		}

		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public int DeleteLog(string strOrderAccount)
		{
			return this.dal.DeleteLog(strOrderAccount);
		}

		public int UpdatePointLog(string strOrderCode, int intPointNumber, int intMemID, string strRemark)
		{
			return this.dal.UpdatePointLog(strOrderCode, intPointNumber, intMemID, strRemark);
		}

		public int MemPointRollback(int memID, int intPointNumber)
		{
			return this.dal.UpdateMemPoint(memID, intPointNumber);
		}

		public DataTable AgainPrint(int pointID)
		{
			return this.dal.AgainPrint(pointID);
		}

		public int GetPointChange(string strSql)
		{
			return this.dal.GetPointChange(strSql);
		}

		public bool IsSignedToday(int memId)
		{
			return this.dal.IsSignedToday(memId, 16);
		}
	}
}
