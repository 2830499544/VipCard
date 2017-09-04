using Chain.DBUtility;
using Chain.Model;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class GoodsLog
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID", "GoodsLog");
		}

		public bool Exists(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from GoodsLog");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.GoodsLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into GoodsLog(");
			strSql.Append("GoodsAccount,Type,GoodsID,TotalPrice,GoodsNumber,Remark,CreateTime,ShopID,UserID,ChangeShopID)");
			strSql.Append(" values (");
			strSql.Append("@GoodsAccount,@Type,@GoodsID,@TotalPrice,@GoodsNumber,@Remark,@CreateTime,@ShopID,@UserID,@ChangeShopID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@Type", SqlDbType.Int, 4),
				new SqlParameter("@GoodsID", SqlDbType.Int, 4),
				new SqlParameter("@TotalPrice", SqlDbType.Money, 8),
				new SqlParameter("@GoodsNumber", SqlDbType.Int, 4),
				new SqlParameter("@Remark", SqlDbType.VarChar, 500),
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@ShopID", SqlDbType.Int, 4),
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@ChangeShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.GoodsAccount;
			parameters[1].Value = model.Type;
			parameters[2].Value = model.GoodsID;
			parameters[3].Value = model.TotalPrice;
			parameters[4].Value = model.GoodsNumber;
			parameters[5].Value = model.Remark;
			parameters[6].Value = model.CreateTime;
			parameters[7].Value = model.ShopID;
			parameters[8].Value = model.UserID;
			parameters[9].Value = model.ChangeShopID;
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

		public bool Update(Chain.Model.GoodsLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update GoodsLog set ");
			strSql.Append("GoodsAccount=@GoodsAccount,");
			strSql.Append("Type=@Type,");
			strSql.Append("GoodsID=@GoodsID,");
			strSql.Append("TotalPrice=@TotalPrice,");
			strSql.Append("GoodsNumber=@GoodsNumber,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("ShopID=@ShopID,");
			strSql.Append("UserID=@UserID,");
			strSql.Append("ChangeShopID=@ChangeShopID");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@Type", SqlDbType.Int, 4),
				new SqlParameter("@GoodsID", SqlDbType.Int, 4),
				new SqlParameter("@TotalPrice", SqlDbType.Money, 8),
				new SqlParameter("@GoodsNumber", SqlDbType.Int, 4),
				new SqlParameter("@Remark", SqlDbType.VarChar, 500),
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@ShopID", SqlDbType.Int, 4),
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@ChangeShopID", SqlDbType.Int, 4),
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.GoodsAccount;
			parameters[1].Value = model.Type;
			parameters[2].Value = model.GoodsID;
			parameters[3].Value = model.TotalPrice;
			parameters[4].Value = model.GoodsNumber;
			parameters[5].Value = model.Remark;
			parameters[6].Value = model.CreateTime;
			parameters[7].Value = model.ShopID;
			parameters[8].Value = model.UserID;
			parameters[9].Value = model.ChangeShopID;
			parameters[10].Value = model.ID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsLog ");
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
			strSql.Append("delete from GoodsLog ");
			strSql.Append(" where ID in (" + IDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.GoodsLog GetModel(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ID,GoodsAccount,Type,GoodsID,TotalPrice,GoodsNumber,Remark,CreateTime,ShopID,UserID,ChangeShopID from GoodsLog ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			Chain.Model.GoodsLog model = new Chain.Model.GoodsLog();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.GoodsLog result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
				{
					model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsAccount"] != null && ds.Tables[0].Rows[0]["GoodsAccount"].ToString() != "")
				{
					model.GoodsAccount = ds.Tables[0].Rows[0]["GoodsAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
				{
					model.Type = int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsID"] != null && ds.Tables[0].Rows[0]["GoodsID"].ToString() != "")
				{
					model.GoodsID = int.Parse(ds.Tables[0].Rows[0]["GoodsID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["TotalPrice"] != null && ds.Tables[0].Rows[0]["TotalPrice"].ToString() != "")
				{
					model.TotalPrice = decimal.Parse(ds.Tables[0].Rows[0]["TotalPrice"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsNumber"] != null && ds.Tables[0].Rows[0]["GoodsNumber"].ToString() != "")
				{
					model.GoodsNumber = int.Parse(ds.Tables[0].Rows[0]["GoodsNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
				{
					model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
				{
					model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ShopID"] != null && ds.Tables[0].Rows[0]["ShopID"].ToString() != "")
				{
					model.ShopID = int.Parse(ds.Tables[0].Rows[0]["ShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
				{
					model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ChangeShopID"] != null && ds.Tables[0].Rows[0]["ChangeShopID"].ToString() != "")
				{
					model.ChangeShopID = int.Parse(ds.Tables[0].Rows[0]["ChangeShopID"].ToString());
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
			strSql.Append("select ID,GoodsAccount,Type,GoodsID,TotalPrice,GoodsNumber,Remark,CreateTime,ShopID,UserID,ChangeShopID ");
			strSql.Append(" FROM GoodsLog ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListsss(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ID,GoodsAccount,Type,GoodsID,TotalPrice,GoodsNumber,Remark,CreateTime,GoodsLog.ShopID,UserID,ChangeShopID,ShopName as ChangeShopName ");
			strSql.Append(" FROM GoodsLog join SysShop on GoodsLog.ChangeShopID=SysShop.ShopID  ");
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
			strSql.Append(" ID,GoodsAccount,Type,GoodsID,TotalPrice,GoodsNumber,Remark,CreateTime,ShopID,UserID,ChangeShopID ");
			strSql.Append(" FROM GoodsLog ");
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
			strSql.Append("select count(1) FROM GoodsLog ");
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
			strSql.Append(")AS Row, T.*  from GoodsLog T ");
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
			string tableName = "GoodsLog,SysShop,SysUser";
			string[] columns = new string[]
			{
				"GoodsLog.*,SysShop.ShopName,SysUser.UserName,(select ShopName from SysShop where GoodsLog.ChangeShopID=SysShop.ShopID) as ChangeShopName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "CreateTime", "ID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public bool ExeclDataInput(ArrayList sqlArray)
		{
			return DbHelperSQL.ExecuteSqlTran(sqlArray);
		}

		public int GetIDByGoodsAccount(string GoodsAccount)
		{
			string sql = string.Format("select ID from  GoodsLog  where GoodsAccount='{0}'", GoodsAccount);
			DataSet ds = DbHelperSQL.Query(sql);
			int result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
			}
			else
			{
				result = 0;
			}
			return result;
		}
	}
}
