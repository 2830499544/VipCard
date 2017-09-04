using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class MemCountDetail
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("CountDetailID", "MemCountDetail");
		}

		public bool Exists(int CountDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemCountDetail");
			strSql.Append(" where CountDetailID=@CountDetailID ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CountDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CountDetailID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Add(Chain.Model.MemCountDetail model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MemCountDetail(");
			strSql.Append("CountDetailCountID,CountDetailGoodsID,CountDetailMemID,CountDetailTotalNumber,CountDetailNumber,CountDetailDiscountMoney,CountDetailPoint,CountCreateTime)");
			strSql.Append(" values (");
			strSql.Append("@CountDetailCountID,@CountDetailGoodsID,@CountDetailMemID,@CountDetailTotalNumber,@CountDetailNumber,@CountDetailDiscountMoney,@CountDetailPoint,@CountCreateTime)");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CountDetailCountID", SqlDbType.Int, 4),
				new SqlParameter("@CountDetailGoodsID", SqlDbType.Int, 4),
				new SqlParameter("@CountDetailMemID", SqlDbType.Int, 4),
				new SqlParameter("@CountDetailTotalNumber", SqlDbType.Int, 4),
				new SqlParameter("@CountDetailNumber", SqlDbType.Int, 4),
				new SqlParameter("@CountDetailDiscountMoney", SqlDbType.Money, 8),
				new SqlParameter("@CountDetailPoint", SqlDbType.Int, 4),
				new SqlParameter("@CountCreateTime", SqlDbType.DateTime)
			};
			parameters[0].Value = model.CountDetailCountID;
			parameters[1].Value = model.CountDetailGoodsID;
			parameters[2].Value = model.CountDetailMemID;
			parameters[3].Value = model.CountDetailTotalNumber;
			parameters[4].Value = model.CountDetailNumber;
			parameters[5].Value = model.CountDetailDiscountMoney;
			parameters[6].Value = model.CountDetailPoint;
			parameters[7].Value = model.CountCreateTime;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Update(Chain.Model.MemCountDetail model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MemCountDetail set ");
			strSql.Append("CountDetailCountID=@CountDetailCountID,");
			strSql.Append("CountDetailGoodsID=@CountDetailGoodsID,");
			strSql.Append("CountDetailMemID=@CountDetailMemID,");
			strSql.Append("CountDetailTotalNumber=@CountDetailTotalNumber,");
			strSql.Append("CountDetailNumber=@CountDetailNumber,");
			strSql.Append("CountDetailDiscountMoney=@CountDetailDiscountMoney,");
			strSql.Append("CountDetailPoint=@CountDetailPoint,");
			strSql.Append("CountCreateTime=@CountCreateTime");
			strSql.Append(" where CountDetailID=@CountDetailID ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CountDetailCountID", SqlDbType.Int, 4),
				new SqlParameter("@CountDetailGoodsID", SqlDbType.Int, 4),
				new SqlParameter("@CountDetailMemID", SqlDbType.Int, 4),
				new SqlParameter("@CountDetailTotalNumber", SqlDbType.Int, 4),
				new SqlParameter("@CountDetailNumber", SqlDbType.Int, 4),
				new SqlParameter("@CountDetailDiscountMoney", SqlDbType.Money, 8),
				new SqlParameter("@CountDetailPoint", SqlDbType.Int, 4),
				new SqlParameter("@CountDetailID", SqlDbType.Int, 4),
				new SqlParameter("@CountCreateTime", SqlDbType.DateTime)
			};
			parameters[0].Value = model.CountDetailCountID;
			parameters[1].Value = model.CountDetailGoodsID;
			parameters[2].Value = model.CountDetailMemID;
			parameters[3].Value = model.CountDetailTotalNumber;
			parameters[4].Value = model.CountDetailNumber;
			parameters[5].Value = model.CountDetailDiscountMoney;
			parameters[6].Value = model.CountDetailPoint;
			parameters[7].Value = model.CountCreateTime;
			parameters[8].Value = model.CountDetailID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int CountDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemCountDetail ");
			strSql.Append(" where CountDetailID=@CountDetailID ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CountDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CountDetailID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string CountDetailIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemCountDetail ");
			strSql.Append(" where CountDetailID in (" + CountDetailIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MemCountDetail GetModel(int CountDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 CountDetailID,CountDetailCountID,CountDetailGoodsID,CountDetailMemID,CountDetailTotalNumber,CountDetailNumber,CountDetailDiscountMoney,CountDetailPoint,CountCreateTime from MemCountDetail ");
			strSql.Append(" where CountDetailID=@CountDetailID ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CountDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CountDetailID;
			Chain.Model.MemCountDetail model = new Chain.Model.MemCountDetail();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MemCountDetail result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["CountDetailID"] != null && ds.Tables[0].Rows[0]["CountDetailID"].ToString() != "")
				{
					model.CountDetailID = int.Parse(ds.Tables[0].Rows[0]["CountDetailID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountDetailCountID"] != null && ds.Tables[0].Rows[0]["CountDetailCountID"].ToString() != "")
				{
					model.CountDetailCountID = int.Parse(ds.Tables[0].Rows[0]["CountDetailCountID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountDetailGoodsID"] != null && ds.Tables[0].Rows[0]["CountDetailGoodsID"].ToString() != "")
				{
					model.CountDetailGoodsID = int.Parse(ds.Tables[0].Rows[0]["CountDetailGoodsID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountDetailMemID"] != null && ds.Tables[0].Rows[0]["CountDetailMemID"].ToString() != "")
				{
					model.CountDetailMemID = int.Parse(ds.Tables[0].Rows[0]["CountDetailMemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountDetailTotalNumber"] != null && ds.Tables[0].Rows[0]["CountDetailTotalNumber"].ToString() != "")
				{
					model.CountDetailTotalNumber = int.Parse(ds.Tables[0].Rows[0]["CountDetailTotalNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountDetailNumber"] != null && ds.Tables[0].Rows[0]["CountDetailNumber"].ToString() != "")
				{
					model.CountDetailNumber = int.Parse(ds.Tables[0].Rows[0]["CountDetailNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountDetailDiscountMoney"] != null && ds.Tables[0].Rows[0]["CountDetailDiscountMoney"].ToString() != "")
				{
					model.CountDetailDiscountMoney = decimal.Parse(ds.Tables[0].Rows[0]["CountDetailDiscountMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountDetailPoint"] != null && ds.Tables[0].Rows[0]["CountDetailPoint"].ToString() != "")
				{
					model.CountDetailPoint = int.Parse(ds.Tables[0].Rows[0]["CountDetailPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountCreateTime"] != null && ds.Tables[0].Rows[0]["CountCreateTime"].ToString() != "")
				{
					model.CountCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CountCreateTime"].ToString());
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
			strSql.Append("select CountDetailID,CountDetailCountID,CountAccount,CountDetailGoodsID,Name,CountDetailMemID,CountDetailTotalNumber,CountDetailNumber,CountDetailDiscountMoney,CountDetailPoint,MemCountDetail.CountCreateTime ");
			strSql.Append(" FROM MemCountDetail,Goods,MemCount ");
			strSql.Append(" where  MemCount.CountID=MemCountDetail.CountDetailCountID and Goods.GoodsID=MemCountDetail.CountDetailGoodsID ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" and " + strWhere);
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
			strSql.Append(" CountDetailID,CountDetailCountID,CountDetailGoodsID,CountDetailMemID,CountDetailTotalNumber,CountDetailNumber,CountDetailDiscountMoney,CountDetailPoint,CountCreateTime ");
			strSql.Append(" FROM MemCountDetail ");
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
			strSql.Append("select count(1) FROM MemCountDetail ");
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
				strSql.Append("order by T.CountDetailID desc");
			}
			strSql.Append(")AS Row, T.*  from MemCountDetail T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetMemCountList(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "V_CountList";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "CountDetailGoodsID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int UpdateCountDetailNumber(int intNumber, int intCountDetailID)
		{
			string sql_mem = string.Format(" update MemCountDetail set CountDetailNumber = CountDetailNumber-({0}) where CountDetailID ={1}", intNumber, intCountDetailID);
			return DbHelperSQL.ExecuteSql(sql_mem);
		}

		public DataSet GetQueryMemCountList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select Goods.[Name],sum(CountDetailNumber) as sumNumber FROM MemCountDetail,Goods ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" and GoodsID=CountDetailGoodsID Group by Goods.[Name] ");
			return DbHelperSQL.Query(strSql.ToString());
		}
	}
}
