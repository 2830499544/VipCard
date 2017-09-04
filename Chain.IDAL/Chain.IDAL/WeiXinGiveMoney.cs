using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class WeiXinGiveMoney
	{
		public int Add(Chain.Model.WeiXinGiveMoney model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into WeiXinGiveMoney(");
			strSql.Append("MoneyID,MemID,GiveMoney,GiveTime,IsWin)");
			strSql.Append(" values (");
			strSql.Append("@MoneyID,@MemID,@GiveMoney,@GiveTime,@IsWin)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyID", SqlDbType.Int, 4),
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@GiveMoney", SqlDbType.Decimal),
				new SqlParameter("@GiveTime", SqlDbType.DateTime),
				new SqlParameter("@IsWin", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MoneyID;
			parameters[1].Value = model.MemID;
			parameters[2].Value = model.GiveMoney;
			parameters[3].Value = model.GiveTime;
			parameters[4].Value = model.IsWin;
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

		public bool Delete(int MoneyID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from WeiXinGiveMoney ");
			strSql.Append(" where MoneyID=@MoneyID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MoneyID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public Chain.Model.WeiXinGiveMoney GetModel(int MoneyID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 MoneyID,MemID,GiveMoney,GiveTime from WeiXinGiveMoney ");
			strSql.Append(" where MoneyID=@MoneyID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MoneyID;
			Chain.Model.WeiXinGiveMoney model = new Chain.Model.WeiXinGiveMoney();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.WeiXinGiveMoney result;
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

		public Chain.Model.WeiXinGiveMoney DataRowToModel(DataRow row)
		{
			Chain.Model.WeiXinGiveMoney model = new Chain.Model.WeiXinGiveMoney();
			if (row != null)
			{
				if (row["GiveTime"] != null && row["GiveTime"].ToString() != "")
				{
					model.GiveTime = DateTime.Parse(row["GiveTime"].ToString());
				}
				if (row["GiveMoney"] != null && row["GiveMoney"].ToString() != "")
				{
					model.GiveMoney = decimal.Parse(row["GiveMoney"].ToString());
				}
				if (row["MoneyID"] != null && row["MoneyID"].ToString() != "")
				{
					model.MoneyID = int.Parse(row["MoneyID"].ToString());
				}
				if (row["MemID"] != null && row["MemID"].ToString() != "")
				{
					model.MemID = int.Parse(row["MemID"].ToString());
				}
			}
			return model;
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ");
			if (Top > 0)
			{
				strSql.Append(" top " + Top.ToString());
			}
			strSql.Append("Mem.MemName,Mem.MemMobile,Mem.MemPhoto, WeiXinGiveMoney.GiveMoney,WeiXinGiveMoney.GiveTime ");
			strSql.Append(" FROM WeiXinGiveMoney,Mem ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by " + filedOrder + " desc");
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select *");
			strSql.Append(" FROM WeiXinGiveMoney");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where" + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "WeiXinGiveMoney,Mem,SysShop";
			string[] columns = new string[]
			{
				"WeiXinGiveMoney.*,Mem.MemCard,Mem.MemName,SysShop.ShopName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MoneyID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetListSP(bool isasc, string order, int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "V_GetWeiXinMoneySum";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, order, "MemID", isasc, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public decimal GetMoneySum(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select sum(GiveMoney) FROM WeiXinGiveMoney ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			decimal result;
			if (obj == null)
			{
				result = 0m;
			}
			else
			{
				result = decimal.Parse(obj.ToString());
			}
			return result;
		}

		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) FROM WeiXinGiveMoney ");
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
	}
}
