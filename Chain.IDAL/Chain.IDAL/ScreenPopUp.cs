using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class ScreenPopUp
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("CallerID", "ScreenPopUp");
		}

		public bool Exists(int CallerID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from ScreenPopUp");
			strSql.Append(" where CallerID=@CallerID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CallerID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CallerID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.ScreenPopUp model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into ScreenPopUp(");
			strSql.Append("CallerMemID,CallerMobile,CallerIsMem,CallerState,CallerDuration,CallerRemark,CallerCreateTime,CallerUserID,CallerShopID)");
			strSql.Append(" values (");
			strSql.Append("@CallerMemID,@CallerMobile,@CallerIsMem,@CallerState,@CallerDuration,@CallerRemark,@CallerCreateTime,@CallerUserID,@CallerShopID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CallerMemID", SqlDbType.Int, 4),
				new SqlParameter("@CallerMobile", SqlDbType.VarChar, 50),
				new SqlParameter("@CallerIsMem", SqlDbType.NVarChar, 50),
				new SqlParameter("@CallerState", SqlDbType.NVarChar, 50),
				new SqlParameter("@CallerDuration", SqlDbType.VarChar, 50),
				new SqlParameter("@CallerRemark", SqlDbType.VarChar, 500),
				new SqlParameter("@CallerCreateTime", SqlDbType.DateTime),
				new SqlParameter("@CallerUserID", SqlDbType.Int, 4),
				new SqlParameter("@CallerShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.CallerMemID;
			parameters[1].Value = model.CallerMobile;
			parameters[2].Value = model.CallerIsMem;
			parameters[3].Value = model.CallerState;
			parameters[4].Value = model.CallerDuration;
			parameters[5].Value = model.CallerRemark;
			parameters[6].Value = model.CallerCreateTime;
			parameters[7].Value = model.CallerUserID;
			parameters[8].Value = model.CallerShopID;
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

		public bool Update(Chain.Model.ScreenPopUp model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update ScreenPopUp set ");
			strSql.Append("CallerMemID=@CallerMemID,");
			strSql.Append("CallerMobile=@CallerMobile,");
			strSql.Append("CallerIsMem=@CallerIsMem,");
			strSql.Append("CallerState=@CallerState,");
			strSql.Append("CallerDuration=@CallerDuration,");
			strSql.Append("CallerRemark=@CallerRemark,");
			strSql.Append("CallerCreateTime=@CallerCreateTime,");
			strSql.Append("CallerUserID=@CallerUserID,");
			strSql.Append("CallerShopID=@CallerShopID");
			strSql.Append(" where CallerID=@CallerID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CallerMemID", SqlDbType.Int, 4),
				new SqlParameter("@CallerMobile", SqlDbType.VarChar, 50),
				new SqlParameter("@CallerIsMem", SqlDbType.NVarChar, 50),
				new SqlParameter("@CallerState", SqlDbType.NVarChar, 50),
				new SqlParameter("@CallerDuration", SqlDbType.VarChar, 50),
				new SqlParameter("@CallerRemark", SqlDbType.VarChar, 500),
				new SqlParameter("@CallerCreateTime", SqlDbType.DateTime),
				new SqlParameter("@CallerUserID", SqlDbType.Int, 4),
				new SqlParameter("@CallerShopID", SqlDbType.Int, 4),
				new SqlParameter("@CallerID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.CallerMemID;
			parameters[1].Value = model.CallerMobile;
			parameters[2].Value = model.CallerIsMem;
			parameters[3].Value = model.CallerState;
			parameters[4].Value = model.CallerDuration;
			parameters[5].Value = model.CallerRemark;
			parameters[6].Value = model.CallerCreateTime;
			parameters[7].Value = model.CallerUserID;
			parameters[8].Value = model.CallerShopID;
			parameters[9].Value = model.CallerID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int CallerID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from ScreenPopUp ");
			strSql.Append(" where CallerID=@CallerID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CallerID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CallerID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string CallerIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from ScreenPopUp ");
			strSql.Append(" where CallerID in (" + CallerIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.ScreenPopUp GetModel(int CallerID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 CallerID,CallerMemID,CallerMobile,CallerIsMem,CallerState,CallerDuration,CallerRemark,CallerCreateTime,CallerUserID,CallerShopID from ScreenPopUp ");
			strSql.Append(" where CallerID=@CallerID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CallerID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CallerID;
			Chain.Model.ScreenPopUp model = new Chain.Model.ScreenPopUp();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.ScreenPopUp result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["CallerID"] != null && ds.Tables[0].Rows[0]["CallerID"].ToString() != "")
				{
					model.CallerID = int.Parse(ds.Tables[0].Rows[0]["CallerID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CallerMemID"] != null && ds.Tables[0].Rows[0]["CallerMemID"].ToString() != "")
				{
					model.CallerMemID = int.Parse(ds.Tables[0].Rows[0]["CallerMemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CallerMobile"] != null && ds.Tables[0].Rows[0]["CallerMobile"].ToString() != "")
				{
					model.CallerMobile = ds.Tables[0].Rows[0]["CallerMobile"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CallerIsMem"] != null && ds.Tables[0].Rows[0]["CallerIsMem"].ToString() != "")
				{
					model.CallerIsMem = ds.Tables[0].Rows[0]["CallerIsMem"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CallerState"] != null && ds.Tables[0].Rows[0]["CallerState"].ToString() != "")
				{
					model.CallerState = ds.Tables[0].Rows[0]["CallerState"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CallerDuration"] != null && ds.Tables[0].Rows[0]["CallerDuration"].ToString() != "")
				{
					model.CallerDuration = ds.Tables[0].Rows[0]["CallerDuration"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CallerRemark"] != null && ds.Tables[0].Rows[0]["CallerRemark"].ToString() != "")
				{
					model.CallerRemark = ds.Tables[0].Rows[0]["CallerRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CallerCreateTime"] != null && ds.Tables[0].Rows[0]["CallerCreateTime"].ToString() != "")
				{
					model.CallerCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CallerCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CallerUserID"] != null && ds.Tables[0].Rows[0]["CallerUserID"].ToString() != "")
				{
					model.CallerUserID = int.Parse(ds.Tables[0].Rows[0]["CallerUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CallerShopID"] != null && ds.Tables[0].Rows[0]["CallerShopID"].ToString() != "")
				{
					model.CallerShopID = int.Parse(ds.Tables[0].Rows[0]["CallerShopID"].ToString());
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
			strSql.Append("select CallerID,CallerMemID,CallerMobile,CallerIsMem,CallerState,CallerDuration,CallerRemark,CallerCreateTime,CallerUserID,CallerShopID ");
			strSql.Append(" FROM ScreenPopUp ");
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
			strSql.Append(" CallerID,CallerMemID,CallerMobile,CallerIsMem,CallerState,CallerDuration,CallerRemark,CallerCreateTime,CallerUserID,CallerShopID ");
			strSql.Append(" FROM ScreenPopUp ");
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
			strSql.Append("select count(1) FROM ScreenPopUp ");
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
				strSql.Append("order by T.CallerID desc");
			}
			strSql.Append(")AS Row, T.*  from ScreenPopUp T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetScreenPopUpList(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "V_GetScreenPopUp";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "CallerID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
