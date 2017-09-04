using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class OrderLog
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("OrderID", "OrderLog");
		}

		public bool Exists(int OrderID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from OrderLog");
			strSql.Append(" where OrderID=@OrderID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderID", SqlDbType.Int, 4)
			};
			parameters[0].Value = OrderID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool ExistsOrderAccount(string OrderAccount)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from OrderLog");
			strSql.Append(" where OrderAccount=@OrderAccount");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderAccount", SqlDbType.VarChar, 50)
			};
			parameters[0].Value = OrderAccount;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.OrderLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into OrderLog(");
			strSql.Append("UsePoint,UsePointAmount,OrderAccount,OrderType,OrderMemID,OrderTotalMoney,OrderDiscountMoney,OrderPayCard,OrderPayCash,OrderPayCoupon,OrderPoint,OrderRemark,OrderPayType,OrderShopID,OrderCreateTime,OrderIsCard,OrderIsCash,OrderIsBink,OrderPayBink,OrderUserID,OldAccount,OrderCardBalance,OrderCardPoint)");
			strSql.Append(" values (");
			strSql.Append("@UsePoint,@UsePointAmount,@OrderAccount,@OrderType,@OrderMemID,@OrderTotalMoney,@OrderDiscountMoney,@OrderPayCard,@OrderPayCash,@OrderPayCoupon,@OrderPoint,@OrderRemark,@OrderPayType,@OrderShopID,@OrderCreateTime,@OrderIsCard,@OrderIsCash,@OrderIsBink,@OrderPayBink,@OrderUserID,@OldAccount,@OrderCardBalance,@OrderCardPoint)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@OrderType", SqlDbType.TinyInt, 1),
				new SqlParameter("@OrderMemID", SqlDbType.Int, 4),
				new SqlParameter("@OrderTotalMoney", SqlDbType.Money, 8),
				new SqlParameter("@OrderDiscountMoney", SqlDbType.Money, 8),
				new SqlParameter("@OrderPayCard", SqlDbType.Money, 8),
				new SqlParameter("@OrderPayCash", SqlDbType.Money, 8),
				new SqlParameter("@OrderPayCoupon", SqlDbType.Money, 8),
				new SqlParameter("@OrderPoint", SqlDbType.Int, 4),
				new SqlParameter("@OrderRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@OrderPayType", SqlDbType.TinyInt, 1),
				new SqlParameter("@OrderShopID", SqlDbType.Int, 4),
				new SqlParameter("@OrderCreateTime", SqlDbType.DateTime),
				new SqlParameter("@OrderIsCard", SqlDbType.Bit),
				new SqlParameter("@OrderIsCash", SqlDbType.Bit),
				new SqlParameter("@OrderIsBink", SqlDbType.Bit),
				new SqlParameter("@OrderPayBink", SqlDbType.Money, 8),
				new SqlParameter("@OrderUserID", SqlDbType.Int, 4),
				new SqlParameter("@OldAccount", SqlDbType.NVarChar, 50),
				new SqlParameter("@OrderCardBalance", SqlDbType.Money, 8),
				new SqlParameter("@OrderCardPoint", SqlDbType.Int, 4),
				new SqlParameter("@UsePoint", SqlDbType.Int, 4),
				new SqlParameter("@UsePointAmount", SqlDbType.Money, 8)
			};
			parameters[0].Value = model.OrderAccount;
			parameters[1].Value = model.OrderType;
			parameters[2].Value = model.OrderMemID;
			parameters[3].Value = model.OrderTotalMoney;
			parameters[4].Value = model.OrderDiscountMoney;
			parameters[5].Value = model.OrderPayCard;
			parameters[6].Value = model.OrderPayCash;
			parameters[7].Value = model.OrderPayCoupon;
			parameters[8].Value = model.OrderPoint;
			parameters[9].Value = model.OrderRemark;
			parameters[10].Value = model.OrderPayType;
			parameters[11].Value = model.OrderShopID;
			parameters[12].Value = model.OrderCreateTime;
			parameters[13].Value = model.OrderIsCard;
			parameters[14].Value = model.OrderIsCash;
			parameters[15].Value = model.OrderIsBink;
			parameters[16].Value = model.OrderPayBink;
			parameters[17].Value = model.OrderUserID;
			parameters[18].Value = model.OldAccount;
			parameters[19].Value = model.OrderCardBalance;
			parameters[20].Value = model.OrderCardPoint;
			parameters[21].Value = model.UsePoint;
			parameters[22].Value = model.UsePointAmount;
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

		public int Update(Chain.Model.OrderLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update OrderLog set ");
			strSql.Append("OrderAccount=@OrderAccount,");
			strSql.Append("OrderType=@OrderType,");
			strSql.Append("OrderMemID=@OrderMemID,");
			strSql.Append("OrderTotalMoney=@OrderTotalMoney,");
			strSql.Append("OrderDiscountMoney=@OrderDiscountMoney,");
			strSql.Append("OrderPayCard=@OrderPayCard,");
			strSql.Append("OrderPayCash=@OrderPayCash,");
			strSql.Append("OrderPoint=@OrderPoint,");
			strSql.Append("OrderRemark=@OrderRemark,");
			strSql.Append("OrderPayType=@OrderPayType,");
			strSql.Append("OrderShopID=@OrderShopID,");
			strSql.Append("OrderCreateTime=@OrderCreateTime,");
			strSql.Append("OrderUserID=@OrderUserID,");
			strSql.Append("OrderIsCard=@OrderIsCard,");
			strSql.Append("OrderIsCash=@OrderIsCash,");
			strSql.Append("OrderIsBink=@OrderIsBink,");
			strSql.Append("OrderPayBink=@OrderPayBink,");
			strSql.Append("OldAccount=@OldAccount,");
			strSql.Append("OrderCardBalance=@OrderCardBalance,");
			strSql.Append("OrderCardPoint=@OrderCardPoint,");
			strSql.Append("UsePoint=@UsePoint,");
			strSql.Append("UsePointAmount=@UsePointAmount");
			strSql.Append(" where OrderID=@OrderID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@OrderType", SqlDbType.TinyInt, 1),
				new SqlParameter("@OrderMemID", SqlDbType.Int, 4),
				new SqlParameter("@OrderTotalMoney", SqlDbType.Money, 8),
				new SqlParameter("@OrderDiscountMoney", SqlDbType.Money, 8),
				new SqlParameter("@OrderPayCard", SqlDbType.Money, 8),
				new SqlParameter("@OrderPayCash", SqlDbType.Money, 8),
				new SqlParameter("@OrderPoint", SqlDbType.Int, 4),
				new SqlParameter("@OrderRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@OrderPayType", SqlDbType.TinyInt, 1),
				new SqlParameter("@OrderShopID", SqlDbType.Int, 4),
				new SqlParameter("@OrderCreateTime", SqlDbType.DateTime),
				new SqlParameter("@OrderUserID", SqlDbType.Int, 4),
				new SqlParameter("@OrderIsCard", SqlDbType.Bit),
				new SqlParameter("@OrderIsCash", SqlDbType.Bit),
				new SqlParameter("@OrderIsBink", SqlDbType.Bit),
				new SqlParameter("@OrderPayBink", SqlDbType.Money, 8),
				new SqlParameter("@OldAccount", SqlDbType.NVarChar, 50),
				new SqlParameter("@OrderCardBalance", SqlDbType.Money, 8),
				new SqlParameter("@OrderCardPoint", SqlDbType.Int, 4),
				new SqlParameter("@OrderID", SqlDbType.Int, 4),
				new SqlParameter("@UsePoint", SqlDbType.Int, 4),
				new SqlParameter("@UsePointAmount", SqlDbType.Money, 8)
			};
			parameters[0].Value = model.OrderAccount;
			parameters[1].Value = model.OrderType;
			parameters[2].Value = model.OrderMemID;
			parameters[3].Value = model.OrderTotalMoney;
			parameters[4].Value = model.OrderDiscountMoney;
			parameters[5].Value = model.OrderPayCard;
			parameters[6].Value = model.OrderPayCash;
			parameters[7].Value = model.OrderPoint;
			parameters[8].Value = model.OrderRemark;
			parameters[9].Value = model.OrderPayType;
			parameters[10].Value = model.OrderShopID;
			parameters[11].Value = model.OrderCreateTime;
			parameters[12].Value = model.OrderUserID;
			parameters[13].Value = model.OrderIsCard;
			parameters[14].Value = model.OrderIsCash;
			parameters[15].Value = model.OrderIsBink;
			parameters[16].Value = model.OrderPayBink;
			parameters[17].Value = model.OldAccount;
			parameters[18].Value = model.OrderCardBalance;
			parameters[19].Value = model.OrderCardPoint;
			parameters[20].Value = model.OrderID;
			parameters[21].Value = model.UsePoint;
			parameters[22].Value = model.UsePointAmount;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			int result;
			if (rows > 0)
			{
				result = rows;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public int UpdateType(int orderID, int type)
		{
			string strSql = string.Concat(new object[]
			{
				" update OrderLog set OrderType=",
				type,
				" where OrderID=",
				orderID
			});
			return DbHelperSQL.ExecuteSql(strSql);
		}

		public bool Delete(int OrderID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from OrderLog ");
			strSql.Append(" where OrderID=@OrderID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderID", SqlDbType.Int, 4)
			};
			parameters[0].Value = OrderID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string OrderIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from OrderLog ");
			strSql.Append(" where OrderID in (" + OrderIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.OrderLog GetModel(int OrderID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 OrderID,OrderAccount,OrderType,OrderMemID,OrderTotalMoney,OrderDiscountMoney,OrderPayCard,OrderPayCash,OrderPayCoupon,OrderPoint,OrderRemark,OrderPayType,OrderShopID,OrderCreateTime,OrderUserID,OrderIsCard,OrderIsCash,OrderIsBink,OrderPayBink,OldAccount,OrderCardBalance,OrderCardPoint from OrderLog ");
			strSql.Append(" where OrderID=@OrderID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderID", SqlDbType.Int, 4)
			};
			parameters[0].Value = OrderID;
			Chain.Model.OrderLog model = new Chain.Model.OrderLog();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.OrderLog result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["OrderID"] != null && ds.Tables[0].Rows[0]["OrderID"].ToString() != "")
				{
					model.OrderID = int.Parse(ds.Tables[0].Rows[0]["OrderID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderAccount"] != null && ds.Tables[0].Rows[0]["OrderAccount"].ToString() != "")
				{
					model.OrderAccount = ds.Tables[0].Rows[0]["OrderAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["OrderType"] != null && ds.Tables[0].Rows[0]["OrderType"].ToString() != "")
				{
					model.OrderType = int.Parse(ds.Tables[0].Rows[0]["OrderType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderMemID"] != null && ds.Tables[0].Rows[0]["OrderMemID"].ToString() != "")
				{
					model.OrderMemID = int.Parse(ds.Tables[0].Rows[0]["OrderMemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderTotalMoney"] != null && ds.Tables[0].Rows[0]["OrderTotalMoney"].ToString() != "")
				{
					model.OrderTotalMoney = decimal.Parse(ds.Tables[0].Rows[0]["OrderTotalMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderDiscountMoney"] != null && ds.Tables[0].Rows[0]["OrderDiscountMoney"].ToString() != "")
				{
					model.OrderDiscountMoney = decimal.Parse(ds.Tables[0].Rows[0]["OrderDiscountMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderPayCard"] != null && ds.Tables[0].Rows[0]["OrderPayCard"].ToString() != "")
				{
					model.OrderPayCard = decimal.Parse(ds.Tables[0].Rows[0]["OrderPayCard"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderPayCash"] != null && ds.Tables[0].Rows[0]["OrderPayCash"].ToString() != "")
				{
					model.OrderPayCash = decimal.Parse(ds.Tables[0].Rows[0]["OrderPayCash"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderPayCoupon"] != null && ds.Tables[0].Rows[0]["OrderPayCoupon"].ToString() != "")
				{
					model.OrderPayCoupon = decimal.Parse(ds.Tables[0].Rows[0]["OrderPayCoupon"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderPoint"] != null && ds.Tables[0].Rows[0]["OrderPoint"].ToString() != "")
				{
					model.OrderPoint = int.Parse(ds.Tables[0].Rows[0]["OrderPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderRemark"] != null && ds.Tables[0].Rows[0]["OrderRemark"].ToString() != "")
				{
					model.OrderRemark = ds.Tables[0].Rows[0]["OrderRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["OrderPayType"] != null && ds.Tables[0].Rows[0]["OrderPayType"].ToString() != "")
				{
					model.OrderPayType = int.Parse(ds.Tables[0].Rows[0]["OrderPayType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderShopID"] != null && ds.Tables[0].Rows[0]["OrderShopID"].ToString() != "")
				{
					model.OrderShopID = int.Parse(ds.Tables[0].Rows[0]["OrderShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderCreateTime"] != null && ds.Tables[0].Rows[0]["OrderCreateTime"].ToString() != "")
				{
					model.OrderCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["OrderCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderUserID"] != null && ds.Tables[0].Rows[0]["OrderUserID"].ToString() != "")
				{
					model.OrderUserID = int.Parse(ds.Tables[0].Rows[0]["OrderUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderIsCard"] != null && ds.Tables[0].Rows[0]["OrderIsCard"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["OrderIsCard"].ToString() == "1" || ds.Tables[0].Rows[0]["OrderIsCard"].ToString().ToLower() == "true")
					{
						model.OrderIsCard = true;
					}
					else
					{
						model.OrderIsCard = false;
					}
				}
				if (ds.Tables[0].Rows[0]["OrderIsCash"] != null && ds.Tables[0].Rows[0]["OrderIsCash"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["OrderIsCash"].ToString() == "1" || ds.Tables[0].Rows[0]["OrderIsCash"].ToString().ToLower() == "true")
					{
						model.OrderIsCash = true;
					}
					else
					{
						model.OrderIsCash = false;
					}
				}
				if (ds.Tables[0].Rows[0]["OrderIsBink"] != null && ds.Tables[0].Rows[0]["OrderIsBink"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["OrderIsBink"].ToString() == "1" || ds.Tables[0].Rows[0]["OrderIsBink"].ToString().ToLower() == "true")
					{
						model.OrderIsBink = true;
					}
					else
					{
						model.OrderIsBink = false;
					}
				}
				if (ds.Tables[0].Rows[0]["OrderPayBink"] != null && ds.Tables[0].Rows[0]["OrderPayBink"].ToString() != "")
				{
					model.OrderPayBink = decimal.Parse(ds.Tables[0].Rows[0]["OrderPayBink"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OldAccount"] != null && ds.Tables[0].Rows[0]["OldAccount"].ToString() != "")
				{
					model.OldAccount = ds.Tables[0].Rows[0]["OldAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["OrderCardBalance"] != null && ds.Tables[0].Rows[0]["OrderCardBalance"].ToString() != "")
				{
					model.OrderCardBalance = decimal.Parse(ds.Tables[0].Rows[0]["OrderCardBalance"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderCardPoint"] != null && ds.Tables[0].Rows[0]["OrderCardPoint"].ToString() != "")
				{
					model.OrderCardPoint = int.Parse(ds.Tables[0].Rows[0]["OrderCardPoint"].ToString());
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
			strSql.Append("select OrderID,OrderAccount,OrderType,OrderMemID,OrderTotalMoney,OrderDiscountMoney,OrderPayCard,OrderPayCash,OrderPayCoupon,OrderPoint,OrderRemark,OrderPayType,OrderShopID,OrderCreateTime,OrderUserID,OrderIsCard,OrderIsCash,OrderIsBink,OrderPayBink,OldAccount,OrderCardBalance,OrderCardPoint ");
			strSql.Append(" FROM OrderLog ");
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
			strSql.Append(" OrderID,OrderAccount,OrderType,OrderMemID,OrderTotalMoney,OrderDiscountMoney,OrderPayCard,OrderPayCash,OrderPoint,OrderRemark,OrderPayType,OrderShopID,OrderCreateTime,OrderUserID,OrderIsCard,OrderIsCash,OrderIsBink,OrderPayBink,OrderPayCoupon,OldAccount,OrderCardBalance,OrderCardPoint ");
			strSql.Append(" FROM OrderLog ");
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
			strSql.Append("select count(1) FROM OrderLog ");
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

		public decimal GetTotalMoney(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select SUM(OrderTotalMoney) FROM OrderLog,Mem,MemLevel,SysShop,SysUser ");
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

		public decimal GetAllMoney(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select SUM(OrderTotalMoney) FROM OrderLog,Mem");
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

		public decimal GetDiscountMoney(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select SUM(OrderDiscountMoney) FROM OrderLog,Mem,MemLevel,SysShop,SysUser ");
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

		public decimal GetDiscount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select SUM(OrderDiscountMoney) FROM OrderLog,Mem ");
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

		public int GetPoint(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select SUM(OrderPoint) FROM OrderLog,Mem ");
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

		public decimal GetCoupon(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select SUM(OrderPayCoupon) FROM OrderLog,Mem ");
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

		public DataSet GetOrderByTime(DateTime starttime, DateTime endtime, string strwhere)
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
			return DbHelperSQL.RunProcedure("MonthOrder", parameters, "#OrderData");
		}

		public DataSet GetGoodsExpense(DateTime starttime, DateTime endtime, string GoodsCode, string strwhere)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EndTime", SqlDbType.DateTime),
				new SqlParameter("@GoodsCode", SqlDbType.NVarChar, 50),
				new SqlParameter("@strWhere", SqlDbType.NVarChar, 2000)
			};
			parameters[0].Value = starttime;
			parameters[1].Value = endtime;
			parameters[2].Value = GoodsCode;
			parameters[3].Value = strwhere;
			return DbHelperSQL.RunProcedure("MonthGoodsExpense", parameters, "#GoodsData");
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
				strSql.Append("order by T.OrderID desc");
			}
			strSql.Append(")AS Row, T.*  from OrderLog T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetTotal(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select sum(OrderDiscountMoney)as TotalMoney,sum(OrderPoint)as TotalPoint ");
			strSql.Append(" FROM OrderLog ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "OrderLog,Mem,SysShop,SysUser";
			string[] columns = new string[]
			{
				"OrderLog.*,Mem.MemName,Mem.MemCard,Mem.MemID,Mem.MemLevelID,SysShop.ShopName,SysUser.UserName,(select count(orderdetailid) from orderdetail where orderdetail.orderid= orderlog.orderid ) as Count"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "OrderCreateTime", "OrderID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetEmptyBillsList(int PageSize, int PageIndex, out int resCount, string strSql, params string[] strWhere)
		{
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = OrderLog.GetEmptyBillsTable(columns, strWhere, "OrderID", strSql, false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public decimal GetTotalCash(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select sum(OrderPayCash) from OrderLog where OrderType <> 3 ");
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

		public int UpdateOrderLog(Chain.Model.OrderLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" update OrderLog set ");
			strSql.Append(" UsePoint=@UsePoint,");
			strSql.Append(" UsePointAmount=@UsePointAmount,");
			strSql.Append(" OrderPayType=@OrderPayType,");
			strSql.Append(" OrderType=@OrderType,");
			strSql.Append(" OrderDiscountMoney=@OrderDiscountMoney,");
			strSql.Append(" OrderPayCard=@OrderPayCard,");
			strSql.Append(" OrderPayCash=@OrderPayCash,");
			strSql.Append(" OrderPayBink=@OrderPayBink,");
			strSql.Append(" OrderCreateTime=@OrderCreateTime");
			strSql.Append(" where OrderID=@OrderID");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@OrderPayType", SqlDbType.TinyInt, 1),
				new SqlParameter("@OrderType", SqlDbType.TinyInt, 1),
				new SqlParameter("@OrderDiscountMoney", SqlDbType.Money, 8),
				new SqlParameter("@OrderPayCard", SqlDbType.Money, 8),
				new SqlParameter("@OrderPayCash", SqlDbType.Money, 8),
				new SqlParameter("@OrderPayBink", SqlDbType.Money, 8),
				new SqlParameter("@OrderCreateTime", SqlDbType.DateTime),
				new SqlParameter("@OrderID", SqlDbType.Int, 4),
				new SqlParameter("@UsePoint", SqlDbType.Int, 4),
				new SqlParameter("@UsePointAmount", SqlDbType.Money, 8)
			};
			parameter[0].Value = model.OrderPayType;
			parameter[1].Value = model.OrderType;
			parameter[2].Value = model.OrderDiscountMoney;
			parameter[3].Value = model.OrderPayCard;
			parameter[4].Value = model.OrderPayCash;
			parameter[5].Value = model.OrderPayBink;
			parameter[6].Value = model.OrderCreateTime;
			parameter[7].Value = model.OrderID;
			parameter[8].Value = model.UsePoint;
			parameter[9].Value = model.UsePointAmount;
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

		public int UpdateLog(int orderID, decimal totalMoney, decimal discountMoney, int point, decimal payCard, decimal payCash, decimal payBank, string oldAccount)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" update OrderLog set OrderTotalMoney =@OrderTotalMoney,");
			strSql.Append(" OrderDiscountMoney=@OrderDiscountMoney,");
			strSql.Append(" OrderPoint=@OrderPoint,");
			strSql.Append(" OrderPayCard=@OrderPayCard,");
			strSql.Append(" OrderPayCash=@OrderPayCash,");
			strSql.Append(" OrderPayBink=@OrderPayBink,");
			strSql.Append(" OldAccount=@OldAccount");
			strSql.Append(" where OrderID=@OrderID");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@OrderTotalMoney", SqlDbType.Money, 8),
				new SqlParameter("@OrderDiscountMoney", SqlDbType.Money, 8),
				new SqlParameter("@OrderPoint", SqlDbType.Int, 8),
				new SqlParameter("@OrderPayCard", SqlDbType.Money, 8),
				new SqlParameter("@OrderPayCash", SqlDbType.Money, 8),
				new SqlParameter("@OrderPayBink", SqlDbType.Money, 8),
				new SqlParameter("@OldAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@OrderID", SqlDbType.Int, 4)
			};
			parameter[0].Value = totalMoney;
			parameter[1].Value = discountMoney;
			parameter[2].Value = point;
			parameter[3].Value = payCard;
			parameter[4].Value = payCash;
			parameter[5].Value = payBank;
			parameter[6].Value = oldAccount;
			parameter[7].Value = orderID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}

		public DataSet GetMemExpenseMoney(string strSql)
		{
			StringBuilder strWhere = new StringBuilder();
			strWhere.Append("select isnull(count( distinct OrderMemID ),0) as TotalNumber,isnull(sum(OrderDiscountMoney),0) as TotalMoney from OrderLog,Mem ");
			if (strSql.Trim() != "")
			{
				strWhere.Append(" where " + strSql);
			}
			return DbHelperSQL.Query(strWhere.ToString());
		}

		public static DataSet GetEmptyBillsTable(string[] column, string[] condition, string IndexColumn, string strSql, bool IsAsc, int PageSize, int Page, int RecordCount)
		{
			string tempsql = "";
			string tempsql2 = "";
			for (int i = 0; i < column.Length; i++)
			{
				tempsql += column[i];
				if (i < column.Length - 1)
				{
					tempsql += ",";
				}
			}
			if (condition != null && condition.Length > 0)
			{
				for (int i = 0; i < condition.Length; i++)
				{
					if (!(condition[i] == ""))
					{
						tempsql2 += condition[i];
						if (i < condition.Length - 1)
						{
							tempsql2 += " and ";
						}
					}
				}
			}
			tempsql2 = DbHelperSQL.ProcessSql(tempsql2);
			SqlParameter[] paras = new SqlParameter[]
			{
				new SqlParameter("@ReturnFields", SqlDbType.VarChar, 500),
				new SqlParameter("@OrderFields", SqlDbType.VarChar, 255),
				new SqlParameter("@PageSize", SqlDbType.Int, 4),
				new SqlParameter("@PageIndex", SqlDbType.Int, 4),
				new SqlParameter("@PKField", SqlDbType.VarChar, 255),
				new SqlParameter("@IsDesc", SqlDbType.Bit),
				new SqlParameter("@Where", SqlDbType.VarChar, 1000),
				new SqlParameter("@levelID", SqlDbType.VarChar, 50)
			};
			paras[0].Value = tempsql;
			paras[1].Value = IndexColumn;
			paras[2].Value = PageSize;
			paras[3].Value = Page;
			paras[4].Value = IndexColumn;
			paras[5].Value = (IsAsc ? 0 : 1);
			paras[6].Value = tempsql2;
			paras[7].Value = strSql;
			return DbHelperSQL.ExecuteProcedure("CP_EmptyBills", paras);
		}
	}
}
