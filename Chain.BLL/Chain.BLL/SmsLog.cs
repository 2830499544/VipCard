using Chain.DAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SmsLog
	{
		private readonly Chain.DAL.SmsLog dal = new Chain.DAL.SmsLog();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int SmsID)
		{
			return this.dal.Exists(SmsID);
		}

		public int Add(Chain.Model.SmsLog model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SmsLog model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int SmsID)
		{
			return this.dal.Delete(SmsID);
		}

		public bool DeleteList(string SmsIDlist)
		{
			return this.dal.DeleteList(SmsIDlist);
		}

		public Chain.Model.SmsLog GetModel(int SmsID)
		{
			return this.dal.GetModel(SmsID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetSmsShopReport(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetSmsShopReport(PageSize, PageIndex, out resCount, strWhere);
		}

		public int GetSmsMonthNumber(string strWhere)
		{
			return this.dal.GetSmsMonthNumber(strWhere);
		}

		public int GetSmsTotalNumber(string strWhere)
		{
			return this.dal.GetSmsTotalNumber(strWhere);
		}

		public DataSet GetSmsShopReportDetail(string strShopID)
		{
			return this.dal.GetSmsShopReportDetail(strShopID);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SmsLog> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SmsLog> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SmsLog> modelList = new List<Chain.Model.SmsLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SmsLog model = new Chain.Model.SmsLog();
					if (dt.Rows[i]["SmsID"] != null && dt.Rows[i]["SmsID"].ToString() != "")
					{
						model.SmsID = int.Parse(dt.Rows[i]["SmsID"].ToString());
					}
					if (dt.Rows[i]["SmsMemID"] != null && dt.Rows[i]["SmsMemID"].ToString() != "")
					{
						model.SmsMemID = int.Parse(dt.Rows[i]["SmsMemID"].ToString());
					}
					if (dt.Rows[i]["SmsMobile"] != null && dt.Rows[i]["SmsMobile"].ToString() != "")
					{
						model.SmsMobile = dt.Rows[i]["SmsMobile"].ToString();
					}
					if (dt.Rows[i]["SmsContent"] != null && dt.Rows[i]["SmsContent"].ToString() != "")
					{
						model.SmsContent = dt.Rows[i]["SmsContent"].ToString();
					}
					if (dt.Rows[i]["SmsShopID"] != null && dt.Rows[i]["SmsShopID"].ToString() != "")
					{
						model.SmsShopID = int.Parse(dt.Rows[i]["SmsShopID"].ToString());
					}
					if (dt.Rows[i]["SmsTime"] != null && dt.Rows[i]["SmsTime"].ToString() != "")
					{
						model.SmsTime = DateTime.Parse(dt.Rows[i]["SmsTime"].ToString());
					}
					if (dt.Rows[i]["SmsUserID"] != null && dt.Rows[i]["SmsUserID"].ToString() != "")
					{
						model.SmsUserID = int.Parse(dt.Rows[i]["SmsUserID"].ToString());
					}
					if (dt.Rows[0]["SmsAmount"] != null && dt.Rows[0]["SmsAmount"].ToString() != "")
					{
						model.SmsAmount = int.Parse(dt.Rows[0]["SmsAmount"].ToString());
					}
					if (dt.Rows[0]["SmsAllAmount"] != null && dt.Rows[0]["SmsAllAmount"].ToString() != "")
					{
						model.SmsAllAmount = int.Parse(dt.Rows[0]["SmsAllAmount"].ToString());
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
	}
}
