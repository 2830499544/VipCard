using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class WeiXinMoneyMem
	{
		public int Add(Chain.Model.WeiXinMoneyMem model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into WeiXinMoneyMem(");
			strSql.Append("MoneyID,MemID)");
			strSql.Append(" values (");
			strSql.Append("@MoneyID,@MemID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyID", SqlDbType.Int, 4),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MoneyID;
			parameters[1].Value = model.MemID;
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
			strSql.Append("delete from WeiXinMoneyMem ");
			strSql.Append(" where MoneyID=@MoneyID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MoneyID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public Chain.Model.WeiXinMoneyMem GetModel(int MoneyID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 MoneyID,MemID from WeiXinMoneyMem ");
			strSql.Append(" where MoneyID=@MoneyID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MoneyID;
			Chain.Model.WeiXinMoneyMem model = new Chain.Model.WeiXinMoneyMem();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.WeiXinMoneyMem result;
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

		public Chain.Model.WeiXinMoneyMem DataRowToModel(DataRow row)
		{
			Chain.Model.WeiXinMoneyMem model = new Chain.Model.WeiXinMoneyMem();
			if (row != null)
			{
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

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select WeiXinMoneyMem.*,Mem.MemCard,Mem.MemName");
			strSql.Append(" FROM WeiXinMoneyMem ,Mem");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where" + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, string[] strWhere, out int resCount)
		{
			string tableName = "WeiXinMoneyMem";
			string[] columns = new string[]
			{
				"* "
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MoneyID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) FROM WeiXinMoneyMem ");
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
