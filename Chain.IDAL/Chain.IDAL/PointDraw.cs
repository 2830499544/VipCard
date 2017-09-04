using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class PointDraw
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("DrawID", "PointDraw");
		}

		public DataSet GetVerifyListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "PointDraw,SysShop,SysUser";
			string[] columns = new string[]
			{
				"DrawID, DrawAccount, DrawShopID, DrawVerifyTime, DrawStatus, DrawPoint, DrawAmount, SysUser.UserName, DrawCreateTime, DrawRemark, ShopName "
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "DrawID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public bool Exists(int DrawID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from PointDraw");
			strSql.Append(" where DrawID=@DrawID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DrawID", SqlDbType.Int, 4)
			};
			parameters[0].Value = DrawID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.PointDraw model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into PointDraw(");
			strSql.Append("DrawAccount,DrawShopID,DrawPoint,DrawAmount,DrawStatus,DrawCreateTime,DrawCreateUserID,DrawRemark)");
			strSql.Append(" values (");
			strSql.Append("@DrawAccount,@DrawShopID,@DrawPoint,@DrawAmount,@DrawStatus,@DrawCreateTime,@DrawCreateUserID,@DrawRemark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DrawAccount", SqlDbType.NVarChar, 50),
				new SqlParameter("@DrawShopID", SqlDbType.Int, 4),
				new SqlParameter("@DrawPoint", SqlDbType.Int, 4),
				new SqlParameter("@DrawAmount", SqlDbType.Money, 8),
				new SqlParameter("@DrawStatus", SqlDbType.Int, 4),
				new SqlParameter("@DrawCreateTime", SqlDbType.DateTime, 4),
				new SqlParameter("@DrawCreateUserID", SqlDbType.Int),
				new SqlParameter("@DrawRemark", SqlDbType.NVarChar, 200)
			};
			parameters[0].Value = model.DrawAccount;
			parameters[1].Value = model.DrawShopID;
			parameters[2].Value = model.DrawPoint;
			parameters[3].Value = model.DrawAmount;
			parameters[4].Value = model.DrawStatus;
			parameters[5].Value = model.DrawCreateTime;
			parameters[6].Value = model.DrawCreateUserID;
			parameters[7].Value = model.DrawRemark;
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

		public bool Update(Chain.Model.PointDraw model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update PointDraw set ");
			strSql.Append("DrawStatus=@DrawStatus,");
			strSql.Append("DrawVerifyUserID=@DrawVerifyUserID,");
			strSql.Append("DrawVerifyTime=@DrawVerifyTime");
			strSql.Append(" where DrawID=@DrawID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DrawStatus", SqlDbType.Int, 4),
				new SqlParameter("@DrawVerifyUserID", SqlDbType.Int, 4),
				new SqlParameter("@DrawVerifyTime", SqlDbType.DateTime, 4),
				new SqlParameter("@DrawID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.DrawStatus;
			parameters[1].Value = model.DrawVerifyUserID;
			parameters[2].Value = model.DrawVerifyTime;
			parameters[3].Value = model.DrawID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int DrawID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from PointDraw ");
			strSql.Append(" where DrawID=@DrawID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DrawID", SqlDbType.Int, 4)
			};
			parameters[0].Value = DrawID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool CheckPointDraw(int DrawID, int status)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update   PointDraw ");
			strSql.Append(string.Concat(new object[]
			{
				" set status=",
				status,
				" where DrawID = ",
				DrawID
			}));
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public bool DeleteList(string DrawIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from PointDraw ");
			strSql.Append(" where DrawID in (" + DrawIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select DrawID,ExchangeMemID,ExchangeGiftID,ExchangeNumber,ExchangeTotalPoint,ExchangeShopID,ExchangeTime,ExchangeUserID ");
			strSql.Append(" FROM PointDraw ");
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
			strSql.Append(" DrawID,ExchangeMemID,ExchangeGiftID,ExchangeNumber,ExchangeTotalPoint,ExchangeShopID,ExchangeTime,ExchangeUserID ");
			strSql.Append(" FROM PointDraw ");
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
			strSql.Append("select count(1) FROM PointDraw ");
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
				strSql.Append("order by T.DrawID desc");
			}
			strSql.Append(")AS Row, T.*  from PointDraw T ");
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
			string tableName = "GiftExchange,Mem,SysShop,SysUser";
			string[] columns = new string[]
			{
				"ExchangeAccount,MemCard,MemName,ExchangeAllNumber,ExchangeAllPoint,ApplicationTime,ExchangeTime,ShopName,UserName,ExchangeType,DrawID,ExchangeStatus"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "GiftExchange.DrawID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public Chain.Model.PointDraw GetModel(int DrawID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 DrawID,DrawAccount,DrawPoint,DrawShopID,DrawAmount,DrawStatus from PointDraw ");
			strSql.Append(" where DrawID=@DrawID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DrawID", SqlDbType.Int, 4)
			};
			parameters[0].Value = DrawID;
			Chain.Model.PointDraw model = new Chain.Model.PointDraw();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.PointDraw result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["DrawID"] != null && ds.Tables[0].Rows[0]["DrawID"].ToString() != "")
				{
					model.DrawID = int.Parse(ds.Tables[0].Rows[0]["DrawID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DrawAccount"] != null && ds.Tables[0].Rows[0]["DrawAccount"].ToString() != "")
				{
					model.DrawAccount = ds.Tables[0].Rows[0]["DrawAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["DrawPoint"] != null && ds.Tables[0].Rows[0]["DrawPoint"].ToString() != "")
				{
					model.DrawPoint = int.Parse(ds.Tables[0].Rows[0]["DrawPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DrawShopID"] != null && ds.Tables[0].Rows[0]["DrawShopID"].ToString() != "")
				{
					model.DrawShopID = int.Parse(ds.Tables[0].Rows[0]["DrawShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DrawAmount"] != null && ds.Tables[0].Rows[0]["DrawAmount"].ToString() != "")
				{
					model.DrawAmount = decimal.Parse(ds.Tables[0].Rows[0]["DrawAmount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DrawStatus"] != null && ds.Tables[0].Rows[0]["DrawStatus"].ToString() != "")
				{
					model.DrawStatus = int.Parse(ds.Tables[0].Rows[0]["DrawStatus"].ToString());
				}
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public int GetShopPointDraw(int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.AppendFormat("SELECT  ISNULL(SUM(DrawPoint),0) AS DrawPoint FROM dbo.PointDraw WHERE DrawShopID = {0} AND DrawStatus=0 ", ShopID);
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			int result;
			if (obj != null)
			{
				result = Convert.ToInt32(obj);
			}
			else
			{
				result = 0;
			}
			return result;
		}
	}
}
