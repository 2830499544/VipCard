using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class Special
	{
		public DataSet GetItemAll(int SpecialID)
		{
			string sql_mem = " select * from Special where SpecialID =" + SpecialID;
			return DbHelperSQL.Query(sql_mem);
		}

		public DataSet GetListsp(int PageSiza, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "Special,SysUser";
			string[] columns = new string[]
			{
				"Special.*,SysUser.UserName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "SpecialID", false, PageSiza, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM Special ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int Add(Chain.Model.Special model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Special(");
			strSql.Append("SpecialName,SpecialRecharge,SpecialGive,SpecialTime,Sremark,SpecialUser,Type,StartTime,EndTime,Week,Month )");
			strSql.Append(" values (");
			strSql.Append("@SpecialName,@SpecialRecharge,@SpecialGive,@SpecialTime,@Sremark,@SpecialUser,@Type,@StartTime,@EndTime,@Week,@Month )");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SpecialName", SqlDbType.NVarChar, 200),
				new SqlParameter("@SpecialRecharge", SqlDbType.Money, 8),
				new SqlParameter("@SpecialGive", SqlDbType.Money, 8),
				new SqlParameter("@SpecialTime", SqlDbType.DateTime),
				new SqlParameter("@Sremark", SqlDbType.NVarChar, 200),
				new SqlParameter("@SpecialUser", SqlDbType.Int, 4),
				new SqlParameter("@Type", SqlDbType.Int, 4),
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EndTime", SqlDbType.DateTime),
				new SqlParameter("@Week", SqlDbType.NVarChar, 20),
				new SqlParameter("@Month", SqlDbType.NVarChar, 100)
			};
			parameters[0].Value = model.SpecialName;
			parameters[1].Value = model.SpecialRecharge;
			parameters[2].Value = model.SpecialGive;
			parameters[3].Value = model.SpecialTime;
			parameters[4].Value = model.Sremark;
			parameters[5].Value = model.SpecialUser;
			parameters[6].Value = model.Type;
			parameters[7].Value = model.StartTime;
			parameters[8].Value = model.EndTime;
			parameters[9].Value = model.Week;
			parameters[10].Value = model.Month;
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

		public int Update(Chain.Model.Special model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update Special set ");
			strSql.Append("SpecialName=@SpecialName,");
			strSql.Append("SpecialRecharge=@SpecialRecharge,");
			strSql.Append("SpecialGive=@SpecialGive,");
			strSql.Append("SpecialTime=@SpecialTime,");
			strSql.Append("Sremark=@Sremark,");
			strSql.Append("SpecialUser=@SpecialUser,");
			strSql.Append("Type=@Type,");
			strSql.Append("StartTime=@StartTime,");
			strSql.Append("EndTime=@EndTime,");
			strSql.Append("Week=@Week,");
			strSql.Append("Month=@Month ");
			strSql.Append(" where SpecialID=@SpecialID ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SpecialName", SqlDbType.NVarChar, 200),
				new SqlParameter("@SpecialRecharge", SqlDbType.Money, 8),
				new SqlParameter("@SpecialGive", SqlDbType.Money, 8),
				new SqlParameter("@SpecialTime", SqlDbType.DateTime),
				new SqlParameter("@Sremark", SqlDbType.NVarChar, 200),
				new SqlParameter("@SpecialUser", SqlDbType.Int, 4),
				new SqlParameter("@Type", SqlDbType.Int, 4),
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EndTime", SqlDbType.DateTime),
				new SqlParameter("@Week", SqlDbType.NVarChar, 20),
				new SqlParameter("@Month", SqlDbType.NVarChar, 100),
				new SqlParameter("@SpecialID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.SpecialName;
			parameters[1].Value = model.SpecialRecharge;
			parameters[2].Value = model.SpecialGive;
			parameters[3].Value = model.SpecialTime;
			parameters[4].Value = model.Sremark;
			parameters[5].Value = model.SpecialUser;
			parameters[6].Value = model.Type;
			parameters[7].Value = model.StartTime;
			parameters[8].Value = model.EndTime;
			parameters[9].Value = model.Week;
			parameters[10].Value = model.Month;
			parameters[11].Value = model.SpecialID;
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

		public bool Delete(int SpecialID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Special ");
			strSql.Append(" where SpecialID=@SpecialID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SpecialID", SqlDbType.Int, 4)
			};
			parameters[0].Value = SpecialID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}
	}
}
