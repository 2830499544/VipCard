using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysShopCmsLog
	{
		public bool Exists(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysShopCmsLog");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysShopCmsLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysShopCmsLog(");
			strSql.Append("ShopCmsAccount,ShopCmsType,Count,Remark,CreateTime,UserID,ShopID,OutShopID)");
			strSql.Append(" values (");
			strSql.Append("@ShopCmsAccount,@ShopCmsType,@Count,@Remark,@CreateTime,@UserID,@ShopID,@OutShopID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopCmsAccount", SqlDbType.VarChar, 200),
				new SqlParameter("@ShopCmsType", SqlDbType.Int, 4),
				new SqlParameter("@Count", SqlDbType.Int, 4),
				new SqlParameter("@Remark", SqlDbType.VarChar, 500),
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4),
				new SqlParameter("@OutShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ShopCmsAccount;
			parameters[1].Value = model.ShopCmsType;
			parameters[2].Value = model.Count;
			parameters[3].Value = model.Remark;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = model.UserID;
			parameters[6].Value = model.ShopID;
			parameters[7].Value = model.OutShopID;
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

		public bool Update(Chain.Model.SysShopCmsLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysShopCmsLog set ");
			strSql.Append("ShopCmsAccount=@ShopCmsAccount,");
			strSql.Append("ShopCmsType=@ShopCmsType,");
			strSql.Append("Count=@Count,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("UserID=@UserID,");
			strSql.Append("ShopID=@ShopID,");
			strSql.Append("OutShopID=@OutShopID");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopCmsAccount", SqlDbType.VarChar, 200),
				new SqlParameter("@ShopCmsType", SqlDbType.Int, 4),
				new SqlParameter("@Count", SqlDbType.Int, 4),
				new SqlParameter("@Remark", SqlDbType.VarChar, 500),
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4),
				new SqlParameter("@OutShopID", SqlDbType.Int, 4),
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ShopCmsAccount;
			parameters[1].Value = model.ShopCmsType;
			parameters[2].Value = model.Count;
			parameters[3].Value = model.Remark;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = model.UserID;
			parameters[6].Value = model.ShopID;
			parameters[7].Value = model.OutShopID;
			parameters[8].Value = model.ID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysShopCmsLog ");
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
			strSql.Append("delete from SysShopCmsLog ");
			strSql.Append(" where ID in (" + IDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = " SysShopCmsLog,SysUser,SysShop ";
			string[] columns = new string[]
			{
				" SysShopCmsLog.*,SysUser.UserName,SysShop.ShopName "
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "CreateTime", "ID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public Chain.Model.SysShopCmsLog GetModel(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ID,ShopCmsAccount,ShopCmsType,Count,Remark,CreateTime,UserID,ShopID,OutShopID from SysShopCmsLog ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			Chain.Model.SysShopCmsLog model = new Chain.Model.SysShopCmsLog();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysShopCmsLog result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
				{
					model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ShopCmsAccount"] != null && ds.Tables[0].Rows[0]["ShopCmsAccount"].ToString() != "")
				{
					model.ShopCmsAccount = ds.Tables[0].Rows[0]["ShopCmsAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ShopCmsType"] != null && ds.Tables[0].Rows[0]["ShopCmsType"].ToString() != "")
				{
					model.ShopCmsType = int.Parse(ds.Tables[0].Rows[0]["ShopCmsType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Count"] != null && ds.Tables[0].Rows[0]["Count"].ToString() != "")
				{
					model.Count = int.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
				{
					model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
				{
					model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
				{
					model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ShopID"] != null && ds.Tables[0].Rows[0]["ShopID"].ToString() != "")
				{
					model.ShopID = int.Parse(ds.Tables[0].Rows[0]["ShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OutShopID"] != null && ds.Tables[0].Rows[0]["OutShopID"].ToString() != "")
				{
					model.OutShopID = int.Parse(ds.Tables[0].Rows[0]["OutShopID"].ToString());
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
			strSql.Append("select ID,ShopCmsAccount,ShopCmsType,Count,Remark,CreateTime,UserID,ShopID,OutShopID ");
			strSql.Append(" FROM SysShopCmsLog ");
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
			strSql.Append(" ID,ShopCmsAccount,ShopCmsType,Count,Remark,CreateTime,UserID,ShopID,OutShopID ");
			strSql.Append(" FROM SysShopCmsLog ");
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
			strSql.Append("select count(1) FROM SysShopCmsLog ");
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
			strSql.Append(")AS Row, T.*  from SysShopCmsLog T ");
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
