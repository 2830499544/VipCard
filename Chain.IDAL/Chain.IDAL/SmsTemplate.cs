using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SmsTemplate
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("TemplateID", "SmsTemplate");
		}

		public bool Exists(int TemplateID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SmsTemplate");
			strSql.Append(" where TemplateID=@TemplateID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@TemplateID", SqlDbType.Int, 4)
			};
			parameters[0].Value = TemplateID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SmsTemplate model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SmsTemplate(");
			strSql.Append("TemplateName,TemplateContent,TemplateShopID)");
			strSql.Append(" values (");
			strSql.Append("@TemplateName,@TemplateContent,@TemplateShopID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@TemplateName", SqlDbType.NVarChar, 100),
				new SqlParameter("@TemplateContent", SqlDbType.NVarChar, 1000),
				new SqlParameter("@TemplateShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.TemplateName;
			parameters[1].Value = model.TemplateContent;
			parameters[2].Value = model.TemplateShopID;
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

		public int Update(Chain.Model.SmsTemplate model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SmsTemplate set ");
			strSql.Append("TemplateName=@TemplateName,");
			strSql.Append("TemplateContent=@TemplateContent");
			strSql.Append(" where TemplateID=@TemplateID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@TemplateName", SqlDbType.NVarChar, 100),
				new SqlParameter("@TemplateContent", SqlDbType.NVarChar, 1000),
				new SqlParameter("@TemplateID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.TemplateName;
			parameters[1].Value = model.TemplateContent;
			parameters[2].Value = model.TemplateID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public bool Delete(int TemplateID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SmsTemplate ");
			strSql.Append(" where TemplateID=@TemplateID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@TemplateID", SqlDbType.Int, 4)
			};
			parameters[0].Value = TemplateID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string TemplateIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SmsTemplate ");
			strSql.Append(" where TemplateID in (" + TemplateIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SmsTemplate GetModel(int TemplateID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 TemplateID,TemplateName,TemplateContent from SmsTemplate ");
			strSql.Append(" where TemplateID=@TemplateID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@TemplateID", SqlDbType.Int, 4)
			};
			parameters[0].Value = TemplateID;
			Chain.Model.SmsTemplate model = new Chain.Model.SmsTemplate();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SmsTemplate result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["TemplateID"] != null && ds.Tables[0].Rows[0]["TemplateID"].ToString() != "")
				{
					model.TemplateID = int.Parse(ds.Tables[0].Rows[0]["TemplateID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["TemplateName"] != null && ds.Tables[0].Rows[0]["TemplateName"].ToString() != "")
				{
					model.TemplateName = ds.Tables[0].Rows[0]["TemplateName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["TemplateContent"] != null && ds.Tables[0].Rows[0]["TemplateContent"].ToString() != "")
				{
					model.TemplateContent = ds.Tables[0].Rows[0]["TemplateContent"].ToString();
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
			strSql.Append("select TemplateID,TemplateName,TemplateContent ");
			strSql.Append(" FROM SmsTemplate ");
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
			strSql.Append(" TemplateID,TemplateName,TemplateContent ");
			strSql.Append(" FROM SmsTemplate ");
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
			strSql.Append("select count(1) FROM SmsTemplate ");
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
				strSql.Append("order by T.TemplateID desc");
			}
			strSql.Append(")AS Row, T.*  from SmsTemplate T ");
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
			string tableName = "SmsTemplate,SysShop";
			string[] columns = new string[]
			{
				"SmsTemplate.*,SysShop.ShopName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "TemplateID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
