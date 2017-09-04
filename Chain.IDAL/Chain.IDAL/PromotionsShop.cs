using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class PromotionsShop
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("PromotionsID", "PromotionsShop");
		}

		public DataSet GetGoodsClassInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "PromotionsShop";
			string[] columns = new string[]
			{
				"PromotionsID,ShopID,ClassRemark"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "PromotionsID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public bool Exists(int PromotionsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from PromotionsShop");
			strSql.Append(" where PromotionsID=@PromotionsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PromotionsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = PromotionsID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.PromotionsShop model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into PromotionsShop(");
			strSql.Append("PromotionsID,ShopID)");
			strSql.Append(" values (");
			strSql.Append("@PromotionsID,@ShopID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PromotionsID", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.PromotionsID;
			parameters[1].Value = model.ShopID;
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

		public bool Update(Chain.Model.PromotionsShop model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update PromotionsShop set ");
			strSql.Append("ClassName=@ClassName,");
			strSql.Append("ClassRemark=@ClassRemark");
			strSql.Append(" where PromotionsID=@PromotionsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassName", SqlDbType.NVarChar, 100),
				new SqlParameter("@ClassRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@ClassShopID", SqlDbType.Int, 4),
				new SqlParameter("@PromotionsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.PromotionsID;
			parameters[1].Value = model.PromotionsID;
			parameters[2].Value = model.PromotionsID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int PromotionsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from PromotionsShop ");
			strSql.Append(" where PromotionsID=@PromotionsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PromotionsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = PromotionsID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string PromotionsIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from PromotionsShop ");
			strSql.Append(" where PromotionsID in (" + PromotionsIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.PromotionsShop GetModel(int PromotionsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 PromotionsID,ClassName,ClassRemark from PromotionsShop ");
			strSql.Append(" where PromotionsID=@PromotionsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PromotionsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = PromotionsID;
			Chain.Model.PromotionsShop model = new Chain.Model.PromotionsShop();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.PromotionsShop result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["PromotionsID"] != null && ds.Tables[0].Rows[0]["PromotionsID"].ToString() != "")
				{
					model.PromotionsID = int.Parse(ds.Tables[0].Rows[0]["PromotionsID"].ToString());
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
			strSql.Append("select PromotionsID,ClassName,ClassRemark ");
			strSql.Append(" FROM PromotionsShop ");
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
			strSql.Append(" PromotionsID,ClassName,ClassRemark ");
			strSql.Append(" FROM PromotionsShop ");
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
			strSql.Append("select count(1) FROM PromotionsShop ");
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
				strSql.Append("order by T.PromotionsID desc");
			}
			strSql.Append(")AS Row, T.*  from PromotionsShop T ");
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
