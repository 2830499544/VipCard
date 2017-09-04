using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class MoneyChangeLog
	{
		public bool Exists(int MoneyChangeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MoneyChangeLog");
			strSql.Append(" where MoneyChangeID=@MoneyChangeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyChangeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MoneyChangeID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(string orderaccount)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MoneyChangeLog");
			strSql.Append(" where MoneyChangeAccount=@MoneyChangeAccount");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyChangeAccount", SqlDbType.VarChar, 20)
			};
			parameters[0].Value = orderaccount;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.MoneyChangeLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MoneyChangeLog(");
			strSql.Append("MoneyChangeMemID,MoneyChangeUserID,MoneyChangeType,MoneyChangeAccount,MoneyChangeMoney,MoneyChangeCash,MoneyChangeBalance,MoneyChangeUnionPay,MemMoney,MoneyChangeCreateTime,MoneyChangeGiveMoney)");
			strSql.Append(" values (");
			strSql.Append("@MoneyChangeMemID,@MoneyChangeUserID,@MoneyChangeType,@MoneyChangeAccount,@MoneyChangeMoney,@MoneyChangeCash,@MoneyChangeBalance,@MoneyChangeUnionPay,@MemMoney,@MoneyChangeCreateTime,@MoneyChangeGiveMoney)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyChangeMemID", SqlDbType.Int, 4),
				new SqlParameter("@MoneyChangeUserID", SqlDbType.Int, 4),
				new SqlParameter("@MoneyChangeType", SqlDbType.Int, 4),
				new SqlParameter("@MoneyChangeAccount", SqlDbType.NVarChar, 50),
				new SqlParameter("@MoneyChangeMoney", SqlDbType.Money, 8),
				new SqlParameter("@MoneyChangeCash", SqlDbType.Money, 8),
				new SqlParameter("@MoneyChangeBalance", SqlDbType.Money, 8),
				new SqlParameter("@MoneyChangeUnionPay", SqlDbType.Money, 8),
				new SqlParameter("@MemMoney", SqlDbType.Money, 8),
				new SqlParameter("@MoneyChangeCreateTime", SqlDbType.DateTime),
				new SqlParameter("@MoneyChangeGiveMoney", SqlDbType.Money, 8)
			};
			parameters[0].Value = model.MoneyChangeMemID;
			parameters[1].Value = model.MoneyChangeUserID;
			parameters[2].Value = model.MoneyChangeType;
			parameters[3].Value = model.MoneyChangeAccount;
			parameters[4].Value = model.MoneyChangeMoney;
			parameters[5].Value = model.MoneyChangeCash;
			parameters[6].Value = model.MoneyChangeBalance;
			parameters[7].Value = model.MoneyChangeUnionPay;
			parameters[8].Value = model.MemMoney;
			parameters[9].Value = model.MoneyChangeCreateTime;
			parameters[10].Value = model.MoneyChangeGiveMoney;
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

		public bool Update(Chain.Model.MoneyChangeLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MoneyChangeLog set ");
			strSql.Append("MoneyChangeMemID=@MoneyChangeMemID,");
			strSql.Append("MoneyChangeUserID=@MoneyChangeUserID,");
			strSql.Append("MoneyChangeType=@MoneyChangeType,");
			strSql.Append("MoneyChangeAccount=@MoneyChangeAccount,");
			strSql.Append("MoneyChangeMoney=@MoneyChangeMoney,");
			strSql.Append("MoneyChangeCash=@MoneyChangeCash,");
			strSql.Append("MoneyChangeBalance=@MoneyChangeBalance,");
			strSql.Append("MoneyChangeUnionPay=@MoneyChangeUnionPay,");
			strSql.Append("MemMoney=@MemMoney,");
			strSql.Append("MoneyChangeCreateTime=@MoneyChangeCreateTime,");
			strSql.Append("MoneyChangeGiveMoney=@MoneyChangeGiveMoney");
			strSql.Append(" where MoneyChangeID=@MoneyChangeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyChangeMemID", SqlDbType.Int, 4),
				new SqlParameter("@MoneyChangeUserID", SqlDbType.Int, 4),
				new SqlParameter("@MoneyChangeType", SqlDbType.Int, 4),
				new SqlParameter("@MoneyChangeAccount", SqlDbType.NVarChar, 50),
				new SqlParameter("@MoneyChangeMoney", SqlDbType.Money, 8),
				new SqlParameter("@MoneyChangeCash", SqlDbType.Money, 8),
				new SqlParameter("@MoneyChangeBalance", SqlDbType.Money, 8),
				new SqlParameter("@MoneyChangeUnionPay", SqlDbType.Money, 8),
				new SqlParameter("@MemMoney", SqlDbType.Money, 8),
				new SqlParameter("@MoneyChangeCreateTime", SqlDbType.DateTime),
				new SqlParameter("@MoneyChangeGiveMoney", SqlDbType.Money, 8),
				new SqlParameter("@MoneyChangeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MoneyChangeMemID;
			parameters[1].Value = model.MoneyChangeUserID;
			parameters[2].Value = model.MoneyChangeType;
			parameters[3].Value = model.MoneyChangeAccount;
			parameters[4].Value = model.MoneyChangeMoney;
			parameters[5].Value = model.MoneyChangeCash;
			parameters[6].Value = model.MoneyChangeBalance;
			parameters[7].Value = model.MoneyChangeUnionPay;
			parameters[8].Value = model.MemMoney;
			parameters[9].Value = model.MoneyChangeCreateTime;
			parameters[10].Value = model.MoneyChangeGiveMoney;
			parameters[11].Value = model.MoneyChangeID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int MoneyChangeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MoneyChangeLog ");
			strSql.Append(" where MoneyChangeID=@MoneyChangeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyChangeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MoneyChangeID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string MoneyChangeIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MoneyChangeLog ");
			strSql.Append(" where MoneyChangeID in (" + MoneyChangeIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MoneyChangeLog GetModel(int MoneyChangeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 MoneyChangeID,MoneyChangeMemID,MoneyChangeUserID,MoneyChangeType,MoneyChangeAccount,MoneyChangeMoney,MoneyChangeCash,MoneyChangeBalance,MoneyChangeUnionPay,MemMoney,MoneyChangeCreateTime,MoneyChangeGiveMoney from MoneyChangeLog ");
			strSql.Append(" where MoneyChangeID=@MoneyChangeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyChangeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MoneyChangeID;
			Chain.Model.MoneyChangeLog model = new Chain.Model.MoneyChangeLog();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MoneyChangeLog result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["MoneyChangeID"] != null && ds.Tables[0].Rows[0]["MoneyChangeID"].ToString() != "")
				{
					model.MoneyChangeID = int.Parse(ds.Tables[0].Rows[0]["MoneyChangeID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MoneyChangeMemID"] != null && ds.Tables[0].Rows[0]["MoneyChangeMemID"].ToString() != "")
				{
					model.MoneyChangeMemID = int.Parse(ds.Tables[0].Rows[0]["MoneyChangeMemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MoneyChangeUserID"] != null && ds.Tables[0].Rows[0]["MoneyChangeUserID"].ToString() != "")
				{
					model.MoneyChangeUserID = int.Parse(ds.Tables[0].Rows[0]["MoneyChangeUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MoneyChangeType"] != null && ds.Tables[0].Rows[0]["MoneyChangeType"].ToString() != "")
				{
					model.MoneyChangeType = int.Parse(ds.Tables[0].Rows[0]["MoneyChangeType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MoneyChangeAccount"] != null && ds.Tables[0].Rows[0]["MoneyChangeAccount"].ToString() != "")
				{
					model.MoneyChangeAccount = ds.Tables[0].Rows[0]["MoneyChangeAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MoneyChangeMoney"] != null && ds.Tables[0].Rows[0]["MoneyChangeMoney"].ToString() != "")
				{
					model.MoneyChangeMoney = decimal.Parse(ds.Tables[0].Rows[0]["MoneyChangeMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MoneyChangeCash"] != null && ds.Tables[0].Rows[0]["MoneyChangeCash"].ToString() != "")
				{
					model.MoneyChangeCash = decimal.Parse(ds.Tables[0].Rows[0]["MoneyChangeCash"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MoneyChangeBalance"] != null && ds.Tables[0].Rows[0]["MoneyChangeBalance"].ToString() != "")
				{
					model.MoneyChangeBalance = decimal.Parse(ds.Tables[0].Rows[0]["MoneyChangeBalance"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MoneyChangeUnionPay"] != null && ds.Tables[0].Rows[0]["MoneyChangeUnionPay"].ToString() != "")
				{
					model.MoneyChangeUnionPay = decimal.Parse(ds.Tables[0].Rows[0]["MoneyChangeUnionPay"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemMoney"] != null && ds.Tables[0].Rows[0]["MemMoney"].ToString() != "")
				{
					model.MemMoney = decimal.Parse(ds.Tables[0].Rows[0]["MemMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MoneyChangeCreateTime"] != null && ds.Tables[0].Rows[0]["MoneyChangeCreateTime"].ToString() != "")
				{
					model.MoneyChangeCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["MoneyChangeCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MoneyChangeGiveMoney"] != null && ds.Tables[0].Rows[0]["MoneyChangeGiveMoney"].ToString() != "")
				{
					model.MoneyChangeGiveMoney = decimal.Parse(ds.Tables[0].Rows[0]["MoneyChangeGiveMoney"].ToString());
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
			strSql.Append("select MoneyChangeID,MoneyChangeMemID,MoneyChangeUserID,MoneyChangeType,MoneyChangeAccount,MoneyChangeMoney,MoneyChangeCash,MoneyChangeBalance,MoneyChangeUnionPay,MemMoney,MoneyChangeCreateTime,MoneyChangeGiveMoney ");
			strSql.Append(" FROM MoneyChangeLog ");
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
			strSql.Append(" MoneyChangeID,MoneyChangeMemID,MoneyChangeUserID,MoneyChangeType,MoneyChangeAccount,MoneyChangeMoney,MoneyChangeCash,MoneyChangeBalance,MoneyChangeUnionPay,MemMoney,MoneyChangeCreateTime,MoneyChangeGiveMoney ");
			strSql.Append(" FROM MoneyChangeLog ");
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
			strSql.Append("select count(1) FROM MoneyChangeLog ");
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
				strSql.Append("order by T.MoneyChangeID desc");
			}
			strSql.Append(")AS Row, T.*  from MoneyChangeLog T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetMoneyChangeLog(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "MoneyChangeLog,Mem,SysUser,SysShop";
			string[] columns = new string[]
			{
				"MoneyChangeLog.*,Mem.MemCard,Mem.MemName,SysUser.UserName,SysShop.ShopName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MoneyChangeID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetMoneyChange(string strSql)
		{
			StringBuilder strWhere = new StringBuilder();
			strWhere.Append(" select isnull(sum(MoneyChangeMoney),0) as MoneyChangeMoney, isnull(sum(MoneyChangeCash),0) as MoneyChangeCash ,isnull(sum(MoneyChangeBalance),0) as MoneyChangeBalance ,isnull(sum(MoneyChangeUnionPay),0) as MoneyChangeUnionPay,isnull(sum(MoneyChangeGiveMoney),0) as MoneyChangeGiveMoney ");
			strWhere.Append(" from MoneyChangeLog,Mem,SysShop,SysUser ");
			if (strSql.Trim() != "")
			{
				strWhere.Append(" where " + strSql);
			}
			return DbHelperSQL.Query(strWhere.ToString());
		}
	}
}
