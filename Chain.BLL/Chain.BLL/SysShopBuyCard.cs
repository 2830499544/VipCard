using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysShopBuyCard
	{
		private readonly Chain.IDAL.SysShopBuyCard dal = new Chain.IDAL.SysShopBuyCard();

		public bool Exists(int BuyCardID)
		{
			return this.dal.Exists(BuyCardID);
		}

		public int Add(Chain.Model.SysShopBuyCard model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SysShopBuyCard model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int BuyCardID)
		{
			return this.dal.Delete(BuyCardID);
		}

		public bool DeleteList(string BuyCardIDlist)
		{
			return this.dal.DeleteList(BuyCardIDlist);
		}

		public Chain.Model.SysShopBuyCard GetModel(int BuyCardID)
		{
			return this.dal.GetModel(BuyCardID);
		}

		public string GetCarNum(string StartNum, string EndNum)
		{
			return this.dal.GetCarNum(StartNum, EndNum);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysShopBuyCard> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysShopBuyCard> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysShopBuyCard> modelList = new List<Chain.Model.SysShopBuyCard>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysShopBuyCard model = new Chain.Model.SysShopBuyCard();
					if (dt.Rows[i]["BuyCardID"] != null && dt.Rows[i]["BuyCardID"].ToString() != "")
					{
						model.BuyCardID = int.Parse(dt.Rows[i]["BuyCardID"].ToString());
					}
					if (dt.Rows[i]["StartCardNumber"] != null && dt.Rows[i]["StartCardNumber"].ToString() != "")
					{
						model.StartCardNumber = dt.Rows[i]["StartCardNumber"].ToString();
					}
					if (dt.Rows[i]["EndCardNumber"] != null && dt.Rows[i]["EndCardNumber"].ToString() != "")
					{
						model.EndCardNumber = dt.Rows[i]["EndCardNumber"].ToString();
					}
					if (dt.Rows[i]["BuyCardShopid"] != null && dt.Rows[i]["BuyCardShopid"].ToString() != "")
					{
						model.BuyCardShopid = int.Parse(dt.Rows[i]["BuyCardShopid"].ToString());
					}
					if (dt.Rows[i]["UserID"] != null && dt.Rows[i]["UserID"].ToString() != "")
					{
						model.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
					}
					if (dt.Rows[i]["Remark"] != null && dt.Rows[i]["Remark"].ToString() != "")
					{
						model.Remark = dt.Rows[i]["Remark"].ToString();
					}
					if (dt.Rows[i]["BuyCardTime"] != null && dt.Rows[i]["BuyCardTime"].ToString() != "")
					{
						model.BuyCardTime = DateTime.Parse(dt.Rows[i]["BuyCardTime"].ToString());
					}
					if (dt.Rows[i]["BuyCardMoney"] != null && dt.Rows[i]["BuyCardMoney"].ToString() != "")
					{
						model.BuyCardMoney = decimal.Parse(dt.Rows[i]["BuyCardMoney"].ToString());
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
			return this.dal.GetListSP(PageSize, PageIndex, strWhere, out resCount);
		}
	}
}
