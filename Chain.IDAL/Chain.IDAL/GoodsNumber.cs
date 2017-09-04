using Chain.DBUtility;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class GoodsNumber
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID", "GoodsNumber");
		}

		public bool Exists(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from GoodsNumber");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.GoodsNumber model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into GoodsNumber(");
			strSql.Append("GoodsID,ShopID,Number)");
			strSql.Append(" values (");
			strSql.Append("@GoodsID,@ShopID,@Number)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsID", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4),
				new SqlParameter("@Number", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.GoodsID;
			parameters[1].Value = model.ShopID;
			parameters[2].Value = model.Number;
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

		public bool Update(Chain.Model.GoodsNumber model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update GoodsNumber set ");
			strSql.Append("GoodsID=@GoodsID,");
			strSql.Append("ShopID=@ShopID,");
			strSql.Append("Number=@Number");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsID", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4),
				new SqlParameter("@Number", SqlDbType.Int, 4),
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.GoodsID;
			parameters[1].Value = model.ShopID;
			parameters[2].Value = model.Number;
			parameters[3].Value = model.ID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsNumber ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteNumber(int GoodsID, int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsNumber ");
			strSql.Append(" where GoodsID=@GoodsID");
			strSql.Append(" and ShopID=@ShopID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsID", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = GoodsID;
			parameters[1].Value = ShopID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteNumber(int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsNumber ");
			strSql.Append(" where");
			strSql.Append("  ShopID=@ShopID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ShopID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public DataSet GetShopIDListByGoods(int GoodsID)
		{
			string strSql = "select shopid from GoodsNumber where GoodsID = " + GoodsID;
			return DbHelperSQL.Query(strSql);
		}

		public bool DeleteList(string IDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsNumber ");
			strSql.Append(" where ID in (" + IDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.GoodsNumber GetModel(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ID,GoodsID,ShopID,Number from GoodsNumber ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			Chain.Model.GoodsNumber model = new Chain.Model.GoodsNumber();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.GoodsNumber result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
				{
					model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsID"] != null && ds.Tables[0].Rows[0]["GoodsID"].ToString() != "")
				{
					model.GoodsID = int.Parse(ds.Tables[0].Rows[0]["GoodsID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ShopID"] != null && ds.Tables[0].Rows[0]["ShopID"].ToString() != "")
				{
					model.ShopID = int.Parse(ds.Tables[0].Rows[0]["ShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Number"] != null && ds.Tables[0].Rows[0]["Number"].ToString() != "")
				{
					model.Number = int.Parse(ds.Tables[0].Rows[0]["Number"].ToString());
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
			strSql.Append("select ID,GoodsID,ShopID,Number ");
			strSql.Append(" FROM GoodsNumber ");
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
			strSql.Append(" ID,GoodsID,ShopID,Number ");
			strSql.Append(" FROM GoodsNumber ");
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
			strSql.Append("select count(1) FROM GoodsNumber ");
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from GoodsNumber T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int InsertGoodsNumber(int intGoodsID)
		{
			string strSql = " insert into GoodsNumber (GoodsID,ShopID,Number) select " + intGoodsID.ToString() + ", SysShop.ShopID, 0 from SysShop where SysShop.ShopID>0";
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		public int InsertGoodsNumber(int intGoodsID, int intShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into GoodsNumber(");
			strSql.Append("GoodsID,ShopID,Number)");
			strSql.Append("values(");
			strSql.Append("@GoodsID,@ShopID,0)");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@GoodsID", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			parameter[0].Value = intGoodsID;
			parameter[1].Value = intShopID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}

		public DataSet GetGoodsNumber(int intGoodsID, int intShopID)
		{
			string strSql = string.Concat(new object[]
			{
				" select * from V_GoodsNumber where ShopID=",
				intShopID,
				" and GoodsID=",
				intGoodsID
			});
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet CkeckInShopGoods(int intGoodsID, int intShopID)
		{
			string sql = string.Format("select * from  dbo.GoodsNumber  where GoodsID={0} and  ShopID={1}", intGoodsID, intShopID);
			return DbHelperSQL.Query(sql);
		}

		public DataSet GetGoodsNumber(string strGoodsCode, int intShopID)
		{
			string strSql = string.Concat(new object[]
			{
				" select * from V_GoodsNumber where ShopID=",
				intShopID,
				" and GoodsCode='",
				strGoodsCode,
				"'"
			});
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetGoodsNumberDelServe(string strGoodsCode, int intShopID)
		{
			string strSql = string.Concat(new object[]
			{
				" select * from V_GoodsNumber where GoodsType = 0 AND ShopID=",
				intShopID,
				" and GoodsCode='",
				strGoodsCode,
				"'"
			});
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int UpdataGoodsNumber(Chain.Model.GoodsNumber model)
		{
			string strSql = string.Concat(new object[]
			{
				" update GoodsNumber set Number =CAST(ISNULL(Number,0)AS MONEY)+CAST(",
				model.Number,
				" AS MONEY) where GoodsID =",
				model.GoodsID,
				" and ShopID=",
				model.ShopID
			});
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		public void InitShopData()
		{
			StringBuilder sqlBuilder = new StringBuilder(200);
			sqlBuilder.Append(" INSERT INTO GoodsNumber (GoodsID,ShopID,Number) ");
			sqlBuilder.Append(" select GTemp.GoodsID AS GID,STemp.ShopID AS SID,0 from ");
			sqlBuilder.Append(" (SELECT ShopID FROM SysShop WHERE ShopID NOT IN(SELECT ShopID FROM GoodsNumber)) AS STemp  , ");
			sqlBuilder.Append(" (select GoodsID FROM Goods  ) AS GTemp ");
			DbHelperSQL.ExecuteSql(sqlBuilder.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "V_GoodsNumber";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "GoodsCreateTime", "GoodsID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int UpdateGoodsNumber(int goodsID, decimal goodsNumber, int shopID)
		{
			string strSql = string.Format(" Update GoodsNumber set Number={0} where GoodsID={1} and ShopID={2}", goodsNumber, goodsID, shopID);
			return DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		public decimal GetNumber(int goodsID, int shopID)
		{
			string strSql = string.Format("select Number from GoodsNumber where GoodsID={0} and ShopID={1}", goodsID, shopID);
			object goodsNumber = DbHelperSQL.GetSingle(strSql);
			return (goodsNumber == null) ? 0m : decimal.Parse(goodsNumber.ToString());
		}

		public int GetGoodsCount(int ClassID)
		{
			string strSql = "select count(1) FROM Goods where GoodsClassID = {0}";
			strSql = string.Format(strSql, ClassID);
			return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
		}

		public int GetGoodsCount(int ClassID, int ShopID)
		{
			string strSql = "select count(1) FROM Goods,GoodsNumber where goods.GoodsID = GoodsNumber.GoodsID and goods.GoodsClassID = {0} and GoodsNumber.ShopID = {1}";
			strSql = string.Format(strSql, ClassID, ShopID);
			return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
		}

		public int SyncGoods(int GoodsID)
		{
			string strSql = "insert into GoodsNumber(GoodsID,ShopID,Number) \r\n                (select {0},ShopID,0 from SysShop where ShopID>0 and ShopID not in(SELECT ShopID FROM GoodsNumber where GoodsID = {0}))";
			strSql = string.Format(strSql, GoodsID);
			return DbHelperSQL.ExecuteSql(strSql);
		}

		public int SyncGoods(int GoodsID, int ShopID)
		{
			string strSql = "insert into GoodsNumber(GoodsID,ShopID,Number) \r\n                (select {0},ShopID,0 from SysShop where ShopID>0 and ShopID={1} and ShopID not in(SELECT ShopID FROM GoodsNumber where GoodsID = {0}))";
			strSql = string.Format(strSql, GoodsID, ShopID);
			return DbHelperSQL.ExecuteSql(strSql);
		}

		public int SyncGoods(int GoodsID, List<int> ShopList)
		{
			int result;
			if (ShopList.Count > 0)
			{
				string shopIDList = string.Empty;
				foreach (int item in ShopList)
				{
					shopIDList = shopIDList + item + ",";
				}
				shopIDList = shopIDList.Trim(new char[]
				{
					','
				});
				string strSql = "insert into GoodsNumber(GoodsID,ShopID,Number) \r\n                (select {0},ShopID,0 from SysShop where ShopID>0 and ShopID in({1}) and ShopID not in(SELECT ShopID FROM GoodsNumber where GoodsID = {0}))";
				strSql = string.Format(strSql, GoodsID, shopIDList);
				result = DbHelperSQL.ExecuteSql(strSql);
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public DataSet GetAllGoodsidByShopID(int ShopID)
		{
			string sql = string.Format("select GoodsID from GoodsNumber where ShopID={0}", ShopID);
			return DbHelperSQL.Query(sql);
		}

		public bool ChenkOutShopGoodsNumber(int OutShopID, int GoodsID, decimal OutNumber)
		{
			int OutShopGoodsNum = 0;
			string sql = string.Format("select Number from GoodsNumber where GoodsID={0} and ShopID={1}", GoodsID, OutShopID);
			DataSet ds = DbHelperSQL.Query(sql);
			if (ds.Tables[0].Rows.Count > 0)
			{
				OutShopGoodsNum = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
			}
			return OutShopGoodsNum >= OutNumber;
		}

		public bool UPdataOutShopGoodsNumber(int OutShopID, int GoodsID, decimal OutNumber)
		{
			string sql = string.Format("Update GoodsNumber set Number=Number-{0} where GoodsID={1} and ShopID={2}", OutNumber, GoodsID, OutShopID);
			return DbHelperSQL.ExecuteSql(sql) > 0;
		}

		public bool UPdataInShopGoodsNumber(int OutShopID, int GoodsID, int OutNumber)
		{
			string sql = string.Format("Update GoodsNumber set Number=Number+{0} where GoodsID={1} and ShopID={2}", OutNumber, GoodsID, OutShopID);
			return DbHelperSQL.ExecuteSql(sql) > 0;
		}
	}
}
