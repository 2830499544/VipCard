using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysArea
	{
		public string GetNameByID(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select Name from SysArea ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
			string result;
			if (obj != null)
			{
				result = obj.ToString();
			}
			else
			{
				result = "";
			}
			return result;
		}

		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID", "SysArea");
		}

		public bool Exists(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysArea");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(int PID, string Name)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select count(1) from SysArea");
			strSql.Append(" where PID=@PID and Name=@Name ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("PID", SqlDbType.Int, 4),
				new SqlParameter("Name", SqlDbType.VarChar, 50)
			};
			parameters[0].Value = PID;
			parameters[1].Value = Name;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(int ID, int PID, string Name)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select count(1) from SysArea");
			strSql.Append(" where ID not in (@ID) and  PID=@PID and Name=@Name ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("ID", SqlDbType.Int, 4),
				new SqlParameter("PID", SqlDbType.Int),
				new SqlParameter("Name", SqlDbType.VarChar, 50)
			};
			parameters[0].Value = ID;
			parameters[1].Value = PID;
			parameters[2].Value = Name;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysArea model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysArea(");
			strSql.Append("PID,Name)");
			strSql.Append(" values (");
			strSql.Append("@PID,@Name)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PID", SqlDbType.Int, 4),
				new SqlParameter("@Name", SqlDbType.NVarChar, 100)
			};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.Name;
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

		public int Update(Chain.Model.SysArea model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysArea set ");
			strSql.Append("PID=@PID,");
			strSql.Append("Name=@Name");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PID", SqlDbType.Int, 4),
				new SqlParameter("@Name", SqlDbType.NVarChar, 100),
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.PID;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.ID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			int result;
			if (rows > 0)
			{
				result = rows;
			}
			else
			{
				result = -1;
			}
			return result;
		}

		public bool Delete(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysArea ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string IDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysArea ");
			strSql.Append(" where ID in (" + IDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysArea GetModel(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ID,PID,Name from SysArea ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			Chain.Model.SysArea model = new Chain.Model.SysArea();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysArea result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
				{
					model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PID"] != null && ds.Tables[0].Rows[0]["PID"].ToString() != "")
				{
					model.PID = int.Parse(ds.Tables[0].Rows[0]["PID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Name"] != null && ds.Tables[0].Rows[0]["Name"].ToString() != "")
				{
					model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
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
			strSql.Append("select ID,PID,Name ");
			strSql.Append(" FROM SysArea ");
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
			strSql.Append(" ID,PID,Name ");
			strSql.Append(" FROM SysArea ");
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
			strSql.Append("select count(1) FROM SysArea ");
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from SysArea T ");
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
			string tableName = "SysArea";
			string[] columns = new string[]
			{
				"SysArea.*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ID", "ID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
