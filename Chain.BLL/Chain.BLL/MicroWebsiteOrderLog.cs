using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MicroWebsiteOrderLog
	{
		private readonly Chain.IDAL.MicroWebsiteOrderLog dal = new Chain.IDAL.MicroWebsiteOrderLog();

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetListSP1(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP1(PageSize, PageIndex, out resCount, strWhere);
		}

		public bool Exists(int MicroOrderID)
		{
			return this.dal.Exists(MicroOrderID);
		}

		public int Add(Chain.Model.MicroWebsiteOrderLog model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.MicroWebsiteOrderLog model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int MicroOrderID)
		{
			return this.dal.Delete(MicroOrderID);
		}

		public bool DeleteList(string MicroOrderIDlist)
		{
			return this.dal.DeleteList(MicroOrderIDlist);
		}

		public Chain.Model.MicroWebsiteOrderLog GetModel(int MicroOrderID)
		{
			return this.dal.GetModel(MicroOrderID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.MicroWebsiteOrderLog> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MicroWebsiteOrderLog> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MicroWebsiteOrderLog> modelList = new List<Chain.Model.MicroWebsiteOrderLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MicroWebsiteOrderLog model = new Chain.Model.MicroWebsiteOrderLog();
					if (dt.Rows[i]["MicroOrderID"] != null && dt.Rows[i]["MicroOrderID"].ToString() != "")
					{
						model.MicroOrderID = int.Parse(dt.Rows[i]["MicroOrderID"].ToString());
					}
					if (dt.Rows[i]["MicroOrderAccount"] != null && dt.Rows[i]["MicroOrderAccount"].ToString() != "")
					{
						model.MicroOrderAccount = dt.Rows[i]["MicroOrderAccount"].ToString();
					}
					if (dt.Rows[i]["MicroOrderType"] != null && dt.Rows[i]["MicroOrderType"].ToString() != "")
					{
						model.MicroOrderType = int.Parse(dt.Rows[i]["MicroOrderType"].ToString());
					}
					if (dt.Rows[i]["MicroOrderMemID"] != null && dt.Rows[i]["MicroOrderMemID"].ToString() != "")
					{
						model.MicroOrderMemID = int.Parse(dt.Rows[i]["MicroOrderMemID"].ToString());
					}
					if (dt.Rows[i]["MicroOrderTotalMoney"] != null && dt.Rows[i]["MicroOrderTotalMoney"].ToString() != "")
					{
						model.MicroOrderTotalMoney = decimal.Parse(dt.Rows[i]["MicroOrderTotalMoney"].ToString());
					}
					if (dt.Rows[i]["MicroOrderDiscountMoney"] != null && dt.Rows[i]["MicroOrderDiscountMoney"].ToString() != "")
					{
						model.MicroOrderDiscountMoney = decimal.Parse(dt.Rows[i]["MicroOrderDiscountMoney"].ToString());
					}
					if (dt.Rows[i]["MicroOrderIsCard"] != null && dt.Rows[i]["MicroOrderIsCard"].ToString() != "")
					{
						if (dt.Rows[i]["MicroOrderIsCard"].ToString() == "1" || dt.Rows[i]["MicroOrderIsCard"].ToString().ToLower() == "true")
						{
							model.MicroOrderIsCard = true;
						}
						else
						{
							model.MicroOrderIsCard = false;
						}
					}
					if (dt.Rows[i]["MicroOrderPayCard"] != null && dt.Rows[i]["MicroOrderPayCard"].ToString() != "")
					{
						model.MicroOrderPayCard = decimal.Parse(dt.Rows[i]["MicroOrderPayCard"].ToString());
					}
					if (dt.Rows[i]["MicroOrderIsCash"] != null && dt.Rows[i]["MicroOrderIsCash"].ToString() != "")
					{
						if (dt.Rows[i]["MicroOrderIsCash"].ToString() == "1" || dt.Rows[i]["MicroOrderIsCash"].ToString().ToLower() == "true")
						{
							model.MicroOrderIsCash = true;
						}
						else
						{
							model.MicroOrderIsCash = false;
						}
					}
					if (dt.Rows[i]["MicroOrderPayCash"] != null && dt.Rows[i]["MicroOrderPayCash"].ToString() != "")
					{
						model.MicroOrderPayCash = decimal.Parse(dt.Rows[i]["MicroOrderPayCash"].ToString());
					}
					if (dt.Rows[i]["MicroOrderIsBink"] != null && dt.Rows[i]["MicroOrderIsBink"].ToString() != "")
					{
						if (dt.Rows[i]["MicroOrderIsBink"].ToString() == "1" || dt.Rows[i]["MicroOrderIsBink"].ToString().ToLower() == "true")
						{
							model.MicroOrderIsBink = true;
						}
						else
						{
							model.MicroOrderIsBink = false;
						}
					}
					if (dt.Rows[i]["MicroOrderPayBink"] != null && dt.Rows[i]["MicroOrderPayBink"].ToString() != "")
					{
						model.MicroOrderPayBink = decimal.Parse(dt.Rows[i]["MicroOrderPayBink"].ToString());
					}
					if (dt.Rows[i]["MicroOrderPayCoupon"] != null && dt.Rows[i]["MicroOrderPayCoupon"].ToString() != "")
					{
						model.MicroOrderPayCoupon = decimal.Parse(dt.Rows[i]["MicroOrderPayCoupon"].ToString());
					}
					if (dt.Rows[i]["MicroOrderPoint"] != null && dt.Rows[i]["MicroOrderPoint"].ToString() != "")
					{
						model.MicroOrderPoint = int.Parse(dt.Rows[i]["MicroOrderPoint"].ToString());
					}
					if (dt.Rows[i]["MicroOrderRemark"] != null && dt.Rows[i]["MicroOrderRemark"].ToString() != "")
					{
						model.MicroOrderRemark = dt.Rows[i]["MicroOrderRemark"].ToString();
					}
					if (dt.Rows[i]["MicroOrderShopID"] != null && dt.Rows[i]["MicroOrderShopID"].ToString() != "")
					{
						model.MicroOrderShopID = int.Parse(dt.Rows[i]["MicroOrderShopID"].ToString());
					}
					if (dt.Rows[i]["MicroOrderUserID"] != null && dt.Rows[i]["MicroOrderUserID"].ToString() != "")
					{
						model.MicroOrderUserID = int.Parse(dt.Rows[i]["MicroOrderUserID"].ToString());
					}
					if (dt.Rows[i]["MicroOrderCreateTime"] != null && dt.Rows[i]["MicroOrderCreateTime"].ToString() != "")
					{
						model.MicroOrderCreateTime = DateTime.Parse(dt.Rows[i]["MicroOrderCreateTime"].ToString());
					}
					if (dt.Rows[i]["MicroOldAccount"] != null && dt.Rows[i]["MicroOldAccount"].ToString() != "")
					{
						model.MicroOldAccount = dt.Rows[i]["MicroOldAccount"].ToString();
					}
					if (dt.Rows[i]["MicroOrderCardBalance"] != null && dt.Rows[i]["MicroOrderCardBalance"].ToString() != "")
					{
						model.MicroOrderCardBalance = decimal.Parse(dt.Rows[i]["MicroOrderCardBalance"].ToString());
					}
					if (dt.Rows[i]["MicroOrderCardPoint"] != null && dt.Rows[i]["MicroOrderCardPoint"].ToString() != "")
					{
						model.MicroOrderCardPoint = int.Parse(dt.Rows[i]["MicroOrderCardPoint"].ToString());
					}
					if (dt.Rows[i]["MicroOrderName"] != null && dt.Rows[i]["MicroOrderName"].ToString() != "")
					{
						model.MicroOrderName = dt.Rows[i]["MicroOrderName"].ToString();
					}
					if (dt.Rows[i]["MicroOrderMobile"] != null && dt.Rows[i]["MicroOrderMobile"].ToString() != "")
					{
						model.MicroOrderMobile = dt.Rows[i]["MicroOrderMobile"].ToString();
					}
					if (dt.Rows[i]["MicroOrderAdress"] != null && dt.Rows[i]["MicroOrderAdress"].ToString() != "")
					{
						model.MicroOrderAdress = dt.Rows[i]["MicroOrderAdress"].ToString();
					}
					if (dt.Rows[i]["MicroOrderStatus"] != null && dt.Rows[i]["MicroOrderStatus"].ToString() != "")
					{
						model.MicroOrderStatus = int.Parse(dt.Rows[i]["MicroOrderStatus"].ToString());
					}
					if (dt.Rows[i]["MicroOrderPassCreateTime"] != null && dt.Rows[i]["MicroOrderPassCreateTime"].ToString() != "")
					{
						model.MicroOrderPassCreateTime = DateTime.Parse(dt.Rows[i]["MicroOrderPassCreateTime"].ToString());
					}
					if (dt.Rows[i]["MicroOrderMark"] != null && dt.Rows[i]["MicroOrderMark"].ToString() != "")
					{
						model.MicroOrderMark = dt.Rows[i]["MicroOrderMark"].ToString();
					}
					if (dt.Rows[i]["MicroOrderPayCreateTime"] != null && dt.Rows[i]["MicroOrderPayCreateTime"].ToString() != "")
					{
						model.MicroOrderPayCreateTime = DateTime.Parse(dt.Rows[i]["MicroOrderPayCreateTime"].ToString());
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
