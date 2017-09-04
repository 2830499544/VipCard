using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class MemRecharge
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("RechargeID", "MemRecharge");
		}

		public bool Exists(int RechargeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemRecharge");
			strSql.Append(" where RechargeID=@RechargeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RechargeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RechargeID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(string orderaccount)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemRecharge");
			strSql.Append(" where RechargeAccount=@RechargeAccount");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RechargeAccount", SqlDbType.VarChar, 50)
			};
			parameters[0].Value = orderaccount;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.MemRecharge model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MemRecharge(");
			strSql.Append("RechargeMemID,RechargeAccount,RechargeType,RechargeMoney,RechargeGive,RechargeRemark,RechargeShopID,RechargeCreateTime,RechargeUserID,RechargeCardBalance,RechargeIsApprove,RechargePoint)");
			strSql.Append(" values (");
			strSql.Append("@RechargeMemID,@RechargeAccount,@RechargeType,@RechargeMoney,@RechargeGive,@RechargeRemark,@RechargeShopID,@RechargeCreateTime,@RechargeUserID,@RechargeCardBalance,@RechargeIsApprove,@RechargePoint)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RechargeMemID", SqlDbType.Int, 4),
				new SqlParameter("@RechargeAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@RechargeType", SqlDbType.TinyInt, 1),
				new SqlParameter("@RechargeMoney", SqlDbType.Money, 8),
				new SqlParameter("@RechargeGive", SqlDbType.Money, 8),
				new SqlParameter("@RechargeRemark", SqlDbType.NVarChar, 1000),
				new SqlParameter("@RechargeShopID", SqlDbType.Int, 4),
				new SqlParameter("@RechargeCreateTime", SqlDbType.DateTime),
				new SqlParameter("@RechargeUserID", SqlDbType.Int, 4),
				new SqlParameter("@RechargeCardBalance", SqlDbType.Money, 8),
				new SqlParameter("@RechargeIsApprove", SqlDbType.Bit),
				new SqlParameter("@RechargePoint", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.RechargeMemID;
			parameters[1].Value = model.RechargeAccount;
			parameters[2].Value = model.RechargeType;
			parameters[3].Value = model.RechargeMoney;
			parameters[4].Value = model.RechargeGive;
			parameters[5].Value = model.RechargeRemark;
			parameters[6].Value = model.RechargeShopID;
			parameters[7].Value = model.RechargeCreateTime;
			parameters[8].Value = model.RechargeUserID;
			parameters[9].Value = model.RechargeCardBalance;
			parameters[10].Value = model.RechargeIsApprove;
			parameters[11].Value = model.RechargePoint;
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

		public bool Update(Chain.Model.MemRecharge model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MemRecharge set ");
			strSql.Append("RechargeMemID=@RechargeMemID,");
			strSql.Append("RechargeAccount=@RechargeAccount,");
			strSql.Append("RechargeType=@RechargeType,");
			strSql.Append("RechargeMoney=@RechargeMoney,");
			strSql.Append("RechargeGive=@RechargeGive,");
			strSql.Append("RechargeRemark=@RechargeRemark,");
			strSql.Append("RechargeShopID=@RechargeShopID,");
			strSql.Append("RechargeCreateTime=@RechargeCreateTime,");
			strSql.Append("RechargeUserID=@RechargeUserID,");
			strSql.Append("RechargeCardBalance=@RechargeCardBalance,");
			strSql.Append("RechargeIsApprove=@RechargeIsApprove,");
			strSql.Append("RechargePoint=@RechargePoint ");
			strSql.Append(" where RechargeID=@RechargeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RechargeMemID", SqlDbType.Int, 4),
				new SqlParameter("@RechargeAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@RechargeType", SqlDbType.TinyInt, 1),
				new SqlParameter("@RechargeMoney", SqlDbType.Money, 8),
				new SqlParameter("@RechargeGive", SqlDbType.Money, 8),
				new SqlParameter("@RechargeRemark", SqlDbType.NVarChar, 1000),
				new SqlParameter("@RechargeShopID", SqlDbType.Int, 4),
				new SqlParameter("@RechargeCreateTime", SqlDbType.DateTime),
				new SqlParameter("@RechargeUserID", SqlDbType.Int, 4),
				new SqlParameter("@RechargeCardBalance", SqlDbType.Money, 8),
				new SqlParameter("@RechargeIsApprove", SqlDbType.Bit),
				new SqlParameter("@RechargePoint", SqlDbType.Int, 4),
				new SqlParameter("@RechargeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.RechargeMemID;
			parameters[1].Value = model.RechargeAccount;
			parameters[2].Value = model.RechargeType;
			parameters[3].Value = model.RechargeMoney;
			parameters[4].Value = model.RechargeGive;
			parameters[5].Value = model.RechargeRemark;
			parameters[6].Value = model.RechargeShopID;
			parameters[7].Value = model.RechargeCreateTime;
			parameters[8].Value = model.RechargeUserID;
			parameters[9].Value = model.RechargeCardBalance;
			parameters[10].Value = model.RechargeIsApprove;
			parameters[11].Value = model.RechargePoint;
			parameters[12].Value = model.RechargeID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int RechargeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemRecharge ");
			strSql.Append(" where RechargeID=@RechargeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RechargeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RechargeID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string RechargeIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemRecharge ");
			strSql.Append(" where RechargeID in (" + RechargeIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MemRecharge GetModel(int RechargeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 RechargeID,RechargeMemID,RechargeAccount,RechargeType,RechargeMoney,RechargeGive,RechargeRemark,RechargeShopID,RechargeCreateTime,RechargeUserID,RechargeCardBalance,RechargeIsApprove,RechargePoint from MemRecharge ");
			strSql.Append(" where RechargeID=@RechargeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RechargeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RechargeID;
			Chain.Model.MemRecharge model = new Chain.Model.MemRecharge();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MemRecharge result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["RechargeID"] != null && ds.Tables[0].Rows[0]["RechargeID"].ToString() != "")
				{
					model.RechargeID = int.Parse(ds.Tables[0].Rows[0]["RechargeID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RechargeMemID"] != null && ds.Tables[0].Rows[0]["RechargeMemID"].ToString() != "")
				{
					model.RechargeMemID = int.Parse(ds.Tables[0].Rows[0]["RechargeMemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RechargeAccount"] != null && ds.Tables[0].Rows[0]["RechargeAccount"].ToString() != "")
				{
					model.RechargeAccount = ds.Tables[0].Rows[0]["RechargeAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RechargeType"] != null && ds.Tables[0].Rows[0]["RechargeType"].ToString() != "")
				{
					model.RechargeType = int.Parse(ds.Tables[0].Rows[0]["RechargeType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RechargeMoney"] != null && ds.Tables[0].Rows[0]["RechargeMoney"].ToString() != "")
				{
					model.RechargeMoney = decimal.Parse(ds.Tables[0].Rows[0]["RechargeMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RechargeGive"] != null && ds.Tables[0].Rows[0]["RechargeGive"].ToString() != "")
				{
					model.RechargeGive = decimal.Parse(ds.Tables[0].Rows[0]["RechargeGive"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RechargeRemark"] != null && ds.Tables[0].Rows[0]["RechargeRemark"].ToString() != "")
				{
					model.RechargeRemark = ds.Tables[0].Rows[0]["RechargeRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["RechargeShopID"] != null && ds.Tables[0].Rows[0]["RechargeShopID"].ToString() != "")
				{
					model.RechargeShopID = int.Parse(ds.Tables[0].Rows[0]["RechargeShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RechargeCreateTime"] != null && ds.Tables[0].Rows[0]["RechargeCreateTime"].ToString() != "")
				{
					model.RechargeCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["RechargeCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RechargeUserID"] != null && ds.Tables[0].Rows[0]["RechargeUserID"].ToString() != "")
				{
					model.RechargeUserID = int.Parse(ds.Tables[0].Rows[0]["RechargeUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RechargeCardBalance"] != null && ds.Tables[0].Rows[0]["RechargeCardBalance"].ToString() != "")
				{
					model.RechargeCardBalance = decimal.Parse(ds.Tables[0].Rows[0]["RechargeCardBalance"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RechargeIsApprove"] != null && ds.Tables[0].Rows[0]["RechargeIsApprove"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["RechargeIsApprove"].ToString() == "1" || ds.Tables[0].Rows[0]["RechargeIsApprove"].ToString().ToLower() == "true")
					{
						model.RechargeIsApprove = true;
					}
					else
					{
						model.RechargeIsApprove = false;
					}
				}
				if (ds.Tables[0].Rows[0]["RechargePoint"] != null && ds.Tables[0].Rows[0]["RechargePoint"].ToString() != "")
				{
					model.RechargePoint = int.Parse(ds.Tables[0].Rows[0]["RechargePoint"].ToString());
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
			strSql.Append("select RechargeID,RechargeMemID,RechargeAccount,RechargeType,RechargeMoney,RechargeGive,RechargeRemark,RechargeShopID,RechargeCreateTime,RechargeUserID,RechargeCardBalance,RechargeIsApprove ,RechargePoint ");
			strSql.Append(" FROM MemRecharge ");
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
			strSql.Append(" RechargeID,RechargeMemID,RechargeAccount,RechargeType,RechargeMoney,RechargeGive,RechargeRemark,RechargeShopID,RechargeCreateTime,RechargeUserID,RechargeCardBalance,RechargeIsApprove,RechargePoint ");
			strSql.Append(" FROM MemRecharge ");
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
			strSql.Append("select count(1) FROM MemRecharge ");
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

		public decimal GetRechargeMoney(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
            strSql.Append("select SUM(RechargeMoney) FROM MemRecharge,Mem,MemLevel,SysShop,SysUser ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			decimal result;
			if (obj == null)
			{
				result = 0m;
			}
			else
			{
				result = Convert.ToDecimal(obj);
			}
			return result;
		}

		public decimal GetGiveMoney(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
            strSql.Append("select SUM(RechargeGive) FROM MemRecharge,Mem,MemLevel,SysShop,SysUser ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			decimal result;
			if (obj == null)
			{
				result = 0m;
			}
			else
			{
				result = Convert.ToDecimal(obj);
			}
			return result;
		}

		public DataSet GetRechargeByTime(DateTime starttime, DateTime endtime, string strwhere)
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
			return DbHelperSQL.RunProcedure("MonthRecharge", parameters, "#RechargeData");
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
				strSql.Append("order by T.RechargeID desc");
			}
			strSql.Append(")AS Row, T.*  from MemRecharge T ");
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
			string tableName = "MemRecharge,Mem,MemLevel,SysShop,SysUser";
			string[] columns = new string[]
			{
				"MemRecharge.*,(RechargeMoney-RechargeGive) as  RechargeOrdMoney,Mem.*,MemLevel.LevelName,SysShop.ShopName,SysUser.UserName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "RechargeID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetListSP1(int PageSize, int PageIndex, string IndexColumn, bool isAsc, out int resCount, params string[] strWhere)
		{
			string[] columns = new string[]
			{
				"*"
			};
			DataSet ds = DbHelperSQL.GetTable(columns, strWhere, IndexColumn, isAsc, PageSize, PageIndex, "CP_MemRechargeReport");
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetListSP2(int PageSize, int PageIndex, string IndexColumn, bool isAsc, out int resCount, params string[] strWhere)
		{
			string[] columns = new string[]
			{
				"*"
			};
			DataSet ds = DbHelperSQL.GetTable(columns, strWhere, IndexColumn, isAsc, PageSize, PageIndex, "CP_ShopRechargeReport");
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public decimal GetRecMoney(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select sum(RechargeMoney-RechargeGive) from MemRecharge where RechargeMoney > 0 AND RechargeIsApprove=1 ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" and " + strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			decimal result;
			if (obj == null)
			{
				result = 0m;
			}
			else
			{
				result = Convert.ToDecimal(obj);
			}
			return result;
		}

		public DataTable AgainPrint(int rechargeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select RechargeMoney,RechargeGive,MemCard,MemName,RechargeCreateTime,UserName,RechargeAccount,MemMoney,RechargeCardBalance,RechargePoint ");
			strSql.Append("from MemRecharge,Mem,SysUser ");
			strSql.Append("where MemRecharge.RechargeMemID=Mem.MemID ");
			strSql.Append("and MemRecharge.RechargeUserID=SysUser.UserID ");
			strSql.Append("and RechargeID=@RechargeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RechargeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = rechargeID;
			return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
		}

		public int UpdateRecharge(int rechargeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.AppendFormat(" update MemRecharge set RechargeIsApprove=1 where RechargeID={0} ", rechargeID);
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}
	}
}
