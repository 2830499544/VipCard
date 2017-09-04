using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class News
	{
		public DataSet GetNewsInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "News,SysUser";
			string[] columns = new string[]
			{
				"NewsID,NewsName,NewsPhoto,NewsDesc,NewsCreateTime,UserName,NewsRemark,IsRecommend"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "NewsID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public bool Exists(int NewsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from News");
			strSql.Append(" where NewsID=@NewsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@NewsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = NewsID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.News model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into News(");
			strSql.Append("IsRecommend,CreateUserID,NewsRemark,NewsName,NewsPhoto,NewsDesc,NewsCreateTime)");
			strSql.Append(" values (");
			strSql.Append("@IsRecommend,@CreateUserID,@NewsRemark,@NewsName,@NewsPhoto,@NewsDesc,@NewsCreateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@NewsName", SqlDbType.NVarChar, 50),
				new SqlParameter("@NewsPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@NewsDesc", SqlDbType.NVarChar, 4000),
				new SqlParameter("@NewsCreateTime", SqlDbType.DateTime),
				new SqlParameter("@NewsRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@CreateUserID", SqlDbType.Int, 4),
				new SqlParameter("@IsRecommend", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.NewsName;
			parameters[1].Value = model.NewsPhoto;
			parameters[2].Value = model.NewsDesc;
			parameters[3].Value = model.NewsCreateTime;
			parameters[4].Value = model.NewsRemark;
			parameters[5].Value = model.CreateUserID;
			parameters[6].Value = model.IsRecommend;
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

		public bool Update(Chain.Model.News model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update News set ");
			strSql.Append("IsRecommend=@IsRecommend,");
			strSql.Append("NewsRemark=@NewsRemark,");
			strSql.Append("NewsName=@NewsName,");
			strSql.Append("NewsPhoto=@NewsPhoto,");
			strSql.Append("NewsDesc=@NewsDesc,");
			strSql.Append("NewsCreateTime=@NewsCreateTime");
			strSql.Append(" where NewsID=@NewsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@NewsName", SqlDbType.NVarChar, 50),
				new SqlParameter("@NewsPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@NewsDesc", SqlDbType.NVarChar, 4000),
				new SqlParameter("@NewsCreateTime", SqlDbType.DateTime),
				new SqlParameter("@NewsRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@NewsID", SqlDbType.Int, 4),
				new SqlParameter("@IsRecommend", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.NewsName;
			parameters[1].Value = model.NewsPhoto;
			parameters[2].Value = model.NewsDesc;
			parameters[3].Value = model.NewsCreateTime;
			parameters[4].Value = model.NewsRemark;
			parameters[5].Value = model.NewsID;
			parameters[6].Value = model.IsRecommend;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int NewsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from News ");
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
			strSql.Append("delete from News ");
			strSql.Append(" where NewsID in (" + NewsIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.News GetModel(int NewsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 IsRecommend,NewsRemark, NewsID,NewsName,NewsPhoto,NewsDesc,NewsCreateTime from News ");
			strSql.Append(" where NewsID=@NewsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@NewsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = NewsID;
			Chain.Model.News model = new Chain.Model.News();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.News result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["IsRecommend"] != null && ds.Tables[0].Rows[0]["IsRecommend"].ToString() != "")
				{
					model.IsRecommend = int.Parse(ds.Tables[0].Rows[0]["IsRecommend"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NewsRemark"] != null && ds.Tables[0].Rows[0]["NewsRemark"].ToString() != "")
				{
					model.NewsRemark = ds.Tables[0].Rows[0]["NewsRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["NewsID"] != null && ds.Tables[0].Rows[0]["NewsID"].ToString() != "")
				{
					model.NewsID = int.Parse(ds.Tables[0].Rows[0]["NewsID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["NewsName"] != null && ds.Tables[0].Rows[0]["NewsName"].ToString() != "")
				{
					model.NewsName = ds.Tables[0].Rows[0]["NewsName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["NewsPhoto"] != null && ds.Tables[0].Rows[0]["NewsPhoto"].ToString() != "")
				{
					model.NewsPhoto = ds.Tables[0].Rows[0]["NewsPhoto"].ToString();
				}
				if (ds.Tables[0].Rows[0]["NewsDesc"] != null && ds.Tables[0].Rows[0]["NewsDesc"].ToString() != "")
				{
					model.NewsDesc = ds.Tables[0].Rows[0]["NewsDesc"].ToString();
				}
				if (ds.Tables[0].Rows[0]["NewsCreateTime"] != null && ds.Tables[0].Rows[0]["NewsCreateTime"].ToString() != "")
				{
					model.NewsCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["NewsCreateTime"].ToString());
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
			strSql.Append("select NewsID,NewsName,NewsPhoto,NewsDesc,NewsCreateTime,IsRecommend ");
			strSql.Append(" FROM News ");
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
			strSql.Append(" NewsID,NewsName,NewsPhoto,NewsDesc,NewsCreateTime,IsRecommend ");
			strSql.Append(" FROM News ");
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
			strSql.Append("select count(1) FROM News ");
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
			strSql.Append(")AS Row, T.*  from News T ");
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
