using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class Photo
	{
		public DataSet GetPhotoInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "Photo,SysUser,Album";
			string[] columns = new string[]
			{
				"PhotoID,PhotoName,PhotoPhoto,PhotoDesc,PhotoCreateTime,UserName,PhotoRemark,AlbumName,Album.AlbumID"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "PhotoID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public bool Exists(int PhotoID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from Photo");
			strSql.Append(" where PhotoID=@PhotoID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PhotoID", SqlDbType.Int, 4)
			};
			parameters[0].Value = PhotoID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.Photo model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Photo(");
			strSql.Append("AlbumID,CreateUserID,PhotoRemark,PhotoName,PhotoPhoto,PhotoDesc,PhotoCreateTime)");
			strSql.Append(" values (");
			strSql.Append("@AlbumID,@CreateUserID,@PhotoRemark,@PhotoName,@PhotoPhoto,@PhotoDesc,@PhotoCreateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PhotoName", SqlDbType.NVarChar, 50),
				new SqlParameter("@PhotoPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@PhotoDesc", SqlDbType.NVarChar, 4000),
				new SqlParameter("@PhotoCreateTime", SqlDbType.DateTime),
				new SqlParameter("@PhotoRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@CreateUserID", SqlDbType.Int, 4),
				new SqlParameter("@AlbumID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.PhotoName;
			parameters[1].Value = model.PhotoPhoto;
			parameters[2].Value = model.PhotoDesc;
			parameters[3].Value = model.PhotoCreateTime;
			parameters[4].Value = model.PhotoRemark;
			parameters[5].Value = model.CreateUserID;
			parameters[6].Value = model.AlbumID;
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

		public bool Update(Chain.Model.Photo model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update Photo set ");
			strSql.Append("AlbumID=@AlbumID,");
			strSql.Append("PhotoRemark=@PhotoRemark,");
			strSql.Append("PhotoName=@PhotoName,");
			strSql.Append("PhotoPhoto=@PhotoPhoto,");
			strSql.Append("PhotoDesc=@PhotoDesc,");
			strSql.Append("PhotoCreateTime=@PhotoCreateTime");
			strSql.Append(" where PhotoID=@PhotoID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PhotoName", SqlDbType.NVarChar, 50),
				new SqlParameter("@PhotoPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@PhotoDesc", SqlDbType.NVarChar, 4000),
				new SqlParameter("@PhotoCreateTime", SqlDbType.DateTime),
				new SqlParameter("@PhotoRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@PhotoID", SqlDbType.Int, 4),
				new SqlParameter("@AlbumID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.PhotoName;
			parameters[1].Value = model.PhotoPhoto;
			parameters[2].Value = model.PhotoDesc;
			parameters[3].Value = model.PhotoCreateTime;
			parameters[4].Value = model.PhotoRemark;
			parameters[5].Value = model.PhotoID;
			parameters[6].Value = model.AlbumID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int PhotoID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Photo ");
			strSql.Append(" where PhotoID=@PhotoID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PhotoID", SqlDbType.Int, 4)
			};
			parameters[0].Value = PhotoID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string PhotoIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Photo ");
			strSql.Append(" where PhotoID in (" + PhotoIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.Photo GetModel(int PhotoID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 AlbumID,PhotoRemark, PhotoID,PhotoName,PhotoPhoto,PhotoDesc,PhotoCreateTime from Photo ");
			strSql.Append(" where PhotoID=@PhotoID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PhotoID", SqlDbType.Int, 4)
			};
			parameters[0].Value = PhotoID;
			Chain.Model.Photo model = new Chain.Model.Photo();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.Photo result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["AlbumID"] != null && ds.Tables[0].Rows[0]["AlbumID"].ToString() != "")
				{
					model.AlbumID = int.Parse(ds.Tables[0].Rows[0]["AlbumID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PhotoRemark"] != null && ds.Tables[0].Rows[0]["PhotoRemark"].ToString() != "")
				{
					model.PhotoRemark = ds.Tables[0].Rows[0]["PhotoRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PhotoID"] != null && ds.Tables[0].Rows[0]["PhotoID"].ToString() != "")
				{
					model.PhotoID = int.Parse(ds.Tables[0].Rows[0]["PhotoID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PhotoName"] != null && ds.Tables[0].Rows[0]["PhotoName"].ToString() != "")
				{
					model.PhotoName = ds.Tables[0].Rows[0]["PhotoName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PhotoPhoto"] != null && ds.Tables[0].Rows[0]["PhotoPhoto"].ToString() != "")
				{
					model.PhotoPhoto = ds.Tables[0].Rows[0]["PhotoPhoto"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PhotoDesc"] != null && ds.Tables[0].Rows[0]["PhotoDesc"].ToString() != "")
				{
					model.PhotoDesc = ds.Tables[0].Rows[0]["PhotoDesc"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PhotoCreateTime"] != null && ds.Tables[0].Rows[0]["PhotoCreateTime"].ToString() != "")
				{
					model.PhotoCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["PhotoCreateTime"].ToString());
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
			strSql.Append("select PhotoID,PhotoName,PhotoPhoto,PhotoDesc,PhotoCreateTime ");
			strSql.Append(" FROM Photo ");
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
			strSql.Append(" PhotoID,PhotoName,PhotoPhoto,PhotoDesc,PhotoCreateTime ");
			strSql.Append(" FROM Photo ");
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
			strSql.Append("select count(1) FROM Photo ");
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
				strSql.Append("order by T.PhotoID desc");
			}
			strSql.Append(")AS Row, T.*  from Photo T ");
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
