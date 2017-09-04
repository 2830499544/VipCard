using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class MicroWebsiteGoods
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("MicroGoodsID", "MicroWebsiteGoods");
		}

		public bool Exists(int MicroGoodsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MicroWebsiteGoods");
			strSql.Append(" where MicroGoodsID=@MicroGoodsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroGoodsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MicroGoodsID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(string strGoodsCode, int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MicroWebsiteGoods");
			strSql.Append(" where MicroGoodsCode=@MicroGoodsCode and MicroGoodsShopID=@MicroGoodsShopID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroGoodsCode", SqlDbType.NVarChar, 50),
				new SqlParameter("@MicroGoodsShopID", SqlDbType.Int)
			};
			parameters[0].Value = strGoodsCode;
			parameters[1].Value = ShopID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(int goodsID, string goodsCode, int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select count(1) from MicroWebsiteGoods");
			strSql.Append(" where MicroGoodsCode=@MicroGoodsCode and MicroGoodsID<>@MicroGoodsID and MicroGoodsShopID<>@MicroGoodsShopID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroGoodsCode", SqlDbType.NVarChar, 50),
				new SqlParameter("@MicroGoodsID", SqlDbType.Int),
				new SqlParameter("@MicroGoodsShopID", SqlDbType.Int)
			};
			parameters[0].Value = goodsCode;
			parameters[1].Value = goodsID;
			parameters[2].Value = ShopID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.MicroWebsiteGoods model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MicroWebsiteGoods(");
			strSql.Append("MicroGoodsCode,MicroGoodsClassID,MicroGoodsName,MicroSalePrice,MicroPrice,MicroPoint,MicroGoodsBidPrice,MicroGoodsRemark,MicroGoodsPicture,MicroGoodsCreateTime,MicroGoodsShopID)");
			strSql.Append(" values (");
			strSql.Append("@MicroGoodsCode,@MicroGoodsClassID,@MicroGoodsName,@MicroSalePrice,@MicroPrice,@MicroPoint,@MicroGoodsBidPrice,@MicroGoodsRemark,@MicroGoodsPicture,@MicroGoodsCreateTime,@MicroGoodsShopID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroGoodsCode", SqlDbType.NVarChar, 50),
				new SqlParameter("@MicroGoodsClassID", SqlDbType.Int, 4),
				new SqlParameter("@MicroGoodsName", SqlDbType.NVarChar, 200),
				new SqlParameter("@MicroSalePrice", SqlDbType.Money, 8),
				new SqlParameter("@MicroPrice", SqlDbType.Money, 8),
				new SqlParameter("@MicroPoint", SqlDbType.Int, 4),
				new SqlParameter("@MicroGoodsBidPrice", SqlDbType.Money, 8),
				new SqlParameter("@MicroGoodsRemark", SqlDbType.NVarChar, 2000),
				new SqlParameter("@MicroGoodsPicture", SqlDbType.NVarChar, 200),
				new SqlParameter("@MicroGoodsCreateTime", SqlDbType.DateTime),
				new SqlParameter("@MicroGoodsShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MicroGoodsCode;
			parameters[1].Value = model.MicroGoodsClassID;
			parameters[2].Value = model.MicroGoodsName;
			parameters[3].Value = model.MicroSalePrice;
			parameters[4].Value = model.MicroPrice;
			parameters[5].Value = model.MicroPoint;
			parameters[6].Value = model.MicroGoodsBidPrice;
			parameters[7].Value = model.MicroGoodsRemark;
			parameters[8].Value = model.MicroGoodsPicture;
			parameters[9].Value = model.MicroGoodsCreateTime;
			parameters[10].Value = model.MicroGoodsShopID;
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

		public int Update(Chain.Model.MicroWebsiteGoods model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MicroWebsiteGoods set ");
			strSql.Append("MicroGoodsCode=@MicroGoodsCode,");
			strSql.Append("MicroGoodsClassID=@MicroGoodsClassID,");
			strSql.Append("MicroGoodsName=@MicroGoodsName,");
			strSql.Append("MicroSalePrice=@MicroSalePrice,");
			strSql.Append("MicroPrice=@MicroPrice,");
			strSql.Append("MicroPoint=@MicroPoint,");
			strSql.Append("MicroGoodsBidPrice=@MicroGoodsBidPrice,");
			strSql.Append("MicroGoodsRemark=@MicroGoodsRemark,");
			strSql.Append("MicroGoodsPicture=@MicroGoodsPicture,");
			strSql.Append("MicroGoodsCreateTime=@MicroGoodsCreateTime,");
			strSql.Append("MicroGoodsShopID=@MicroGoodsShopID");
			strSql.Append(" where MicroGoodsID=@MicroGoodsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroGoodsCode", SqlDbType.NVarChar, 50),
				new SqlParameter("@MicroGoodsClassID", SqlDbType.Int, 4),
				new SqlParameter("@MicroGoodsName", SqlDbType.NVarChar, 200),
				new SqlParameter("@MicroSalePrice", SqlDbType.Money, 8),
				new SqlParameter("@MicroPrice", SqlDbType.Money, 8),
				new SqlParameter("@MicroPoint", SqlDbType.Int, 4),
				new SqlParameter("@MicroGoodsBidPrice", SqlDbType.Money, 8),
				new SqlParameter("@MicroGoodsRemark", SqlDbType.NVarChar, 2000),
				new SqlParameter("@MicroGoodsPicture", SqlDbType.NVarChar, 200),
				new SqlParameter("@MicroGoodsCreateTime", SqlDbType.DateTime),
				new SqlParameter("@MicroGoodsShopID", SqlDbType.Int, 4),
				new SqlParameter("@MicroGoodsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MicroGoodsCode;
			parameters[1].Value = model.MicroGoodsClassID;
			parameters[2].Value = model.MicroGoodsName;
			parameters[3].Value = model.MicroSalePrice;
			parameters[4].Value = model.MicroPrice;
			parameters[5].Value = model.MicroPoint;
			parameters[6].Value = model.MicroGoodsBidPrice;
			parameters[7].Value = model.MicroGoodsRemark;
			parameters[8].Value = model.MicroGoodsPicture;
			parameters[9].Value = model.MicroGoodsCreateTime;
			parameters[10].Value = model.MicroGoodsShopID;
			parameters[11].Value = model.MicroGoodsID;
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

		public bool Delete(int MicroGoodsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MicroWebsiteGoods ");
			strSql.Append(" where MicroGoodsID=@MicroGoodsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroGoodsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MicroGoodsID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string MicroGoodsIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MicroWebsiteGoods ");
			strSql.Append(" where MicroGoodsID in (" + MicroGoodsIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MicroWebsiteGoods GetModel(int MicroGoodsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 MicroGoodsID,MicroGoodsCode,MicroGoodsClassID,MicroGoodsName,MicroSalePrice,MicroPrice,MicroPoint,MicroGoodsBidPrice,MicroGoodsRemark,MicroGoodsPicture,MicroGoodsCreateTime,MicroGoodsShopID from MicroWebsiteGoods ");
			strSql.Append(" where MicroGoodsID=@MicroGoodsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroGoodsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MicroGoodsID;
			Chain.Model.MicroWebsiteGoods model = new Chain.Model.MicroWebsiteGoods();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MicroWebsiteGoods result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["MicroGoodsID"] != null && ds.Tables[0].Rows[0]["MicroGoodsID"].ToString() != "")
				{
					model.MicroGoodsID = int.Parse(ds.Tables[0].Rows[0]["MicroGoodsID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroGoodsCode"] != null && ds.Tables[0].Rows[0]["MicroGoodsCode"].ToString() != "")
				{
					model.MicroGoodsCode = ds.Tables[0].Rows[0]["MicroGoodsCode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MicroGoodsClassID"] != null && ds.Tables[0].Rows[0]["MicroGoodsClassID"].ToString() != "")
				{
					model.MicroGoodsClassID = int.Parse(ds.Tables[0].Rows[0]["MicroGoodsClassID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroGoodsName"] != null && ds.Tables[0].Rows[0]["MicroGoodsName"].ToString() != "")
				{
					model.MicroGoodsName = ds.Tables[0].Rows[0]["MicroGoodsName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MicroSalePrice"] != null && ds.Tables[0].Rows[0]["MicroSalePrice"].ToString() != "")
				{
					model.MicroSalePrice = decimal.Parse(ds.Tables[0].Rows[0]["MicroSalePrice"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroPrice"] != null && ds.Tables[0].Rows[0]["MicroPrice"].ToString() != "")
				{
					model.MicroPrice = decimal.Parse(ds.Tables[0].Rows[0]["MicroPrice"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroPoint"] != null && ds.Tables[0].Rows[0]["MicroPoint"].ToString() != "")
				{
					model.MicroPoint = int.Parse(ds.Tables[0].Rows[0]["MicroPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroGoodsBidPrice"] != null && ds.Tables[0].Rows[0]["MicroGoodsBidPrice"].ToString() != "")
				{
					model.MicroGoodsBidPrice = decimal.Parse(ds.Tables[0].Rows[0]["MicroGoodsBidPrice"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroGoodsRemark"] != null && ds.Tables[0].Rows[0]["MicroGoodsRemark"].ToString() != "")
				{
					model.MicroGoodsRemark = ds.Tables[0].Rows[0]["MicroGoodsRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MicroGoodsPicture"] != null && ds.Tables[0].Rows[0]["MicroGoodsPicture"].ToString() != "")
				{
					model.MicroGoodsPicture = ds.Tables[0].Rows[0]["MicroGoodsPicture"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MicroGoodsCreateTime"] != null && ds.Tables[0].Rows[0]["MicroGoodsCreateTime"].ToString() != "")
				{
					model.MicroGoodsCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["MicroGoodsCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroGoodsShopID"] != null && ds.Tables[0].Rows[0]["MicroGoodsShopID"].ToString() != "")
				{
					model.MicroGoodsShopID = int.Parse(ds.Tables[0].Rows[0]["MicroGoodsShopID"].ToString());
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
			strSql.Append("select MicroGoodsID,MicroGoodsCode,MicroGoodsClassID,MicroGoodsName,MicroSalePrice,MicroPrice,MicroPoint,MicroGoodsBidPrice,MicroGoodsRemark,MicroGoodsPicture,MicroGoodsCreateTime,MicroGoodsShopID ");
			strSql.Append(" FROM MicroWebsiteGoods ");
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
			strSql.Append(" MicroGoodsID,MicroGoodsCode,MicroGoodsClassID,MicroGoodsName,MicroSalePrice,MicroPrice,MicroPoint,MicroGoodsBidPrice,MicroGoodsRemark,MicroGoodsPicture,MicroGoodsCreateTime,MicroGoodsShopID ");
			strSql.Append(" FROM MicroWebsiteGoods ");
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
			strSql.Append("select count(1) FROM MicroWebsiteGoods ");
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
				strSql.Append("order by T.MicroGoodsID desc");
			}
			strSql.Append(")AS Row, T.*  from MicroWebsiteGoods T ");
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
			string tableName = "MicroWebsiteGoods,MicroWebsiteGoodsClass";
			string[] columns = new string[]
			{
				"MicroWebsiteGoods.*,MicroGoodsClassName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MicroGoodsCreateTime", "MicroGoodsID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
