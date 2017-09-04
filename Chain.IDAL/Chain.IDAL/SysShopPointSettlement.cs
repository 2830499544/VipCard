using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysShopPointSettlement
	{
		public bool Exists(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysShopPointSettlement");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysShopPointSettlement model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysShopPointSettlement(");
			strSql.Append("StartTime,EndTime,RechargePoint,DeductionPoint,GivePoint,ReturnPoint,FanliPoint,ReturnOrderPoint,DrawPoint,IsFinish,FinishTime,Remark,OutShopID,UserID)");
			strSql.Append(" values (");
			strSql.Append("@StartTime,@EndTime,@RechargePoint,@DeductionPoint,@GivePoint,@ReturnPoint,@FanliPoint,@ReturnOrderPoint,@DrawPoint,,@IsFinish,@FinishTime,@Remark,@OutShopID,@UserID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EndTime", SqlDbType.DateTime),
				new SqlParameter("@RechargePoint", SqlDbType.Int, 4),
				new SqlParameter("@DeductionPoint", SqlDbType.Int, 4),
				new SqlParameter("@GivePoint", SqlDbType.Int, 4),
				new SqlParameter("@ReturnPoint", SqlDbType.Int, 4),
				new SqlParameter("@FanliPoint", SqlDbType.Int, 4),
				new SqlParameter("@ReturnOrderPoint", SqlDbType.Int, 4),
				new SqlParameter("@DrawPoint", SqlDbType.Int, 4),
				new SqlParameter("@IsFinish", SqlDbType.Bit, 1),
				new SqlParameter("@FinishTime", SqlDbType.DateTime),
				new SqlParameter("@Remark", SqlDbType.NVarChar, 400),
				new SqlParameter("@OutShopID", SqlDbType.Int, 4),
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@AllExpenseMoney", SqlDbType.Decimal),
				new SqlParameter("@ProportionMoney", SqlDbType.Decimal),
				new SqlParameter("@Proportion", SqlDbType.Decimal)
			};
			parameters[0].Value = model.StartTime;
			parameters[1].Value = model.EndTime;
			parameters[2].Value = model.RechargePoint;
			parameters[3].Value = model.DeductionPoint;
			parameters[4].Value = model.GivePoint;
			parameters[5].Value = model.ReturnPoint;
			parameters[6].Value = model.FanliPoint;
			parameters[7].Value = model.ReturnOrderPoint;
			parameters[8].Value = model.DrawPoint;
			parameters[9].Value = model.IsFinish;
			parameters[10].Value = model.FinishTime;
			parameters[11].Value = model.Remark;
			parameters[12].Value = model.OutShopID;
			parameters[13].Value = model.UserID;
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

		public bool Update(Chain.Model.SysShopPointSettlement model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysShopPointSettlement set ");
			strSql.Append("IsFinish=@IsFinish,");
			strSql.Append("FinishTime=@FinishTime,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IsFinish", SqlDbType.Bit, 1),
				new SqlParameter("@FinishTime", SqlDbType.DateTime),
				new SqlParameter("@Remark", SqlDbType.NVarChar, 400),
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.IsFinish;
			parameters[1].Value = model.FinishTime;
			parameters[2].Value = model.Remark;
			parameters[3].Value = model.ID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public void UpDateSettlement()
		{
			SqlParameter[] parameters = new SqlParameter[0];
			DbHelperSQL.RunProcedure("CP_ShopPointSettlement", parameters);
		}

		public bool Delete(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysShopPointSettlement ");
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
			strSql.Append("delete from SysShopPointSettlement ");
			strSql.Append(" where ID in (" + IDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysShopPointSettlement GetModel(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ID,StartTime,EndTime,RechargePoint,DeductionPoint,GivePoint,ReturnPoint,FanliPoint,ReturnOrderPoint,DrawPoint,IsFinish,FinishTime,Remark,OutShopID,UserID from SysShopPointSettlement ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			Chain.Model.SysShopPointSettlement model = new Chain.Model.SysShopPointSettlement();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysShopPointSettlement result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
				{
					model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StartTime"] != null && ds.Tables[0].Rows[0]["StartTime"].ToString() != "")
				{
					model.StartTime = DateTime.Parse(ds.Tables[0].Rows[0]["StartTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["EndTime"] != null && ds.Tables[0].Rows[0]["EndTime"].ToString() != "")
				{
					model.EndTime = DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RechargePoint"] != null && ds.Tables[0].Rows[0]["RechargePoint"].ToString() != "")
				{
					model.RechargePoint = int.Parse(ds.Tables[0].Rows[0]["RechargePoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DeductionPoint"] != null && ds.Tables[0].Rows[0]["DeductionPoint"].ToString() != "")
				{
					model.DeductionPoint = int.Parse(ds.Tables[0].Rows[0]["DeductionPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GivePoint"] != null && ds.Tables[0].Rows[0]["GivePoint"].ToString() != "")
				{
					model.GivePoint = int.Parse(ds.Tables[0].Rows[0]["GivePoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ReturnPoint"] != null && ds.Tables[0].Rows[0]["ReturnPoint"].ToString() != "")
				{
					model.ReturnPoint = int.Parse(ds.Tables[0].Rows[0]["ReturnPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["FanliPoint"] != null && ds.Tables[0].Rows[0]["FanliPoint"].ToString() != "")
				{
					model.FanliPoint = int.Parse(ds.Tables[0].Rows[0]["FanliPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ReturnOrderPoint"] != null && ds.Tables[0].Rows[0]["ReturnOrderPoint"].ToString() != "")
				{
					model.ReturnOrderPoint = int.Parse(ds.Tables[0].Rows[0]["ReturnOrderPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DrawPoint"] != null && ds.Tables[0].Rows[0]["DrawPoint"].ToString() != "")
				{
					model.DrawPoint = int.Parse(ds.Tables[0].Rows[0]["DrawPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["IsFinish"] != null && ds.Tables[0].Rows[0]["IsFinish"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsFinish"].ToString() == "1" || ds.Tables[0].Rows[0]["IsFinish"].ToString().ToLower() == "true")
					{
						model.IsFinish = true;
					}
					else
					{
						model.IsFinish = false;
					}
				}
				if (ds.Tables[0].Rows[0]["FinishTime"] != null && ds.Tables[0].Rows[0]["FinishTime"].ToString() != "")
				{
					model.FinishTime = DateTime.Parse(ds.Tables[0].Rows[0]["FinishTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
				{
					model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["OutShopID"] != null && ds.Tables[0].Rows[0]["OutShopID"].ToString() != "")
				{
					model.OutShopID = int.Parse(ds.Tables[0].Rows[0]["OutShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
				{
					model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
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
			strSql.Append("select ID,StartTime,EndTime,RechargePoint,DeductionPoint,GivePoint,ReturnPoint,FanliPoint,ReturnOrderPoint,DrawPoint,IsFinish,FinishTime,Remark,OutShopID,UserID ");
			strSql.Append(" FROM SysShopPointSettlement ");
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
			strSql.Append(" ID,StartTime,EndTime,RechargeMoney,DrawMoney,PayCard,IsFinish,FinishTime,Remark,OutShopID,UserID,AllExpenseMoney,ProportionMoney,Proportion ");
			strSql.Append(" FROM SysShopPointSettlement ");
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
			strSql.Append("select count(1) FROM SysShopPointSettlement ");
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
			strSql.Append(")AS Row, T.*  from SysShopPointSettlement T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, string join, out int resCount, params string[] strWhere)
		{
			string tableName = " SysShopPointSettlement left join SysShop  " + join;
			string[] columns = new string[]
			{
				"SysShopPointSettlement.*,SysShop.ShopName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "OutShopID,EndTime", "ID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
