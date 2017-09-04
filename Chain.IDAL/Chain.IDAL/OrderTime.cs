using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class OrderTime
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("OrderTimeID", "OrderTime");
		}

		public bool Exists(int OrderTimeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from OrderTime");
			strSql.Append(" where OrderTimeID=@OrderTimeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderTimeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = OrderTimeID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool ExistsRules(int RulesID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from OrderTime");
			strSql.Append(" where OrderRulesID=@OrderRulesID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderRulesID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RulesID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool ExistsProject(int ProjectID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from OrderTime");
			strSql.Append(" where OrderProjectID=@OrderProjectID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderProjectID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ProjectID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool isUse(int Project)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from OrderTime");
			strSql.Append(" where OrderState=0 and OrderProjectID=@OrderProjectID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderProjectID", SqlDbType.Int, 4)
			};
			parameters[0].Value = Project;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.OrderTime model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into OrderTime(");
			strSql.Append("OrderTimeCode,OrderMemID,OrderToken,OrderMarchTime,OrderState,OrderStartUserID,OrderEndUserID,OrderCreateTime,OrderShopID,OrderProjectID,OrderRulesID,OrderPredictTime)");
			strSql.Append(" values (");
			strSql.Append("@OrderTimeCode,@OrderMemID,@OrderToken,@OrderMarchTime,@OrderState,@OrderStartUserID,@OrderEndUserID,@OrderCreateTime,@OrderShopID,@OrderProjectID,@OrderRulesID,@OrderPredictTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderTimeCode", SqlDbType.VarChar, 50),
				new SqlParameter("@OrderMemID", SqlDbType.Int, 4),
				new SqlParameter("@OrderToken", SqlDbType.VarChar, 50),
				new SqlParameter("@OrderMarchTime", SqlDbType.DateTime),
				new SqlParameter("@OrderState", SqlDbType.Bit, 1),
				new SqlParameter("@OrderStartUserID", SqlDbType.Int, 4),
				new SqlParameter("@OrderEndUserID", SqlDbType.Int, 4),
				new SqlParameter("@OrderCreateTime", SqlDbType.DateTime),
				new SqlParameter("@OrderShopID", SqlDbType.Int, 4),
				new SqlParameter("@OrderProjectID", SqlDbType.Int),
				new SqlParameter("@OrderRulesID", SqlDbType.Int),
				new SqlParameter("@OrderPredictTime", SqlDbType.Money)
			};
			parameters[0].Value = model.OrderTimeCode;
			parameters[1].Value = model.OrderMemID;
			parameters[2].Value = model.OrderToken;
			parameters[3].Value = model.OrderMarchTime;
			parameters[4].Value = model.OrderState;
			parameters[5].Value = model.OrderStartUserID;
			parameters[6].Value = model.OrderEndUserID;
			parameters[7].Value = model.OrderCreateTime;
			parameters[8].Value = model.OrderShopID;
			parameters[9].Value = model.OrderProjectID;
			parameters[10].Value = model.OrderRulesID;
			parameters[11].Value = model.OrderPredictTime;
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

		public bool Update(Chain.Model.OrderTime model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update OrderTime set ");
			strSql.Append("OrderTimeCode=@OrderTimeCode,");
			strSql.Append("OrderMemID=@OrderMemID,");
			strSql.Append("OrderToken=@OrderToken,");
			strSql.Append("OrderMarchTime=@OrderMarchTime,");
			strSql.Append("OrderState=@OrderState,");
			strSql.Append("OrderOutTime=@OrderOutTime,");
			strSql.Append("OrderStartUserID=@OrderStartUserID,");
			strSql.Append("OrderEndUserID=@OrderEndUserID,");
			strSql.Append("OrderCreateTime=@OrderCreateTime,");
			strSql.Append("OrderShopID=@OrderShopID,");
			strSql.Append("OrderProjectID=@OrderProjectID,");
			strSql.Append("OrderRulesID=@OrderRulesID,");
			strSql.Append("OrderRemark=@OrderRemark");
			strSql.Append(" where OrderTimeID=@OrderTimeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderTimeCode", SqlDbType.VarChar, 50),
				new SqlParameter("@OrderMemID", SqlDbType.Int, 4),
				new SqlParameter("@OrderToken", SqlDbType.VarChar, 50),
				new SqlParameter("@OrderMarchTime", SqlDbType.DateTime),
				new SqlParameter("@OrderState", SqlDbType.Bit, 1),
				new SqlParameter("@OrderOutTime", SqlDbType.DateTime),
				new SqlParameter("@OrderStartUserID", SqlDbType.Int, 4),
				new SqlParameter("@OrderEndUserID", SqlDbType.Int, 4),
				new SqlParameter("@OrderCreateTime", SqlDbType.DateTime),
				new SqlParameter("@OrderShopID", SqlDbType.Int, 4),
				new SqlParameter("@OrderTimeID", SqlDbType.Int, 4),
				new SqlParameter("@OrderProjectID", SqlDbType.Int, 4),
				new SqlParameter("@OrderRulesID", SqlDbType.Int, 4),
				new SqlParameter("@OrderRemark", SqlDbType.NVarChar, 400)
			};
			parameters[0].Value = model.OrderTimeCode;
			parameters[1].Value = model.OrderMemID;
			parameters[2].Value = model.OrderToken;
			parameters[3].Value = model.OrderMarchTime;
			parameters[4].Value = model.OrderState;
			parameters[5].Value = model.OrderOutTime;
			parameters[6].Value = model.OrderStartUserID;
			parameters[7].Value = model.OrderEndUserID;
			parameters[8].Value = model.OrderCreateTime;
			parameters[9].Value = model.OrderShopID;
			parameters[10].Value = model.OrderTimeID;
			parameters[11].Value = model.OrderProjectID;
			parameters[12].Value = model.OrderRulesID;
			parameters[13].Value = model.OrderRemark;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int OrderTimeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from OrderTime ");
			strSql.Append(" where OrderTimeID=@OrderTimeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderTimeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = OrderTimeID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string OrderTimeIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from OrderTime ");
			strSql.Append(" where OrderTimeID in (" + OrderTimeIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.OrderTime GetModel(int OrderTimeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 * from OrderTime ");
			strSql.Append(" where OrderTimeID=@OrderTimeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderTimeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = OrderTimeID;
			Chain.Model.OrderTime model = new Chain.Model.OrderTime();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.OrderTime result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["OrderTimeID"] != null && ds.Tables[0].Rows[0]["OrderTimeID"].ToString() != "")
				{
					model.OrderTimeID = int.Parse(ds.Tables[0].Rows[0]["OrderTimeID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderTimeCode"] != null && ds.Tables[0].Rows[0]["OrderTimeCode"].ToString() != "")
				{
					model.OrderTimeCode = ds.Tables[0].Rows[0]["OrderTimeCode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["OrderMemID"] != null && ds.Tables[0].Rows[0]["OrderMemID"].ToString() != "")
				{
					model.OrderMemID = int.Parse(ds.Tables[0].Rows[0]["OrderMemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderToken"] != null && ds.Tables[0].Rows[0]["OrderToken"].ToString() != "")
				{
					model.OrderToken = ds.Tables[0].Rows[0]["OrderToken"].ToString();
				}
				if (ds.Tables[0].Rows[0]["OrderMarchTime"] != null && ds.Tables[0].Rows[0]["OrderMarchTime"].ToString() != "")
				{
					model.OrderMarchTime = DateTime.Parse(ds.Tables[0].Rows[0]["OrderMarchTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderState"] != null && ds.Tables[0].Rows[0]["OrderState"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["OrderState"].ToString() == "1" || ds.Tables[0].Rows[0]["OrderState"].ToString().ToLower() == "true")
					{
						model.OrderState = true;
					}
					else
					{
						model.OrderState = false;
					}
				}
				if (ds.Tables[0].Rows[0]["OrderOutTime"] != null && ds.Tables[0].Rows[0]["OrderOutTime"].ToString() != "")
				{
					model.OrderOutTime = DateTime.Parse(ds.Tables[0].Rows[0]["OrderOutTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderStartUserID"] != null && ds.Tables[0].Rows[0]["OrderStartUserID"].ToString() != "")
				{
					model.OrderStartUserID = int.Parse(ds.Tables[0].Rows[0]["OrderStartUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderEndUserID"] != null && ds.Tables[0].Rows[0]["OrderEndUserID"].ToString() != "")
				{
					model.OrderEndUserID = int.Parse(ds.Tables[0].Rows[0]["OrderEndUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderCreateTime"] != null && ds.Tables[0].Rows[0]["OrderCreateTime"].ToString() != "")
				{
					model.OrderCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["OrderCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderShopID"] != null && ds.Tables[0].Rows[0]["OrderShopID"].ToString() != "")
				{
					model.OrderShopID = int.Parse(ds.Tables[0].Rows[0]["OrderShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderProjectID"] != null && ds.Tables[0].Rows[0]["OrderProjectID"].ToString() != "")
				{
					model.OrderProjectID = int.Parse(ds.Tables[0].Rows[0]["OrderProjectID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderRulesID"] != null && ds.Tables[0].Rows[0]["OrderRulesID"].ToString() != "")
				{
					model.OrderRulesID = int.Parse(ds.Tables[0].Rows[0]["OrderRulesID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderRemark"] != null && ds.Tables[0].Rows[0]["OrderRemark"].ToString() != "")
				{
					model.OrderRemark = ds.Tables[0].Rows[0]["OrderRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["OrderPredictTime"] != null && ds.Tables[0].Rows[0]["OrderPredictTime"].ToString() != "")
				{
					model.OrderPredictTime = Convert.ToDecimal(ds.Tables[0].Rows[0]["OrderPredictTime"]);
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
			strSql.Append("select * ");
			strSql.Append(" FROM OrderTime ");
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
			strSql.Append(" * FROM OrderTime ");
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
			strSql.Append("select count(1) FROM OrderTime ");
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
				strSql.Append("order by T.OrderTimeID desc");
			}
			strSql.Append(")AS Row, T.*  from OrderTime T ");
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
			string tableName = "V_OrderTime";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "OrderCreateTime", "OrderTimeID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
