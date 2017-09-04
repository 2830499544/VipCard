using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class SysUserWork
	{
		public bool Exists(int SysUserWorkID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysUserWork");
			strSql.Append(" where SysUserWorkID=@SysUserWorkID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SysUserWorkID", SqlDbType.Int, 4)
			};
			parameters[0].Value = SysUserWorkID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysUserWork model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysUserWork(");
			strSql.Append("UserID,StartTime,EedTime,AddNewUser,CardMoney,ExpenseSumMoneys,ExpenseBinkMoneys,ExpenseCouponMoneys,SRechargeMoney,FRechargeMoney,RechargeBank,FRechargeGiveMoney,AllMoneys,sjMoneys,HandoverUserID,Arrearage,Ispay,remark)");
			strSql.Append(" values (");
			strSql.Append("@UserID,@StartTime,@EedTime,@AddNewUser,@CardMoney,@ExpenseSumMoneys,@ExpenseBinkMoneys,@ExpenseCouponMoneys,@SRechargeMoney,@FRechargeMoney,@RechargeBank,@FRechargeGiveMoney,@AllMoneys,@sjMoneys,@HandoverUserID,@Arrearage,@Ispay,@remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EedTime", SqlDbType.DateTime),
				new SqlParameter("@AddNewUser", SqlDbType.Int, 4),
				new SqlParameter("@CardMoney", SqlDbType.Money, 8),
				new SqlParameter("@ExpenseSumMoneys", SqlDbType.Money, 8),
				new SqlParameter("@ExpenseBinkMoneys", SqlDbType.Money, 8),
				new SqlParameter("@ExpenseCouponMoneys", SqlDbType.Money, 8),
				new SqlParameter("@SRechargeMoney", SqlDbType.Money, 8),
				new SqlParameter("@FRechargeMoney", SqlDbType.Money, 8),
				new SqlParameter("@RechargeBank", SqlDbType.Money, 8),
				new SqlParameter("@FRechargeGiveMoney", SqlDbType.Money, 8),
				new SqlParameter("@AllMoneys", SqlDbType.Money, 8),
				new SqlParameter("@sjMoneys", SqlDbType.Money, 8),
				new SqlParameter("@HandoverUserID", SqlDbType.Int, 4),
				new SqlParameter("@Arrearage", SqlDbType.Money, 8),
				new SqlParameter("@Ispay", SqlDbType.Bit, 1),
				new SqlParameter("@remark", SqlDbType.NVarChar, 500)
			};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.StartTime;
			parameters[2].Value = model.EedTime;
			parameters[3].Value = model.AddNewUser;
			parameters[4].Value = model.CardMoney;
			parameters[5].Value = model.ExpenseSumMoneys;
			parameters[6].Value = model.ExpenseBinkMoneys;
			parameters[7].Value = model.ExpenseCouponMoneys;
			parameters[8].Value = model.SRechargeMoney;
			parameters[9].Value = model.FRechargeMoney;
			parameters[10].Value = model.RechargeBank;
			parameters[11].Value = model.FRechargeGiveMoney;
			parameters[12].Value = model.AllMoneys;
			parameters[13].Value = model.sjMoneys;
			parameters[14].Value = model.HandoverUserID;
			parameters[15].Value = model.Arrearage;
			parameters[16].Value = model.Ispay;
			parameters[17].Value = model.remark;
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

		public bool Update(Chain.Model.SysUserWork model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysUserWork set ");
			strSql.Append("UserID=@UserID,");
			strSql.Append("StartTime=@StartTime,");
			strSql.Append("EedTime=@EedTime,");
			strSql.Append("AddNewUser=@AddNewUser,");
			strSql.Append("CardMoney=@CardMoney,");
			strSql.Append("ExpenseSumMoneys=@ExpenseSumMoneys,");
			strSql.Append("ExpenseBinkMoneys=@ExpenseBinkMoneys,");
			strSql.Append("ExpenseCouponMoneys=@ExpenseCouponMoneys,");
			strSql.Append("SRechargeMoney=@SRechargeMoney,");
			strSql.Append("FRechargeMoney=@FRechargeMoney,");
			strSql.Append("RechargeBank=@RechargeBank,");
			strSql.Append("FRechargeGiveMoney=@FRechargeGiveMoney,");
			strSql.Append("AllMoneys=@AllMoneys,");
			strSql.Append("sjMoneys=@sjMoneys,");
			strSql.Append("HandoverUserID=@HandoverUserID,");
			strSql.Append("Arrearage=@Arrearage,");
			strSql.Append("Ispay=@Ispay,");
			strSql.Append("remark=@remark");
			strSql.Append(" where SysUserWorkID=@SysUserWorkID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EedTime", SqlDbType.DateTime),
				new SqlParameter("@AddNewUser", SqlDbType.Int, 4),
				new SqlParameter("@CardMoney", SqlDbType.Money, 8),
				new SqlParameter("@ExpenseSumMoneys", SqlDbType.Money, 8),
				new SqlParameter("@ExpenseBinkMoneys", SqlDbType.Money, 8),
				new SqlParameter("@ExpenseCouponMoneys", SqlDbType.Money, 8),
				new SqlParameter("@SRechargeMoney", SqlDbType.Money, 8),
				new SqlParameter("@FRechargeMoney", SqlDbType.Money, 8),
				new SqlParameter("@RechargeBank", SqlDbType.Money, 8),
				new SqlParameter("@FRechargeGiveMoney", SqlDbType.Money, 8),
				new SqlParameter("@AllMoneys", SqlDbType.Money, 8),
				new SqlParameter("@sjMoneys", SqlDbType.Money, 8),
				new SqlParameter("@HandoverUserID", SqlDbType.Int, 4),
				new SqlParameter("@Arrearage", SqlDbType.Money, 8),
				new SqlParameter("@Ispay", SqlDbType.Bit, 1),
				new SqlParameter("@remark", SqlDbType.NVarChar, 500),
				new SqlParameter("@SysUserWorkID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.StartTime;
			parameters[2].Value = model.EedTime;
			parameters[3].Value = model.AddNewUser;
			parameters[4].Value = model.CardMoney;
			parameters[5].Value = model.ExpenseSumMoneys;
			parameters[6].Value = model.ExpenseBinkMoneys;
			parameters[7].Value = model.ExpenseCouponMoneys;
			parameters[8].Value = model.SRechargeMoney;
			parameters[9].Value = model.FRechargeMoney;
			parameters[10].Value = model.RechargeBank;
			parameters[11].Value = model.FRechargeGiveMoney;
			parameters[12].Value = model.AllMoneys;
			parameters[13].Value = model.sjMoneys;
			parameters[14].Value = model.HandoverUserID;
			parameters[15].Value = model.Arrearage;
			parameters[16].Value = model.Ispay;
			parameters[17].Value = model.remark;
			parameters[18].Value = model.SysUserWorkID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int SysUserWorkID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysUserWork ");
			strSql.Append(" where SysUserWorkID=@SysUserWorkID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SysUserWorkID", SqlDbType.Int, 4)
			};
			parameters[0].Value = SysUserWorkID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string SysUserWorkIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysUserWork ");
			strSql.Append(" where SysUserWorkID in (" + SysUserWorkIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysUserWork GetModel(int SysUserWorkID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 SysUserWorkID,UserID,StartTime,EedTime,AddNewUser,CardMoney,ExpenseSumMoneys,ExpenseBinkMoneys,ExpenseCouponMoneys,SRechargeMoney,FRechargeMoney,RechargeBank,FRechargeGiveMoney,AllMoneys,sjMoneys,HandoverUserID,Arrearage,Ispay,remark from SysUserWork ");
			strSql.Append(" where SysUserWorkID=@SysUserWorkID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SysUserWorkID", SqlDbType.Int, 4)
			};
			parameters[0].Value = SysUserWorkID;
			Chain.Model.SysUserWork model = new Chain.Model.SysUserWork();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysUserWork result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["SysUserWorkID"] != null && ds.Tables[0].Rows[0]["SysUserWorkID"].ToString() != "")
				{
					model.SysUserWorkID = int.Parse(ds.Tables[0].Rows[0]["SysUserWorkID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
				{
					model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StartTime"] != null && ds.Tables[0].Rows[0]["StartTime"].ToString() != "")
				{
					model.StartTime = DateTime.Parse(ds.Tables[0].Rows[0]["StartTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["EedTime"] != null && ds.Tables[0].Rows[0]["EedTime"].ToString() != "")
				{
					model.EedTime = DateTime.Parse(ds.Tables[0].Rows[0]["EedTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AddNewUser"] != null && ds.Tables[0].Rows[0]["AddNewUser"].ToString() != "")
				{
					model.AddNewUser = int.Parse(ds.Tables[0].Rows[0]["AddNewUser"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CardMoney"] != null && ds.Tables[0].Rows[0]["CardMoney"].ToString() != "")
				{
					model.CardMoney = decimal.Parse(ds.Tables[0].Rows[0]["CardMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExpenseSumMoneys"] != null && ds.Tables[0].Rows[0]["ExpenseSumMoneys"].ToString() != "")
				{
					model.ExpenseSumMoneys = decimal.Parse(ds.Tables[0].Rows[0]["ExpenseSumMoneys"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExpenseBinkMoneys"] != null && ds.Tables[0].Rows[0]["ExpenseBinkMoneys"].ToString() != "")
				{
					model.ExpenseBinkMoneys = decimal.Parse(ds.Tables[0].Rows[0]["ExpenseBinkMoneys"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExpenseCouponMoneys"] != null && ds.Tables[0].Rows[0]["ExpenseCouponMoneys"].ToString() != "")
				{
					model.ExpenseCouponMoneys = decimal.Parse(ds.Tables[0].Rows[0]["ExpenseCouponMoneys"].ToString());
				}
				if (ds.Tables[0].Rows[0]["SRechargeMoney"] != null && ds.Tables[0].Rows[0]["SRechargeMoney"].ToString() != "")
				{
					model.SRechargeMoney = decimal.Parse(ds.Tables[0].Rows[0]["SRechargeMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FRechargeMoney"] != null && ds.Tables[0].Rows[0]["FRechargeMoney"].ToString() != "")
				{
					model.FRechargeMoney = decimal.Parse(ds.Tables[0].Rows[0]["FRechargeMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RechargeBank"] != null && ds.Tables[0].Rows[0]["RechargeBank"].ToString() != "")
				{
					model.RechargeBank = decimal.Parse(ds.Tables[0].Rows[0]["RechargeBank"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FRechargeGiveMoney"] != null && ds.Tables[0].Rows[0]["FRechargeGiveMoney"].ToString() != "")
				{
					model.FRechargeGiveMoney = decimal.Parse(ds.Tables[0].Rows[0]["FRechargeGiveMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AllMoneys"] != null && ds.Tables[0].Rows[0]["AllMoneys"].ToString() != "")
				{
					model.AllMoneys = decimal.Parse(ds.Tables[0].Rows[0]["AllMoneys"].ToString());
				}
				if (ds.Tables[0].Rows[0]["sjMoneys"] != null && ds.Tables[0].Rows[0]["sjMoneys"].ToString() != "")
				{
					model.sjMoneys = decimal.Parse(ds.Tables[0].Rows[0]["sjMoneys"].ToString());
				}
				if (ds.Tables[0].Rows[0]["HandoverUserID"] != null && ds.Tables[0].Rows[0]["HandoverUserID"].ToString() != "")
				{
					model.HandoverUserID = int.Parse(ds.Tables[0].Rows[0]["HandoverUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Arrearage"] != null && ds.Tables[0].Rows[0]["Arrearage"].ToString() != "")
				{
					model.Arrearage = decimal.Parse(ds.Tables[0].Rows[0]["Arrearage"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Ispay"] != null && ds.Tables[0].Rows[0]["Ispay"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["Ispay"].ToString() == "1" || ds.Tables[0].Rows[0]["Ispay"].ToString().ToLower() == "true")
					{
						model.Ispay = true;
					}
					else
					{
						model.Ispay = false;
					}
				}
				if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
				{
					model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
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
			strSql.Append("select SysUserWorkID,UserID,StartTime,EedTime,AddNewUser,CardMoney,ExpenseSumMoneys,ExpenseBinkMoneys,ExpenseCouponMoneys,SRechargeMoney,FRechargeMoney,RechargeBank,FRechargeGiveMoney,AllMoneys,sjMoneys,HandoverUserID,Arrearage,Ispay,remark ");
			strSql.Append(" FROM SysUserWork ");
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
			strSql.Append(" SysUserWorkID,UserID,StartTime,EedTime,AddNewUser,CardMoney,ExpenseSumMoneys,ExpenseBinkMoneys,ExpenseCouponMoneys,SRechargeMoney,FRechargeMoney,RechargeBank,FRechargeGiveMoney,AllMoneys,sjMoneys,HandoverUserID,Arrearage,Ispay,remark ");
			strSql.Append(" FROM SysUserWork ");
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
			strSql.Append("select count(1) FROM SysUserWork ");
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
				strSql.Append("order by T.SysUserWorkID desc");
			}
			strSql.Append(")AS Row, T.*  from SysUserWork T ");
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
			string tableName = "SysUserWork,SysUser,SysShop";
			string[] columns = new string[]
			{
				"SysUser.UserName,SysUser.UserNumber,SysUser.UserShopID,SysShop.ShopName,SysUserWork.*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "EedTime", "SysUserWorkID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
