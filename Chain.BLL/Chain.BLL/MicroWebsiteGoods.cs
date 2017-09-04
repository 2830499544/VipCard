using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class MicroWebsiteGoods
	{
		private readonly Chain.IDAL.MicroWebsiteGoods dal = new Chain.IDAL.MicroWebsiteGoods();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int MicroGoodsID)
		{
			return this.dal.Exists(MicroGoodsID);
		}

		public bool Exists(string strGoodsCode, int ShopID)
		{
			return this.dal.Exists(strGoodsCode, ShopID);
		}

		public bool Exists(int goodsID, string goodsCode, int ShopID)
		{
			return this.dal.Exists(goodsID, goodsCode, ShopID);
		}

		public int Add(Chain.Model.MicroWebsiteGoods model)
		{
			int result;
			if (this.Exists(model.MicroGoodsCode, model.MicroGoodsShopID))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Add(model);
			}
			return result;
		}

		public int Update(Chain.Model.MicroWebsiteGoods model)
		{
			int result;
			if (this.Exists(model.MicroGoodsID, model.MicroGoodsCode, model.MicroGoodsShopID))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Update(model);
			}
			return result;
		}

		public bool Delete(int MicroGoodsID)
		{
			return this.dal.Delete(MicroGoodsID);
		}

		public bool DeleteList(string MicroGoodsIDlist)
		{
			return this.dal.DeleteList(MicroGoodsIDlist);
		}

		public Chain.Model.MicroWebsiteGoods GetModel(int MicroGoodsID)
		{
			return this.dal.GetModel(MicroGoodsID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.MicroWebsiteGoods> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.MicroWebsiteGoods> DataTableToList(DataTable dt)
		{
			List<Chain.Model.MicroWebsiteGoods> modelList = new List<Chain.Model.MicroWebsiteGoods>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.MicroWebsiteGoods model = new Chain.Model.MicroWebsiteGoods();
					if (dt.Rows[i]["MicroGoodsID"] != null && dt.Rows[i]["MicroGoodsID"].ToString() != "")
					{
						model.MicroGoodsID = int.Parse(dt.Rows[i]["MicroGoodsID"].ToString());
					}
					if (dt.Rows[i]["MicroGoodsCode"] != null && dt.Rows[i]["MicroGoodsCode"].ToString() != "")
					{
						model.MicroGoodsCode = dt.Rows[i]["MicroGoodsCode"].ToString();
					}
					if (dt.Rows[i]["MicroGoodsClassID"] != null && dt.Rows[i]["MicroGoodsClassID"].ToString() != "")
					{
						model.MicroGoodsClassID = int.Parse(dt.Rows[i]["MicroGoodsClassID"].ToString());
					}
					if (dt.Rows[i]["MicroGoodsName"] != null && dt.Rows[i]["MicroGoodsName"].ToString() != "")
					{
						model.MicroGoodsName = dt.Rows[i]["MicroGoodsName"].ToString();
					}
					if (dt.Rows[i]["MicroSalePrice"] != null && dt.Rows[i]["MicroSalePrice"].ToString() != "")
					{
						model.MicroSalePrice = decimal.Parse(dt.Rows[i]["MicroSalePrice"].ToString());
					}
					if (dt.Rows[i]["MicroPrice"] != null && dt.Rows[i]["MicroPrice"].ToString() != "")
					{
						model.MicroPrice = decimal.Parse(dt.Rows[i]["MicroPrice"].ToString());
					}
					if (dt.Rows[i]["MicroPoint"] != null && dt.Rows[i]["MicroPoint"].ToString() != "")
					{
						model.MicroPoint = int.Parse(dt.Rows[i]["MicroPoint"].ToString());
					}
					if (dt.Rows[i]["MicroGoodsBidPrice"] != null && dt.Rows[i]["MicroGoodsBidPrice"].ToString() != "")
					{
						model.MicroGoodsBidPrice = decimal.Parse(dt.Rows[i]["MicroGoodsBidPrice"].ToString());
					}
					if (dt.Rows[i]["MicroGoodsRemark"] != null && dt.Rows[i]["MicroGoodsRemark"].ToString() != "")
					{
						model.MicroGoodsRemark = dt.Rows[i]["MicroGoodsRemark"].ToString();
					}
					if (dt.Rows[i]["MicroGoodsPicture"] != null && dt.Rows[i]["MicroGoodsPicture"].ToString() != "")
					{
						model.MicroGoodsPicture = dt.Rows[i]["MicroGoodsPicture"].ToString();
					}
					if (dt.Rows[i]["MicroGoodsCreateTime"] != null && dt.Rows[i]["MicroGoodsCreateTime"].ToString() != "")
					{
						model.MicroGoodsCreateTime = DateTime.Parse(dt.Rows[i]["MicroGoodsCreateTime"].ToString());
					}
					if (dt.Rows[i]["MicroGoodsShopID"] != null && dt.Rows[i]["MicroGoodsShopID"].ToString() != "")
					{
						model.MicroGoodsShopID = int.Parse(dt.Rows[i]["MicroGoodsShopID"].ToString());
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
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}
	}
}
