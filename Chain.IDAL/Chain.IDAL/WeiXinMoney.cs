using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class WeiXinMoney
	{
		public int Add(Chain.Model.WeiXinMoney model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into WeiXinMoney(");
			strSql.Append("QuerySql,MoneyRegion,GiveMoney,CreateTime,CreateUserID,MoneyTitle,ImageUrl,MoneyDesc,MoneyWish,StartTime,EndTime,TotalMoney,MoneyType,StartMoney,EndMoney,FixedMoney,MaxCount,MoneyRate)");
			strSql.Append(" values (");
			strSql.Append("@QuerySql,@MoneyRegion,@GiveMoney,@CreateTime,@CreateUserID,@MoneyTitle,@ImageUrl,@MoneyDesc,@MoneyWish,@StartTime,@EndTime,@TotalMoney,@MoneyType,@StartMoney,@EndMoney,@FixedMoney,@MaxCount,@MoneyRate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyTitle", SqlDbType.NVarChar, 100),
				new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200),
				new SqlParameter("@MoneyDesc", SqlDbType.NVarChar, 500),
				new SqlParameter("@MoneyWish", SqlDbType.NVarChar, 500),
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EndTime", SqlDbType.DateTime),
				new SqlParameter("@TotalMoney", SqlDbType.Decimal, 2),
				new SqlParameter("@MoneyType", SqlDbType.Int, 4),
				new SqlParameter("@StartMoney", SqlDbType.Decimal, 2),
				new SqlParameter("@EndMoney", SqlDbType.Decimal, 2),
				new SqlParameter("@FixedMoney", SqlDbType.Decimal, 2),
				new SqlParameter("@MaxCount", SqlDbType.Int, 4),
				new SqlParameter("@MoneyRate", SqlDbType.Decimal, 2),
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@CreateUserID", SqlDbType.Int, 4),
				new SqlParameter("@GiveMoney", SqlDbType.Decimal, 2),
				new SqlParameter("@MoneyRegion", SqlDbType.NVarChar, 50),
				new SqlParameter("@QuerySql", SqlDbType.NVarChar, 2000)
			};
			parameters[0].Value = model.MoneyTitle;
			parameters[1].Value = model.ImageUrl;
			parameters[2].Value = model.MoneyDesc;
			parameters[3].Value = model.MoneyWish;
			parameters[4].Value = model.StartTime;
			parameters[5].Value = model.EndTime;
			parameters[6].Value = model.TotalMoney;
			parameters[7].Value = model.MoneyType;
			parameters[8].Value = model.StartMoney;
			parameters[9].Value = model.EndMoney;
			parameters[10].Value = model.FixedMoney;
			parameters[11].Value = model.MaxCount;
			parameters[12].Value = model.MoneyRate;
			parameters[13].Value = model.CreateTime;
			parameters[14].Value = model.CreateUserID;
			parameters[15].Value = model.GiveMoney;
			parameters[16].Value = model.MoneyRegion;
			parameters[17].Value = model.QuerySql;
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

		public int Update(Chain.Model.WeiXinMoney model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update WeiXinMoney set ");
			strSql.Append("QuerySql=@QuerySql,");
			strSql.Append("MoneyTitle=@MoneyTitle,");
			strSql.Append("ImageUrl=@ImageUrl,");
			strSql.Append("MoneyDesc=@MoneyDesc,");
			strSql.Append("MoneyWish=@MoneyWish,");
			strSql.Append("StartTime=@StartTime,");
			strSql.Append("EndTime=@EndTime,");
			strSql.Append("TotalMoney=@TotalMoney,");
			strSql.Append("MoneyType=@MoneyType,");
			strSql.Append("StartMoney=@StartMoney,");
			strSql.Append("EndMoney=@EndMoney,");
			strSql.Append("FixedMoney=@FixedMoney,");
			strSql.Append("MaxCount=@MaxCount,");
			strSql.Append("MoneyRate=@MoneyRate,");
			strSql.Append("MoneyRegion=@MoneyRegion");
			strSql.Append(" where MoneyID=@MoneyID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyTitle", SqlDbType.NVarChar, 100),
				new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 200),
				new SqlParameter("@MoneyDesc", SqlDbType.NVarChar, 500),
				new SqlParameter("@MoneyWish", SqlDbType.NVarChar, 500),
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EndTime", SqlDbType.DateTime),
				new SqlParameter("@TotalMoney", SqlDbType.Decimal, 2),
				new SqlParameter("@MoneyType", SqlDbType.Int, 4),
				new SqlParameter("@StartMoney", SqlDbType.Decimal, 2),
				new SqlParameter("@EndMoney", SqlDbType.Decimal, 2),
				new SqlParameter("@FixedMoney", SqlDbType.Decimal, 2),
				new SqlParameter("@MaxCount", SqlDbType.Int, 4),
				new SqlParameter("@MoneyRate", SqlDbType.Decimal, 2),
				new SqlParameter("@MoneyID", SqlDbType.Int, 4),
				new SqlParameter("@MoneyRegion", SqlDbType.NVarChar, 50),
				new SqlParameter("@QuerySql", SqlDbType.NVarChar, 2000)
			};
			parameters[0].Value = model.MoneyTitle;
			parameters[1].Value = model.ImageUrl;
			parameters[2].Value = model.MoneyDesc;
			parameters[3].Value = model.MoneyWish;
			parameters[4].Value = model.StartTime;
			parameters[5].Value = model.EndTime;
			parameters[6].Value = model.TotalMoney;
			parameters[7].Value = model.MoneyType;
			parameters[8].Value = model.StartMoney;
			parameters[9].Value = model.EndMoney;
			parameters[10].Value = model.FixedMoney;
			parameters[11].Value = model.MaxCount;
			parameters[12].Value = model.MoneyRate;
			parameters[13].Value = model.MoneyID;
			parameters[14].Value = model.MoneyRegion;
			parameters[15].Value = model.QuerySql;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public bool UpdateGiveMoney(int MoneyID, decimal GiveMoney)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update  WeiXinMoney set GiveMoney=GiveMoney+@GiveMoney  ");
			strSql.Append(" where MoneyID=@MoneyID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyID", SqlDbType.Int, 4),
				new SqlParameter("@GiveMoney", SqlDbType.Decimal, 2)
			};
			parameters[0].Value = MoneyID;
			parameters[1].Value = GiveMoney;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int MoneyID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from WeiXinMoney ");
			strSql.Append(" where MoneyID=@MoneyID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MoneyID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public Chain.Model.WeiXinMoney GetModel(int MoneyID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 QuerySql,MoneyRegion,GiveMoney,MoneyID, CreateTime,CreateUserID,MoneyTitle,ImageUrl,MoneyDesc,MoneyWish,StartTime,EndTime,TotalMoney,MoneyType,StartMoney,EndMoney,FixedMoney,MaxCount,MoneyRate from WeiXinMoney ");
			strSql.Append(" where MoneyID=@MoneyID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MoneyID;
			Chain.Model.WeiXinMoney model = new Chain.Model.WeiXinMoney();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.WeiXinMoney result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				result = this.DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Chain.Model.WeiXinMoney GetModelByWhere(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 QuerySql,MoneyRegion,GiveMoney,CreateTime,CreateUserID,MoneyID,MoneyTitle,ImageUrl,MoneyDesc,MoneyWish,StartTime,EndTime,TotalMoney,MoneyType,StartMoney,EndMoney,FixedMoney,MaxCount,MoneyRate from WeiXinMoney ");
			strSql.Append(" where " + strWhere);
			DataSet ds = DbHelperSQL.Query(strSql.ToString());
			Chain.Model.WeiXinMoney result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				result = this.DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public string GetMoneyIDByMoneyRegion(string MoneyRegion)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select top 1 MoneyID from WeiXinMoney ");
			strSql.Append(" where MoneyRegion=@MoneyRegion");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MoneyRegion", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = MoneyRegion;
			object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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

		public Chain.Model.WeiXinMoney DataRowToModel(DataRow row)
		{
			Chain.Model.WeiXinMoney model = new Chain.Model.WeiXinMoney();
			if (row != null)
			{
				if (row["MoneyRegion"] != null && row["MoneyRegion"].ToString() != "")
				{
					model.MoneyRegion = row["MoneyRegion"].ToString();
				}
				if (row["QuerySql"] != null && row["QuerySql"].ToString() != "")
				{
					model.QuerySql = row["QuerySql"].ToString();
				}
				if (row["MoneyTitle"] != null && row["MoneyTitle"].ToString() != "")
				{
					model.MoneyTitle = row["MoneyTitle"].ToString();
				}
				if (row["GiveMoney"] != null && row["GiveMoney"].ToString() != "")
				{
					model.GiveMoney = decimal.Parse(row["GiveMoney"].ToString());
				}
				if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
				{
					model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
				}
				if (row["MoneyID"] != null && row["MoneyID"].ToString() != "")
				{
					model.MoneyID = int.Parse(row["MoneyID"].ToString());
				}
				if (row["CreateUserID"] != null && row["CreateUserID"].ToString() != "")
				{
					model.CreateUserID = int.Parse(row["CreateUserID"].ToString());
				}
				if (row["ImageUrl"] != null)
				{
					model.ImageUrl = row["ImageUrl"].ToString();
				}
				if (row["MoneyDesc"] != null && row["MoneyDesc"].ToString() != "")
				{
					model.MoneyDesc = row["MoneyDesc"].ToString();
				}
				if (row["MoneyDesc"] != null && row["MoneyDesc"].ToString() != "")
				{
					model.MoneyDesc = row["MoneyDesc"].ToString();
				}
				if (row["MoneyWish"] != null && row["MoneyWish"].ToString() != "")
				{
					model.MoneyWish = row["MoneyWish"].ToString();
				}
				if (row["StartTime"] != null && row["StartTime"].ToString() != "")
				{
					model.StartTime = DateTime.Parse(row["StartTime"].ToString());
				}
				if (row["EndTime"] != null && row["EndTime"].ToString() != "")
				{
					model.EndTime = DateTime.Parse(row["EndTime"].ToString());
				}
				if (row["TotalMoney"] != null && row["TotalMoney"].ToString() != "")
				{
					model.TotalMoney = decimal.Parse(row["TotalMoney"].ToString());
				}
				if (row["MoneyType"] != null && row["MoneyType"].ToString() != "")
				{
					model.MoneyType = int.Parse(row["MoneyType"].ToString());
				}
				if (row["StartMoney"] != null && row["StartMoney"].ToString() != "")
				{
					model.StartMoney = decimal.Parse(row["StartMoney"].ToString());
				}
				if (row["EndMoney"] != null && row["EndMoney"].ToString() != "")
				{
					model.EndMoney = decimal.Parse(row["EndMoney"].ToString());
				}
				if (row["FixedMoney"] != null && row["FixedMoney"].ToString() != "")
				{
					model.FixedMoney = decimal.Parse(row["FixedMoney"].ToString());
				}
				if (row["MaxCount"] != null && row["MaxCount"].ToString() != "")
				{
					model.MaxCount = int.Parse(row["MaxCount"].ToString());
				}
				if (row["MoneyRate"] != null && row["MoneyRate"].ToString() != "")
				{
					model.MoneyRate = decimal.Parse(row["MoneyRate"].ToString());
				}
			}
			return model;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select GiveMoney,CreateTime,CreateUserID,MoneyTitle,ImageUrl,MoneyDesc,MoneyWish,StartTime,EndTime,TotalMoney,MoneyType,StartMoney,EndMoney,FixedMoney,MaxCount,MoneyRate ");
			strSql.Append(" FROM WeiXinMoney ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "WeiXinMoney";
			string[] columns = new string[]
			{
				"*"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MoneyID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) FROM WeiXinMoney ");
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
	}
}
