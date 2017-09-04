using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class TimingCategory
	{
		public bool Exists(int CategoryID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from TimingCategory");
			strSql.Append(" where CategoryID=@CategoryID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CategoryID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CategoryID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.TimingCategory model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into TimingCategory(");
			strSql.Append("CategoryName,CategoryFatherID,CategoryShopID,CategoryUserID,CategoryrRemark)");
			strSql.Append(" values (");
			strSql.Append("@CategoryName,@CategoryFatherID,@CategoryShopID,@CategoryUserID,@CategoryrRemark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CategoryName", SqlDbType.NVarChar, 50),
				new SqlParameter("@CategoryFatherID", SqlDbType.Int, 4),
				new SqlParameter("@CategoryShopID", SqlDbType.Int, 4),
				new SqlParameter("@CategoryUserID", SqlDbType.Int, 4),
				new SqlParameter("@CategoryrRemark", SqlDbType.NVarChar, 200)
			};
			parameters[0].Value = model.CategoryName;
			parameters[1].Value = model.CategoryFatherID;
			parameters[2].Value = model.CategoryShopID;
			parameters[3].Value = model.CategoryUserID;
			parameters[4].Value = model.CategoryrRemark;
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

		public bool Update(Chain.Model.TimingCategory model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update TimingCategory set ");
			strSql.Append("CategoryName=@CategoryName,");
			strSql.Append("CategoryFatherID=@CategoryFatherID,");
			strSql.Append("CategoryShopID=@CategoryShopID,");
			strSql.Append("CategoryUserID=@CategoryUserID,");
			strSql.Append("CategoryrRemark=@CategoryrRemark");
			strSql.Append(" where CategoryID=@CategoryID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CategoryName", SqlDbType.NVarChar, 50),
				new SqlParameter("@CategoryFatherID", SqlDbType.Int, 4),
				new SqlParameter("@CategoryShopID", SqlDbType.Int, 4),
				new SqlParameter("@CategoryUserID", SqlDbType.Int, 4),
				new SqlParameter("@CategoryID", SqlDbType.Int, 4),
				new SqlParameter("@CategoryrRemark", SqlDbType.NVarChar, 200)
			};
			parameters[0].Value = model.CategoryName;
			parameters[1].Value = model.CategoryFatherID;
			parameters[2].Value = model.CategoryShopID;
			parameters[3].Value = model.CategoryUserID;
			parameters[4].Value = model.CategoryID;
			parameters[5].Value = model.CategoryrRemark;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int CategoryID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from TimingCategory ");
			strSql.Append(" where CategoryID=@CategoryID OR CategoryFatherID = @CategoryFatherID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CategoryID", SqlDbType.Int, 4),
				new SqlParameter("@CategoryFatherID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CategoryID;
			parameters[1].Value = CategoryID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string CategoryIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from TimingCategory ");
			strSql.Append(" where CategoryID in (" + CategoryIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.TimingCategory GetModel(int CategoryID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 CategoryID,CategoryName,CategoryFatherID,CategoryShopID,CategoryUserID,CategoryrRemark from TimingCategory ");
			strSql.Append(" where CategoryID=@CategoryID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CategoryID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CategoryID;
			Chain.Model.TimingCategory model = new Chain.Model.TimingCategory();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.TimingCategory result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["CategoryID"] != null && ds.Tables[0].Rows[0]["CategoryID"].ToString() != "")
				{
					model.CategoryID = int.Parse(ds.Tables[0].Rows[0]["CategoryID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CategoryName"] != null && ds.Tables[0].Rows[0]["CategoryName"].ToString() != "")
				{
					model.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CategoryFatherID"] != null && ds.Tables[0].Rows[0]["CategoryFatherID"].ToString() != "")
				{
					model.CategoryFatherID = int.Parse(ds.Tables[0].Rows[0]["CategoryFatherID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CategoryShopID"] != null && ds.Tables[0].Rows[0]["CategoryShopID"].ToString() != "")
				{
					model.CategoryShopID = int.Parse(ds.Tables[0].Rows[0]["CategoryShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CategoryUserID"] != null && ds.Tables[0].Rows[0]["CategoryUserID"].ToString() != "")
				{
					model.CategoryUserID = int.Parse(ds.Tables[0].Rows[0]["CategoryUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CategoryrRemark"] != null && ds.Tables[0].Rows[0]["CategoryrRemark"].ToString() != "")
				{
					model.CategoryrRemark = ds.Tables[0].Rows[0]["CategoryrRemark"].ToString();
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
			strSql.Append("select CategoryID,CategoryName,CategoryFatherID,CategoryShopID,CategoryUserID,CategoryrRemark ");
			strSql.Append(" FROM TimingCategory ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetNifo(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("SELECT TimingCategory.*,SysUser.UserName FROM TimingCategory INNER JOIN SysUser ON TimingCategory.CategoryUserID = SysUser.UserID");
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
			strSql.Append(" CategoryID,CategoryName,CategoryFatherID,CategoryShopID,CategoryUserID,CategoryrRemark ");
			strSql.Append(" FROM TimingCategory ");
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
			strSql.Append("select count(1) FROM TimingCategory ");
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
				strSql.Append("order by T.CategoryID desc");
			}
			strSql.Append(")AS Row, T.*  from TimingCategory T ");
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
