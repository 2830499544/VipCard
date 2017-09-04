using Chain.DBUtility;
using Chain.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class PointExchange
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ExchangeID", "PointExchange");
		}

		public bool Exists(int ExchangeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from PointExchange");
			strSql.Append(" where ExchangeID=@ExchangeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ExchangeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ExchangeID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.PointExchange model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into PointExchange(");
			strSql.Append("ExchangeMemID,ExchangeGiftID,ExchangeNumber,ExchangeTotalPoint,ExchangeShopID,ExchangeTime,ExchangeUserID)");
			strSql.Append(" values (");
			strSql.Append("@ExchangeMemID,@ExchangeGiftID,@ExchangeNumber,@ExchangeTotalPoint,@ExchangeShopID,@ExchangeTime,@ExchangeUserID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ExchangeMemID", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeGiftID", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeNumber", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeTotalPoint", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeShopID", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeTime", SqlDbType.DateTime),
				new SqlParameter("@ExchangeUserID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ExchangeMemID;
			parameters[1].Value = model.ExchangeGiftID;
			parameters[2].Value = model.ExchangeNumber;
			parameters[3].Value = model.ExchangeTotalPoint;
			parameters[4].Value = model.ExchangeShopID;
			parameters[5].Value = model.ExchangeTime;
			parameters[6].Value = model.ExchangeUserID;
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

		public bool Add(List<Chain.Model.PointExchange> list)
		{
			int totalPoint = 0;
			ArrayList arr = new ArrayList();
			foreach (Chain.Model.PointExchange model in list)
			{
				totalPoint += model.ExchangeTotalPoint;
				StringBuilder strSql = new StringBuilder();
				strSql.Append("insert into PointExchange(");
				strSql.Append("ExchangeMemID,ExchangeGiftID,ExchangeNumber,ExchangeTotalPoint,ExchangeShopID,ExchangeTime,ExchangeUserID)");
				strSql.Append(" values (");
				strSql.Append("@ExchangeMemID,@ExchangeGiftID,@ExchangeNumber,@ExchangeTotalPoint,@ExchangeShopID,@ExchangeTime,@ExchangeUserID)");
				strSql.Append(";select @@IDENTITY");
				SqlParameter[] parameters = new SqlParameter[]
				{
					new SqlParameter("@ExchangeMemID", SqlDbType.Int, 4),
					new SqlParameter("@ExchangeGiftID", SqlDbType.Int, 4),
					new SqlParameter("@ExchangeNumber", SqlDbType.Int, 4),
					new SqlParameter("@ExchangeTotalPoint", SqlDbType.Int, 4),
					new SqlParameter("@ExchangeShopID", SqlDbType.Int, 4),
					new SqlParameter("@ExchangeTime", SqlDbType.DateTime),
					new SqlParameter("@ExchangeUserID", SqlDbType.Int, 4)
				};
				parameters[0].Value = model.ExchangeMemID;
				parameters[1].Value = model.ExchangeGiftID;
				parameters[2].Value = model.ExchangeNumber;
				parameters[3].Value = model.ExchangeTotalPoint;
				parameters[4].Value = model.ExchangeShopID;
				parameters[5].Value = model.ExchangeTime;
				parameters[6].Value = model.ExchangeUserID;
				arr.Add(new DictionaryEntry(strSql.ToString(), parameters));
				string sqlGift = string.Format("Update PointGift set GiftStockNumber =GiftStockNumber -{0},GiftExchangeNumber = GiftExchangeNumber +{0} where GiftID={1}", model.ExchangeNumber, model.ExchangeGiftID);
				arr.Add(new DictionaryEntry(sqlGift, null));
			}
			return DbHelperSQL.ExecuteSqlTranPara(arr);
		}

		public bool Update(Chain.Model.PointExchange model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update PointExchange set ");
			strSql.Append("ExchangeMemID=@ExchangeMemID,");
			strSql.Append("ExchangeGiftID=@ExchangeGiftID,");
			strSql.Append("ExchangeNumber=@ExchangeNumber,");
			strSql.Append("ExchangeTotalPoint=@ExchangeTotalPoint,");
			strSql.Append("ExchangeShopID=@ExchangeShopID,");
			strSql.Append("ExchangeTime=@ExchangeTime,");
			strSql.Append("ExchangeUserID=@ExchangeUserID");
			strSql.Append(" where ExchangeID=@ExchangeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ExchangeMemID", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeGiftID", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeNumber", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeTotalPoint", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeShopID", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeTime", SqlDbType.DateTime),
				new SqlParameter("@ExchangeUserID", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ExchangeMemID;
			parameters[1].Value = model.ExchangeGiftID;
			parameters[2].Value = model.ExchangeNumber;
			parameters[3].Value = model.ExchangeTotalPoint;
			parameters[4].Value = model.ExchangeShopID;
			parameters[5].Value = model.ExchangeTime;
			parameters[6].Value = model.ExchangeUserID;
			parameters[7].Value = model.ExchangeID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ExchangeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from PointExchange ");
			strSql.Append(" where ExchangeID=@ExchangeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ExchangeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ExchangeID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string ExchangeIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from PointExchange ");
			strSql.Append(" where ExchangeID in (" + ExchangeIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.PointExchange GetModel(int ExchangeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ExchangeID,ExchangeMemID,ExchangeGiftID,ExchangeNumber,ExchangeTotalPoint,ExchangeShopID,ExchangeTime,ExchangeUserID from PointExchange ");
			strSql.Append(" where ExchangeID=@ExchangeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ExchangeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ExchangeID;
			Chain.Model.PointExchange model = new Chain.Model.PointExchange();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.PointExchange result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ExchangeID"] != null && ds.Tables[0].Rows[0]["ExchangeID"].ToString() != "")
				{
					model.ExchangeID = int.Parse(ds.Tables[0].Rows[0]["ExchangeID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExchangeMemID"] != null && ds.Tables[0].Rows[0]["ExchangeMemID"].ToString() != "")
				{
					model.ExchangeMemID = int.Parse(ds.Tables[0].Rows[0]["ExchangeMemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExchangeGiftID"] != null && ds.Tables[0].Rows[0]["ExchangeGiftID"].ToString() != "")
				{
					model.ExchangeGiftID = int.Parse(ds.Tables[0].Rows[0]["ExchangeGiftID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExchangeNumber"] != null && ds.Tables[0].Rows[0]["ExchangeNumber"].ToString() != "")
				{
					model.ExchangeNumber = int.Parse(ds.Tables[0].Rows[0]["ExchangeNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExchangeTotalPoint"] != null && ds.Tables[0].Rows[0]["ExchangeTotalPoint"].ToString() != "")
				{
					model.ExchangeTotalPoint = int.Parse(ds.Tables[0].Rows[0]["ExchangeTotalPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExchangeShopID"] != null && ds.Tables[0].Rows[0]["ExchangeShopID"].ToString() != "")
				{
					model.ExchangeShopID = int.Parse(ds.Tables[0].Rows[0]["ExchangeShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExchangeTime"] != null && ds.Tables[0].Rows[0]["ExchangeTime"].ToString() != "")
				{
					model.ExchangeTime = DateTime.Parse(ds.Tables[0].Rows[0]["ExchangeTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExchangeUserID"] != null && ds.Tables[0].Rows[0]["ExchangeUserID"].ToString() != "")
				{
					model.ExchangeUserID = int.Parse(ds.Tables[0].Rows[0]["ExchangeUserID"].ToString());
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
			strSql.Append("select ExchangeID,ExchangeMemID,ExchangeGiftID,ExchangeNumber,ExchangeTotalPoint,ExchangeShopID,ExchangeTime,ExchangeUserID ");
			strSql.Append(" FROM PointExchange ");
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
			strSql.Append(" ExchangeID,ExchangeMemID,ExchangeGiftID,ExchangeNumber,ExchangeTotalPoint,ExchangeShopID,ExchangeTime,ExchangeUserID ");
			strSql.Append(" FROM PointExchange ");
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
			strSql.Append("select count(1) FROM PointExchange ");
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
				strSql.Append("order by T.ExchangeID desc");
			}
			strSql.Append(")AS Row, T.*  from PointExchange T ");
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
				"ExchangeAccount,MemCard,GiftExchange.MemName,ExchangeAllNumber,ExchangeAllPoint,ApplicationTime,ExchangeTime,ShopName,UserName,ExchangeType,ExchangeID,ExchangeStatus"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "GiftExchange.ExchangeID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
