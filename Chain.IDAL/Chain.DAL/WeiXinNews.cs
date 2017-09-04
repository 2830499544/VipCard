using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class WeiXinNews
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("NewsID", "WeiXinNews");
		}

		public bool Exists(int NewsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from WeiXinNews");
			strSql.Append(" where NewsID=@NewsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@NewsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = NewsID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.WeiXinNews model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into WeiXinNews(");
			strSql.Append("NewsRuleID,NewsTitle,NewsDesc,NewsUrlFirst,NewsUrlSecond,NewsLinkContent,NewsCreateTime)");
			strSql.Append(" values (");
			strSql.Append("@NewsRuleID,@NewsTitle,@NewsDesc,@NewsUrlFirst,@NewsUrlSecond,@NewsLinkContent,@NewsCreateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@NewsRuleID", SqlDbType.Int, 4),
				new SqlParameter("@NewsTitle", SqlDbType.NVarChar, 500),
				new SqlParameter("@NewsDesc", SqlDbType.NVarChar, 1000),
				new SqlParameter("@NewsUrlFirst", SqlDbType.NVarChar, 200),
				new SqlParameter("@NewsUrlSecond", SqlDbType.NVarChar, 200),
				new SqlParameter("@NewsLinkContent", SqlDbType.NVarChar, 2147483647),
				new SqlParameter("@NewsCreateTime", SqlDbType.DateTime)
			};
			parameters[0].Value = model.NewsRuleID;
			parameters[1].Value = model.NewsTitle;
			parameters[2].Value = model.NewsDesc;
			parameters[3].Value = model.NewsUrlFirst;
			parameters[4].Value = model.NewsUrlSecond;
			parameters[5].Value = model.NewsLinkContent;
			parameters[6].Value = model.NewsCreateTime;
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

		public bool Update(Chain.Model.WeiXinNews model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update WeiXinNews set ");
			strSql.Append("NewsRuleID=@NewsRuleID,");
			strSql.Append("NewsTitle=@NewsTitle,");
			strSql.Append("NewsDesc=@NewsDesc,");
			strSql.Append("NewsUrlFirst=@NewsUrlFirst,");
			strSql.Append("NewsUrlSecond=@NewsUrlSecond,");
			strSql.Append("NewsLinkContent=@NewsLinkContent,");
			strSql.Append("NewsCreateTime=@NewsCreateTime");
			strSql.Append(" where NewsID=@NewsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@NewsRuleID", SqlDbType.Int, 4),
				new SqlParameter("@NewsTitle", SqlDbType.NVarChar, 500),
				new SqlParameter("@NewsDesc", SqlDbType.NVarChar, 1000),
				new SqlParameter("@NewsUrlFirst", SqlDbType.NVarChar, 200),
				new SqlParameter("@NewsUrlSecond", SqlDbType.NVarChar, 200),
				new SqlParameter("@NewsLinkContent", SqlDbType.NVarChar, 2147483647),
				new SqlParameter("@NewsCreateTime", SqlDbType.DateTime),
				new SqlParameter("@NewsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.NewsRuleID;
			parameters[1].Value = model.NewsTitle;
			parameters[2].Value = model.NewsDesc;
			parameters[3].Value = model.NewsUrlFirst;
			parameters[4].Value = model.NewsUrlSecond;
			parameters[5].Value = model.NewsLinkContent;
			parameters[6].Value = model.NewsCreateTime;
			parameters[7].Value = model.NewsID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int NewsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from WeiXinNews ");
			strSql.Append(" where NewsID=@NewsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@NewsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = NewsID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string NewsIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from WeiXinNews ");
			strSql.Append(" where NewsID in (" + NewsIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.WeiXinNews GetModel(int NewsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 NewsID,NewsRuleID,NewsTitle,NewsDesc,NewsUrlFirst,NewsUrlSecond,NewsLinkContent,NewsCreateTime from WeiXinNews ");
			strSql.Append(" where NewsID=@NewsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@NewsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = NewsID;
			Chain.Model.WeiXinNews model = new Chain.Model.WeiXinNews();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.WeiXinNews result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["NewsID"] != null && ds.Tables[0].Rows[0]["NewsID"].ToString() != "")
				{
					model.NewsID = int.Parse(ds.Tables[0].Rows[0]["NewsID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NewsRuleID"] != null && ds.Tables[0].Rows[0]["NewsRuleID"].ToString() != "")
				{
					model.NewsRuleID = int.Parse(ds.Tables[0].Rows[0]["NewsRuleID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NewsTitle"] != null && ds.Tables[0].Rows[0]["NewsTitle"].ToString() != "")
				{
					model.NewsTitle = ds.Tables[0].Rows[0]["NewsTitle"].ToString();
				}
				if (ds.Tables[0].Rows[0]["NewsDesc"] != null && ds.Tables[0].Rows[0]["NewsDesc"].ToString() != "")
				{
					model.NewsDesc = ds.Tables[0].Rows[0]["NewsDesc"].ToString();
				}
				if (ds.Tables[0].Rows[0]["NewsUrlFirst"] != null && ds.Tables[0].Rows[0]["NewsUrlFirst"].ToString() != "")
				{
					model.NewsUrlFirst = ds.Tables[0].Rows[0]["NewsUrlFirst"].ToString();
				}
				if (ds.Tables[0].Rows[0]["NewsUrlSecond"] != null && ds.Tables[0].Rows[0]["NewsUrlSecond"].ToString() != "")
				{
					model.NewsUrlSecond = ds.Tables[0].Rows[0]["NewsUrlSecond"].ToString();
				}
				if (ds.Tables[0].Rows[0]["NewsCreateTime"] != null && ds.Tables[0].Rows[0]["NewsCreateTime"].ToString() != "")
				{
					model.NewsCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["NewsCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NewsLinkContent"] != null && ds.Tables[0].Rows[0]["NewsLinkContent"].ToString() != "")
				{
					model.NewsLinkContent = ds.Tables[0].Rows[0]["NewsLinkContent"].ToString();
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
			strSql.Append("select NewsID,NewsRuleID,NewsTitle,NewsDesc,NewsUrlFirst,NewsUrlSecond,NewsLinkContent,NewsCreateTime,RuleNUmber ");
			strSql.Append(" FROM WeiXinNews,WeiXinRule ");
			strSql.Append("where NewsRuleID=RuleID ");
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
			strSql.Append(" NewsID,NewsRuleID,NewsTitle,NewsDesc,NewsUrlFirst,NewsUrlSecond,NewsCreateTime ");
			strSql.Append(" FROM WeiXinNews ");
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
			strSql.Append("select count(1) FROM WeiXinNews ");
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
				strSql.Append("order by T.NewsID desc");
			}
			strSql.Append(")AS Row, T.*  from WeiXinNews T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataTable GetParent()
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ");
			strSql.Append("WeiXinRule.*,UserName,(select count(*) from WeiXinNews where NewsRuleID=RuleID) as NewsThisCount ");
			strSql.Append("from WeiXinRule ,SysUser ");
			strSql.Append("where RuleNewsType='news' ");
			strSql.Append("and UserID=RuleUserID");
			return DbHelperSQL.Query(strSql.ToString()).Tables[0];
		}
	}
}
