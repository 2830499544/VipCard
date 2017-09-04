using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class Promotions
	{
		public bool Exists(int PromotionsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from Promotions");
			strSql.Append(" where PromotionsID=@PromotionsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PromotionsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = PromotionsID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.Promotions model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Promotions(");
			strSql.Append("ShopList,CreateUserID,PromotionsRemark,PromotionsDesc,PromotionsPhoto,PromotionsTitle,PromotionsStart,PromotionsEnd,PromotionsMemLevel,PromotionsType,PromotionsTime)");
			strSql.Append(" values (");
			strSql.Append("@ShopList,@CreateUserID,@PromotionsRemark,@PromotionsDesc,@PromotionsPhoto,@PromotionsTitle,@PromotionsStart,@PromotionsEnd,@PromotionsMemLevel,@PromotionsType,@PromotionsTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PromotionsTitle", SqlDbType.NVarChar, 1000),
				new SqlParameter("@PromotionsStart", SqlDbType.DateTime),
				new SqlParameter("@PromotionsEnd", SqlDbType.DateTime),
				new SqlParameter("@PromotionsMemLevel", SqlDbType.Int, 4),
				new SqlParameter("@PromotionsType", SqlDbType.Int, 4),
				new SqlParameter("@PromotionsTime", SqlDbType.DateTime),
				new SqlParameter("@PromotionsRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@PromotionsDesc", SqlDbType.Text),
				new SqlParameter("@PromotionsPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@CreateUserID", SqlDbType.Int, 4),
				new SqlParameter("@ShopList", SqlDbType.NVarChar, 2000)
			};
			parameters[0].Value = model.PromotionsTitle;
			parameters[1].Value = model.PromotionsStart;
			parameters[2].Value = model.PromotionsEnd;
			parameters[3].Value = model.PromotionsMemLevel;
			parameters[4].Value = model.PromotionsType;
			parameters[5].Value = model.PromotionsTime;
			parameters[6].Value = model.PromotionsRemark;
			parameters[7].Value = model.PromotionsDesc;
			parameters[8].Value = model.PromotionsPhoto;
			parameters[9].Value = model.CreateUserID;
			parameters[10].Value = model.ShopList;
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

		public bool Update(Chain.Model.Promotions model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update Promotions set ");
			strSql.Append("ShopList=@ShopList,");
			strSql.Append("PromotionsRemark=@PromotionsRemark,");
			strSql.Append("PromotionsDesc=@PromotionsDesc,");
			strSql.Append("PromotionsPhoto=@PromotionsPhoto,");
			strSql.Append("PromotionsTitle=@PromotionsTitle,");
			strSql.Append("PromotionsStart=@PromotionsStart,");
			strSql.Append("PromotionsEnd=@PromotionsEnd,");
			strSql.Append("PromotionsMemLevel=@PromotionsMemLevel,");
			strSql.Append("PromotionsType=@PromotionsType,");
			strSql.Append("PromotionsTime=@PromotionsTime");
			strSql.Append(" where PromotionsID=@PromotionsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PromotionsTitle", SqlDbType.NVarChar, 1000),
				new SqlParameter("@PromotionsStart", SqlDbType.DateTime),
				new SqlParameter("@PromotionsEnd", SqlDbType.DateTime),
				new SqlParameter("@PromotionsMemLevel", SqlDbType.Int, 4),
				new SqlParameter("@PromotionsType", SqlDbType.Int, 4),
				new SqlParameter("@PromotionsTime", SqlDbType.DateTime),
				new SqlParameter("@PromotionsRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@PromotionsDesc", SqlDbType.Text),
				new SqlParameter("@PromotionsPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@PromotionsID", SqlDbType.Int, 4),
				new SqlParameter("@ShopList", SqlDbType.NVarChar, 2000)
			};
			parameters[0].Value = model.PromotionsTitle;
			parameters[1].Value = model.PromotionsStart;
			parameters[2].Value = model.PromotionsEnd;
			parameters[3].Value = model.PromotionsMemLevel;
			parameters[4].Value = model.PromotionsType;
			parameters[5].Value = model.PromotionsTime;
			parameters[6].Value = model.PromotionsRemark;
			parameters[7].Value = model.PromotionsDesc;
			parameters[8].Value = model.PromotionsPhoto;
			parameters[9].Value = model.PromotionsID;
			parameters[10].Value = model.ShopList;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int PromotionsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Promotions ");
			strSql.Append(" where PromotionsID=@PromotionsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PromotionsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = PromotionsID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string PromotionsIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Promotions ");
			strSql.Append(" where PromotionsID in (" + PromotionsIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.Promotions GetModel(int PromotionsID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ShopList,PromotionsDesc,PromotionsRemark,PromotionsDesc,PromotionsPhoto,PromotionsID,PromotionsTitle,PromotionsStart,PromotionsEnd,PromotionsMemLevel,PromotionsType,PromotionsTime from Promotions ");
			strSql.Append(" where PromotionsID=@PromotionsID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PromotionsID", SqlDbType.Int, 4)
			};
			parameters[0].Value = PromotionsID;
			Chain.Model.Promotions model = new Chain.Model.Promotions();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.Promotions result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ShopList"] != null && ds.Tables[0].Rows[0]["ShopList"].ToString() != "")
				{
					model.ShopList = ds.Tables[0].Rows[0]["ShopList"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PromotionsRemark"] != null && ds.Tables[0].Rows[0]["PromotionsRemark"].ToString() != "")
				{
					model.PromotionsRemark = ds.Tables[0].Rows[0]["PromotionsRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PromotionsDesc"] != null && ds.Tables[0].Rows[0]["PromotionsDesc"].ToString() != "")
				{
					model.PromotionsDesc = ds.Tables[0].Rows[0]["PromotionsDesc"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PromotionsPhoto"] != null && ds.Tables[0].Rows[0]["PromotionsPhoto"].ToString() != "")
				{
					model.PromotionsPhoto = ds.Tables[0].Rows[0]["PromotionsPhoto"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PromotionsID"] != null && ds.Tables[0].Rows[0]["PromotionsID"].ToString() != "")
				{
					model.PromotionsID = int.Parse(ds.Tables[0].Rows[0]["PromotionsID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PromotionsTitle"] != null && ds.Tables[0].Rows[0]["PromotionsTitle"].ToString() != "")
				{
					model.PromotionsTitle = ds.Tables[0].Rows[0]["PromotionsTitle"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PromotionsStart"] != null && ds.Tables[0].Rows[0]["PromotionsStart"].ToString() != "")
				{
					model.PromotionsStart = DateTime.Parse(ds.Tables[0].Rows[0]["PromotionsStart"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PromotionsEnd"] != null && ds.Tables[0].Rows[0]["PromotionsEnd"].ToString() != "")
				{
					model.PromotionsEnd = DateTime.Parse(ds.Tables[0].Rows[0]["PromotionsEnd"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PromotionsMemLevel"] != null && ds.Tables[0].Rows[0]["PromotionsMemLevel"].ToString() != "")
				{
					model.PromotionsMemLevel = int.Parse(ds.Tables[0].Rows[0]["PromotionsMemLevel"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PromotionsType"] != null && ds.Tables[0].Rows[0]["PromotionsType"].ToString() != "")
				{
					model.PromotionsType = int.Parse(ds.Tables[0].Rows[0]["PromotionsType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PromotionsTime"] != null && ds.Tables[0].Rows[0]["PromotionsTime"].ToString() != "")
				{
					model.PromotionsTime = DateTime.Parse(ds.Tables[0].Rows[0]["PromotionsTime"].ToString());
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
			strSql.Append("select PromotionsID,PromotionsTitle,PromotionsStart,PromotionsEnd,PromotionsMemLevel,PromotionsType,PromotionsTime,PromotionsPhoto ");
			strSql.Append(" FROM Promotions ");
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
			strSql.Append(" PromotionsPhoto,PromotionsID,PromotionsTitle,PromotionsStart,PromotionsEnd,PromotionsMemLevel,PromotionsType,PromotionsTime ");
			strSql.Append(" FROM Promotions ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by " + filedOrder + " desc ");
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) FROM Promotions ");
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
				strSql.Append("order by T.PromotionsID desc");
			}
			strSql.Append(")AS Row, T.*  from Promotions T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetPromotionsInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * from ");
			strSql.Append("( ");
			strSql.Append("select row_number() over(order by PromotionsID desc) as row,* from Promotions left join MemLevel on Promotions.PromotionsMemLevel=MemLevel.LevelID left join SysUser on SysUser.UserID=Promotions.CreateUserID ");
			strSql.Append(") as T ");
			strSql.AppendFormat("where T.row>={0} and T.row<{1}", PageSize * (PageIndex - 1) + 1, PageSize * PageIndex + 1);
			DataSet ds = DbHelperSQL.Query(strSql.ToString());
			resCount = ds.Tables[0].Rows.Count;
			return ds;
		}
	}
}
