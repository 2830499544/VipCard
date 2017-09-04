using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class SubMem
	{
		public int Add(Chain.Model.SubMem model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SubMem(");
			strSql.Append("SubCardNumber,MemID,MemCard,SubName,SubMemMobile,IsUsed)");
			strSql.Append(" values (");
			strSql.Append("@SubCardNumber,@MemID,@MemCard,@SubName,@SubMemMobile,@IsUsed)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SubCardNumber", SqlDbType.VarChar, 50),
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@MemCard", SqlDbType.VarChar, 50),
				new SqlParameter("@SubName", SqlDbType.NVarChar, 50),
				new SqlParameter("@SubMemMobile", SqlDbType.VarChar, 50),
				new SqlParameter("@IsUsed", SqlDbType.Bit)
			};
			parameters[0].Value = model.SubCardNumber;
			parameters[1].Value = model.MemID;
			parameters[2].Value = model.MemCard;
			parameters[3].Value = model.SubName;
			parameters[4].Value = model.SubMemMobile;
			parameters[5].Value = model.IsUsed;
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

		public bool Update(Chain.Model.SubMem model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SubMem set ");
			strSql.Append("SubCardNumber=@SubCardNumber,");
			strSql.Append("MemID=@MemID,");
			strSql.Append("MemCard=@MemCard,");
			strSql.Append("SubName=@SubName,");
			strSql.Append("SubMemMobile=@SubMemMobile,");
			strSql.Append("IsUsed=@IsUsed ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SubCardNumber", SqlDbType.VarChar, 50),
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@MemCard", SqlDbType.VarChar, 50),
				new SqlParameter("@SubName", SqlDbType.NVarChar, 50),
				new SqlParameter("@SubMemMobile", SqlDbType.VarChar, 50),
				new SqlParameter("@IsUsed", SqlDbType.Bit),
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.SubCardNumber;
			parameters[1].Value = model.MemID;
			parameters[2].Value = model.MemCard;
			parameters[3].Value = model.SubName;
			parameters[4].Value = model.SubMemMobile;
			parameters[5].Value = model.IsUsed;
			parameters[6].Value = model.ID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SubMem ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM SubMem ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public bool IsHasMemCard(string MemCard)
		{
			string sql = string.Concat(new string[]
			{
				"select * from SubMem where SubCardNumber='",
				MemCard,
				"' or SubMemMobile='",
				MemCard,
				"'"
			});
			DataTable dt = DbHelperSQL.Query(sql).Tables[0];
			return dt.Rows.Count > 0;
		}

		public Chain.Model.SubMem GetModel(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 * from SubMem ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			Chain.Model.SubMem model = new Chain.Model.SubMem();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SubMem result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
				{
					model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SubCardNumber"] != null && ds.Tables[0].Rows[0]["SubCardNumber"].ToString() != "")
				{
					model.SubCardNumber = ds.Tables[0].Rows[0]["SubCardNumber"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MemID"] != null && ds.Tables[0].Rows[0]["MemID"].ToString() != "")
				{
					model.MemID = int.Parse(ds.Tables[0].Rows[0]["MemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["IsUsed"] != null && ds.Tables[0].Rows[0]["IsUsed"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsUsed"].ToString() == "1" || ds.Tables[0].Rows[0]["IsUsed"].ToString().ToLower() == "true")
					{
						model.IsUsed = true;
					}
					else
					{
						model.IsUsed = false;
					}
				}
				if (ds.Tables[0].Rows[0]["MemCard"] != null && ds.Tables[0].Rows[0]["MemCard"].ToString() != "")
				{
					model.MemCard = ds.Tables[0].Rows[0]["MemCard"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SubName"] != null && ds.Tables[0].Rows[0]["SubName"].ToString() != "")
				{
					model.SubName = ds.Tables[0].Rows[0]["SubName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SubMemMobile"] != null && ds.Tables[0].Rows[0]["SubMemMobile"].ToString() != "")
				{
					model.SubMemMobile = ds.Tables[0].Rows[0]["SubMemMobile"].ToString();
				}
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
