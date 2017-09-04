using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysModule
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ModuleID", "SysModule");
		}

		public bool Exists(int ModuleID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysModule");
			strSql.Append(" where ModuleID=@ModuleID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ModuleID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ModuleID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysModule model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysModule(");
			strSql.Append("ModuleCaption,ModuleLink,ModuleParentID,ModuleOrder,ModuleVisible,ModuleIcoPath,ModuleRemark)");
			strSql.Append(" values (");
			strSql.Append("@ModuleCaption,@ModuleLink,@ModuleParentID,@ModuleOrder,@ModuleVisible,@ModuleIcoPath,@ModuleRemark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ModuleCaption", SqlDbType.NVarChar, 200),
				new SqlParameter("@ModuleLink", SqlDbType.VarChar, 500),
				new SqlParameter("@ModuleParentID", SqlDbType.Int, 4),
				new SqlParameter("@ModuleOrder", SqlDbType.Int, 4),
				new SqlParameter("@ModuleVisible", SqlDbType.Bit, 1),
				new SqlParameter("@ModuleIcoPath", SqlDbType.VarChar, 50),
				new SqlParameter("@ModuleRemark", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = model.ModuleCaption;
			parameters[1].Value = model.ModuleLink;
			parameters[2].Value = model.ModuleParentID;
			parameters[3].Value = model.ModuleOrder;
			parameters[4].Value = model.ModuleVisible;
			parameters[5].Value = model.ModuleIcoPath;
			parameters[6].Value = model.ModuleRemark;
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

		public bool Update(Chain.Model.SysModule model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysModule set ");
			strSql.Append("ModuleCaption=@ModuleCaption,");
			strSql.Append("ModuleLink=@ModuleLink,");
			strSql.Append("ModuleParentID=@ModuleParentID,");
			strSql.Append("ModuleOrder=@ModuleOrder,");
			strSql.Append("ModuleVisible=@ModuleVisible,");
			strSql.Append("ModuleIcoPath=@ModuleIcoPath,");
			strSql.Append("ModuleRemark=@ModuleRemark");
			strSql.Append(" where ModuleID=@ModuleID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ModuleCaption", SqlDbType.NVarChar, 200),
				new SqlParameter("@ModuleLink", SqlDbType.VarChar, 500),
				new SqlParameter("@ModuleParentID", SqlDbType.Int, 4),
				new SqlParameter("@ModuleOrder", SqlDbType.Int, 4),
				new SqlParameter("@ModuleVisible", SqlDbType.Bit, 1),
				new SqlParameter("@ModuleIcoPath", SqlDbType.VarChar, 50),
				new SqlParameter("@ModuleRemark", SqlDbType.NVarChar, 50),
				new SqlParameter("@ModuleID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ModuleCaption;
			parameters[1].Value = model.ModuleLink;
			parameters[2].Value = model.ModuleParentID;
			parameters[3].Value = model.ModuleOrder;
			parameters[4].Value = model.ModuleVisible;
			parameters[5].Value = model.ModuleIcoPath;
			parameters[6].Value = model.ModuleRemark;
			parameters[7].Value = model.ModuleID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ModuleID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysModule ");
			strSql.Append(" where ModuleID=@ModuleID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ModuleID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ModuleID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string ModuleIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysModule ");
			strSql.Append(" where ModuleID in (" + ModuleIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysModule GetModel(int ModuleID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ModuleID,ModuleCaption,ModuleLink,ModuleParentID,ModuleOrder,ModuleVisible,ModuleIcoPath,ModuleRemark from SysModule ");
			strSql.Append(" where ModuleID=@ModuleID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ModuleID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ModuleID;
			Chain.Model.SysModule model = new Chain.Model.SysModule();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysModule result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				result = this.DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Chain.Model.SysModule DataRowToModel(DataRow row)
		{
			Chain.Model.SysModule model = new Chain.Model.SysModule();
			if (row != null)
			{
				if (row["ModuleID"] != null && row["ModuleID"].ToString() != "")
				{
					model.ModuleID = int.Parse(row["ModuleID"].ToString());
				}
				if (row["ModuleCaption"] != null)
				{
					model.ModuleCaption = row["ModuleCaption"].ToString();
				}
				if (row["ModuleLink"] != null)
				{
					model.ModuleLink = row["ModuleLink"].ToString();
				}
				if (row["ModuleParentID"] != null && row["ModuleParentID"].ToString() != "")
				{
					model.ModuleParentID = int.Parse(row["ModuleParentID"].ToString());
				}
				if (row["ModuleOrder"] != null && row["ModuleOrder"].ToString() != "")
				{
					model.ModuleOrder = int.Parse(row["ModuleOrder"].ToString());
				}
				if (row["ModuleVisible"] != null && row["ModuleVisible"].ToString() != "")
				{
					if (row["ModuleVisible"].ToString() == "1" || row["ModuleVisible"].ToString().ToLower() == "true")
					{
						model.ModuleVisible = true;
					}
					else
					{
						model.ModuleVisible = false;
					}
				}
				if (row["ModuleIcoPath"] != null)
				{
					model.ModuleIcoPath = row["ModuleIcoPath"].ToString();
				}
				if (row["ModuleRemark"] != null)
				{
					model.ModuleRemark = row["ModuleRemark"].ToString();
				}
			}
			return model;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ModuleID,ModuleCaption,ModuleLink,ModuleParentID,ModuleOrder,ModuleVisible,ModuleIcoPath,ModuleRemark ");
			strSql.Append(" FROM SysModule ");
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
			strSql.Append(" ModuleID,ModuleCaption,ModuleLink,ModuleParentID,ModuleOrder,ModuleVisible,ModuleIcoPath,ModuleRemark ");
			strSql.Append(" FROM SysModule ");
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
			strSql.Append("select count(1) FROM SysModule ");
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
				strSql.Append("order by T.ModuleID desc");
			}
			strSql.Append(")AS Row, T.*  from SysModule T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetList(int PageSize, int PageIndex, string[] strWhere, out int resCount)
		{
			string tableName = "SysModule";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int UpdateIsDataInit(int intType)
		{
			string strSql = " update SysModule set ModuleVisible=" + intType + " where ModuleID=83";
			return DbHelperSQL.ExecuteSql(strSql);
		}

		public DataTable GetList(int moduleParentID)
		{
			string strSql = "select * from SysModule where ModuleParentID=@ModuleParentID order by ModuleOrder,ModuleID";
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ModuleParentID", SqlDbType.Int, 4)
			};
			parameters[0].Value = moduleParentID;
			return DbHelperSQL.Query(strSql, parameters).Tables[0];
		}
	}
}
