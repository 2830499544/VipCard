using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class Message
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("MessageID", "Message");
		}

		public bool Exists(int MessageID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from Message");
			strSql.Append(" where MessageID=@MessageID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MessageID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MessageID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.Message model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Message(");
			strSql.Append("MessageMemID,MessageContent,MessageTime,MessageIsReply)");
			strSql.Append(" values (");
			strSql.Append("@MessageMemID,@MessageContent,@MessageTime,@MessageIsReply)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MessageMemID", SqlDbType.Int, 4),
				new SqlParameter("@MessageContent", SqlDbType.NVarChar, 4000),
				new SqlParameter("@MessageTime", SqlDbType.DateTime),
				new SqlParameter("@MessageIsReply", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MessageMemID;
			parameters[1].Value = model.MessageContent;
			parameters[2].Value = model.MessageTime;
			parameters[3].Value = model.MessageIsReply;
			object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
			int result;
			if (obj == null)
			{
				result = 0;
			}
			else
			{
				result = 1;
			}
			return result;
		}

		public bool Update(Chain.Model.Message model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update Message set ");
			strSql.Append("MessageMemID=@MessageMemID,");
			strSql.Append("MessageContent=@MessageContent,");
			strSql.Append("MessageTime=@MessageTime,");
			strSql.Append("MessageIsReply=@MessageIsReply,");
			strSql.Append("MessageReplyContent=@MessageReplyContent,");
			strSql.Append("MessageReplyTime=@MessageReplyTime,");
			strSql.Append("MessageReplyUserID=@MessageReplyUserID");
			strSql.Append(" where MessageID=@MessageID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MessageMemID", SqlDbType.Int, 4),
				new SqlParameter("@MessageContent", SqlDbType.NVarChar, 4000),
				new SqlParameter("@MessageTime", SqlDbType.DateTime),
				new SqlParameter("@MessageIsReply", SqlDbType.Int, 4),
				new SqlParameter("@MessageReplyContent", SqlDbType.NVarChar, 4000),
				new SqlParameter("@MessageReplyTime", SqlDbType.DateTime),
				new SqlParameter("@MessageReplyUserID", SqlDbType.Int, 4),
				new SqlParameter("@MessageID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MessageMemID;
			parameters[1].Value = model.MessageContent;
			parameters[2].Value = model.MessageTime;
			parameters[3].Value = model.MessageIsReply;
			parameters[4].Value = model.MessageReplyContent;
			parameters[5].Value = model.MessageReplyTime;
			parameters[6].Value = model.MessageReplyUserID;
			parameters[7].Value = model.MessageID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int MessageID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Message ");
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
			strSql.Append("delete from Message ");
			strSql.Append(" where MessageID in (" + MessageIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.Message GetModel(int MessageID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 MessageID,MessageMemID,MessageContent,MessageTime,MessageIsReply,MessageReplyContent,MessageReplyTime,MessageReplyUserID from Message ");
			strSql.Append(" where MessageID=@MessageID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MessageID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MessageID;
			Chain.Model.Message model = new Chain.Model.Message();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.Message result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["MessageID"] != null && ds.Tables[0].Rows[0]["MessageID"].ToString() != "")
				{
					model.MessageID = int.Parse(ds.Tables[0].Rows[0]["MessageID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MessageMemID"] != null && ds.Tables[0].Rows[0]["MessageMemID"].ToString() != "")
				{
					model.MessageMemID = int.Parse(ds.Tables[0].Rows[0]["MessageMemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MessageContent"] != null && ds.Tables[0].Rows[0]["MessageContent"].ToString() != "")
				{
					model.MessageContent = ds.Tables[0].Rows[0]["MessageContent"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MessageTime"] != null && ds.Tables[0].Rows[0]["MessageTime"].ToString() != "")
				{
					model.MessageTime = DateTime.Parse(ds.Tables[0].Rows[0]["MessageTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MessageIsReply"] != null && ds.Tables[0].Rows[0]["MessageIsReply"].ToString() != "")
				{
					model.MessageIsReply = int.Parse(ds.Tables[0].Rows[0]["MessageIsReply"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MessageReplyContent"] != null && ds.Tables[0].Rows[0]["MessageReplyContent"].ToString() != "")
				{
					model.MessageReplyContent = ds.Tables[0].Rows[0]["MessageReplyContent"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MessageReplyTime"] != null && ds.Tables[0].Rows[0]["MessageReplyTime"].ToString() != "")
				{
					model.MessageReplyTime = DateTime.Parse(ds.Tables[0].Rows[0]["MessageReplyTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MessageReplyUserID"] != null && ds.Tables[0].Rows[0]["MessageReplyUserID"].ToString() != "")
				{
					model.MessageReplyUserID = int.Parse(ds.Tables[0].Rows[0]["MessageReplyUserID"].ToString());
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
			strSql.Append("select MessageID,MessageMemID,MessageContent,MessageTime,MessageIsReply,MessageReplyContent,MessageReplyTime,MessageReplyUserID ");
			strSql.Append(" FROM Message ");
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
			strSql.Append(" MessageID,MessageMemID,MessageContent,MessageTime,MessageIsReply,MessageReplyContent,MessageReplyTime,MessageReplyUserID ");
			strSql.Append(" FROM Message ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetMemMessageList(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = " [Message] ";
			string[] columns = new string[]
			{
				" [Message].* "
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MessageID", "MessageID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetListSPInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = " [Message],Mem ";
			string[] columns = new string[]
			{
				" [Message].*,MemCard,MemName,(select UserName from SysUser where [Message].MessageReplyUserID=SysUser.UserID) as UserName "
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MessageID", "MessageID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) FROM Message ");
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
			strSql.Append(")AS Row, T.*  from Message T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "Message,Mem,SysShop";
			string[] columns = new string[]
			{
				"MemID, MemCard,MemName,MemMobile,ShopName,avg(MessageIsReply) as MessageIsReply ,Max(MessageTime) as MessageTime, count(MessageID) as MessageCount"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MemID", true, PageSize, PageIndex, recordCount);
			try
			{
				resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			}
			catch
			{
				resCount = 0;
			}
			return ds;
		}

		public DataSet GetMessenger(string strWhere)
		{
			DataSet ds = new DataSet();
			StringBuilder sb = new StringBuilder();
			sb.Append("select MemID, MemCard,MemName,MemMobile,ShopName,avg(MessageIsReply) as MessageIsReply ,Max(MessageTime) as MessageTime, count(MessageID) as MessageCount from Message,Mem,SysShop ");
			if (strWhere != "")
			{
				sb.Append(" where " + strWhere);
			}
			sb.Append(" group by MemCard,MemName,MemMobile,ShopName,MemID ");
			return DbHelperSQL.Query(sb.ToString());
		}

		public int MessageDel(int intMemID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Message ");
			strSql.Append(" where MessageMemID=@MessageMemID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MessageMemID", SqlDbType.Int, 4)
			};
			parameters[0].Value = intMemID;
			object obj = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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

		public bool Reply(Chain.Model.Message modelMessage)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.AppendFormat(" update Message set MessageReplyContent='{0}', MessageReplyTime='{1}', MessageReplyUserID={2}, MessageIsReply={3} ", new object[]
			{
				modelMessage.MessageReplyContent,
				modelMessage.MessageReplyTime,
				modelMessage.MessageReplyUserID,
				modelMessage.MessageIsReply
			});
			strSql.AppendFormat(" where MessageID={0}", modelMessage.MessageID);
			return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
		}
	}
}
