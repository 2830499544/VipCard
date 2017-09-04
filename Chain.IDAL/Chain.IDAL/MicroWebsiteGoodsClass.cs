using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class MicroWebsiteGoodsClass
	{
		public DataSet GetGoodsClassInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "MicroWebsiteGoodsClass";
			string[] columns = new string[]
			{
				"MicroGoodsClassID,MicroGoodsClassName,MicroGoodsClassRemark,MicroGoodsClassShopID"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MicroGoodsClassID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("MicroGoodsClassID", "MicroWebsiteGoodsClass");
		}

		public bool Exists(int MicroGoodsClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MicroWebsiteGoodsClass");
			strSql.Append(" where MicroGoodsClassID=@MicroGoodsClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroGoodsClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MicroGoodsClassID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.MicroWebsiteGoodsClass model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MicroWebsiteGoodsClass(");
			strSql.Append("MicroGoodsClassName,MicroGoodsClassRemark,MicroGoodsClassShopID)");
			strSql.Append(" values (");
			strSql.Append("@MicroGoodsClassName,@MicroGoodsClassRemark,@MicroGoodsClassShopID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroGoodsClassName", SqlDbType.NVarChar, 100),
				new SqlParameter("@MicroGoodsClassRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@MicroGoodsClassShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MicroGoodsClassName;
			parameters[1].Value = model.MicroGoodsClassRemark;
			parameters[2].Value = model.MicroGoodsClassShopID;
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

		public bool Update(Chain.Model.MicroWebsiteGoodsClass model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MicroWebsiteGoodsClass set ");
			strSql.Append("MicroGoodsClassName=@MicroGoodsClassName,");
			strSql.Append("MicroGoodsClassRemark=@MicroGoodsClassRemark,");
			strSql.Append("MicroGoodsClassShopID=@MicroGoodsClassShopID");
			strSql.Append(" where MicroGoodsClassID=@MicroGoodsClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroGoodsClassName", SqlDbType.NVarChar, 100),
				new SqlParameter("@MicroGoodsClassRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@MicroGoodsClassShopID", SqlDbType.Int, 4),
				new SqlParameter("@MicroGoodsClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MicroGoodsClassName;
			parameters[1].Value = model.MicroGoodsClassRemark;
			parameters[2].Value = model.MicroGoodsClassShopID;
			parameters[3].Value = model.MicroGoodsClassID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int MicroGoodsClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MicroWebsiteGoodsClass ");
			strSql.Append(" where MicroGoodsClassID=@MicroGoodsClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroGoodsClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MicroGoodsClassID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string MicroGoodsClassIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MicroWebsiteGoodsClass ");
			strSql.Append(" where MicroGoodsClassID in (" + MicroGoodsClassIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MicroWebsiteGoodsClass GetModel(int MicroGoodsClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 MicroGoodsClassID,MicroGoodsClassName,MicroGoodsClassRemark,MicroGoodsClassShopID from MicroWebsiteGoodsClass ");
			strSql.Append(" where MicroGoodsClassID=@MicroGoodsClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroGoodsClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MicroGoodsClassID;
			Chain.Model.MicroWebsiteGoodsClass model = new Chain.Model.MicroWebsiteGoodsClass();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MicroWebsiteGoodsClass result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["MicroGoodsClassID"] != null && ds.Tables[0].Rows[0]["MicroGoodsClassID"].ToString() != "")
				{
					model.MicroGoodsClassID = int.Parse(ds.Tables[0].Rows[0]["MicroGoodsClassID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroGoodsClassName"] != null && ds.Tables[0].Rows[0]["MicroGoodsClassName"].ToString() != "")
				{
					model.MicroGoodsClassName = ds.Tables[0].Rows[0]["MicroGoodsClassName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MicroGoodsClassRemark"] != null && ds.Tables[0].Rows[0]["MicroGoodsClassRemark"].ToString() != "")
				{
					model.MicroGoodsClassRemark = ds.Tables[0].Rows[0]["MicroGoodsClassRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MicroGoodsClassShopID"] != null && ds.Tables[0].Rows[0]["MicroGoodsClassShopID"].ToString() != "")
				{
					model.MicroGoodsClassShopID = int.Parse(ds.Tables[0].Rows[0]["MicroGoodsClassShopID"].ToString());
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
			strSql.Append("select MicroGoodsClassID,MicroGoodsClassName,MicroGoodsClassRemark,MicroGoodsClassShopID ");
			strSql.Append(" FROM MicroWebsiteGoodsClass ");
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
			strSql.Append(" MicroGoodsClassID,MicroGoodsClassName,MicroGoodsClassRemark,MicroGoodsClassShopID ");
			strSql.Append(" FROM MicroWebsiteGoodsClass ");
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
			strSql.Append("select count(1) FROM MicroWebsiteGoodsClass ");
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
				strSql.Append("order by T.MicroGoodsClassID desc");
			}
			strSql.Append(")AS Row, T.*  from MicroWebsiteGoodsClass T ");
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
