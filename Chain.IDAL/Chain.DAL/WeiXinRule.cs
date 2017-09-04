using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class WeiXinRule
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("RuleID", "WeiXinRule");
		}

		public bool Exists(int RuleID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from WeiXinRule");
			strSql.Append(" where RuleID=@RuleID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RuleID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RuleID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.WeiXinRule model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into WeiXinRule(");
			strSql.Append("RuleNUmber,RuleNewsType,RuleDesc,RuleContent,RuleUserID,RuleCreateTime)");
			strSql.Append(" values (");
			strSql.Append("@RuleNUmber,@RuleNewsType,@RuleDesc,@RuleContent,@RuleUserID,@RuleCreateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RuleNUmber", SqlDbType.NVarChar, 50),
				new SqlParameter("@RuleNewsType", SqlDbType.NVarChar, 50),
				new SqlParameter("@RuleDesc", SqlDbType.NVarChar, 500),
				new SqlParameter("@RuleContent", SqlDbType.NVarChar, 1000),
				new SqlParameter("@RuleUserID", SqlDbType.Int, 4),
				new SqlParameter("@RuleCreateTime", SqlDbType.DateTime)
			};
			parameters[0].Value = model.RuleNUmber;
			parameters[1].Value = model.RuleNewsType;
			parameters[2].Value = model.RuleDesc;
			parameters[3].Value = model.RuleContent;
			parameters[4].Value = model.RuleUserID;
			parameters[5].Value = model.RuleCreateTime;
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

		public bool Update(Chain.Model.WeiXinRule model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update WeiXinRule set ");
			strSql.Append("RuleNUmber=@RuleNUmber,");
			strSql.Append("RuleNewsType=@RuleNewsType,");
			strSql.Append("RuleDesc=@RuleDesc,");
			strSql.Append("RuleContent=@RuleContent,");
			strSql.Append("RuleUserID=@RuleUserID,");
			strSql.Append("RuleCreateTime=@RuleCreateTime");
			strSql.Append(" where RuleID=@RuleID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RuleNUmber", SqlDbType.NVarChar, 50),
				new SqlParameter("@RuleNewsType", SqlDbType.NVarChar, 50),
				new SqlParameter("@RuleDesc", SqlDbType.NVarChar, 500),
				new SqlParameter("@RuleContent", SqlDbType.NVarChar, 1000),
				new SqlParameter("@RuleUserID", SqlDbType.Int, 4),
				new SqlParameter("@RuleCreateTime", SqlDbType.DateTime),
				new SqlParameter("@RuleID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.RuleNUmber;
			parameters[1].Value = model.RuleNewsType;
			parameters[2].Value = model.RuleDesc;
			parameters[3].Value = model.RuleContent;
			parameters[4].Value = model.RuleUserID;
			parameters[5].Value = model.RuleCreateTime;
			parameters[6].Value = model.RuleID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int RuleID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from WeiXinRule ");
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
			strSql.Append("delete from WeiXinRule ");
			strSql.Append(" where RuleID in (" + RuleIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.WeiXinRule GetModel(int RuleID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 RuleID,RuleNUmber,RuleNewsType,RuleDesc,RuleContent,RuleUserID,RuleCreateTime from WeiXinRule ");
			strSql.Append(" where RuleID=@RuleID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RuleID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RuleID;
			Chain.Model.WeiXinRule model = new Chain.Model.WeiXinRule();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.WeiXinRule result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["RuleID"] != null && ds.Tables[0].Rows[0]["RuleID"].ToString() != "")
				{
					model.RuleID = int.Parse(ds.Tables[0].Rows[0]["RuleID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RuleNUmber"] != null && ds.Tables[0].Rows[0]["RuleNUmber"].ToString() != "")
				{
					model.RuleNUmber = ds.Tables[0].Rows[0]["RuleNUmber"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RuleNewsType"] != null && ds.Tables[0].Rows[0]["RuleNewsType"].ToString() != "")
				{
					model.RuleNewsType = ds.Tables[0].Rows[0]["RuleNewsType"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RuleDesc"] != null && ds.Tables[0].Rows[0]["RuleDesc"].ToString() != "")
				{
					model.RuleDesc = ds.Tables[0].Rows[0]["RuleDesc"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RuleContent"] != null && ds.Tables[0].Rows[0]["RuleContent"].ToString() != "")
				{
					model.RuleContent = ds.Tables[0].Rows[0]["RuleContent"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RuleUserID"] != null && ds.Tables[0].Rows[0]["RuleUserID"].ToString() != "")
				{
					model.RuleUserID = int.Parse(ds.Tables[0].Rows[0]["RuleUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RuleCreateTime"] != null && ds.Tables[0].Rows[0]["RuleCreateTime"].ToString() != "")
				{
					model.RuleCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["RuleCreateTime"].ToString());
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
			strSql.Append("select RuleID,RuleNUmber,RuleNewsType,RuleDesc,RuleContent,RuleUserID,RuleCreateTime,UserName ");
			strSql.Append("FROM WeiXinRule,SysUser ");
			strSql.Append("where UserID=RuleUserID ");
			if (strWhere.Trim() != "")
			{
				strSql.Append("and " + strWhere);
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
			strSql.Append(" FROM WeiXinRule ");
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
			strSql.Append("select count(1) FROM WeiXinRule ");
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
			strSql.Append(")AS Row, T.*  from WeiXinRule T ");
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
			sbSql.Append("select * from (select *, ROW_NUMBER () over (order by RuleID)as a from WeiXinRule where RuleID<=4) as a");
			sbSql.Append(" union all ");
			sbSql.Append("select * from (select *, ROW_NUMBER () over (order by RuleID)as a from WeiXinRule where RuleID>=5) as b");
			return DbHelperSQL.Query(sbSql.ToString()).Tables[0];
		}

		public string Reply1()
		{
			return DbHelperSQL.Query("select RuleContent from WeiXinRule where RuleNUmber='1'").Tables[0].Rows[0][0].ToString();
		}

		public string Reply2()
		{
			return DbHelperSQL.Query("select RuleContent from WeiXinRule where RuleNUmber='2'").Tables[0].Rows[0][0].ToString();
		}

		public string GetCardDesc()
		{
			string returnStr = "";
			DataTable dt = DbHelperSQL.Query("select RuleContent from WeiXinRule where RuleNUmber='-1'").Tables[0];
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
			sbSql.Append("select * from (select *, ROW_NUMBER () over (order by RuleID)as a from WeiXinRule where RuleID<=4) as a");
			sbSql.Append(" union all ");
			sbSql.Append("select * from (select *, ROW_NUMBER () over (order by RuleID desc)as a from WeiXinRule where RuleID>=5) as b");
			DataTable dt = DbHelperSQL.Query(sbSql.ToString()).Tables[0];
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				DataRow item = dt.Rows[i];
				sbStr.AppendLine(item["RuleDesc"].ToString()).AppendLine();
			}
			return sbStr.ToString();
		}

		public Chain.Model.WeiXinRule GetModelByNewsRuleID(string RuleNUmber)
		{
			DataTable dt = DbHelperSQL.Query("select * from WeiXinRule where RuleNUmber=@RuleNUmber", new SqlParameter[]
			{
				new SqlParameter("@RuleNUmber", RuleNUmber)
			}).Tables[0];
			Chain.Model.WeiXinRule result;
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
