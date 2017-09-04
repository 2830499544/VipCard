using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class GiftExchange
	{
		public int GetMemExchangePoint(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select sum(ExchangeAllPoint) FROM GiftExchange ");
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

		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ExchangeID", "GiftExchange");
		}

		public bool Exists(int ExchangeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from GiftExchange");
			strSql.Append(" where ExchangeID=@ExchangeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ExchangeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ExchangeID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.GiftExchange model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into GiftExchange(");
			strSql.Append("MemID,ExchangeTelePhone,ExchangeAddress,ExchangeAccount,ExchangeAllNumber,ExchangeAllPoint,ApplicationTime,ApplicationRemark,ExchangeStatus,ExchangeType,ShopID)");
			strSql.Append(" values (");
			strSql.Append("@MemID,@ExchangeTelePhone,@ExchangeAddress,@ExchangeAccount,@ExchangeAllNumber,@ExchangeAllPoint,@ApplicationTime,@ApplicationRemark,@ExchangeStatus,@ExchangeType,@ShopID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeTelePhone", SqlDbType.VarChar, 50),
				new SqlParameter("@ExchangeAddress", SqlDbType.NVarChar, 50),
				new SqlParameter("@ExchangeAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@ExchangeAllNumber", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeAllPoint", SqlDbType.Int, 4),
				new SqlParameter("@ApplicationTime", SqlDbType.DateTime),
				new SqlParameter("@ApplicationRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@ExchangeStatus", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeType", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MemID;
			parameters[1].Value = model.ExchangeTelePhone;
			parameters[2].Value = model.ExchangeAddress;
			parameters[3].Value = model.ExchangeAccount;
			parameters[4].Value = model.ExchangeAllNumber;
			parameters[5].Value = model.ExchangeAllPoint;
			parameters[6].Value = model.ApplicationTime;
			parameters[7].Value = model.ApplicationRemark;
			parameters[8].Value = model.ExchangeStatus;
			parameters[9].Value = model.ExchangeType;
			parameters[10].Value = model.ShopID;
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

		public int MainSystemAdd(Chain.Model.GiftExchange model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into GiftExchange(");
			strSql.Append("MemID,ExchangeTelePhone,ExchangeAddress,ExchangeAccount,ExchangeAllNumber,ExchangeAllPoint,ApplicationTime,ApplicationRemark,ExchangeStatus,ExchangeTime,ExchangeUserID,ExchangeRemark,ExchangeType,ShopID)");
			strSql.Append(" values (");
			strSql.Append("@MemID,@ExchangeTelePhone,@ExchangeAddress,@ExchangeAccount,@ExchangeAllNumber,@ExchangeAllPoint,@ApplicationTime,@ApplicationRemark,@ExchangeStatus,@ExchangeTime,@ExchangeUserID,@ExchangeRemark,@ExchangeType,@ShopID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeTelePhone", SqlDbType.VarChar, 50),
				new SqlParameter("@ExchangeAddress", SqlDbType.NVarChar, 50),
				new SqlParameter("@ExchangeAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@ExchangeAllNumber", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeAllPoint", SqlDbType.Int, 4),
				new SqlParameter("@ApplicationTime", SqlDbType.DateTime),
				new SqlParameter("@ApplicationRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@ExchangeStatus", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeTime", SqlDbType.DateTime),
				new SqlParameter("@ExchangeUserID", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@ExchangeType", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MemID;
			parameters[1].Value = model.ExchangeTelePhone;
			parameters[2].Value = model.ExchangeAddress;
			parameters[3].Value = model.ExchangeAccount;
			parameters[4].Value = model.ExchangeAllNumber;
			parameters[5].Value = model.ExchangeAllPoint;
			parameters[6].Value = model.ApplicationTime;
			parameters[7].Value = model.ApplicationRemark;
			parameters[8].Value = model.ExchangeStatus;
			parameters[9].Value = model.ExchangeTime;
			parameters[10].Value = model.ExchangeUserID;
			parameters[11].Value = model.ExchangeRemark;
			parameters[12].Value = model.ExchangeType;
			parameters[13].Value = model.ShopID;
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

		public bool Update(Chain.Model.GiftExchange model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update GiftExchange set ");
			strSql.Append("MemID=@MemID,");
			strSql.Append("ExchangeTelePhone=@ExchangeTelePhone,");
			strSql.Append("ExchangeAddress=@ExchangeAddress,");
			strSql.Append("ExchangeAccount=@ExchangeAccount,");
			strSql.Append("ExchangeAllNumber=@ExchangeAllNumber,");
			strSql.Append("ExchangeAllPoint=@ExchangeAllPoint,");
			strSql.Append("ApplicationTime=@ApplicationTime,");
			strSql.Append("ApplicationRemark=@ApplicationRemark,");
			strSql.Append("ExchangeStatus=@ExchangeStatus,");
			strSql.Append("ExchangeTime=@ExchangeTime,");
			strSql.Append("ExchangeUserID=@ExchangeUserID,");
			strSql.Append("ExchangeRemark=@ExchangeRemark,");
			strSql.Append("ExchangeType=@ExchangeType");
			strSql.Append(" where ExchangeID=@ExchangeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeTelePhone", SqlDbType.VarChar, 50),
				new SqlParameter("@ExchangeAddress", SqlDbType.NVarChar, 50),
				new SqlParameter("@ExchangeAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@ExchangeAllNumber", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeAllPoint", SqlDbType.Int, 4),
				new SqlParameter("@ApplicationTime", SqlDbType.DateTime),
				new SqlParameter("@ApplicationRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@ExchangeStatus", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeTime", SqlDbType.DateTime),
				new SqlParameter("@ExchangeUserID", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@ExchangeType", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MemID;
			parameters[1].Value = model.ExchangeTelePhone;
			parameters[2].Value = model.ExchangeAddress;
			parameters[3].Value = model.ExchangeAccount;
			parameters[4].Value = model.ExchangeAllNumber;
			parameters[5].Value = model.ExchangeAllPoint;
			parameters[6].Value = model.ApplicationTime;
			parameters[7].Value = model.ApplicationRemark;
			parameters[8].Value = model.ExchangeStatus;
			parameters[9].Value = model.ExchangeTime;
			parameters[10].Value = model.ExchangeUserID;
			parameters[11].Value = model.ExchangeRemark;
			parameters[12].Value = model.ExchangeType;
			parameters[13].Value = model.ExchangeID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ExchangeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GiftExchange ");
			strSql.Append(" where ExchangeID=@ExchangeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ExchangeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ExchangeID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string ExchangeIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GiftExchange ");
			strSql.Append(" where ExchangeID in (" + ExchangeIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.GiftExchange GetModel(int ExchangeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ExchangeID,MemID,ExchangeTelePhone,ExchangeAddress,ExchangeAccount,ExchangeAllNumber,ExchangeAllPoint,ApplicationTime,ApplicationRemark,ExchangeStatus,ExchangeTime,ExchangeUserID,ExchangeRemark,ExchangeType from GiftExchange ");
			strSql.Append(" where ExchangeID=@ExchangeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ExchangeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ExchangeID;
			Chain.Model.GiftExchange model = new Chain.Model.GiftExchange();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.GiftExchange result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ExchangeID"] != null && ds.Tables[0].Rows[0]["ExchangeID"].ToString() != "")
				{
					model.ExchangeID = int.Parse(ds.Tables[0].Rows[0]["ExchangeID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemID"] != null && ds.Tables[0].Rows[0]["MemID"].ToString() != "")
				{
					model.MemID = int.Parse(ds.Tables[0].Rows[0]["MemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExchangeTelePhone"] != null && ds.Tables[0].Rows[0]["ExchangeTelePhone"].ToString() != "")
				{
					model.ExchangeTelePhone = ds.Tables[0].Rows[0]["ExchangeTelePhone"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ExchangeAddress"] != null && ds.Tables[0].Rows[0]["ExchangeAddress"].ToString() != "")
				{
					model.ExchangeAddress = ds.Tables[0].Rows[0]["ExchangeAddress"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ExchangeAccount"] != null && ds.Tables[0].Rows[0]["ExchangeAccount"].ToString() != "")
				{
					model.ExchangeAccount = ds.Tables[0].Rows[0]["ExchangeAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ExchangeAllNumber"] != null && ds.Tables[0].Rows[0]["ExchangeAllNumber"].ToString() != "")
				{
					model.ExchangeAllNumber = int.Parse(ds.Tables[0].Rows[0]["ExchangeAllNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExchangeAllPoint"] != null && ds.Tables[0].Rows[0]["ExchangeAllPoint"].ToString() != "")
				{
					model.ExchangeAllPoint = int.Parse(ds.Tables[0].Rows[0]["ExchangeAllPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ApplicationTime"] != null && ds.Tables[0].Rows[0]["ApplicationTime"].ToString() != "")
				{
					model.ApplicationTime = DateTime.Parse(ds.Tables[0].Rows[0]["ApplicationTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ApplicationRemark"] != null && ds.Tables[0].Rows[0]["ApplicationRemark"].ToString() != "")
				{
					model.ApplicationRemark = ds.Tables[0].Rows[0]["ApplicationRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ExchangeStatus"] != null && ds.Tables[0].Rows[0]["ExchangeStatus"].ToString() != "")
				{
					model.ExchangeStatus = int.Parse(ds.Tables[0].Rows[0]["ExchangeStatus"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExchangeTime"] != null && ds.Tables[0].Rows[0]["ExchangeTime"].ToString() != "")
				{
					model.ExchangeTime = DateTime.Parse(ds.Tables[0].Rows[0]["ExchangeTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExchangeUserID"] != null && ds.Tables[0].Rows[0]["ExchangeUserID"].ToString() != "")
				{
					model.ExchangeUserID = int.Parse(ds.Tables[0].Rows[0]["ExchangeUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExchangeRemark"] != null && ds.Tables[0].Rows[0]["ExchangeRemark"].ToString() != "")
				{
					model.ExchangeRemark = ds.Tables[0].Rows[0]["ExchangeRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ExchangeType"] != null && ds.Tables[0].Rows[0]["ExchangeType"].ToString() != "")
				{
					model.ExchangeType = int.Parse(ds.Tables[0].Rows[0]["ExchangeType"].ToString());
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
			strSql.Append("select ExchangeID,MemID,ExchangeTelePhone,ExchangeAddress,ExchangeAccount,ExchangeAllNumber,ExchangeAllPoint,ApplicationTime,ApplicationRemark,ExchangeStatus,ExchangeTime,ExchangeUserID,ExchangeRemark,ExchangeType ");
			strSql.Append(" FROM GiftExchange ");
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
			strSql.Append(" ExchangeID,MemID,ExchangeTelePhone,ExchangeAddress,ExchangeAccount,ExchangeAllNumber,ExchangeAllPoint,ApplicationTime,ApplicationRemark,ExchangeStatus,ExchangeTime,ExchangeUserID,ExchangeRemark,ExchangeType ");
			strSql.Append(" FROM GiftExchange ");
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
			strSql.Append("select count(1) FROM GiftExchange ");
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

		public DataSet GetGiftExchangeByTime(DateTime starttime, DateTime endtime, string strwhere)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EndTime", SqlDbType.DateTime),
				new SqlParameter("@strWhere", SqlDbType.NVarChar, 2000)
			};
			parameters[0].Value = starttime;
			parameters[1].Value = endtime;
			parameters[2].Value = strwhere;
			return DbHelperSQL.RunProcedure("MonthGiftExchange", parameters, "#GiftExchangeData");
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
				strSql.Append("order by T.ExchangeID desc");
			}
			strSql.Append(")AS Row, T.*  from GiftExchange T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetVerifyListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "GiftExchange,Mem";
			string[] columns = new string[]
			{
				"ExchangeID, ExchangeTelePhone, ExchangeAddress, ExchangeAccount, ExchangeAllNumber, ExchangeAllPoint, ApplicationTime, ApplicationRemark, ExchangeStatus, ExchangeTime, ExchangeUserID, ExchangeRemark, ExchangeType, ShopID,Mem.* "
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ExchangeID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "GiftExchange,Mem,SysShop,SysUser";
			string[] columns = new string[]
			{
				"ExchangeAccount,MemCard,Mem.MemName,ExchangeAllNumber,ExchangeAllPoint,ApplicationTime,ExchangeTime,ShopName,UserName,ExchangeType,ExchangeID,ExchangeStatus"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ExchangeID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetListGiftExchangeSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "GiftExchange";
			string[] columns = new string[]
			{
				"ExchangeID, ExchangeAccount,ExchangeAllNumber,ExchangeAllPoint,ApplicationTime,ExchangeStatus,ExchangeRemark,ExchangeType"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ExchangeID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetListGiftExchangeSPForWeiXin(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "GiftExchange,Mem";
			string[] columns = new string[]
			{
				"ExchangeID, ExchangeAccount,ExchangeAllNumber,ExchangeAllPoint,ApplicationTime,ExchangeStatus,ExchangeRemark,ExchangeType"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ExchangeID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataTable GetExchangeIDByGiftNameOrGiftCode(string giftNameOrGiftCodeStrWhere)
		{
			return DbHelperSQL.Query("select DISTINCT ExchangeID from GiftExchangeDetail join PointGift on GiftExchangeDetail.ExchangeGiftID=PointGift.GiftID where " + giftNameOrGiftCodeStrWhere).Tables[0];
		}

		public DataTable AgainPrint(int rechargeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ");
			strSql.Append("MemCard,MemName,ExchangeAllPoint,ExchangeAllNumber,MemPoint,UserName,ExchangeTime,ExchangeAccount ");
			strSql.Append("from SysUser,GiftExchange,Mem ");
			strSql.Append("where SysUser.UserID=GiftExchange.ExchangeUserID ");
			strSql.Append("and Mem.MemID=GiftExchange.MemID ");
			strSql.Append("and ExchangeID=@ExchangeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ExchangeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = rechargeID;
			return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
		}

		public DataSet GetGiftExchange(string strSql)
		{
			StringBuilder strWhere = new StringBuilder();
			strWhere.Append(" select isnull(sum(ExchangeAllNumber),0) as ExchangeAllNumber ,isnull(sum(ExchangeAllPoint),0) as ExchangeAllPoint from GiftExchange,Mem ");
			if (strSql != "")
			{
				strWhere.Append(" where " + strSql);
			}
			return DbHelperSQL.Query(strWhere.ToString());
		}
	}
}
