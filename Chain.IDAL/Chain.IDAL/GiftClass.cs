using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class GiftClass
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("GiftClassID", "GiftClass");
		}

		public bool Exists(int GiftClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from GiftClass");
			strSql.Append(" where GiftClassID=@GiftClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GiftClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GiftClassID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(string ClassName, int classID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.AppendFormat(" select GiftClassID,GiftClassName from GiftClass where GiftClassID not in ({0}) and GiftClassName ='{1}'", classID, ClassName);
			return DbHelperSQL.Exists(strSql.ToString());
		}

		public int Add(Chain.Model.GiftClass model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into GiftClass(");
			strSql.Append("GiftClassName,GiftClassRemark,GiftParentID)");
			strSql.Append(" values (");
			strSql.Append("@GiftClassName,@GiftClassRemark,@GiftParentID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GiftClassName", SqlDbType.VarChar, 50),
				new SqlParameter("@GiftClassRemark", SqlDbType.NVarChar, 100),
				new SqlParameter("@GiftParentID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.GiftClassName;
			parameters[1].Value = model.GiftClassRemark;
			parameters[2].Value = model.GiftParentID;
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

		public int Update(Chain.Model.GiftClass model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update GiftClass set ");
			strSql.Append("GiftClassName=@GiftClassName,");
			strSql.Append("GiftClassRemark=@GiftClassRemark,");
			strSql.Append("GiftParentID=@GiftParentID");
			strSql.Append(" where GiftClassID=@GiftClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GiftClassName", SqlDbType.VarChar, 50),
				new SqlParameter("@GiftClassRemark", SqlDbType.NVarChar, 100),
				new SqlParameter("@GiftParentID", SqlDbType.Int, 4),
				new SqlParameter("@GiftClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.GiftClassName;
			parameters[1].Value = model.GiftClassRemark;
			parameters[2].Value = model.GiftParentID;
			parameters[3].Value = model.GiftClassID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			int result;
			if (rows > 0)
			{
				result = rows;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public bool Delete(int GiftClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GiftClass ");
			strSql.Append(" where GiftClassID=@GiftClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GiftClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GiftClassID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string GiftClassIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GiftClass ");
			strSql.Append(" where GiftClassID in (" + GiftClassIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.GiftClass GetModel(int GiftClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 GiftClassID,GiftClassName,GiftClassRemark,GiftParentID from GiftClass ");
			strSql.Append(" where GiftClassID=@GiftClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GiftClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GiftClassID;
			Chain.Model.GiftClass model = new Chain.Model.GiftClass();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.GiftClass result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["GiftClassID"] != null && ds.Tables[0].Rows[0]["GiftClassID"].ToString() != "")
				{
					model.GiftClassID = int.Parse(ds.Tables[0].Rows[0]["GiftClassID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GiftClassName"] != null && ds.Tables[0].Rows[0]["GiftClassName"].ToString() != "")
				{
					model.GiftClassName = ds.Tables[0].Rows[0]["GiftClassName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GiftClassRemark"] != null && ds.Tables[0].Rows[0]["GiftClassRemark"].ToString() != "")
				{
					model.GiftClassRemark = ds.Tables[0].Rows[0]["GiftClassRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GiftParentID"] != null && ds.Tables[0].Rows[0]["GiftParentID"].ToString() != "")
				{
					model.GiftParentID = int.Parse(ds.Tables[0].Rows[0]["GiftParentID"].ToString());
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
			strSql.Append("select GiftClassID,GiftClassName,GiftClassRemark,GiftParentID ");
			strSql.Append(" FROM GiftClass ");
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
			strSql.Append(" GiftClassID,GiftClassName,GiftClassRemark,GiftParentID ");
			strSql.Append(" FROM GiftClass ");
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
			strSql.Append("select count(1) FROM GiftClass ");
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
				strSql.Append("order by T.GiftClassID desc");
			}
			strSql.Append(")AS Row, T.*  from GiftClass T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetItem(int ClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * from GiftClass ");
			strSql.Append(" where GiftClassID=@GiftClassID ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GiftClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassID;
			return DbHelperSQL.Query(strSql.ToString(), parameters);
		}

		public int DeleteByParentID(int ParentID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GiftClass ");
			strSql.Append(" where GiftParentID=@GiftParentID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GiftParentID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ParentID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			int result;
			if (rows > 0)
			{
				result = 1;
			}
			else
			{
				result = -3;
			}
			return result;
		}
	}
}
