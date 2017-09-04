using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class SmsLog
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("SmsID", "SmsLog");
		}

		public bool Exists(int SmsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SmsLog");
			strSql.Append(" where SmsID=@SmsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SmsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = SmsID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SmsLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SmsLog(");
			strSql.Append("SmsMemID,SmsMobile,SmsContent,SmsShopID,SmsTime,SmsUserID,SmsAmount,SmsAllAmount)");
			strSql.Append(" values (");
			strSql.Append("@SmsMemID,@SmsMobile,@SmsContent,@SmsShopID,@SmsTime,@SmsUserID,@SmsAmount,@SmsAllAmount)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SmsMemID", SqlDbType.Int, 4),
				new SqlParameter("@SmsMobile", SqlDbType.VarChar, 50),
				new SqlParameter("@SmsContent", SqlDbType.NVarChar, 1000),
				new SqlParameter("@SmsShopID", SqlDbType.Int, 4),
				new SqlParameter("@SmsTime", SqlDbType.DateTime),
				new SqlParameter("@SmsUserID", SqlDbType.Int, 4),
				new SqlParameter("@SmsAmount", SqlDbType.Int, 4),
				new SqlParameter("@SmsAllAmount", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.SmsMemID;
			parameters[1].Value = model.SmsMobile;
			parameters[2].Value = model.SmsContent;
			parameters[3].Value = model.SmsShopID;
			parameters[4].Value = model.SmsTime;
			parameters[5].Value = model.SmsUserID;
			parameters[6].Value = model.SmsAmount;
			parameters[7].Value = model.SmsAllAmount;
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

		public bool Update(Chain.Model.SmsLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SmsLog set ");
			strSql.Append("SmsMemID=@SmsMemID,");
			strSql.Append("SmsMobile=@SmsMobile,");
			strSql.Append("SmsContent=@SmsContent,");
			strSql.Append("SmsShopID=@SmsShopID,");
			strSql.Append("SmsTime=@SmsTime,");
			strSql.Append("SmsUserID=@SmsUserID,");
			strSql.Append("SmsAmount=@SmsAmount,");
			strSql.Append("SmsAllAmount=@SmsAllAmount");
			strSql.Append(" where SmsID=@SmsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SmsMemID", SqlDbType.Int, 4),
				new SqlParameter("@SmsMobile", SqlDbType.VarChar, 50),
				new SqlParameter("@SmsContent", SqlDbType.NVarChar, 1000),
				new SqlParameter("@SmsShopID", SqlDbType.Int, 4),
				new SqlParameter("@SmsTime", SqlDbType.DateTime),
				new SqlParameter("@SmsUserID", SqlDbType.Int, 4),
				new SqlParameter("@SmsAmount", SqlDbType.Int, 4),
				new SqlParameter("@SmsAllAmount", SqlDbType.Int, 4),
				new SqlParameter("@SmsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.SmsMemID;
			parameters[1].Value = model.SmsMobile;
			parameters[2].Value = model.SmsContent;
			parameters[3].Value = model.SmsShopID;
			parameters[4].Value = model.SmsTime;
			parameters[5].Value = model.SmsUserID;
			parameters[6].Value = model.SmsAmount;
			parameters[7].Value = model.SmsAllAmount;
			parameters[8].Value = model.SmsID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int SmsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SmsLog ");
			strSql.Append(" where SmsID=@SmsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SmsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = SmsID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string SmsIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SmsLog ");
			strSql.Append(" where SmsID in (" + SmsIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SmsLog GetModel(int SmsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 SmsID,SmsMemID,SmsMobile,SmsContent,SmsShopID,SmsTime,SmsUserID,SmsAmount,SmsAllAmount from SmsLog ");
			strSql.Append(" where SmsID=@SmsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SmsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = SmsID;
			Chain.Model.SmsLog model = new Chain.Model.SmsLog();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SmsLog result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["SmsID"] != null && ds.Tables[0].Rows[0]["SmsID"].ToString() != "")
				{
					model.SmsID = int.Parse(ds.Tables[0].Rows[0]["SmsID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SmsMemID"] != null && ds.Tables[0].Rows[0]["SmsMemID"].ToString() != "")
				{
					model.SmsMemID = int.Parse(ds.Tables[0].Rows[0]["SmsMemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SmsMobile"] != null && ds.Tables[0].Rows[0]["SmsMobile"].ToString() != "")
				{
					model.SmsMobile = ds.Tables[0].Rows[0]["SmsMobile"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SmsContent"] != null && ds.Tables[0].Rows[0]["SmsContent"].ToString() != "")
				{
					model.SmsContent = ds.Tables[0].Rows[0]["SmsContent"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SmsShopID"] != null && ds.Tables[0].Rows[0]["SmsShopID"].ToString() != "")
				{
					model.SmsShopID = int.Parse(ds.Tables[0].Rows[0]["SmsShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SmsTime"] != null && ds.Tables[0].Rows[0]["SmsTime"].ToString() != "")
				{
					model.SmsTime = DateTime.Parse(ds.Tables[0].Rows[0]["SmsTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SmsUserID"] != null && ds.Tables[0].Rows[0]["SmsUserID"].ToString() != "")
				{
					model.SmsUserID = int.Parse(ds.Tables[0].Rows[0]["SmsUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SmsAmount"] != null && ds.Tables[0].Rows[0]["SmsAmount"].ToString() != "")
				{
					model.SmsAmount = int.Parse(ds.Tables[0].Rows[0]["SmsAmount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SmsAllAmount"] != null && ds.Tables[0].Rows[0]["SmsAllAmount"].ToString() != "")
				{
					model.SmsAllAmount = int.Parse(ds.Tables[0].Rows[0]["SmsAllAmount"].ToString());
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
			strSql.Append("select SmsID,SmsMemID,SmsMobile,SmsContent,SmsShopID,SmsTime,SmsUserID,SmsAmount,SmsAllAmount ");
			strSql.Append(" FROM SmsLog ");
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
			strSql.Append(" SmsID,SmsMemID,SmsMobile,SmsContent,SmsShopID,SmsTime,SmsUserID,SmsAmount,SmsAllAmount ");
			strSql.Append(" FROM SmsLog ");
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
			strSql.Append("select count(1) FROM SmsLog ");
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
				strSql.Append("order by T.SmsID desc");
			}
			strSql.Append(")AS Row, T.*  from SmsLog T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "SysShop,SmsLog,SysUser";
			string[] columns = new string[]
			{
				"SysShop.ShopName,SmsLog.*,SysUser.UserName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "SmsID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetSmsShopReport(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "V_SmsShopReport";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ShopID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int GetSmsMonthNumber(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select sum(SmsMonthNumber) FROM V_SmsShopReport ");
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

		public int GetSmsTotalNumber(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select sum(SmsTotalNumber) FROM V_SmsShopReport ");
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

		public DataSet GetSmsShopReportDetail(string strShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("SELECT ShopID,ShopName,SubString(Convert(varchar(30), SmsTime,23),0,8) as SmsYearMonth,sum(SmsAllAmount) AS SmsNumber");
			strSql.Append(" FROM SmsLog,SysShop");
			strSql.Append(" WHERE SysShop.ShopID = SmsLog.SmsShopID AND SmsShopID =" + strShopID);
			strSql.Append(" GROUP BY ShopID,ShopName,SubString(Convert(varchar(30), SmsTime,23),0,8) ORDER BY SubString(Convert(varchar(30), SmsTime,23),0,8) DESC");
			return DbHelperSQL.Query(strSql.ToString());
		}
	}
}
