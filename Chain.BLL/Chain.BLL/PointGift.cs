using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class PointGift
	{
		private readonly Chain.IDAL.PointGift dal = new Chain.IDAL.PointGift();

		public int GetStockNumber(int GiftID)
		{
			return this.dal.GetStockNumber(GiftID);
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool ExistsAdd(string GiftName, int shopID)
		{
			return this.dal.ExistsAdd(GiftName, shopID);
		}

		public bool Exists(string GiftName, int GiftID, int shopID)
		{
			return this.dal.Exists(GiftName, GiftID, shopID);
		}

		public int Add(Chain.Model.PointGift model)
		{
			int result;
			if (this.ExistsAdd(model.GiftName, model.GiftShopID))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Add(model);
			}
			return result;
		}

		public int Update(Chain.Model.PointGift model)
		{
			int result;
			if (this.Exists(model.GiftName, model.GiftID, model.GiftShopID))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Update(model);
			}
			return result;
		}

		public bool Delete(int GiftID)
		{
			return this.dal.Delete(GiftID);
		}

		public bool DeleteList(string GiftIDlist)
		{
			return this.dal.DeleteList(GiftIDlist);
		}

		public Chain.Model.PointGift GetModel(int GiftID)
		{
			return this.dal.GetModel(GiftID);
		}

		public DataSet GetItemModel(int GiftID)
		{
			return this.dal.GetItemModel(GiftID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.PointGift> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.PointGift> DataTableToList(DataTable dt)
		{
			List<Chain.Model.PointGift> modelList = new List<Chain.Model.PointGift>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.PointGift model = new Chain.Model.PointGift();
					if (dt.Rows[i]["GiftID"] != null && dt.Rows[i]["GiftID"].ToString() != "")
					{
						model.GiftID = int.Parse(dt.Rows[i]["GiftID"].ToString());
					}
					if (dt.Rows[i]["GiftName"] != null && dt.Rows[i]["GiftName"].ToString() != "")
					{
						model.GiftName = dt.Rows[i]["GiftName"].ToString();
					}
					if (dt.Rows[i]["GiftCode"] != null && dt.Rows[i]["GiftCode"].ToString() != "")
					{
						model.GiftCode = dt.Rows[i]["GiftCode"].ToString();
					}
					if (dt.Rows[i]["GiftClassID"] != null && dt.Rows[i]["GiftClassID"].ToString() != "")
					{
						model.GiftClassID = int.Parse(dt.Rows[i]["GiftClassID"].ToString());
					}
					if (dt.Rows[i]["GiftPhoto"] != null && dt.Rows[i]["GiftPhoto"].ToString() != "")
					{
						model.GiftPhoto = dt.Rows[i]["GiftPhoto"].ToString();
					}
					if (dt.Rows[i]["GiftExchangePoint"] != null && dt.Rows[i]["GiftExchangePoint"].ToString() != "")
					{
						model.GiftExchangePoint = int.Parse(dt.Rows[i]["GiftExchangePoint"].ToString());
					}
					if (dt.Rows[i]["GiftStockNumber"] != null && dt.Rows[i]["GiftStockNumber"].ToString() != "")
					{
						model.GiftStockNumber = int.Parse(dt.Rows[i]["GiftStockNumber"].ToString());
					}
					if (dt.Rows[i]["GiftExchangeNumber"] != null && dt.Rows[i]["GiftExchangeNumber"].ToString() != "")
					{
						model.GiftExchangeNumber = int.Parse(dt.Rows[i]["GiftExchangeNumber"].ToString());
					}
					if (dt.Rows[i]["GiftShopID"] != null && dt.Rows[i]["GiftShopID"].ToString() != "")
					{
						model.GiftShopID = int.Parse(dt.Rows[i]["GiftShopID"].ToString());
					}
					if (dt.Rows[i]["GiftRemark"] != null && dt.Rows[i]["GiftRemark"].ToString() != "")
					{
						model.GiftRemark = dt.Rows[i]["GiftRemark"].ToString();
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

		public DataSet GetListSP(int PageSize, int PageIndex, bool IsAsc, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, IsAsc, strWhere, out resCount);
		}

		public DataSet GetListS(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListS(PageSize, PageIndex, strWhere, out resCount);
		}

		public int UpdateGiftNumber(int giftID, int number)
		{
			return this.dal.UpdateGiftNumber(giftID, number);
		}
	}
}
