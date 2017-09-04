using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class GoodsAllotDetail
	{
		private readonly Chain.IDAL.GoodsAllotDetail dal = new Chain.IDAL.GoodsAllotDetail();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int AllotDetailID)
		{
			return this.dal.Exists(AllotDetailID);
		}

		public int Add(Chain.Model.GoodsAllotDetail model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.GoodsAllotDetail model)
		{
			return this.dal.Update(model);
		}

		public bool Updates(Chain.Model.GoodsAllotDetail model)
		{
			return this.dal.Updates(model);
		}

		public bool Delete(int AllotDetailID)
		{
			return this.dal.Delete(AllotDetailID);
		}

		public bool DeleteList(string AllotDetailIDlist)
		{
			return this.dal.DeleteList(AllotDetailIDlist);
		}

		public Chain.Model.GoodsAllotDetail GetModel(int AllotDetailID)
		{
			return this.dal.GetModel(AllotDetailID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetListSP(string strWhere)
		{
			return this.dal.GetListSP(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.GoodsAllotDetail> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.GoodsAllotDetail> DataTableToList(DataTable dt)
		{
			List<Chain.Model.GoodsAllotDetail> modelList = new List<Chain.Model.GoodsAllotDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.GoodsAllotDetail model = new Chain.Model.GoodsAllotDetail();
					if (dt.Rows[i]["AllotDetailID"] != null && dt.Rows[i]["AllotDetailID"].ToString() != "")
					{
						model.AllotDetailID = int.Parse(dt.Rows[i]["AllotDetailID"].ToString());
					}
					if (dt.Rows[i]["AllotDetailAllotID"] != null && dt.Rows[i]["AllotDetailAllotID"].ToString() != "")
					{
						model.AllotDetailAllotID = int.Parse(dt.Rows[i]["AllotDetailAllotID"].ToString());
					}
					if (dt.Rows[i]["AllotDetailGoodsID"] != null && dt.Rows[i]["AllotDetailGoodsID"].ToString() != "")
					{
						model.AllotDetailGoodsID = int.Parse(dt.Rows[i]["AllotDetailGoodsID"].ToString());
					}
					if (dt.Rows[i]["AllotDetailNumber"] != null && dt.Rows[i]["AllotDetailNumber"].ToString() != "")
					{
						model.AllotDetailNumber = decimal.Parse(dt.Rows[i]["AllotDetailNumber"].ToString());
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

		public DataSet GetAllotDetailByAllotID(int AllotID)
		{
			return this.dal.GetAllotDetailByAllotID(AllotID);
		}

		public DataSet AllotDetailByAllotID(int AllotID)
		{
			return this.dal.AllotDetailByAllotID(AllotID);
		}

		public bool DeleteAllorDetail(int AllotDetailAllotID)
		{
			return this.dal.DeleteAllorDetail(AllotDetailAllotID);
		}
	}
}
