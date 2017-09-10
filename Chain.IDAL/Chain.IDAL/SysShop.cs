using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysShop
	{
		public bool Exists(int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysShop");
			strSql.Append(" where ShopID=@ShopID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ShopID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.SysShop model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysShop(");
			strSql.Append("IsMain,RechargeMaxMoney,IsRecharge,ShopImageUrl,ShopProvince,ShopCity,ShopCounty,ShopName,ShopTelephone,ShopContactMan,ShopAreaID,ShopAddress,ShopRemark,ShopCreateTime,ShopState,ShopPrintTitle,ShopPrintFoot,ShopSmsName,SettlementInterval,ShopProportion,IsAllianceProgram,FatherShopID,PointCount,SmsCount,PointType,SmsType,RechargeProportion,ShopType,TotalRate)");
			strSql.Append(" values (");
			strSql.Append("@IsMain,@RechargeMaxMoney,@IsRecharge,@ShopImageUrl,@ShopProvince,@ShopCity,@ShopCounty,@ShopName,@ShopTelephone,@ShopContactMan,@ShopAreaID,@ShopAddress,@ShopRemark,@ShopCreateTime,@ShopState,@ShopPrintTitle,@ShopPrintFoot,@ShopSmsName,@SettlementInterval,@ShopProportion,@IsAllianceProgram,@FatherShopID,@PointCount,@SmsCount,@PointType,@SmsType,@RechargeProportion,@ShopType,@TotalRate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopName", SqlDbType.NVarChar, 100),
				new SqlParameter("@ShopTelephone", SqlDbType.VarChar, 50),
				new SqlParameter("@ShopContactMan", SqlDbType.NVarChar, 50),
				new SqlParameter("@ShopAreaID", SqlDbType.Int, 4),
				new SqlParameter("@ShopAddress", SqlDbType.NVarChar, 500),
				new SqlParameter("@ShopRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@ShopCreateTime", SqlDbType.DateTime),
				new SqlParameter("@ShopState", SqlDbType.Bit, 1),
				new SqlParameter("@ShopPrintTitle", SqlDbType.NVarChar, 500),
				new SqlParameter("@ShopPrintFoot", SqlDbType.NVarChar, 500),
				new SqlParameter("@ShopSmsName", SqlDbType.NVarChar, 500),
				new SqlParameter("@SettlementInterval", SqlDbType.Int, 4),
				new SqlParameter("@ShopProportion", SqlDbType.Decimal, 8),
				new SqlParameter("@IsAllianceProgram", SqlDbType.Bit, 1),
				new SqlParameter("@FatherShopID", SqlDbType.Int, 4),
				new SqlParameter("@PointCount", SqlDbType.Int, 4),
				new SqlParameter("@SmsCount", SqlDbType.Int, 4),
				new SqlParameter("@PointType", SqlDbType.Int, 4),
				new SqlParameter("@SmsType", SqlDbType.Int, 4),
				new SqlParameter("@ShopProvince", SqlDbType.Int, 4),
				new SqlParameter("@ShopCity", SqlDbType.Int, 4),
				new SqlParameter("@ShopCounty", SqlDbType.Int, 4),
				new SqlParameter("@ShopImageUrl", SqlDbType.NVarChar, 200),
				new SqlParameter("@RechargeProportion", SqlDbType.Decimal, 8),
				new SqlParameter("@ShopType", SqlDbType.Int, 4),
				new SqlParameter("@TotalRate", SqlDbType.Decimal, 8),
				new SqlParameter("@RechargeMaxMoney", SqlDbType.Decimal, 8),
				new SqlParameter("@IsRecharge", SqlDbType.Bit, 1),
				new SqlParameter("@IsMain", SqlDbType.Bit, 1)
			};
			parameters[0].Value = model.ShopName;
			parameters[1].Value = model.ShopTelephone;
			parameters[2].Value = model.ShopContactMan;
			parameters[3].Value = model.ShopAreaID;
			parameters[4].Value = model.ShopAddress;
			parameters[5].Value = model.ShopRemark;
			parameters[6].Value = model.ShopCreateTime;
			parameters[7].Value = model.ShopState;
			parameters[8].Value = model.ShopPrintTitle;
			parameters[9].Value = model.ShopPrintFoot;
			parameters[10].Value = model.ShopSmsName;
			parameters[11].Value = model.SettlementInterval;
			parameters[12].Value = model.ShopProportion;
			parameters[13].Value = model.IsAllianceProgram;
			parameters[14].Value = model.FatherShopID;
			parameters[15].Value = model.PointCount;
			parameters[16].Value = model.SmsCount;
			parameters[17].Value = model.PointType;
			parameters[18].Value = model.SmsType;
			parameters[19].Value = model.ShopProvince;
			parameters[20].Value = model.ShopCity;
			parameters[21].Value = model.ShopCounty;
			parameters[22].Value = model.ShopImageUrl;
			parameters[23].Value = model.RechargeProportion;
			parameters[24].Value = model.ShopType;
			parameters[25].Value = model.TotalRate;
			parameters[26].Value = model.RechargeMaxMoney;
			parameters[27].Value = model.IsRecharge;
			parameters[28].Value = model.IsMain;
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

		public int Update(Chain.Model.SysShop model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysShop set ");
			strSql.Append("IsMain=@IsMain,");
			strSql.Append("IsRecharge=@IsRecharge,");
			strSql.Append("RechargeMaxMoney=@RechargeMaxMoney,");
			strSql.Append("ShopName=@ShopName,");
			strSql.Append("ShopTelephone=@ShopTelephone,");
			strSql.Append("ShopContactMan=@ShopContactMan,");
			strSql.Append("ShopAreaID=@ShopAreaID,");
			strSql.Append("ShopAddress=@ShopAddress,");
			strSql.Append("ShopRemark=@ShopRemark,");
			strSql.Append("ShopState=@ShopState,");
			strSql.Append("ShopPrintTitle=@ShopPrintTitle,");
			strSql.Append("ShopPrintFoot=@ShopPrintFoot,");
			strSql.Append("ShopSmsName=@ShopSmsName,");
			strSql.Append("SettlementInterval=@SettlementInterval,");
			strSql.Append("ShopProportion=@ShopProportion,");
			strSql.Append("PointCount=@PointCount,");
			strSql.Append("SmsCount=@SmsCount,");
			strSql.Append("PointType=@PointType,");
			strSql.Append("SmsType=@SmsType,");
			strSql.Append("ShopProvince=@ShopProvince,");
			strSql.Append("ShopCity=@ShopCity,");
			strSql.Append("ShopCounty=@ShopCounty,");
			strSql.Append("ShopImageUrl=@ShopImageUrl,");
			strSql.Append("RechargeProportion=@RechargeProportion,");
			strSql.Append("TotalRate=@TotalRate");
			strSql.Append(" where ShopID=@ShopID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopName", SqlDbType.NVarChar, 100),
				new SqlParameter("@ShopTelephone", SqlDbType.VarChar, 50),
				new SqlParameter("@ShopContactMan", SqlDbType.NVarChar, 50),
				new SqlParameter("@ShopAreaID", SqlDbType.Int, 4),
				new SqlParameter("@ShopAddress", SqlDbType.NVarChar, 500),
				new SqlParameter("@ShopRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@ShopState", SqlDbType.Bit, 1),
				new SqlParameter("@ShopPrintTitle", SqlDbType.NVarChar, 500),
				new SqlParameter("@ShopPrintFoot", SqlDbType.NVarChar, 500),
				new SqlParameter("@ShopSmsName", SqlDbType.NVarChar, 500),
				new SqlParameter("@SettlementInterval", SqlDbType.Int, 4),
				new SqlParameter("@ShopProportion", SqlDbType.Decimal, 8),
				new SqlParameter("@PointCount", SqlDbType.Int, 4),
				new SqlParameter("@SmsCount", SqlDbType.Int, 4),
				new SqlParameter("@PointType", SqlDbType.Int, 4),
				new SqlParameter("@SmsType", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4),
				new SqlParameter("@ShopProvince", SqlDbType.Int, 4),
				new SqlParameter("@ShopCity", SqlDbType.Int, 4),
				new SqlParameter("@ShopCounty", SqlDbType.Int, 4),
				new SqlParameter("@ShopImageUrl", SqlDbType.NVarChar, 200),
				new SqlParameter("@RechargeProportion", SqlDbType.Decimal, 200),
				new SqlParameter("@TotalRate", SqlDbType.Decimal, 8),
				new SqlParameter("@RechargeMaxMoney", SqlDbType.Decimal, 8),
				new SqlParameter("@IsRecharge", SqlDbType.Bit, 1),
				new SqlParameter("@IsMain", SqlDbType.Bit, 1)
			};
			parameters[0].Value = model.ShopName;
			parameters[1].Value = model.ShopTelephone;
			parameters[2].Value = model.ShopContactMan;
			parameters[3].Value = model.ShopAreaID;
			parameters[4].Value = model.ShopAddress;
			parameters[5].Value = model.ShopRemark;
			parameters[6].Value = model.ShopState;
			parameters[7].Value = model.ShopPrintTitle;
			parameters[8].Value = model.ShopPrintFoot;
			parameters[9].Value = model.ShopSmsName;
			parameters[10].Value = model.SettlementInterval;
			parameters[11].Value = model.ShopProportion;
			parameters[12].Value = model.PointCount;
			parameters[13].Value = model.SmsCount;
			parameters[14].Value = model.PointType;
			parameters[15].Value = model.SmsType;
			parameters[16].Value = model.ShopID;
			parameters[17].Value = model.ShopProvince;
			parameters[18].Value = model.ShopCity;
			parameters[19].Value = model.ShopCounty;
			parameters[20].Value = model.ShopImageUrl;
			parameters[21].Value = model.RechargeProportion;
			parameters[22].Value = model.TotalRate;
			parameters[23].Value = model.RechargeMaxMoney;
			parameters[24].Value = model.IsRecharge;
			parameters[25].Value = model.IsMain;
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

		public int GetShopIDByWhere(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select top 1 ShopID from SysShop ");
			strSql.Append(" where  " + strWhere);
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			int result;
			if (obj != null)
			{
				result = int.Parse(obj.ToString());
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public string GetShopNamebyShopID(int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select shopName from SysShop ");
			strSql.Append(" where ShopID = " + ShopID);
			return DbHelperSQL.GetSingle(strSql.ToString()).ToString();
		}

		public bool Delete(int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysShop ");
			strSql.Append(" where ShopID=@ShopID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ShopID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string ShopIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysShop ");
			strSql.Append(" where ShopID in (" + ShopIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysShop GetModel(int ShopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ShopType,IsMain,IsRecharge,RechargeMaxMoney,ShopImageUrl,isnull(ShopProvince,0)ShopProvince,isnull(ShopCity,0)ShopCity,isnull(ShopCounty,0)ShopCounty,TotalRate,ShopID,ShopName,ShopTelephone,ShopContactMan,ShopAreaID,ShopAddress,ShopRemark,ShopCreateTime,ShopState,ShopPrintTitle,ShopPrintFoot,ShopSmsName,SettlementInterval,ShopProportion,IsAllianceProgram,FatherShopID,PointCount,SmsCount,PointType,SmsType from SysShop ");
			strSql.Append(" where ShopID=@ShopID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ShopID;
			Chain.Model.SysShop model = new Chain.Model.SysShop();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysShop result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ShopType"] != null && ds.Tables[0].Rows[0]["ShopType"].ToString() != "")
				{
					model.ShopType = int.Parse(ds.Tables[0].Rows[0]["ShopType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RechargeMaxMoney"] != null && ds.Tables[0].Rows[0]["RechargeMaxMoney"].ToString() != "")
				{
					model.RechargeMaxMoney = decimal.Parse(ds.Tables[0].Rows[0]["RechargeMaxMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ShopImageUrl"] != null && ds.Tables[0].Rows[0]["ShopImageUrl"].ToString() != "")
				{
					model.ShopImageUrl = ds.Tables[0].Rows[0]["ShopImageUrl"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ShopProvince"] != null && ds.Tables[0].Rows[0]["ShopProvince"].ToString() != "")
				{
					model.ShopProvince = int.Parse(ds.Tables[0].Rows[0]["ShopProvince"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ShopCity"] != null && ds.Tables[0].Rows[0]["ShopCity"].ToString() != "")
				{
					model.ShopCity = int.Parse(ds.Tables[0].Rows[0]["ShopCity"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ShopCounty"] != null && ds.Tables[0].Rows[0]["ShopCounty"].ToString() != "")
				{
					model.ShopCounty = int.Parse(ds.Tables[0].Rows[0]["ShopCounty"].ToString());
				}
				if (ds.Tables[0].Rows[0]["TotalRate"] != null && ds.Tables[0].Rows[0]["TotalRate"].ToString() != "")
				{
					model.TotalRate = decimal.Parse(ds.Tables[0].Rows[0]["TotalRate"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ShopID"] != null && ds.Tables[0].Rows[0]["ShopID"].ToString() != "")
				{
					model.ShopID = int.Parse(ds.Tables[0].Rows[0]["ShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ShopName"] != null && ds.Tables[0].Rows[0]["ShopName"].ToString() != "")
				{
					model.ShopName = ds.Tables[0].Rows[0]["ShopName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ShopTelephone"] != null && ds.Tables[0].Rows[0]["ShopTelephone"].ToString() != "")
				{
					model.ShopTelephone = ds.Tables[0].Rows[0]["ShopTelephone"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ShopContactMan"] != null && ds.Tables[0].Rows[0]["ShopContactMan"].ToString() != "")
				{
					model.ShopContactMan = ds.Tables[0].Rows[0]["ShopContactMan"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ShopAreaID"] != null && ds.Tables[0].Rows[0]["ShopAreaID"].ToString() != "")
				{
					model.ShopAreaID = int.Parse(ds.Tables[0].Rows[0]["ShopAreaID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ShopAddress"] != null && ds.Tables[0].Rows[0]["ShopAddress"].ToString() != "")
				{
					model.ShopAddress = ds.Tables[0].Rows[0]["ShopAddress"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ShopRemark"] != null && ds.Tables[0].Rows[0]["ShopRemark"].ToString() != "")
				{
					model.ShopRemark = ds.Tables[0].Rows[0]["ShopRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ShopCreateTime"] != null && ds.Tables[0].Rows[0]["ShopCreateTime"].ToString() != "")
				{
					model.ShopCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["ShopCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ShopState"] != null && ds.Tables[0].Rows[0]["ShopState"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["ShopState"].ToString() == "1" || ds.Tables[0].Rows[0]["ShopState"].ToString().ToLower() == "true")
					{
						model.ShopState = true;
					}
					else
					{
						model.ShopState = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsMain"] != null && ds.Tables[0].Rows[0]["IsMain"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsMain"].ToString() == "1" || ds.Tables[0].Rows[0]["IsMain"].ToString().ToLower() == "true")
					{
						model.IsMain = true;
					}
					else
					{
						model.IsMain = false;
					}
				}
				if (ds.Tables[0].Rows[0]["IsRecharge"] != null && ds.Tables[0].Rows[0]["IsRecharge"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsRecharge"].ToString() == "1" || ds.Tables[0].Rows[0]["IsRecharge"].ToString().ToLower() == "true")
					{
						model.IsRecharge = true;
					}
					else
					{
						model.IsRecharge = false;
					}
				}
				if (ds.Tables[0].Rows[0]["ShopPrintTitle"] != null && ds.Tables[0].Rows[0]["ShopPrintTitle"].ToString() != "")
				{
					model.ShopPrintTitle = ds.Tables[0].Rows[0]["ShopPrintTitle"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ShopPrintFoot"] != null && ds.Tables[0].Rows[0]["ShopPrintFoot"].ToString() != "")
				{
					model.ShopPrintFoot = ds.Tables[0].Rows[0]["ShopPrintFoot"].ToString();
				}
				if (ds.Tables[0].Rows[0]["ShopSmsName"] != null && ds.Tables[0].Rows[0]["ShopSmsName"].ToString() != "")
				{
					model.ShopSmsName = ds.Tables[0].Rows[0]["ShopSmsName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["SettlementInterval"] != null && ds.Tables[0].Rows[0]["SettlementInterval"].ToString() != "")
				{
					model.SettlementInterval = int.Parse(ds.Tables[0].Rows[0]["SettlementInterval"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ShopProportion"] != null && ds.Tables[0].Rows[0]["ShopProportion"].ToString() != "")
				{
					model.ShopProportion = decimal.Parse(ds.Tables[0].Rows[0]["ShopProportion"].ToString());
				}
				if (ds.Tables[0].Rows[0]["IsAllianceProgram"] != null && ds.Tables[0].Rows[0]["IsAllianceProgram"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["IsAllianceProgram"].ToString() == "1" || ds.Tables[0].Rows[0]["IsAllianceProgram"].ToString().ToLower() == "true")
					{
						model.IsAllianceProgram = true;
					}
					else
					{
						model.IsAllianceProgram = false;
					}
				}
				if (ds.Tables[0].Rows[0]["FatherShopID"] != null && ds.Tables[0].Rows[0]["FatherShopID"].ToString() != "")
				{
					model.FatherShopID = int.Parse(ds.Tables[0].Rows[0]["FatherShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PointCount"] != null && ds.Tables[0].Rows[0]["PointCount"].ToString() != "")
				{
					model.PointCount = Convert.ToInt32(ds.Tables[0].Rows[0]["PointCount"]);
				}
				if (ds.Tables[0].Rows[0]["SmsCount"] != null && ds.Tables[0].Rows[0]["SmsCount"].ToString() != "")
				{
					model.SmsCount = Convert.ToInt32(ds.Tables[0].Rows[0]["SmsCount"]);
				}
				if (ds.Tables[0].Rows[0]["PointType"] != null && ds.Tables[0].Rows[0]["PointType"].ToString() != "")
				{
					model.PointType = Convert.ToInt32(ds.Tables[0].Rows[0]["PointType"]);
				}
				if (ds.Tables[0].Rows[0]["SmsType"] != null && ds.Tables[0].Rows[0]["SmsType"].ToString() != "")
				{
					model.SmsType = Convert.ToInt32(ds.Tables[0].Rows[0]["SmsType"]);
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
			strSql.Append("select  * ");
			strSql.Append(" FROM SysShop ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListShop(int PageSize, int PageIndex, out int resCount, string strsql, params string[] strWhere)
		{
			string tableName = "SysShop";
			string[] columns = new string[]
			{
				string.Concat(new string[]
				{
					"ShopName,ShopID,PointCount,FatherShopID,\r\n(select count(*) from OrderLog,Mem where OrderLog.OrderMemID=Mem.MemID and  OrderShopID=SysShop.ShopID ",
					strsql,
					") as TotalNumber,(select isnull(sum(OrderDiscountMoney),0) from OrderLog,Mem where OrderLog.OrderMemID=Mem.MemID and    OrderShopID=SysShop.ShopID ",
					strsql,
					") as TotalMoney,(select count(*) from  OrderLog,Mem where OrderLog.OrderMemID=Mem.MemID and   OrderShopID=SysShop.ShopID ",
					strsql,
					" ) as MemNumber,(select isnull(sum(OrderDiscountMoney),0) from OrderLog,Mem where OrderLog.OrderMemID=Mem.MemID and   OrderShopID=SysShop.ShopID ",
					strsql,
					") as MemMoney,(select count(*) from OrderLog,Mem where OrderLog.OrderMemID=Mem.MemID and  MemShopID=SysShop.ShopID ",
					strsql,
					") as MemTotalNumber,(select isnull(sum(OrderDiscountMoney),0) from OrderLog,Mem where OrderLog.OrderMemID=Mem.MemID and   MemShopID=SysShop.ShopID ",
					strsql,
					") as MemTotalMoney"
				})
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ShopID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetAllianceListShop(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "SysShop";
			string[] columns = new string[]
			{
				" ShopName,SysShop.ShopID,PointCount,ShopState,ShopCreateTime,SmsCount,\r\n(select count(*) from Mem  where Mem.MemShopId=SysShop.ShopID ) as MemCount,(select count(*) from SysShop as AllianceShop  where AllianceShop.FatherShopID=SysShop.ShopID ) as ShopCount"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ShopID", true, PageSize, PageIndex, recordCount);
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
			strSql.Append(" ShopImageUrl,isnull(ShopProvince,0)ShopProvince,isnull(ShopCity,0)ShopCity,isnull(ShopCounty,0)ShopCounty,ShopID,ShopName,ShopTelephone,ShopContactMan,ShopAreaID,ShopAddress,ShopRemark,ShopCreateTime,ShopState,ShopPrintTitle,ShopPrintFoot,ShopSmsName,SettlementInterval,ShopProportion,IsAllianceProgram,FatherShopID,PointCount,SmsCount,PointType,SmsType  ");
			strSql.Append(" FROM SysShop ");
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
			strSql.Append("select count(1) FROM SysShop ");
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
				strSql.Append("order by T.ShopID desc");
			}
			strSql.Append(")AS Row, T.*  from SysShop T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public int Update(int shopid, int smsType, int pointType)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysShop set ");
			strSql.Append("PointType=@PointType,");
			strSql.Append("SmsType=@SmsType");
			strSql.Append(" where ShopID=@ShopID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PointType", SqlDbType.Int, 4),
				new SqlParameter("@SmsType", SqlDbType.Int, 4),
				new SqlParameter("@ShopID", SqlDbType.Int, 4)
			};
			parameters[0].Value = pointType;
			parameters[1].Value = smsType;
			parameters[2].Value = shopid;
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

		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ShopID", "SysShop");
		}

		public bool Exists(string shopName)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysShop");
			strSql.Append(" where ShopName=@ShopName");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopName", SqlDbType.NVarChar, 100)
			};
			parameters[0].Value = shopName;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(string shopName, int shopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysShop");
			strSql.Append(" where ShopName = @ShopName and ShopID<>@ShopID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ShopName", SqlDbType.VarChar, 50),
				new SqlParameter("@ShopID", SqlDbType.Int)
			};
			parameters[0].Value = shopName;
			parameters[1].Value = shopID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

        public bool Exists_ShopContactMan(string ShopContactMan)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SysShop");
            strSql.Append(" where ShopContactMan = @ShopContactMan");
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ShopContactMan", SqlDbType.VarChar, 50),
            };
            parameters[0].Value = ShopContactMan;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public Chain.Model.SysShop DataRowToModel(DataRow row)
		{
			Chain.Model.SysShop model = new Chain.Model.SysShop();
			if (row != null)
			{
				if (row["ShopID"] != null && row["ShopID"].ToString() != "")
				{
					model.ShopID = int.Parse(row["ShopID"].ToString());
				}
				if (row["ShopName"] != null)
				{
					model.ShopName = row["ShopName"].ToString();
				}
				if (row["ShopTelephone"] != null)
				{
					model.ShopTelephone = row["ShopTelephone"].ToString();
				}
				if (row["ShopContactMan"] != null)
				{
					model.ShopContactMan = row["ShopContactMan"].ToString();
				}
				if (row["ShopAreaID"] != null && row["ShopAreaID"].ToString() != "")
				{
					model.ShopAreaID = int.Parse(row["ShopAreaID"].ToString());
				}
				if (row["ShopAddress"] != null)
				{
					model.ShopAddress = row["ShopAddress"].ToString();
				}
				if (row["ShopRemark"] != null)
				{
					model.ShopRemark = row["ShopRemark"].ToString();
				}
				if (row["ShopCreateTime"] != null && row["ShopCreateTime"].ToString() != "")
				{
					model.ShopCreateTime = DateTime.Parse(row["ShopCreateTime"].ToString());
				}
				if (row["ShopState"] != null && row["ShopState"].ToString() != "")
				{
					if (row["ShopState"].ToString() == "1" || row["ShopState"].ToString().ToLower() == "true")
					{
						model.ShopState = true;
					}
					else
					{
						model.ShopState = false;
					}
				}
				if (row["ShopPrintTitle"] != null)
				{
					model.ShopPrintTitle = row["ShopPrintTitle"].ToString();
				}
				if (row["ShopPrintFoot"] != null)
				{
					model.ShopPrintFoot = row["ShopPrintFoot"].ToString();
				}
				if (row["ShopSmsName"] != null)
				{
					model.ShopSmsName = row["ShopSmsName"].ToString();
				}
				if (row["RechargeProportion"] != null)
				{
					model.RechargeProportion = Convert.ToDecimal(row["RechargeProportion"].ToString());
				}
				if (row["ShopProportion"] != null)
				{
					model.ShopProportion = Convert.ToDecimal(row["ShopProportion"].ToString());
				}
			}
			return model;
		}

		public DataSet GetListS(int PageSize, int PageIndex, string[] strWhere, out int resCount)
		{
			string tableName = "SysShop";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ShopID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public string GetShopNameByShopid(string shopid)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ShopName from SysShop where ShopID=" + shopid);
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			string result;
			if (obj != null)
			{
				result = obj.ToString();
			}
			else
			{
				result = "";
			}
			return result;
		}

		public int GetShopPointByShopid(int shopid, int type)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(string.Concat(new object[]
			{
				"select PointCount from SysShop where ShopID=",
				shopid,
				" and ShopType=",
				type
			}));
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			return int.Parse(obj.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, string[] strWhere, out int resCount)
		{
			string tableName = "SysShop";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ShopID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetShop(params string[] strWhere)
		{
			string tableName = "V_RptShop";
			string[] columns = new string[]
			{
				"*"
			};
			return DbHelperSQL.GetTable(tableName, columns, strWhere, "ShopID", true, 100, 1, 0);
		}

		public DataSet getTotalShop(string dtStartTime, string dtEndTime, params string[] strWhere)
		{
			string[] columns = new string[]
			{
				"*"
			};
			return SysShop.GetShopTable(columns, strWhere, "ShopID", true, dtStartTime, dtEndTime, 100, 1, 0);
		}

		public DataSet GetTotalRptData(string memWhere, string rechargeWhere, string orderWhere, string countWhere, string memstoragetimingWhere, string drawmoneyWhere)
		{
			string sql_mem = string.Format("select LevelID,(select count(MemID) from Mem where Mem.MemLevelID = MemLevel.LevelID and MemID>0 and {0})as MemNumber from MemLevel", memWhere);
			string sql_recharge = string.Format("SELECT Sum(RechargeMoney-RechargeGive),Sum(RechargeGive),RechargeType FROM MemRecharge where {0} group by RechargeType", rechargeWhere);
			string sql_order = string.Format("select CAST(OrderTotalMoney AS numeric(20,2)) AS OrderTotalMoney ,OrderDiscountMoney,OrderPayCoupon,OrderPayType,OrderPayCard,OrderPayCash,OrderPayBink ,UsePointAmount from OrderLog  where {0} ", orderWhere);
			string sql_count = string.Format(" select CAST(CountTotalMoney AS numeric(20,2)) AS CountTotalMoney ,CountDiscountMoney,CountPayCoupon,CountPayType,CountPayCard,CountPayCash,CountPayBink from MemCount  where {0} ", countWhere);
			string sql_memstoragetiming = string.Format("SELECT ISNULL(SUM(StorageTimingDiscountMoney),0) AS StorageTimingDiscountMoney, ISNULL(SUM(StorageTimingPayCard),0) AS StorageTimingPayCard,ISNULL(SUM(StorageTimingPayCash),0) AS StorageTimingPayCash,\r\n                                                          ISNULL(SUM(StorageTimingPayBink),0) AS StorageTimingPayBink,ISNULL(SUM(StorageTimingPayCoupon),0) AS StorageTimingPayCoupon FROM MemStorageTiming WHERE {0}", memstoragetimingWhere);
			string sql_drawmoney = string.Format("SELECT ISNULL(SUM(DrawMoney),0) as AllDrawMoney, ISNULL(SUM(DrawActualMoney),0) AS AllDrawActualMoney FROM MemDrawMoney where {0}", drawmoneyWhere);
			DataTable dt_memcard = DbHelperSQL.Query(sql_mem).Tables[0];
			DataTable dt_recharge = DbHelperSQL.Query(sql_recharge).Tables[0];
			DataTable dt_order = DbHelperSQL.Query(sql_order).Tables[0];
			DataTable dt_count = DbHelperSQL.Query(sql_count).Tables[0];
			DataTable dt_memstoragetiming = DbHelperSQL.Query(sql_memstoragetiming).Tables[0];
			DataTable dt_drawmoney = DbHelperSQL.Query(sql_drawmoney).Tables[0];
			dt_memcard.TableName = "dtMem";
			dt_recharge.TableName = "dtRecharge";
			dt_order.TableName = "dtOrder";
			dt_count.TableName = "dtCount";
			dt_memstoragetiming.TableName = "dtmemstoragetiming";
			dt_drawmoney.TableName = "dtdrawmoney";
			return new DataSet
			{
				Tables = 
				{
					dt_memcard.Copy(),
					dt_recharge.Copy(),
					dt_order.Copy(),
					dt_count.Copy(),
					dt_memstoragetiming.Copy(),
					dt_drawmoney.Copy()
				}
			};
		}

		public static DataSet GetShopTable(string[] column, string[] condition, string IndexColumn, bool IsAsc, string dtStartTime, string dtEndTime, int PageSize, int Page, int RecordCount)
		{
			string tempsql = "";
			string tempsql2 = "";
			for (int i = 0; i < column.Length; i++)
			{
				tempsql += column[i];
				if (i < column.Length - 1)
				{
					tempsql += ",";
				}
			}
			if (condition != null && condition.Length > 0)
			{
				for (int i = 0; i < condition.Length; i++)
				{
					if (!(condition[i] == ""))
					{
						tempsql2 += condition[i];
						if (i < condition.Length - 1)
						{
							tempsql2 += " and ";
						}
					}
				}
			}
			tempsql2 = DbHelperSQL.ProcessSql(tempsql2);
			SqlParameter[] paras = new SqlParameter[]
			{
				new SqlParameter("@ReturnFields", SqlDbType.VarChar, 500),
				new SqlParameter("@OrderFields", SqlDbType.VarChar, 255),
				new SqlParameter("@PKField", SqlDbType.VarChar, 255),
				new SqlParameter("@IsDesc", SqlDbType.Bit),
				new SqlParameter("@Where", SqlDbType.VarChar, 1000),
				new SqlParameter("@StartTime", SqlDbType.VarChar, 50),
				new SqlParameter("@EndTime", SqlDbType.VarChar, 50),
				new SqlParameter("@PageSize", SqlDbType.Int),
				new SqlParameter("@PageIndex", SqlDbType.Int)
			};
			paras[0].Value = tempsql;
			paras[1].Value = IndexColumn;
			paras[2].Value = IndexColumn;
			paras[3].Value = (IsAsc ? 0 : 1);
			paras[4].Value = tempsql2;
			paras[5].Value = dtStartTime;
			paras[6].Value = dtEndTime;
			paras[7].Value = PageSize;
			paras[8].Value = Page;
			return DbHelperSQL.ExecuteProcedure("CP_ShopTotal", paras);
		}

		public DataTable GetAllianceByCard(string strWhere, int shopID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.AppendFormat("SELECT * FROM dbo.SysShopBuyCard WHERE BuyCardShopid IN (SELECT FatherShopID FROM dbo.SysShop WHERE ShopID ={0}) ", shopID);
			strSql.Append(" and " + strWhere);
			return DbHelperSQL.Query(strSql.ToString()).Tables[0];
		}
	}
}
