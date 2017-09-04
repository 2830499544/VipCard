using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class StaffMoney
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("StaffMoneyID", "StaffMoney");
		}

		public bool Exists(int StaffMoneyID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from StaffMoney");
			strSql.Append(" where StaffMoneyID=@StaffMoneyID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StaffMoneyID", SqlDbType.Int, 4)
			};
			parameters[0].Value = StaffMoneyID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.StaffMoney model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into StaffMoney(");
			strSql.Append("StaffID,StaffTotalMoney,StaffOrderCode,StaffMemID,StaffGoodsID,StaffShopID,StaffCreateTime,StaffOrderDetailID,StaffType)");
			strSql.Append(" values (");
			strSql.Append("@StaffID,@StaffTotalMoney,@StaffOrderCode,@StaffMemID,@StaffGoodsID,@StaffShopID,@StaffCreateTime,@StaffOrderDetailID,@StaffType)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StaffID", SqlDbType.Int, 4),
				new SqlParameter("@StaffTotalMoney", SqlDbType.Money, 8),
				new SqlParameter("@StaffOrderCode", SqlDbType.VarChar, 50),
				new SqlParameter("@StaffMemID", SqlDbType.Int, 4),
				new SqlParameter("@StaffGoodsID", SqlDbType.Int, 4),
				new SqlParameter("@StaffShopID", SqlDbType.Int, 4),
				new SqlParameter("@StaffCreateTime", SqlDbType.DateTime),
				new SqlParameter("@StaffOrderDetailID", SqlDbType.Int, 4),
				new SqlParameter("@StaffType", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.StaffID;
			parameters[1].Value = model.StaffTotalMoney;
			parameters[2].Value = model.StaffOrderCode;
			parameters[3].Value = model.StaffMemID;
			parameters[4].Value = model.StaffGoodsID;
			parameters[5].Value = model.StaffShopID;
			parameters[6].Value = model.StaffCreateTime;
			parameters[7].Value = model.StaffOrderDetailID;
			parameters[8].Value = model.StaffType;
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

		public bool Update(Chain.Model.StaffMoney model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update StaffMoney set ");
			strSql.Append("StaffID=@StaffID,");
			strSql.Append("StaffTotalMoney=@StaffTotalMoney,");
			strSql.Append("StaffOrderCode=@StaffOrderCode,");
			strSql.Append("StaffMemID=@StaffMemID,");
			strSql.Append("StaffGoodsID=@StaffGoodsID,");
			strSql.Append("StaffShopID=@StaffShopID,");
			strSql.Append("StaffCreateTime=@StaffCreateTime,");
			strSql.Append("StaffOrderDetailID=@StaffOrderDetailID,");
			strSql.Append("StaffType=@StaffType");
			strSql.Append(" where StaffMoneyID=@StaffMoneyID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StaffID", SqlDbType.Int, 4),
				new SqlParameter("@StaffTotalMoney", SqlDbType.Money, 8),
				new SqlParameter("@StaffOrderCode", SqlDbType.VarChar, 50),
				new SqlParameter("@StaffMemID", SqlDbType.Int, 4),
				new SqlParameter("@StaffGoodsID", SqlDbType.Int, 4),
				new SqlParameter("@StaffShopID", SqlDbType.Int, 4),
				new SqlParameter("@StaffCreateTime", SqlDbType.DateTime),
				new SqlParameter("@StaffOrderDetailID", SqlDbType.Int, 4),
				new SqlParameter("@StaffType", SqlDbType.Int, 4),
				new SqlParameter("@StaffMoneyID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.StaffID;
			parameters[1].Value = model.StaffTotalMoney;
			parameters[2].Value = model.StaffOrderCode;
			parameters[3].Value = model.StaffMemID;
			parameters[4].Value = model.StaffGoodsID;
			parameters[5].Value = model.StaffShopID;
			parameters[6].Value = model.StaffCreateTime;
			parameters[7].Value = model.StaffOrderDetailID;
			parameters[8].Value = model.StaffType;
			parameters[9].Value = model.StaffMoneyID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int StaffMoneyID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from StaffMoney ");
			strSql.Append(" where StaffMoneyID=@StaffMoneyID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StaffMoneyID", SqlDbType.Int, 4)
			};
			parameters[0].Value = StaffMoneyID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string StaffMoneyIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from StaffMoney ");
			strSql.Append(" where StaffMoneyID in (" + StaffMoneyIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.StaffMoney GetModel(int StaffMoneyID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 StaffMoneyID,StaffID,StaffTotalMoney,StaffOrderCode,StaffMemID,StaffGoodsID,StaffShopID,StaffCreateTime,StaffOrderDetailID,StaffType from StaffMoney ");
			strSql.Append(" where StaffMoneyID=@StaffMoneyID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StaffMoneyID", SqlDbType.Int, 4)
			};
			parameters[0].Value = StaffMoneyID;
			Chain.Model.StaffMoney model = new Chain.Model.StaffMoney();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.StaffMoney result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["StaffMoneyID"] != null && ds.Tables[0].Rows[0]["StaffMoneyID"].ToString() != "")
				{
					model.StaffMoneyID = int.Parse(ds.Tables[0].Rows[0]["StaffMoneyID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StaffID"] != null && ds.Tables[0].Rows[0]["StaffID"].ToString() != "")
				{
					model.StaffID = int.Parse(ds.Tables[0].Rows[0]["StaffID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StaffTotalMoney"] != null && ds.Tables[0].Rows[0]["StaffTotalMoney"].ToString() != "")
				{
					model.StaffTotalMoney = decimal.Parse(ds.Tables[0].Rows[0]["StaffTotalMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StaffOrderCode"] != null && ds.Tables[0].Rows[0]["StaffOrderCode"].ToString() != "")
				{
					model.StaffOrderCode = ds.Tables[0].Rows[0]["StaffOrderCode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["StaffMemID"] != null && ds.Tables[0].Rows[0]["StaffMemID"].ToString() != "")
				{
					model.StaffMemID = int.Parse(ds.Tables[0].Rows[0]["StaffMemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StaffGoodsID"] != null && ds.Tables[0].Rows[0]["StaffGoodsID"].ToString() != "")
				{
					model.StaffGoodsID = int.Parse(ds.Tables[0].Rows[0]["StaffGoodsID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StaffShopID"] != null && ds.Tables[0].Rows[0]["StaffShopID"].ToString() != "")
				{
					model.StaffShopID = int.Parse(ds.Tables[0].Rows[0]["StaffShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StaffCreateTime"] != null && ds.Tables[0].Rows[0]["StaffCreateTime"].ToString() != "")
				{
					model.StaffCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["StaffCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StaffOrderDetailID"] != null && ds.Tables[0].Rows[0]["StaffOrderDetailID"].ToString() != "")
				{
					model.StaffOrderDetailID = int.Parse(ds.Tables[0].Rows[0]["StaffOrderDetailID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StaffType"] != null && ds.Tables[0].Rows[0]["StaffType"].ToString() != "")
				{
					model.StaffType = int.Parse(ds.Tables[0].Rows[0]["StaffType"].ToString());
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
			strSql.Append("select StaffMoneyID,StaffID,StaffTotalMoney,StaffOrderCode,StaffMemID,StaffGoodsID,StaffShopID,StaffCreateTime,StaffOrderDetailID,StaffType ");
			strSql.Append(" FROM StaffMoney ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetStaffMoneyByTime(DateTime starttime, DateTime endtime, string strwhere)
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
			return DbHelperSQL.RunProcedure("MonthStaffTotalMoney", parameters, "#StaffTotalMoneyData");
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ");
			if (Top > 0)
			{
				strSql.Append(" top " + Top.ToString());
			}
			strSql.Append(" StaffMoneyID,StaffID,StaffTotalMoney,StaffOrderCode,StaffMemID,StaffGoodsID,StaffShopID,StaffCreateTime,StaffOrderDetailID,StaffType ");
			strSql.Append(" FROM StaffMoney ");
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
			strSql.Append("select count(1) FROM StaffMoney ");
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
				strSql.Append("order by T.StaffMoneyID desc");
			}
			strSql.Append(")AS Row, T.*  from StaffMoney T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public decimal GetTotalStaffMoney(string strWhere)
		{
			StringBuilder StrSql = new StringBuilder();
			StrSql.Append(" select sum(TotalMoney) from V_StaffMoney ");
			if (strWhere.Trim() != "")
			{
				StrSql.Append(" where " + strWhere);
			}
			object obj = DbHelperSQL.GetSingle(StrSql.ToString());
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

		public int UpdateStaffMoney(string strOrderCode, int intGoodsID, decimal money)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" update StaffMoney set StaffTotalMoney =@StaffTotalMoney ");
			strSql.Append(" where StaffOrderCode =@StaffOrderCode and StaffGoodsID=@StaffGoodsID");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@StaffTotalMoney", SqlDbType.Money, 8),
				new SqlParameter("@StaffOrderCode", SqlDbType.VarChar, 50),
				new SqlParameter("@StaffGoodsID", SqlDbType.Int, 4)
			};
			parameter[0].Value = money;
			parameter[1].Value = strOrderCode;
			parameter[2].Value = intGoodsID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}

		public DataSet GetListSP(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select * from V_StaffMoney");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" Order by StaffOrderDetailID asc");
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int DeleteStaff(string strStaffOrderCode)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" delete from StaffMoney where StaffOrderCode=@StaffOrderCode");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@StaffOrderCode", SqlDbType.VarChar, 50)
			};
			parameter[0].Value = strStaffOrderCode;
			object obj = DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
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

		public DataSet GetStaffMoney(string strWhere)
		{
			string strSql = " select * from V_GoodsReturn ";
			if (strWhere != "")
			{
				strSql = strSql + " where " + strWhere;
			}
			return DbHelperSQL.Query(strSql);
		}

		public decimal GetStaffTotalMoney(string strSql)
		{
			StringBuilder strWhere = new StringBuilder();
			strWhere.Append(" SELECT SUM(ISNULL(StaffTotalMoney,0)) FROM StaffMoney,StaffClass,Staff,SysShop ");
			if (strSql.Trim() != "")
			{
				strWhere.Append(" where " + strSql);
			}
			object obj = DbHelperSQL.GetSingle(strWhere.ToString());
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
	}
}
