using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MicroWebsiteGiftExchangeDetail
	{
		private readonly Chain.IDAL.MicroWebsiteGiftExchangeDetail dal = new Chain.IDAL.MicroWebsiteGiftExchangeDetail();

		public DataTable GetGiftExchangeDetailByExchangeID(int exchangeID)
		{
			return this.dal.GetGiftExchangeDetailByExchangeID(exchangeID);
		}

		public bool Exists(int ExchangeDetailID)
		{
			return this.dal.Exists(ExchangeDetailID);
		}

		public int Add(Chain.Model.MicroWebsiteGiftExchangeDetail model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.MicroWebsiteGiftExchangeDetail model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ExchangeDetailID)
		{
			return this.dal.Delete(ExchangeDetailID);
		}

		public bool DeleteList(string ExchangeDetailIDlist)
		{
			return this.dal.DeleteList(ExchangeDetailIDlist);
		}

		public Chain.Model.MicroWebsiteGiftExchangeDetail GetModel(int ExchangeDetailID)
		{
			return this.dal.GetModel(ExchangeDetailID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.MicroWebsiteGiftExchangeDetail> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MicroWebsiteGiftExchangeDetail> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MicroWebsiteGiftExchangeDetail> modelList = new List<Chain.Model.MicroWebsiteGiftExchangeDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MicroWebsiteGiftExchangeDetail model = new Chain.Model.MicroWebsiteGiftExchangeDetail();
					if (dt.Rows[i]["ExchangeDetailID"] != null && dt.Rows[i]["ExchangeDetailID"].ToString() != "")
					{
						model.ExchangeDetailID = int.Parse(dt.Rows[i]["ExchangeDetailID"].ToString());
					}
					if (dt.Rows[i]["ExchangeID"] != null && dt.Rows[i]["ExchangeID"].ToString() != "")
					{
						model.ExchangeID = int.Parse(dt.Rows[i]["ExchangeID"].ToString());
					}
					if (dt.Rows[i]["ExchangeGiftID"] != null && dt.Rows[i]["ExchangeGiftID"].ToString() != "")
					{
						model.ExchangeGiftID = int.Parse(dt.Rows[i]["ExchangeGiftID"].ToString());
					}
					if (dt.Rows[i]["ExchangeNumber"] != null && dt.Rows[i]["ExchangeNumber"].ToString() != "")
					{
						model.ExchangeNumber = int.Parse(dt.Rows[i]["ExchangeNumber"].ToString());
					}
					if (dt.Rows[i]["ExchangePoint"] != null && dt.Rows[i]["ExchangePoint"].ToString() != "")
					{
						model.ExchangePoint = int.Parse(dt.Rows[i]["ExchangePoint"].ToString());
					}
					if (dt.Rows[i]["GiftName"] != null && dt.Rows[i]["GiftName"].ToString() != "")
					{
						model.GiftName = dt.Rows[i]["GiftName"].ToString();
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
