using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysUser
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("UserID", "SysUser");
		}

		public string GetUserNameByUserID(string userid)
		{
			string strSql = "SELECT UserName FROM SysUser WHERE UserID = @UserID";
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserID", SqlDbType.Int, 4)
			};
			parameters[0].Value = userid;
			string result;
			try
			{
				result = DbHelperSQL.GetSingle(strSql, parameters).ToString();
			}
			catch
			{
				result = "无此会员";
			}
			return result;
		}

		public bool Exists(string useraccount)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysUser");
			strSql.Append(" where UserAccount=@UserAccount");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserAccount", SqlDbType.VarChar, 200)
			};
			parameters[0].Value = useraccount;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool ExiNumber(string usernumber)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysUser");
			strSql.Append(" where UserNumber=@UserNumber");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserNumber", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = usernumber;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(string useraccount, string usernumber)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysUser");
			strSql.Append(" where UserAccount=@UserAccount or UserNumber=@UserNumber");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserAccount", SqlDbType.VarChar, 200),
				new SqlParameter("@UserNumber", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = useraccount;
			parameters[1].Value = usernumber;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(string useraccount, int userid)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysUser");
			strSql.Append(" where UserAccount = @UserAccount and UserID<>@UserID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserAccount", SqlDbType.VarChar, 200),
				new SqlParameter("@UserID", SqlDbType.Int)
			};
			parameters[0].Value = useraccount;
			parameters[1].Value = userid;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysUser model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysUser(");
			strSql.Append("UserAccount,UserName,UserPassword,UserShopID,UserGroupID,UserLock,UserRemark,UserCreateTime,UserTelephone,UserNumber)");
			strSql.Append(" values (");
			strSql.Append("@UserAccount,@UserName,@UserPassword,@UserShopID,@UserGroupID,@UserLock,@UserRemark,@UserCreateTime,@UserTelephone,@UserNumber)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserAccount", SqlDbType.VarChar, 200),
				new SqlParameter("@UserName", SqlDbType.NVarChar, 50),
				new SqlParameter("@UserPassword", SqlDbType.VarChar, 500),
				new SqlParameter("@UserShopID", SqlDbType.Int, 4),
				new SqlParameter("@UserGroupID", SqlDbType.Int, 4),
				new SqlParameter("@UserLock", SqlDbType.Bit, 1),
				new SqlParameter("@UserRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@UserCreateTime", SqlDbType.DateTime),
				new SqlParameter("@UserTelephone", SqlDbType.VarChar, 50),
				new SqlParameter("@UserNumber", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = model.UserAccount;
			parameters[1].Value = model.UserName;
			parameters[2].Value = model.UserPassword;
			parameters[3].Value = model.UserShopID;
			parameters[4].Value = model.UserGroupID;
			parameters[5].Value = model.UserLock;
			parameters[6].Value = model.UserRemark;
			parameters[7].Value = model.UserCreateTime;
			parameters[8].Value = model.UserTelephone;
			parameters[9].Value = model.UserNumber;
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

		public int Update(Chain.Model.SysUser model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysUser set ");
			strSql.Append("UserAccount=@UserAccount,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("UserPassword=@UserPassword,");
			strSql.Append("UserShopID=@UserShopID,");
			strSql.Append("UserGroupID=@UserGroupID,");
			strSql.Append("UserLock=@UserLock,");
			strSql.Append("UserRemark=@UserRemark,");
			strSql.Append("UserCreateTime=@UserCreateTime,");
			strSql.Append("UserTelephone=@UserTelephone,");
			strSql.Append("UserNumber=@UserNumber");
			strSql.Append(" where UserID=@UserID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserAccount", SqlDbType.VarChar, 200),
				new SqlParameter("@UserName", SqlDbType.NVarChar, 50),
				new SqlParameter("@UserPassword", SqlDbType.VarChar, 500),
				new SqlParameter("@UserShopID", SqlDbType.Int, 4),
				new SqlParameter("@UserGroupID", SqlDbType.Int, 4),
				new SqlParameter("@UserLock", SqlDbType.Bit, 1),
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@UserRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@UserCreateTime", SqlDbType.DateTime),
				new SqlParameter("@UserTelephone", SqlDbType.VarChar, 50),
				new SqlParameter("@UserNumber", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = model.UserAccount;
			parameters[1].Value = model.UserName;
			parameters[2].Value = model.UserPassword;
			parameters[3].Value = model.UserShopID;
			parameters[4].Value = model.UserGroupID;
			parameters[5].Value = model.UserLock;
			parameters[6].Value = model.UserID;
			parameters[7].Value = model.UserRemark;
			parameters[8].Value = model.UserCreateTime;
			parameters[9].Value = model.UserTelephone;
			parameters[10].Value = model.UserNumber;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			int result;
			if (rows > 0)
			{
				result = 1;
			}
			else
			{
				result = -3;
			}
			return result;
		}

		public bool Delete(int UserID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysUser ");
			strSql.Append(" where UserID=@UserID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserID", SqlDbType.Int, 4)
			};
			parameters[0].Value = UserID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public DataSet GetIsCanDelUser(int userid)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserID", SqlDbType.Int)
			};
			parameters[0].Value = userid;
			return DbHelperSQL.RunProcedure("UserIsCanDel", parameters, "#UserIsCanDel");
		}

		public bool DeleteList(string UserIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysUser ");
			strSql.Append(" where UserID in (" + UserIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public DataTable CheckUserLogin(string UserAccount, string Md5Pwd)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 UserID,UserAccount,UserName,UserPassword,UserShopID,UserGroupID,UserLock,UserRemark,UserCreateTime,UserTelephone,UserNumber from SysUser ");
			strSql.Append(" where UserAccount=@UserAccount and UserPassword=@UserPassword");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@UserPassword", SqlDbType.VarChar, 200)
			};
			parameters[0].Value = UserAccount;
			parameters[1].Value = Md5Pwd;
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			return ds.Tables[0];
		}

		public Chain.Model.SysUser GetModel(int UserID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 UserID,UserAccount,UserName,UserPassword,UserShopID,UserGroupID,UserLock,UserRemark,UserCreateTime,UserTelephone,UserNumber from SysUser ");
			strSql.Append(" where UserID=@UserID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserID", SqlDbType.Int, 4)
			};
			parameters[0].Value = UserID;
			Chain.Model.SysUser model = new Chain.Model.SysUser();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysUser result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
				{
					model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["UserAccount"] != null && ds.Tables[0].Rows[0]["UserAccount"].ToString() != "")
				{
					model.UserAccount = ds.Tables[0].Rows[0]["UserAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
				{
					model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["UserPassword"] != null && ds.Tables[0].Rows[0]["UserPassword"].ToString() != "")
				{
					model.UserPassword = ds.Tables[0].Rows[0]["UserPassword"].ToString();
				}
				if (ds.Tables[0].Rows[0]["UserShopID"] != null && ds.Tables[0].Rows[0]["UserShopID"].ToString() != "")
				{
					model.UserShopID = int.Parse(ds.Tables[0].Rows[0]["UserShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["UserGroupID"] != null && ds.Tables[0].Rows[0]["UserGroupID"].ToString() != "")
				{
					model.UserGroupID = int.Parse(ds.Tables[0].Rows[0]["UserGroupID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["UserLock"] != null && ds.Tables[0].Rows[0]["UserLock"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["UserLock"].ToString() == "1" || ds.Tables[0].Rows[0]["UserLock"].ToString().ToLower() == "true")
					{
						model.UserLock = true;
					}
					else
					{
						model.UserLock = false;
					}
				}
				if (ds.Tables[0].Rows[0]["UserRemark"] != null && ds.Tables[0].Rows[0]["UserRemark"].ToString() != "")
				{
					model.UserRemark = ds.Tables[0].Rows[0]["UserRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["UserCreateTime"] != null && ds.Tables[0].Rows[0]["UserCreateTime"].ToString() != "")
				{
					model.UserCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UserCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["UserTelephone"] != null && ds.Tables[0].Rows[0]["UserTelephone"].ToString() != "")
				{
					model.UserTelephone = ds.Tables[0].Rows[0]["UserTelephone"].ToString();
				}
				if (ds.Tables[0].Rows[0]["UserNumber"] != null && ds.Tables[0].Rows[0]["UserNumber"].ToString() != "")
				{
					model.UserNumber = ds.Tables[0].Rows[0]["UserNumber"].ToString();
				}
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Chain.Model.SysUser GetModel(string Account)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 UserID,UserAccount,UserName,UserPassword,UserShopID,UserGroupID,UserLock,UserRemark,UserCreateTime,UserTelephone,UserNumber from SysUser ");
			strSql.Append(" where UserAccount=@UserAccount");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserAccount", SqlDbType.VarChar, 255)
			};
			parameters[0].Value = Account;
			Chain.Model.SysUser model = new Chain.Model.SysUser();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysUser result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
				{
					model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["UserAccount"] != null && ds.Tables[0].Rows[0]["UserAccount"].ToString() != "")
				{
					model.UserAccount = ds.Tables[0].Rows[0]["UserAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["UserName"] != null && ds.Tables[0].Rows[0]["UserName"].ToString() != "")
				{
					model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["UserPassword"] != null && ds.Tables[0].Rows[0]["UserPassword"].ToString() != "")
				{
					model.UserPassword = ds.Tables[0].Rows[0]["UserPassword"].ToString();
				}
				if (ds.Tables[0].Rows[0]["UserShopID"] != null && ds.Tables[0].Rows[0]["UserShopID"].ToString() != "")
				{
					model.UserShopID = int.Parse(ds.Tables[0].Rows[0]["UserShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["UserGroupID"] != null && ds.Tables[0].Rows[0]["UserGroupID"].ToString() != "")
				{
					model.UserGroupID = int.Parse(ds.Tables[0].Rows[0]["UserGroupID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["UserLock"] != null && ds.Tables[0].Rows[0]["UserLock"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["UserLock"].ToString() == "1" || ds.Tables[0].Rows[0]["UserLock"].ToString().ToLower() == "true")
					{
						model.UserLock = true;
					}
					else
					{
						model.UserLock = false;
					}
				}
				if (ds.Tables[0].Rows[0]["UserRemark"] != null && ds.Tables[0].Rows[0]["UserRemark"].ToString() != "")
				{
					model.UserRemark = ds.Tables[0].Rows[0]["UserRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["UserCreateTime"] != null && ds.Tables[0].Rows[0]["UserCreateTime"].ToString() != "")
				{
					model.UserCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UserCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["UserTelephone"] != null && ds.Tables[0].Rows[0]["UserTelephone"].ToString() != "")
				{
					model.UserTelephone = ds.Tables[0].Rows[0]["UserTelephone"].ToString();
				}
				if (ds.Tables[0].Rows[0]["UserNumber"] != null && ds.Tables[0].Rows[0]["UserNumber"].ToString() != "")
				{
					model.UserNumber = ds.Tables[0].Rows[0]["UserNumber"].ToString();
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
			strSql.Append("select UserID,UserAccount,UserName,UserPassword,UserShopID,UserGroupID,UserLock,UserRemark,UserCreateTime,UserTelephone,UserNumber ");
			strSql.Append(" FROM SysUser ");
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
			strSql.Append(" UserID,UserAccount,UserName,UserPassword,UserShopID,UserGroupID,UserLock,UserRemark,UserCreateTime,UserTelephone,UserNumber ");
			strSql.Append(" FROM SysUser ");
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
			strSql.Append("select count(1) FROM SysUser ");
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
				strSql.Append("order by T.UserID desc");
			}
			strSql.Append(")AS Row, T.*  from SysUser T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetList(int PageSize, int PageIndex, string[] where, out int resCount)
		{
			string tableName = "SysUser";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, where, "UserID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "SysUser,SysShop,SysGroup";
			string[] columns = new string[]
			{
				"SysUser.*,SysShop.ShopName,SysGroup.GroupName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "UserID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int ExistPwd(int UserID, string oldPwd)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.AppendFormat("select UserPassword from SysUser where UserID={0}", UserID);
			string strPwd = DbHelperSQL.GetSingle(strSql.ToString()).ToString();
			int result;
			if (strPwd != oldPwd)
			{
				result = -1;
			}
			else
			{
				result = 1;
			}
			return result;
		}

		public int UpdateUserPwd(int UsreID, string newPwd)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" update SysUser set UserPassword=@UserPassword ");
			strSql.Append(" where UserID=@UserID ");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@UserPassword", SqlDbType.VarChar, 500),
				new SqlParameter("@UserID", SqlDbType.Int, 4)
			};
			parameter[0].Value = newPwd;
			parameter[1].Value = UsreID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}

		public int UpdateUserLock(int ShopID, int type)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" update SysUser set UserLock=@UserLock ");
			strSql.Append(" where UserShopID=@UserShopID");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@UserLock", SqlDbType.Bit, 1),
				new SqlParameter("@UserShopID", SqlDbType.Int, 4)
			};
			parameter[0].Value = type;
			parameter[1].Value = ShopID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}
	}
}
