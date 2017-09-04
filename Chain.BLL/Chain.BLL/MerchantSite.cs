using Chain.DAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MerchantSite
	{
		private readonly Chain.DAL.MerchantSite dal = new Chain.DAL.MerchantSite();

		public bool Exists(int MerchantID)
		{
			return this.dal.Exists(MerchantID);
		}

		public int Add(Chain.Model.MerchantSite model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.MerchantSite model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int MerchantID)
		{
			return this.dal.Delete(MerchantID);
		}

		public bool DeleteList(string MerchantIDlist)
		{
			return this.dal.DeleteList(MerchantIDlist);
		}

		public Chain.Model.MerchantSite GetModel(int MerchantID)
		{
			return this.dal.GetModel(MerchantID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.MerchantSite> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MerchantSite> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MerchantSite> modelList = new List<Chain.Model.MerchantSite>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MerchantSite model = new Chain.Model.MerchantSite();
					if (dt.Rows[i]["MerchantID"] != null && dt.Rows[i]["MerchantID"].ToString() != "")
					{
						model.MerchantID = int.Parse(dt.Rows[i]["MerchantID"].ToString());
					}
					if (dt.Rows[i]["MerchantDesc"] != null && dt.Rows[i]["MerchantDesc"].ToString() != "")
					{
						model.MerchantDesc = dt.Rows[i]["MerchantDesc"].ToString();
					}
					if (dt.Rows[i]["MerchantPhoto"] != null && dt.Rows[i]["MerchantPhoto"].ToString() != "")
					{
						model.MerchantPhoto = dt.Rows[i]["MerchantPhoto"].ToString();
					}
					if (dt.Rows[i]["MerchantRemark"] != null && dt.Rows[i]["MerchantRemark"].ToString() != "")
					{
						model.MerchantRemark = dt.Rows[i]["MerchantRemark"].ToString();
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
