using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class EmailLog
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("EmailID", "EmailLog");
		}

		public bool Exists(int EmailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from EmailLog");
			strSql.Append(" where EmailID=@EmailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@EmailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = EmailID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.EmailLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into EmailLog(");
			strSql.Append("EmailAdress,EmailTitle,EmailContent,EmailState,EmailSendTime,EmailShopID,EmailUserID,EmailCount)");
			strSql.Append(" values (");
			strSql.Append("@EmailAdress,@EmailTitle,@EmailContent,@EmailState,@EmailSendTime,@EmailShopID,@EmailUserID,@EmailCount)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@EmailAdress", SqlDbType.NVarChar, 100),
				new SqlParameter("@EmailTitle", SqlDbType.NVarChar, 500),
				new SqlParameter("@EmailContent", SqlDbType.NVarChar, 500),
				new SqlParameter("@EmailState", SqlDbType.Int, 4),
				new SqlParameter("@EmailSendTime", SqlDbType.DateTime),
				new SqlParameter("@EmailShopID", SqlDbType.Int, 4),
				new SqlParameter("@EmailUserID", SqlDbType.Int, 4),
				new SqlParameter("@EmailCount", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.EmailAdress;
			parameters[1].Value = model.EmailTitle;
			parameters[2].Value = model.EmailContent;
			parameters[3].Value = model.EmailState;
			parameters[4].Value = model.EmailSendTime;
			parameters[5].Value = model.EmailShopID;
			parameters[6].Value = model.EmailUserID;
			parameters[7].Value = model.EmailCount;
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

		public bool Update(Chain.Model.EmailLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update EmailLog set ");
			strSql.Append("EmailAdress=@EmailAdress,");
			strSql.Append("EmailTitle=@EmailTitle,");
			strSql.Append("EmailContent=@EmailContent,");
			strSql.Append("EmailState=@EmailState,");
			strSql.Append("EmailSendTime=@EmailSendTime,");
			strSql.Append("EmailShopID=@EmailShopID,");
			strSql.Append("EmailUserID=@EmailUserID,");
			strSql.Append("EmailCount=@EmailCount");
			strSql.Append(" where EmailID=@EmailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@EmailAdress", SqlDbType.NVarChar, 100),
				new SqlParameter("@EmailTitle", SqlDbType.NVarChar, 500),
				new SqlParameter("@EmailContent", SqlDbType.NVarChar, 500),
				new SqlParameter("@EmailState", SqlDbType.Int, 4),
				new SqlParameter("@EmailSendTime", SqlDbType.DateTime),
				new SqlParameter("@EmailShopID", SqlDbType.Int, 4),
				new SqlParameter("@EmailUserID", SqlDbType.Int, 4),
				new SqlParameter("@EmailCount", SqlDbType.Int, 4),
				new SqlParameter("@EmailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.EmailAdress;
			parameters[1].Value = model.EmailTitle;
			parameters[2].Value = model.EmailContent;
			parameters[3].Value = model.EmailState;
			parameters[4].Value = model.EmailSendTime;
			parameters[5].Value = model.EmailShopID;
			parameters[6].Value = model.EmailUserID;
			parameters[7].Value = model.EmailCount;
			parameters[8].Value = model.EmailID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int EmailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from EmailLog ");
			strSql.Append(" where EmailID=@EmailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@EmailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = EmailID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string EmailIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from EmailLog ");
			strSql.Append(" where EmailID in (" + EmailIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.EmailLog GetModel(int EmailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 EmailID,EmailAdress,EmailTitle,EmailContent,EmailState,EmailSendTime,EmailShopID,EmailUserID,EmailCount from EmailLog ");
			strSql.Append(" where EmailID=@EmailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@EmailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = EmailID;
			Chain.Model.EmailLog model = new Chain.Model.EmailLog();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.EmailLog result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["EmailID"] != null && ds.Tables[0].Rows[0]["EmailID"].ToString() != "")
				{
					model.EmailID = int.Parse(ds.Tables[0].Rows[0]["EmailID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["EmailAdress"] != null && ds.Tables[0].Rows[0]["EmailAdress"].ToString() != "")
				{
					model.EmailAdress = ds.Tables[0].Rows[0]["EmailAdress"].ToString();
				}
				if (ds.Tables[0].Rows[0]["EmailTitle"] != null && ds.Tables[0].Rows[0]["EmailTitle"].ToString() != "")
				{
					model.EmailTitle = ds.Tables[0].Rows[0]["EmailTitle"].ToString();
				}
				if (ds.Tables[0].Rows[0]["EmailContent"] != null && ds.Tables[0].Rows[0]["EmailContent"].ToString() != "")
				{
					model.EmailContent = ds.Tables[0].Rows[0]["EmailContent"].ToString();
				}
				if (ds.Tables[0].Rows[0]["EmailState"] != null && ds.Tables[0].Rows[0]["EmailState"].ToString() != "")
				{
					model.EmailState = int.Parse(ds.Tables[0].Rows[0]["EmailState"].ToString());
				}
				if (ds.Tables[0].Rows[0]["EmailSendTime"] != null && ds.Tables[0].Rows[0]["EmailSendTime"].ToString() != "")
				{
					model.EmailSendTime = DateTime.Parse(ds.Tables[0].Rows[0]["EmailSendTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["EmailShopID"] != null && ds.Tables[0].Rows[0]["EmailShopID"].ToString() != "")
				{
					model.EmailShopID = int.Parse(ds.Tables[0].Rows[0]["EmailShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["EmailUserID"] != null && ds.Tables[0].Rows[0]["EmailUserID"].ToString() != "")
				{
					model.EmailUserID = int.Parse(ds.Tables[0].Rows[0]["EmailUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["EmailCount"] != null && ds.Tables[0].Rows[0]["EmailCount"].ToString() != "")
				{
					model.EmailCount = int.Parse(ds.Tables[0].Rows[0]["EmailCount"].ToString());
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
			strSql.Append("select EmailID,EmailAdress,EmailTitle,EmailContent,EmailState,EmailSendTime,EmailShopID,EmailUserID,EmailCount ");
			strSql.Append(" FROM EmailLog ");
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
			strSql.Append(" EmailID,EmailAdress,EmailTitle,EmailContent,EmailState,EmailSendTime,EmailShopID,EmailUserID,EmailCount ");
			strSql.Append(" FROM EmailLog ");
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
			strSql.Append("select count(1) FROM EmailLog ");
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
				strSql.Append("order by T.EmailID desc");
			}
			strSql.Append(")AS Row, T.*  from EmailLog T ");
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
			string tableName = "SysShop,EmailLog,SysUser";
			string[] columns = new string[]
			{
				"SysShop.ShopName,EmailLog.*,SysUser.UserName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "EmailID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int UpdateEmail(int intID, int state)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" Update EmailLog set EmailState=@EmailState ");
			strSql.Append(" where EmailID=@EmailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@EmailState", SqlDbType.Int, 4),
				new SqlParameter("@EmailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = state;
			parameters[1].Value = intID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public int UpdateEmailCount(int intID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" Update EmailLog set EmailCount=EmailCount+1 ");
			strSql.Append(" where EmailID=@EmailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@EmailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = intID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public int EmailResend(int intEamilID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" Update EmailLog set EmailState=0, ");
			strSql.Append(" EmailCount=0 ");
			strSql.Append(" where EmailID=@EmailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@EmailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = intEamilID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}
	}
}
