using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class StaffClass
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ClassID", "StaffClass");
		}

		public bool Exists(int ClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from StaffClass");
			strSql.Append(" where ClassID=@ClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(int intClassID, string strClassName, int ShopID)
		{
			string strSql = "select ClassName from StaffClass where ClassID not in ({0}) and (ClassName = '{1}') and ClassShopID = '{2}'";
			strSql = string.Format(strSql, intClassID, strClassName, ShopID);
			DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
			return dt.Rows.Count > 0;
		}

		public int Add(Chain.Model.StaffClass model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into StaffClass(");
			strSql.Append("ClassName,ClassType,ClassPercent,ClassShopID,ClassRemark)");
			strSql.Append(" values (");
			strSql.Append("@ClassName,@ClassType,@ClassPercent,@ClassShopID,@ClassRemark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassName", SqlDbType.NVarChar, 50),
				new SqlParameter("@ClassType", SqlDbType.Bit, 1),
				new SqlParameter("@ClassPercent", SqlDbType.Float, 8),
				new SqlParameter("@ClassShopID", SqlDbType.Int, 4),
				new SqlParameter("@ClassRemark", SqlDbType.NVarChar, 500)
			};
			parameters[0].Value = model.ClassName;
			parameters[1].Value = model.ClassType;
			parameters[2].Value = model.ClassPercent;
			parameters[3].Value = model.ClassShopID;
			parameters[4].Value = model.ClassRemark;
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

		public int Update(Chain.Model.StaffClass model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update StaffClass set ");
			strSql.Append("ClassName=@ClassName,");
			strSql.Append("ClassType=@ClassType,");
			strSql.Append("ClassPercent=@ClassPercent,");
			strSql.Append("ClassShopID=@ClassShopID,");
			strSql.Append("ClassRemark=@ClassRemark");
			strSql.Append(" where ClassID=@ClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassName", SqlDbType.NVarChar, 50),
				new SqlParameter("@ClassType", SqlDbType.Bit, 1),
				new SqlParameter("@ClassPercent", SqlDbType.Float, 8),
				new SqlParameter("@ClassShopID", SqlDbType.Int, 4),
				new SqlParameter("@ClassRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@ClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ClassName;
			parameters[1].Value = model.ClassType;
			parameters[2].Value = model.ClassPercent;
			parameters[3].Value = model.ClassShopID;
			parameters[4].Value = model.ClassRemark;
			parameters[5].Value = model.ClassID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public int Delete(int ClassID)
		{
			string strSelectSql = "select StaffID,staffClassID from Staff where staffClassID=" + ClassID;
			DataTable dt = DbHelperSQL.Query(strSelectSql).Tables[0];
			int result;
			if (dt.Rows.Count > 0)
			{
				result = -1;
			}
			else
			{
				StringBuilder strSql = new StringBuilder();
				strSql.Append("delete from StaffClass ");
				strSql.Append(" where ClassID=@ClassID");
				SqlParameter[] parameters = new SqlParameter[]
				{
					new SqlParameter("@ClassID", SqlDbType.Int, 4)
				};
				parameters[0].Value = ClassID;
				result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			}
			return result;
		}

		public bool DeleteList(string ClassIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from StaffClass ");
			strSql.Append(" where ClassID in (" + ClassIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.StaffClass GetModel(int ClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ClassID,ClassName,ClassType,ClassPercent,ClassShopID,ClassRemark from StaffClass ");
			strSql.Append(" where ClassID=@ClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassID;
			Chain.Model.StaffClass model = new Chain.Model.StaffClass();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.StaffClass result;
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
				if (ds.Tables[0].Rows[0]["ClassType"] != null && ds.Tables[0].Rows[0]["ClassType"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["ClassType"].ToString() == "1" || ds.Tables[0].Rows[0]["ClassType"].ToString().ToLower() == "true")
					{
						model.ClassType = true;
					}
					else
					{
						model.ClassType = false;
					}
				}
				if (ds.Tables[0].Rows[0]["ClassPercent"] != null && ds.Tables[0].Rows[0]["ClassPercent"].ToString() != "")
				{
					model.ClassPercent = decimal.Parse(ds.Tables[0].Rows[0]["ClassPercent"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ClassShopID"] != null && ds.Tables[0].Rows[0]["ClassShopID"].ToString() != "")
				{
					model.ClassShopID = int.Parse(ds.Tables[0].Rows[0]["ClassShopID"].ToString());
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
			strSql.Append("select ClassID,ClassName,ClassType,ClassPercent,ClassShopID,ShopName,ClassRemark ");
			strSql.Append(" FROM StaffClass,SysShop ");
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
			strSql.Append(" ClassID,ClassName,ClassType,ClassPercent,ClassShopID,ClassRemark ");
			strSql.Append(" FROM StaffClass ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = " StaffClass,SysShop ";
			string[] columns = new string[]
			{
				" * "
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ClassShopID", "ClassID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) FROM StaffClass ");
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
			strSql.Append(")AS Row, T.*  from StaffClass T ");
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
