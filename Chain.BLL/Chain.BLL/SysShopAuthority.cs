using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class SysShopAuthority
	{
		private readonly Chain.IDAL.SysShopAuthority dal = new Chain.IDAL.SysShopAuthority();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ShopAuthorityID)
		{
			return this.dal.Exists(ShopAuthorityID);
		}

		public int Add(Chain.Model.SysShopAuthority model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.SysShopAuthority model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ShopAuthorityID)
		{
			return this.dal.Delete(ShopAuthorityID);
		}

		public bool DeleteList(string ShopAuthorityIDlist)
		{
			return this.dal.DeleteList(ShopAuthorityIDlist);
		}

		public Chain.Model.SysShopAuthority GetModel(int ShopAuthorityID)
		{
			return this.dal.GetModel(ShopAuthorityID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.SysShopAuthority> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.SysShopAuthority> DataTableToList(DataTable dt)
		{
			List<Chain.Model.SysShopAuthority> modelList = new List<Chain.Model.SysShopAuthority>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.SysShopAuthority model = this.dal.DataRowToModel(dt.Rows[i]);
					if (model != null)
					{
						modelList.Add(model);
					}
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

		public DataSet GetShopAuthority(int ShopID)
		{
			return this.dal.GetShopAuthority(ShopID);
		}
	}
}
