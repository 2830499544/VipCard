using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class Timingrules
	{
		public bool Exists(int RulesID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from Timingrules");
			strSql.Append(" where RulesID=@RulesID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RulesID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RulesID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.Timingrules model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Timingrules(");
			strSql.Append("RulesName,RulesInterval,RulesUnitPrice,RulesExceedTime,RulesAddTime,RulesShopID,RulesUserID,RulesRemark)");
			strSql.Append(" values (");
			strSql.Append("@RulesName,@RulesInterval,@RulesUnitPrice,@RulesExceedTime,@RulesAddTime,@RulesShopID,@RulesUserID,@RulesRemark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RulesName", SqlDbType.NVarChar, 50),
				new SqlParameter("@RulesInterval", SqlDbType.Int, 4),
				new SqlParameter("@RulesUnitPrice", SqlDbType.Money, 8),
				new SqlParameter("@RulesExceedTime", SqlDbType.Int, 4),
				new SqlParameter("@RulesAddTime", SqlDbType.DateTime),
				new SqlParameter("@RulesShopID", SqlDbType.Int, 4),
				new SqlParameter("@RulesUserID", SqlDbType.Int, 4),
				new SqlParameter("@RulesRemark", SqlDbType.NVarChar, 200)
			};
			parameters[0].Value = model.RulesName;
			parameters[1].Value = model.RulesInterval;
			parameters[2].Value = model.RulesUnitPrice;
			parameters[3].Value = model.RulesExceedTime;
			parameters[4].Value = model.RulesAddTime;
			parameters[5].Value = model.RulesShopID;
			parameters[6].Value = model.RulesUserID;
			parameters[7].Value = model.RulesRemark;
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

		public bool Update(Chain.Model.Timingrules model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update Timingrules set ");
			strSql.Append("RulesName=@RulesName,");
			strSql.Append("RulesInterval=@RulesInterval,");
			strSql.Append("RulesUnitPrice=@RulesUnitPrice,");
			strSql.Append("RulesExceedTime=@RulesExceedTime,");
			strSql.Append("RulesAddTime=@RulesAddTime,");
			strSql.Append("RulesShopID=@RulesShopID,");
			strSql.Append("RulesUserID=@RulesUserID,");
			strSql.Append("RulesRemark=@RulesRemark");
			strSql.Append(" where RulesID=@RulesID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RulesName", SqlDbType.NVarChar, 50),
				new SqlParameter("@RulesInterval", SqlDbType.Int, 4),
				new SqlParameter("@RulesUnitPrice", SqlDbType.Money, 8),
				new SqlParameter("@RulesExceedTime", SqlDbType.Int, 4),
				new SqlParameter("@RulesAddTime", SqlDbType.DateTime),
				new SqlParameter("@RulesShopID", SqlDbType.Int, 4),
				new SqlParameter("@RulesUserID", SqlDbType.Int, 4),
				new SqlParameter("@RulesID", SqlDbType.Int, 4),
				new SqlParameter("@RulesRemark", SqlDbType.NVarChar, 200)
			};
			parameters[0].Value = model.RulesName;
			parameters[1].Value = model.RulesInterval;
			parameters[2].Value = model.RulesUnitPrice;
			parameters[3].Value = model.RulesExceedTime;
			parameters[4].Value = model.RulesAddTime;
			parameters[5].Value = model.RulesShopID;
			parameters[6].Value = model.RulesUserID;
			parameters[7].Value = model.RulesID;
			parameters[8].Value = model.RulesRemark;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int RulesID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Timingrules ");
			strSql.Append(" where RulesID=@RulesID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RulesID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RulesID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string RulesIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Timingrules ");
			strSql.Append(" where RulesID in (" + RulesIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.Timingrules GetModel(int RulesID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 * from Timingrules ");
			strSql.Append(" where RulesID=@RulesID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RulesID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RulesID;
			Chain.Model.Timingrules model = new Chain.Model.Timingrules();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.Timingrules result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["RulesID"] != null && ds.Tables[0].Rows[0]["RulesID"].ToString() != "")
				{
					model.RulesID = int.Parse(ds.Tables[0].Rows[0]["RulesID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RulesName"] != null && ds.Tables[0].Rows[0]["RulesName"].ToString() != "")
				{
					model.RulesName = ds.Tables[0].Rows[0]["RulesName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RulesInterval"] != null && ds.Tables[0].Rows[0]["RulesInterval"].ToString() != "")
				{
					model.RulesInterval = int.Parse(ds.Tables[0].Rows[0]["RulesInterval"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RulesUnitPrice"] != null && ds.Tables[0].Rows[0]["RulesUnitPrice"].ToString() != "")
				{
					model.RulesUnitPrice = decimal.Parse(ds.Tables[0].Rows[0]["RulesUnitPrice"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RulesExceedTime"] != null && ds.Tables[0].Rows[0]["RulesExceedTime"].ToString() != "")
				{
					model.RulesExceedTime = int.Parse(ds.Tables[0].Rows[0]["RulesExceedTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RulesAddTime"] != null && ds.Tables[0].Rows[0]["RulesAddTime"].ToString() != "")
				{
					model.RulesAddTime = DateTime.Parse(ds.Tables[0].Rows[0]["RulesAddTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RulesShopID"] != null && ds.Tables[0].Rows[0]["RulesShopID"].ToString() != "")
				{
					model.RulesShopID = int.Parse(ds.Tables[0].Rows[0]["RulesShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RulesUserID"] != null && ds.Tables[0].Rows[0]["RulesUserID"].ToString() != "")
				{
					model.RulesUserID = int.Parse(ds.Tables[0].Rows[0]["RulesUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RulesRemark"] != null && ds.Tables[0].Rows[0]["RulesRemark"].ToString() != "")
				{
					model.RulesRemark = ds.Tables[0].Rows[0]["RulesRemark"].ToString();
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
			strSql.Append("select * ");
			strSql.Append(" FROM Timingrules ");
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
			strSql.Append(" * FROM Timingrules ");
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
			strSql.Append("select count(1) FROM Timingrules ");
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
				strSql.Append("order by T.RulesID desc");
			}
			strSql.Append(")AS Row, T.*  from Timingrules T ");
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
			string tableName = "Timingrules,SysUser";
			string[] columns = new string[]
			{
				"Timingrules.*,SysUser.UserName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "RulesAddTime", "RulesID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
