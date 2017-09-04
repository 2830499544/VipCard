using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysGroup
	{
		public bool UpdateParentId(int OldParentGroupID, int NewParentGroupID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysGroup set ");
			strSql.Append("ParentGroupID=@NewParentGroupID");
			strSql.Append(" where ParentGroupID=@OldParentGroupID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@NewParentGroupID", SqlDbType.Int, 4),
				new SqlParameter("@OldParentGroupID", SqlDbType.Int, 4)
			};
			parameters[0].Value = NewParentGroupID;
			parameters[1].Value = OldParentGroupID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public DataSet GetGroupAuthority(int GroupID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM V_GroupAuthority ");
			strSql.Append(" where GroupID=" + GroupID);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetGroupAuthority(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM V_GroupAuthority ");
			strSql.Append(" where " + strWhere);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListS(int PageSize, int PageIndex, string[] strWhere, out int resCount)
		{
			string tableName = "SysGroup";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "GroupID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataTable GetSysGroupByID(int groupID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.AppendFormat("with GroupTree AS (SELECT * from dbo.SysGroup where GroupID={0} UNION ALL  SELECT dbo.SysGroup.* from GroupTree JOIN dbo.SysGroup on GroupTree.ParentGroupID= dbo.SysGroup.GroupID) SELECT * from GroupTree;", groupID);
			return DbHelperSQL.Query(strSql.ToString()).Tables[0];
		}

		public DataTable GetSysGroupByParentID(int groupID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.AppendFormat("WITH CityTree AS (SELECT * from dbo.SysGroup where ParentGroupID = 1 UNION ALL SELECT dbo.SysGroup.* from Citytree JOIN dbo.SysGroup on CityTree.GroupID = dbo.SysGroup.ParentGroupID)SELECT* FROM CityTree; ", groupID);
			return DbHelperSQL.Query(strSql.ToString()).Tables[0];
		}

		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("GroupID", "SysGroup");
		}

		public bool Exists(int GroupID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysGroup");
			strSql.Append(" where GroupID=@GroupID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GroupID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GroupID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysGroup model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysGroup(");
			strSql.Append("GroupName,GroupRemark,ParentGroupID,ParentIDStr,IsPublic,CreateUserID,GroupType)");
			strSql.Append(" values (");
			strSql.Append("@GroupName,@GroupRemark,@ParentGroupID,@ParentIDStr,@IsPublic,@CreateUserID,@GroupType)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GroupName", SqlDbType.NVarChar, 50),
				new SqlParameter("@GroupRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@ParentGroupID", SqlDbType.Int, 4),
				new SqlParameter("@ParentIDStr", SqlDbType.NVarChar, 1000),
				new SqlParameter("@IsPublic", SqlDbType.Bit, 1),
				new SqlParameter("@CreateUserID", SqlDbType.Int, 4),
				new SqlParameter("@GroupType", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.GroupName;
			parameters[1].Value = model.GroupRemark;
			parameters[2].Value = model.ParentGroupID;
			parameters[3].Value = model.ParentIDStr;
			parameters[4].Value = model.IsPublic;
			parameters[5].Value = model.CreateUserID;
			parameters[6].Value = model.GroupType;
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

		public bool Update(Chain.Model.SysGroup model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysGroup set ");
			strSql.Append("GroupName=@GroupName,");
			strSql.Append("GroupRemark=@GroupRemark,");
			strSql.Append("ParentGroupID=@ParentGroupID,");
			strSql.Append("ParentIDStr=@ParentIDStr,");
			strSql.Append("IsPublic=@IsPublic,");
			strSql.Append("CreateUserID=@CreateUserID,");
			strSql.Append("GroupType=@GroupType");
			strSql.Append(" where GroupID=@GroupID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GroupName", SqlDbType.NVarChar, 50),
				new SqlParameter("@GroupRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@ParentGroupID", SqlDbType.Int, 4),
				new SqlParameter("@ParentIDStr", SqlDbType.NVarChar, 1000),
				new SqlParameter("@IsPublic", SqlDbType.Bit, 1),
				new SqlParameter("@CreateUserID", SqlDbType.Int, 4),
				new SqlParameter("@GroupType", SqlDbType.Int, 4),
				new SqlParameter("@GroupID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.GroupName;
			parameters[1].Value = model.GroupRemark;
			parameters[2].Value = model.ParentGroupID;
			parameters[3].Value = model.ParentIDStr;
			parameters[4].Value = model.IsPublic;
			parameters[5].Value = model.CreateUserID;
			parameters[6].Value = model.GroupType;
			parameters[7].Value = model.GroupID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int GroupID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysGroup ");
			strSql.Append(" where GroupID=@GroupID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GroupID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GroupID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string GroupIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysGroup ");
			strSql.Append(" where GroupID in (" + GroupIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysGroup GetModelbyGroupType(int GroupID, int GroupType)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 GroupID,GroupName,GroupRemark,ParentGroupID,ParentIDStr,IsPublic,CreateUserID,GroupType from SysGroup ");
			strSql.Append(" where GroupID=@GroupID");
			strSql.Append(" and GroupType=@GroupType or ParentGroupID=0");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GroupID", SqlDbType.Int, 4),
				new SqlParameter("@GroupType", SqlDbType.Int, 4)
			};
			parameters[0].Value = GroupID;
			parameters[1].Value = GroupType;
			Chain.Model.SysGroup model = new Chain.Model.SysGroup();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysGroup result;
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

		public Chain.Model.SysGroup GetModelByGroupType(int GroupType)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 GroupID,GroupName,GroupRemark,ParentGroupID,ParentIDStr,IsPublic,CreateUserID,GroupType from SysGroup ");
			strSql.Append(" where GroupType=@GroupType and ParentGroupID=0");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GroupType", SqlDbType.Int, 4)
			};
			parameters[0].Value = GroupType;
			Chain.Model.SysGroup model = new Chain.Model.SysGroup();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysGroup result;
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

		public Chain.Model.SysGroup GetModel(int GroupID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 GroupID,GroupName,GroupRemark,ParentGroupID,ParentIDStr,IsPublic,CreateUserID,GroupType from SysGroup ");
			strSql.Append(" where GroupID=@GroupID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GroupID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GroupID;
			Chain.Model.SysGroup model = new Chain.Model.SysGroup();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysGroup result;
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

		public Chain.Model.SysGroup DataRowToModel(DataRow row)
		{
			Chain.Model.SysGroup model = new Chain.Model.SysGroup();
			if (row != null)
			{
				if (row["GroupID"] != null && row["GroupID"].ToString() != "")
				{
					model.GroupID = int.Parse(row["GroupID"].ToString());
				}
				if (row["GroupName"] != null)
				{
					model.GroupName = row["GroupName"].ToString();
				}
				if (row["GroupRemark"] != null)
				{
					model.GroupRemark = row["GroupRemark"].ToString();
				}
				if (row["ParentGroupID"] != null)
				{
					model.ParentGroupID = int.Parse(row["ParentGroupID"].ToString());
				}
				if (row["ParentIDStr"] != null)
				{
					model.ParentIDStr = row["ParentIDStr"].ToString();
				}
				if (row["IsPublic"] != null)
				{
					model.IsPublic = bool.Parse(row["IsPublic"].ToString());
				}
				if (row["CreateUserID"] != null)
				{
					model.CreateUserID = int.Parse(row["CreateUserID"].ToString());
				}
			}
			return model;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select GroupID,GroupName,GroupRemark,ParentGroupID,ParentIDStr,IsPublic,CreateUserID,SysUser.UserName,GroupType ");
			strSql.Append(" FROM SysGroup left join SysUser on SysGroup.CreateUserID = SysUser.UserID ");
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
			strSql.Append(" GroupID,GroupName,GroupRemark,ParentGroupID,ParentIDStr,IsPublic,CreateUserID ");
			strSql.Append(" FROM SysGroup ");
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
			strSql.Append("select count(1) FROM SysGroup ");
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
				strSql.Append("order by T.GroupID desc");
			}
			strSql.Append(")AS Row, T.*  from SysGroup T ");
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
