using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysGroupAuthority
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("GAID", "SysGroupAuthority");
		}

		public bool Exists(int GAID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysGroupAuthority");
			strSql.Append(" where GAID=@GAID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GAID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GAID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysGroupAuthority model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysGroupAuthority(");
			strSql.Append("GroupID,ModuleID,ActionID,ActionValue)");
			strSql.Append(" values (");
			strSql.Append("@GroupID,@ModuleID,@ActionID,@ActionValue)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GroupID", SqlDbType.Int, 4),
				new SqlParameter("@ModuleID", SqlDbType.Int, 4),
				new SqlParameter("@ActionID", SqlDbType.Int, 4),
				new SqlParameter("@ActionValue", SqlDbType.Bit, 1)
			};
			parameters[0].Value = model.GroupID;
			parameters[1].Value = model.ModuleID;
			parameters[2].Value = model.ActionID;
			parameters[3].Value = model.ActionValue;
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

		public bool Update(Chain.Model.SysGroupAuthority model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysGroupAuthority set ");
			strSql.Append("GroupID=@GroupID,");
			strSql.Append("ModuleID=@ModuleID,");
			strSql.Append("ActionID=@ActionID,");
			strSql.Append("ActionValue=@ActionValue");
			strSql.Append(" where GAID=@GAID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GroupID", SqlDbType.Int, 4),
				new SqlParameter("@ModuleID", SqlDbType.Int, 4),
				new SqlParameter("@ActionID", SqlDbType.Int, 4),
				new SqlParameter("@ActionValue", SqlDbType.Bit, 1),
				new SqlParameter("@GAID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.GroupID;
			parameters[1].Value = model.ModuleID;
			parameters[2].Value = model.ActionID;
			parameters[3].Value = model.ActionValue;
			parameters[4].Value = model.GAID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int GAID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysGroupAuthority ");
			strSql.Append(" where GAID=@GAID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GAID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GAID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string GAIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysGroupAuthority ");
			strSql.Append(" where GAID in (" + GAIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public bool DeleteList(int GroupID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysGroupAuthority ");
			strSql.Append(" where GroupID=" + GroupID);
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysGroupAuthority GetModel(int GAID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 GAID,GroupID,ModuleID,ActionID,ActionValue from SysGroupAuthority ");
			strSql.Append(" where GAID=@GAID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GAID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GAID;
			Chain.Model.SysGroupAuthority model = new Chain.Model.SysGroupAuthority();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysGroupAuthority result;
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

		public Chain.Model.SysGroupAuthority DataRowToModel(DataRow row)
		{
			Chain.Model.SysGroupAuthority model = new Chain.Model.SysGroupAuthority();
			if (row != null)
			{
				if (row["GAID"] != null && row["GAID"].ToString() != "")
				{
					model.GAID = int.Parse(row["GAID"].ToString());
				}
				if (row["GroupID"] != null && row["GroupID"].ToString() != "")
				{
					model.GroupID = new int?(int.Parse(row["GroupID"].ToString()));
				}
				if (row["ModuleID"] != null && row["ModuleID"].ToString() != "")
				{
					model.ModuleID = new int?(int.Parse(row["ModuleID"].ToString()));
				}
				if (row["ActionID"] != null && row["ActionID"].ToString() != "")
				{
					model.ActionID = new int?(int.Parse(row["ActionID"].ToString()));
				}
				if (row["ActionValue"] != null && row["ActionValue"].ToString() != "")
				{
					if (row["ActionValue"].ToString() == "1" || row["ActionValue"].ToString().ToLower() == "true")
					{
						model.ActionValue = true;
					}
					else
					{
						model.ActionValue = false;
					}
				}
			}
			return model;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select GAID,GroupID,ModuleID,ActionID,ActionValue ");
			strSql.Append(" FROM SysGroupAuthority ");
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
			strSql.Append(" GAID,GroupID,ModuleID,ActionID,ActionValue ");
			strSql.Append(" FROM SysGroupAuthority ");
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
			strSql.Append("select count(1) FROM SysGroupAuthority ");
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
				strSql.Append("order by T.GAID desc");
			}
			strSql.Append(")AS Row, T.*  from SysGroupAuthority T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataTable GetList(int groupID, int moduleID, int actionID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * from SysGroupAuthority ");
			strSql.Append("where GroupID=@groupID ");
			strSql.Append("and ModuleID=@moduleID ");
			strSql.Append("and ActionID=@actionID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@groupID", SqlDbType.Int, 4),
				new SqlParameter("@moduleID", SqlDbType.Int, 4),
				new SqlParameter("@actionID", SqlDbType.Int, 4)
			};
			parameters[0].Value = groupID;
			parameters[1].Value = moduleID;
			parameters[2].Value = actionID;
			return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
		}

		public bool CheckChildGroup(int GroupID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysGroupAuthority set ActionValue = 0 ");
			strSql.Append("where ");
			strSql.Append("GroupID in (select GroupID from SysGroup where ParentIDStr like @GroupIDStr) ");
			strSql.Append("and ActionID not in (select ActionID from SysGroupAuthority where GroupID = @GroupID and ActionValue=1)");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GroupID", SqlDbType.Int, 4),
				new SqlParameter("@GroupIDStr", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = GroupID;
			parameters[1].Value = "%/" + GroupID + "/%";
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public int getModuleID(string AbsolutePath)
		{
			int ModuleID = 0;
			StringBuilder SqlCommandtxt = new StringBuilder();
			SqlCommandtxt.AppendFormat("DECLARE @Url NVARCHAR(100);SET @Url='{0}';DECLARE @Pid INT ;SET @Pid=0;SELECT TOP 1 @Pid=a.ModuleID FROM dbo.SysModule a WITH(NOLOCK) WHERE a .ModuleParentID>0 AND ModuleLink=@Url;IF @Pid=0 BEGIN SET @Url=@Url+'?PID='; SELECT TOP 1 @Pid=a.ModuleID FROM dbo.SysModule a WITH(NOLOCK) WHERE a.ModuleLink=@Url+CAST(a.ModuleID AS NVARCHAR(10)); END SELECT @Pid AS PID", AbsolutePath);
			DataTable dt = DbHelperSQL.Query(SqlCommandtxt.ToString()).Tables[0];
			if (dt != null)
			{
				ModuleID = int.Parse(dt.Rows[0][0].ToString());
			}
			return ModuleID;
		}
	}
}
