using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class Goods
	{
		private readonly Chain.IDAL.Goods dal = new Chain.IDAL.Goods();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int GoodsID)
		{
			return this.dal.Exists(GoodsID);
		}

		public bool Delete(int GoodsID)
		{
			return this.dal.Delete(GoodsID);
		}

		public bool DeleteList(string GoodsIDlist)
		{
			return this.dal.DeleteList(GoodsIDlist);
		}

		public Chain.Model.Goods GetModel(int GoodsID)
		{
			return this.dal.GetModel(GoodsID);
		}

		public Chain.Model.Goods GetModel(string GoodsCode)
		{
			return this.dal.GetModel(GoodsCode);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetGoodsList(string strWhere)
		{
			return this.dal.GetGoodsList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.Goods> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.Goods> DataTableToList(DataTable dt)
		{
			List<Chain.Model.Goods> modelList = new List<Chain.Model.Goods>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.Goods model = new Chain.Model.Goods();
					if (dt.Rows[i]["GoodsID"] != null && dt.Rows[i]["GoodsID"].ToString() != "")
					{
						model.GoodsID = int.Parse(dt.Rows[i]["GoodsID"].ToString());
					}
					if (dt.Rows[i]["GoodsCode"] != null && dt.Rows[i]["GoodsCode"].ToString() != "")
					{
						model.GoodsCode = dt.Rows[i]["GoodsCode"].ToString();
					}
					if (dt.Rows[i]["GoodsClassID"] != null && dt.Rows[i]["GoodsClassID"].ToString() != "")
					{
						model.GoodsClassID = int.Parse(dt.Rows[i]["GoodsClassID"].ToString());
					}
					if (dt.Rows[i]["Name"] != null && dt.Rows[i]["Name"].ToString() != "")
					{
						model.Name = dt.Rows[i]["Name"].ToString();
					}
					if (dt.Rows[i]["NameCode"] != null && dt.Rows[i]["NameCode"].ToString() != "")
					{
						model.NameCode = dt.Rows[i]["NameCode"].ToString();
					}
					if (dt.Rows[i]["Unit"] != null && dt.Rows[i]["Unit"].ToString() != "")
					{
						model.Unit = dt.Rows[i]["Unit"].ToString();
					}
					if (dt.Rows[i]["GoodsNumber"] != null && dt.Rows[i]["GoodsNumber"].ToString() != "")
					{
						model.GoodsNumber = int.Parse(dt.Rows[i]["GoodsNumber"].ToString());
					}
					if (dt.Rows[i]["SalePercet"] != null && dt.Rows[i]["SalePercet"].ToString() != "")
					{
						model.SalePercet = decimal.Parse(dt.Rows[i]["SalePercet"].ToString());
					}
					if (dt.Rows[i]["GoodsSaleNumber"] != null && dt.Rows[i]["GoodsSaleNumber"].ToString() != "")
					{
						model.GoodsSaleNumber = int.Parse(dt.Rows[i]["GoodsSaleNumber"].ToString());
					}
					if (dt.Rows[i]["Price"] != null && dt.Rows[i]["Price"].ToString() != "")
					{
						model.Price = decimal.Parse(dt.Rows[i]["Price"].ToString());
					}
					if (dt.Rows[i]["CommissionType"] != null && dt.Rows[i]["CommissionType"].ToString() != "")
					{
						model.CommissionType = int.Parse(dt.Rows[i]["CommissionType"].ToString());
					}
					if (dt.Rows[i]["CommissionNumber"] != null && dt.Rows[i]["CommissionNumber"].ToString() != "")
					{
						model.CommissionNumber = decimal.Parse(dt.Rows[i]["CommissionNumber"].ToString());
					}
					if (dt.Rows[i]["Point"] != null && dt.Rows[i]["Point"].ToString() != "")
					{
						model.Point = int.Parse(dt.Rows[i]["Point"].ToString());
					}
					if (dt.Rows[i]["MinPercent"] != null && dt.Rows[i]["MinPercent"].ToString() != "")
					{
						model.MinPercent = decimal.Parse(dt.Rows[i]["MinPercent"].ToString());
					}
					if (dt.Rows[i]["GoodsType"] != null && dt.Rows[i]["GoodsType"].ToString() != "")
					{
						model.GoodsType = int.Parse(dt.Rows[i]["GoodsType"].ToString());
					}
					if (dt.Rows[i]["GoodsBidPrice"] != null && dt.Rows[i]["GoodsBidPrice"].ToString() != "")
					{
						model.GoodsBidPrice = decimal.Parse(dt.Rows[i]["GoodsBidPrice"].ToString());
					}
					if (dt.Rows[i]["GoodsRemark"] != null && dt.Rows[i]["GoodsRemark"].ToString() != "")
					{
						model.GoodsRemark = dt.Rows[i]["GoodsRemark"].ToString();
					}
					if (dt.Rows[i]["GoodsPicture"] != null && dt.Rows[i]["GoodsPicture"].ToString() != "")
					{
						model.GoodsPicture = dt.Rows[i]["GoodsPicture"].ToString();
					}
					if (dt.Rows[i]["GoodsCreateTime"] != null && dt.Rows[i]["GoodsCreateTime"].ToString() != "")
					{
						model.GoodsCreateTime = DateTime.Parse(dt.Rows[i]["GoodsCreateTime"].ToString());
					}
					if (dt.Rows[i]["CreateShopID"] != null && dt.Rows[i]["CreateShopID"].ToString() != "")
					{
						model.CreateShopID = int.Parse(dt.Rows[i]["CreateShopID"].ToString());
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

		public DataSet GetGoodsInfo(string goodscode, int levelid)
		{
			return this.dal.GetGoodsInfo(goodscode, levelid);
		}

		public bool Exists(string strGoodsCode)
		{
			return this.dal.Exists(strGoodsCode);
		}

		public bool Exists(string strGoodsCode, int ShopID)
		{
			return this.dal.Exists(strGoodsCode, ShopID);
		}

		public bool Exists(int goodsID, string goodsCode)
		{
			return this.dal.Exists(goodsID, goodsCode);
		}

		public bool Exists(int goodsID, string goodsCode, int ShopID)
		{
			return this.dal.Exists(goodsID, goodsCode, ShopID);
		}

		public int Add(Chain.Model.Goods model)
		{
			int result;
			if (this.Exists(model.GoodsCode, model.CreateShopID))
			{
				result = -1;
			}
			else
			{
				result = this.dal.Add(model);
			}
			return result;
		}

		public int AddCustomField(string strGoodsCode, Hashtable customhash)
		{
			return this.dal.AddCustomField(strGoodsCode, customhash);
		}

		public int Update(Chain.Model.Goods model)
		{
			return this.dal.Update(model) ? 1 : -3;
		}

		public DataSet GetItemAll(int intGoodsID)
		{
			return this.dal.GetItemAll(intGoodsID);
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetListSP(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetGoodsExpense(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetGoodsExpense(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetGoodLists(int PageSize, int PageIndex, out int resCount, string strTime, params string[] strWhere)
		{
			return this.dal.GetGoodLists(PageSize, PageIndex, out resCount, strTime, strWhere);
		}

		public DataSet GetList(int PageSiza, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetList(PageSiza, PageIndex, out resCount, strWhere);
		}

		public DataSet GetGoodsList(int PageSiza, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetGoodsList(PageSiza, PageIndex, out resCount, strWhere);
		}

		public DataSet GetGoodsListByMember(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetGoodsListByMember(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetGoodsListByNoMember(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			return this.dal.GetGoodsListByNoMember(PageSize, PageIndex, out resCount, strWhere);
		}

		public DataSet GetGoodsStockList(int ShopId, int MemLevelId, string GoodsIdList, int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			DataSet ds = this.dal.GetGoodsStockList(ShopId, MemLevelId, GoodsIdList, PageSize, PageIndex, out resCount, strWhere);
			DataTable dt = ds.Tables[0];
			dt.Columns.Add("GoodsDiscount");
			dt.Columns.Add("PointDiscount");
			dt.Columns.Add("DiscountPrice");
			dt.Columns.Add("DiscountPoint");
			if (dt.Rows.Count > 0)
			{
				foreach (DataRow item in dt.Rows)
				{
					decimal goodsDiscount = 1m;
					decimal minPercent = Convert.ToDecimal(item["minPercent"]);
					decimal salePercet = Convert.ToDecimal(item["salePercet"]);
					if (MemLevelId == -1 && salePercet > 0m)
					{
						goodsDiscount = salePercet;
					}
					if (MemLevelId != -1)
					{
						goodsDiscount = Convert.ToDecimal(item["ClassDiscountPercent"]);
						if (salePercet > 0m)
						{
							goodsDiscount = ((goodsDiscount > salePercet) ? salePercet : goodsDiscount);
						}
						if (minPercent > 0m)
						{
							goodsDiscount = ((goodsDiscount > minPercent) ? goodsDiscount : minPercent);
						}
					}
					item["GoodsDiscount"] = goodsDiscount;
					item["DiscountPrice"] = decimal.Round(Convert.ToDecimal(item["Price"]) * goodsDiscount, 2);
					decimal pointDiscount = 0m;
					decimal point = Convert.ToDecimal(item["point"]);
					if (MemLevelId != -1)
					{
						if (point == -1m)
						{
							pointDiscount = 0m;
						}
						if (point == 0m)
						{
							pointDiscount = Convert.ToDecimal(item["ClassPointPercent"]);
						}
					}
					item["PointDiscount"] = pointDiscount;
					if (point > 0m)
					{
						item["DiscountPoint"] = point;
					}
					else if (pointDiscount == 0m)
					{
						item["DiscountPoint"] = 0;
					}
					else
					{
						item["DiscountPoint"] = Math.Floor(Convert.ToDecimal(item["DiscountPrice"]) / pointDiscount);
					}
					item["Price"] = decimal.Round(Convert.ToDecimal(item["Price"]), 2);
					if (MemLevelId == -1)
					{
						item["ClassDiscountPercent"] = 1;
						item["ClassPointPercent"] = 0;
					}
				}
			}
			return ds;
		}

		public DataSet GetStockRemind(string strWhere, int count)
		{
			return this.dal.GetStockRemind(strWhere, count);
		}

		public DataSet GetStockRemind(string strWhere)
		{
			return this.dal.GetStockRemind(strWhere);
		}

		public DataSet GetGoodsListByShopID(int PageSize, int PageIndex, int ShopID, out int resCount, params string[] strWhere)
		{
			return this.dal.GetGoodsListByShopID(PageSize, PageIndex, ShopID, out resCount, strWhere);
		}

		public bool ExeclDataInput(ArrayList sqlArray)
		{
			return this.dal.ExeclDataInput(sqlArray);
		}

		public int GetGoodsID(string GoodsAccount)
		{
			return this.dal.GetGoodsID(GoodsAccount);
		}
	}
}
