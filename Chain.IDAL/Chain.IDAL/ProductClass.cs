using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class ProductClass
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ClassID", "ProductClass");
		}

		public DataSet GetGoodsClassInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "ProductClass";
			string[] columns = new string[]
			{
				"ClassID,ClassName,ClassRemark"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ClassID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public bool Exists(int ClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from ProductClass");
			strSql.Append(" where ClassID=@ClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.ProductClass model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into ProductClass(");
			strSql.Append("ClassName,ClassRemark)");
			strSql.Append(" values (");
			strSql.Append("@ClassName,@ClassRemark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassName", SqlDbType.NVarChar, 100),
				new SqlParameter("@ClassRemark", SqlDbType.NVarChar, 200)
			};
			parameters[0].Value = model.ClassName;
			parameters[1].Value = model.ClassRemark;
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

		public bool Update(Chain.Model.ProductClass model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update ProductClass set ");
			strSql.Append("ClassName=@ClassName,");
			strSql.Append("ClassRemark=@ClassRemark");
			strSql.Append(" where ClassID=@ClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassName", SqlDbType.NVarChar, 100),
				new SqlParameter("@ClassRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@ClassShopID", SqlDbType.Int, 4),
				new SqlParameter("@ClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ClassName;
			parameters[1].Value = model.ClassRemark;
			parameters[2].Value = model.ClassID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from ProductClass ");
			strSql.Append(" where ClassID=@ClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string ClassIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from ProductClass ");
			strSql.Append(" where ClassID in (" + ClassIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.ProductClass GetModel(int ClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ClassID,ClassName,ClassRemark from ProductClass ");
			strSql.Append(" where ClassID=@ClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassID;
			Chain.Model.ProductClass model = new Chain.Model.ProductClass();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.ProductClass result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ClassID"] != null && ds.Tables[0].Rows[0]["ClassID"].ToString() != "")
				{
					model.ClassID = int.Parse(ds.Tables[0].Rows[0]["ClassID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ClassName"] != null && ds.Tables[0].Rows[0]["ClassName"].ToString() != "")
				{
					model.ClassName = ds.Tables[0].Rows[0]["ClassName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ClassRemark"] != null && ds.Tables[0].Rows[0]["ClassRemark"].ToString() != "")
				{
					model.ClassRemark = ds.Tables[0].Rows[0]["ClassRemark"].ToString();
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
			strSql.Append("select ClassID,ClassName,ClassRemark ");
			strSql.Append(" FROM ProductClass ");
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
			strSql.Append(" ClassID,ClassName,ClassRemark ");
			strSql.Append(" FROM ProductClass ");
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
			strSql.Append("select count(1) FROM ProductClass ");
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
				strSql.Append("order by T.ClassID desc");
			}
			strSql.Append(")AS Row, T.*  from ProductClass T ");
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
