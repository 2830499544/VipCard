using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class MicroWebsiteOrderLogDetail
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("MicroOrderLogDetailID", "MicroWebsiteOrderLogDetail");
		}

		public bool Exists(int MicroOrderLogDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MicroWebsiteOrderLogDetail");
			strSql.Append(" where MicroOrderLogDetailID=@MicroOrderLogDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroOrderLogDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MicroOrderLogDetailID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.MicroWebsiteOrderLogDetail model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MicroWebsiteOrderLogDetail(");
			strSql.Append("MicroOrderID,MicroGoodsID,MicroOrderDetailPrice,MicroOrderDetailPoint,MicroOrderDetailDiscountPrice,MicroOrderDetailNumber)");
			strSql.Append(" values (");
			strSql.Append("@MicroOrderID,@MicroGoodsID,@MicroOrderDetailPrice,@MicroOrderDetailPoint,@MicroOrderDetailDiscountPrice,@MicroOrderDetailNumber)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroOrderID", SqlDbType.Int, 4),
				new SqlParameter("@MicroGoodsID", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderDetailPrice", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderDetailPoint", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderDetailDiscountPrice", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderDetailNumber", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MicroOrderID;
			parameters[1].Value = model.MicroGoodsID;
			parameters[2].Value = model.MicroOrderDetailPrice;
			parameters[3].Value = model.MicroOrderDetailPoint;
			parameters[4].Value = model.MicroOrderDetailDiscountPrice;
			parameters[5].Value = model.MicroOrderDetailNumber;
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

		public bool Update(Chain.Model.MicroWebsiteOrderLogDetail model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MicroWebsiteOrderLogDetail set ");
			strSql.Append("MicroOrderID=@MicroOrderID,");
			strSql.Append("MicroGoodsID=@MicroGoodsID,");
			strSql.Append("MicroOrderDetailPrice=@MicroOrderDetailPrice,");
			strSql.Append("MicroOrderDetailPoint=@MicroOrderDetailPoint,");
			strSql.Append("MicroOrderDetailDiscountPrice=@MicroOrderDetailDiscountPrice,");
			strSql.Append("MicroOrderDetailNumber=@MicroOrderDetailNumber");
			strSql.Append(" where MicroOrderLogDetailID=@MicroOrderLogDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroOrderID", SqlDbType.Int, 4),
				new SqlParameter("@MicroGoodsID", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderDetailPrice", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderDetailPoint", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderDetailDiscountPrice", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderDetailNumber", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderLogDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MicroOrderID;
			parameters[1].Value = model.MicroGoodsID;
			parameters[2].Value = model.MicroOrderDetailPrice;
			parameters[3].Value = model.MicroOrderDetailPoint;
			parameters[4].Value = model.MicroOrderDetailDiscountPrice;
			parameters[5].Value = model.MicroOrderDetailNumber;
			parameters[6].Value = model.MicroOrderLogDetailID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int MicroOrderLogDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MicroWebsiteOrderLogDetail ");
			strSql.Append(" where MicroOrderLogDetailID=@MicroOrderLogDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroOrderLogDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MicroOrderLogDetailID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string MicroOrderLogDetailIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MicroWebsiteOrderLogDetail ");
			strSql.Append(" where MicroOrderLogDetailID in (" + MicroOrderLogDetailIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MicroWebsiteOrderLogDetail GetModel(int MicroOrderLogDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 MicroOrderLogDetailID,MicroOrderID,MicroGoodsID,MicroOrderDetailPrice,MicroOrderDetailPoint,MicroOrderDetailDiscountPrice,MicroOrderDetailNumber from MicroWebsiteOrderLogDetail ");
			strSql.Append(" where MicroOrderLogDetailID=@MicroOrderLogDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroOrderLogDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MicroOrderLogDetailID;
			Chain.Model.MicroWebsiteOrderLogDetail model = new Chain.Model.MicroWebsiteOrderLogDetail();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MicroWebsiteOrderLogDetail result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["MicroOrderLogDetailID"] != null && ds.Tables[0].Rows[0]["MicroOrderLogDetailID"].ToString() != "")
				{
					model.MicroOrderLogDetailID = int.Parse(ds.Tables[0].Rows[0]["MicroOrderLogDetailID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderID"] != null && ds.Tables[0].Rows[0]["MicroOrderID"].ToString() != "")
				{
					model.MicroOrderID = int.Parse(ds.Tables[0].Rows[0]["MicroOrderID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroGoodsID"] != null && ds.Tables[0].Rows[0]["MicroGoodsID"].ToString() != "")
				{
					model.MicroGoodsID = int.Parse(ds.Tables[0].Rows[0]["MicroGoodsID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderDetailPrice"] != null && ds.Tables[0].Rows[0]["MicroOrderDetailPrice"].ToString() != "")
				{
					model.MicroOrderDetailPrice = decimal.Parse(ds.Tables[0].Rows[0]["MicroOrderDetailPrice"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderDetailPoint"] != null && ds.Tables[0].Rows[0]["MicroOrderDetailPoint"].ToString() != "")
				{
					model.MicroOrderDetailPoint = int.Parse(ds.Tables[0].Rows[0]["MicroOrderDetailPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderDetailDiscountPrice"] != null && ds.Tables[0].Rows[0]["MicroOrderDetailDiscountPrice"].ToString() != "")
				{
					model.MicroOrderDetailDiscountPrice = decimal.Parse(ds.Tables[0].Rows[0]["MicroOrderDetailDiscountPrice"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderDetailNumber"] != null && ds.Tables[0].Rows[0]["MicroOrderDetailNumber"].ToString() != "")
				{
					model.MicroOrderDetailNumber = int.Parse(ds.Tables[0].Rows[0]["MicroOrderDetailNumber"].ToString());
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
			strSql.Append("select MicroOrderLogDetailID,MicroOrderID,MicroGoodsID,MicroOrderDetailPrice,MicroOrderDetailPoint,MicroOrderDetailDiscountPrice,MicroOrderDetailNumber ");
			strSql.Append(" FROM MicroWebsiteOrderLogDetail ");
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
			strSql.Append(" MicroOrderLogDetailID,MicroOrderID,MicroGoodsID,MicroOrderDetailPrice,MicroOrderDetailPoint,MicroOrderDetailDiscountPrice,MicroOrderDetailNumber ");
			strSql.Append(" FROM MicroWebsiteOrderLogDetail ");
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
			strSql.Append("select count(1) FROM MicroWebsiteOrderLogDetail ");
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
				strSql.Append("order by T.MicroOrderLogDetailID desc");
			}
			strSql.Append(")AS Row, T.*  from MicroWebsiteOrderLogDetail T ");
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
			strSql.Append(" select MicroWebsiteOrderLogDetail.*,MicroWebsiteGoods.MicroGoodsName,MicroWebsiteGoods.MicroGoodsCode,MicroWebsiteGoods.MicroSalePrice ");
			strSql.Append(" from MicroWebsiteOrderLogDetail,MicroWebsiteGoods,MicroWebsiteOrderLog");
			strSql.Append(" where " + strWhere);
			return DbHelperSQL.Query(strSql.ToString());
		}
	}
}
