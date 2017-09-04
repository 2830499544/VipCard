using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class MemberExplanation
	{
		public bool Exists(int MemberExplanationID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemberExplanation");
			strSql.Append(" where MemberExplanationID=@MemberExplanationID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemberExplanationID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MemberExplanationID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.MemberExplanation model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MemberExplanation(");
			strSql.Append("MemberExplanationDesc,MemberExplanationTime)");
			strSql.Append(" values (");
			strSql.Append("@MemberExplanationDesc,@MemberExplanationTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemberExplanationDesc", SqlDbType.NVarChar, 400),
				new SqlParameter("@MemberExplanationTime", SqlDbType.DateTime)
			};
			parameters[0].Value = model.MemberExplanationDesc;
			parameters[1].Value = model.MemberExplanationTime;
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

		public bool Update(Chain.Model.MemberExplanation model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MemberExplanation set ");
			strSql.Append("MemberExplanationDesc=@MemberExplanationDesc,");
			strSql.Append("MemberExplanationTime=@MemberExplanationTime");
			strSql.Append(" where MemberExplanationID=@MemberExplanationID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemberExplanationDesc", SqlDbType.NVarChar, 400),
				new SqlParameter("@MemberExplanationTime", SqlDbType.DateTime),
				new SqlParameter("@MemberExplanationID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MemberExplanationDesc;
			parameters[1].Value = model.MemberExplanationTime;
			parameters[2].Value = model.MemberExplanationID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int MemberExplanationID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemberExplanation ");
			strSql.Append(" where MemberExplanationID=@MemberExplanationID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemberExplanationID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MemberExplanationID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string MemberExplanationIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemberExplanation ");
			strSql.Append(" where MemberExplanationID in (" + MemberExplanationIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MemberExplanation GetModel(int MemberExplanationID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 MemberExplanationID,MemberExplanationDesc,MemberExplanationTime from MemberExplanation ");
			strSql.Append(" where MemberExplanationID=@MemberExplanationID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemberExplanationID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MemberExplanationID;
			Chain.Model.MemberExplanation model = new Chain.Model.MemberExplanation();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MemberExplanation result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["MemberExplanationID"] != null && ds.Tables[0].Rows[0]["MemberExplanationID"].ToString() != "")
				{
					model.MemberExplanationID = int.Parse(ds.Tables[0].Rows[0]["MemberExplanationID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemberExplanationDesc"] != null && ds.Tables[0].Rows[0]["MemberExplanationDesc"].ToString() != "")
				{
					model.MemberExplanationDesc = ds.Tables[0].Rows[0]["MemberExplanationDesc"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MemberExplanationTime"] != null && ds.Tables[0].Rows[0]["MemberExplanationTime"].ToString() != "")
				{
					model.MemberExplanationTime = DateTime.Parse(ds.Tables[0].Rows[0]["MemberExplanationTime"].ToString());
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
			strSql.Append("select MemberExplanationID,MemberExplanationDesc,MemberExplanationTime ");
			strSql.Append(" FROM MemberExplanation ");
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
			strSql.Append(" MemberExplanationID,MemberExplanationDesc,MemberExplanationTime ");
			strSql.Append(" FROM MemberExplanation ");
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
			strSql.Append("select count(1) FROM MemberExplanation ");
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
				strSql.Append("order by T.MemberExplanationID desc");
			}
			strSql.Append(")AS Row, T.*  from MemberExplanation T ");
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
