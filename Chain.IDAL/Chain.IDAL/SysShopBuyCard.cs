using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysShopBuyCard
	{
		public bool Exists(int BuyCardID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysShopBuyCard");
			strSql.Append(" where BuyCardID=@BuyCardID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@BuyCardID", SqlDbType.Int, 4)
			};
			parameters[0].Value = BuyCardID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public string GetCarNum(string StartNum, string EndNum)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 Num from f_GetCarNum(@StartNum,@EndNum)  where Num not in(select MemCard from Mem)");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StartNum", SqlDbType.NVarChar, 50),
				new SqlParameter("@EndNum", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = StartNum;
			parameters[1].Value = EndNum;
			object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
			string result;
			if (obj != null)
			{
				result = obj.ToString();
			}
			else
			{
				result = "0";
			}
			return result;
		}

		public int Add(Chain.Model.SysShopBuyCard model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysShopBuyCard(");
			strSql.Append("BuyType,StartCardNumber,EndCardNumber,BuyCardShopid,UserID,Remark,BuyCardTime,BuyCardMoney)");
			strSql.Append(" values (");
			strSql.Append("@BuyType,@StartCardNumber,@EndCardNumber,@BuyCardShopid,@UserID,@Remark,@BuyCardTime,@BuyCardMoney)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StartCardNumber", SqlDbType.NVarChar, 200),
				new SqlParameter("@EndCardNumber", SqlDbType.NVarChar, 200),
				new SqlParameter("@BuyCardShopid", SqlDbType.Int, 4),
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@Remark", SqlDbType.NVarChar, 200),
				new SqlParameter("@BuyCardTime", SqlDbType.DateTime),
				new SqlParameter("@BuyCardMoney", SqlDbType.Money, 8),
				new SqlParameter("@BuyType", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.StartCardNumber;
			parameters[1].Value = model.EndCardNumber;
			parameters[2].Value = model.BuyCardShopid;
			parameters[3].Value = model.UserID;
			parameters[4].Value = model.Remark;
			parameters[5].Value = model.BuyCardTime;
			parameters[6].Value = model.BuyCardMoney;
			parameters[7].Value = model.BuyType;
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

		public bool Update(Chain.Model.SysShopBuyCard model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysShopBuyCard set ");
			strSql.Append("StartCardNumber=@StartCardNumber,");
			strSql.Append("EndCardNumber=@EndCardNumber,");
			strSql.Append("BuyCardShopid=@BuyCardShopid,");
			strSql.Append("UserID=@UserID,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("BuyCardTime=@BuyCardTime,");
			strSql.Append("BuyCardMoney=@BuyCardMoney");
			strSql.Append(" where BuyCardID=@BuyCardID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StartCardNumber", SqlDbType.NVarChar, 200),
				new SqlParameter("@EndCardNumber", SqlDbType.NVarChar, 200),
				new SqlParameter("@BuyCardShopid", SqlDbType.Int, 4),
				new SqlParameter("@UserID", SqlDbType.Int, 4),
				new SqlParameter("@Remark", SqlDbType.NVarChar, 200),
				new SqlParameter("@BuyCardTime", SqlDbType.DateTime),
				new SqlParameter("@BuyCardMoney", SqlDbType.Money, 8),
				new SqlParameter("@BuyCardID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.StartCardNumber;
			parameters[1].Value = model.EndCardNumber;
			parameters[2].Value = model.BuyCardShopid;
			parameters[3].Value = model.UserID;
			parameters[4].Value = model.Remark;
			parameters[5].Value = model.BuyCardTime;
			parameters[6].Value = model.BuyCardMoney;
			parameters[7].Value = model.BuyCardID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int BuyCardID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysShopBuyCard ");
			strSql.Append(" where BuyCardID=@BuyCardID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@BuyCardID", SqlDbType.Int, 4)
			};
			parameters[0].Value = BuyCardID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string BuyCardIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysShopBuyCard ");
			strSql.Append(" where BuyCardID in (" + BuyCardIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysShopBuyCard GetModel(int BuyCardID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 BuyType,BuyCardID,StartCardNumber,EndCardNumber,BuyCardShopid,UserID,Remark,BuyCardTime,BuyCardMoney from SysShopBuyCard ");
			strSql.Append(" where BuyCardID=@BuyCardID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@BuyCardID", SqlDbType.Int, 4)
			};
			parameters[0].Value = BuyCardID;
			Chain.Model.SysShopBuyCard model = new Chain.Model.SysShopBuyCard();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysShopBuyCard result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["BuyType"] != null && ds.Tables[0].Rows[0]["BuyType"].ToString() != "")
				{
					model.BuyType = int.Parse(ds.Tables[0].Rows[0]["BuyType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["BuyCardID"] != null && ds.Tables[0].Rows[0]["BuyCardID"].ToString() != "")
				{
					model.BuyCardID = int.Parse(ds.Tables[0].Rows[0]["BuyCardID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StartCardNumber"] != null && ds.Tables[0].Rows[0]["StartCardNumber"].ToString() != "")
				{
					model.StartCardNumber = ds.Tables[0].Rows[0]["StartCardNumber"].ToString();
				}
				if (ds.Tables[0].Rows[0]["EndCardNumber"] != null && ds.Tables[0].Rows[0]["EndCardNumber"].ToString() != "")
				{
					model.EndCardNumber = ds.Tables[0].Rows[0]["EndCardNumber"].ToString();
				}
				if (ds.Tables[0].Rows[0]["BuyCardShopid"] != null && ds.Tables[0].Rows[0]["BuyCardShopid"].ToString() != "")
				{
					model.BuyCardShopid = int.Parse(ds.Tables[0].Rows[0]["BuyCardShopid"].ToString());
				}
				if (ds.Tables[0].Rows[0]["UserID"] != null && ds.Tables[0].Rows[0]["UserID"].ToString() != "")
				{
					model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
				{
					model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["BuyCardTime"] != null && ds.Tables[0].Rows[0]["BuyCardTime"].ToString() != "")
				{
					model.BuyCardTime = DateTime.Parse(ds.Tables[0].Rows[0]["BuyCardTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["BuyCardMoney"] != null && ds.Tables[0].Rows[0]["BuyCardMoney"].ToString() != "")
				{
					model.BuyCardMoney = decimal.Parse(ds.Tables[0].Rows[0]["BuyCardMoney"].ToString());
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
			strSql.Append("select BuyType,BuyCardID,StartCardNumber,EndCardNumber,BuyCardShopid,UserID,Remark,BuyCardTime,BuyCardMoney ");
			strSql.Append(" FROM SysShopBuyCard ");
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
			strSql.Append(" BuyCardID,StartCardNumber,EndCardNumber,BuyCardShopid,UserID,Remark,BuyCardTime,BuyCardMoney ");
			strSql.Append(" FROM SysShopBuyCard ");
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
			strSql.Append("select count(1) FROM SysShopBuyCard ");
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
				strSql.Append("order by T.BuyCardID desc");
			}
			strSql.Append(")AS Row, T.*  from SysShopBuyCard T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, string[] strWhere, out int resCount)
		{
			string tableName = "SysShopBuyCard,SysShop,SysUser";
			string[] columns = new string[]
			{
				"SysShopBuyCard.*,SysShop.ShopName,SysUser.UserName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "BuyCardID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
