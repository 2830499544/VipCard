using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class WeiXinLog
	{
		public bool Exists(int WeiXinLogID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from WeiXinLog");
			strSql.Append(" where WeiXinLogID=@WeiXinLogID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@WeiXinLogID", SqlDbType.Int, 4)
			};
			parameters[0].Value = WeiXinLogID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}


        public void AddWeixinRec(string data)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert wxr values(@txt)");
            SqlParameter[] par = new SqlParameter[]{
                new SqlParameter("@txt",SqlDbType.VarChar,2000)
            };
            par[0].Value = data;
            DbHelperSQL.ExecuteSql(strSql.ToString(), par);

        }
		public int Add(Chain.Model.WeiXinLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into WeiXinLog(");
			strSql.Append("MemWeiXinCard,RecordContent,RecordContentType,StatusCode,RandomCode,ErrorTimes,WeiXinLogCreateTime)");
			strSql.Append(" values (");
			strSql.Append("@MemWeiXinCard,@RecordContent,@RecordContentType,@StatusCode,@RandomCode,@ErrorTimes,@WeiXinLogCreateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemWeiXinCard", SqlDbType.NVarChar, 50),
				new SqlParameter("@RecordContent", SqlDbType.NVarChar, 50),
				new SqlParameter("@RecordContentType", SqlDbType.Int, 4),
				new SqlParameter("@StatusCode", SqlDbType.NVarChar, 50),
				new SqlParameter("@RandomCode", SqlDbType.NVarChar, 50),
				new SqlParameter("@ErrorTimes", SqlDbType.Int, 4),
				new SqlParameter("@WeiXinLogCreateTime", SqlDbType.DateTime)
			};
			parameters[0].Value = model.MemWeiXinCard;
			parameters[1].Value = model.RecordContent;
			parameters[2].Value = model.RecordContentType;
			parameters[3].Value = model.StatusCode;
			parameters[4].Value = model.RandomCode;
			parameters[5].Value = model.ErrorTimes;
			parameters[6].Value = model.WeiXinLogCreateTime;
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

		public bool Update(Chain.Model.WeiXinLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update WeiXinLog set ");
			strSql.Append("MemWeiXinCard=@MemWeiXinCard,");
			strSql.Append("RecordContent=@RecordContent,");
			strSql.Append("RecordContentType=@RecordContentType,");
			strSql.Append("StatusCode=@StatusCode,");
			strSql.Append("RandomCode=@RandomCode,");
			strSql.Append("ErrorTimes=@ErrorTimes,");
			strSql.Append("WeiXinLogCreateTime=@WeiXinLogCreateTime");
			strSql.Append(" where WeiXinLogID=@WeiXinLogID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemWeiXinCard", SqlDbType.NVarChar, 50),
				new SqlParameter("@RecordContent", SqlDbType.NVarChar, 50),
				new SqlParameter("@RecordContentType", SqlDbType.Int, 4),
				new SqlParameter("@StatusCode", SqlDbType.NVarChar, 50),
				new SqlParameter("@RandomCode", SqlDbType.NVarChar, 50),
				new SqlParameter("@ErrorTimes", SqlDbType.Int, 4),
				new SqlParameter("@WeiXinLogCreateTime", SqlDbType.DateTime),
				new SqlParameter("@WeiXinLogID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MemWeiXinCard;
			parameters[1].Value = model.RecordContent;
			parameters[2].Value = model.RecordContentType;
			parameters[3].Value = model.StatusCode;
			parameters[4].Value = model.RandomCode;
			parameters[5].Value = model.ErrorTimes;
			parameters[6].Value = model.WeiXinLogCreateTime;
			parameters[7].Value = model.WeiXinLogID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int WeiXinLogID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from WeiXinLog ");
			strSql.Append(" where WeiXinLogID=@WeiXinLogID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@WeiXinLogID", SqlDbType.Int, 4)
			};
			parameters[0].Value = WeiXinLogID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(string MemWeiXinCard)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from WeiXinLog ");
			strSql.Append(" where MemWeiXinCard=@MemWeiXinCard");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemWeiXinCard", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = MemWeiXinCard;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string WeiXinLogIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from WeiXinLog ");
			strSql.Append(" where WeiXinLogID in (" + WeiXinLogIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.WeiXinLog GetModel(int WeiXinLogID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 WeiXinLogID,MemWeiXinCard,RecordContent,RecordContentType,StatusCode,RandomCode,ErrorTimes,WeiXinLogCreateTime from WeiXinLog ");
			strSql.Append(" where WeiXinLogID=@WeiXinLogID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@WeiXinLogID", SqlDbType.Int, 4)
			};
			parameters[0].Value = WeiXinLogID;
			Chain.Model.WeiXinLog model = new Chain.Model.WeiXinLog();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.WeiXinLog result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["WeiXinLogID"] != null && ds.Tables[0].Rows[0]["WeiXinLogID"].ToString() != "")
				{
					model.WeiXinLogID = int.Parse(ds.Tables[0].Rows[0]["WeiXinLogID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemWeiXinCard"] != null && ds.Tables[0].Rows[0]["MemWeiXinCard"].ToString() != "")
				{
					model.MemWeiXinCard = ds.Tables[0].Rows[0]["MemWeiXinCard"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RecordContent"] != null && ds.Tables[0].Rows[0]["RecordContent"].ToString() != "")
				{
					model.RecordContent = ds.Tables[0].Rows[0]["RecordContent"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RecordContentType"] != null && ds.Tables[0].Rows[0]["RecordContentType"].ToString() != "")
				{
					model.RecordContentType = int.Parse(ds.Tables[0].Rows[0]["RecordContentType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StatusCode"] != null && ds.Tables[0].Rows[0]["StatusCode"].ToString() != "")
				{
					model.StatusCode = ds.Tables[0].Rows[0]["StatusCode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RandomCode"] != null && ds.Tables[0].Rows[0]["RandomCode"].ToString() != "")
				{
					model.RandomCode = ds.Tables[0].Rows[0]["RandomCode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ErrorTimes"] != null && ds.Tables[0].Rows[0]["ErrorTimes"].ToString() != "")
				{
					model.ErrorTimes = int.Parse(ds.Tables[0].Rows[0]["ErrorTimes"].ToString());
				}
				if (ds.Tables[0].Rows[0]["WeiXinLogCreateTime"] != null && ds.Tables[0].Rows[0]["WeiXinLogCreateTime"].ToString() != "")
				{
					model.WeiXinLogCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["WeiXinLogCreateTime"].ToString());
				}
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Chain.Model.WeiXinLog GetModel(string MemWeiXinCard)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 WeiXinLogID,MemWeiXinCard,RecordContent,RecordContentType,StatusCode,RandomCode,ErrorTimes,WeiXinLogCreateTime from WeiXinLog ");
			strSql.Append(" where MemWeiXinCard=@MemWeiXinCard");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemWeiXinCard", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = MemWeiXinCard;
			Chain.Model.WeiXinLog model = new Chain.Model.WeiXinLog();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.WeiXinLog result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["WeiXinLogID"] != null && ds.Tables[0].Rows[0]["WeiXinLogID"].ToString() != "")
				{
					model.WeiXinLogID = int.Parse(ds.Tables[0].Rows[0]["WeiXinLogID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemWeiXinCard"] != null && ds.Tables[0].Rows[0]["MemWeiXinCard"].ToString() != "")
				{
					model.MemWeiXinCard = ds.Tables[0].Rows[0]["MemWeiXinCard"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RecordContent"] != null && ds.Tables[0].Rows[0]["RecordContent"].ToString() != "")
				{
					model.RecordContent = ds.Tables[0].Rows[0]["RecordContent"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RecordContentType"] != null && ds.Tables[0].Rows[0]["RecordContentType"].ToString() != "")
				{
					model.RecordContentType = int.Parse(ds.Tables[0].Rows[0]["RecordContentType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StatusCode"] != null && ds.Tables[0].Rows[0]["StatusCode"].ToString() != "")
				{
					model.StatusCode = ds.Tables[0].Rows[0]["StatusCode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RandomCode"] != null && ds.Tables[0].Rows[0]["RandomCode"].ToString() != "")
				{
					model.RandomCode = ds.Tables[0].Rows[0]["RandomCode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ErrorTimes"] != null && ds.Tables[0].Rows[0]["ErrorTimes"].ToString() != "")
				{
					model.ErrorTimes = int.Parse(ds.Tables[0].Rows[0]["ErrorTimes"].ToString());
				}
				if (ds.Tables[0].Rows[0]["WeiXinLogCreateTime"] != null && ds.Tables[0].Rows[0]["WeiXinLogCreateTime"].ToString() != "")
				{
					model.WeiXinLogCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["WeiXinLogCreateTime"].ToString());
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
			strSql.Append("select WeiXinLogID,MemWeiXinCard,RecordContent,RecordContentType,StatusCode,RandomCode,ErrorTimes,WeiXinLogCreateTime ");
			strSql.Append(" FROM WeiXinLog ");
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
			strSql.Append(" WeiXinLogID,MemWeiXinCard,RecordContent,RecordContentType,StatusCode,RandomCode,ErrorTimes,WeiXinLogCreateTime ");
			strSql.Append(" FROM WeiXinLog ");
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
			strSql.Append("select count(1) FROM WeiXinLog ");
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
				strSql.Append("order by T.WeiXinLogID desc");
			}
			strSql.Append(")AS Row, T.*  from WeiXinLog T ");
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
