using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class MemTransferLog
	{
		public int Add(Chain.Model.MemTransferLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MemTransferLog(");
			strSql.Append("ElseMoney,TotalMoney,TransferAccount,TransferFromMemID,TransferToMemID,TransferMoney,TransferRemark,TransferCreateTime,UserID)");
			strSql.Append(" values (");
			strSql.Append("@ElseMoney,@TotalMoney,@TransferAccount,@TransferFromMemID,@TransferToMemID,@TransferMoney,@TransferRemark,@TransferCreateTime,@UserID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@TransferAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@TransferFromMemID", SqlDbType.Int, 4),
				new SqlParameter("@TransferToMemID", SqlDbType.Int, 4),
				new SqlParameter("@TransferMoney", SqlDbType.Money),
				new SqlParameter("@TransferRemark", SqlDbType.NVarChar, 400),
				new SqlParameter("@TransferCreateTime", SqlDbType.DateTime),
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@ElseMoney", SqlDbType.Money),
				new SqlParameter("@TotalMoney", SqlDbType.Money)
			};
			parameters[0].Value = model.TransferAccount;
			parameters[1].Value = model.TransferFromMemID;
			parameters[2].Value = model.TransferToMemID;
			parameters[3].Value = model.TransferMoney;
			parameters[4].Value = model.TransferRemark;
			parameters[5].Value = model.TransferCreateTime;
			parameters[6].Value = model.UserID;
			parameters[7].Value = model.ElseMoney;
			parameters[8].Value = model.TotalMoney;
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

		public bool Update(Chain.Model.MemTransferLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MemTransferLog set ");
			strSql.Append("TransferAccount=@TransferAccount,");
			strSql.Append("TransferFromMemID=@TransferFromMemID,");
			strSql.Append("TransferToMemID=@TransferToMemID,");
			strSql.Append("TransferMoney=@TransferMoney,");
			strSql.Append("TransferRemark=@TransferRemark,");
			strSql.Append("TransferCreateTime=@TransferCreateTime,");
			strSql.Append("UserID=@UserID ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@TransferAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@TransferFromMemID", SqlDbType.Int, 4),
				new SqlParameter("@TransferToMemID", SqlDbType.Int, 4),
				new SqlParameter("@TransferMoney", SqlDbType.Money),
				new SqlParameter("@TransferRemark", SqlDbType.NVarChar, 400),
				new SqlParameter("@TransferCreateTime", SqlDbType.DateTime),
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.TransferAccount;
			parameters[1].Value = model.TransferFromMemID;
			parameters[2].Value = model.TransferToMemID;
			parameters[3].Value = model.TransferMoney;
			parameters[4].Value = model.TransferRemark;
			parameters[5].Value = model.TransferCreateTime;
			parameters[6].Value = model.UserID;
			parameters[7].Value = model.ID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemTransferLog ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM MemTransferLog ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "MemTransferLog,SysUser";
			string[] columns = new string[]
			{
				" MemTransferLog.*,SysUser.UserName,(select MemName from Mem where Mem.MemID = MemTransferLog.TransferFromMemID) as TransferFromMemName ,(select MemName from Mem where Mem.MemID = MemTransferLog.TransferToMemID) as TransferToMemName ,(select MemCard from Mem where Mem.MemID = MemTransferLog.TransferFromMemID) as TransferFromMemCard ,(select MemCard from Mem where Mem.MemID = MemTransferLog.TransferToMemID) as TransferToMemCard "
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetMoneyCount(string strSql)
		{
			StringBuilder strWhere = new StringBuilder();
			strWhere.Append(" select isnull(sum(TransferMoney),0) as TransferMoney from MemTransferLog ");
			if (strSql.Trim() != "")
			{
				strWhere.Append(" where " + strSql);
			}
			return DbHelperSQL.Query(strWhere.ToString());
		}
	}
}
