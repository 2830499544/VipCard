using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class GoodsAllotDetail
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("AllotDetailID", "GoodsAllotDetail");
		}

		public bool Exists(int AllotDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from GoodsAllotDetail");
			strSql.Append(" where AllotDetailID=@AllotDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AllotDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = AllotDetailID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.GoodsAllotDetail model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into GoodsAllotDetail(");
			strSql.Append("AllotDetailAllotID,AllotDetailGoodsID,AllotDetailNumber)");
			strSql.Append(" values (");
			strSql.Append("@AllotDetailAllotID,@AllotDetailGoodsID,@AllotDetailNumber)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AllotDetailAllotID", SqlDbType.Int, 4),
				new SqlParameter("@AllotDetailGoodsID", SqlDbType.Int, 4),
				new SqlParameter("@AllotDetailNumber", SqlDbType.Decimal)
			};
			parameters[0].Value = model.AllotDetailAllotID;
			parameters[1].Value = model.AllotDetailGoodsID;
			parameters[2].Value = model.AllotDetailNumber;
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

		public bool Update(Chain.Model.GoodsAllotDetail model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update GoodsAllotDetail set ");
			strSql.Append("AllotDetailAllotID=@AllotDetailAllotID,");
			strSql.Append("AllotDetailGoodsID=@AllotDetailGoodsID,");
			strSql.Append("AllotDetailNumber=@AllotDetailNumber");
			strSql.Append(" where AllotDetailID=@AllotDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AllotDetailAllotID", SqlDbType.Int, 4),
				new SqlParameter("@AllotDetailGoodsID", SqlDbType.Int, 4),
				new SqlParameter("@AllotDetailNumber", SqlDbType.Int, 4),
				new SqlParameter("@AllotDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.AllotDetailAllotID;
			parameters[1].Value = model.AllotDetailGoodsID;
			parameters[2].Value = model.AllotDetailNumber;
			parameters[3].Value = model.AllotDetailID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Updates(Chain.Model.GoodsAllotDetail model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update GoodsAllotDetail set ");
			strSql.Append("AllotDetailAllotID=@AllotDetailAllotID,");
			strSql.Append("AllotDetailGoodsID=@AllotDetailGoodsID,");
			strSql.Append("AllotDetailNumber=@AllotDetailNumber");
			strSql.Append(" where AllotDetailAllotID=@AllotDetailAllotID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AllotDetailAllotID", SqlDbType.Int, 4),
				new SqlParameter("@AllotDetailGoodsID", SqlDbType.Int, 4),
				new SqlParameter("@AllotDetailNumber", SqlDbType.Int, 4),
				new SqlParameter("@AllotDetailAllotID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.AllotDetailAllotID;
			parameters[1].Value = model.AllotDetailGoodsID;
			parameters[2].Value = model.AllotDetailNumber;
			parameters[3].Value = model.AllotDetailAllotID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int AllotDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsAllotDetail ");
			strSql.Append(" where AllotDetailID=@AllotDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AllotDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = AllotDetailID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string AllotDetailIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsAllotDetail ");
			strSql.Append(" where AllotDetailID in (" + AllotDetailIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.GoodsAllotDetail GetModel(int AllotDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 AllotDetailID,AllotDetailAllotID,AllotDetailGoodsID,AllotDetailNumber from GoodsAllotDetail ");
			strSql.Append(" where AllotDetailID=@AllotDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AllotDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = AllotDetailID;
			Chain.Model.GoodsAllotDetail model = new Chain.Model.GoodsAllotDetail();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.GoodsAllotDetail result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["AllotDetailID"] != null && ds.Tables[0].Rows[0]["AllotDetailID"].ToString() != "")
				{
					model.AllotDetailID = int.Parse(ds.Tables[0].Rows[0]["AllotDetailID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AllotDetailAllotID"] != null && ds.Tables[0].Rows[0]["AllotDetailAllotID"].ToString() != "")
				{
					model.AllotDetailAllotID = int.Parse(ds.Tables[0].Rows[0]["AllotDetailAllotID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AllotDetailGoodsID"] != null && ds.Tables[0].Rows[0]["AllotDetailGoodsID"].ToString() != "")
				{
					model.AllotDetailGoodsID = int.Parse(ds.Tables[0].Rows[0]["AllotDetailGoodsID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AllotDetailNumber"] != null && ds.Tables[0].Rows[0]["AllotDetailNumber"].ToString() != "")
				{
					model.AllotDetailNumber = decimal.Parse(ds.Tables[0].Rows[0]["AllotDetailNumber"].ToString());
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
			strSql.Append("select AllotDetailID,AllotDetailAllotID,AllotDetailGoodsID,AllotDetailNumber ");
			strSql.Append(" FROM GoodsAllotDetail ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select GoodsAllotDetail.*,Goods.Name,Goods.GoodsCode,Goods.GoodsType");
			strSql.Append(" from GoodsAllotDetail,Goods");
			strSql.Append(" where " + strWhere);
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
			strSql.Append(" AllotDetailID,AllotDetailAllotID,AllotDetailGoodsID,AllotDetailNumber ");
			strSql.Append(" FROM GoodsAllotDetail ");
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
			strSql.Append("select count(1) FROM GoodsAllotDetail ");
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
				strSql.Append("order by T.AllotDetailID desc");
			}
			strSql.Append(")AS Row, T.*  from GoodsAllotDetail T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetAllotDetailByAllotID(int AllotID)
		{
			string sql = string.Format("select  * from GoodsAllotDetail where AllotDetailAllotID={0}", AllotID);
			return DbHelperSQL.Query(sql);
		}

		public DataSet AllotDetailByAllotID(int AllotID)
		{
			string sql = string.Format("select GoodsID,Name,AllotDetailNumber  from dbo.Goods join GoodsAllotDetail on Goods.GoodsID=GoodsAllotDetail.AllotDetailGoodsID where AllotDetailAllotID={0}", AllotID);
			return DbHelperSQL.Query(sql);
		}

		public bool DeleteAllorDetail(int AllotDetailAllotID)
		{
			string sql = string.Format("delete from dbo.GoodsAllotDetail  where AllotDetailAllotID={0}", AllotDetailAllotID);
			int ds = DbHelperSQL.ExecuteSql(sql);
			return ds > 0;
		}
	}
}
