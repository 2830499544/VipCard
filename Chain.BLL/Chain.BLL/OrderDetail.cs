using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class OrderDetail
	{
		private readonly Chain.IDAL.OrderDetail dal = new Chain.IDAL.OrderDetail();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int OrderDetailID)
		{
			return this.dal.Exists(OrderDetailID);
		}

		public int Add(Chain.Model.OrderDetail model)
		{
			return this.dal.Add(model);
		}

		public int Update(Chain.Model.OrderDetail model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int OrderDetailID)
		{
			return this.dal.Delete(OrderDetailID);
		}

		public bool DeleteList(string OrderDetailIDlist)
		{
			return this.dal.DeleteList(OrderDetailIDlist);
		}

		public Chain.Model.OrderDetail GetModel(int OrderDetailID)
		{
			return this.dal.GetModel(OrderDetailID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.OrderDetail> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.OrderDetail> DataTableToList(DataTable dt)
		{
			List<Chain.Model.OrderDetail> modelList = new List<Chain.Model.OrderDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.OrderDetail model = new Chain.Model.OrderDetail();
					if (dt.Rows[i]["OrderDetailID"] != null && dt.Rows[i]["OrderDetailID"].ToString() != "")
					{
						model.OrderDetailID = int.Parse(dt.Rows[i]["OrderDetailID"].ToString());
					}
					if (dt.Rows[i]["OrderID"] != null && dt.Rows[i]["OrderID"].ToString() != "")
					{
						model.OrderID = int.Parse(dt.Rows[i]["OrderID"].ToString());
					}
					if (dt.Rows[i]["GoodsID"] != null && dt.Rows[i]["GoodsID"].ToString() != "")
					{
						model.GoodsID = int.Parse(dt.Rows[i]["GoodsID"].ToString());
					}
					if (dt.Rows[i]["OrderDetailPrice"] != null && dt.Rows[i]["OrderDetailPrice"].ToString() != "")
					{
						model.OrderDetailPrice = decimal.Parse(dt.Rows[i]["OrderDetailPrice"].ToString());
					}
					if (dt.Rows[i]["OrderDetailPoint"] != null && dt.Rows[i]["OrderDetailPoint"].ToString() != "")
					{
						model.OrderDetailPoint = int.Parse(dt.Rows[i]["OrderDetailPoint"].ToString());
					}
					if (dt.Rows[i]["OrderDetailDiscountPrice"] != null && dt.Rows[i]["OrderDetailDiscountPrice"].ToString() != "")
					{
						model.OrderDetailDiscountPrice = decimal.Parse(dt.Rows[i]["OrderDetailDiscountPrice"].ToString());
					}
					if (dt.Rows[i]["OrderDetailNumber"] != null && dt.Rows[i]["OrderDetailNumber"].ToString() != "")
					{
						model.OrderDetailNumber = decimal.Parse(dt.Rows[i]["OrderDetailNumber"].ToString());
					}
					if (dt.Rows[i]["OrderDetailType"] != null && dt.Rows[i]["OrderDetailType"].ToString() != "")
					{
						model.OrderDetailType = int.Parse(dt.Rows[i]["OrderDetailType"].ToString());
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

		public DataSet GetListSP(string strWhere)
		{
			return this.dal.GetListSP(strWhere);
		}

		public int DeleteDetail(int inOrderID)
		{
			return this.dal.DeleteDetail(inOrderID);
		}

		public DataSet GetDetail(int intOrderID)
		{
			return this.dal.GetDetail(intOrderID);
		}

		public int DeleteItem(int intOrder, int intGoodsID)
		{
			return this.dal.DeleteItem(intOrder, intGoodsID);
		}

		public DataSet GetOrderDetail(string strWhere)
		{
			return this.dal.GetOrderDetail(strWhere);
		}

		public DataSet GetGoodsExpenseDetail(string strWhere)
		{
			return this.dal.GetGoodsExpenseDetail(strWhere);
		}

		public DataSet GetExpenseDetail(int PageSiza, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetExpenseDetail(PageSiza, PageIndex, out resCount, strWhere);
		}

		public string ProjectName(int OrderMemID, int OrderID)
		{
			return this.dal.ProjectName(OrderMemID, OrderID);
		}
	}
}
