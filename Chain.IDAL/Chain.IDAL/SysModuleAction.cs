using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysModuleAction
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ActionID", "SysModuleAction");
		}

		public bool Exists(int ActionID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysModuleAction");
			strSql.Append(" where ActionID=@ActionID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ActionID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ActionID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysModuleAction model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysModuleAction(");
			strSql.Append("ActionCaption,ActionControl,ActionRemark,ActionModuleID)");
			strSql.Append(" values (");
			strSql.Append("@ActionCaption,@ActionControl,@ActionRemark,@ActionModuleID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ActionCaption", SqlDbType.NVarChar, 50),
				new SqlParameter("@ActionControl", SqlDbType.VarChar, 200),
				new SqlParameter("@ActionRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@ActionModuleID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ActionCaption;
			parameters[1].Value = model.ActionControl;
			parameters[2].Value = model.ActionRemark;
			parameters[3].Value = model.ActionModuleID;
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

		public bool Update(Chain.Model.SysModuleAction model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysModuleAction set ");
			strSql.Append("ActionCaption=@ActionCaption,");
			strSql.Append("ActionControl=@ActionControl,");
			strSql.Append("ActionRemark=@ActionRemark,");
			strSql.Append("ActionModuleID=@ActionModuleID");
			strSql.Append(" where ActionID=@ActionID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ActionCaption", SqlDbType.NVarChar, 50),
				new SqlParameter("@ActionControl", SqlDbType.VarChar, 200),
				new SqlParameter("@ActionRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@ActionModuleID", SqlDbType.Int, 4),
				new SqlParameter("@ActionID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ActionCaption;
			parameters[1].Value = model.ActionControl;
			parameters[2].Value = model.ActionRemark;
			parameters[3].Value = model.ActionModuleID;
			parameters[4].Value = model.ActionID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ActionID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysModuleAction ");
			strSql.Append(" where ActionID=@ActionID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ActionID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ActionID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string ActionIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysModuleAction ");
			strSql.Append(" where ActionID in (" + ActionIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysModuleAction GetModel(int ActionID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ActionID,ActionCaption,ActionControl,ActionRemark,ActionModuleID from SysModuleAction ");
			strSql.Append(" where ActionID=@ActionID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ActionID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ActionID;
			Chain.Model.SysModuleAction model = new Chain.Model.SysModuleAction();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysModuleAction result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				result = this.DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Chain.Model.SysModuleAction DataRowToModel(DataRow row)
		{
			Chain.Model.SysModuleAction model = new Chain.Model.SysModuleAction();
			if (row != null)
			{
				if (row["ActionID"] != null && row["ActionID"].ToString() != "")
				{
					model.ActionID = int.Parse(row["ActionID"].ToString());
				}
				if (row["ActionCaption"] != null)
				{
					model.ActionCaption = row["ActionCaption"].ToString();
				}
				if (row["ActionControl"] != null)
				{
					model.ActionControl = row["ActionControl"].ToString();
				}
				if (row["ActionRemark"] != null)
				{
					model.ActionRemark = row["ActionRemark"].ToString();
				}
				if (row["ActionModuleID"] != null && row["ActionModuleID"].ToString() != "")
				{
					model.ActionModuleID = new int?(int.Parse(row["ActionModuleID"].ToString()));
				}
			}
			return model;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ActionID,ActionCaption,ActionControl,ActionRemark,ActionModuleID ");
			strSql.Append(" FROM SysModuleAction ");
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
			strSql.Append(" ActionID,ActionCaption,ActionControl,ActionRemark,ActionModuleID ");
			strSql.Append(" FROM SysModuleAction ");
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
			strSql.Append("select count(1) FROM SysModuleAction ");
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
				strSql.Append("order by T.ActionID desc");
			}
			strSql.Append(")AS Row, T.*  from SysModuleAction T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetGroupMenuAction(int PageID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("Select ActionID,ActionCaption,ActionControl,ActionRemark,ActionModuleID ");
			strSql.Append(" FROM SysModuleAction ");
			if (PageID > 0)
			{
				strSql.Append(" where ActionModuleID=" + PageID);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public Chain.Model.SysModuleAction GetModelByModuleIDAndControl(int actionModuleID, string actionControl)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * from SysModuleAction where ActionModuleID=@ActionModuleID and ActionControl=@ActionControl");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ActionModuleID", SqlDbType.Int, 4),
				new SqlParameter("@ActionControl", SqlDbType.VarChar, 200)
			};
			parameters[0].Value = actionModuleID;
			parameters[1].Value = actionControl;
			DataTable dt = DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
			Chain.Model.SysModuleAction result;
			if (dt.Rows.Count > 0)
			{
				result = this.DataRowToModel(dt.Rows[0]);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public DataSet GetGroupMenuActionNotPage(int PageID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("Select ActionID,ActionCaption,ActionControl,ActionRemark,ActionModuleID ");
			strSql.Append(" FROM SysModuleAction ");
			if (PageID > 0)
			{
				strSql.Append(" where ActionModuleID=" + PageID + " and ActionControl!='page'");
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
	}
}
