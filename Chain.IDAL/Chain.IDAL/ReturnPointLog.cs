using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class ReturnPointLog
	{
		public int Add(Chain.Model.ReturnPointLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into ReturnPointLog(");
			strSql.Append("ReturnShopID,CreateTime,OrderAccount,MemID,totalPoint,AlliancePoint,CardShopPoint,ZBPoint,Remark)");
			strSql.Append(" values (");
			strSql.Append("@ReturnShopID,@CreateTime,@OrderAccount,@MemID,@totalPoint,@AlliancePoint,@CardShopPoint,@ZBPoint,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderAccount", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@totalPoint", SqlDbType.Int, 4),
				new SqlParameter("@AlliancePoint", SqlDbType.Int, 4),
				new SqlParameter("@CardShopPoint", SqlDbType.Int, 4),
				new SqlParameter("@ZBPoint", SqlDbType.Int, 4),
				new SqlParameter("@Remark", SqlDbType.NVarChar, 200),
				new SqlParameter("@ReturnShopID", SqlDbType.Int, 4),
				new SqlParameter("@CreateTime", SqlDbType.DateTime)
			};
			parameters[0].Value = model.OrderAccount;
			parameters[1].Value = model.MemID;
			parameters[2].Value = model.TotalPoint;
			parameters[3].Value = model.AlliancePoint;
			parameters[4].Value = model.CardShopPoint;
			parameters[5].Value = model.ZbPoint;
			parameters[6].Value = model.Remark;
			parameters[7].Value = model.ReturnShopID;
			parameters[8].Value = model.CreateTime;
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

		public Chain.Model.ReturnPointLog GetModelByOrderAccount(string OrderAccount)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 * from ReturnPointLog ");
			strSql.Append(" where OrderAccount=@OrderAccount");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderAccount", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = OrderAccount;
			Chain.Model.ReturnPointLog model = new Chain.Model.ReturnPointLog();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.ReturnPointLog result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["totalPoint"] != null && ds.Tables[0].Rows[0]["totalPoint"].ToString() != "")
				{
					model.TotalPoint = int.Parse(ds.Tables[0].Rows[0]["totalPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AlliancePoint"] != null && ds.Tables[0].Rows[0]["AlliancePoint"].ToString() != "")
				{
					model.AlliancePoint = int.Parse(ds.Tables[0].Rows[0]["AlliancePoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CardShopPoint"] != null && ds.Tables[0].Rows[0]["CardShopPoint"].ToString() != "")
				{
					model.CardShopPoint = int.Parse(ds.Tables[0].Rows[0]["CardShopPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ZbPoint"] != null && ds.Tables[0].Rows[0]["ZbPoint"].ToString() != "")
				{
					model.ZbPoint = int.Parse(ds.Tables[0].Rows[0]["ZbPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderAccount"] != null && ds.Tables[0].Rows[0]["OrderAccount"].ToString() != "")
				{
					model.OrderAccount = ds.Tables[0].Rows[0]["OrderAccount"].ToString();
				}
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) FROM ReturnPointLog ");
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

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "ReturnPointLog,SysShop";
			string[] columns = new string[]
			{
				"ReturnPointLog.*,SysShop.ShopName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "LogID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
