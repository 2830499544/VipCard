using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MicroWebsiteOrderLogDetail
	{
		private readonly Chain.IDAL.MicroWebsiteOrderLogDetail dal = new Chain.IDAL.MicroWebsiteOrderLogDetail();

		public DataSet GetListSP(string strWhere)
		{
			return this.dal.GetListSP(strWhere);
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int MicroOrderLogDetailID)
		{
			return this.dal.Exists(MicroOrderLogDetailID);
		}

		public int Add(Chain.Model.MicroWebsiteOrderLogDetail model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.MicroWebsiteOrderLogDetail model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int MicroOrderLogDetailID)
		{
			return this.dal.Delete(MicroOrderLogDetailID);
		}

		public bool DeleteList(string MicroOrderLogDetailIDlist)
		{
			return this.dal.DeleteList(MicroOrderLogDetailIDlist);
		}

		public Chain.Model.MicroWebsiteOrderLogDetail GetModel(int MicroOrderLogDetailID)
		{
			return this.dal.GetModel(MicroOrderLogDetailID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.MicroWebsiteOrderLogDetail> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MicroWebsiteOrderLogDetail> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MicroWebsiteOrderLogDetail> modelList = new List<Chain.Model.MicroWebsiteOrderLogDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MicroWebsiteOrderLogDetail model = new Chain.Model.MicroWebsiteOrderLogDetail();
					if (dt.Rows[i]["MicroOrderLogDetailID"] != null && dt.Rows[i]["MicroOrderLogDetailID"].ToString() != "")
					{
						model.MicroOrderLogDetailID = int.Parse(dt.Rows[i]["MicroOrderLogDetailID"].ToString());
					}
					if (dt.Rows[i]["MicroOrderID"] != null && dt.Rows[i]["MicroOrderID"].ToString() != "")
					{
						model.MicroOrderID = int.Parse(dt.Rows[i]["MicroOrderID"].ToString());
					}
					if (dt.Rows[i]["MicroGoodsID"] != null && dt.Rows[i]["MicroGoodsID"].ToString() != "")
					{
						model.MicroGoodsID = int.Parse(dt.Rows[i]["MicroGoodsID"].ToString());
					}
					if (dt.Rows[i]["MicroOrderDetailPrice"] != null && dt.Rows[i]["MicroOrderDetailPrice"].ToString() != "")
					{
						model.MicroOrderDetailPrice = decimal.Parse(dt.Rows[i]["MicroOrderDetailPrice"].ToString());
					}
					if (dt.Rows[i]["MicroOrderDetailPoint"] != null && dt.Rows[i]["MicroOrderDetailPoint"].ToString() != "")
					{
						model.MicroOrderDetailPoint = int.Parse(dt.Rows[i]["MicroOrderDetailPoint"].ToString());
					}
					if (dt.Rows[i]["MicroOrderDetailDiscountPrice"] != null && dt.Rows[i]["MicroOrderDetailDiscountPrice"].ToString() != "")
					{
						model.MicroOrderDetailDiscountPrice = decimal.Parse(dt.Rows[i]["MicroOrderDetailDiscountPrice"].ToString());
					}
					if (dt.Rows[i]["MicroOrderDetailNumber"] != null && dt.Rows[i]["MicroOrderDetailNumber"].ToString() != "")
					{
						model.MicroOrderDetailNumber = int.Parse(dt.Rows[i]["MicroOrderDetailNumber"].ToString());
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
