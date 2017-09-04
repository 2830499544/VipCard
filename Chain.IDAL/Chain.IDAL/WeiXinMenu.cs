using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class WeiXinMenu
	{
		public bool Exists(int MenuID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from WeiXinMenu");
			strSql.Append(" where MenuID=@MenuID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MenuID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MenuID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.WeiXinMenu model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into WeiXinMenu(");
			strSql.Append("MenuName,MenuType,MenuKey,MenuUrl,parentMenuID)");
			strSql.Append(" values (");
			strSql.Append("@MenuName,@MenuType,@MenuKey,@MenuUrl,@parentMenuID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MenuName", SqlDbType.NVarChar, 50),
				new SqlParameter("@MenuType", SqlDbType.Int, 4),
				new SqlParameter("@MenuKey", SqlDbType.NVarChar, 128),
				new SqlParameter("@MenuUrl", SqlDbType.NVarChar, 256),
				new SqlParameter("@parentMenuID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MenuName;
			parameters[1].Value = model.MenuType;
			parameters[2].Value = model.MenuKey;
			parameters[3].Value = model.MenuUrl;
			parameters[4].Value = model.parentMenuID;
			object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
			int result;
			if (obj == null)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(obj);
			}
			return result;
		}

		public bool Update(Chain.Model.WeiXinMenu model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update WeiXinMenu set ");
			strSql.Append("MenuName=@MenuName,");
			strSql.Append("MenuType=@MenuType,");
			strSql.Append("MenuKey=@MenuKey,");
			strSql.Append("MenuUrl=@MenuUrl,");
			strSql.Append("parentMenuID=@parentMenuID");
			strSql.Append(" where MenuID=@MenuID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MenuName", SqlDbType.NVarChar, 50),
				new SqlParameter("@MenuType", SqlDbType.Int, 4),
				new SqlParameter("@MenuKey", SqlDbType.NVarChar, 128),
				new SqlParameter("@MenuUrl", SqlDbType.NVarChar, 256),
				new SqlParameter("@parentMenuID", SqlDbType.Int, 4),
				new SqlParameter("@MenuID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MenuName;
			parameters[1].Value = model.MenuType;
			parameters[2].Value = model.MenuKey;
			parameters[3].Value = model.MenuUrl;
			parameters[4].Value = model.parentMenuID;
			parameters[5].Value = model.MenuID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int MenuID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from WeiXinMenu ");
			strSql.Append(" where MenuID=@MenuID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MenuID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MenuID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string MenuIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from WeiXinMenu ");
			strSql.Append(" where MenuID in (" + MenuIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.WeiXinMenu GetModel(int MenuID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 MenuID,MenuName,MenuType,MenuKey,MenuUrl,parentMenuID from WeiXinMenu ");
			strSql.Append(" where MenuID=@MenuID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MenuID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MenuID;
			Chain.Model.WeiXinMenu model = new Chain.Model.WeiXinMenu();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.WeiXinMenu result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["MenuID"] != null && ds.Tables[0].Rows[0]["MenuID"].ToString() != "")
				{
					model.MenuID = int.Parse(ds.Tables[0].Rows[0]["MenuID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MenuName"] != null && ds.Tables[0].Rows[0]["MenuName"].ToString() != "")
				{
					model.MenuName = ds.Tables[0].Rows[0]["MenuName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MenuType"] != null && ds.Tables[0].Rows[0]["MenuType"].ToString() != "")
				{
					model.MenuType = int.Parse(ds.Tables[0].Rows[0]["MenuType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MenuKey"] != null && ds.Tables[0].Rows[0]["MenuKey"].ToString() != "")
				{
					model.MenuKey = ds.Tables[0].Rows[0]["MenuKey"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MenuUrl"] != null && ds.Tables[0].Rows[0]["MenuUrl"].ToString() != "")
				{
					model.MenuUrl = ds.Tables[0].Rows[0]["MenuUrl"].ToString();
				}
				if (ds.Tables[0].Rows[0]["parentMenuID"] != null && ds.Tables[0].Rows[0]["parentMenuID"].ToString() != "")
				{
					model.parentMenuID = int.Parse(ds.Tables[0].Rows[0]["parentMenuID"].ToString());
				}
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select MenuID,MenuName,MenuType,MenuKey,MenuUrl,parentMenuID ");
			strSql.Append(" FROM WeiXinMenu ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ");
			if (Top > 0)
			{
				strSql.Append(" top " + Top.ToString());
			}
			strSql.Append(" MenuID,MenuName,MenuType,MenuKey,MenuUrl,parentMenuID ");
			strSql.Append(" FROM WeiXinMenu ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) FROM WeiXinMenu ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			int result;
			if (obj == null)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(obj);
			}
			return result;
		}

		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby);
			}
			else
			{
				strSql.Append("order by T.MenuID desc");
			}
			strSql.Append(")AS Row, T.*  from WeiXinMenu T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetMenuParentInfo()
		{
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("select MenuID,MenuName,MenuType,MenuKey,MenuUrl,parentMenuID ");
			sbSql.Append(",(select count(*) from WeiXinMenu as WeiXinMenuChild where WeiXinMenuChild.parentMenuID=WeiXinMenuParent.MenuID) as childNum ");
			sbSql.Append("from WeiXinMenu as WeiXinMenuParent where parentMenuID=0");
			return DbHelperSQL.Query(sbSql.ToString());
		}

		public DataSet GetMenuChildInfo(int parentMenuID)
		{
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("select MenuID,MenuName,MenuType,MenuKey,MenuUrl,parentMenuID ");
			sbSql.Append("from WeiXinMenu as WeiXinMenuParent where parentMenuID=@parentMenuID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@parentMenuID", SqlDbType.Int, 4)
			};
			parameters[0].Value = parentMenuID;
			return DbHelperSQL.Query(sbSql.ToString(), parameters);
		}

		public DataSet GetMenuAllInfo()
		{
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("select MenuID,MenuName,MenuType,MenuKey,MenuUrl,parentMenuID ");
			sbSql.Append(",(select count(*) from WeiXinMenu as WeiXinMenuChild where WeiXinMenuChild.parentMenuID=WeiXinMenuParent.MenuID) as childNum ");
			sbSql.Append("from WeiXinMenu as WeiXinMenuParent");
			sbSql.Append(";select count(*) from WeiXinMenu where parentMenuID=0");
			return DbHelperSQL.Query(sbSql.ToString());
		}

		public DataSet GetMenuAllInfo2()
		{
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("select *,(select count(*) from WeiXinMenu where ParentMenuID=1) as childNum from WeiXinMenu  where MenuID=1 ");
			sbSql.Append("union all ");
			sbSql.Append("select *,0 as childNum from WeiXinMenu where ParentMenuID=1 ");
			sbSql.Append("union all ");
			sbSql.Append("select *,(select count(*) from WeiXinMenu where ParentMenuID=2) as childNum from WeiXinMenu  where MenuID=2 ");
			sbSql.Append("union all ");
			sbSql.Append("select *,0 as childNum from WeiXinMenu where ParentMenuID=2 ");
			sbSql.Append("union all ");
			sbSql.Append("select *,(select count(*) from WeiXinMenu where ParentMenuID=3) as childNum from WeiXinMenu  where MenuID=3 ");
			sbSql.Append("union all ");
			sbSql.Append("select *,0 as childNum from WeiXinMenu where ParentMenuID=3 ");
			sbSql.Append(";select count(*) from WeiXinMenu where parentMenuID=0 ");
			return DbHelperSQL.Query(sbSql.ToString());
		}

		public int UpdateMenuKey(string OldMenuKey, string NewMenuKey)
		{
			string strSql = "update WeiXinMenu set MenuKey=@NewMenuKey where MenuKey=@OldMenuKey";
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OldMenuKey", SqlDbType.NVarChar, 128),
				new SqlParameter("@NewMenuKey", SqlDbType.NVarChar, 128)
			};
			parameters[0].Value = OldMenuKey;
			parameters[1].Value = NewMenuKey;
			return DbHelperSQL.ExecuteSql(strSql, parameters);
		}

		public int GetUseCountByRuleID(int RuleID)
		{
			string strSql = " select count(*) from WeiXinRule,WeiXinMenu where WeiXinRule.RuleNUmber=WeiXinMenu.MenuKey and WeiXinRule.RuleID=@RuleID";
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RuleID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RuleID;
			object obj = DbHelperSQL.GetSingle(strSql, parameters);
			int result;
			if (obj == null)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(obj);
			}
			return result;
		}
	}
}
