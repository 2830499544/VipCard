using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class MemCount
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("CountID", "MemCount");
		}

		public bool Exists(int CountID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemCount");
			strSql.Append(" where CountID=@CountID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CountID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CountID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(string account)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemCount");
			strSql.Append(" where CountAccount=@CountAccount");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CountAccount", SqlDbType.VarChar, 50)
			};
			parameters[0].Value = account;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.MemCount model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MemCount(");
			strSql.Append("CountMemID,CountAccount,CountTotalMoney,CountDiscountMoney,CountPayCard,CountPayCash,CountPayCoupon,CountPayType,CountPoint,CountRemark,CountShopID,CountCreateTime,CountUserID,CountIsCard,CountIsCash,CountIsBink,CountPayBink)");
			strSql.Append(" values (");
			strSql.Append("@CountMemID,@CountAccount,@CountTotalMoney,@CountDiscountMoney,@CountPayCard,@CountPayCash,@CountPayCoupon,@CountPayType,@CountPoint,@CountRemark,@CountShopID,@CountCreateTime,@CountUserID,@CountIsCard,@CountIsCash,@CountIsBink,@CountPayBink)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CountMemID", SqlDbType.Int, 4),
				new SqlParameter("@CountAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@CountTotalMoney", SqlDbType.Money, 8),
				new SqlParameter("@CountDiscountMoney", SqlDbType.Money, 8),
				new SqlParameter("@CountPayCard", SqlDbType.Money, 8),
				new SqlParameter("@CountPayCash", SqlDbType.Money, 8),
				new SqlParameter("@CountPayCoupon", SqlDbType.Money, 8),
				new SqlParameter("@CountPayType", SqlDbType.TinyInt, 1),
				new SqlParameter("@CountPoint", SqlDbType.Int, 4),
				new SqlParameter("@CountRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@CountShopID", SqlDbType.Int, 4),
				new SqlParameter("@CountCreateTime", SqlDbType.DateTime),
				new SqlParameter("@CountUserID", SqlDbType.Int, 4),
				new SqlParameter("@CountIsCard", SqlDbType.Bit),
				new SqlParameter("@CountIsCash", SqlDbType.Bit),
				new SqlParameter("@CountIsBink", SqlDbType.Bit),
				new SqlParameter("@CountPayBink", SqlDbType.Money, 8)
			};
			parameters[0].Value = model.CountMemID;
			parameters[1].Value = model.CountAccount;
			parameters[2].Value = model.CountTotalMoney;
			parameters[3].Value = model.CountDiscountMoney;
			parameters[4].Value = model.CountPayCard;
			parameters[5].Value = model.CountPayCash;
			parameters[6].Value = model.CountPayCoupon;
			parameters[7].Value = model.CountPayType;
			parameters[8].Value = model.CountPoint;
			parameters[9].Value = model.CountRemark;
			parameters[10].Value = model.CountShopID;
			parameters[11].Value = model.CountCreateTime;
			parameters[12].Value = model.CountUserID;
			parameters[13].Value = model.CountIsCard;
			parameters[14].Value = model.CountIsCash;
			parameters[15].Value = model.CountIsBink;
			parameters[16].Value = model.CountPayBink;
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

		public bool Update(Chain.Model.MemCount model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MemCount set ");
			strSql.Append("CountMemID=@CountMemID,");
			strSql.Append("CountAccount=@CountAccount,");
			strSql.Append("CountTotalMoney=@CountTotalMoney,");
			strSql.Append("CountDiscountMoney=@CountDiscountMoney,");
			strSql.Append("CountPayCard=@CountPayCard,");
			strSql.Append("CountPayCash=@CountPayCash,");
			strSql.Append("CountPayCoupon=@CountPayCoupon");
			strSql.Append("CountPayType=@CountPayType,");
			strSql.Append("CountPoint=@CountPoint,");
			strSql.Append("CountRemark=@CountRemark,");
			strSql.Append("CountShopID=@CountShopID,");
			strSql.Append("CountCreateTime=@CountCreateTime,");
			strSql.Append("CountUserID=@CountUserID,");
			strSql.Append("CountIsCard=@CountIsCard,");
			strSql.Append("CountIsCash=@CountIsCash,");
			strSql.Append("CountIsBink=@CountIsBink,");
			strSql.Append("CountPayBink=@CountPayBink");
			strSql.Append(" where CountID=@CountID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CountMemID", SqlDbType.Int, 4),
				new SqlParameter("@CountAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@CountTotalMoney", SqlDbType.Money, 8),
				new SqlParameter("@CountDiscountMoney", SqlDbType.Money, 8),
				new SqlParameter("@CountPayCard", SqlDbType.Money, 8),
				new SqlParameter("@CountPayCash", SqlDbType.Money, 8),
				new SqlParameter("@CountPayCoupon", SqlDbType.Money, 8),
				new SqlParameter("@CountPayType", SqlDbType.TinyInt, 1),
				new SqlParameter("@CountPoint", SqlDbType.Int, 4),
				new SqlParameter("@CountRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@CountShopID", SqlDbType.Int, 4),
				new SqlParameter("@CountCreateTime", SqlDbType.DateTime),
				new SqlParameter("@CountUserID", SqlDbType.Int, 4),
				new SqlParameter("@CountIsCard", SqlDbType.Bit),
				new SqlParameter("@CountIsCash", SqlDbType.Bit),
				new SqlParameter("@CountIsBink", SqlDbType.Bit),
				new SqlParameter("@CountPayBink", SqlDbType.Money, 8),
				new SqlParameter("@CountID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.CountMemID;
			parameters[1].Value = model.CountAccount;
			parameters[2].Value = model.CountTotalMoney;
			parameters[3].Value = model.CountDiscountMoney;
			parameters[4].Value = model.CountPayCard;
			parameters[5].Value = model.CountPayCash;
			parameters[6].Value = model.CountPayCoupon;
			parameters[7].Value = model.CountPayType;
			parameters[8].Value = model.CountPoint;
			parameters[9].Value = model.CountRemark;
			parameters[10].Value = model.CountShopID;
			parameters[11].Value = model.CountCreateTime;
			parameters[12].Value = model.CountUserID;
			parameters[13].Value = model.CountID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int CountID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemCount ");
			strSql.Append(" where CountID=@CountID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CountID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CountID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string CountIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemCount ");
			strSql.Append(" where CountID in (" + CountIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MemCount GetModel(int CountID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 CountID,CountMemID,CountAccount,CountTotalMoney,CountDiscountMoney,CountPayCard,CountPayCash,CountPayCoupon,CountPayType,CountPoint,CountRemark,CountShopID,CountCreateTime,CountUserID,CountIsCard,CountIsCash,CountIsBink,CountPayBink from MemCount ");
			strSql.Append(" where CountID=@CountID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CountID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CountID;
			Chain.Model.MemCount model = new Chain.Model.MemCount();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MemCount result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["CountID"] != null && ds.Tables[0].Rows[0]["CountID"].ToString() != "")
				{
					model.CountID = int.Parse(ds.Tables[0].Rows[0]["CountID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountMemID"] != null && ds.Tables[0].Rows[0]["CountMemID"].ToString() != "")
				{
					model.CountMemID = int.Parse(ds.Tables[0].Rows[0]["CountMemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountAccount"] != null && ds.Tables[0].Rows[0]["CountAccount"].ToString() != "")
				{
					model.CountAccount = ds.Tables[0].Rows[0]["CountAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CountTotalMoney"] != null && ds.Tables[0].Rows[0]["CountTotalMoney"].ToString() != "")
				{
					model.CountTotalMoney = decimal.Parse(ds.Tables[0].Rows[0]["CountTotalMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountDiscountMoney"] != null && ds.Tables[0].Rows[0]["CountDiscountMoney"].ToString() != "")
				{
					model.CountDiscountMoney = decimal.Parse(ds.Tables[0].Rows[0]["CountDiscountMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountPayCard"] != null && ds.Tables[0].Rows[0]["CountPayCard"].ToString() != "")
				{
					model.CountPayCard = decimal.Parse(ds.Tables[0].Rows[0]["CountPayCard"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountPayCash"] != null && ds.Tables[0].Rows[0]["CountPayCash"].ToString() != "")
				{
					model.CountPayCash = decimal.Parse(ds.Tables[0].Rows[0]["CountPayCash"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountPayCoupon"] != null && ds.Tables[0].Rows[0]["CountPayCoupon"].ToString() != "")
				{
					model.CountPayCoupon = decimal.Parse(ds.Tables[0].Rows[0]["CountPayCoupon"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountPayType"] != null && ds.Tables[0].Rows[0]["CountPayType"].ToString() != "")
				{
					model.CountPayType = int.Parse(ds.Tables[0].Rows[0]["CountPayType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountPoint"] != null && ds.Tables[0].Rows[0]["CountPoint"].ToString() != "")
				{
					model.CountPoint = int.Parse(ds.Tables[0].Rows[0]["CountPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountRemark"] != null && ds.Tables[0].Rows[0]["CountRemark"].ToString() != "")
				{
					model.CountRemark = ds.Tables[0].Rows[0]["CountRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CountShopID"] != null && ds.Tables[0].Rows[0]["CountShopID"].ToString() != "")
				{
					model.CountShopID = int.Parse(ds.Tables[0].Rows[0]["CountShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountCreateTime"] != null && ds.Tables[0].Rows[0]["CountCreateTime"].ToString() != "")
				{
					model.CountCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CountCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountUserID"] != null && ds.Tables[0].Rows[0]["CountUserID"].ToString() != "")
				{
					model.CountUserID = int.Parse(ds.Tables[0].Rows[0]["CountUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CountIsCard"] != null && ds.Tables[0].Rows[0]["CountIsCard"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["CountIsCard"].ToString() == "1" || ds.Tables[0].Rows[0]["CountIsCard"].ToString().ToLower() == "true")
					{
						model.CountIsCard = true;
					}
					else
					{
						model.CountIsCard = false;
					}
				}
				if (ds.Tables[0].Rows[0]["CountIsCash"] != null && ds.Tables[0].Rows[0]["CountIsCash"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["CountIsCash"].ToString() == "1" || ds.Tables[0].Rows[0]["CountIsCash"].ToString().ToLower() == "true")
					{
						model.CountIsCash = true;
					}
					else
					{
						model.CountIsCash = false;
					}
				}
				if (ds.Tables[0].Rows[0]["CountIsBink"] != null && ds.Tables[0].Rows[0]["CountIsBink"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["CountIsBink"].ToString() == "1" || ds.Tables[0].Rows[0]["CountIsBink"].ToString().ToLower() == "true")
					{
						model.CountIsBink = true;
					}
					else
					{
						model.CountIsBink = false;
					}
				}
				if (ds.Tables[0].Rows[0]["CountPayBink"] != null && ds.Tables[0].Rows[0]["CountPayBink"].ToString() != "")
				{
					model.CountPayBink = decimal.Parse(ds.Tables[0].Rows[0]["CountPayBink"].ToString());
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
			strSql.Append("select CountID,CountMemID,CountAccount,CountTotalMoney,CountDiscountMoney,CountPayCard,CountPayCash,CountPayCoupon,CountPayType,CountPoint,CountRemark,CountShopID,CountCreateTime,CountUserID,CountIsCard,CountIsCash,CountIsBink,CountPayBink ");
			strSql.Append(" FROM MemCount ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "Mem,MemCount,SysShop,SysUser,MemLevel";
			string[] columns = new string[]
			{
				"Mem.MemCard,Mem.MemName,SysShop.ShopName,MemCount.*,SysUser.UserName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "CountID", "CountID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ");
			if (Top > 0)
			{
				strSql.Append(" top " + Top.ToString());
			}
			strSql.Append(" CountID,CountMemID,CountAccount,CountTotalMoney,CountDiscountMoney,CountPayCard,CountPayCash,CountPayCoupon,CountPayType,CountPoint,CountRemark,CountShopID,CountCreateTime,CountUserID,CountIsCard,CountIsCash,CountIsBink,CountPayBink ");
			strSql.Append(" FROM MemCount ");
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
			strSql.Append("select count(1) FROM MemCount ");
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
				strSql.Append("order by T.CountID desc");
			}
			strSql.Append(")AS Row, T.*  from MemCount T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetCountMoney(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select isnull(sum(CountTotalMoney),0) as TotalMoney,isnull(sum(CountDiscountMoney),0) as DiscountMoney,isnull(sum(CountPoint),0) as TotalPoint FROM MemCount,Mem ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where MemCount.CountMemID=Mem.MemID and " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetCountNumber(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select isnull(sum(MemCountDetail.CountDetailTotalNumber),0) as TotalNumber,isnull(sum(MemCountDetail.CountDetailNumber),0) as RemainCount from MemCountDetail,MemCount,Mem ");
			strSql.Append(" where  MemCount.CountID=MemCountDetail.CountDetailCountID and MemCount.CountMemID=Mem.MemID");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" and " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public decimal GetTotalCash(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select sum(CountPayCash) from MemCount where 1=1 ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" and " + strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			decimal result;
			if (obj == null)
			{
				result = 0m;
			}
			else
			{
				result = Convert.ToDecimal(obj);
			}
			return result;
		}
	}
}
