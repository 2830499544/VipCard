using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class GoodsClassDiscount
	{
		private readonly Chain.IDAL.GoodsClassDiscount dal = new Chain.IDAL.GoodsClassDiscount();

		public bool AddList(ArrayList sqlArray)
		{
			return this.dal.AddList(sqlArray);
		}

		public void InitGoodsLevelDiscountByGoodsClassID(int goodsClassID)
		{
			this.dal.InitGoodsLevelDiscountByGoodsClassID(goodsClassID);
		}

		public void DelGoodsClassDiscountByGoodsClassID(int goodsClassID)
		{
			this.dal.DelGoodsClassDiscountByGoodsClassID(goodsClassID);
		}

		public void InitGoodsLevelDiscountByMemLevelID(Chain.Model.MemLevel mdMemLevel)
		{
			this.dal.InitGoodsLevelDiscountByMemLevelID(mdMemLevel);
		}

		public void DelGoodsClassDiscountByMemLevelID(int memLevelID)
		{
			this.dal.DelGoodsClassDiscountByMemLevelID(memLevelID);
		}

		public DataSet GetListByClassID(int classID)
		{
			return this.dal.GetListByClassID(classID);
		}

		public DataSet GetListByClassDiscountID(int classDiscountID)
		{
			return this.dal.GetListByClassDiscountID(classDiscountID);
		}

		public DataSet GetList(int MemLevelID, int ShopID)
		{
			return this.dal.GetList(MemLevelID, ShopID);
		}

		public int DeleteDiscount(int ClassID, int ShopID)
		{
			return this.dal.DeleteDiscount(ClassID, ShopID);
		}

		public int DeleteDiscountByShopID(int ShopID)
		{
			return this.dal.DeleteDiscountByShopID(ShopID);
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ClassDiscountID)
		{
			return this.dal.Exists(ClassDiscountID);
		}

		public int Add(Chain.Model.GoodsClassDiscount model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.GoodsClassDiscount model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ClassDiscountID)
		{
			return this.dal.Delete(ClassDiscountID);
		}

		public bool DeleteList(string ClassDiscountIDlist)
		{
			return this.dal.DeleteList(ClassDiscountIDlist);
		}

		public Chain.Model.GoodsClassDiscount GetModel(int ClassDiscountID)
		{
			return this.dal.GetModel(ClassDiscountID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.GoodsClassDiscount> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.GoodsClassDiscount> DataTableToList(DataTable dt)
		{
			List<Chain.Model.GoodsClassDiscount> modelList = new List<Chain.Model.GoodsClassDiscount>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.GoodsClassDiscount model = new Chain.Model.GoodsClassDiscount();
					if (dt.Rows[i]["ClassDiscountID"] != null && dt.Rows[i]["ClassDiscountID"].ToString() != "")
					{
						model.ClassDiscountID = int.Parse(dt.Rows[i]["ClassDiscountID"].ToString());
					}
					if (dt.Rows[i]["GoodsClassID"] != null && dt.Rows[i]["GoodsClassID"].ToString() != "")
					{
						model.GoodsClassID = int.Parse(dt.Rows[i]["GoodsClassID"].ToString());
					}
					if (dt.Rows[i]["MemLevelID"] != null && dt.Rows[i]["MemLevelID"].ToString() != "")
					{
						model.MemLevelID = int.Parse(dt.Rows[i]["MemLevelID"].ToString());
					}
					if (dt.Rows[i]["DiscountShopID"] != null && dt.Rows[i]["DiscountShopID"].ToString() != "")
					{
						model.DiscountShopID = int.Parse(dt.Rows[i]["DiscountShopID"].ToString());
					}
					if (dt.Rows[i]["ClassDiscountPercent"] != null && dt.Rows[i]["ClassDiscountPercent"].ToString() != "")
					{
						model.ClassDiscountPercent = decimal.Parse(dt.Rows[i]["ClassDiscountPercent"].ToString());
					}
					if (dt.Rows[i]["ClassPointPercent"] != null && dt.Rows[i]["ClassPointPercent"].ToString() != "")
					{
						model.ClassPointPercent = decimal.Parse(dt.Rows[i]["ClassPointPercent"].ToString());
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
