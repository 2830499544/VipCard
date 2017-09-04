using Chain.DBUtility;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class GoodsClassAuthority
	{
		public bool Exists(int ClassID, int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from Goods");
			strSql.Append(" where ClassID=@ClassID and ShopID=@ShopID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassID", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassID;
			parameters[1].Value = ShopID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.GoodsClassAuthority model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into GoodsClassAuthority(");
			strSql.Append("ClassID,ShopID)");
			strSql.Append(" values (");
			strSql.Append("@ClassID,@ShopID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassID", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ClassID;
			parameters[1].Value = model.ShopID;
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

		public bool Update(Chain.Model.GoodsClassAuthority model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update GoodsClassAuthority set ");
			strSql.Append("ClassID=@ClassID,");
			strSql.Append("ShopID=@ShopID");
			strSql.Append(" where ClassAuthorityID=@ClassAuthorityID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassID", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4),
				new SqlParameter("@ClassAuthorityID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ClassID;
			parameters[1].Value = model.ShopID;
			parameters[2].Value = model.ClassAuthorityID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteByWhere(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsClassAuthority ");
			strSql.Append(" where " + strWhere);
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public bool Delete(int ClassAuthorityID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsClassAuthority ");
			strSql.Append(" where ClassAuthorityID=@ClassAuthorityID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassAuthorityID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassAuthorityID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string ClassAuthorityIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsClassAuthority ");
			strSql.Append(" where ClassAuthorityID in (" + ClassAuthorityIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.GoodsClassAuthority GetModel(int ClassAuthorityID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ClassAuthorityID,ClassID,ShopID from GoodsClassAuthority ");
			strSql.Append(" where ClassAuthorityID=@ClassAuthorityID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassAuthorityID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassAuthorityID;
			Chain.Model.GoodsClassAuthority model = new Chain.Model.GoodsClassAuthority();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.GoodsClassAuthority result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ClassAuthorityID"] != null && ds.Tables[0].Rows[0]["ClassAuthorityID"].ToString() != "")
				{
					model.ClassAuthorityID = int.Parse(ds.Tables[0].Rows[0]["ClassAuthorityID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ClassID"] != null && ds.Tables[0].Rows[0]["ClassID"].ToString() != "")
				{
					model.ClassID = int.Parse(ds.Tables[0].Rows[0]["ClassID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ShopID"] != null && ds.Tables[0].Rows[0]["ShopID"].ToString() != "")
				{
					model.ShopID = int.Parse(ds.Tables[0].Rows[0]["ShopID"].ToString());
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
			strSql.Append("select ClassAuthorityID,ClassID,ShopID ");
			strSql.Append(" FROM GoodsClassAuthority ");
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
			strSql.Append(" ClassAuthorityID,ClassID,ShopID ");
			strSql.Append(" FROM GoodsClassAuthority ");
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
			strSql.Append("select count(1) FROM GoodsClassAuthority ");
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
				strSql.Append("order by T.ClassAuthorityID desc");
			}
			strSql.Append(")AS Row, T.*  from GoodsClassAuthority T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int SyncGoodsClass(int ClassID)
		{
			string strSql = "insert into GoodsClassAuthority(ClassID,ShopID) \r\n                (select {0},ShopID from SysShop where ShopID>0 and ShopID not in(SELECT ShopID FROM GoodsClassAuthority where ClassID = {0}))";
			strSql = string.Format(strSql, ClassID);
			int count = DbHelperSQL.ExecuteSql(strSql);
			int result;
			if (count > 0)
			{
				string strSql2 = "INSERT INTO GoodsClassDiscount (GoodsClassID,MemLevelID,DiscountShopID,ClassDiscountPercent,ClassPointPercent) \r\n                    (select {0},LevelID,ShopID,1,1 from SysShop,MemLevel where ShopID>0 and ShopID not in (SELECT DiscountShopID FROM GoodsClassDiscount where GoodsClassID = {0}))";
				strSql2 = string.Format(strSql2, ClassID);
				result = DbHelperSQL.ExecuteSql(strSql2);
			}
			else
			{
				result = count;
			}
			return result;
		}

		public int SyncGoodsClass(int ClassID, int ShopID)
		{
			string strSql = "insert into GoodsClassAuthority(ClassID,ShopID) \r\n                (select {0},ShopID from SysShop where ShopID>0 and ShopID = {1} and ShopID not in(SELECT ShopID FROM GoodsClassAuthority where ClassID = {0}))";
			strSql = string.Format(strSql, ClassID, ShopID);
			int count = DbHelperSQL.ExecuteSql(strSql);
			int result;
			if (count > 0)
			{
				string strSql2 = "INSERT INTO GoodsClassDiscount(GoodsClassID,MemLevelID,DiscountShopID,ClassDiscountPercent,ClassPointPercent) \r\n                    (select {0},LevelID,ShopID,1,0.01 from SysShop,MemLevel where ShopID>0 and ShopID = {1} and ShopID not in (SELECT DiscountShopID FROM GoodsClassDiscount where GoodsClassID = {0}))";
				strSql2 = string.Format(strSql2, ClassID, ShopID);
				result = DbHelperSQL.ExecuteSql(strSql2);
			}
			else
			{
				result = count;
			}
			return result;
		}

		public int SyncGoodsClass(int ClassID, List<int> ShopList)
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
				string strSql = "insert into GoodsClassAuthority(ClassID,ShopID) \r\n                (select {0},ShopID from SysShop where ShopID>0 and ShopID in ({1}) and ShopID not in(SELECT ShopID FROM GoodsClassAuthority where ClassID = {0}))";
				strSql = string.Format(strSql, ClassID, shopIDList);
				int count = DbHelperSQL.ExecuteSql(strSql);
				if (count > 0)
				{
					string strSql2 = "INSERT INTO GoodsClassDiscount(GoodsClassID,MemLevelID,DiscountShopID,ClassDiscountPercent,ClassPointPercent) \r\n                    (select {0},LevelID,ShopID,1,0.01 from SysShop,MemLevel where ShopID>0 and ShopID in ({1}) and ShopID not in (SELECT DiscountShopID FROM GoodsClassDiscount where GoodsClassID = {0}))";
					strSql2 = string.Format(strSql2, ClassID, shopIDList);
					result = DbHelperSQL.ExecuteSql(strSql2);
				}
				else
				{
					result = count;
				}
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public DataSet GetShopIDListByClass(int ClassID)
		{
			string strSql = "select shopid from GoodsClassAuthority where ClassID = " + ClassID;
			return DbHelperSQL.Query(strSql);
		}

		public int DeleteAuthority(int ClassID, int ShopID)
		{
			string strSql = "delete from GoodsClassAuthority where ClassID = {0} and ShopID = {1}";
			strSql = string.Format(strSql, ClassID, ShopID);
			return DbHelperSQL.ExecuteSql(strSql);
		}

		public int GetClassCountByParentID(int ParentID, int ShopID)
		{
			string strSql = "select count(0) from GoodsClass,GoodsClassAuthority \r\n                where GoodsClass.ClassID = GoodsClassAuthority.ClassID and ParentID = {0} and GoodsClassAuthority.ShopID = {1}";
			strSql = string.Format(strSql, ParentID, ShopID);
			return int.Parse(DbHelperSQL.GetSingle(strSql).ToString());
		}

		public int GetClassCountByParentID(int ParentID)
		{
			string strSql = "select count(0) from GoodsClass where ParentID = {0}";
			strSql = string.Format(strSql, ParentID);
			return int.Parse(DbHelperSQL.GetSingle(strSql).ToString());
		}
	}
}
