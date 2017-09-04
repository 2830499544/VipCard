using Chain.DAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysShopMemLevel
	{
		private readonly Chain.DAL.SysShopMemLevel dal = new Chain.DAL.SysShopMemLevel();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ShopMemLevelID)
		{
			return this.dal.Exists(ShopMemLevelID);
		}

		public int Add(Chain.Model.SysShopMemLevel model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SysShopMemLevel model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ShopMemLevelID)
		{
			return this.dal.Delete(ShopMemLevelID);
		}

		public bool DeleteList(string ShopMemLevelIDlist)
		{
			return this.dal.DeleteList(ShopMemLevelIDlist);
		}

		public Chain.Model.SysShopMemLevel GetModel(int ShopMemLevelID)
		{
			return this.dal.GetModel(ShopMemLevelID);
		}

		public Chain.Model.SysShopMemLevel GetModel(int MemLevelID, int shopID)
		{
			return this.dal.GetModel(MemLevelID, shopID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysShopMemLevel> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysShopMemLevel> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysShopMemLevel> modelList = new List<Chain.Model.SysShopMemLevel>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysShopMemLevel model = new Chain.Model.SysShopMemLevel();
					if (dt.Rows[i]["ShopMemLevelID"] != null && dt.Rows[i]["ShopMemLevelID"].ToString() != "")
					{
						model.ShopMemLevelID = int.Parse(dt.Rows[i]["ShopMemLevelID"].ToString());
					}
					if (dt.Rows[i]["ShopID"] != null && dt.Rows[i]["ShopID"].ToString() != "")
					{
						model.ShopID = int.Parse(dt.Rows[i]["ShopID"].ToString());
					}
					if (dt.Rows[i]["MemLevelID"] != null && dt.Rows[i]["MemLevelID"].ToString() != "")
					{
						model.MemLevelID = int.Parse(dt.Rows[i]["MemLevelID"].ToString());
					}
					if (dt.Rows[i]["ClassDiscountPercent"] != null && dt.Rows[i]["ClassDiscountPercent"].ToString() != "")
					{
						model.ClassDiscountPercent = decimal.Parse(dt.Rows[i]["ClassDiscountPercent"].ToString());
					}
					if (dt.Rows[i]["ClassPointPercent"] != null && dt.Rows[i]["ClassPointPercent"].ToString() != "")
					{
						model.ClassPointPercent = decimal.Parse(dt.Rows[i]["ClassPointPercent"].ToString());
					}
					if (dt.Rows[i]["ClassRechargePointRate"] != null && dt.Rows[i]["ClassRechargePointRate"].ToString() != "")
					{
						model.ClassRechargePointRate = decimal.Parse(dt.Rows[i]["ClassRechargePointRate"].ToString());
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

		public int GetRechargePointRate(int memLevelID, int shopID)
		{
			return this.dal.GetRechargePointRate(memLevelID, shopID);
		}

		public bool DeleteByMemLevelID(int MemLevelID)
		{
			return this.dal.DeleteByMemLevelID(MemLevelID);
		}
	}
}
