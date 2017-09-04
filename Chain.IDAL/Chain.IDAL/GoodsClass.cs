using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class GoodsClass
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ClassID", "GoodsClass");
		}

		public bool Exists(int ClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from GoodsClass");
			strSql.Append(" where ClassID=@ClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.GoodsClass model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into GoodsClass(");
			strSql.Append("ClassName,ClassRemark,ParentID,CreateShopID)");
			strSql.Append(" values (");
			strSql.Append("@ClassName,@ClassRemark,@ParentID,@CreateShopID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassName", SqlDbType.VarChar, 50),
				new SqlParameter("@ClassRemark", SqlDbType.NVarChar, 100),
				new SqlParameter("@ParentID", SqlDbType.Int, 4),
				new SqlParameter("@CreateShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ClassName;
			parameters[1].Value = model.ClassRemark;
			parameters[2].Value = model.ParentID;
			parameters[3].Value = model.CreateShopID;
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

		public bool Update(Chain.Model.GoodsClass model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update GoodsClass set ");
			strSql.Append("ClassName=@ClassName,");
			strSql.Append("ClassRemark=@ClassRemark,");
			strSql.Append("ParentID=@ParentID,");
			strSql.Append("CreateShopID=@CreateShopID");
			strSql.Append(" where ClassID=@ClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassName", SqlDbType.VarChar, 50),
				new SqlParameter("@ClassRemark", SqlDbType.NVarChar, 100),
				new SqlParameter("@ParentID", SqlDbType.Int, 4),
				new SqlParameter("@CreateShopID", SqlDbType.Int, 4),
				new SqlParameter("@ClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ClassName;
			parameters[1].Value = model.ClassRemark;
			parameters[2].Value = model.ParentID;
			parameters[3].Value = model.CreateShopID;
			parameters[4].Value = model.ClassID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsClass ");
			strSql.Append(" where ClassID=@ClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string ClassIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsClass ");
			strSql.Append(" where ClassID in (" + ClassIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.GoodsClass GetModel(int ClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ClassID,ClassName,ClassRemark,ParentID,CreateShopID from GoodsClass ");
			strSql.Append(" where ClassID=@ClassID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassID;
			Chain.Model.GoodsClass model = new Chain.Model.GoodsClass();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.GoodsClass result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ClassID"] != null && ds.Tables[0].Rows[0]["ClassID"].ToString() != "")
				{
					model.ClassID = int.Parse(ds.Tables[0].Rows[0]["ClassID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ClassName"] != null && ds.Tables[0].Rows[0]["ClassName"].ToString() != "")
				{
					model.ClassName = ds.Tables[0].Rows[0]["ClassName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ClassRemark"] != null && ds.Tables[0].Rows[0]["ClassRemark"].ToString() != "")
				{
					model.ClassRemark = ds.Tables[0].Rows[0]["ClassRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ParentID"] != null && ds.Tables[0].Rows[0]["ParentID"].ToString() != "")
				{
					model.ParentID = int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CreateShopID"] != null && ds.Tables[0].Rows[0]["CreateShopID"].ToString() != "")
				{
					model.CreateShopID = int.Parse(ds.Tables[0].Rows[0]["CreateShopID"].ToString());
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
			strSql.Append("select ClassID,ClassName,ClassRemark,ParentID,CreateShopID ");
			strSql.Append(" FROM GoodsClass ");
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
			strSql.Append(" ClassID,ClassName,ClassRemark,ParentID,CreateShopID ");
			strSql.Append(" FROM GoodsClass ");
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
			strSql.Append("select count(1) FROM GoodsClass ");
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
				strSql.Append("order by T.ClassID desc");
			}
			strSql.Append(")AS Row, T.*  from GoodsClass T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public bool Exists(int classID, string ClassName, int CreateShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from GoodsClass");
			strSql.Append(" where ClassID not in (@ClassID) ");
			strSql.Append(" and (ClassName=@ClassName)");
			strSql.Append(" and CreateShopID=@CreateShopID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassID", SqlDbType.Int),
				new SqlParameter("@ClassName", SqlDbType.VarChar, 50),
				new SqlParameter("@CreateShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = classID;
			parameters[1].Value = ClassName;
			parameters[2].Value = CreateShopID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(string ClassName, int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from GoodsClass");
			strSql.Append(" where ClassName=@ClassName");
			strSql.Append(" and ShopID=@ShopID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassName", SqlDbType.VarChar, 50),
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassName;
			parameters[1].Value = ShopID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int DeleteByParentID(int ParentID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsClass ");
			strSql.Append(" where ParentID=@ParentID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ParentID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ParentID;
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

		public DataSet GetItem(int ClassID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ClassID,ClassName,ParentID,ClassRemark,ShopID from GoodsClass ");
			strSql.Append(" where ClassID=@ClassID ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ClassID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ClassID;
			return DbHelperSQL.Query(strSql.ToString(), parameters);
		}

		public DataSet GetListByShopID(int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select GoodsClass.ClassID,ClassName,ClassRemark,ParentID,GoodsClassAuthority.ShopID ");
			strSql.Append(" FROM GoodsClass INNER JOIN GoodsClassAuthority ");
			strSql.Append(" ON GoodsClass.ClassID = GoodsClassAuthority.ClassID ");
			if (ShopID > 0)
			{
				strSql.Append(" AND GoodsClassAuthority.ShopID = " + ShopID);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
	}
}
