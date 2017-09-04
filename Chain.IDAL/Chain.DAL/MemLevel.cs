using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class MemLevel
	{
		public string GetNameByID(int LevelID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select LevelName FROM MemLevel  where LevelID=" + LevelID);
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			string result;
			if (obj == null)
			{
				result = "";
			}
			else
			{
				result = obj.ToString();
			}
			return result;
		}

		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("LevelID", "MemLevel");
		}

		public bool Exists(int LevelID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemLevel");
			strSql.Append(" where LevelID=@LevelID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@LevelID", SqlDbType.Int, 4)
			};
			parameters[0].Value = LevelID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(int LevelID, int LevelPoint)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemLevel");
			strSql.Append(" where LevelID <> @LevelID AND LevelPoint = @LevelPoint");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@LevelID", SqlDbType.Int, 4),
				new SqlParameter("@LevelPoint", SqlDbType.Int)
			};
			parameters[0].Value = LevelID;
			parameters[1].Value = LevelPoint;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.MemLevel model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MemLevel(");
			strSql.Append("LevelName,LevelPoint,LevelDiscountPercent,LevelPointPercent,LevellLock,LevelRechargePointRate)");
			strSql.Append(" values (");
			strSql.Append("@LevelName,@LevelPoint,@LevelDiscountPercent,@LevelPointPercent,@LevellLock,@LevelRechargePointRate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@LevelName", SqlDbType.NVarChar, 50),
				new SqlParameter("@LevelPoint", SqlDbType.Int, 4),
				new SqlParameter("@LevelDiscountPercent", SqlDbType.Float, 8),
				new SqlParameter("@LevelPointPercent", SqlDbType.Float, 8),
				new SqlParameter("@LevellLock", SqlDbType.Bit, 1),
				new SqlParameter("@LevelRechargePointRate", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.LevelName;
			parameters[1].Value = model.LevelPoint;
			parameters[2].Value = model.LevelDiscountPercent;
			parameters[3].Value = model.LevelPointPercent;
			parameters[4].Value = model.LevellLock;
			parameters[5].Value = model.LevelRechargePointRate;
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

		public int Update(Chain.Model.MemLevel model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MemLevel set ");
			strSql.Append("LevelName=@LevelName,");
			strSql.Append("LevelPoint=@LevelPoint,");
			strSql.Append("LevelDiscountPercent=@LevelDiscountPercent,");
			strSql.Append("LevelPointPercent=@LevelPointPercent,");
			strSql.Append("LevellLock=@LevellLock,");
			strSql.Append("LevelRechargePointRate=@LevelRechargePointRate ");
			strSql.Append(" where LevelID=@LevelID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@LevelName", SqlDbType.NVarChar, 50),
				new SqlParameter("@LevelPoint", SqlDbType.Int, 4),
				new SqlParameter("@LevelDiscountPercent", SqlDbType.Float, 8),
				new SqlParameter("@LevelPointPercent", SqlDbType.Float, 8),
				new SqlParameter("@LevellLock", SqlDbType.Bit, 1),
				new SqlParameter("@LevelRechargePointRate", SqlDbType.Int, 4),
				new SqlParameter("@LevelID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.LevelName;
			parameters[1].Value = model.LevelPoint;
			parameters[2].Value = model.LevelDiscountPercent;
			parameters[3].Value = model.LevelPointPercent;
			parameters[4].Value = model.LevellLock;
			parameters[5].Value = model.LevelRechargePointRate;
			parameters[6].Value = model.LevelID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			int result;
			if (rows > 0)
			{
				result = rows;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public int Delete(int LevelID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemLevel ");
			strSql.Append(" where LevelID=@LevelID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@LevelID", SqlDbType.Int, 4)
			};
			parameters[0].Value = LevelID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			int result;
			if (rows > 0)
			{
				result = rows;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public bool DeleteList(string LevelIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemLevel ");
			strSql.Append(" where LevelID in (" + LevelIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MemLevel GetModel(int LevelID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 LevelID,LevelName,LevelPoint,LevelDiscountPercent,LevelPointPercent,LevellLock,LevelRechargePointRate from MemLevel ");
			strSql.Append(" where LevelID=@LevelID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@LevelID", SqlDbType.Int, 4)
			};
			parameters[0].Value = LevelID;
			Chain.Model.MemLevel model = new Chain.Model.MemLevel();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MemLevel result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["LevelID"] != null && ds.Tables[0].Rows[0]["LevelID"].ToString() != "")
				{
					model.LevelID = int.Parse(ds.Tables[0].Rows[0]["LevelID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LevelName"] != null && ds.Tables[0].Rows[0]["LevelName"].ToString() != "")
				{
					model.LevelName = ds.Tables[0].Rows[0]["LevelName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["LevelPoint"] != null && ds.Tables[0].Rows[0]["LevelPoint"].ToString() != "")
				{
					model.LevelPoint = int.Parse(ds.Tables[0].Rows[0]["LevelPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["LevelDiscountPercent"] != null && ds.Tables[0].Rows[0]["LevelDiscountPercent"].ToString() != "")
				{
					model.LevelDiscountPercent = Convert.ToDecimal(ds.Tables[0].Rows[0]["LevelDiscountPercent"]);
				}
				if (ds.Tables[0].Rows[0]["LevelPointPercent"] != null && ds.Tables[0].Rows[0]["LevelPointPercent"].ToString() != "")
				{
					model.LevelPointPercent = Convert.ToDecimal(ds.Tables[0].Rows[0]["LevelPointPercent"]);
				}
				if (ds.Tables[0].Rows[0]["LevellLock"] != null && ds.Tables[0].Rows[0]["LevellLock"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["LevellLock"].ToString() == "1" || ds.Tables[0].Rows[0]["LevellLock"].ToString().ToLower() == "true")
					{
						model.LevellLock = true;
					}
					else
					{
						model.LevellLock = false;
					}
				}
				if (ds.Tables[0].Rows[0]["LevelRechargePointRate"] != null && ds.Tables[0].Rows[0]["LevelRechargePointRate"].ToString() != "")
				{
					model.LevelRechargePointRate = Convert.ToInt32(ds.Tables[0].Rows[0]["LevelRechargePointRate"]);
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
			strSql.Append("select LevelID,LevelName,LevelPoint,LevelDiscountPercent,LevelPointPercent,LevellLock,LevelRechargePointRate ");
			strSql.Append(" FROM MemLevel ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" ORDER BY LevelPoint asc");
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetLists(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select LevelID,ShopMemLevelID, LevelName,LevelPoint,ClassDiscountPercent,ClassPointPercent,ClassRechargePointRate,LevellLock from SysShopMemLevel join MemLevel on MemLevel.LevelID=SysShopMemLevel.MemLevelID ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" ORDER BY LevelPoint asc");
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
			strSql.Append(" LevelID,LevelName,LevelPoint,LevelDiscountPercent,LevelPointPercent,LevellLock,LevelRechargePointRate ");
			strSql.Append(" FROM MemLevel ");
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
			strSql.Append("select count(1) FROM MemLevel ");
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
				strSql.Append("order by T.LevelID desc");
			}
			strSql.Append(")AS Row, T.*  from MemLevel T ");
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
