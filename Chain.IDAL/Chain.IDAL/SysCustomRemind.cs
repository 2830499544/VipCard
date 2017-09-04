using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysCustomRemind
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("CustomRemindID", "SysCustomRemind");
		}

		public bool Exists(int CustomRemindID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysCustomRemind");
			strSql.Append(" where CustomRemindID=@CustomRemindID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CustomRemindID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CustomRemindID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysCustomRemind model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysCustomRemind(");
			strSql.Append("CustomRemindTitle,CustomRemindDetail,CustomReminder,CustomRemindTime,CustomRemindCreateTime,CustomRemindShopID,CustomRemindUserID)");
			strSql.Append(" values (");
			strSql.Append("@CustomRemindTitle,@CustomRemindDetail,@CustomReminder,@CustomRemindTime,@CustomRemindCreateTime,@CustomRemindShopID,@CustomRemindUserID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CustomRemindTitle", SqlDbType.NVarChar, 300),
				new SqlParameter("@CustomRemindDetail", SqlDbType.NVarChar, 500),
				new SqlParameter("@CustomReminder", SqlDbType.VarChar, 50),
				new SqlParameter("@CustomRemindTime", SqlDbType.DateTime),
				new SqlParameter("@CustomRemindCreateTime", SqlDbType.DateTime),
				new SqlParameter("@CustomRemindShopID", SqlDbType.Int, 4),
				new SqlParameter("@CustomRemindUserID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.CustomRemindTitle;
			parameters[1].Value = model.CustomRemindDetail;
			parameters[2].Value = model.CustomReminder;
			parameters[3].Value = model.CustomRemindTime;
			parameters[4].Value = model.CustomRemindCreateTime;
			parameters[5].Value = model.CustomRemindShopID;
			parameters[6].Value = model.CustomRemindUserID;
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

		public bool Update(Chain.Model.SysCustomRemind model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysCustomRemind set ");
			strSql.Append("CustomRemindTitle=@CustomRemindTitle,");
			strSql.Append("CustomRemindDetail=@CustomRemindDetail,");
			strSql.Append("CustomReminder=@CustomReminder,");
			strSql.Append("CustomRemindTime=@CustomRemindTime,");
			strSql.Append("CustomRemindCreateTime=@CustomRemindCreateTime,");
			strSql.Append("CustomRemindShopID=@CustomRemindShopID,");
			strSql.Append("CustomRemindUserID=@CustomRemindUserID");
			strSql.Append(" where CustomRemindID=@CustomRemindID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CustomRemindTitle", SqlDbType.NVarChar, 300),
				new SqlParameter("@CustomRemindDetail", SqlDbType.NVarChar, 500),
				new SqlParameter("@CustomReminder", SqlDbType.VarChar, 50),
				new SqlParameter("@CustomRemindTime", SqlDbType.DateTime),
				new SqlParameter("@CustomRemindCreateTime", SqlDbType.DateTime),
				new SqlParameter("@CustomRemindShopID", SqlDbType.Int, 4),
				new SqlParameter("@CustomRemindUserID", SqlDbType.Int, 4),
				new SqlParameter("@CustomRemindID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.CustomRemindTitle;
			parameters[1].Value = model.CustomRemindDetail;
			parameters[2].Value = model.CustomReminder;
			parameters[3].Value = model.CustomRemindTime;
			parameters[4].Value = model.CustomRemindCreateTime;
			parameters[5].Value = model.CustomRemindShopID;
			parameters[6].Value = model.CustomRemindUserID;
			parameters[7].Value = model.CustomRemindID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int CustomRemindID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysCustomRemind ");
			strSql.Append(" where CustomRemindID=@CustomRemindID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CustomRemindID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CustomRemindID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string CustomRemindIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysCustomRemind ");
			strSql.Append(" where CustomRemindID in (" + CustomRemindIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysCustomRemind GetModel(int CustomRemindID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 CustomRemindID,CustomRemindTitle,CustomRemindDetail,CustomReminder,CustomRemindTime,CustomRemindCreateTime,CustomRemindShopID,CustomRemindUserID from SysCustomRemind ");
			strSql.Append(" where CustomRemindID=@CustomRemindID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CustomRemindID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CustomRemindID;
			Chain.Model.SysCustomRemind model = new Chain.Model.SysCustomRemind();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysCustomRemind result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["CustomRemindID"] != null && ds.Tables[0].Rows[0]["CustomRemindID"].ToString() != "")
				{
					model.CustomRemindID = int.Parse(ds.Tables[0].Rows[0]["CustomRemindID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CustomRemindTitle"] != null && ds.Tables[0].Rows[0]["CustomRemindTitle"].ToString() != "")
				{
					model.CustomRemindTitle = ds.Tables[0].Rows[0]["CustomRemindTitle"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CustomRemindDetail"] != null && ds.Tables[0].Rows[0]["CustomRemindDetail"].ToString() != "")
				{
					model.CustomRemindDetail = ds.Tables[0].Rows[0]["CustomRemindDetail"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CustomReminder"] != null && ds.Tables[0].Rows[0]["CustomReminder"].ToString() != "")
				{
					model.CustomReminder = ds.Tables[0].Rows[0]["CustomReminder"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CustomRemindTime"] != null && ds.Tables[0].Rows[0]["CustomRemindTime"].ToString() != "")
				{
					model.CustomRemindTime = DateTime.Parse(ds.Tables[0].Rows[0]["CustomRemindTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CustomRemindCreateTime"] != null && ds.Tables[0].Rows[0]["CustomRemindCreateTime"].ToString() != "")
				{
					model.CustomRemindCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CustomRemindCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CustomRemindShopID"] != null && ds.Tables[0].Rows[0]["CustomRemindShopID"].ToString() != "")
				{
					model.CustomRemindShopID = int.Parse(ds.Tables[0].Rows[0]["CustomRemindShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CustomRemindUserID"] != null && ds.Tables[0].Rows[0]["CustomRemindUserID"].ToString() != "")
				{
					model.CustomRemindUserID = int.Parse(ds.Tables[0].Rows[0]["CustomRemindUserID"].ToString());
				}
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public DataSet GetList(string strWhere, int count)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select top " + count + " SysCustomRemind.* ,(select ShopName from SysShop where SysCustomRemind.CustomRemindShopID = SysShop.ShopID) as ShopName,(select UserName from SysUser where SysCustomRemind.CustomRemindUserID = SysUser.UserID) as UserName ");
			strSql.Append(" FROM SysCustomRemind ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by CustomRemindTime asc ");
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select SysCustomRemind.* ,(select ShopName from SysShop where SysCustomRemind.CustomRemindShopID = SysShop.ShopID) as ShopName,(select UserName from SysUser where SysCustomRemind.CustomRemindUserID = SysUser.UserID) as UserName ");
			strSql.Append(" FROM SysCustomRemind ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by CustomRemindTime asc ");
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
			strSql.Append(" CustomRemindID,CustomRemindTitle,CustomRemindDetail,CustomReminder,CustomRemindTime,CustomRemindCreateTime,CustomRemindShopID,CustomRemindUserID ");
			strSql.Append(" FROM SysCustomRemind ");
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
			strSql.Append("select count(1) FROM SysCustomRemind ");
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
				strSql.Append("order by T.CustomRemindID desc");
			}
			strSql.Append(")AS Row, T.*  from SysCustomRemind T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetCustomField()
		{
			string strSql = "select * from SysCustomRemind where CustomRemindTime = convert(varchar(10),getdate(),120)";
			return DbHelperSQL.Query(strSql.ToString());
		}
	}
}
