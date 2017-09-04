using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysNotice
	{
		private readonly Chain.IDAL.SysNotice dal = new Chain.IDAL.SysNotice();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int SysNoticeID)
		{
			return this.dal.Exists(SysNoticeID);
		}

		public int Add(Chain.Model.SysNotice model)
		{
			return this.dal.Add(model);
		}

		public int Update(Chain.Model.SysNotice model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int SysNoticeID)
		{
			return this.dal.Delete(SysNoticeID);
		}

		public bool DeleteList(string SysNoticeIDlist)
		{
			return this.dal.DeleteList(SysNoticeIDlist);
		}

		public Chain.Model.SysNotice GetModel(int SysNoticeID)
		{
			return this.dal.GetModel(SysNoticeID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysNotice> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysNotice> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysNotice> modelList = new List<Chain.Model.SysNotice>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysNotice model = new Chain.Model.SysNotice();
					if (dt.Rows[i]["SysNoticeID"] != null && dt.Rows[i]["SysNoticeID"].ToString() != "")
					{
						model.SysNoticeID = int.Parse(dt.Rows[i]["SysNoticeID"].ToString());
					}
					if (dt.Rows[i]["SysNoticeCode"] != null && dt.Rows[i]["SysNoticeCode"].ToString() != "")
					{
						model.SysNoticeCode = dt.Rows[i]["SysNoticeCode"].ToString();
					}
					if (dt.Rows[i]["SysNotieceName"] != null && dt.Rows[i]["SysNotieceName"].ToString() != "")
					{
						model.SysNotieceName = dt.Rows[i]["SysNotieceName"].ToString();
					}
					if (dt.Rows[i]["SysNoticeTitle"] != null && dt.Rows[i]["SysNoticeTitle"].ToString() != "")
					{
						model.SysNoticeTitle = dt.Rows[i]["SysNoticeTitle"].ToString();
					}
					if (dt.Rows[i]["SysNoticeDetail"] != null && dt.Rows[i]["SysNoticeDetail"].ToString() != "")
					{
						model.SysNoticeDetail = dt.Rows[i]["SysNoticeDetail"].ToString();
					}
					if (dt.Rows[i]["SysNoticeTime"] != null && dt.Rows[i]["SysNoticeTime"].ToString() != "")
					{
						model.SysNoticeTime = DateTime.Parse(dt.Rows[i]["SysNoticeTime"].ToString());
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
