using Chain.IDAL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace Chain.BLL
{
	public class WeiXinMenu
	{
		private readonly Chain.IDAL.WeiXinMenu dal = new Chain.IDAL.WeiXinMenu();

		public DataSet GetMenuParentInfo()
		{
			return this.dal.GetMenuParentInfo();
		}

		public DataSet GetMenuChildInfo(int parentMenuID)
		{
			return this.dal.GetMenuChildInfo(parentMenuID);
		}

		public DataSet GetMenuAllInfo()
		{
			return this.dal.GetMenuAllInfo();
		}

		public DataSet GetMenuAllInfo2()
		{
			return this.dal.GetMenuAllInfo2();
		}

		public int UpdateMenuKey(string OldMenuKey, string NewMenuKey)
		{
			return this.dal.UpdateMenuKey(OldMenuKey, NewMenuKey);
		}

		public int GetUseCountByRuleID(int RuleID)
		{
			return this.dal.GetUseCountByRuleID(RuleID);
		}

		public bool Exists(int MenuID)
		{
			return this.dal.Exists(MenuID);
		}

		public int Add(Chain.Model.WeiXinMenu model)
		{
			return this.dal.Add(model);
		}

		public bool Update(Chain.Model.WeiXinMenu model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int MenuID)
		{
			return this.dal.Delete(MenuID);
		}

		public bool DeleteList(string MenuIDlist)
		{
			return this.dal.DeleteList(MenuIDlist);
		}

		public Chain.Model.WeiXinMenu GetModel(int MenuID)
		{
			return this.dal.GetModel(MenuID);
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<Chain.Model.WeiXinMenu> GetModelList(string strWhere)
		{
			DataSet ds = this.dal.GetList(strWhere);
			return this.DataTableToList(ds.Tables[0]);
		}

		public List<Chain.Model.WeiXinMenu> DataTableToList(DataTable dt)
		{
			List<Chain.Model.WeiXinMenu> modelList = new List<Chain.Model.WeiXinMenu>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				for (int i = 0; i < rowsCount; i++)
				{
					Chain.Model.WeiXinMenu model = new Chain.Model.WeiXinMenu();
					if (dt.Rows[i]["MenuID"] != null && dt.Rows[i]["MenuID"].ToString() != "")
					{
						model.MenuID = int.Parse(dt.Rows[i]["MenuID"].ToString());
					}
					if (dt.Rows[i]["MenuName"] != null && dt.Rows[i]["MenuName"].ToString() != "")
					{
						model.MenuName = dt.Rows[i]["MenuName"].ToString();
					}
					if (dt.Rows[i]["MenuType"] != null && dt.Rows[i]["MenuType"].ToString() != "")
					{
						model.MenuType = int.Parse(dt.Rows[i]["MenuType"].ToString());
					}
					if (dt.Rows[i]["MenuKey"] != null && dt.Rows[i]["MenuKey"].ToString() != "")
					{
						model.MenuKey = dt.Rows[i]["MenuKey"].ToString();
					}
					if (dt.Rows[i]["MenuUrl"] != null && dt.Rows[i]["MenuUrl"].ToString() != "")
					{
						model.MenuUrl = dt.Rows[i]["MenuUrl"].ToString();
					}
					if (dt.Rows[i]["parentMenuID"] != null && dt.Rows[i]["parentMenuID"].ToString() != "")
					{
						model.parentMenuID = int.Parse(dt.Rows[i]["parentMenuID"].ToString());
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
