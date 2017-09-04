using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class OrderDetail
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("OrderDetailID", "OrderDetail");
		}

		public bool Exists(int OrderDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from OrderDetail");
			strSql.Append(" where OrderDetailID=@OrderDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = OrderDetailID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.OrderDetail model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into OrderDetail(");
			strSql.Append("OrderID,GoodsID,OrderDetailPrice,OrderDetailPoint,OrderDetailDiscountPrice,OrderDetailNumber,OrderDetailType)");
			strSql.Append(" values (");
			strSql.Append("@OrderID,@GoodsID,@OrderDetailPrice,@OrderDetailPoint,@OrderDetailDiscountPrice,@OrderDetailNumber,@OrderDetailType)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderID", SqlDbType.Int, 4),
				new SqlParameter("@GoodsID", SqlDbType.Int, 4),
				new SqlParameter("@OrderDetailPrice", SqlDbType.Money, 8),
				new SqlParameter("@OrderDetailPoint", SqlDbType.Int, 4),
				new SqlParameter("@OrderDetailDiscountPrice", SqlDbType.Money, 8),
				new SqlParameter("@OrderDetailNumber", SqlDbType.Float, 8),
				new SqlParameter("@OrderDetailType", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.OrderID;
			parameters[1].Value = model.GoodsID;
			parameters[2].Value = model.OrderDetailPrice;
			parameters[3].Value = model.OrderDetailPoint;
			parameters[4].Value = model.OrderDetailDiscountPrice;
			parameters[5].Value = model.OrderDetailNumber;
			parameters[6].Value = model.OrderDetailType;
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

		public int Update(Chain.Model.OrderDetail model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update OrderDetail set ");
			strSql.Append("OrderID=@OrderID,");
			strSql.Append("GoodsID=@GoodsID,");
			strSql.Append("OrderDetailPrice=@OrderDetailPrice,");
			strSql.Append("OrderDetailPoint=@OrderDetailPoint,");
			strSql.Append("OrderDetailDiscountPrice=@OrderDetailDiscountPrice,");
			strSql.Append("OrderDetailNumber=@OrderDetailNumber,");
			strSql.Append("OrderDetailType=@OrderDetailType");
			strSql.Append(" where OrderDetailID=@OrderDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderID", SqlDbType.Int, 4),
				new SqlParameter("@GoodsID", SqlDbType.Int, 4),
				new SqlParameter("@OrderDetailPrice", SqlDbType.Money, 8),
				new SqlParameter("@OrderDetailPoint", SqlDbType.Int, 4),
				new SqlParameter("@OrderDetailDiscountPrice", SqlDbType.Money, 8),
				new SqlParameter("@OrderDetailNumber", SqlDbType.Float, 8),
				new SqlParameter("@OrderDetailType", SqlDbType.Int, 4),
				new SqlParameter("@OrderDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.OrderID;
			parameters[1].Value = model.GoodsID;
			parameters[2].Value = model.OrderDetailPrice;
			parameters[3].Value = model.OrderDetailPoint;
			parameters[4].Value = model.OrderDetailDiscountPrice;
			parameters[5].Value = model.OrderDetailNumber;
			parameters[6].Value = model.OrderDetailType;
			parameters[7].Value = model.OrderDetailID;
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

		public bool Delete(int OrderDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from OrderDetail ");
			strSql.Append(" where OrderDetailID=@OrderDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = OrderDetailID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string OrderDetailIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from OrderDetail ");
			strSql.Append(" where OrderDetailID in (" + OrderDetailIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.OrderDetail GetModel(int OrderDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 OrderDetailID,OrderID,GoodsID,OrderDetailPrice,OrderDetailPoint,OrderDetailDiscountPrice,OrderDetailNumber,OrderDetailType from OrderDetail ");
			strSql.Append(" where OrderDetailID=@OrderDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = OrderDetailID;
			Chain.Model.OrderDetail model = new Chain.Model.OrderDetail();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.OrderDetail result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["OrderDetailID"] != null && ds.Tables[0].Rows[0]["OrderDetailID"].ToString() != "")
				{
					model.OrderDetailID = int.Parse(ds.Tables[0].Rows[0]["OrderDetailID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderID"] != null && ds.Tables[0].Rows[0]["OrderID"].ToString() != "")
				{
					model.OrderID = int.Parse(ds.Tables[0].Rows[0]["OrderID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsID"] != null && ds.Tables[0].Rows[0]["GoodsID"].ToString() != "")
				{
					model.GoodsID = int.Parse(ds.Tables[0].Rows[0]["GoodsID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderDetailPrice"] != null && ds.Tables[0].Rows[0]["OrderDetailPrice"].ToString() != "")
				{
					model.OrderDetailPrice = decimal.Parse(ds.Tables[0].Rows[0]["OrderDetailPrice"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderDetailPoint"] != null && ds.Tables[0].Rows[0]["OrderDetailPoint"].ToString() != "")
				{
					model.OrderDetailPoint = int.Parse(ds.Tables[0].Rows[0]["OrderDetailPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderDetailDiscountPrice"] != null && ds.Tables[0].Rows[0]["OrderDetailDiscountPrice"].ToString() != "")
				{
					model.OrderDetailDiscountPrice = decimal.Parse(ds.Tables[0].Rows[0]["OrderDetailDiscountPrice"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderDetailNumber"] != null && ds.Tables[0].Rows[0]["OrderDetailNumber"].ToString() != "")
				{
					model.OrderDetailNumber = decimal.Parse(ds.Tables[0].Rows[0]["OrderDetailNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["OrderDetailType"] != null && ds.Tables[0].Rows[0]["OrderDetailType"].ToString() != "")
				{
					model.OrderDetailType = int.Parse(ds.Tables[0].Rows[0]["OrderDetailType"].ToString());
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
			strSql.Append("select OrderDetailID,OrderID,GoodsID,OrderDetailPrice,OrderDetailPoint,OrderDetailDiscountPrice,OrderDetailNumber,OrderDetailType ");
			strSql.Append(" FROM OrderDetail ");
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
			strSql.Append(" OrderDetailID,OrderID,GoodsID,OrderDetailPrice,OrderDetailPoint,OrderDetailDiscountPrice,OrderDetailNumber,OrderDetailType ");
			strSql.Append(" FROM OrderDetail ");
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
			strSql.Append("select count(1) FROM OrderDetail ");
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
				strSql.Append("order by T.OrderDetailID desc");
			}
			strSql.Append(")AS Row, T.*  from OrderDetail T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select OrderDetail.*,Goods.Name,Goods.GoodsCode,Goods.GoodsType");
			strSql.Append(" from OrderDetail,Goods,OrderLog");
			strSql.Append(" where " + strWhere);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int DeleteDetail(int inOrderID)
		{
			string strSql = " delete from OrderDetail where OrderID=" + inOrderID;
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		public DataSet GetDetail(int intOrderID)
		{
			string strSql = " select * from V_GoodsReturn where OrderID=" + intOrderID;
			string strAllNumber = string.Format("SELECT SUM(OrderDetailNumber) AS AllNumber FROM V_GoodsReturn WHERE OrderID ={0}", intOrderID);
			DataSet dsResults = new DataSet();
			dsResults.Tables.Add(DbHelperSQL.Query(strSql).Tables[0].Copy());
			DataTable dtNumber = DbHelperSQL.Query(strAllNumber).Tables[0];
			dtNumber.TableName = "AllNumber";
			dsResults.Tables.Add(dtNumber.Copy());
			return dsResults;
		}

		public int DeleteItem(int intOrder, int intGoodsID)
		{
			string strSql = string.Concat(new object[]
			{
				" delete from OrderDetail where OrderID=",
				intOrder,
				" and GoodsID=",
				intGoodsID
			});
			return DbHelperSQL.ExecuteSql(strSql);
		}

		public DataSet GetOrderDetail(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select OrderDetail.*,Goods.Name,Goods.GoodsCode,OrderLog.OrderRemark ");
			strSql.Append(" from OrderDetail,Goods,OrderLog");
			strSql.Append(" where OrderDetail.GoodsID=Goods.GoodsID AND OrderLog.OrderID = OrderDetail.OrderID and " + strWhere);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetGoodsExpenseDetail(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select ABS(OrderDetail.OrderDetailNumber) as OrderDetailNumber,OrderDetail.GoodsID,OrderDetail.OrderDetailDiscountPrice,OrderDetail.OrderDetailType, ");
			strSql.Append(" OrderLog.OrderAccount,OrderLog.OrderCreateTime,Mem.MemName,Mem.MemCard from OrderDetail,OrderLog,Mem");
			strSql.Append(" where OrderDetail.OrderID = OrderLog.OrderID and OrderLog.OrderMemID=Mem.MemID ");
			if (strWhere != "")
			{
				strSql.Append(strWhere);
			}
			strSql.Append(" order by OrderLog.OrderCreateTime desc ");
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetExpenseDetail(int PageSiza, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "OrderDetail,OrderLog,Mem,Goods";
			string[] columns = new string[]
			{
				"ABS(OrderDetail.OrderDetailNumber) as OrderDetailNumber,OrderDetail.GoodsID,OrderDetail.OrderDetailDiscountPrice,OrderDetail.OrderDetailType,OrderLog.OrderAccount,OrderLog.OrderCreateTime,Mem.MemName,Mem.MemCard "
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "OrderLog.OrderCreateTime", "OrderDetail.OrderID", false, PageSiza, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public string ProjectName(int OrderMemID, int OrderID)
		{
			string result = string.Empty;
			StringBuilder sbStr = new StringBuilder();
			sbStr.Append("select ProjectName from ");
			sbStr.Append("ordertime,OrderLog,TimingProject ");
			sbStr.Append("where ordertime.OrderTimeCode=OrderLog.OrderAccount ");
			sbStr.Append("and OrderLog.OrderMemID=@OrderMemID and OrderType=1 and OrderID=@OrderID ");
			sbStr.Append("and TimingProject.ProjectID=ordertime.OrderProjectID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OrderMemID", SqlDbType.Int, 4),
				new SqlParameter("@OrderID", SqlDbType.Int, 4)
			};
			parameters[0].Value = OrderMemID;
			parameters[1].Value = OrderID;
			DataTable dt = DbHelperSQL.Query(sbStr.ToString(), parameters).Tables[0];
			if (dt.Rows.Count > 0)
			{
				result = dt.Rows[0][0].ToString();
			}
			return result;
		}
	}
}
