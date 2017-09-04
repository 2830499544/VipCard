using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysNotice
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("SysNoticeID", "SysNotice");
		}

		public bool Exists(int SysNoticeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysNotice");
			strSql.Append(" where SysNoticeID=@SysNoticeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SysNoticeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = SysNoticeID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysNotice model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysNotice(");
			strSql.Append("SysNoticeCode,SysNotieceName,SysNoticeTitle,SysNoticeDetail,SysNoticeTime)");
			strSql.Append(" values (");
			strSql.Append("@SysNoticeCode,@SysNotieceName,@SysNoticeTitle,@SysNoticeDetail,@SysNoticeTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SysNoticeCode", SqlDbType.VarChar, 50),
				new SqlParameter("@SysNotieceName", SqlDbType.VarChar, 50),
				new SqlParameter("@SysNoticeTitle", SqlDbType.VarChar, 500),
				new SqlParameter("@SysNoticeDetail", SqlDbType.VarChar, 5000),
				new SqlParameter("@SysNoticeTime", SqlDbType.DateTime)
			};
			parameters[0].Value = model.SysNoticeCode;
			parameters[1].Value = model.SysNotieceName;
			parameters[2].Value = model.SysNoticeTitle;
			parameters[3].Value = model.SysNoticeDetail;
			parameters[4].Value = model.SysNoticeTime;
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

		public int Update(Chain.Model.SysNotice model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysNotice set ");
			strSql.Append("SysNoticeCode=@SysNoticeCode,");
			strSql.Append("SysNotieceName=@SysNotieceName,");
			strSql.Append("SysNoticeTitle=@SysNoticeTitle,");
			strSql.Append("SysNoticeDetail=@SysNoticeDetail,");
			strSql.Append("SysNoticeTime=@SysNoticeTime");
			strSql.Append(" where SysNoticeID=@SysNoticeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SysNoticeCode", SqlDbType.VarChar, 50),
				new SqlParameter("@SysNotieceName", SqlDbType.VarChar, 50),
				new SqlParameter("@SysNoticeTitle", SqlDbType.VarChar, 500),
				new SqlParameter("@SysNoticeDetail", SqlDbType.VarChar, 5000),
				new SqlParameter("@SysNoticeTime", SqlDbType.DateTime),
				new SqlParameter("@SysNoticeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.SysNoticeCode;
			parameters[1].Value = model.SysNotieceName;
			parameters[2].Value = model.SysNoticeTitle;
			parameters[3].Value = model.SysNoticeDetail;
			parameters[4].Value = model.SysNoticeTime;
			parameters[5].Value = model.SysNoticeID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			int result;
			if (rows > 0)
			{
				result = 1;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public bool Delete(int SysNoticeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysNotice ");
			strSql.Append(" where SysNoticeID=@SysNoticeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SysNoticeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = SysNoticeID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string SysNoticeIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysNotice ");
			strSql.Append(" where SysNoticeID in (" + SysNoticeIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysNotice GetModel(int SysNoticeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 SysNoticeID,SysNoticeCode,SysNotieceName,SysNoticeTitle,SysNoticeDetail,SysNoticeTime from SysNotice ");
			strSql.Append(" where SysNoticeID=@SysNoticeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SysNoticeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = SysNoticeID;
			Chain.Model.SysNotice model = new Chain.Model.SysNotice();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysNotice result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["SysNoticeID"] != null && ds.Tables[0].Rows[0]["SysNoticeID"].ToString() != "")
				{
					model.SysNoticeID = int.Parse(ds.Tables[0].Rows[0]["SysNoticeID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SysNoticeCode"] != null && ds.Tables[0].Rows[0]["SysNoticeCode"].ToString() != "")
				{
					model.SysNoticeCode = ds.Tables[0].Rows[0]["SysNoticeCode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SysNotieceName"] != null && ds.Tables[0].Rows[0]["SysNotieceName"].ToString() != "")
				{
					model.SysNotieceName = ds.Tables[0].Rows[0]["SysNotieceName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SysNoticeTitle"] != null && ds.Tables[0].Rows[0]["SysNoticeTitle"].ToString() != "")
				{
					model.SysNoticeTitle = ds.Tables[0].Rows[0]["SysNoticeTitle"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SysNoticeDetail"] != null && ds.Tables[0].Rows[0]["SysNoticeDetail"].ToString() != "")
				{
					model.SysNoticeDetail = ds.Tables[0].Rows[0]["SysNoticeDetail"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SysNoticeTime"] != null && ds.Tables[0].Rows[0]["SysNoticeTime"].ToString() != "")
				{
					model.SysNoticeTime = DateTime.Parse(ds.Tables[0].Rows[0]["SysNoticeTime"].ToString());
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
			strSql.Append("select SysNoticeID,SysNoticeCode,SysNotieceName,SysNoticeTitle,SysNoticeDetail,SysNoticeTime ");
			strSql.Append(" FROM SysNotice ");
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
			strSql.Append(" SysNoticeID,SysNoticeCode,SysNotieceName,SysNoticeTitle,SysNoticeDetail,SysNoticeTime ");
			strSql.Append(" FROM SysNotice ");
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
			strSql.Append("select count(1) FROM SysNotice ");
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
				strSql.Append("order by T.SysNoticeID desc");
			}
			strSql.Append(")AS Row, T.*  from SysNotice T ");
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
			string tableName = "SysNotice";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "SysNoticeID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
