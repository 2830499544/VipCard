using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class SysRotateCount
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID", "SysRotateCount");
		}

		public bool Exists(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from SysRotateCount");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(int intClassID, string strClassName, int ShopID)
		{
			string strSql = "select ClassName from SysRotateCount where ID not in ({0}) and (ClassName = '{1}') and ClassShopID = '{2}'";
			strSql = string.Format(strSql, intClassID, strClassName, ShopID);
			DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
			return dt.Rows.Count > 0;
		}

		public int Add(Chain.Model.SysRotateCount model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into SysRotateCount(");
			strSql.Append("CostAmount,RotateCount,StartTime,EndTime,RotateID)");
			strSql.Append(" values (");
			strSql.Append("@CostAmount,@RotateCount,@StartTime,@EndTime,@RotateID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CostAmount", SqlDbType.Decimal, 2),
				new SqlParameter("@RotateCount", SqlDbType.Int, 4),
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EndTime", SqlDbType.DateTime),
				new SqlParameter("@RotateID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.CostAmount;
			parameters[1].Value = model.RotateCount;
			parameters[2].Value = model.StartTime;
			parameters[3].Value = model.EndTime;
			parameters[4].Value = model.RotateID;
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

		public int Update(Chain.Model.SysRotateCount model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update SysRotateCount set ");
			strSql.Append("RotateID=@RotateID,");
			strSql.Append("CostAmount=@CostAmount,");
			strSql.Append("RotateCount=@RotateCount,");
			strSql.Append("StartTime=@StartTime,");
			strSql.Append("EndTime=@EndTime");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CostAmount", SqlDbType.Int, 4),
				new SqlParameter("@RotateCount", SqlDbType.Decimal, 2),
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EndTime", SqlDbType.DateTime),
				new SqlParameter("@ID", SqlDbType.Int, 4),
				new SqlParameter("@RotateID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.CostAmount;
			parameters[1].Value = model.RotateCount;
			parameters[2].Value = model.StartTime;
			parameters[3].Value = model.EndTime;
			parameters[4].Value = model.ID;
			parameters[5].Value = model.RotateID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public int Delete(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysRotateCount ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public bool DeleteList(string ClassIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from SysRotateCount ");
			strSql.Append(" where ID in (" + ClassIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.SysRotateCount GetModelbyRotateID(int RotateID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ID,CostAmount,RotateCount,StartTime,EndTime,RotateID from SysRotateCount ");
			strSql.Append(" where RotateID=@RotateID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@RotateID", SqlDbType.Int, 4)
			};
			parameters[0].Value = RotateID;
			Chain.Model.SysRotateCount model = new Chain.Model.SysRotateCount();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysRotateCount result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
				{
					model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CostAmount"] != null && ds.Tables[0].Rows[0]["CostAmount"].ToString() != "")
				{
					model.CostAmount = decimal.Parse(ds.Tables[0].Rows[0]["CostAmount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RotateCount"] != null && ds.Tables[0].Rows[0]["RotateCount"].ToString() != "")
				{
					model.RotateCount = int.Parse(ds.Tables[0].Rows[0]["RotateCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RotateCount"] != null && ds.Tables[0].Rows[0]["RotateCount"].ToString() != "")
				{
					model.RotateCount = int.Parse(ds.Tables[0].Rows[0]["RotateCount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StartTime"] != null && ds.Tables[0].Rows[0]["StartTime"].ToString() != "")
				{
					model.StartTime = DateTime.Parse(ds.Tables[0].Rows[0]["StartTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["EndTime"] != null && ds.Tables[0].Rows[0]["EndTime"].ToString() != "")
				{
					model.EndTime = DateTime.Parse(ds.Tables[0].Rows[0]["EndTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["RotateID"] != null && ds.Tables[0].Rows[0]["RotateID"].ToString() != "")
				{
					model.RotateID = int.Parse(ds.Tables[0].Rows[0]["RotateID"].ToString());
				}
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Chain.Model.SysRotateCount GetModel(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ID,ClassName,ClassType,ClassPercent,ClassShopID,ClassRemark from SysRotateCount ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			Chain.Model.SysRotateCount model = new Chain.Model.SysRotateCount();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.SysRotateCount result;
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
			strSql.Append("select  ID,CostAmount,convert(char(10),StartTime,121) as StartTime,convert(char(10),EndTime,121)as EndTime,RotateCount,RotateID ");
			strSql.Append(" FROM SysRotateCount ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
				strSql.Append(" order by CostAmount desc");
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
			strSql.Append(" ID,ClassName,ClassType,ClassPercent,ClassShopID,ClassRemark ");
			strSql.Append(" FROM SysRotateCount ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = " SysRotateCount ";
			string[] columns = new string[]
			{
				" * "
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ID", "ID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int GetDaysByCoinAmount(decimal coinAmount)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select top 1 coinDays from syscoin where CoinAmount <" + coinAmount + " order by  CoinAmount desc");
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

		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) FROM SysRotateCount ");
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

		public decimal GetMemOrderLogCostAmount(string starttime, string endtime, int MemID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(string.Concat(new object[]
			{
				"select isnull(sum(OrderDiscountMoney),0) FROM OrderLog  where convert(char(10),OrderCreateTime,121)>='",
				starttime,
				"' and convert(char(10),OrderCreateTime,121)<='",
				endtime,
				"' and OrderType not in(3) and OrderMemID=",
				MemID
			}));
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			decimal result;
			if (obj == null)
			{
				result = 0m;
			}
			else
			{
				result = decimal.Parse(obj.ToString());
			}
			return result;
		}

		public decimal GetMemCountCostAmount(string starttime, string endtime, int MemID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(string.Concat(new object[]
			{
				"select isnull(sum(CountDiscountMoney),0) FROM MemCount  where convert(char(10),CountCreateTime,121)>='",
				starttime,
				"' and convert(char(10),CountCreateTime,121)<='",
				endtime,
				"'  and CountMemID=",
				MemID
			}));
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			decimal result;
			if (obj == null)
			{
				result = 0m;
			}
			else
			{
				result = decimal.Parse(obj.ToString());
			}
			return result;
		}

		public decimal GetMemStorageTimingCostAmount(string starttime, string endtime, int MemID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(string.Concat(new object[]
			{
				"select isnull(sum(StorageTimingDiscountMoney),0) FROM MemStorageTiming  where convert(char(10),StorageTimingCreateTime,121)>='",
				starttime,
				"' and convert(char(10),StorageTimingCreateTime,121)<='",
				endtime,
				"'  and StorageTimingMemID=",
				MemID
			}));
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			decimal result;
			if (obj == null)
			{
				result = 0m;
			}
			else
			{
				result = decimal.Parse(obj.ToString());
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from SysRotateCount T ");
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
