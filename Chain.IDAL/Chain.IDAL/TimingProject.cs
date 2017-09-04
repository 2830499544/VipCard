using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class TimingProject
	{
		public bool Exists(int ProjectID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from TimingProject");
			strSql.Append(" where ProjectID=@ProjectID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProjectID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ProjectID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool ExistsRules(int RulesID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from TimingProject");
			strSql.Append(" where ProjectRulesID=@ProjectRulesID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProjectRulesID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RulesID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool ExistsCategory(int CategoryID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from TimingProject");
			strSql.Append(" where EXISTS(SELECT 1 FROM TimingCategory WHERE TimingCategory.CategoryID = TimingProject.ProjectCategoryID \r\n                           AND (TimingCategory.CategoryFatherID = @CategoryFatherID OR TimingCategory.CategoryID = @CategoryID))");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CategoryFatherID", SqlDbType.Int, 4),
				new SqlParameter("@CategoryID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CategoryID;
			parameters[1].Value = CategoryID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.TimingProject model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into TimingProject(");
			strSql.Append("ProjectName,ProjectCategoryID,ProjectRulesID,ProjectAddTime,ProjectShopID,ProjectUserID,ProjectRemark)");
			strSql.Append(" values (");
			strSql.Append("@ProjectName,@ProjectCategoryID,@ProjectRulesID,@ProjectAddTime,@ProjectShopID,@ProjectUserID,@ProjectRemark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProjectName", SqlDbType.NVarChar, 50),
				new SqlParameter("@ProjectCategoryID", SqlDbType.Int, 4),
				new SqlParameter("@ProjectRulesID", SqlDbType.Int, 4),
				new SqlParameter("@ProjectAddTime", SqlDbType.DateTime),
				new SqlParameter("@ProjectShopID", SqlDbType.Int, 4),
				new SqlParameter("@ProjectUserID", SqlDbType.Int, 4),
				new SqlParameter("@ProjectRemark", SqlDbType.NVarChar, 200)
			};
			parameters[0].Value = model.ProjectName;
			parameters[1].Value = model.ProjectCategoryID;
			parameters[2].Value = model.ProjectRulesID;
			parameters[3].Value = model.ProjectAddTime;
			parameters[4].Value = model.ProjectShopID;
			parameters[5].Value = model.ProjectUserID;
			parameters[6].Value = model.ProjectRemark;
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

		public bool Update(Chain.Model.TimingProject model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update TimingProject set ");
			strSql.Append("ProjectName=@ProjectName,");
			strSql.Append("ProjectCategoryID=@ProjectCategoryID,");
			strSql.Append("ProjectRulesID=@ProjectRulesID,");
			strSql.Append("ProjectAddTime=@ProjectAddTime,");
			strSql.Append("ProjectShopID=@ProjectShopID,");
			strSql.Append("ProjectUserID=@ProjectUserID,");
			strSql.Append("ProjectRemark=@ProjectRemark");
			strSql.Append(" where ProjectID=@ProjectID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProjectName", SqlDbType.NVarChar, 50),
				new SqlParameter("@ProjectCategoryID", SqlDbType.Int, 4),
				new SqlParameter("@ProjectRulesID", SqlDbType.Int, 4),
				new SqlParameter("@ProjectAddTime", SqlDbType.DateTime),
				new SqlParameter("@ProjectShopID", SqlDbType.Int, 4),
				new SqlParameter("@ProjectUserID", SqlDbType.Int, 4),
				new SqlParameter("@ProjectID", SqlDbType.Int, 4),
				new SqlParameter("@ProjectRemark", SqlDbType.NVarChar, 200)
			};
			parameters[0].Value = model.ProjectName;
			parameters[1].Value = model.ProjectCategoryID;
			parameters[2].Value = model.ProjectRulesID;
			parameters[3].Value = model.ProjectAddTime;
			parameters[4].Value = model.ProjectShopID;
			parameters[5].Value = model.ProjectUserID;
			parameters[6].Value = model.ProjectID;
			parameters[7].Value = model.ProjectRemark;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ProjectID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from TimingProject ");
			strSql.Append(" where ProjectID=@ProjectID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProjectID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ProjectID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string ProjectIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from TimingProject ");
			strSql.Append(" where ProjectID in (" + ProjectIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.TimingProject GetModel(int ProjectID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ProjectID,ProjectName,ProjectCategoryID,ProjectRulesID,ProjectAddTime,ProjectShopID,ProjectUserID,ProjectRemark from TimingProject ");
			strSql.Append(" where ProjectID=@ProjectID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ProjectID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ProjectID;
			Chain.Model.TimingProject model = new Chain.Model.TimingProject();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.TimingProject result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ProjectID"] != null && ds.Tables[0].Rows[0]["ProjectID"].ToString() != "")
				{
					model.ProjectID = int.Parse(ds.Tables[0].Rows[0]["ProjectID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ProjectName"] != null && ds.Tables[0].Rows[0]["ProjectName"].ToString() != "")
				{
					model.ProjectName = ds.Tables[0].Rows[0]["ProjectName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ProjectCategoryID"] != null && ds.Tables[0].Rows[0]["ProjectCategoryID"].ToString() != "")
				{
					model.ProjectCategoryID = int.Parse(ds.Tables[0].Rows[0]["ProjectCategoryID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ProjectRulesID"] != null && ds.Tables[0].Rows[0]["ProjectRulesID"].ToString() != "")
				{
					model.ProjectRulesID = int.Parse(ds.Tables[0].Rows[0]["ProjectRulesID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ProjectAddTime"] != null && ds.Tables[0].Rows[0]["ProjectAddTime"].ToString() != "")
				{
					model.ProjectAddTime = DateTime.Parse(ds.Tables[0].Rows[0]["ProjectAddTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ProjectShopID"] != null && ds.Tables[0].Rows[0]["ProjectShopID"].ToString() != "")
				{
					model.ProjectShopID = int.Parse(ds.Tables[0].Rows[0]["ProjectShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ProjectUserID"] != null && ds.Tables[0].Rows[0]["ProjectUserID"].ToString() != "")
				{
					model.ProjectUserID = int.Parse(ds.Tables[0].Rows[0]["ProjectUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ProjectRemark"] != null && ds.Tables[0].Rows[0]["ProjectRemark"].ToString() != "")
				{
					model.ProjectRemark = ds.Tables[0].Rows[0]["ProjectRemark"].ToString();
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
			strSql.Append("select ProjectID,ProjectName,ProjectCategoryID,ProjectRulesID,ProjectAddTime,ProjectShopID,ProjectUserID,ProjectRemark ");
			strSql.Append(" FROM TimingProject ");
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
			strSql.Append(" ProjectID,ProjectName,ProjectCategoryID,ProjectRulesID,ProjectAddTime,ProjectShopID,ProjectUserID,ProjectRemark ");
			strSql.Append(" FROM TimingProject ");
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
			strSql.Append("select count(1) FROM TimingProject ");
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
				strSql.Append("order by T.ProjectID desc");
			}
			strSql.Append(")AS Row, T.*  from TimingProject T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int MemID, int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "TimingProject,SysUser,Timingrules";
			string[] columns = new string[]
			{
				string.Format("TimingProject.*,SysUser.UserName,Timingrules.RulesName,Timingrules.RulesRemark,Timingrules.RulesID,ISNULL((SELECT SUM(StorageResidueTime) FROM MemStorageTiming  WHERE TimingProject.ProjectID = MemStorageTiming.StorageTimingProjectID AND MemStorageTiming.StorageTimingMemID = {0}),0) AS AllCount", MemID)
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ProjectAddTime", "ProjectID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
