using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class ProductCenter
	{
		public bool Exists(int ProductID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from ProductCenter");
			strSql.Append(" where ProductID=@ProductID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProductID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ProductID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.ProductCenter model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into ProductCenter(");
			strSql.Append("ClassID,CreateUserID,ProductRemark,ProductName,ProductPhoto,ProductDesc,ProductCreateTime)");
			strSql.Append(" values (");
			strSql.Append("@ClassID,@CreateUserID,@ProductRemark,@ProductName,@ProductPhoto,@ProductDesc,@ProductCreateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProductName", SqlDbType.NVarChar, 50),
				new SqlParameter("@ProductPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@ProductDesc", SqlDbType.NVarChar, 4000),
				new SqlParameter("@ProductCreateTime", SqlDbType.DateTime),
				new SqlParameter("@ProductRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@CreateUserID", SqlDbType.Int, 4),
				new SqlParameter("@ClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ProductName;
			parameters[1].Value = model.ProductPhoto;
			parameters[2].Value = model.ProductDesc;
			parameters[3].Value = model.ProductCreateTime;
			parameters[4].Value = model.ProductRemark;
			parameters[5].Value = model.CreateUserID;
			parameters[6].Value = model.ClassID;
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

		public bool Update(Chain.Model.ProductCenter model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update ProductCenter set ");
			strSql.Append("ClassID=@ClassID,");
			strSql.Append("ProductRemark=@ProductRemark,");
			strSql.Append("ProductName=@ProductName,");
			strSql.Append("ProductPhoto=@ProductPhoto,");
			strSql.Append("ProductDesc=@ProductDesc,");
			strSql.Append("ProductCreateTime=@ProductCreateTime");
			strSql.Append(" where ProductID=@ProductID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProductName", SqlDbType.NVarChar, 50),
				new SqlParameter("@ProductPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@ProductDesc", SqlDbType.NVarChar, 4000),
				new SqlParameter("@ProductCreateTime", SqlDbType.DateTime),
				new SqlParameter("@ProductRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@ProductID", SqlDbType.Int, 4),
				new SqlParameter("@ClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ProductName;
			parameters[1].Value = model.ProductPhoto;
			parameters[2].Value = model.ProductDesc;
			parameters[3].Value = model.ProductCreateTime;
			parameters[4].Value = model.ProductRemark;
			parameters[5].Value = model.ProductID;
			parameters[6].Value = model.ClassID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ProductID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from ProductCenter ");
			strSql.Append(" where ProductID=@ProductID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProductID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ProductID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string ProductIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from ProductCenter ");
			strSql.Append(" where ProductID in (" + ProductIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.ProductCenter GetModel(int ProductID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ClassID,ProductRemark, ProductID,ProductName,ProductPhoto,ProductDesc,ProductCreateTime from ProductCenter ");
			strSql.Append(" where ProductID=@ProductID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProductID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ProductID;
			Chain.Model.ProductCenter model = new Chain.Model.ProductCenter();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.ProductCenter result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ClassID"] != null && ds.Tables[0].Rows[0]["ClassID"].ToString() != "")
				{
					model.ClassID = int.Parse(ds.Tables[0].Rows[0]["ClassID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ProductRemark"] != null && ds.Tables[0].Rows[0]["ProductRemark"].ToString() != "")
				{
					model.ProductRemark = ds.Tables[0].Rows[0]["ProductRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ProductID"] != null && ds.Tables[0].Rows[0]["ProductID"].ToString() != "")
				{
					model.ProductID = int.Parse(ds.Tables[0].Rows[0]["ProductID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ProductName"] != null && ds.Tables[0].Rows[0]["ProductName"].ToString() != "")
				{
					model.ProductName = ds.Tables[0].Rows[0]["ProductName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ProductPhoto"] != null && ds.Tables[0].Rows[0]["ProductPhoto"].ToString() != "")
				{
					model.ProductPhoto = ds.Tables[0].Rows[0]["ProductPhoto"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ProductDesc"] != null && ds.Tables[0].Rows[0]["ProductDesc"].ToString() != "")
				{
					model.ProductDesc = ds.Tables[0].Rows[0]["ProductDesc"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ProductCreateTime"] != null && ds.Tables[0].Rows[0]["ProductCreateTime"].ToString() != "")
				{
					model.ProductCreateTime = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["ProductCreateTime"].ToString()));
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
			strSql.Append("select ProductID,ProductName,ProductPhoto,ProductDesc,ProductCreateTime ");
			strSql.Append(" FROM ProductCenter ");
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
			strSql.Append(" ProductID,ProductName,ProductPhoto,ProductDesc,ProductCreateTime ");
			strSql.Append(" FROM ProductCenter ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by " + filedOrder + " desc");
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) FROM ProductCenter ");
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
				strSql.Append("order by T.ProductID desc");
			}
			strSql.Append(")AS Row, T.*  from ProductCenter T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetProductCenterInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "ProductCenter,SysUser,ProductClass";
			string[] columns = new string[]
			{
				"ProductID,ProductName,ProductPhoto,ProductDesc,ProductCreateTime,UserName,ProductRemark,ClassName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ProductID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
