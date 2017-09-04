using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysError
	{
		public bool Exists(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysError");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysError model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysError(");
			strSql.Append("ErrorContent,ErrorTime,Ipaddress,ErrorType,UserID,ShopID)");
			strSql.Append(" values (");
			strSql.Append("@ErrorContent,@ErrorTime,@Ipaddress,@ErrorType,@UserID,@ShopID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ErrorContent", SqlDbType.NText),
				new SqlParameter("@ErrorTime", SqlDbType.DateTime),
				new SqlParameter("@Ipaddress", SqlDbType.NVarChar, 200),
				new SqlParameter("@ErrorType", SqlDbType.NVarChar, 50),
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ErrorContent;
			parameters[1].Value = model.ErrorTime;
			parameters[2].Value = model.Ipaddress;
			parameters[3].Value = model.ErrorType;
			parameters[4].Value = model.UserID;
			parameters[5].Value = model.ShopID;
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

		public bool Update(Chain.Model.SysError model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysError set ");
			strSql.Append("ErrorContent=@ErrorContent,");
			strSql.Append("ErrorTime=@ErrorTime,");
			strSql.Append("Ipaddress=@Ipaddress,");
			strSql.Append("ErrorType=@ErrorType,");
			strSql.Append("UserID=@UserID,");
			strSql.Append("ShopID=@ShopID");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ErrorContent", SqlDbType.NText),
				new SqlParameter("@ErrorTime", SqlDbType.DateTime),
				new SqlParameter("@Ipaddress", SqlDbType.NVarChar, 200),
				new SqlParameter("@ID", SqlDbType.Int, 4),
				new SqlParameter("@ErrorType", SqlDbType.NVarChar, 50),
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ErrorContent;
			parameters[1].Value = model.ErrorTime;
			parameters[2].Value = model.Ipaddress;
			parameters[3].Value = model.ID;
			parameters[4].Value = model.ErrorType;
			parameters[5].Value = model.UserID;
			parameters[6].Value = model.ShopID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysError ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string IDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysError ");
			strSql.Append(" where ID in (" + IDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysError GetModel(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ID,ErrorContent,ErrorTime,Ipaddress,ErrorType,UserID,ShopID from SysError ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			Chain.Model.SysError model = new Chain.Model.SysError();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysError result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
				{
					model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ErrorContent"] != null && ds.Tables[0].Rows[0]["ErrorContent"].ToString() != "")
				{
					model.ErrorContent = ds.Tables[0].Rows[0]["ErrorContent"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ErrorTime"] != null && ds.Tables[0].Rows[0]["ErrorTime"].ToString() != "")
				{
					model.ErrorTime = DateTime.Parse(ds.Tables[0].Rows[0]["ErrorTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Ipaddress"] != null && ds.Tables[0].Rows[0]["Ipaddress"].ToString() != "")
				{
					model.Ipaddress = ds.Tables[0].Rows[0]["Ipaddress"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ErrorType"] != null && ds.Tables[0].Rows[0]["ErrorType"].ToString() != "")
				{
					model.ErrorType = ds.Tables[0].Rows[0]["Ipaddress"].ToString();
				}
				if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
				{
					model.UserID = Convert.ToInt32(ds.Tables[0].Rows[0]["UserID"]);
				}
				if (ds.Tables[0].Rows[0]["ShopID"] != null && ds.Tables[0].Rows[0]["ShopID"].ToString() != "")
				{
					model.ShopID = Convert.ToInt32(ds.Tables[0].Rows[0]["ShopID"]);
				}
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public DataSet GetErrorContent(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ID,ErrorContent");
			strSql.Append(" FROM SysError ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ID,ErrorContent,ErrorTime,Ipaddress,ErrorType,UserID,ShopID ");
			strSql.Append(" FROM SysError ");
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
			strSql.Append(" ID,ErrorContent,ErrorTime,Ipaddress,ErrorType,UserID,ShopID ");
			strSql.Append(" FROM SysError ");
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
			strSql.Append("select count(1) FROM SysError ");
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from SysError T ");
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
			string tableName = "V_SysErrorLog";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ErrorTime", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int CleadSysError(int strDay)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.AppendFormat(" delete from SysError where ErrorTime<=(dateadd(day,(-{0}),getdate()))", strDay);
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}
	}
}
