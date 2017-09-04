using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class OrderLog
	{
		private readonly Chain.IDAL.OrderLog dal = new Chain.IDAL.OrderLog();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int OrderID)
		{
			return this.dal.Exists(OrderID);
		}

		public bool ExistsOrderAccount(string OrderAccount)
		{
			return this.dal.ExistsOrderAccount(OrderAccount);
		}

		public int Add(Chain.Model.OrderLog model, string OrderAccount)
		{
			int result;
			if (!this.ExistsOrderAccount(OrderAccount))
			{
				result = this.dal.Add(model);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public int Add(Chain.Model.OrderLog model)
		{
			return this.dal.Add(model);
		}

		public int Update(Chain.Model.OrderLog model)
		{
			return this.dal.Update(model);
		}

		public int UpdateType(int orderID, int type)
		{
			return this.dal.UpdateType(orderID, type);
		}

		public bool Delete(int OrderID)
		{
			return this.dal.Delete(OrderID);
		}

		public bool DeleteList(string OrderIDlist)
		{
			return this.dal.DeleteList(OrderIDlist);
		}

		public Chain.Model.OrderLog GetModel(int OrderID)
		{
			return this.dal.GetModel(OrderID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.OrderLog> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.OrderLog> DataTableToList(DataTable dt)
		{
			List<Chain.Model.OrderLog> modelList = new List<Chain.Model.OrderLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.OrderLog model = new Chain.Model.OrderLog();
					if (dt.Rows[i]["OrderID"] != null && dt.Rows[i]["OrderID"].ToString() != "")
					{
						model.OrderID = int.Parse(dt.Rows[i]["OrderID"].ToString());
					}
					if (dt.Rows[i]["OrderAccount"] != null && dt.Rows[i]["OrderAccount"].ToString() != "")
					{
						model.OrderAccount = dt.Rows[i]["OrderAccount"].ToString();
					}
					if (dt.Rows[i]["OrderType"] != null && dt.Rows[i]["OrderType"].ToString() != "")
					{
						model.OrderType = int.Parse(dt.Rows[i]["OrderType"].ToString());
					}
					if (dt.Rows[i]["OrderMemID"] != null && dt.Rows[i]["OrderMemID"].ToString() != "")
					{
						model.OrderMemID = int.Parse(dt.Rows[i]["OrderMemID"].ToString());
					}
					if (dt.Rows[i]["OrderTotalMoney"] != null && dt.Rows[i]["OrderTotalMoney"].ToString() != "")
					{
						model.OrderTotalMoney = decimal.Parse(dt.Rows[i]["OrderTotalMoney"].ToString());
					}
					if (dt.Rows[i]["OrderDiscountMoney"] != null && dt.Rows[i]["OrderDiscountMoney"].ToString() != "")
					{
						model.OrderDiscountMoney = decimal.Parse(dt.Rows[i]["OrderDiscountMoney"].ToString());
					}
					if (dt.Rows[i]["OrderPayCard"] != null && dt.Rows[i]["OrderPayCard"].ToString() != "")
					{
						model.OrderPayCard = decimal.Parse(dt.Rows[i]["OrderPayCard"].ToString());
					}
					if (dt.Rows[i]["OrderPayCash"] != null && dt.Rows[i]["OrderPayCash"].ToString() != "")
					{
						model.OrderPayCash = decimal.Parse(dt.Rows[i]["OrderPayCash"].ToString());
					}
					if (dt.Rows[i]["OrderPayCoupon"] != null && dt.Rows[i]["OrderPayCoupon"].ToString() != "")
					{
						model.OrderPayCoupon = decimal.Parse(dt.Rows[i]["OrderPayCoupon"].ToString());
					}
					if (dt.Rows[i]["OrderPoint"] != null && dt.Rows[i]["OrderPoint"].ToString() != "")
					{
						model.OrderPoint = int.Parse(dt.Rows[i]["OrderPoint"].ToString());
					}
					if (dt.Rows[i]["OrderRemark"] != null && dt.Rows[i]["OrderRemark"].ToString() != "")
					{
						model.OrderRemark = dt.Rows[i]["OrderRemark"].ToString();
					}
					if (dt.Rows[i]["OrderPayType"] != null && dt.Rows[i]["OrderPayType"].ToString() != "")
					{
						model.OrderPayType = int.Parse(dt.Rows[i]["OrderPayType"].ToString());
					}
					if (dt.Rows[i]["OrderShopID"] != null && dt.Rows[i]["OrderShopID"].ToString() != "")
					{
						model.OrderShopID = int.Parse(dt.Rows[i]["OrderShopID"].ToString());
					}
					if (dt.Rows[i]["OrderCreateTime"] != null && dt.Rows[i]["OrderCreateTime"].ToString() != "")
					{
						model.OrderCreateTime = DateTime.Parse(dt.Rows[i]["OrderCreateTime"].ToString());
					}
					if (dt.Rows[i]["OrderUserID"] != null && dt.Rows[i]["OrderUserID"].ToString() != "")
					{
						model.OrderUserID = int.Parse(dt.Rows[i]["OrderUserID"].ToString());
					}
					if (dt.Rows[i]["OrderIsCard"] != null && dt.Rows[i]["OrderIsCard"].ToString() != "")
					{
						if (dt.Rows[i]["OrderIsCard"].ToString() == "1" || dt.Rows[i]["OrderIsCard"].ToString().ToLower() == "true")
						{
							model.OrderIsCard = true;
						}
						else
						{
							model.OrderIsCard = false;
						}
					}
					if (dt.Rows[i]["OrderIsCash"] != null && dt.Rows[i]["OrderIsCash"].ToString() != "")
					{
						if (dt.Rows[i]["OrderIsCash"].ToString() == "1" || dt.Rows[i]["OrderIsCash"].ToString().ToLower() == "true")
						{
							model.OrderIsCash = true;
						}
						else
						{
							model.OrderIsCash = false;
						}
					}
					if (dt.Rows[i]["OrderIsBink"] != null && dt.Rows[i]["OrderIsBink"].ToString() != "")
					{
						if (dt.Rows[i]["OrderIsBink"].ToString() == "1" || dt.Rows[i]["OrderIsBink"].ToString().ToLower() == "true")
						{
							model.OrderIsBink = true;
						}
						else
						{
							model.OrderIsBink = false;
						}
					}
					if (dt.Rows[i]["OrderPayBink"] != null && dt.Rows[i]["OrderPayBink"].ToString() != "")
					{
						model.OrderPayBink = decimal.Parse(dt.Rows[i]["OrderPayBink"].ToString());
					}
					if (dt.Rows[i]["OldAccount"] != null && dt.Rows[i]["OldAccount"].ToString() != "")
					{
						model.OldAccount = dt.Rows[i]["OldAccount"].ToString();
					}
					if (dt.Rows[i]["OrderCardBalance"] != null && dt.Rows[i]["OrderCardBalance"].ToString() != "")
					{
						model.OrderCardBalance = decimal.Parse(dt.Rows[i]["OrderCardBalance"].ToString());
					}
					if (dt.Rows[i]["OrderCardPoint"] != null && dt.Rows[i]["OrderCardPoint"].ToString() != "")
					{
						model.OrderCardPoint = int.Parse(dt.Rows[i]["OrderCardPoint"].ToString());
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

		public decimal GetTotalMoney(string strWhere)
		{
			return this.dal.GetTotalMoney(strWhere);
		}

		public decimal GetAllMoney(string strWhere)
		{
			return this.dal.GetAllMoney(strWhere);
		}

		public decimal GetDiscountMoney(string strWhere)
		{
			return this.dal.GetDiscountMoney(strWhere);
		}

		public decimal GetDiscount(string strWhere)
		{
			return this.dal.GetDiscount(strWhere);
		}

		public decimal GetCoupon(string strWhere)
		{
			return this.dal.GetCoupon(strWhere);
		}

		public int GetPoint(string strWhere)
		{
			return this.dal.GetPoint(strWhere);
		}

		public DataSet GetOrderByTime(DateTime starttime, DateTime endtime, string strwhere)
		{
			return this.dal.GetOrderByTime(starttime, endtime, strwhere);
		}

		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return this.dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetGoodsExpense(DateTime starttime, DateTime endtime, string GoodsCode, string strwhere)
		{
			return this.dal.GetGoodsExpense(starttime, endtime, GoodsCode, strwhere);
		}

		public DataSet GetEmptyBillsList(int PageSize, int PageIndex, out int resCount, string strSql, params string[] strWhere)
		{
			return this.dal.GetEmptyBillsList(PageSize, PageIndex, out resCount, strSql, strWhere);
		}

		public decimal GetTotalCash(string strwhere)
		{
			return this.dal.GetTotalCash(strwhere);
		}

		public int UpdateOrderLog(Chain.Model.OrderLog model)
		{
			return this.dal.UpdateOrderLog(model);
		}

		public int UpdateLog(int orderID, decimal totalMoney, decimal discountMoney, int point, decimal payCard, decimal payCash, decimal payBank, string oldAccount)
		{
			return this.dal.UpdateLog(orderID, totalMoney, discountMoney, point, payCard, payCash, payBank, oldAccount);
		}

		public DataSet GetMemExpenseMoney(string strSql)
		{
			return this.dal.GetMemExpenseMoney(strSql);
		}
	}
}
