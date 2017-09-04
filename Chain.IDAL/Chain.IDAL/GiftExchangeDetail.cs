using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class GiftExchangeDetail
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ExchangeDetailID", "GiftExchangeDetail");
		}

		public bool Exists(int ExchangeDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from GiftExchangeDetail");
			strSql.Append(" where ExchangeDetailID=@ExchangeDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ExchangeDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ExchangeDetailID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.GiftExchangeDetail model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into GiftExchangeDetail(");
			strSql.Append("ExchangeID,ExchangeGiftID,ExchangeNumber,ExchangePoint,GiftName)");
			strSql.Append(" values (");
			strSql.Append("@ExchangeID,@ExchangeGiftID,@ExchangeNumber,@ExchangePoint,@GiftName)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ExchangeID", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeGiftID", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeNumber", SqlDbType.Int, 4),
				new SqlParameter("@ExchangePoint", SqlDbType.Int, 4),
				new SqlParameter("@GiftName", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = model.ExchangeID;
			parameters[1].Value = model.ExchangeGiftID;
			parameters[2].Value = model.ExchangeNumber;
			parameters[3].Value = model.ExchangePoint;
			parameters[4].Value = model.Giftname;
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

		public bool Update(Chain.Model.GiftExchangeDetail model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update GiftExchangeDetail set ");
			strSql.Append("ExchangeID=@ExchangeID,");
			strSql.Append("ExchangeGiftID=@ExchangeGiftID,");
			strSql.Append("ExchangeNumber=@ExchangeNumber,");
			strSql.Append("ExchangePoint=@ExchangePoint,");
			strSql.Append("GiftName=@GiftName");
			strSql.Append(" where ExchangeDetailID=@ExchangeDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ExchangeID", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeGiftID", SqlDbType.Int, 4),
				new SqlParameter("@ExchangeNumber", SqlDbType.Int, 4),
				new SqlParameter("@ExchangePoint", SqlDbType.Int, 4),
				new SqlParameter("@GiftName", SqlDbType.NVarChar, 50),
				new SqlParameter("@ExchangeDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.ExchangeID;
			parameters[1].Value = model.ExchangeGiftID;
			parameters[2].Value = model.ExchangeNumber;
			parameters[3].Value = model.ExchangePoint;
			parameters[4].Value = model.Giftname;
			parameters[5].Value = model.ExchangeDetailID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ExchangeDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GiftExchangeDetail ");
			strSql.Append(" where ExchangeDetailID=@ExchangeDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ExchangeDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ExchangeDetailID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string ExchangeDetailIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GiftExchangeDetail ");
			strSql.Append(" where ExchangeDetailID in (" + ExchangeDetailIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.GiftExchangeDetail GetModel(int ExchangeDetailID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ExchangeDetailID,ExchangeID,ExchangeGiftID,ExchangeNumber,ExchangePoint,GiftName from GiftExchangeDetail ");
			strSql.Append(" where ExchangeDetailID=@ExchangeDetailID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ExchangeDetailID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ExchangeDetailID;
			Chain.Model.GiftExchangeDetail model = new Chain.Model.GiftExchangeDetail();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.GiftExchangeDetail result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ExchangeDetailID"] != null && ds.Tables[0].Rows[0]["ExchangeDetailID"].ToString() != "")
				{
					model.ExchangeDetailID = int.Parse(ds.Tables[0].Rows[0]["ExchangeDetailID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExchangeID"] != null && ds.Tables[0].Rows[0]["ExchangeID"].ToString() != "")
				{
					model.ExchangeID = int.Parse(ds.Tables[0].Rows[0]["ExchangeID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExchangeGiftID"] != null && ds.Tables[0].Rows[0]["ExchangeGiftID"].ToString() != "")
				{
					model.ExchangeGiftID = int.Parse(ds.Tables[0].Rows[0]["ExchangeGiftID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExchangeNumber"] != null && ds.Tables[0].Rows[0]["ExchangeNumber"].ToString() != "")
				{
					model.ExchangeNumber = int.Parse(ds.Tables[0].Rows[0]["ExchangeNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ExchangePoint"] != null && ds.Tables[0].Rows[0]["ExchangePoint"].ToString() != "")
				{
					model.ExchangePoint = int.Parse(ds.Tables[0].Rows[0]["ExchangePoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["GiftName"] != null && ds.Tables[0].Rows[0]["GiftName"].ToString() != "")
				{
					model.Giftname = ds.Tables[0].Rows[0]["GiftName"].ToString();
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
			strSql.Append("select GiftPhoto,ExchangeDetailID,ExchangeID,ExchangeGiftID,ExchangeNumber,ExchangePoint,PointGift.GiftName ");
			strSql.Append(" FROM GiftExchangeDetail,PointGift ");
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
			strSql.Append(" ExchangeDetailID,ExchangeID,ExchangeGiftID,ExchangeNumber,ExchangePoint,GiftName ");
			strSql.Append(" FROM GiftExchangeDetail ");
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
			strSql.Append("select count(1) FROM GiftExchangeDetail ");
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
				strSql.Append("order by T.ExchangeDetailID desc");
			}
			strSql.Append(")AS Row, T.*  from GiftExchangeDetail T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataTable GetExchangeAccountByMemID(int memID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ExchangeAccount from GiftExchange ");
			strSql.Append(" where MemID=@MemID order by ApplicationTime desc");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameters[0].Value = memID;
			return DbHelperSQL.Query(strSql.ToString()).Tables[0];
		}

		public DataTable GetGiftExchangeInfoBystrWhere(string strWhere)
		{
			return DbHelperSQL.Query("select ExchangeID,ExchangeAccount,ExchangeAllNumber,ExchangeAllPoint,ApplicationTime,ExchangeStatus,GiftName from GiftExchange " + strWhere).Tables[0];
		}

		public DataTable GetGiftExchangeDetailByExchangeID(int exchangeID)
		{
			return DbHelperSQL.Query("select GiftExchangeDetail.GiftName,ExchangeNumber,ExchangeGiftID, ExchangePoint from GiftExchangeDetail join PointGift on GiftExchangeDetail.ExchangeGiftID=PointGift.GiftID where ExchangeID=@ExchangeID", new SqlParameter[]
			{
				new SqlParameter("ExchangeID", exchangeID)
			}).Tables[0];
		}

		public DataTable GetWeiXinList(int ExchangeID)
		{
			StringBuilder sbStr = new StringBuilder();
			sbStr.Append("select PointGift.GiftName,ExchangeNumber,GiftExchangePoint,ExchangePoint ");
			sbStr.Append("from PointGift,GiftExchangeDetail ");
			sbStr.Append("where GiftID=ExchangeGiftID ");
			sbStr.Append("and ExchangeID=@ExchangeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ExchangeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ExchangeID;
			return DbHelperSQL.Query(sbStr.ToString(), parameters).Tables[0];
		}

		public DataTable AgainPrint(int rechargeID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ");
			strSql.Append("GiftExchangeDetail.GiftName,ExchangeNumber,GiftExchangePoint,ExchangePoint,PointGift.GiftCode ");
			strSql.Append("from GiftExchangeDetail,PointGift ");
			strSql.Append("where GiftExchangeDetail.ExchangeGiftID=PointGift.GiftID ");
			strSql.Append("and ExchangeID=@ExchangeID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ExchangeID", SqlDbType.Int, 4)
			};
			parameters[0].Value = rechargeID;
			return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
		}
	}
}
