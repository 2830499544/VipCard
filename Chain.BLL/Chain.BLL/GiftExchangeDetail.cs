using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class GiftExchangeDetail
	{
		private readonly Chain.IDAL.GiftExchangeDetail dal = new Chain.IDAL.GiftExchangeDetail();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ExchangeDetailID)
		{
			return this.dal.Exists(ExchangeDetailID);
		}

		public int Add(Chain.Model.GiftExchangeDetail model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.GiftExchangeDetail model)
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

		public Chain.Model.GiftExchangeDetail GetModel(int ExchangeDetailID)
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

		public List<Chain.Model.GiftExchangeDetail> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.GiftExchangeDetail> DataTableToList(DataTable dt)
		{
			List<Chain.Model.GiftExchangeDetail> modelList = new List<Chain.Model.GiftExchangeDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.GiftExchangeDetail model = new Chain.Model.GiftExchangeDetail();
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
						model.Giftname = dt.Rows[i]["GiftName"].ToString();
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

		public DataTable GetExchangeAccountByMemID(int memID)
		{
			return this.dal.GetExchangeAccountByMemID(memID);
		}

		public DataTable GetGiftExchangeInfoBystrWhere(string strWhere)
		{
			return this.dal.GetGiftExchangeInfoBystrWhere(strWhere);
		}

		public DataTable GetGiftExchangeDetailByExchangeID(int exchangeID)
		{
			return this.dal.GetGiftExchangeDetailByExchangeID(exchangeID);
		}

		public DataTable GetWeiXinList(int ExchangeID)
		{
			return this.dal.GetWeiXinList(ExchangeID);
		}

		public DataTable AgainPrint(int rechargeID)
		{
			return this.dal.AgainPrint(rechargeID);
		}
	}
}
