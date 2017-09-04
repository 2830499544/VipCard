using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SymbolShow
	{
		public bool Exists(int SymbolID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SymbolShow");
			strSql.Append(" where SymbolID=@SymbolID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SymbolID", SqlDbType.Int, 4)
			};
			parameters[0].Value = SymbolID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SymbolShow model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SymbolShow(");
			strSql.Append("SymbolTitle,SymbolPhoto,SymbolDesc,SymbolTime)");
			strSql.Append(" values (");
			strSql.Append("@SymbolTitle,@SymbolPhoto,@SymbolDesc,@SymbolTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SymbolTitle", SqlDbType.NVarChar, 50),
				new SqlParameter("@SymbolPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@SymbolDesc", SqlDbType.NVarChar, 4000),
				new SqlParameter("@SymbolTime", SqlDbType.DateTime)
			};
			parameters[0].Value = model.SymbolTitle;
			parameters[1].Value = model.SymbolPhoto;
			parameters[2].Value = model.SymbolDesc;
			parameters[3].Value = model.SymbolTime;
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

		public bool Update(Chain.Model.SymbolShow model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SymbolShow set ");
			strSql.Append("SymbolTitle=@SymbolTitle,");
			strSql.Append("SymbolPhoto=@SymbolPhoto,");
			strSql.Append("SymbolDesc=@SymbolDesc,");
			strSql.Append("SymbolTime=@SymbolTime");
			strSql.Append(" where SymbolID=@SymbolID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SymbolTitle", SqlDbType.NVarChar, 50),
				new SqlParameter("@SymbolPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@SymbolDesc", SqlDbType.NVarChar, 4000),
				new SqlParameter("@SymbolTime", SqlDbType.DateTime),
				new SqlParameter("@SymbolID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.SymbolTitle;
			parameters[1].Value = model.SymbolPhoto;
			parameters[2].Value = model.SymbolDesc;
			parameters[3].Value = model.SymbolTime;
			parameters[4].Value = model.SymbolID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int SymbolID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SymbolShow ");
			strSql.Append(" where SymbolID=@SymbolID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SymbolID", SqlDbType.Int, 4)
			};
			parameters[0].Value = SymbolID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string SymbolIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SymbolShow ");
			strSql.Append(" where SymbolID in (" + SymbolIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SymbolShow GetModel(int SymbolID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 SymbolID,SymbolTitle,SymbolPhoto,SymbolDesc,SymbolTime from SymbolShow ");
			strSql.Append(" where SymbolID=@SymbolID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SymbolID", SqlDbType.Int, 4)
			};
			parameters[0].Value = SymbolID;
			Chain.Model.SymbolShow model = new Chain.Model.SymbolShow();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SymbolShow result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["SymbolID"] != null && ds.Tables[0].Rows[0]["SymbolID"].ToString() != "")
				{
					model.SymbolID = int.Parse(ds.Tables[0].Rows[0]["SymbolID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SymbolTitle"] != null && ds.Tables[0].Rows[0]["SymbolTitle"].ToString() != "")
				{
					model.SymbolTitle = ds.Tables[0].Rows[0]["SymbolTitle"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SymbolPhoto"] != null && ds.Tables[0].Rows[0]["SymbolPhoto"].ToString() != "")
				{
					model.SymbolPhoto = ds.Tables[0].Rows[0]["SymbolPhoto"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SymbolDesc"] != null && ds.Tables[0].Rows[0]["SymbolDesc"].ToString() != "")
				{
					model.SymbolDesc = ds.Tables[0].Rows[0]["SymbolDesc"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SymbolTime"] != null && ds.Tables[0].Rows[0]["SymbolTime"].ToString() != "")
				{
					model.SymbolTime = DateTime.Parse(ds.Tables[0].Rows[0]["SymbolTime"].ToString());
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
			strSql.Append("select SymbolID,SymbolTitle,SymbolPhoto,SymbolDesc,SymbolTime ");
			strSql.Append(" FROM SymbolShow ");
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
			strSql.Append(" SymbolID,SymbolTitle,SymbolPhoto,SymbolDesc,SymbolTime ");
			strSql.Append(" FROM SymbolShow ");
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
			strSql.Append("select count(1) FROM SymbolShow ");
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
				strSql.Append("order by T.SymbolID desc");
			}
			strSql.Append(")AS Row, T.*  from SymbolShow T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetSymbolShowInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "SymbolShow";
			string[] columns = new string[]
			{
				"SymbolID,SymbolTitle,SymbolPhoto,SymbolDesc,SymbolTime"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "SymbolID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
