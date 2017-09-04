using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class MemChild
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ChildID", "MemChild");
		}

		public bool Exists(int ChildID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemChild");
			strSql.Append(" where ChildID=@ChildID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ChildID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ChildID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(int ChildID, int LevelPoint)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemChild");
			strSql.Append(" where ChildID <> @ChildID AND LevelPoint = @LevelPoint");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ChildID", SqlDbType.Int, 4),
				new SqlParameter("@LevelPoint", SqlDbType.Int)
			};
			parameters[0].Value = ChildID;
			parameters[1].Value = LevelPoint;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.MemChild model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MemChild(");
			strSql.Append("ChildCard,ChildName,ChildMobile,MemID,ChildStatus,CreateTime)");
			strSql.Append(" values (");
			strSql.Append("@ChildCard,@ChildName,@ChildMobile,@MemID,@ChildStatus,@CreateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ChildCard", SqlDbType.NVarChar, 50),
				new SqlParameter("@ChildName", SqlDbType.NVarChar, 50),
				new SqlParameter("@ChildMobile", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@ChildStatus", SqlDbType.Int, 4),
				new SqlParameter("@CreateTime", SqlDbType.DateTime)
			};
			parameters[0].Value = model.ChildCard;
			parameters[1].Value = model.ChildName;
			parameters[2].Value = model.ChildMobile;
			parameters[3].Value = model.MemID;
			parameters[4].Value = model.ChildStatus;
			parameters[5].Value = model.CreateTime;
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

		public int Update(Chain.Model.MemChild model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MemChild set ");
			strSql.Append("LevelName=@LevelName,");
			strSql.Append("LevelPoint=@LevelPoint,");
			strSql.Append("LevelDiscountPercent=@LevelDiscountPercent,");
			strSql.Append("LevelPointPercent=@LevelPointPercent,");
			strSql.Append("LevellLock=@LevellLock,");
			strSql.Append("LevelRechargePointRate=@LevelRechargePointRate ");
			strSql.Append(" where ChildID=@ChildID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ChildCard", SqlDbType.NVarChar, 50),
				new SqlParameter("@ChildName", SqlDbType.NVarChar, 50),
				new SqlParameter("@ChildMobile", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@ChildStatus", SqlDbType.Int, 4),
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@ChildID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ChildCard;
			parameters[1].Value = model.ChildName;
			parameters[2].Value = model.ChildMobile;
			parameters[3].Value = model.MemID;
			parameters[4].Value = model.ChildStatus;
			parameters[5].Value = model.CreateTime;
			parameters[6].Value = model.ChildID;
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

		public int Delete(int ChildID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemChild ");
			strSql.Append(" where ChildID=@ChildID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ChildID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ChildID;
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

		public bool DeleteList(string LevelIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemChild ");
			strSql.Append(" where ChildID in (" + LevelIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MemChild GetModel(int ChildID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ChildID,LevelName,LevelPoint,LevelDiscountPercent,LevelPointPercent,LevellLock,LevelRechargePointRate from MemChild ");
			strSql.Append(" where ChildID=@ChildID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ChildID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ChildID;
			Chain.Model.MemChild model = new Chain.Model.MemChild();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MemChild result;
			if (ds.Tables[0].Rows.Count > 0)
			{
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
			strSql.Append("select ChildID,LevelName,LevelPoint,LevelDiscountPercent,LevelPointPercent,LevellLock,LevelRechargePointRate ");
			strSql.Append(" FROM MemChild ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" ORDER BY LevelPoint asc");
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetLists(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ChildID,ShopMemLevelID, LevelName,LevelPoint,ClassDiscountPercent,ClassPointPercent,ClassRechargePointRate,LevellLock from SysShopMemLevel join MemChild on MemChild.ChildID=SysShopMemLevel.MemLevelID ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" ORDER BY LevelPoint asc");
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
			strSql.Append(" ChildID,LevelName,LevelPoint,LevelDiscountPercent,LevelPointPercent,LevellLock,LevelRechargePointRate ");
			strSql.Append(" FROM MemChild ");
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
			strSql.Append("select count(1) FROM MemChild ");
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
				strSql.Append("order by T.ChildID desc");
			}
			strSql.Append(")AS Row, T.*  from MemChild T ");
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
