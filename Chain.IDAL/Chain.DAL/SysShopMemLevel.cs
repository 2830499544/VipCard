using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class SysShopMemLevel
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ShopMemLevelID", "SysShopMemLevel");
		}

		public bool Exists(int ShopMemLevelID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysShopMemLevel");
			strSql.Append(" where ShopMemLevelID=@ShopMemLevelID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopMemLevelID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ShopMemLevelID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysShopMemLevel model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysShopMemLevel(");
			strSql.Append("ShopID,MemLevelID,ClassDiscountPercent,ClassPointPercent,ClassRechargePointRate)");
			strSql.Append(" values (");
			strSql.Append("@ShopID,@MemLevelID,@ClassDiscountPercent,@ClassPointPercent,@ClassRechargePointRate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopID", SqlDbType.Int, 4),
				new SqlParameter("@MemLevelID", SqlDbType.Int, 4),
				new SqlParameter("@ClassDiscountPercent", SqlDbType.Money, 8),
				new SqlParameter("@ClassPointPercent", SqlDbType.Money, 8),
				new SqlParameter("@ClassRechargePointRate", SqlDbType.Money, 8)
			};
			parameters[0].Value = model.ShopID;
			parameters[1].Value = model.MemLevelID;
			parameters[2].Value = model.ClassDiscountPercent;
			parameters[3].Value = model.ClassPointPercent;
			parameters[4].Value = model.ClassRechargePointRate;
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

		public bool Update(Chain.Model.SysShopMemLevel model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysShopMemLevel set ");
			strSql.Append("ShopID=@ShopID,");
			strSql.Append("MemLevelID=@MemLevelID,");
			strSql.Append("ClassDiscountPercent=@ClassDiscountPercent,");
			strSql.Append("ClassPointPercent=@ClassPointPercent,");
			strSql.Append("ClassRechargePointRate=@ClassRechargePointRate");
			strSql.Append(" where ShopMemLevelID=@ShopMemLevelID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopID", SqlDbType.Int, 4),
				new SqlParameter("@MemLevelID", SqlDbType.Int, 4),
				new SqlParameter("@ClassDiscountPercent", SqlDbType.Money, 8),
				new SqlParameter("@ClassPointPercent", SqlDbType.Money, 8),
				new SqlParameter("@ClassRechargePointRate", SqlDbType.Money, 8),
				new SqlParameter("@ShopMemLevelID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ShopID;
			parameters[1].Value = model.MemLevelID;
			parameters[2].Value = model.ClassDiscountPercent;
			parameters[3].Value = model.ClassPointPercent;
			parameters[4].Value = model.ClassRechargePointRate;
			parameters[5].Value = model.ShopMemLevelID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ShopMemLevelID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysShopMemLevel ");
			strSql.Append(" where ShopMemLevelID=@ShopMemLevelID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopMemLevelID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ShopMemLevelID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string ShopMemLevelIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysShopMemLevel ");
			strSql.Append(" where ShopMemLevelID in (" + ShopMemLevelIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysShopMemLevel GetModel(int ShopMemLevelID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ShopMemLevelID,ShopID,MemLevelID,ClassDiscountPercent,ClassPointPercent,ClassRechargePointRate from SysShopMemLevel ");
			strSql.Append(" where ShopMemLevelID=@ShopMemLevelID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopMemLevelID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ShopMemLevelID;
			return SysShopMemLevel.NewMethod(strSql, parameters);
		}

		public Chain.Model.SysShopMemLevel GetModel(int MemLevelID, int shopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ShopMemLevelID,ShopID,MemLevelID,ClassDiscountPercent,ClassPointPercent,ClassRechargePointRate from SysShopMemLevel ");
			strSql.Append(" where MemLevelID=@MemLevelID and ShopID = @ShopID ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemLevelID", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MemLevelID;
			parameters[1].Value = shopID;
			return SysShopMemLevel.NewMethod(strSql, parameters);
		}

		private static Chain.Model.SysShopMemLevel NewMethod(StringBuilder strSql, SqlParameter[] parameters)
		{
			Chain.Model.SysShopMemLevel model = new Chain.Model.SysShopMemLevel();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysShopMemLevel result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ShopMemLevelID"] != null && ds.Tables[0].Rows[0]["ShopMemLevelID"].ToString() != "")
				{
					model.ShopMemLevelID = int.Parse(ds.Tables[0].Rows[0]["ShopMemLevelID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ShopID"] != null && ds.Tables[0].Rows[0]["ShopID"].ToString() != "")
				{
					model.ShopID = int.Parse(ds.Tables[0].Rows[0]["ShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemLevelID"] != null && ds.Tables[0].Rows[0]["MemLevelID"].ToString() != "")
				{
					model.MemLevelID = int.Parse(ds.Tables[0].Rows[0]["MemLevelID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ClassDiscountPercent"] != null && ds.Tables[0].Rows[0]["ClassDiscountPercent"].ToString() != "")
				{
					model.ClassDiscountPercent = decimal.Parse(ds.Tables[0].Rows[0]["ClassDiscountPercent"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ClassPointPercent"] != null && ds.Tables[0].Rows[0]["ClassPointPercent"].ToString() != "")
				{
					model.ClassPointPercent = decimal.Parse(ds.Tables[0].Rows[0]["ClassPointPercent"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ClassRechargePointRate"] != null && ds.Tables[0].Rows[0]["ClassRechargePointRate"].ToString() != "")
				{
					model.ClassRechargePointRate = decimal.Parse(ds.Tables[0].Rows[0]["ClassRechargePointRate"].ToString());
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
			strSql.Append("select ShopMemLevelID,ShopID,MemLevelID,ClassDiscountPercent,ClassPointPercent,ClassRechargePointRate ");
			strSql.Append(" FROM SysShopMemLevel ");
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
			strSql.Append(" ShopMemLevelID,ShopID,MemLevelID,ClassDiscountPercent,ClassPointPercent,ClassRechargePointRate ");
			strSql.Append(" FROM SysShopMemLevel ");
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
			strSql.Append("select count(1) FROM SysShopMemLevel ");
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
				strSql.Append("order by T.ShopMemLevelID desc");
			}
			strSql.Append(")AS Row, T.*  from SysShopMemLevel T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int GetRechargePointRate(int memLevelID, int shopID)
		{
			string sql = "select ClassRechargePointRate from SysShopMemLevel where MemLevelID = @MemLevelID and ShopID = @ShopID";
			SqlParameter[] par = new SqlParameter[]
			{
				new SqlParameter("@MemLevelID", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			par[0].Value = memLevelID;
			par[1].Value = shopID;
			int result;
			try
			{
				SqlDataReader dr = DbHelperSQL.ExecuteReader(sql, par);
				if (dr.HasRows)
				{
					dr.Read();
					result = dr.GetInt32(dr.GetOrdinal("ClassRechargePointRate"));
				}
				else
				{
					result = 0;
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		public bool DeleteByMemLevelID(int MemLevelID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysShopMemLevel ");
			strSql.Append(" where MemLevelID=@MemLevelID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemLevelID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MemLevelID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}
	}
}
