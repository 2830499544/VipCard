using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysShopPointLog
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID", "SysShopPointLog");
		}

		public bool Exists(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysShopPointLog");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Add(Chain.Model.SysShopPointLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysShopPointLog(");
			strSql.Append("ShopPointAccount,ShopPointType,Count,Remark,CreateTime,UserID,ShopID,OutShopID)");
			strSql.Append(" values (");
			strSql.Append("@ShopPointAccount,@ShopPointType,@Count,@Remark,@CreateTime,@UserID,@ShopID,@OutShopID)");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopPointAccount", SqlDbType.VarChar, 200),
				new SqlParameter("@ShopPointType", SqlDbType.Int, 4),
				new SqlParameter("@Count", SqlDbType.Int, 4),
				new SqlParameter("@Remark", SqlDbType.VarChar, 500),
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4),
				new SqlParameter("@OutShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ShopPointAccount;
			parameters[1].Value = model.ShopPointType;
			parameters[2].Value = model.Count;
			parameters[3].Value = model.Remark;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = model.UserID;
			parameters[6].Value = model.ShopID;
			parameters[7].Value = model.OutShopID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Update(Chain.Model.SysShopPointLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysShopPointLog set ");
			strSql.Append("ShopPointAccount=@ShopPointAccount,");
			strSql.Append("ShopPointType=@ShopPointType,");
			strSql.Append("Count=@Count,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("UserID=@UserID,");
			strSql.Append("ShopID=@ShopID,");
			strSql.Append("OutShopID=@OutShopID");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopPointAccount", SqlDbType.VarChar, 200),
				new SqlParameter("@ShopPointType", SqlDbType.Int, 4),
				new SqlParameter("@Count", SqlDbType.Int, 4),
				new SqlParameter("@Remark", SqlDbType.VarChar, 500),
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4),
				new SqlParameter("@OutShopID", SqlDbType.Int, 4),
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ShopPointAccount;
			parameters[1].Value = model.ShopPointType;
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
			strSql.Append("delete from SysShopPointLog ");
			strSql.Append(" where ID=@ID ");
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
			strSql.Append("delete from SysShopPointLog ");
			strSql.Append(" where ID in (" + IDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = " SysShopPointLog,SysUser,SysShop ";
			string[] columns = new string[]
			{
				" SysShopPointLog.ID,SysShopPointLog.ShopPointAccount,SysShopPointLog.ShopPointType,SysShopPointLog.Count\r\n                                 ,SysUser.UserName,SysShop.ShopName,SysShopPointLog.Remark,SysShopPointLog.CreateTime,SysShopPointLog.UserID\r\n                                 ,SysShopPointLog.ShopID,SysShopPointLog.OutShopID,SysShop.ShopType"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "CreateTime", "ID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public Chain.Model.SysShopPointLog GetModel(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ID,ShopPointAccount,ShopPointType,Count,Remark,CreateTime,UserID,ShopID,OutShopID from SysShopPointLog ");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			Chain.Model.SysShopPointLog model = new Chain.Model.SysShopPointLog();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysShopPointLog result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
				{
					model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ShopPointAccount"] != null && ds.Tables[0].Rows[0]["ShopPointAccount"].ToString() != "")
				{
					model.ShopPointAccount = ds.Tables[0].Rows[0]["ShopPointAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ShopPointType"] != null && ds.Tables[0].Rows[0]["ShopPointType"].ToString() != "")
				{
					model.ShopPointType = int.Parse(ds.Tables[0].Rows[0]["ShopPointType"].ToString());
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
			strSql.Append("select ID,ShopPointAccount,ShopPointType,Count,Remark,CreateTime,UserID,ShopID,OutShopID ");
			strSql.Append(" FROM SysShopPointLog ");
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
			strSql.Append(" ID,ShopPointAccount,ShopPointType,Count,Remark,CreateTime,UserID,ShopID,OutShopID ");
			strSql.Append(" FROM SysShopPointLog ");
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
			strSql.Append("select count(1) FROM SysShopPointLog ");
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
			strSql.Append(")AS Row, T.*  from SysShopPointLog T ");
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
