using Chain.DBUtility;
using Chain.Model;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysLog
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("LogID", "SysLog");
		}

		public bool Exists(int LogID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysLog");
			strSql.Append(" where LogID=@LogID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@LogID", SqlDbType.Int, 4)
			};
			parameters[0].Value = LogID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysLog(");
			strSql.Append("LogUserID,LogActionID,LogType,LogDetail,LogShopID,LogCreateTime,LogIPAdress)");
			strSql.Append(" values (");
			strSql.Append("@LogUserID,@LogActionID,@LogType,@LogDetail,@LogShopID,@LogCreateTime,@LogIPAdress)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@LogUserID", SqlDbType.Int, 4),
				new SqlParameter("@LogActionID", SqlDbType.Int, 4),
				new SqlParameter("@LogType", SqlDbType.NVarChar, 50),
				new SqlParameter("@LogDetail", SqlDbType.NVarChar, 1000),
				new SqlParameter("@LogShopID", SqlDbType.Int, 4),
				new SqlParameter("@LogCreateTime", SqlDbType.DateTime),
				new SqlParameter("@LogIPAdress", SqlDbType.VarChar, 50)
			};
			parameters[0].Value = model.LogUserID;
			parameters[1].Value = model.LogActionID;
			parameters[2].Value = model.LogType;
			parameters[3].Value = model.LogDetail;
			parameters[4].Value = model.LogShopID;
			parameters[5].Value = model.LogCreateTime;
			parameters[6].Value = model.LogIPAdress;
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

		public bool Update(Chain.Model.SysLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysLog set ");
			strSql.Append("LogUserID=@LogUserID,");
			strSql.Append("LogActionID=@LogActionID,");
			strSql.Append("LogType=@LogType,");
			strSql.Append("LogDetail=@LogDetail,");
			strSql.Append("LogShopID=@LogShopID,");
			strSql.Append("LogCreateTime=@LogCreateTime,");
			strSql.Append("LogIPAdress=@LogIPAdress");
			strSql.Append(" where LogID=@LogID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@LogUserID", SqlDbType.Int, 4),
				new SqlParameter("@LogActionID", SqlDbType.Int, 4),
				new SqlParameter("@LogType", SqlDbType.NVarChar, 50),
				new SqlParameter("@LogDetail", SqlDbType.NVarChar, 1000),
				new SqlParameter("@LogShopID", SqlDbType.Int, 4),
				new SqlParameter("@LogCreateTime", SqlDbType.DateTime),
				new SqlParameter("@LogID", SqlDbType.Int, 4),
				new SqlParameter("@LogIPAdress", SqlDbType.VarChar, 50)
			};
			parameters[0].Value = model.LogUserID;
			parameters[1].Value = model.LogActionID;
			parameters[2].Value = model.LogType;
			parameters[3].Value = model.LogDetail;
			parameters[4].Value = model.LogShopID;
			parameters[5].Value = model.LogCreateTime;
			parameters[6].Value = model.LogID;
			parameters[7].Value = model.LogIPAdress;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int LogID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysLog ");
			strSql.Append(" where LogID=@LogID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@LogID", SqlDbType.Int, 4)
			};
			parameters[0].Value = LogID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string LogIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysLog ");
			strSql.Append(" where LogID in (" + LogIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysLog GetModel(int LogID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 LogID,LogUserID,LogActionID,LogType,LogDetail,LogShopID,LogCreateTime,LogIPAdress from SysLog ");
			strSql.Append(" where LogID=@LogID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@LogID", SqlDbType.Int, 4)
			};
			parameters[0].Value = LogID;
			Chain.Model.SysLog model = new Chain.Model.SysLog();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysLog result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["LogID"] != null && ds.Tables[0].Rows[0]["LogID"].ToString() != "")
				{
					model.LogID = int.Parse(ds.Tables[0].Rows[0]["LogID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LogUserID"] != null && ds.Tables[0].Rows[0]["LogUserID"].ToString() != "")
				{
					model.LogUserID = int.Parse(ds.Tables[0].Rows[0]["LogUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LogActionID"] != null && ds.Tables[0].Rows[0]["LogActionID"].ToString() != "")
				{
					model.LogActionID = int.Parse(ds.Tables[0].Rows[0]["LogActionID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LogType"] != null && ds.Tables[0].Rows[0]["LogType"].ToString() != "")
				{
					model.LogType = ds.Tables[0].Rows[0]["LogType"].ToString();
				}
				if (ds.Tables[0].Rows[0]["LogDetail"] != null && ds.Tables[0].Rows[0]["LogDetail"].ToString() != "")
				{
					model.LogDetail = ds.Tables[0].Rows[0]["LogDetail"].ToString();
				}
				if (ds.Tables[0].Rows[0]["LogShopID"] != null && ds.Tables[0].Rows[0]["LogShopID"].ToString() != "")
				{
					model.LogShopID = int.Parse(ds.Tables[0].Rows[0]["LogShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LogCreateTime"] != null && ds.Tables[0].Rows[0]["LogCreateTime"].ToString() != "")
				{
					model.LogCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["LogCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LogIPAdress"] != null && ds.Tables[0].Rows[0]["LogIPAdress"].ToString() != "")
				{
					model.LogIPAdress = ds.Tables[0].Rows[0]["LogIPAdress"].ToString();
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
			strSql.Append("select LogID,LogUserID,LogActionID,LogType,LogDetail,LogShopID,LogCreateTime,LogIPAdress ");
			strSql.Append(" FROM SysLog ");
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
			strSql.Append(" LogID,LogUserID,LogActionID,LogType,LogDetail,LogShopID,LogCreateTime,LogIPAdress ");
			strSql.Append(" FROM SysLog ");
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
			strSql.Append("select count(1) FROM SysLog ");
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
				strSql.Append("order by T.LogID desc");
			}
			strSql.Append(")AS Row, T.*  from SysLog T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "SysLog,SysShop,SysUser";
			string[] columns = new string[]
			{
				"SysLog.*,SysShop.ShopName,SysUser.UserAccount,SysUser.UserName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "LogCreateTime", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetActionList(string MemCard)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select top 1 * from SysLog");
			strSql.AppendFormat(" where LogDetail like '%' + '{0}' + '%' ", MemCard);
			strSql.Append(" and LogActionID>0");
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int CleadSysLog(int strDay)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.AppendFormat(" delete from SysLog where LogCreateTime<=(dateadd(day,(-{0}),getdate()))", strDay);
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		public bool DataBaseInit(ArrayList arrSql)
		{
			return DbHelperSQL.ExecuteSqlTran(arrSql);
		}
	}
}
