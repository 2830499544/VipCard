using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class PointLog
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("PointID", "PointLog");
		}

		public bool Exists(int PointID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from PointLog");
			strSql.Append(" where PointID=@PointID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PointID", SqlDbType.Int, 4)
			};
			parameters[0].Value = PointID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(string account)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from PointLog");
			strSql.Append(" where PointOrderCode=@PointOrderCode");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PointOrderCode", SqlDbType.VarChar, 50)
			};
			parameters[0].Value = account;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.PointLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into PointLog(");
			strSql.Append("PointMemID,PointNumber,PointChangeType,PointRemark,PointShopID,PointCreateTime,PointUserID,PointOrderCode,PointGiveMemID)");
			strSql.Append(" values (");
			strSql.Append("@PointMemID,@PointNumber,@PointChangeType,@PointRemark,@PointShopID,@PointCreateTime,@PointUserID,@PointOrderCode,@PointGiveMemID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PointMemID", SqlDbType.Int, 4),
				new SqlParameter("@PointNumber", SqlDbType.Int, 4),
				new SqlParameter("@PointChangeType", SqlDbType.TinyInt, 1),
				new SqlParameter("@PointRemark", SqlDbType.NVarChar, 1000),
				new SqlParameter("@PointShopID", SqlDbType.Int, 4),
				new SqlParameter("@PointCreateTime", SqlDbType.DateTime),
				new SqlParameter("@PointUserID", SqlDbType.Int, 4),
				new SqlParameter("@PointOrderCode", SqlDbType.NVarChar, 40),
				new SqlParameter("@PointGiveMemID", SqlDbType.Int)
			};
			parameters[0].Value = model.PointMemID;
			parameters[1].Value = model.PointNumber;
			parameters[2].Value = model.PointChangeType;
			parameters[3].Value = model.PointRemark;
			parameters[4].Value = model.PointShopID;
			parameters[5].Value = model.PointCreateTime;
			parameters[6].Value = model.PointUserID;
			parameters[7].Value = model.PointOrderCode;
			parameters[8].Value = model.PointGiveMemID;
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

		public bool Update(Chain.Model.PointLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update PointLog set ");
			strSql.Append("PointMemID=@PointMemID,");
			strSql.Append("PointNumber=@PointNumber,");
			strSql.Append("PointChangeType=@PointChangeType,");
			strSql.Append("PointRemark=@PointRemark,");
			strSql.Append("PointShopID=@PointShopID,");
			strSql.Append("PointCreateTime=@PointCreateTime,");
			strSql.Append("PointUserID=@PointUserID,");
			strSql.Append("PointOrderCode=@PointOrderCode,");
			strSql.Append("PointGiveMemID=@PointGiveMemID");
			strSql.Append(" where PointID=@PointID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PointMemID", SqlDbType.Int, 4),
				new SqlParameter("@PointNumber", SqlDbType.Int, 4),
				new SqlParameter("@PointChangeType", SqlDbType.TinyInt, 1),
				new SqlParameter("@PointRemark", SqlDbType.NVarChar, 1000),
				new SqlParameter("@PointShopID", SqlDbType.Int, 4),
				new SqlParameter("@PointCreateTime", SqlDbType.DateTime),
				new SqlParameter("@PointUserID", SqlDbType.Int, 4),
				new SqlParameter("@PointID", SqlDbType.Int, 4),
				new SqlParameter("@PointOrderCode", SqlDbType.NVarChar, 40),
				new SqlParameter("@PointGiveMemID", SqlDbType.Int)
			};
			parameters[0].Value = model.PointMemID;
			parameters[1].Value = model.PointNumber;
			parameters[2].Value = model.PointChangeType;
			parameters[3].Value = model.PointRemark;
			parameters[4].Value = model.PointShopID;
			parameters[5].Value = model.PointCreateTime;
			parameters[6].Value = model.PointUserID;
			parameters[7].Value = model.PointID;
			parameters[8].Value = model.PointOrderCode;
			parameters[9].Value = model.PointGiveMemID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int PointID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from PointLog ");
			strSql.Append(" where PointID=@PointID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PointID", SqlDbType.Int, 4)
			};
			parameters[0].Value = PointID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string PointIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from PointLog ");
			strSql.Append(" where PointID in (" + PointIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.PointLog GetModel(int PointID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 PointID,PointMemID,PointNumber,PointChangeType,PointRemark,PointShopID,PointCreateTime,PointUserID,PointOrderCode,PointGiveMemID from PointLog ");
			strSql.Append(" where PointID=@PointID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PointID", SqlDbType.Int, 4)
			};
			parameters[0].Value = PointID;
			Chain.Model.PointLog model = new Chain.Model.PointLog();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.PointLog result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["PointID"] != null && ds.Tables[0].Rows[0]["PointID"].ToString() != "")
				{
					model.PointID = int.Parse(ds.Tables[0].Rows[0]["PointID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PointMemID"] != null && ds.Tables[0].Rows[0]["PointMemID"].ToString() != "")
				{
					model.PointMemID = int.Parse(ds.Tables[0].Rows[0]["PointMemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PointNumber"] != null && ds.Tables[0].Rows[0]["PointNumber"].ToString() != "")
				{
					model.PointNumber = int.Parse(ds.Tables[0].Rows[0]["PointNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PointChangeType"] != null && ds.Tables[0].Rows[0]["PointChangeType"].ToString() != "")
				{
					model.PointChangeType = int.Parse(ds.Tables[0].Rows[0]["PointChangeType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PointRemark"] != null && ds.Tables[0].Rows[0]["PointRemark"].ToString() != "")
				{
					model.PointRemark = ds.Tables[0].Rows[0]["PointRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PointShopID"] != null && ds.Tables[0].Rows[0]["PointShopID"].ToString() != "")
				{
					model.PointShopID = int.Parse(ds.Tables[0].Rows[0]["PointShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PointCreateTime"] != null && ds.Tables[0].Rows[0]["PointCreateTime"].ToString() != "")
				{
					model.PointCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["PointCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PointUserID"] != null && ds.Tables[0].Rows[0]["PointUserID"].ToString() != "")
				{
					model.PointUserID = int.Parse(ds.Tables[0].Rows[0]["PointUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["PointOrderCode"] != null && ds.Tables[0].Rows[0]["PointOrderCode"].ToString() != "")
				{
					model.PointOrderCode = ds.Tables[0].Rows[0]["PointOrderCode"].ToString();
				}
				if (ds.Tables[0].Rows[0]["PointGiveMemID"] != null && ds.Tables[0].Rows[0]["PointGiveMemID"].ToString() != "")
				{
					model.PointGiveMemID = Convert.ToInt32(ds.Tables[0].Rows[0]["PointGiveMemID"]);
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
			strSql.Append("select PointID,PointMemID,PointNumber,PointChangeType,PointRemark,PointShopID,PointCreateTime,PointUserID,PointOrderCode,PointGiveMemID ");
			strSql.Append(" FROM PointLog ");
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
			strSql.Append(" PointID,PointMemID,PointNumber,PointChangeType,PointRemark,PointShopID,PointCreateTime,PointUserID,PointOrderCode,PointGiveMemID ");
			strSql.Append(" FROM PointLog ");
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
			strSql.Append("select count(1) FROM PointLog ");
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

		public int GetPointNumber(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select SUM(PointNumber) FROM PointLog,Mem,MemLevel,SysShop,SysUser ");
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

		public DataSet GetPointByTime(DateTime starttime, DateTime endtime, string strwhere)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StartTime", SqlDbType.DateTime),
				new SqlParameter("@EndTime", SqlDbType.DateTime),
				new SqlParameter("@strWhere", SqlDbType.NVarChar, 2000)
			};
			parameters[0].Value = starttime;
			parameters[1].Value = endtime;
			parameters[2].Value = strwhere;
			return DbHelperSQL.RunProcedure("MonthPointLog", parameters, "#PointLogData");
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
				strSql.Append("order by T.PointID desc");
			}
			strSql.Append(")AS Row, T.*  from PointLog T ");
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
			string tableName = "PointLog,Mem,MemLevel,SysShop,SysUser";
			string[] columns = new string[]
			{
				"PointLog.*,Mem.*,MemLevel.LevelName,SysShop.ShopName,SysUser.UserName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "PointID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public int DeleteLog(string strOrderAccount)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from PointLog where PointOrderCode=@PointOrderCode");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("PointOrderCode", SqlDbType.VarChar, 50)
			};
			parameter[0].Value = strOrderAccount;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}

		public int UpdatePointLog(string strOrderCode, int intPointNumber, int intMemID, string strRemark)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" update PointLog set PointNumber=@PointNumber，");
			strSql.Append(" PointRemark=@PointRemark");
			strSql.Append(" where PointOrderCode=@PointOrderCode and PointMemID=@PointMemID");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@PointNumber", SqlDbType.Int, 4),
				new SqlParameter("@PointRemark", SqlDbType.VarChar, 1000),
				new SqlParameter("@PointOrderCode", SqlDbType.VarChar, 50),
				new SqlParameter("@PointMemID", SqlDbType.Int, 4)
			};
			parameter[0].Value = intPointNumber;
			parameter[1].Value = strRemark;
			parameter[2].Value = strOrderCode;
			parameter[3].Value = intMemID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}

		public int UpdateMemPoint(int memID, int intPointNumber)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update Mem set MemPoint=MemPoint-@MemPoint");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameter = new SqlParameter[]
			{
				new SqlParameter("@MemPoint", SqlDbType.Int, 4),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameter[0].Value = intPointNumber;
			parameter[1].Value = memID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameter);
		}

		public DataTable AgainPrint(int pointID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select ");
			strSql.Append("MemCard,MemName,PointChangeType,PointNumber,MemPoint,UserName,PointCreateTime,PointRemark ");
			strSql.Append("from ");
			strSql.Append("Mem,PointLog,SysUser ");
			strSql.Append("where Mem.MemID=PointLog.PointMemID ");
			strSql.Append("and SysUser.UserID=PointLog.PointUserID ");
			strSql.Append("and PointID=@PointID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PointID", SqlDbType.Int, 4)
			};
			parameters[0].Value = pointID;
			return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
		}

		public int GetPointChange(string strSql)
		{
			StringBuilder strWhere = new StringBuilder();
			strWhere.Append(" select isnull(sum(PointNumber),0) from PointLog,Mem ");
			if (strSql.Trim() != "")
			{
				strWhere.Append(" where " + strSql);
			}
			object obj = DbHelperSQL.GetSingle(strWhere.ToString());
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

		public int MemSign(Chain.Model.Mem member)
		{
			int result;
			if (member == null)
			{
				result = -1;
			}
			else
			{
				result = 1;
			}
			return result;
		}

		public bool IsSignedToday(int memId, int pointLogType)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("SELECT COUNT(0) FROM PointLog");
			strSql.Append(" WHERE PointMemID =@PointMemID");
			strSql.Append(" AND PointChangeType = @PointChangeType");
			strSql.Append(" AND PointCreateTime BETWEEN CONVERT(varchar(10),GETDATE(),120) AND CONVERT(varchar(10),DATEADD(dd,1,GETDATE()),120)");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PointMemID", SqlDbType.Int, 4),
				new SqlParameter("@PointChangeType", SqlDbType.Int, 4)
			};
			parameters[0].Value = memId;
			parameters[1].Value = pointLogType;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}
	}
}
