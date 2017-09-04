using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class GoodsClassAuthority
	{
		private readonly Chain.IDAL.GoodsClassAuthority dal = new Chain.IDAL.GoodsClassAuthority();

		public bool Exists(int ClassID, int ShopID)
		{
			return this.dal.Exists(ClassID, ShopID);
		}

		public int Add(Chain.Model.GoodsClassAuthority model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.GoodsClassAuthority model)
		{
			return this.dal.Update(model);
		}

		public bool DeleteByWhere(string strWhere)
		{
			return this.dal.DeleteByWhere(strWhere);
		}

		public bool Delete(int ClassAuthorityID)
		{
			return this.dal.Delete(ClassAuthorityID);
		}

		public bool DeleteList(string ClassAuthorityIDlist)
		{
			return this.dal.DeleteList(ClassAuthorityIDlist);
		}

		public Chain.Model.GoodsClassAuthority GetModel(int ClassAuthorityID)
		{
			return this.dal.GetModel(ClassAuthorityID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.GoodsClassAuthority> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.GoodsClassAuthority> DataTableToList(DataTable dt)
		{
			List<Chain.Model.GoodsClassAuthority> modelList = new List<Chain.Model.GoodsClassAuthority>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.GoodsClassAuthority model = new Chain.Model.GoodsClassAuthority();
					if (dt.Rows[i]["ClassAuthorityID"] != null && dt.Rows[i]["ClassAuthorityID"].ToString() != "")
					{
						model.ClassAuthorityID = int.Parse(dt.Rows[i]["ClassAuthorityID"].ToString());
					}
					if (dt.Rows[i]["ClassID"] != null && dt.Rows[i]["ClassID"].ToString() != "")
					{
						model.ClassID = int.Parse(dt.Rows[i]["ClassID"].ToString());
					}
					if (dt.Rows[i]["ShopID"] != null && dt.Rows[i]["ShopID"].ToString() != "")
					{
						model.ShopID = int.Parse(dt.Rows[i]["ShopID"].ToString());
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

		public int SyncGoodsClass(int ClassID)
		{
			GoodsClass bllGC = new GoodsClass();
			Chain.Model.GoodsClass model = bllGC.GetModel(ClassID);
			int result;
			if (model != null)
			{
				if (model.ParentID != 0)
				{
					this.dal.SyncGoodsClass(model.ParentID);
				}
				result = this.dal.SyncGoodsClass(ClassID);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public int SyncGoodsClass(int ClassID, int ShopID)
		{
			GoodsClass bllGC = new GoodsClass();
			Chain.Model.GoodsClass model = bllGC.GetModel(ClassID);
			int result;
			if (model != null)
			{
				if (model.ParentID != 0)
				{
					this.dal.SyncGoodsClass(model.ParentID, ShopID);
				}
				result = this.dal.SyncGoodsClass(ClassID, ShopID);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public int SyncGoodsClass(int ClassID, List<int> ShopList)
		{
			GoodsClass bllGC = new GoodsClass();
			Chain.Model.GoodsClass model = bllGC.GetModel(ClassID);
			int result;
			if (model != null && ShopList.Count > 0)
			{
				if (model.ParentID != 0)
				{
					this.dal.SyncGoodsClass(model.ParentID, ShopList);
				}
				result = this.dal.SyncGoodsClass(ClassID, ShopList);
			}
			else
			{
				result = -1;
			}
			return result;
		}

		public DataSet GetShopIDListByClass(int ClassID)
		{
			return this.dal.GetShopIDListByClass(ClassID);
		}

		public int DeleteAuthority(int ClassID, int ShopID)
		{
			return this.dal.DeleteAuthority(ClassID, ShopID);
		}

		public int GetClassCountByParentID(int ParentID, int ShopID)
		{
			return this.dal.GetClassCountByParentID(ParentID, ShopID);
		}

		public int GetClassCountByParentID(int ParentID)
		{
			return this.dal.GetClassCountByParentID(ParentID);
		}
	}
}
