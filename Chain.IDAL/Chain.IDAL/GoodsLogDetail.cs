using Chain.DBUtility;
using Chain.Model;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class GoodsLogDetail
	{
		public bool Exists(int GoodsLogDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from GoodsLogDetail");
			strSql.Append(" where GoodsLogDetailID=@GoodsLogDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsLogDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GoodsLogDetailID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.GoodsLogDetail model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into GoodsLogDetail(");
			strSql.Append("GoodsLogID,GoodsID,GoodsInPrice,GoodsOutPrice,GoodsNumber)");
			strSql.Append(" values (");
			strSql.Append("@GoodsLogID,@GoodsID,@GoodsInPrice,@GoodsOutPrice,@GoodsNumber)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsLogID", SqlDbType.Int, 4),
				new SqlParameter("@GoodsID", SqlDbType.Int, 4),
				new SqlParameter("@GoodsInPrice", SqlDbType.Money, 8),
				new SqlParameter("@GoodsOutPrice", SqlDbType.Money, 8),
				new SqlParameter("@GoodsNumber", SqlDbType.Float, 8)
			};
			parameters[0].Value = model.GoodsLogID;
			parameters[1].Value = model.GoodsID;
			parameters[2].Value = model.GoodsInPrice;
			parameters[3].Value = model.GoodsOutPrice;
			parameters[4].Value = model.GoodsNumber;
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

		public bool Update(Chain.Model.GoodsLogDetail model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update GoodsLogDetail set ");
			strSql.Append("GoodsLogID=@GoodsLogID,");
			strSql.Append("GoodsID=@GoodsID,");
			strSql.Append("GoodsInPrice=@GoodsInPrice,");
			strSql.Append("GoodsOutPrice=@GoodsOutPrice,");
			strSql.Append("GoodsNumber=@GoodsNumber");
			strSql.Append(" where GoodsLogDetailID=@GoodsLogDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsLogID", SqlDbType.Int, 4),
				new SqlParameter("@GoodsID", SqlDbType.Int, 4),
				new SqlParameter("@GoodsInPrice", SqlDbType.Money, 8),
				new SqlParameter("@GoodsOutPrice", SqlDbType.Money, 8),
				new SqlParameter("@GoodsNumber", SqlDbType.Float, 8),
				new SqlParameter("@GoodsLogDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.GoodsLogID;
			parameters[1].Value = model.GoodsID;
			parameters[2].Value = model.GoodsInPrice;
			parameters[3].Value = model.GoodsOutPrice;
			parameters[4].Value = model.GoodsNumber;
			parameters[5].Value = model.GoodsLogDetailID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int GoodsLogDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsLogDetail ");
			strSql.Append(" where GoodsLogDetailID=@GoodsLogDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsLogDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GoodsLogDetailID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string GoodsLogDetailIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsLogDetail ");
			strSql.Append(" where GoodsLogDetailID in (" + GoodsLogDetailIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.GoodsLogDetail GetModel(int GoodsLogDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 GoodsLogDetailID,GoodsLogID,GoodsID,GoodsInPrice,GoodsOutPrice,GoodsNumber from GoodsLogDetail ");
			strSql.Append(" where GoodsLogDetailID=@GoodsLogDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsLogDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GoodsLogDetailID;
			Chain.Model.GoodsLogDetail model = new Chain.Model.GoodsLogDetail();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.GoodsLogDetail result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["GoodsLogDetailID"] != null && ds.Tables[0].Rows[0]["GoodsLogDetailID"].ToString() != "")
				{
					model.GoodsLogDetailID = int.Parse(ds.Tables[0].Rows[0]["GoodsLogDetailID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsLogID"] != null && ds.Tables[0].Rows[0]["GoodsLogID"].ToString() != "")
				{
					model.GoodsLogID = int.Parse(ds.Tables[0].Rows[0]["GoodsLogID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsID"] != null && ds.Tables[0].Rows[0]["GoodsID"].ToString() != "")
				{
					model.GoodsID = int.Parse(ds.Tables[0].Rows[0]["GoodsID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsInPrice"] != null && ds.Tables[0].Rows[0]["GoodsInPrice"].ToString() != "")
				{
					model.GoodsInPrice = decimal.Parse(ds.Tables[0].Rows[0]["GoodsInPrice"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsOutPrice"] != null && ds.Tables[0].Rows[0]["GoodsOutPrice"].ToString() != "")
				{
					model.GoodsOutPrice = decimal.Parse(ds.Tables[0].Rows[0]["GoodsOutPrice"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsNumber"] != null && ds.Tables[0].Rows[0]["GoodsNumber"].ToString() != "")
				{
					model.GoodsNumber = decimal.Parse(ds.Tables[0].Rows[0]["GoodsNumber"].ToString());
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
			strSql.Append("select GoodsLogDetailID,GoodsLogID,GoodsID,GoodsInPrice,GoodsOutPrice,GoodsNumber ");
			strSql.Append(" FROM GoodsLogDetail ");
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
			strSql.Append(" GoodsLogDetailID,GoodsLogID,GoodsID,GoodsInPrice,GoodsOutPrice,GoodsNumber ");
			strSql.Append(" FROM GoodsLogDetail ");
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
			strSql.Append("select count(1) FROM GoodsLogDetail ");
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
				strSql.Append("order by T.GoodsLogDetailID desc");
			}
			strSql.Append(")AS Row, T.*  from GoodsLogDetail T ");
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
			strSql.Append(" select GoodsLogDetail.*,Goods.Name,Goods.GoodsCode,Goods.GoodsType");
			strSql.Append(" from GoodsLogDetail,Goods");
			strSql.Append(" where " + strWhere);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public bool DeleteDetail(int goodsLogID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsLogDetail ");
			strSql.Append(" where GoodsLogID=@GoodsLogID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsLogID", SqlDbType.Int, 4)
			};
			parameters[0].Value = goodsLogID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public DataSet GetInsufficientCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("SELECT ISNULL(COUNT(1),0) AS InsufficientCount FROM GoodsLogDetail INNER JOIN GoodsLog ON GoodsLogDetail.GoodsLogID = GoodsLog.ID  INNER JOIN GoodsNumber ON GoodsNumber.GoodsID = GoodsLogDetail.GoodsID  AND GoodsNumber.ShopID = GoodsLog.ChangeShopID WHERE GoodsLogDetail.GoodsNumber > GoodsNumber.Number");
			strSql.Append(" AND " + strWhere);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public bool ExeclDataInput(ArrayList sqlArray)
		{
			return DbHelperSQL.ExecuteSqlTran(sqlArray);
		}

		public DataSet getGoodsLogDetail(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select GoodsLogDetail.*,Goods.Name,Goods.GoodsCode ");
			strSql.Append(" from GoodsLogDetail,Goods");
			strSql.Append(" where GoodsLogDetail.GoodsID=Goods.GoodsID and " + strWhere);
			return DbHelperSQL.Query(strSql.ToString());
		}
	}
}
