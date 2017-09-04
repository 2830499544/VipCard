using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class PointExchange
	{
		private readonly Chain.IDAL.PointExchange dal = new Chain.IDAL.PointExchange();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ExchangeID)
		{
			return this.dal.Exists(ExchangeID);
		}

		public int Add(Chain.Model.PointExchange model)
		{
			return this.dal.Add(model);
		}

		public bool Add(List<Chain.Model.PointExchange> list)
		{
			return this.dal.Add(list);
		}

		public bool Update(Chain.Model.PointExchange model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ExchangeID)
		{
			return this.dal.Delete(ExchangeID);
		}

		public bool DeleteList(string ExchangeIDlist)
		{
			return this.dal.DeleteList(ExchangeIDlist);
		}

		public Chain.Model.PointExchange GetModel(int ExchangeID)
		{
			return this.dal.GetModel(ExchangeID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.PointExchange> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.PointExchange> DataTableToList(DataTable dt)
		{
			List<Chain.Model.PointExchange> modelList = new List<Chain.Model.PointExchange>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.PointExchange model = new Chain.Model.PointExchange();
					if (dt.Rows[i]["ExchangeID"] != null && dt.Rows[i]["ExchangeID"].ToString() != "")
					{
						model.ExchangeID = int.Parse(dt.Rows[i]["ExchangeID"].ToString());
					}
					if (dt.Rows[i]["ExchangeMemID"] != null && dt.Rows[i]["ExchangeMemID"].ToString() != "")
					{
						model.ExchangeMemID = int.Parse(dt.Rows[i]["ExchangeMemID"].ToString());
					}
					if (dt.Rows[i]["ExchangeGiftID"] != null && dt.Rows[i]["ExchangeGiftID"].ToString() != "")
					{
						model.ExchangeGiftID = int.Parse(dt.Rows[i]["ExchangeGiftID"].ToString());
					}
					if (dt.Rows[i]["ExchangeNumber"] != null && dt.Rows[i]["ExchangeNumber"].ToString() != "")
					{
						model.ExchangeNumber = int.Parse(dt.Rows[i]["ExchangeNumber"].ToString());
					}
					if (dt.Rows[i]["ExchangeTotalPoint"] != null && dt.Rows[i]["ExchangeTotalPoint"].ToString() != "")
					{
						model.ExchangeTotalPoint = int.Parse(dt.Rows[i]["ExchangeTotalPoint"].ToString());
					}
					if (dt.Rows[i]["ExchangeShopID"] != null && dt.Rows[i]["ExchangeShopID"].ToString() != "")
					{
						model.ExchangeShopID = int.Parse(dt.Rows[i]["ExchangeShopID"].ToString());
					}
					if (dt.Rows[i]["ExchangeTime"] != null && dt.Rows[i]["ExchangeTime"].ToString() != "")
					{
						model.ExchangeTime = DateTime.Parse(dt.Rows[i]["ExchangeTime"].ToString());
					}
					if (dt.Rows[i]["ExchangeUserID"] != null && dt.Rows[i]["ExchangeUserID"].ToString() != "")
					{
						model.ExchangeUserID = int.Parse(dt.Rows[i]["ExchangeUserID"].ToString());
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
