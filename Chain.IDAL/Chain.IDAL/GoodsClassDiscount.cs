using Chain.DBUtility;
using Chain.Model;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class GoodsClassDiscount
	{
		public bool AddList(ArrayList sqlArray)
		{
			return DbHelperSQL.ExecuteSqlTran(sqlArray);
		}

		public void InitGoodsLevelDiscountByGoodsClassID(int goodsClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into GoodsClassDiscount(");
			strSql.Append("GoodsClassID,MemLevelID,ClassDiscountPercent,ClassPointPercent)");
			strSql.AppendFormat(" select {0},{1},{2},{3} ", new object[]
			{
				goodsClassID,
				"LevelID",
				"LevelDiscountPercent",
				"LevelPointPercent"
			});
			strSql.Append("from MemLevel");
			DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		public void InitGoodsLevelDiscountByMemLevelID(MemLevel mdMemLevel)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into GoodsClassDiscount(");
			strSql.Append("GoodsClassID,MemLevelID,ClassDiscountPercent,ClassPointPercent)");
			strSql.AppendFormat(" select {0},{1},{2},{3} ", new object[]
			{
				"ClassID",
				mdMemLevel.LevelID,
				mdMemLevel.LevelDiscountPercent,
				mdMemLevel.LevelPointPercent
			});
			strSql.Append("from GoodsClass");
			DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		public void DelGoodsClassDiscountByGoodsClassID(int goodsClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsClassDiscount where GoodsClassID in");
			strSql.Append("(");
			strSql.Append("select ClassID from GoodsClass where ParentID=@ParentID");
			strSql.Append(")");
			strSql.Append("delete from GoodsClassDiscount where GoodsClassID=@ParentID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ParentID", SqlDbType.Int, 4)
			};
			parameters[0].Value = goodsClassID;
			DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public void DelGoodsClassDiscountByMemLevelID(int memLevelID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsClassDiscount where MemLevelID=@MemLevelID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemLevelID", SqlDbType.Int, 4)
			};
			parameters[0].Value = memLevelID;
			DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public DataSet GetListByClassID(int classID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select LevelID,LevelName,LevelPoint,ClassDiscountPercent,ClassPointPercent,LevellLock,ClassName,ClassDiscountID ");
			strSql.Append("from MemLevel,GoodsClassDiscount,GoodsClass ");
			strSql.Append("where MemLevel.LevelID=GoodsClassDiscount.MemLevelID ");
			strSql.Append("and GoodsClassDiscount.GoodsClassID=GoodsClass.ClassID ");
			strSql.Append("and ClassID=@ClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = classID;
			return DbHelperSQL.Query(strSql.ToString(), parameters);
		}

		public DataSet GetListByClassDiscountID(int classDiscountID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select LevelID,LevelName,LevelPoint,ClassDiscountPercent,ClassPointPercent,LevellLock,ClassName,ClassDiscountID ");
			strSql.Append("from MemLevel,GoodsClassDiscount,GoodsClass ");
			strSql.Append("where MemLevel.LevelID=GoodsClassDiscount.MemLevelID ");
			strSql.Append("and GoodsClassDiscount.GoodsClassID=GoodsClass.ClassID ");
			strSql.Append("and ClassDiscountID=@ClassDiscountID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassDiscountID", SqlDbType.Int, 4)
			};
			parameters[0].Value = classDiscountID;
			return DbHelperSQL.Query(strSql.ToString(), parameters);
		}

		public DataSet GetList(int MemLevelID, int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ClassDiscountID,GoodsClassID,MemLevelID,DiscountShopID,ClassDiscountPercent,ClassPointPercent ");
			strSql.Append(" FROM GoodsClassDiscount ");
			strSql.AppendFormat(" where MemLevelID = {0} and DiscountShopID = {1}", MemLevelID, ShopID);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int DeleteDiscount(int ClassID, int ShopID)
		{
			string strSql = "delete from GoodsClassDiscount where GoodsClassID = {0} and DiscountShopID ={1}";
			strSql = string.Format(strSql, ClassID, ShopID);
			return DbHelperSQL.ExecuteSql(strSql);
		}

		public int DeleteDiscountByShopID(int ShopID)
		{
			string strSql = "delete from GoodsClassDiscount where  DiscountShopID ={0}";
			strSql = string.Format(strSql, ShopID);
			return DbHelperSQL.ExecuteSql(strSql);
		}

		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ClassDiscountID", "GoodsClassDiscount");
		}

		public bool Exists(int ClassDiscountID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from GoodsClassDiscount");
			strSql.Append(" where ClassDiscountID=@ClassDiscountID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassDiscountID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassDiscountID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.GoodsClassDiscount model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into GoodsClassDiscount(");
			strSql.Append("GoodsClassID,MemLevelID,DiscountShopID,ClassDiscountPercent,ClassPointPercent)");
			strSql.Append(" values (");
			strSql.Append("@GoodsClassID,@MemLevelID,@DiscountShopID,@ClassDiscountPercent,@ClassPointPercent)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsClassID", SqlDbType.Int, 4),
				new SqlParameter("@MemLevelID", SqlDbType.Int, 4),
				new SqlParameter("@DiscountShopID", SqlDbType.Int, 4),
				new SqlParameter("@ClassDiscountPercent", SqlDbType.Float, 8),
				new SqlParameter("@ClassPointPercent", SqlDbType.Float, 8)
			};
			parameters[0].Value = model.GoodsClassID;
			parameters[1].Value = model.MemLevelID;
			parameters[2].Value = model.DiscountShopID;
			parameters[3].Value = model.ClassDiscountPercent;
			parameters[4].Value = model.ClassPointPercent;
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

		public bool Update(Chain.Model.GoodsClassDiscount model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update GoodsClassDiscount set ");
			strSql.Append("GoodsClassID=@GoodsClassID,");
			strSql.Append("MemLevelID=@MemLevelID,");
			strSql.Append("DiscountShopID=@DiscountShopID,");
			strSql.Append("ClassDiscountPercent=@ClassDiscountPercent,");
			strSql.Append("ClassPointPercent=@ClassPointPercent");
			strSql.Append(" where ClassDiscountID=@ClassDiscountID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@GoodsClassID", SqlDbType.Int, 4),
				new SqlParameter("@MemLevelID", SqlDbType.Int, 4),
				new SqlParameter("@DiscountShopID", SqlDbType.Int, 4),
				new SqlParameter("@ClassDiscountPercent", SqlDbType.Float, 8),
				new SqlParameter("@ClassPointPercent", SqlDbType.Float, 8),
				new SqlParameter("@ClassDiscountID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.GoodsClassID;
			parameters[1].Value = model.MemLevelID;
			parameters[2].Value = model.DiscountShopID;
			parameters[3].Value = model.ClassDiscountPercent;
			parameters[4].Value = model.ClassPointPercent;
			parameters[5].Value = model.ClassDiscountID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ClassDiscountID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsClassDiscount ");
			strSql.Append(" where ClassDiscountID=@ClassDiscountID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassDiscountID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassDiscountID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string ClassDiscountIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsClassDiscount ");
			strSql.Append(" where ClassDiscountID in (" + ClassDiscountIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.GoodsClassDiscount GetModel(int ClassDiscountID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ClassDiscountID,GoodsClassID,MemLevelID,DiscountShopID,ClassDiscountPercent,ClassPointPercent from GoodsClassDiscount ");
			strSql.Append(" where ClassDiscountID=@ClassDiscountID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassDiscountID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassDiscountID;
			Chain.Model.GoodsClassDiscount model = new Chain.Model.GoodsClassDiscount();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.GoodsClassDiscount result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ClassDiscountID"] != null && ds.Tables[0].Rows[0]["ClassDiscountID"].ToString() != "")
				{
					model.ClassDiscountID = int.Parse(ds.Tables[0].Rows[0]["ClassDiscountID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GoodsClassID"] != null && ds.Tables[0].Rows[0]["GoodsClassID"].ToString() != "")
				{
					model.GoodsClassID = int.Parse(ds.Tables[0].Rows[0]["GoodsClassID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemLevelID"] != null && ds.Tables[0].Rows[0]["MemLevelID"].ToString() != "")
				{
					model.MemLevelID = int.Parse(ds.Tables[0].Rows[0]["MemLevelID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DiscountShopID"] != null && ds.Tables[0].Rows[0]["DiscountShopID"].ToString() != "")
				{
					model.DiscountShopID = int.Parse(ds.Tables[0].Rows[0]["DiscountShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ClassDiscountPercent"] != null && ds.Tables[0].Rows[0]["ClassDiscountPercent"].ToString() != "")
				{
					model.ClassDiscountPercent = decimal.Parse(ds.Tables[0].Rows[0]["ClassDiscountPercent"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ClassPointPercent"] != null && ds.Tables[0].Rows[0]["ClassPointPercent"].ToString() != "")
				{
					model.ClassPointPercent = decimal.Parse(ds.Tables[0].Rows[0]["ClassPointPercent"].ToString());
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
			strSql.Append("select ClassDiscountID,GoodsClassID,MemLevelID,DiscountShopID,ClassDiscountPercent,ClassPointPercent ");
			strSql.Append(" FROM GoodsClassDiscount ");
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
			strSql.Append(" ClassDiscountID,GoodsClassID,MemLevelID,DiscountShopID,ClassDiscountPercent,ClassPointPercent ");
			strSql.Append(" FROM GoodsClassDiscount ");
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
			strSql.Append("select count(1) FROM GoodsClassDiscount ");
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
				strSql.Append("order by T.ClassDiscountID desc");
			}
			strSql.Append(")AS Row, T.*  from GoodsClassDiscount T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}
	}
}
