using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysShopAuthority
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ShopAuthorityID", "SysShopAuthority");
		}

		public bool Exists(int ShopAuthorityID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysShopAuthority");
			strSql.Append(" where ShopAuthorityID=@ShopAuthorityID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopAuthorityID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ShopAuthorityID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysShopAuthority model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysShopAuthority(");
			strSql.Append("ShopAuthorityShopID,ShopAuthorityData)");
			strSql.Append(" values (");
			strSql.Append("@ShopAuthorityShopID,@ShopAuthorityData)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopAuthorityShopID", SqlDbType.Int, 4),
				new SqlParameter("@ShopAuthorityData", SqlDbType.VarChar, 4000)
			};
			parameters[0].Value = model.ShopAuthorityShopID;
			parameters[1].Value = model.ShopAuthorityData;
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

		public bool Update(Chain.Model.SysShopAuthority model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysShopAuthority set ");
			strSql.Append("ShopAuthorityShopID=@ShopAuthorityShopID,");
			strSql.Append("ShopAuthorityData=@ShopAuthorityData");
			strSql.Append(" where ShopAuthorityID=@ShopAuthorityID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopAuthorityShopID", SqlDbType.Int, 4),
				new SqlParameter("@ShopAuthorityData", SqlDbType.VarChar, 4000),
				new SqlParameter("@ShopAuthorityID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ShopAuthorityShopID;
			parameters[1].Value = model.ShopAuthorityData;
			parameters[2].Value = model.ShopAuthorityID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ShopAuthorityID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysShopAuthority ");
			strSql.Append(" where ShopAuthorityID=@ShopAuthorityID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopAuthorityID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ShopAuthorityID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string ShopAuthorityIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysShopAuthority ");
			strSql.Append(" where ShopAuthorityID in (" + ShopAuthorityIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysShopAuthority GetModel(int ShopAuthorityID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ShopAuthorityID,ShopAuthorityShopID,ShopAuthorityData from SysShopAuthority ");
			strSql.Append(" where ShopAuthorityID=@ShopAuthorityID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopAuthorityID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ShopAuthorityID;
			Chain.Model.SysShopAuthority model = new Chain.Model.SysShopAuthority();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysShopAuthority result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				result = this.DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Chain.Model.SysShopAuthority DataRowToModel(DataRow row)
		{
			Chain.Model.SysShopAuthority model = new Chain.Model.SysShopAuthority();
			if (row != null)
			{
				if (row["ShopAuthorityID"] != null && row["ShopAuthorityID"].ToString() != "")
				{
					model.ShopAuthorityID = int.Parse(row["ShopAuthorityID"].ToString());
				}
				if (row["ShopAuthorityShopID"] != null && row["ShopAuthorityShopID"].ToString() != "")
				{
					model.ShopAuthorityShopID = new int?(int.Parse(row["ShopAuthorityShopID"].ToString()));
				}
				if (row["ShopAuthorityData"] != null)
				{
					model.ShopAuthorityData = row["ShopAuthorityData"].ToString();
				}
			}
			return model;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ShopAuthorityID,ShopAuthorityShopID,ShopAuthorityData ");
			strSql.Append(" FROM SysShopAuthority ");
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
			strSql.Append(" ShopAuthorityID,ShopAuthorityShopID,ShopAuthorityData ");
			strSql.Append(" FROM SysShopAuthority ");
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
			strSql.Append("select count(1) FROM SysShopAuthority ");
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
				strSql.Append("order by T.ShopAuthorityID desc");
			}
			strSql.Append(")AS Row, T.*  from SysShopAuthority T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetShopAuthority(int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select * ");
			strSql.Append(" from SysShopAuthority");
			strSql.Append(" where ShopAuthorityShopID=" + ShopID);
			return DbHelperSQL.Query(strSql.ToString());
		}
	}
}
