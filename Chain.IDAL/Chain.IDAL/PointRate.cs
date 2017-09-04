using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class PointRate
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("PointRateID", "PointRate");
		}

		public DataRow GetDataRow()
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select top 1 * from PointRate");
			DataSet ds = DbHelperSQL.Query(strSql.ToString());
			DataRow result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				result = ds.Tables[0].Rows[0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Chain.Model.PointRate GetPointRate()
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select top 1 * from PointRate");
			DataSet ds = DbHelperSQL.Query(strSql.ToString());
			Chain.Model.PointRate result;
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

		public int Update(Chain.Model.PointRate model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update PointRate set ");
			strSql.Append("PointRateType=@PointRateType,");
			strSql.Append("MemLevel1=@MemLevel1,");
			strSql.Append("MemLevel2=@MemLevel2,");
			strSql.Append("MemLevel3=@MemLevel3,");
			strSql.Append("MemLevel4=@MemLevel4,");
			strSql.Append("MemLevel5=@MemLevel5,");
			strSql.Append("MemLevel6=@MemLevel6,");
			strSql.Append("MemLevel7=@MemLevel7,");
			strSql.Append("MemLevel8=@MemLevel8,");
			strSql.Append("MemLevel9=@MemLevel9,");
			strSql.Append("MemLevel10=@MemLevel10,");
			strSql.Append("MemLevel11=@MemLevel11,");
			strSql.Append("MemLevel12=@MemLevel12,");
			strSql.Append("MemLevel13=@MemLevel13,");
			strSql.Append("MemLevel14=@MemLevel14,");
			strSql.Append("MemLevel15=@MemLevel15,");
			strSql.Append("PointRateLevel=@PointRateLevel");
			strSql.Append(" where PointRateID=@PointRateID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PointRateType", SqlDbType.Bit, 4),
				new SqlParameter("@MemLevel1", SqlDbType.Float, 6),
				new SqlParameter("@MemLevel2", SqlDbType.Float, 6),
				new SqlParameter("@MemLevel3", SqlDbType.Float, 6),
				new SqlParameter("@MemLevel4", SqlDbType.Float, 6),
				new SqlParameter("@MemLevel5", SqlDbType.Float, 6),
				new SqlParameter("@MemLevel6", SqlDbType.Float, 6),
				new SqlParameter("@MemLevel7", SqlDbType.Float, 6),
				new SqlParameter("@MemLevel8", SqlDbType.Float, 6),
				new SqlParameter("@MemLevel9", SqlDbType.Float, 6),
				new SqlParameter("@MemLevel10", SqlDbType.Float, 6),
				new SqlParameter("@MemLevel11", SqlDbType.Float, 6),
				new SqlParameter("@MemLevel12", SqlDbType.Float, 6),
				new SqlParameter("@MemLevel13", SqlDbType.Float, 6),
				new SqlParameter("@MemLevel14", SqlDbType.Float, 6),
				new SqlParameter("@MemLevel15", SqlDbType.Float, 6),
				new SqlParameter("@PointRateLevel", SqlDbType.Int, 4),
				new SqlParameter("@PointRateID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.PointRateType;
			parameters[1].Value = model.MemLevel1;
			parameters[2].Value = model.MemLevel2;
			parameters[3].Value = model.MemLevel3;
			parameters[4].Value = model.MemLevel4;
			parameters[5].Value = model.MemLevel5;
			parameters[6].Value = model.MemLevel6;
			parameters[7].Value = model.MemLevel7;
			parameters[8].Value = model.MemLevel8;
			parameters[9].Value = model.MemLevel9;
			parameters[10].Value = model.MemLevel10;
			parameters[11].Value = model.MemLevel11;
			parameters[12].Value = model.MemLevel12;
			parameters[13].Value = model.MemLevel13;
			parameters[14].Value = model.MemLevel14;
			parameters[15].Value = model.MemLevel15;
			parameters[16].Value = model.PointRateLevel;
			parameters[17].Value = model.PointRateID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public Chain.Model.PointRate DataRowToModel(DataRow row)
		{
			Chain.Model.PointRate model = new Chain.Model.PointRate();
			if (row != null)
			{
				if (row["PointRateID"] != null && row["PointRateID"].ToString() != "")
				{
					model.PointRateID = int.Parse(row["PointRateID"].ToString());
				}
				if (row["PointRateType"] != null && row["PointRateType"].ToString() != "")
				{
					if (row["PointRateType"].ToString() == "1" || row["PointRateType"].ToString().ToLower() == "true")
					{
						model.PointRateType = true;
					}
					else
					{
						model.PointRateType = false;
					}
				}
				if (row["PointRateLevel"] != null && row["PointRateLevel"].ToString() != "")
				{
					model.PointRateLevel = int.Parse(row["PointRateLevel"].ToString());
				}
				if (row["MemLevel1"] != null && row["MemLevel1"].ToString() != "")
				{
					model.MemLevel1 = decimal.Parse(row["MemLevel1"].ToString());
				}
				if (row["MemLevel2"] != null && row["MemLevel2"].ToString() != "")
				{
					model.MemLevel2 = decimal.Parse(row["MemLevel2"].ToString());
				}
				if (row["MemLevel3"] != null && row["MemLevel3"].ToString() != "")
				{
					model.MemLevel3 = decimal.Parse(row["MemLevel3"].ToString());
				}
				if (row["MemLevel4"] != null && row["MemLevel4"].ToString() != "")
				{
					model.MemLevel4 = decimal.Parse(row["MemLevel4"].ToString());
				}
				if (row["MemLevel5"] != null && row["MemLevel5"].ToString() != "")
				{
					model.MemLevel5 = decimal.Parse(row["MemLevel5"].ToString());
				}
				if (row["MemLevel6"] != null && row["MemLevel6"].ToString() != "")
				{
					model.MemLevel6 = decimal.Parse(row["MemLevel6"].ToString());
				}
				if (row["MemLevel7"] != null && row["MemLevel7"].ToString() != "")
				{
					model.MemLevel7 = decimal.Parse(row["MemLevel7"].ToString());
				}
				if (row["MemLevel8"] != null && row["MemLevel8"].ToString() != "")
				{
					model.MemLevel8 = decimal.Parse(row["MemLevel8"].ToString());
				}
				if (row["MemLevel9"] != null && row["MemLevel9"].ToString() != "")
				{
					model.MemLevel9 = decimal.Parse(row["MemLevel9"].ToString());
				}
				if (row["MemLevel10"] != null && row["MemLevel10"].ToString() != "")
				{
					model.MemLevel10 = decimal.Parse(row["MemLevel10"].ToString());
				}
				if (row["MemLevel11"] != null && row["MemLevel11"].ToString() != "")
				{
					model.MemLevel11 = decimal.Parse(row["MemLevel11"].ToString());
				}
				if (row["MemLevel12"] != null && row["MemLevel12"].ToString() != "")
				{
					model.MemLevel12 = decimal.Parse(row["MemLevel12"].ToString());
				}
				if (row["MemLevel13"] != null && row["MemLevel13"].ToString() != "")
				{
					model.MemLevel13 = decimal.Parse(row["MemLevel13"].ToString());
				}
				if (row["MemLevel14"] != null && row["MemLevel14"].ToString() != "")
				{
					model.MemLevel14 = decimal.Parse(row["MemLevel14"].ToString());
				}
				if (row["MemLevel15"] != null && row["MemLevel15"].ToString() != "")
				{
					model.MemLevel15 = decimal.Parse(row["MemLevel15"].ToString());
				}
			}
			return model;
		}

		public int GetPointRateNumber(string strWhere)
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

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, string strTime, params string[] strWhere)
		{
			string tableName = "MEM ,SYSSHOP,MEMLEVEL,SYSUSER";
			string[] columns = new string[]
			{
				"MEMID,MEMCARD,MEMNAME,MemPoint,SHOPNAME,LEVELNAME,SYSUSER.USERNAME,(SELECT ISNULL(SUM(PointNumber),0) from PointLOG  where PointLOG.PointMemID=Mem.MEMID and PointLOG.PointChangeTYPE in (9,12) and 1=1 ) AS RatePointCount,(select count(pointmemid) from PointLog where PointLog.PointMemID=Mem.MEMID and PointLog.PointChangeType=9 and 1=1 ) as MemRecommend "
			};
			columns[0] = columns[0].Replace("and 1=1 ", strTime);
			strWhere[0] = strWhere[0].Substring(32);
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "MemID", "MemID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetMemDetailByMemCard(int MemID, string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select PointLog.*,Mem.MemCard,Mem.MemName ");
			strSql.Append(" from PointLog,Mem ");
			strSql.Append(" where PointLog.PointMemID = Mem.MemID ");
			strSql.Append(" and PointMemID=" + MemID + " and PointChangeType in (9,12)");
			if (strWhere != "")
			{
				strSql.Append(strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetMemDetailByMemID(int MemID, string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select PointLog.*,Mem.MemCard,Mem.MemName ");
			strSql.Append(" from PointLog,Mem ");
			strSql.Append(" where PointLog.PointGiveMemID = Mem.MemID ");
			strSql.Append(" and PointMemID=" + MemID + " and PointChangeType in (9,12)");
			if (strWhere != "")
			{
				strSql.Append(strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetMyTeamList(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = " V_MyTeam";
			string[] columns = new string[]
			{
				"Rowid,SumNumber,MemCard,MemName,MemMobile,PointGiveMemID "
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "Rowid", "Rowid", true, PageSize, PageIndex, recordCount);
			try
			{
				resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			}
			catch
			{
				resCount = 0;
			}
			return ds;
		}

		public DataSet GetPointLog(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("SELECT * FROM V_PointLog ");
			if (strWhere != "")
			{
				strSql.Append("WHERE " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
	}
}
