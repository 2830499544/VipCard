using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class Album
	{
		public DataSet GetAlbumInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "Album,SysUser";
			string[] columns = new string[]
			{
				"AlbumID,AlbumName,AlbumPhoto,AlbumDesc,AlbumCreateTime,UserName,AlbumRemark"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "AlbumID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public bool Exists(int AlbumID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from Album");
			strSql.Append(" where AlbumID=@AlbumID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AlbumID", SqlDbType.Int, 4)
			};
			parameters[0].Value = AlbumID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.Album model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Album(");
			strSql.Append("CreateUserID,AlbumRemark,AlbumName,AlbumPhoto,AlbumDesc,AlbumCreateTime)");
			strSql.Append(" values (");
			strSql.Append("@CreateUserID,@AlbumRemark,@AlbumName,@AlbumPhoto,@AlbumDesc,@AlbumCreateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AlbumName", SqlDbType.NVarChar, 50),
				new SqlParameter("@AlbumPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@AlbumDesc", SqlDbType.NVarChar, 4000),
				new SqlParameter("@AlbumCreateTime", SqlDbType.DateTime),
				new SqlParameter("@AlbumRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@CreateUserID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.AlbumName;
			parameters[1].Value = model.AlbumPhoto;
			parameters[2].Value = model.AlbumDesc;
			parameters[3].Value = model.AlbumCreateTime;
			parameters[4].Value = model.AlbumRemark;
			parameters[5].Value = model.CreateUserID;
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

		public bool Update(Chain.Model.Album model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update Album set ");
			strSql.Append("AlbumRemark=@AlbumRemark,");
			strSql.Append("AlbumName=@AlbumName,");
			strSql.Append("AlbumPhoto=@AlbumPhoto,");
			strSql.Append("AlbumDesc=@AlbumDesc,");
			strSql.Append("AlbumCreateTime=@AlbumCreateTime");
			strSql.Append(" where AlbumID=@AlbumID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AlbumName", SqlDbType.NVarChar, 50),
				new SqlParameter("@AlbumPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@AlbumDesc", SqlDbType.NVarChar, 4000),
				new SqlParameter("@AlbumCreateTime", SqlDbType.DateTime),
				new SqlParameter("@AlbumRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@AlbumID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.AlbumName;
			parameters[1].Value = model.AlbumPhoto;
			parameters[2].Value = model.AlbumDesc;
			parameters[3].Value = model.AlbumCreateTime;
			parameters[4].Value = model.AlbumRemark;
			parameters[5].Value = model.AlbumID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int AlbumID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Album ");
			strSql.Append(" where AlbumID=@AlbumID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AlbumID", SqlDbType.Int, 4)
			};
			parameters[0].Value = AlbumID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string AlbumIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Album ");
			strSql.Append(" where AlbumID in (" + AlbumIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.Album GetModel(int AlbumID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 AlbumRemark, AlbumID,AlbumName,AlbumPhoto,AlbumDesc,AlbumCreateTime from Album ");
			strSql.Append(" where AlbumID=@AlbumID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AlbumID", SqlDbType.Int, 4)
			};
			parameters[0].Value = AlbumID;
			Chain.Model.Album model = new Chain.Model.Album();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.Album result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["AlbumRemark"] != null && ds.Tables[0].Rows[0]["AlbumRemark"].ToString() != "")
				{
					model.AlbumRemark = ds.Tables[0].Rows[0]["AlbumRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["AlbumID"] != null && ds.Tables[0].Rows[0]["AlbumID"].ToString() != "")
				{
					model.AlbumID = int.Parse(ds.Tables[0].Rows[0]["AlbumID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AlbumName"] != null && ds.Tables[0].Rows[0]["AlbumName"].ToString() != "")
				{
					model.AlbumName = ds.Tables[0].Rows[0]["AlbumName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["AlbumPhoto"] != null && ds.Tables[0].Rows[0]["AlbumPhoto"].ToString() != "")
				{
					model.AlbumPhoto = ds.Tables[0].Rows[0]["AlbumPhoto"].ToString();
				}
				if (ds.Tables[0].Rows[0]["AlbumDesc"] != null && ds.Tables[0].Rows[0]["AlbumDesc"].ToString() != "")
				{
					model.AlbumDesc = ds.Tables[0].Rows[0]["AlbumDesc"].ToString();
				}
				if (ds.Tables[0].Rows[0]["AlbumCreateTime"] != null && ds.Tables[0].Rows[0]["AlbumCreateTime"].ToString() != "")
				{
					model.AlbumCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["AlbumCreateTime"].ToString());
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
			strSql.Append("select AlbumID,AlbumName,AlbumPhoto,AlbumDesc,AlbumCreateTime ");
			strSql.Append(" FROM Album ");
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
			strSql.Append(" AlbumID,AlbumName,AlbumPhoto,AlbumDesc,AlbumCreateTime ");
			strSql.Append(" FROM Album ");
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
			strSql.Append("select count(1) FROM Album ");
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
				strSql.Append("order by T.AlbumID desc");
			}
			strSql.Append(")AS Row, T.*  from Album T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}
	}
}
