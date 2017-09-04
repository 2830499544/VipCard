using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class GiftExchange
	{
		private readonly Chain.IDAL.GiftExchange dal = new Chain.IDAL.GiftExchange();

		public int GetMemExchangePoint(string strWhere)
		{
			return this.dal.GetMemExchangePoint(strWhere);
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ExchangeID)
		{
			return this.dal.Exists(ExchangeID);
		}

		public int Add(Chain.Model.GiftExchange model)
		{
			return this.dal.Add(model);
		}

		public int MainSystemAdd(Chain.Model.GiftExchange model)
		{
			return this.dal.MainSystemAdd(model);
		}

		public bool Update(Chain.Model.GiftExchange model)
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

		public Chain.Model.GiftExchange GetModel(int ExchangeID)
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

		public List<Chain.Model.GiftExchange> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.GiftExchange> DataTableToList(DataTable dt)
		{
			List<Chain.Model.GiftExchange> modelList = new List<Chain.Model.GiftExchange>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.GiftExchange model = new Chain.Model.GiftExchange();
					if (dt.Rows[i]["ExchangeID"] != null && dt.Rows[i]["ExchangeID"].ToString() != "")
					{
						model.ExchangeID = int.Parse(dt.Rows[i]["ExchangeID"].ToString());
					}
					if (dt.Rows[i]["MemID"] != null && dt.Rows[i]["MemID"].ToString() != "")
					{
						model.MemID = int.Parse(dt.Rows[i]["MemID"].ToString());
					}
					if (dt.Rows[i]["ExchangeTelePhone"] != null && dt.Rows[i]["ExchangeTelePhone"].ToString() != "")
					{
						model.ExchangeTelePhone = dt.Rows[i]["ExchangeTelePhone"].ToString();
					}
					if (dt.Rows[i]["ExchangeAddress"] != null && dt.Rows[i]["ExchangeAddress"].ToString() != "")
					{
						model.ExchangeAddress = dt.Rows[i]["ExchangeAddress"].ToString();
					}
					if (dt.Rows[i]["ExchangeAccount"] != null && dt.Rows[i]["ExchangeAccount"].ToString() != "")
					{
						model.ExchangeAccount = dt.Rows[i]["ExchangeAccount"].ToString();
					}
					if (dt.Rows[i]["ExchangeAllNumber"] != null && dt.Rows[i]["ExchangeAllNumber"].ToString() != "")
					{
						model.ExchangeAllNumber = int.Parse(dt.Rows[i]["ExchangeAllNumber"].ToString());
					}
					if (dt.Rows[i]["ExchangeAllPoint"] != null && dt.Rows[i]["ExchangeAllPoint"].ToString() != "")
					{
						model.ExchangeAllPoint = int.Parse(dt.Rows[i]["ExchangeAllPoint"].ToString());
					}
					if (dt.Rows[i]["ApplicationTime"] != null && dt.Rows[i]["ApplicationTime"].ToString() != "")
					{
						model.ApplicationTime = DateTime.Parse(dt.Rows[i]["ApplicationTime"].ToString());
					}
					if (dt.Rows[i]["ApplicationRemark"] != null && dt.Rows[i]["ApplicationRemark"].ToString() != "")
					{
						model.ApplicationRemark = dt.Rows[i]["ApplicationRemark"].ToString();
					}
					if (dt.Rows[i]["ExchangeStatus"] != null && dt.Rows[i]["ExchangeStatus"].ToString() != "")
					{
						model.ExchangeStatus = int.Parse(dt.Rows[i]["ExchangeStatus"].ToString());
					}
					if (dt.Rows[i]["ExchangeTime"] != null && dt.Rows[i]["ExchangeTime"].ToString() != "")
					{
						model.ExchangeTime = DateTime.Parse(dt.Rows[i]["ExchangeTime"].ToString());
					}
					if (dt.Rows[i]["ExchangeUserID"] != null && dt.Rows[i]["ExchangeUserID"].ToString() != "")
					{
						model.ExchangeUserID = int.Parse(dt.Rows[i]["ExchangeUserID"].ToString());
					}
					if (dt.Rows[i]["ExchangeRemark"] != null && dt.Rows[i]["ExchangeRemark"].ToString() != "")
					{
						model.ExchangeRemark = dt.Rows[i]["ExchangeRemark"].ToString();
					}
					if (dt.Rows[i]["ExchangeType"] != null && dt.Rows[i]["ExchangeType"].ToString() != "")
					{
						model.ExchangeType = int.Parse(dt.Rows[i]["ExchangeType"].ToString());
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

		public DataSet GetGiftExchangeByTime(DateTime starttime, DateTime endtime, string strwhere)
		{
			return this.dal.GetGiftExchangeByTime(starttime, endtime, strwhere);
		}

		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
		}

		public DataSet GetVerifyListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetVerifyListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetListGiftExchangeSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListGiftExchangeSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetListGiftExchangeSPForWeiXin(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListGiftExchangeSPForWeiXin(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataTable GetExchangeIDByGiftNameOrGiftCode(string giftNameOrGiftCodeStrWhere)
		{
			return this.dal.GetExchangeIDByGiftNameOrGiftCode(giftNameOrGiftCodeStrWhere);
		}

		public DataTable AgainPrint(int rechargeID)
		{
			return this.dal.AgainPrint(rechargeID);
		}

		public DataSet GetGiftExchange(string strSql)
		{
			return this.dal.GetGiftExchange(strSql);
		}
	}
}
