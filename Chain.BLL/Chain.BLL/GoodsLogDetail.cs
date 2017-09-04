using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class GoodsLogDetail
	{
		private readonly Chain.IDAL.GoodsLogDetail dal = new Chain.IDAL.GoodsLogDetail();

		public bool Exists(int GoodsLogDetailID)
		{
			return this.dal.Exists(GoodsLogDetailID);
		}

		public int Add(Chain.Model.GoodsLogDetail model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.GoodsLogDetail model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int GoodsLogDetailID)
		{
			return this.dal.Delete(GoodsLogDetailID);
		}

		public Chain.Model.GoodsLogDetail GetModel(int GoodsLogDetailID)
		{
			return this.dal.GetModel(GoodsLogDetailID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.GoodsLogDetail> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.GoodsLogDetail> DataTableToList(DataTable dt)
		{
			List<Chain.Model.GoodsLogDetail> modelList = new List<Chain.Model.GoodsLogDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.GoodsLogDetail model = new Chain.Model.GoodsLogDetail();
					if (dt.Rows[i]["GoodsLogDetailID"] != null && dt.Rows[i]["GoodsLogDetailID"].ToString() != "")
					{
						model.GoodsLogDetailID = int.Parse(dt.Rows[i]["GoodsLogDetailID"].ToString());
					}
					if (dt.Rows[i]["GoodsLogID"] != null && dt.Rows[i]["GoodsLogID"].ToString() != "")
					{
						model.GoodsLogID = int.Parse(dt.Rows[i]["GoodsLogID"].ToString());
					}
					if (dt.Rows[i]["GoodsID"] != null && dt.Rows[i]["GoodsID"].ToString() != "")
					{
						model.GoodsID = int.Parse(dt.Rows[i]["GoodsID"].ToString());
					}
					if (dt.Rows[i]["GoodsInPrice"] != null && dt.Rows[i]["GoodsInPrice"].ToString() != "")
					{
						model.GoodsInPrice = decimal.Parse(dt.Rows[i]["GoodsInPrice"].ToString());
					}
					if (dt.Rows[i]["GoodsOutPrice"] != null && dt.Rows[i]["GoodsOutPrice"].ToString() != "")
					{
						model.GoodsOutPrice = decimal.Parse(dt.Rows[i]["GoodsOutPrice"].ToString());
					}
					if (dt.Rows[i]["GoodsNumber"] != null && dt.Rows[i]["GoodsNumber"].ToString() != "")
					{
						model.GoodsNumber = decimal.Parse(dt.Rows[i]["GoodsNumber"].ToString());
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

		public bool DeleteDetail(int goodsLogID)
		{
			return this.dal.DeleteDetail(goodsLogID);
		}

		public bool DeleteList(string GoodsLogDetailIDlist)
		{
			return this.dal.DeleteList(GoodsLogDetailIDlist);
		}

		public DataSet GetInsufficientCount(string strWhere)
		{
			return this.dal.GetInsufficientCount(strWhere);
		}

		public bool ExeclDataInput(ArrayList sqlArray)
		{
			return this.dal.ExeclDataInput(sqlArray);
		}

		public DataSet getGoodsLogDetail(string strWhere)
		{
			return this.dal.getGoodsLogDetail(strWhere);
		}
	}
}
