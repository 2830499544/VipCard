using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysRotatePrizeLog
	{
		public bool ExistsPrizeCode(int RotateID, string PrizeCode)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysRotatePrizeLog");
			strSql.Append(" where RotateID=@RotateID and PrizeCode=@PrizeCode");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RotateID", SqlDbType.Int, 4),
				new SqlParameter("@PrizeCode", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = RotateID;
			parameters[1].Value = PrizeCode;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(int PrizeLogID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysRotatePrizeLog");
			strSql.Append(" where PrizeLogID=@PrizeLogID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PrizeLogID", SqlDbType.Int, 4)
			};
			parameters[0].Value = PrizeLogID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysRotatePrizeLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysRotatePrizeLog(");
			strSql.Append("PrizeAccount,RotateID,PrizeLevel,MemID,CreateTime,PrizeStatus,PrizeCode)");
			strSql.Append(" values (");
			strSql.Append("@PrizeAccount,@RotateID,@PrizeLevel,@MemID,@CreateTime,@PrizeStatus,@PrizeCode)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RotateID", SqlDbType.Int, 4),
				new SqlParameter("@PrizeLevel", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@PrizeStatus", SqlDbType.Int, 4),
				new SqlParameter("@PrizeAccount", SqlDbType.NVarChar, 50),
				new SqlParameter("@PrizeCode", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = model.RotateID;
			parameters[1].Value = model.PrizeLevel;
			parameters[2].Value = model.MemID;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.PrizeStatus;
			parameters[5].Value = model.PrizeAccount;
			parameters[6].Value = model.PrizeCode;
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

		public int Update(Chain.Model.SysRotatePrizeLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysRotatePrizeLog set ");
			strSql.Append("RotateID=@RotateID,");
			strSql.Append("PrizeLevel=@PrizeLevel,");
			strSql.Append("MemID=@MemID,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("PrizeStatus=@PrizeStatus,");
			strSql.Append("PrizeAccount=@PrizeAccount,");
			strSql.Append("GiveTime=@GiveTime,");
			strSql.Append("GiveUserID=@GiveUserID");
			strSql.Append(" where PrizeLogID=@PrizeLogID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RotateID", SqlDbType.Int, 4),
				new SqlParameter("@PrizeLevel", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@PrizeStatus", SqlDbType.Int, 4),
				new SqlParameter("@PrizeAccount", SqlDbType.NVarChar, 50),
				new SqlParameter("@PrizeLogID", SqlDbType.Int, 4),
				new SqlParameter("@GiveTime", SqlDbType.DateTime),
				new SqlParameter("@GiveUserID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.RotateID;
			parameters[1].Value = model.PrizeLevel;
			parameters[2].Value = model.MemID;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.PrizeStatus;
			parameters[5].Value = model.PrizeAccount;
			parameters[6].Value = model.PrizeLogID;
			parameters[7].Value = model.GiveTime;
			parameters[8].Value = model.GiveUserID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public bool Delete(int PrizeLogID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysRotatePrizeLog ");
			strSql.Append(" where PrizeLogID=@PrizeLogID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PrizeLogID", SqlDbType.Int, 4)
			};
			parameters[0].Value = PrizeLogID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string RotateIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysRotatePrizeLog ");
			strSql.Append(" where PrizeLogID in (" + RotateIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysRotatePrizeLog GetModel(int PrizeLogID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 PrizeLogID,PrizeAccount,RotateID,PrizeLevel,MemID,CreateTime,PrizeStatus ,GiveUserID,GiveTime,PrizeCode from SysRotatePrizeLog ");
			strSql.Append(" where PrizeLogID=@PrizeLogID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PrizeLogID", SqlDbType.Int, 4)
			};
			parameters[0].Value = PrizeLogID;
			Chain.Model.SysRotatePrizeLog model = new Chain.Model.SysRotatePrizeLog();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysRotatePrizeLog result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["PrizeCode"] != null && ds.Tables[0].Rows[0]["PrizeCode"].ToString() != "")
				{
					model.PrizeCode = ds.Tables[0].Rows[0]["PrizeCode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GiveUserID"] != null && ds.Tables[0].Rows[0]["GiveUserID"].ToString() != "")
				{
					model.GiveUserID = int.Parse(ds.Tables[0].Rows[0]["GiveUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GiveTime"] != null && ds.Tables[0].Rows[0]["GiveTime"].ToString() != "")
				{
					model.GiveTime = DateTime.Parse(ds.Tables[0].Rows[0]["GiveTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PrizeLogID"] != null && ds.Tables[0].Rows[0]["PrizeLogID"].ToString() != "")
				{
					model.PrizeLogID = int.Parse(ds.Tables[0].Rows[0]["PrizeLogID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PrizeAccount"] != null && ds.Tables[0].Rows[0]["PrizeAccount"].ToString() != "")
				{
					model.PrizeAccount = ds.Tables[0].Rows[0]["PrizeAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RotateID"] != null && ds.Tables[0].Rows[0]["RotateID"].ToString() != "")
				{
					model.RotateID = int.Parse(ds.Tables[0].Rows[0]["RotateID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PrizeLevel"] != null && ds.Tables[0].Rows[0]["PrizeLevel"].ToString() != "")
				{
					model.PrizeLevel = ds.Tables[0].Rows[0]["PrizeLevel"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MemID"] != null && ds.Tables[0].Rows[0]["MemID"].ToString() != "")
				{
					model.MemID = int.Parse(ds.Tables[0].Rows[0]["MemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
				{
					model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PrizeStatus"] != null && ds.Tables[0].Rows[0]["PrizeStatus"].ToString() != "")
				{
					model.PrizeStatus = int.Parse(ds.Tables[0].Rows[0]["PrizeStatus"].ToString());
				}
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public int GetMemRotateCount(int MemID, int RotateID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) from SysRotatePrizeLog");
			strSql.Append(" where MemID=@MemID and RotateID=@RotateID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@RotateID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MemID;
			parameters[1].Value = RotateID;
			return int.Parse(DbHelperSQL.GetSingle(strSql.ToString(), parameters).ToString());
		}

		public int GetRotateCount(int RotateID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(*) from SysRotatePrizeLog");
			strSql.Append(" where RotateID=@RotateID and PrizeLevel<>'未中奖' ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RotateID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RotateID;
			return int.Parse(DbHelperSQL.GetSingle(strSql.ToString(), parameters).ToString());
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select PrizeLogID,LogUserID,LogActionID,LogType,LogDetail,LogShopID,LogCreateTime,LogIPAdress ");
			strSql.Append(" FROM SysRotatePrizeLog ");
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
			strSql.Append("Mem.MemName,Mem.MemMobile,Mem.MemPhoto, SysRotatePrizeLog.PrizeLevel,SysRotatePrizeLog.CreateTime,case when PrizeLevel='一等奖' then  OneName  when PrizeLevel='二等奖' then  TwoName  when PrizeLevel='三等奖' then  ThreeName  when PrizeLevel='四等奖' then  FourName when PrizeLevel='五等奖' then  FiveName  when PrizeLevel='六等奖' then  SixName else '未中奖' end PrizeLevel,case when PrizeLevel='一等奖' then  OnePrizeName  when PrizeLevel='二等奖' then  TwoPrizeName  when PrizeLevel='三等奖' then  ThreePrizeName  when PrizeLevel='四等奖' then  FourPrizeName when PrizeLevel='五等奖' then  FivePrizeName  when PrizeLevel='六等奖' then  SixPrizeName else '' end PrizeName");
			strSql.Append(" FROM SysRotatePrizeLog,Mem,SysRotate ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by " + filedOrder + " desc");
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) FROM SysRotatePrizeLog ");
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
				strSql.Append("order by T.PrizeLogID desc");
			}
			strSql.Append(")AS Row, T.*  from SysRotatePrizeLog T ");
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
			string tableName = "SysRotatePrizeLog,Mem,SysRotate,MemLevel,SysShop";
			string[] columns = new string[]
			{
				"SysRotatePrizeLog.*,Mem.MemName,Mem.MemCard,MemLevel.LevelName,SysShop.ShopName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "SysRotatePrizeLog.CreateTime", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetListSPNew(int PageSize, int PageIndex, params string[] strWhere)
		{
			string tableName = "SysRotatePrizeLog,Mem,SysRotate";
			string[] columns = new string[]
			{
				"SysRotate.RotateID,StartTime,EndTime,RotateName,Mem.MemID,MemName,MemMobile,            case when PrizeLevel='一等奖' then  OneName  when PrizeLevel='二等奖' then  TwoName  when PrizeLevel='三等奖' then  ThreeName  when PrizeLevel='四等奖' then  FourName when PrizeLevel='五等奖' then  FiveName  when PrizeLevel='六等奖' then  SixName else '未中奖' end PrizeLevel,            case when PrizeLevel='一等奖' then  OnePrizeName  when PrizeLevel='二等奖' then  TwoPrizeName  when PrizeLevel='三等奖' then  ThreePrizeName  when PrizeLevel='四等奖' then  FourPrizeName when PrizeLevel='五等奖' then  FivePrizeName  when PrizeLevel='六等奖' then  SixPrizeName else '' end PrizeName,SysRotatePrizeLog.CreateTime"
			};
			int recordCount = 1;
			return DbHelperSQL.GetTable(tableName, columns, strWhere, "SysRotatePrizeLog.CreateTime", false, PageSize, PageIndex, recordCount);
		}

		public DataSet GetListNew(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ");
			strSql.Append("SysRotate.RotateID,StartTime,EndTime,RotateName,Mem.MemID,MemName,MemMobile, case when PrizeLevel='一等奖' then  OneName  when PrizeLevel='二等奖' then  TwoName  when PrizeLevel='三等奖' then  ThreeName  when PrizeLevel='四等奖' then  FourName when PrizeLevel='五等奖' then  FiveName  when PrizeLevel='六等奖' then  SixName else '未中奖' end PrizeLevel,case when PrizeLevel='一等奖' then  OnePrizeName  when PrizeLevel='二等奖' then  TwoPrizeName  when PrizeLevel='三等奖' then  ThreePrizeName  when PrizeLevel='四等奖' then  FourPrizeName when PrizeLevel='五等奖' then  FivePrizeName  when PrizeLevel='六等奖' then  SixPrizeName else '' end PrizeName,SysRotatePrizeLog.CreateTime");
			strSql.Append(" FROM SysRotatePrizeLog,Mem,SysRotate ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by SysRotate.CreateTime ");
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(bool isasc, string order, int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "V_GetSysRotateSum";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, order, "MemID", isasc, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
