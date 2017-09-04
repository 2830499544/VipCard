using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class PointGift
	{
		public int GetStockNumber(int GiftID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select GiftStockNumber from PointGift ");
			strSql.Append(" where GiftID=@GiftID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GiftID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GiftID;
			object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
			int result;
			if (obj != null)
			{
				result = int.Parse(obj.ToString());
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("GiftID", "PointGift");
		}

		public bool ExistsAdd(string GiftName, int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from PointGift");
			strSql.Append(" where GiftName=@GiftName and GiftShopID=@GiftShopID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GiftName", SqlDbType.NVarChar, 50),
				new SqlParameter("@GiftShopID", SqlDbType.Int)
			};
			parameters[0].Value = GiftName;
			parameters[1].Value = ShopID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(string GiftName, int GiftID, int shopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from PointGift");
			strSql.Append(" where GiftName=@GiftName and GiftShopID=@GiftShopID and GiftID<>@GiftID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GiftName", SqlDbType.NVarChar, 50),
				new SqlParameter("@GiftShopID", SqlDbType.Int),
				new SqlParameter("@GiftID", SqlDbType.Int)
			};
			parameters[0].Value = GiftName;
			parameters[1].Value = shopID;
			parameters[2].Value = GiftID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.PointGift model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into PointGift(");
			strSql.Append("GiftName,GiftCode,GiftClassID,GiftPhoto,GiftExchangePoint,GiftStockNumber,GiftExchangeNumber,GiftShopID,GiftRemark)");
			strSql.Append(" values (");
			strSql.Append("@GiftName,@GiftCode,@GiftClassID,@GiftPhoto,@GiftExchangePoint,@GiftStockNumber,@GiftExchangeNumber,@GiftShopID,@GiftRemark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GiftName", SqlDbType.NVarChar, 50),
				new SqlParameter("@GiftCode", SqlDbType.VarChar, 50),
				new SqlParameter("@GiftClassID", SqlDbType.Int, 4),
				new SqlParameter("@GiftPhoto", SqlDbType.NVarChar, 500),
				new SqlParameter("@GiftExchangePoint", SqlDbType.Int, 4),
				new SqlParameter("@GiftStockNumber", SqlDbType.Int, 4),
				new SqlParameter("@GiftExchangeNumber", SqlDbType.Int, 4),
				new SqlParameter("@GiftShopID", SqlDbType.Int, 4),
				new SqlParameter("@GiftRemark", SqlDbType.NVarChar, 500)
			};
			parameters[0].Value = model.GiftName;
			parameters[1].Value = model.GiftCode;
			parameters[2].Value = model.GiftClassID;
			parameters[3].Value = model.GiftPhoto;
			parameters[4].Value = model.GiftExchangePoint;
			parameters[5].Value = model.GiftStockNumber;
			parameters[6].Value = model.GiftExchangeNumber;
			parameters[7].Value = model.GiftShopID;
			parameters[8].Value = model.GiftRemark;
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

		public int Update(Chain.Model.PointGift model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update PointGift set ");
			strSql.Append("GiftName=@GiftName,");
			strSql.Append("GiftCode=@GiftCode,");
			strSql.Append("GiftClassID=@GiftClassID,");
			strSql.Append("GiftPhoto=@GiftPhoto,");
			strSql.Append("GiftExchangePoint=@GiftExchangePoint,");
			strSql.Append("GiftStockNumber=@GiftStockNumber,");
			strSql.Append("GiftExchangeNumber=@GiftExchangeNumber,");
			strSql.Append("GiftShopID=@GiftShopID,");
			strSql.Append("GiftRemark=@GiftRemark");
			strSql.Append(" where GiftID=@GiftID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GiftName", SqlDbType.NVarChar, 50),
				new SqlParameter("@GiftCode", SqlDbType.VarChar, 50),
				new SqlParameter("@GiftClassID", SqlDbType.Int, 4),
				new SqlParameter("@GiftPhoto", SqlDbType.NVarChar, 500),
				new SqlParameter("@GiftExchangePoint", SqlDbType.Int, 4),
				new SqlParameter("@GiftStockNumber", SqlDbType.Int, 4),
				new SqlParameter("@GiftExchangeNumber", SqlDbType.Int, 4),
				new SqlParameter("@GiftShopID", SqlDbType.Int, 4),
				new SqlParameter("@GiftID", SqlDbType.Int, 4),
				new SqlParameter("@GiftRemark", SqlDbType.NVarChar, 500)
			};
			parameters[0].Value = model.GiftName;
			parameters[1].Value = model.GiftCode;
			parameters[2].Value = model.GiftClassID;
			parameters[3].Value = model.GiftPhoto;
			parameters[4].Value = model.GiftExchangePoint;
			parameters[5].Value = model.GiftStockNumber;
			parameters[6].Value = model.GiftExchangeNumber;
			parameters[7].Value = model.GiftShopID;
			parameters[8].Value = model.GiftID;
			parameters[9].Value = model.GiftRemark;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			int result;
			if (rows > 0)
			{
				result = 1;
			}
			else
			{
				result = -3;
			}
			return result;
		}

		public bool Delete(int GiftID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from PointGift ");
			strSql.Append(" where GiftID=@GiftID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GiftID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GiftID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string GiftIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from PointGift ");
			strSql.Append(" where GiftID in (" + GiftIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.PointGift GetModel(int GiftID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 GiftID,GiftName,GiftCode,GiftClassID,GiftPhoto,GiftExchangePoint,GiftStockNumber,GiftExchangeNumber,GiftShopID,GiftRemark from PointGift ");
			strSql.Append(" where GiftID=@GiftID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GiftID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GiftID;
			Chain.Model.PointGift model = new Chain.Model.PointGift();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.PointGift result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["GiftID"] != null && ds.Tables[0].Rows[0]["GiftID"].ToString() != "")
				{
					model.GiftID = int.Parse(ds.Tables[0].Rows[0]["GiftID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GiftName"] != null && ds.Tables[0].Rows[0]["GiftName"].ToString() != "")
				{
					model.GiftName = ds.Tables[0].Rows[0]["GiftName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GiftCode"] != null && ds.Tables[0].Rows[0]["GiftCode"].ToString() != "")
				{
					model.GiftCode = ds.Tables[0].Rows[0]["GiftCode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GiftClassID"] != null && ds.Tables[0].Rows[0]["GiftClassID"].ToString() != "")
				{
					model.GiftClassID = int.Parse(ds.Tables[0].Rows[0]["GiftClassID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GiftPhoto"] != null && ds.Tables[0].Rows[0]["GiftPhoto"].ToString() != "")
				{
					model.GiftPhoto = ds.Tables[0].Rows[0]["GiftPhoto"].ToString();
				}
				if (ds.Tables[0].Rows[0]["GiftExchangePoint"] != null && ds.Tables[0].Rows[0]["GiftExchangePoint"].ToString() != "")
				{
					model.GiftExchangePoint = int.Parse(ds.Tables[0].Rows[0]["GiftExchangePoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GiftStockNumber"] != null && ds.Tables[0].Rows[0]["GiftStockNumber"].ToString() != "")
				{
					model.GiftStockNumber = int.Parse(ds.Tables[0].Rows[0]["GiftStockNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GiftExchangeNumber"] != null && ds.Tables[0].Rows[0]["GiftExchangeNumber"].ToString() != "")
				{
					model.GiftExchangeNumber = int.Parse(ds.Tables[0].Rows[0]["GiftExchangeNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GiftShopID"] != null && ds.Tables[0].Rows[0]["GiftShopID"].ToString() != "")
				{
					model.GiftShopID = int.Parse(ds.Tables[0].Rows[0]["GiftShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GiftRemark"] != null && ds.Tables[0].Rows[0]["GiftRemark"].ToString() != "")
				{
					model.GiftRemark = ds.Tables[0].Rows[0]["GiftRemark"].ToString();
				}
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public DataSet GetItemModel(int GiftID)
		{
			string str_sql = "select * from PointGift where GiftID=" + GiftID;
			return DbHelperSQL.Query(str_sql);
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select GiftID,GiftName,GiftCode,GiftClassID,GiftPhoto,GiftExchangePoint,GiftStockNumber,GiftExchangeNumber,GiftShopID,GiftRemark ");
			strSql.Append(" FROM PointGift ");
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
			strSql.Append(" GiftID,GiftName,GiftCode,GiftClassID,GiftPhoto,GiftExchangePoint,GiftStockNumber,GiftExchangeNumber,GiftShopID,GiftRemark ");
			strSql.Append(" FROM PointGift ");
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
			strSql.Append("select count(1) FROM PointGift ");
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
				strSql.Append("order by T.GiftID desc");
			}
			strSql.Append(")AS Row, T.*  from PointGift T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, bool IsAsc, string[] strWhere, out int resCount)
		{
			string tableName = "PointGift,SysShop,GiftClass";
			string[] columns = new string[]
			{
				"PointGift.*,SysShop.ShopName,GiftClassName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "GiftExchangePoint", "GiftID", IsAsc, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetListS(int PageSize, int PageIndex, string[] strWhere, out int resCount)
		{
			string tableName = "PointGift";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "GiftID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int UpdateGiftNumber(int giftID, int number)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update PointGift set GiftStockNumber=GiftStockNumber-@GiftExchangeNumber,");
			strSql.Append("GiftExchangeNumber=GiftExchangeNumber+@GiftExchangeNumber ");
			strSql.Append(" where GiftID=@GiftID");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@GiftExchangeNumber", SqlDbType.Int, 4),
				new SqlParameter("@GiftID", SqlDbType.Int, 4)
			};
			parameter[0].Value = number;
			parameter[1].Value = giftID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}
	}
}
