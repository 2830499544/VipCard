using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysShopSettlement
	{
		public bool Exists(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysShopSettlement");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public string GetEndTime(int OutShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select top 1 EndTime from SysShopSettlement  ");
			strSql.Append(" where OutShopID=@OutShopID order by EndTime desc");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OutShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = OutShopID;
			object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
			string result;
			if (obj != null)
			{
				result = obj.ToString();
			}
			else
			{
				result = "";
			}
			return result;
		}

		public int Add(Chain.Model.SysShopSettlement model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysShopSettlement(");
			strSql.Append("StartTime,EndTime,RechargeMoney,DrawMoney,PayCard,IsFinish,FinishTime,Remark,OutShopID,UserID,AllExpenseMoney,ProportionMoney,Proportion)");
			strSql.Append(" values (");
			strSql.Append("@StartTime,@EndTime,@RechargeMoney,@DrawMoney,@PayCard,@IsFinish,@FinishTime,@Remark,@OutShopID,@UserID,@AllExpenseMoney,@ProportionMoney,@Proportion)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EndTime", SqlDbType.DateTime),
				new SqlParameter("@RechargeMoney", SqlDbType.Money, 8),
				new SqlParameter("@DrawMoney", SqlDbType.Money, 8),
				new SqlParameter("@PayCard", SqlDbType.Money, 8),
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
			parameters[2].Value = model.RechargeMoney;
			parameters[3].Value = model.DrawMoney;
			parameters[4].Value = model.PayCard;
			parameters[5].Value = model.IsFinish;
			parameters[6].Value = model.FinishTime;
			parameters[7].Value = model.Remark;
			parameters[8].Value = model.OutShopID;
			parameters[9].Value = model.UserID;
			parameters[10].Value = model.AllExpenseMoney;
			parameters[11].Value = model.ProportionMoney;
			parameters[12].Value = model.Proportion;
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

		public bool Update(Chain.Model.SysShopSettlement model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysShopSettlement set ");
			strSql.Append("StartTime=@StartTime,");
			strSql.Append("EndTime=@EndTime,");
			strSql.Append("RechargeMoney=@RechargeMoney,");
			strSql.Append("DrawMoney=@DrawMoney,");
			strSql.Append("PayCard=@PayCard,");
			strSql.Append("IsFinish=@IsFinish,");
			strSql.Append("FinishTime=@FinishTime,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("OutShopID=@OutShopID,");
			strSql.Append("UserID=@UserID,");
			strSql.Append("AllExpenseMoney=@AllExpenseMoney,");
			strSql.Append("ProportionMoney=@ProportionMoney,");
			strSql.Append("Proportion=@Proportion");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EndTime", SqlDbType.DateTime),
				new SqlParameter("@RechargeMoney", SqlDbType.Money, 8),
				new SqlParameter("@DrawMoney", SqlDbType.Money, 8),
				new SqlParameter("@PayCard", SqlDbType.Money, 8),
				new SqlParameter("@IsFinish", SqlDbType.Bit, 1),
				new SqlParameter("@FinishTime", SqlDbType.DateTime),
				new SqlParameter("@Remark", SqlDbType.NVarChar, 400),
				new SqlParameter("@OutShopID", SqlDbType.Int, 4),
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@ID", SqlDbType.Int, 4),
				new SqlParameter("@AllExpenseMoney", SqlDbType.Decimal),
				new SqlParameter("@ProportionMoney", SqlDbType.Decimal),
				new SqlParameter("@Proportion", SqlDbType.Decimal)
			};
			parameters[0].Value = model.StartTime;
			parameters[1].Value = model.EndTime;
			parameters[2].Value = model.RechargeMoney;
			parameters[3].Value = model.DrawMoney;
			parameters[4].Value = model.PayCard;
			parameters[5].Value = model.IsFinish;
			parameters[6].Value = model.FinishTime;
			parameters[7].Value = model.Remark;
			parameters[8].Value = model.OutShopID;
			parameters[9].Value = model.UserID;
			parameters[10].Value = model.ID;
			parameters[11].Value = model.AllExpenseMoney;
			parameters[12].Value = model.ProportionMoney;
			parameters[13].Value = model.Proportion;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public void UpDateSettlement()
		{
			SqlParameter[] parameters = new SqlParameter[0];
			DbHelperSQL.RunProcedure("CP_ShopSettlement", parameters);
		}

		public bool Delete(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysShopSettlement ");
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
			strSql.Append("delete from SysShopSettlement ");
			strSql.Append(" where ID in (" + IDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysShopSettlement GetModel(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ID,StartTime,EndTime,RechargeMoney,DrawMoney,PayCard,IsFinish,FinishTime,Remark,OutShopID,UserID,AllExpenseMoney,ProportionMoney,Proportion from SysShopSettlement ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			Chain.Model.SysShopSettlement model = new Chain.Model.SysShopSettlement();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysShopSettlement result;
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
				if (ds.Tables[0].Rows[0]["RechargeMoney"] != null && ds.Tables[0].Rows[0]["RechargeMoney"].ToString() != "")
				{
					model.RechargeMoney = decimal.Parse(ds.Tables[0].Rows[0]["RechargeMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DrawMoney"] != null && ds.Tables[0].Rows[0]["DrawMoney"].ToString() != "")
				{
					model.DrawMoney = decimal.Parse(ds.Tables[0].Rows[0]["DrawMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PayCard"] != null && ds.Tables[0].Rows[0]["PayCard"].ToString() != "")
				{
					model.PayCard = decimal.Parse(ds.Tables[0].Rows[0]["PayCard"].ToString());
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
				if (ds.Tables[0].Rows[0]["AllExpenseMoney"] != null && ds.Tables[0].Rows[0]["AllExpenseMoney"].ToString() != "")
				{
					model.AllExpenseMoney = Convert.ToDecimal(ds.Tables[0].Rows[0]["AllExpenseMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ProportionMoney"] != null && ds.Tables[0].Rows[0]["ProportionMoney"].ToString() != "")
				{
					model.ProportionMoney = Convert.ToDecimal(ds.Tables[0].Rows[0]["ProportionMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Proportion"] != null && ds.Tables[0].Rows[0]["Proportion"].ToString() != "")
				{
					model.Proportion = Convert.ToDecimal(ds.Tables[0].Rows[0]["Proportion"].ToString());
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
			strSql.Append("select RechargeProportion,RhgPropMoney,ID,StartTime,EndTime,RechargeMoney,DrawMoney,PointDrawMoney,PointAmount,PayCard,IsFinish,FinishTime,Remark,OutShopID,UserID,AllExpenseMoney,ProportionMoney,Proportion ");
			strSql.Append(" FROM SysShopSettlement ");
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
			strSql.Append(" FROM SysShopSettlement ");
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
			strSql.Append("select count(1) FROM SysShopSettlement ");
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
			strSql.Append(")AS Row, T.*  from SysShopSettlement T ");
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
			string tableName = " SysShopSettlement left join SysShop  " + join;
			string[] columns = new string[]
			{
				"SysShopSettlement.*,SysShop.ShopName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "OutShopID,EndTime", "ID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
