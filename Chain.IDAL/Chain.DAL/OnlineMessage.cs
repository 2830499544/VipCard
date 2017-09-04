using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class OnlineMessage
	{
		public bool Exists(int MessageID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from OnlineMessage");
			strSql.Append(" where MessageID=@MessageID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MessageID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MessageID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public DataSet GetProposalInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "OnlineMessage";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MessageTime", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int Add(Chain.Model.OnlineMessage model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into OnlineMessage(");
			strSql.Append("IsShow,MessageType,MessageContent,MemID,MemCard,MessageTime)");
			strSql.Append(" values (");
			strSql.Append("@IsShow,@MessageType,@MessageContent,@MemID,@MemCard,@MessageTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MessageContent", SqlDbType.NVarChar, 4000),
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@MemCard", SqlDbType.NVarChar, 50),
				new SqlParameter("@MessageTime", SqlDbType.DateTime),
				new SqlParameter("@MessageType", SqlDbType.Int, 4),
				new SqlParameter("@IsShow", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MessageContent;
			parameters[1].Value = model.MemID;
			parameters[2].Value = model.MemCard;
			parameters[3].Value = model.MessageTime;
			parameters[4].Value = model.MessageType;
			parameters[5].Value = model.IsShow;
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

		public bool UpdateShowStatus(string MemCard)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update OnlineMessage set ");
			strSql.Append("IsShow=1");
			strSql.Append(" where MemCard=@MemCard");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemCard", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = MemCard;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Update(Chain.Model.OnlineMessage model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update OnlineMessage set ");
			strSql.Append("MessageContent=@MessageContent,");
			strSql.Append("MemID=@MemID,");
			strSql.Append("MemCard=@MemCard,");
			strSql.Append("MessageTime=@MessageTime");
			strSql.Append(" where MessageID=@MessageID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MessageContent", SqlDbType.NVarChar, 4000),
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@MemCard", SqlDbType.NVarChar, 50),
				new SqlParameter("@MessageTime", SqlDbType.DateTime),
				new SqlParameter("@MessageID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MessageContent;
			parameters[1].Value = model.MemID;
			parameters[2].Value = model.MemCard;
			parameters[3].Value = model.MessageTime;
			parameters[4].Value = model.MessageID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Reply(Chain.Model.OnlineMessage modelMessage)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.AppendFormat(" update OnlineMessage set ReplyContent='{0}', ReplyTime='{1}', ReplyUserID={2},IsReply={3} ", new object[]
			{
				modelMessage.ReplyContent,
				modelMessage.ReplyTime,
				modelMessage.ReplyUserID,
				modelMessage.IsReply
			});
			strSql.AppendFormat(" where MessageID={0}", modelMessage.MessageID);
			return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
		}

		public bool Delete(int MessageID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from OnlineMessage ");
			strSql.Append(" where MessageID=@MessageID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MessageID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MessageID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string MessageIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from OnlineMessage ");
			strSql.Append(" where MessageID in (" + MessageIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.OnlineMessage GetModel(int MessageID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 MessageID,MessageContent,MemID,MemCard,MessageTime from OnlineMessage ");
			strSql.Append(" where MessageID=@MessageID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MessageID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MessageID;
			Chain.Model.OnlineMessage model = new Chain.Model.OnlineMessage();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.OnlineMessage result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["MessageID"] != null && ds.Tables[0].Rows[0]["MessageID"].ToString() != "")
				{
					model.MessageID = int.Parse(ds.Tables[0].Rows[0]["MessageID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MessageContent"] != null && ds.Tables[0].Rows[0]["MessageContent"].ToString() != "")
				{
					model.MessageContent = ds.Tables[0].Rows[0]["MessageContent"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MemID"] != null && ds.Tables[0].Rows[0]["MemID"].ToString() != "")
				{
					model.MemID = new int?(int.Parse(ds.Tables[0].Rows[0]["MemID"].ToString()));
				}
				if (ds.Tables[0].Rows[0]["MemCard"] != null && ds.Tables[0].Rows[0]["MemCard"].ToString() != "")
				{
					model.MemCard = ds.Tables[0].Rows[0]["MemCard"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MessageTime"] != null && ds.Tables[0].Rows[0]["MessageTime"].ToString() != "")
				{
					model.MessageTime = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["MessageTime"].ToString()));
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
			strSql.Append("select MessageID,MessageContent,MemID,MemCard,MessageTime,ReplyContent,MessageType ");
			strSql.Append(" FROM OnlineMessage ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  * from ( select  ");
			if (Top > 0)
			{
				strSql.Append(" top " + Top.ToString());
			}
			strSql.Append(" MessageID,MessageContent,MemID,MemCard,MessageTime,ReplyContent ");
			strSql.Append(" FROM OnlineMessage ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by MessageTime desc");
			strSql.Append(")temp order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) FROM OnlineMessage ");
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
				strSql.Append("order by T.MessageID desc");
			}
			strSql.Append(")AS Row, T.*  from OnlineMessage T ");
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
