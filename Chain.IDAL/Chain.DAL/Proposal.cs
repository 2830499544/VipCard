using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class Proposal
	{
		public bool Exists(int ProposalID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from Proposal");
			strSql.Append(" where ProposalID=@ProposalID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProposalID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ProposalID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.Proposal model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Proposal(");
			strSql.Append("ProposalContent,MemID,MemMobile,ProposalTime)");
			strSql.Append(" values (");
			strSql.Append("@ProposalContent,@MemID,@MemMobile,@ProposalTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProposalContent", SqlDbType.NVarChar, 4000),
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@MemMobile", SqlDbType.NVarChar, 50),
				new SqlParameter("@ProposalTime", SqlDbType.DateTime)
			};
			parameters[0].Value = model.ProposalContent;
			parameters[1].Value = model.MemID;
			parameters[2].Value = model.MemMobile;
			parameters[3].Value = model.ProposalTime;
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

		public bool Update(Chain.Model.Proposal model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update Proposal set ");
			strSql.Append("ProposalContent=@ProposalContent,");
			strSql.Append("MemID=@MemID,");
			strSql.Append("MemMobile=@MemMobile,");
			strSql.Append("ProposalTime=@ProposalTime");
			strSql.Append(" where ProposalID=@ProposalID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProposalContent", SqlDbType.NVarChar, 4000),
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@MemMobile", SqlDbType.NVarChar, 50),
				new SqlParameter("@ProposalTime", SqlDbType.DateTime),
				new SqlParameter("@ProposalID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ProposalContent;
			parameters[1].Value = model.MemID;
			parameters[2].Value = model.MemMobile;
			parameters[3].Value = model.ProposalTime;
			parameters[4].Value = model.ProposalID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ProposalID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Proposal ");
			strSql.Append(" where ProposalID=@ProposalID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProposalID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ProposalID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string ProposalIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Proposal ");
			strSql.Append(" where ProposalID in (" + ProposalIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.Proposal GetModel(int ProposalID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ProposalID,ProposalContent,MemID,MemMobile,ProposalTime from Proposal ");
			strSql.Append(" where ProposalID=@ProposalID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProposalID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ProposalID;
			Chain.Model.Proposal model = new Chain.Model.Proposal();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.Proposal result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ProposalID"] != null && ds.Tables[0].Rows[0]["ProposalID"].ToString() != "")
				{
					model.ProposalID = int.Parse(ds.Tables[0].Rows[0]["ProposalID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ProposalContent"] != null && ds.Tables[0].Rows[0]["ProposalContent"].ToString() != "")
				{
					model.ProposalContent = ds.Tables[0].Rows[0]["ProposalContent"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MemID"] != null && ds.Tables[0].Rows[0]["MemID"].ToString() != "")
				{
					model.MemID = new int?(int.Parse(ds.Tables[0].Rows[0]["MemID"].ToString()));
				}
				if (ds.Tables[0].Rows[0]["MemMobile"] != null && ds.Tables[0].Rows[0]["MemMobile"].ToString() != "")
				{
					model.MemMobile = ds.Tables[0].Rows[0]["MemMobile"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ProposalTime"] != null && ds.Tables[0].Rows[0]["ProposalTime"].ToString() != "")
				{
					model.ProposalTime = new DateTime?(DateTime.Parse(ds.Tables[0].Rows[0]["ProposalTime"].ToString()));
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
			strSql.Append("select ProposalID,ProposalContent,MemID,MemMobile,ProposalTime ");
			strSql.Append(" FROM Proposal ");
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
			strSql.Append(" ProposalID,ProposalContent,MemID,MemMobile,ProposalTime ");
			strSql.Append(" FROM Proposal ");
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
			strSql.Append("select count(1) FROM Proposal ");
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
				strSql.Append("order by T.ProposalID desc");
			}
			strSql.Append(")AS Row, T.*  from Proposal T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetProposalInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "Proposal,Mem,SysShop";
			string[] columns = new string[]
			{
				"ProposalID,MemName,MemCard,MemSex,ProposalContent,Proposal.MemMobile,ShopName,ProposalTime"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ProposalID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
