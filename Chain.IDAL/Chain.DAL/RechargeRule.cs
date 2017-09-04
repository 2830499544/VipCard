using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class RechargeRule
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("RuleID", "RechargeRule");
		}

		public bool Exists(int RuleID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from RechargeRule");
			strSql.Append(" where RuleID=@RuleID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RuleID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RuleID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.RechargeRule model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into RechargeRule(");
			strSql.Append("RechargeMoney,GiveMoney,RuleDesc,CreateUserID,CreateTime)");
			strSql.Append(" values (");
			strSql.Append("@RechargeMoney,@GiveMoney,@RuleDesc,@CreateUserID,@CreateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RechargeMoney", SqlDbType.Decimal),
				new SqlParameter("@GiveMoney", SqlDbType.Decimal),
				new SqlParameter("@RuleDesc", SqlDbType.NVarChar, 500),
				new SqlParameter("@CreateUserID", SqlDbType.Int, 4),
				new SqlParameter("@CreateTime", SqlDbType.DateTime)
			};
			parameters[0].Value = model.RechargeMoney;
			parameters[1].Value = model.GiveMoney;
			parameters[2].Value = model.RuleDesc;
			parameters[3].Value = model.CreateUserID;
			parameters[4].Value = model.CreateTime;
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

		public bool Update(Chain.Model.RechargeRule model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update RechargeRule set ");
			strSql.Append("RechargeMoney=@RechargeMoney,");
			strSql.Append("GiveMoney=@GiveMoney,");
			strSql.Append("RuleDesc=@RuleDesc,");
			strSql.Append("CreateUserID=@CreateUserID,");
			strSql.Append("CreateTime=@CreateTime");
			strSql.Append(" where RuleID=@RuleID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RechargeMoney", SqlDbType.Decimal),
				new SqlParameter("@GiveMoney", SqlDbType.Decimal),
				new SqlParameter("@RuleDesc", SqlDbType.NVarChar, 500),
				new SqlParameter("@CreateUserID", SqlDbType.Int, 4),
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@RuleID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.RechargeMoney;
			parameters[1].Value = model.GiveMoney;
			parameters[2].Value = model.RuleDesc;
			parameters[3].Value = model.CreateUserID;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = model.RuleID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int RuleID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from RechargeRule ");
			strSql.Append(" where RuleID=@RuleID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RuleID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RuleID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string RuleIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from RechargeRule ");
			strSql.Append(" where RuleID in (" + RuleIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.RechargeRule GetModel(int RuleID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 RuleID,RechargeMoney,GiveMoney,RuleDesc,CreateUserID,CreateTime from RechargeRule ");
			strSql.Append(" where RuleID=@RuleID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RuleID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RuleID;
			Chain.Model.RechargeRule model = new Chain.Model.RechargeRule();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.RechargeRule result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["RuleID"] != null && ds.Tables[0].Rows[0]["RuleID"].ToString() != "")
				{
					model.RuleID = int.Parse(ds.Tables[0].Rows[0]["RuleID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RechargeMoney"] != null && ds.Tables[0].Rows[0]["RechargeMoney"].ToString() != "")
				{
					model.RechargeMoney = decimal.Parse(ds.Tables[0].Rows[0]["RechargeMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GiveMoney"] != null && ds.Tables[0].Rows[0]["GiveMoney"].ToString() != "")
				{
					model.GiveMoney = decimal.Parse(ds.Tables[0].Rows[0]["GiveMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RuleDesc"] != null && ds.Tables[0].Rows[0]["RuleDesc"].ToString() != "")
				{
					model.RuleDesc = ds.Tables[0].Rows[0]["RuleDesc"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RuleDesc"] != null && ds.Tables[0].Rows[0]["RuleDesc"].ToString() != "")
				{
					model.RuleDesc = ds.Tables[0].Rows[0]["RuleDesc"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CreateUserID"] != null && ds.Tables[0].Rows[0]["CreateUserID"].ToString() != "")
				{
					model.CreateUserID = int.Parse(ds.Tables[0].Rows[0]["CreateUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
				{
					model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
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
			strSql.Append("select RuleID,RechargeMoney,GiveMoney,RuleDesc,CreateUserID,CreateTime ");
			strSql.Append("FROM RechargeRule ");
			strSql.Append("where 1=1 ");
			if (strWhere.Trim() != "")
			{
				strSql.Append("and " + strWhere + " order by RechargeMoney desc");
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
			strSql.Append(" RuleID,RuleNUmber,RuleNewsType,RuleDesc,RuleContent,RuleUserID,RuleCreateTime ");
			strSql.Append(" FROM RechargeRule ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "RechargeRule,SysUser";
			string[] columns = new string[]
			{
				"RechargeRule.*,UserName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "CreateTime", "RuleID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) FROM RechargeRule ");
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
				strSql.Append("order by T.RuleID desc");
			}
			strSql.Append(")AS Row, T.*  from RechargeRule T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public string AttentionStr()
		{
			StringBuilder sbStr = new StringBuilder();
			DataTable dt = this.Attention();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				DataRow item = dt.Rows[i];
				sbStr.AppendLine(item["RuleDesc"].ToString()).AppendLine();
			}
			return sbStr.ToString();
		}

		public DataTable Attention()
		{
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("select * from (select *, ROW_NUMBER () over (order by RuleID)as a from RechargeRule where RuleID<=4) as a");
			sbSql.Append(" union all ");
			sbSql.Append("select * from (select *, ROW_NUMBER () over (order by RuleID)as a from RechargeRule where RuleID>=5) as b");
			return DbHelperSQL.Query(sbSql.ToString()).Tables[0];
		}

		public string Reply1()
		{
			return DbHelperSQL.Query("select RuleContent from RechargeRule where RuleNUmber='1'").Tables[0].Rows[0][0].ToString();
		}

		public string Reply2()
		{
			return DbHelperSQL.Query("select RuleContent from RechargeRule where RuleNUmber='2'").Tables[0].Rows[0][0].ToString();
		}

		public string GetCardDesc()
		{
			string returnStr = "";
			DataTable dt = DbHelperSQL.Query("select RuleContent from RechargeRule where RuleNUmber='-1'").Tables[0];
			if (dt.Rows.Count > 0)
			{
				returnStr = dt.Rows[0][0].ToString();
			}
			return returnStr;
		}

		public string ErrorStr()
		{
			StringBuilder sbStr = new StringBuilder();
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("select * from (select *, ROW_NUMBER () over (order by RuleID)as a from RechargeRule where RuleID<=4) as a");
			sbSql.Append(" union all ");
			sbSql.Append("select * from (select *, ROW_NUMBER () over (order by RuleID desc)as a from RechargeRule where RuleID>=5) as b");
			DataTable dt = DbHelperSQL.Query(sbSql.ToString()).Tables[0];
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				DataRow item = dt.Rows[i];
				sbStr.AppendLine(item["RuleDesc"].ToString()).AppendLine();
			}
			return sbStr.ToString();
		}

		public Chain.Model.RechargeRule GetModelByNewsRuleID(string RuleNUmber)
		{
			DataTable dt = DbHelperSQL.Query("select * from RechargeRule where RuleNUmber=@RuleNUmber", new SqlParameter[]
			{
				new SqlParameter("@RuleNUmber", RuleNUmber)
			}).Tables[0];
			Chain.Model.RechargeRule result;
			if (dt.Rows.Count > 0)
			{
				result = this.GetModel(int.Parse(dt.Rows[0]["RuleID"].ToString()));
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
