using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MemCountDetail
	{
		private readonly Chain.IDAL.MemCountDetail dal = new Chain.IDAL.MemCountDetail();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int CountDetailID)
		{
			return this.dal.Exists(CountDetailID);
		}

		public bool Add(Chain.Model.MemCountDetail model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.MemCountDetail model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int CountDetailID)
		{
			return this.dal.Delete(CountDetailID);
		}

		public bool DeleteList(string CountDetailIDlist)
		{
			return this.dal.DeleteList(CountDetailIDlist);
		}

		public Chain.Model.MemCountDetail GetModel(int CountDetailID)
		{
			return this.dal.GetModel(CountDetailID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.MemCountDetail> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MemCountDetail> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MemCountDetail> modelList = new List<Chain.Model.MemCountDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MemCountDetail model = new Chain.Model.MemCountDetail();
					if (dt.Rows[i]["CountDetailID"] != null && dt.Rows[i]["CountDetailID"].ToString() != "")
					{
						model.CountDetailID = int.Parse(dt.Rows[i]["CountDetailID"].ToString());
					}
					if (dt.Rows[i]["CountDetailCountID"] != null && dt.Rows[i]["CountDetailCountID"].ToString() != "")
					{
						model.CountDetailCountID = int.Parse(dt.Rows[i]["CountDetailCountID"].ToString());
					}
					if (dt.Rows[i]["CountDetailGoodsID"] != null && dt.Rows[i]["CountDetailGoodsID"].ToString() != "")
					{
						model.CountDetailGoodsID = int.Parse(dt.Rows[i]["CountDetailGoodsID"].ToString());
					}
					if (dt.Rows[i]["CountDetailNumber"] != null && dt.Rows[i]["CountDetailNumber"].ToString() != "")
					{
						model.CountDetailNumber = int.Parse(dt.Rows[i]["CountDetailNumber"].ToString());
					}
					if (dt.Rows[i]["CountDetailDiscountMoney"] != null && dt.Rows[i]["CountDetailDiscountMoney"].ToString() != "")
					{
						model.CountDetailDiscountMoney = decimal.Parse(dt.Rows[i]["CountDetailDiscountMoney"].ToString());
					}
					if (dt.Rows[i]["CountDetailPoint"] != null && dt.Rows[i]["CountDetailPoint"].ToString() != "")
					{
						model.CountDetailPoint = int.Parse(dt.Rows[i]["CountDetailPoint"].ToString());
					}
					if (dt.Rows[i]["CountCreateTime"] != null && dt.Rows[i]["CountCreateTime"].ToString() != "")
					{
						model.CountCreateTime = DateTime.Parse(dt.Rows[i]["CountCreateTime"].ToString());
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

		public DataSet GetMemCountList(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetMemCountList(PageSize, PageIndex, out resCount, strWhere);
		}

		public int UpdateCountDetailNumber(int intNumber, int intCountDetailID)
		{
			return this.dal.UpdateCountDetailNumber(intNumber, intCountDetailID);
		}

		public DataSet GetQueryMemCountList(string strWhere)
		{
			return this.dal.GetQueryMemCountList(strWhere);
		}
	}
}
