using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class MicroWebsiteOrderLog
	{
		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "MicroWebsiteOrderLog,Mem,SysShop,SysUser";
			string[] columns = new string[]
			{
				"MicroWebsiteOrderLog.*,Mem.MemName,Mem.MemCard,Mem.MemMoney,Mem.MemID,Mem.MemLevelID,SysShop.ShopName,SysUser.UserName,(select count(MicroOrderLogDetailID) from MicroWebsiteOrderLogDetail where MicroWebsiteOrderLogDetail.MicroOrderID= MicroWebsiteOrderLog.MicroOrderID ) as Count"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MicroOrderCreateTime", "MicroOrderID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetListSP1(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "MicroWebsiteOrderLog,Mem,SysShop";
			string[] columns = new string[]
			{
				"MicroWebsiteOrderLog.*,Mem.MemName,Mem.MemCard,Mem.MemID,Mem.MemLevelID,SysShop.ShopName,(select count(MicroOrderLogDetailID) from MicroWebsiteOrderLogDetail where MicroWebsiteOrderLogDetail.MicroOrderID= MicroWebsiteOrderLog.MicroOrderID ) as Count"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MicroOrderCreateTime", "MicroOrderID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public bool Exists(int MicroOrderID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MicroWebsiteOrderLog");
			strSql.Append(" where MicroOrderID=@MicroOrderID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroOrderID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MicroOrderID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.MicroWebsiteOrderLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MicroWebsiteOrderLog(");
			strSql.Append("MicroOrderAccount,MicroOrderType,MicroOrderMemID,MicroOrderTotalMoney,MicroOrderDiscountMoney,MicroOrderIsCard,MicroOrderPayCard,MicroOrderIsCash,MicroOrderPayCash,MicroOrderIsBink,MicroOrderPayBink,MicroOrderPayCoupon,MicroOrderPoint,MicroOrderRemark,MicroOrderShopID,MicroOrderUserID,MicroOrderCreateTime,MicroOldAccount,MicroOrderCardBalance,MicroOrderCardPoint,MicroOrderName,MicroOrderMobile,MicroOrderAdress,MicroOrderStatus,MicroOrderPassCreateTime,MicroOrderMark,MicroOrderPayCreateTime)");
			strSql.Append(" values (");
			strSql.Append("@MicroOrderAccount,@MicroOrderType,@MicroOrderMemID,@MicroOrderTotalMoney,@MicroOrderDiscountMoney,@MicroOrderIsCard,@MicroOrderPayCard,@MicroOrderIsCash,@MicroOrderPayCash,@MicroOrderIsBink,@MicroOrderPayBink,@MicroOrderPayCoupon,@MicroOrderPoint,@MicroOrderRemark,@MicroOrderShopID,@MicroOrderUserID,@MicroOrderCreateTime,@MicroOldAccount,@MicroOrderCardBalance,@MicroOrderCardPoint,@MicroOrderName,@MicroOrderMobile,@MicroOrderAdress,@MicroOrderStatus,@MicroOrderPassCreateTime,@MicroOrderMark,@MicroOrderPayCreateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroOrderAccount", SqlDbType.NVarChar, 50),
				new SqlParameter("@MicroOrderType", SqlDbType.TinyInt, 1),
				new SqlParameter("@MicroOrderMemID", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderTotalMoney", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderDiscountMoney", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderIsCard", SqlDbType.Bit, 1),
				new SqlParameter("@MicroOrderPayCard", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderIsCash", SqlDbType.Bit, 1),
				new SqlParameter("@MicroOrderPayCash", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderIsBink", SqlDbType.Bit, 1),
				new SqlParameter("@MicroOrderPayBink", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderPayCoupon", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderPoint", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@MicroOrderShopID", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderUserID", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderCreateTime", SqlDbType.DateTime),
				new SqlParameter("@MicroOldAccount", SqlDbType.NVarChar, 50),
				new SqlParameter("@MicroOrderCardBalance", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderCardPoint", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderName", SqlDbType.NVarChar, 50),
				new SqlParameter("@MicroOrderMobile", SqlDbType.NVarChar, 50),
				new SqlParameter("@MicroOrderAdress", SqlDbType.NVarChar, 500),
				new SqlParameter("@MicroOrderStatus", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderPassCreateTime", SqlDbType.DateTime),
				new SqlParameter("@MicroOrderMark", SqlDbType.NVarChar, 1000),
				new SqlParameter("@MicroOrderPayCreateTime", SqlDbType.DateTime)
			};
			parameters[0].Value = model.MicroOrderAccount;
			parameters[1].Value = model.MicroOrderType;
			parameters[2].Value = model.MicroOrderMemID;
			parameters[3].Value = model.MicroOrderTotalMoney;
			parameters[4].Value = model.MicroOrderDiscountMoney;
			parameters[5].Value = model.MicroOrderIsCard;
			parameters[6].Value = model.MicroOrderPayCard;
			parameters[7].Value = model.MicroOrderIsCash;
			parameters[8].Value = model.MicroOrderPayCash;
			parameters[9].Value = model.MicroOrderIsBink;
			parameters[10].Value = model.MicroOrderPayBink;
			parameters[11].Value = model.MicroOrderPayCoupon;
			parameters[12].Value = model.MicroOrderPoint;
			parameters[13].Value = model.MicroOrderRemark;
			parameters[14].Value = model.MicroOrderShopID;
			parameters[15].Value = model.MicroOrderUserID;
			parameters[16].Value = model.MicroOrderCreateTime;
			parameters[17].Value = model.MicroOldAccount;
			parameters[18].Value = model.MicroOrderCardBalance;
			parameters[19].Value = model.MicroOrderCardPoint;
			parameters[20].Value = model.MicroOrderName;
			parameters[21].Value = model.MicroOrderMobile;
			parameters[22].Value = model.MicroOrderAdress;
			parameters[23].Value = model.MicroOrderStatus;
			parameters[24].Value = model.MicroOrderPassCreateTime;
			parameters[25].Value = model.MicroOrderMark;
			parameters[26].Value = model.MicroOrderPayCreateTime;
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

		public bool Update(Chain.Model.MicroWebsiteOrderLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MicroWebsiteOrderLog set ");
			strSql.Append("MicroOrderAccount=@MicroOrderAccount,");
			strSql.Append("MicroOrderType=@MicroOrderType,");
			strSql.Append("MicroOrderMemID=@MicroOrderMemID,");
			strSql.Append("MicroOrderTotalMoney=@MicroOrderTotalMoney,");
			strSql.Append("MicroOrderDiscountMoney=@MicroOrderDiscountMoney,");
			strSql.Append("MicroOrderIsCard=@MicroOrderIsCard,");
			strSql.Append("MicroOrderPayCard=@MicroOrderPayCard,");
			strSql.Append("MicroOrderIsCash=@MicroOrderIsCash,");
			strSql.Append("MicroOrderPayCash=@MicroOrderPayCash,");
			strSql.Append("MicroOrderIsBink=@MicroOrderIsBink,");
			strSql.Append("MicroOrderPayBink=@MicroOrderPayBink,");
			strSql.Append("MicroOrderPayCoupon=@MicroOrderPayCoupon,");
			strSql.Append("MicroOrderPoint=@MicroOrderPoint,");
			strSql.Append("MicroOrderRemark=@MicroOrderRemark,");
			strSql.Append("MicroOrderShopID=@MicroOrderShopID,");
			strSql.Append("MicroOrderUserID=@MicroOrderUserID,");
			strSql.Append("MicroOrderCreateTime=@MicroOrderCreateTime,");
			strSql.Append("MicroOldAccount=@MicroOldAccount,");
			strSql.Append("MicroOrderCardBalance=@MicroOrderCardBalance,");
			strSql.Append("MicroOrderCardPoint=@MicroOrderCardPoint,");
			strSql.Append("MicroOrderName=@MicroOrderName,");
			strSql.Append("MicroOrderMobile=@MicroOrderMobile,");
			strSql.Append("MicroOrderAdress=@MicroOrderAdress,");
			strSql.Append("MicroOrderStatus=@MicroOrderStatus,");
			strSql.Append("MicroOrderPassCreateTime=@MicroOrderPassCreateTime,");
			strSql.Append("MicroOrderMark=@MicroOrderMark,");
			strSql.Append("MicroOrderPayCreateTime=@MicroOrderPayCreateTime");
			strSql.Append(" where MicroOrderID=@MicroOrderID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroOrderAccount", SqlDbType.NVarChar, 50),
				new SqlParameter("@MicroOrderType", SqlDbType.TinyInt, 1),
				new SqlParameter("@MicroOrderMemID", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderTotalMoney", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderDiscountMoney", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderIsCard", SqlDbType.Bit, 1),
				new SqlParameter("@MicroOrderPayCard", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderIsCash", SqlDbType.Bit, 1),
				new SqlParameter("@MicroOrderPayCash", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderIsBink", SqlDbType.Bit, 1),
				new SqlParameter("@MicroOrderPayBink", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderPayCoupon", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderPoint", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@MicroOrderShopID", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderUserID", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderCreateTime", SqlDbType.DateTime),
				new SqlParameter("@MicroOldAccount", SqlDbType.NVarChar, 50),
				new SqlParameter("@MicroOrderCardBalance", SqlDbType.Money, 8),
				new SqlParameter("@MicroOrderCardPoint", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderName", SqlDbType.NVarChar, 50),
				new SqlParameter("@MicroOrderMobile", SqlDbType.NVarChar, 50),
				new SqlParameter("@MicroOrderAdress", SqlDbType.NVarChar, 500),
				new SqlParameter("@MicroOrderStatus", SqlDbType.Int, 4),
				new SqlParameter("@MicroOrderPassCreateTime", SqlDbType.DateTime),
				new SqlParameter("@MicroOrderMark", SqlDbType.NVarChar, 1000),
				new SqlParameter("@MicroOrderPayCreateTime", SqlDbType.DateTime),
				new SqlParameter("@MicroOrderID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MicroOrderAccount;
			parameters[1].Value = model.MicroOrderType;
			parameters[2].Value = model.MicroOrderMemID;
			parameters[3].Value = model.MicroOrderTotalMoney;
			parameters[4].Value = model.MicroOrderDiscountMoney;
			parameters[5].Value = model.MicroOrderIsCard;
			parameters[6].Value = model.MicroOrderPayCard;
			parameters[7].Value = model.MicroOrderIsCash;
			parameters[8].Value = model.MicroOrderPayCash;
			parameters[9].Value = model.MicroOrderIsBink;
			parameters[10].Value = model.MicroOrderPayBink;
			parameters[11].Value = model.MicroOrderPayCoupon;
			parameters[12].Value = model.MicroOrderPoint;
			parameters[13].Value = model.MicroOrderRemark;
			parameters[14].Value = model.MicroOrderShopID;
			parameters[15].Value = model.MicroOrderUserID;
			parameters[16].Value = model.MicroOrderCreateTime;
			parameters[17].Value = model.MicroOldAccount;
			parameters[18].Value = model.MicroOrderCardBalance;
			parameters[19].Value = model.MicroOrderCardPoint;
			parameters[20].Value = model.MicroOrderName;
			parameters[21].Value = model.MicroOrderMobile;
			parameters[22].Value = model.MicroOrderAdress;
			parameters[23].Value = model.MicroOrderStatus;
			parameters[24].Value = model.MicroOrderPassCreateTime;
			parameters[25].Value = model.MicroOrderMark;
			parameters[26].Value = model.MicroOrderPayCreateTime;
			parameters[27].Value = model.MicroOrderID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int MicroOrderID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MicroWebsiteOrderLog ");
			strSql.Append(" where MicroOrderID=@MicroOrderID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroOrderID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MicroOrderID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string MicroOrderIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MicroWebsiteOrderLog ");
			strSql.Append(" where MicroOrderID in (" + MicroOrderIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MicroWebsiteOrderLog GetModel(int MicroOrderID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 MicroOrderID,MicroOrderAccount,MicroOrderType,MicroOrderMemID,MicroOrderTotalMoney,MicroOrderDiscountMoney,MicroOrderIsCard,MicroOrderPayCard,MicroOrderIsCash,MicroOrderPayCash,MicroOrderIsBink,MicroOrderPayBink,MicroOrderPayCoupon,MicroOrderPoint,MicroOrderRemark,MicroOrderShopID,MicroOrderUserID,MicroOrderCreateTime,MicroOldAccount,MicroOrderCardBalance,MicroOrderCardPoint,MicroOrderName,MicroOrderMobile,MicroOrderAdress,MicroOrderStatus,MicroOrderPassCreateTime,MicroOrderMark,MicroOrderPayCreateTime from MicroWebsiteOrderLog ");
			strSql.Append(" where MicroOrderID=@MicroOrderID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MicroOrderID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MicroOrderID;
			Chain.Model.MicroWebsiteOrderLog model = new Chain.Model.MicroWebsiteOrderLog();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MicroWebsiteOrderLog result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["MicroOrderID"] != null && ds.Tables[0].Rows[0]["MicroOrderID"].ToString() != "")
				{
					model.MicroOrderID = int.Parse(ds.Tables[0].Rows[0]["MicroOrderID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderAccount"] != null && ds.Tables[0].Rows[0]["MicroOrderAccount"].ToString() != "")
				{
					model.MicroOrderAccount = ds.Tables[0].Rows[0]["MicroOrderAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MicroOrderType"] != null && ds.Tables[0].Rows[0]["MicroOrderType"].ToString() != "")
				{
					model.MicroOrderType = int.Parse(ds.Tables[0].Rows[0]["MicroOrderType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderMemID"] != null && ds.Tables[0].Rows[0]["MicroOrderMemID"].ToString() != "")
				{
					model.MicroOrderMemID = int.Parse(ds.Tables[0].Rows[0]["MicroOrderMemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderTotalMoney"] != null && ds.Tables[0].Rows[0]["MicroOrderTotalMoney"].ToString() != "")
				{
					model.MicroOrderTotalMoney = decimal.Parse(ds.Tables[0].Rows[0]["MicroOrderTotalMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderDiscountMoney"] != null && ds.Tables[0].Rows[0]["MicroOrderDiscountMoney"].ToString() != "")
				{
					model.MicroOrderDiscountMoney = decimal.Parse(ds.Tables[0].Rows[0]["MicroOrderDiscountMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderIsCard"] != null && ds.Tables[0].Rows[0]["MicroOrderIsCard"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["MicroOrderIsCard"].ToString() == "1" || ds.Tables[0].Rows[0]["MicroOrderIsCard"].ToString().ToLower() == "true")
					{
						model.MicroOrderIsCard = true;
					}
					else
					{
						model.MicroOrderIsCard = false;
					}
				}
				if (ds.Tables[0].Rows[0]["MicroOrderPayCard"] != null && ds.Tables[0].Rows[0]["MicroOrderPayCard"].ToString() != "")
				{
					model.MicroOrderPayCard = decimal.Parse(ds.Tables[0].Rows[0]["MicroOrderPayCard"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderIsCash"] != null && ds.Tables[0].Rows[0]["MicroOrderIsCash"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["MicroOrderIsCash"].ToString() == "1" || ds.Tables[0].Rows[0]["MicroOrderIsCash"].ToString().ToLower() == "true")
					{
						model.MicroOrderIsCash = true;
					}
					else
					{
						model.MicroOrderIsCash = false;
					}
				}
				if (ds.Tables[0].Rows[0]["MicroOrderPayCash"] != null && ds.Tables[0].Rows[0]["MicroOrderPayCash"].ToString() != "")
				{
					model.MicroOrderPayCash = decimal.Parse(ds.Tables[0].Rows[0]["MicroOrderPayCash"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderIsBink"] != null && ds.Tables[0].Rows[0]["MicroOrderIsBink"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["MicroOrderIsBink"].ToString() == "1" || ds.Tables[0].Rows[0]["MicroOrderIsBink"].ToString().ToLower() == "true")
					{
						model.MicroOrderIsBink = true;
					}
					else
					{
						model.MicroOrderIsBink = false;
					}
				}
				if (ds.Tables[0].Rows[0]["MicroOrderPayBink"] != null && ds.Tables[0].Rows[0]["MicroOrderPayBink"].ToString() != "")
				{
					model.MicroOrderPayBink = decimal.Parse(ds.Tables[0].Rows[0]["MicroOrderPayBink"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderPayCoupon"] != null && ds.Tables[0].Rows[0]["MicroOrderPayCoupon"].ToString() != "")
				{
					model.MicroOrderPayCoupon = decimal.Parse(ds.Tables[0].Rows[0]["MicroOrderPayCoupon"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderPoint"] != null && ds.Tables[0].Rows[0]["MicroOrderPoint"].ToString() != "")
				{
					model.MicroOrderPoint = int.Parse(ds.Tables[0].Rows[0]["MicroOrderPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderRemark"] != null && ds.Tables[0].Rows[0]["MicroOrderRemark"].ToString() != "")
				{
					model.MicroOrderRemark = ds.Tables[0].Rows[0]["MicroOrderRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MicroOrderShopID"] != null && ds.Tables[0].Rows[0]["MicroOrderShopID"].ToString() != "")
				{
					model.MicroOrderShopID = int.Parse(ds.Tables[0].Rows[0]["MicroOrderShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderUserID"] != null && ds.Tables[0].Rows[0]["MicroOrderUserID"].ToString() != "")
				{
					model.MicroOrderUserID = int.Parse(ds.Tables[0].Rows[0]["MicroOrderUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderCreateTime"] != null && ds.Tables[0].Rows[0]["MicroOrderCreateTime"].ToString() != "")
				{
					model.MicroOrderCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["MicroOrderCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOldAccount"] != null && ds.Tables[0].Rows[0]["MicroOldAccount"].ToString() != "")
				{
					model.MicroOldAccount = ds.Tables[0].Rows[0]["MicroOldAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MicroOrderCardBalance"] != null && ds.Tables[0].Rows[0]["MicroOrderCardBalance"].ToString() != "")
				{
					model.MicroOrderCardBalance = decimal.Parse(ds.Tables[0].Rows[0]["MicroOrderCardBalance"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderCardPoint"] != null && ds.Tables[0].Rows[0]["MicroOrderCardPoint"].ToString() != "")
				{
					model.MicroOrderCardPoint = int.Parse(ds.Tables[0].Rows[0]["MicroOrderCardPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderName"] != null && ds.Tables[0].Rows[0]["MicroOrderName"].ToString() != "")
				{
					model.MicroOrderName = ds.Tables[0].Rows[0]["MicroOrderName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MicroOrderMobile"] != null && ds.Tables[0].Rows[0]["MicroOrderMobile"].ToString() != "")
				{
					model.MicroOrderMobile = ds.Tables[0].Rows[0]["MicroOrderMobile"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MicroOrderAdress"] != null && ds.Tables[0].Rows[0]["MicroOrderAdress"].ToString() != "")
				{
					model.MicroOrderAdress = ds.Tables[0].Rows[0]["MicroOrderAdress"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MicroOrderStatus"] != null && ds.Tables[0].Rows[0]["MicroOrderStatus"].ToString() != "")
				{
					model.MicroOrderStatus = int.Parse(ds.Tables[0].Rows[0]["MicroOrderStatus"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderPassCreateTime"] != null && ds.Tables[0].Rows[0]["MicroOrderPassCreateTime"].ToString() != "")
				{
					model.MicroOrderPassCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["MicroOrderPassCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MicroOrderMark"] != null && ds.Tables[0].Rows[0]["MicroOrderMark"].ToString() != "")
				{
					model.MicroOrderMark = ds.Tables[0].Rows[0]["MicroOrderMark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MicroOrderPayCreateTime"] != null && ds.Tables[0].Rows[0]["MicroOrderPayCreateTime"].ToString() != "")
				{
					model.MicroOrderPayCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["MicroOrderPayCreateTime"].ToString());
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
			strSql.Append("select MicroOrderID,MicroOrderAccount,MicroOrderType,MicroOrderMemID,MicroOrderTotalMoney,MicroOrderDiscountMoney,MicroOrderIsCard,MicroOrderPayCard,MicroOrderIsCash,MicroOrderPayCash,MicroOrderIsBink,MicroOrderPayBink,MicroOrderPayCoupon,MicroOrderPoint,MicroOrderRemark,MicroOrderShopID,MicroOrderUserID,MicroOrderCreateTime,MicroOldAccount,MicroOrderCardBalance,MicroOrderCardPoint,MicroOrderName,MicroOrderMobile,MicroOrderAdress,MicroOrderStatus,MicroOrderPassCreateTime,MicroOrderMark,MicroOrderPayCreateTime ");
			strSql.Append(" FROM MicroWebsiteOrderLog ");
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
			strSql.Append(" MicroOrderID,MicroOrderAccount,MicroOrderType,MicroOrderMemID,MicroOrderTotalMoney,MicroOrderDiscountMoney,MicroOrderIsCard,MicroOrderPayCard,MicroOrderIsCash,MicroOrderPayCash,MicroOrderIsBink,MicroOrderPayBink,MicroOrderPayCoupon,MicroOrderPoint,MicroOrderRemark,MicroOrderShopID,MicroOrderUserID,MicroOrderCreateTime,MicroOldAccount,MicroOrderCardBalance,MicroOrderCardPoint,MicroOrderName,MicroOrderMobile,MicroOrderAdress,MicroOrderStatus,MicroOrderPassCreateTime,MicroOrderMark,MicroOrderPayCreateTime ");
			strSql.Append(" FROM MicroWebsiteOrderLog ");
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
			strSql.Append("select count(1) FROM MicroWebsiteOrderLog ");
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
				strSql.Append("order by T.MicroOrderID desc");
			}
			strSql.Append(")AS Row, T.*  from MicroWebsiteOrderLog T ");
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
