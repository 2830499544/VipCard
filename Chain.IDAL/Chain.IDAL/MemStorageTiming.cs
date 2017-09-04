using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class MemStorageTiming
	{
		public bool Exists(int StorageTimingID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemStorageTiming");
			strSql.Append(" where StorageTimingID=@StorageTimingID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StorageTimingID", SqlDbType.Int, 4)
			};
			parameters[0].Value = StorageTimingID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool ExistsProject(int ProjectID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemStorageTiming");
			strSql.Append(" where StorageTimingProjectID=@StorageTimingProjectID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StorageTimingProjectID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ProjectID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.MemStorageTiming model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MemStorageTiming(");
			strSql.Append("StorageTimingMemID,StorageTimingAccount,StorageTimingTotalMoney,StorageTimingDiscountMoney,StorageTimingIsCard,StorageTimingPayCard,StorageTimingIsCash,StorageTimingPayCash,StorageTimingIsBink,StorageTimingPayBink,StorageTimingPayCoupon,StorageTimingPayType,StorageTimingPoint,StorageTimingRemark,StorageTimingShopID,StorageTimingUserID,StorageTimingCreateTime,StorageTotalTime,StorageResidueTime,StorageTimingProjectID)");
			strSql.Append(" values (");
			strSql.Append("@StorageTimingMemID,@StorageTimingAccount,@StorageTimingTotalMoney,@StorageTimingDiscountMoney,@StorageTimingIsCard,@StorageTimingPayCard,@StorageTimingIsCash,@StorageTimingPayCash,@StorageTimingIsBink,@StorageTimingPayBink,@StorageTimingPayCoupon,@StorageTimingPayType,@StorageTimingPoint,@StorageTimingRemark,@StorageTimingShopID,@StorageTimingUserID,@StorageTimingCreateTime,@StorageTotalTime,@StorageResidueTime,@StorageTimingProjectID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StorageTimingMemID", SqlDbType.Int, 4),
				new SqlParameter("@StorageTimingAccount", SqlDbType.NVarChar, 50),
				new SqlParameter("@StorageTimingTotalMoney", SqlDbType.Money, 8),
				new SqlParameter("@StorageTimingDiscountMoney", SqlDbType.Money, 8),
				new SqlParameter("@StorageTimingIsCard", SqlDbType.Bit, 1),
				new SqlParameter("@StorageTimingPayCard", SqlDbType.Money, 8),
				new SqlParameter("@StorageTimingIsCash", SqlDbType.Bit, 1),
				new SqlParameter("@StorageTimingPayCash", SqlDbType.Money, 8),
				new SqlParameter("@StorageTimingIsBink", SqlDbType.Bit, 1),
				new SqlParameter("@StorageTimingPayBink", SqlDbType.Money, 8),
				new SqlParameter("@StorageTimingPayCoupon", SqlDbType.Money, 8),
				new SqlParameter("@StorageTimingPayType", SqlDbType.Int, 4),
				new SqlParameter("@StorageTimingPoint", SqlDbType.Int, 4),
				new SqlParameter("@StorageTimingRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@StorageTimingShopID", SqlDbType.Int, 4),
				new SqlParameter("@StorageTimingUserID", SqlDbType.Int, 4),
				new SqlParameter("@StorageTimingCreateTime", SqlDbType.DateTime),
				new SqlParameter("@StorageTotalTime", SqlDbType.Int, 4),
				new SqlParameter("@StorageResidueTime", SqlDbType.Int, 4),
				new SqlParameter("@StorageTimingProjectID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.StorageTimingMemID;
			parameters[1].Value = model.StorageTimingAccount;
			parameters[2].Value = model.StorageTimingTotalMoney;
			parameters[3].Value = model.StorageTimingDiscountMoney;
			parameters[4].Value = model.StorageTimingIsCard;
			parameters[5].Value = model.StorageTimingPayCard;
			parameters[6].Value = model.StorageTimingIsCash;
			parameters[7].Value = model.StorageTimingPayCash;
			parameters[8].Value = model.StorageTimingIsBink;
			parameters[9].Value = model.StorageTimingPayBink;
			parameters[10].Value = model.StorageTimingPayCoupon;
			parameters[11].Value = model.StorageTimingPayType;
			parameters[12].Value = model.StorageTimingPoint;
			parameters[13].Value = model.StorageTimingRemark;
			parameters[14].Value = model.StorageTimingShopID;
			parameters[15].Value = model.StorageTimingUserID;
			parameters[16].Value = model.StorageTimingCreateTime;
			parameters[17].Value = model.StorageTotalTime;
			parameters[18].Value = model.StorageResidueTime;
			parameters[19].Value = model.StorageTimingProjectID;
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

		public bool Update(Chain.Model.MemStorageTiming model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MemStorageTiming set ");
			strSql.Append("StorageTimingMemID=@StorageTimingMemID,");
			strSql.Append("StorageTimingAccount=@StorageTimingAccount,");
			strSql.Append("StorageTimingTotalMoney=@StorageTimingTotalMoney,");
			strSql.Append("StorageTimingDiscountMoney=@StorageTimingDiscountMoney,");
			strSql.Append("StorageTimingIsCard=@StorageTimingIsCard,");
			strSql.Append("StorageTimingPayCard=@StorageTimingPayCard,");
			strSql.Append("StorageTimingIsCash=@StorageTimingIsCash,");
			strSql.Append("StorageTimingPayCash=@StorageTimingPayCash,");
			strSql.Append("StorageTimingIsBink=@StorageTimingIsBink,");
			strSql.Append("StorageTimingPayBink=@StorageTimingPayBink,");
			strSql.Append("StorageTimingPayCoupon=@StorageTimingPayCoupon,");
			strSql.Append("StorageTimingPayType=@StorageTimingPayType,");
			strSql.Append("StorageTimingPoint=@StorageTimingPoint,");
			strSql.Append("StorageTimingRemark=@StorageTimingRemark,");
			strSql.Append("StorageTimingShopID=@StorageTimingShopID,");
			strSql.Append("StorageTimingUserID=@StorageTimingUserID,");
			strSql.Append("StorageTimingCreateTime=@StorageTimingCreateTime,");
			strSql.Append("StorageTotalTime=@StorageTotalTime,");
			strSql.Append("StorageResidueTime=@StorageResidueTime");
			strSql.Append(" where StorageTimingID=@StorageTimingID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StorageTimingMemID", SqlDbType.Int, 4),
				new SqlParameter("@StorageTimingAccount", SqlDbType.NVarChar, 50),
				new SqlParameter("@StorageTimingTotalMoney", SqlDbType.Money, 8),
				new SqlParameter("@StorageTimingDiscountMoney", SqlDbType.Money, 8),
				new SqlParameter("@StorageTimingIsCard", SqlDbType.Bit, 1),
				new SqlParameter("@StorageTimingPayCard", SqlDbType.Money, 8),
				new SqlParameter("@StorageTimingIsCash", SqlDbType.Bit, 1),
				new SqlParameter("@StorageTimingPayCash", SqlDbType.Money, 8),
				new SqlParameter("@StorageTimingIsBink", SqlDbType.Bit, 1),
				new SqlParameter("@StorageTimingPayBink", SqlDbType.Money, 8),
				new SqlParameter("@StorageTimingPayCoupon", SqlDbType.Money, 8),
				new SqlParameter("@StorageTimingPayType", SqlDbType.Int, 4),
				new SqlParameter("@StorageTimingPoint", SqlDbType.Int, 4),
				new SqlParameter("@StorageTimingRemark", SqlDbType.NVarChar, 200),
				new SqlParameter("@StorageTimingShopID", SqlDbType.Int, 4),
				new SqlParameter("@StorageTimingUserID", SqlDbType.Int, 4),
				new SqlParameter("@StorageTimingCreateTime", SqlDbType.DateTime),
				new SqlParameter("@StorageTotalTime", SqlDbType.Int, 4),
				new SqlParameter("@StorageResidueTime", SqlDbType.Int, 4),
				new SqlParameter("@StorageTimingID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.StorageTimingMemID;
			parameters[1].Value = model.StorageTimingAccount;
			parameters[2].Value = model.StorageTimingTotalMoney;
			parameters[3].Value = model.StorageTimingDiscountMoney;
			parameters[4].Value = model.StorageTimingIsCard;
			parameters[5].Value = model.StorageTimingPayCard;
			parameters[6].Value = model.StorageTimingIsCash;
			parameters[7].Value = model.StorageTimingPayCash;
			parameters[8].Value = model.StorageTimingIsBink;
			parameters[9].Value = model.StorageTimingPayBink;
			parameters[10].Value = model.StorageTimingPayCoupon;
			parameters[11].Value = model.StorageTimingPayType;
			parameters[12].Value = model.StorageTimingPoint;
			parameters[13].Value = model.StorageTimingRemark;
			parameters[14].Value = model.StorageTimingShopID;
			parameters[15].Value = model.StorageTimingUserID;
			parameters[16].Value = model.StorageTimingCreateTime;
			parameters[17].Value = model.StorageTotalTime;
			parameters[18].Value = model.StorageResidueTime;
			parameters[19].Value = model.StorageTimingID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool UpdateStorageResidueTime(int StorageTimingID, int StorageResidueTime)
		{
			string strSql = string.Format("UPDATE MemStorageTiming SET StorageResidueTime = {0} WHERE StorageTimingID = {1}", StorageResidueTime, StorageTimingID);
			int rows = DbHelperSQL.ExecuteSql(strSql);
			return rows > 0;
		}

		public bool Delete(int StorageTimingID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemStorageTiming ");
			strSql.Append(" where StorageTimingID=@StorageTimingID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StorageTimingID", SqlDbType.Int, 4)
			};
			parameters[0].Value = StorageTimingID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string StorageTimingIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemStorageTiming ");
			strSql.Append(" where StorageTimingID in (" + StorageTimingIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MemStorageTiming GetModel(int StorageTimingID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 * from MemStorageTiming ");
			strSql.Append(" where StorageTimingID=@StorageTimingID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StorageTimingID", SqlDbType.Int, 4)
			};
			parameters[0].Value = StorageTimingID;
			Chain.Model.MemStorageTiming model = new Chain.Model.MemStorageTiming();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MemStorageTiming result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["StorageTimingID"] != null && ds.Tables[0].Rows[0]["StorageTimingID"].ToString() != "")
				{
					model.StorageTimingID = int.Parse(ds.Tables[0].Rows[0]["StorageTimingID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StorageTimingMemID"] != null && ds.Tables[0].Rows[0]["StorageTimingMemID"].ToString() != "")
				{
					model.StorageTimingMemID = int.Parse(ds.Tables[0].Rows[0]["StorageTimingMemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StorageTimingAccount"] != null && ds.Tables[0].Rows[0]["StorageTimingAccount"].ToString() != "")
				{
					model.StorageTimingAccount = ds.Tables[0].Rows[0]["StorageTimingAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["StorageTimingTotalMoney"] != null && ds.Tables[0].Rows[0]["StorageTimingTotalMoney"].ToString() != "")
				{
					model.StorageTimingTotalMoney = decimal.Parse(ds.Tables[0].Rows[0]["StorageTimingTotalMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StorageTimingDiscountMoney"] != null && ds.Tables[0].Rows[0]["StorageTimingDiscountMoney"].ToString() != "")
				{
					model.StorageTimingDiscountMoney = decimal.Parse(ds.Tables[0].Rows[0]["StorageTimingDiscountMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StorageTimingIsCard"] != null && ds.Tables[0].Rows[0]["StorageTimingIsCard"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["StorageTimingIsCard"].ToString() == "1" || ds.Tables[0].Rows[0]["StorageTimingIsCard"].ToString().ToLower() == "true")
					{
						model.StorageTimingIsCard = true;
					}
					else
					{
						model.StorageTimingIsCard = false;
					}
				}
				if (ds.Tables[0].Rows[0]["StorageTimingPayCard"] != null && ds.Tables[0].Rows[0]["StorageTimingPayCard"].ToString() != "")
				{
					model.StorageTimingPayCard = decimal.Parse(ds.Tables[0].Rows[0]["StorageTimingPayCard"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StorageTimingIsCash"] != null && ds.Tables[0].Rows[0]["StorageTimingIsCash"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["StorageTimingIsCash"].ToString() == "1" || ds.Tables[0].Rows[0]["StorageTimingIsCash"].ToString().ToLower() == "true")
					{
						model.StorageTimingIsCash = true;
					}
					else
					{
						model.StorageTimingIsCash = false;
					}
				}
				if (ds.Tables[0].Rows[0]["StorageTimingPayCash"] != null && ds.Tables[0].Rows[0]["StorageTimingPayCash"].ToString() != "")
				{
					model.StorageTimingPayCash = decimal.Parse(ds.Tables[0].Rows[0]["StorageTimingPayCash"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StorageTimingIsBink"] != null && ds.Tables[0].Rows[0]["StorageTimingIsBink"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["StorageTimingIsBink"].ToString() == "1" || ds.Tables[0].Rows[0]["StorageTimingIsBink"].ToString().ToLower() == "true")
					{
						model.StorageTimingIsBink = true;
					}
					else
					{
						model.StorageTimingIsBink = false;
					}
				}
				if (ds.Tables[0].Rows[0]["StorageTimingPayBink"] != null && ds.Tables[0].Rows[0]["StorageTimingPayBink"].ToString() != "")
				{
					model.StorageTimingPayBink = decimal.Parse(ds.Tables[0].Rows[0]["StorageTimingPayBink"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StorageTimingPayCoupon"] != null && ds.Tables[0].Rows[0]["StorageTimingPayCoupon"].ToString() != "")
				{
					model.StorageTimingPayCoupon = decimal.Parse(ds.Tables[0].Rows[0]["StorageTimingPayCoupon"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StorageTimingPayType"] != null && ds.Tables[0].Rows[0]["StorageTimingPayType"].ToString() != "")
				{
					model.StorageTimingPayType = int.Parse(ds.Tables[0].Rows[0]["StorageTimingPayType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StorageTimingPoint"] != null && ds.Tables[0].Rows[0]["StorageTimingPoint"].ToString() != "")
				{
					model.StorageTimingPoint = int.Parse(ds.Tables[0].Rows[0]["StorageTimingPoint"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StorageTimingRemark"] != null && ds.Tables[0].Rows[0]["StorageTimingRemark"].ToString() != "")
				{
					model.StorageTimingRemark = ds.Tables[0].Rows[0]["StorageTimingRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["StorageTimingShopID"] != null && ds.Tables[0].Rows[0]["StorageTimingShopID"].ToString() != "")
				{
					model.StorageTimingShopID = int.Parse(ds.Tables[0].Rows[0]["StorageTimingShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StorageTimingUserID"] != null && ds.Tables[0].Rows[0]["StorageTimingUserID"].ToString() != "")
				{
					model.StorageTimingUserID = int.Parse(ds.Tables[0].Rows[0]["StorageTimingUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StorageTimingCreateTime"] != null && ds.Tables[0].Rows[0]["StorageTimingCreateTime"].ToString() != "")
				{
					model.StorageTimingCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["StorageTimingCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StorageTotalTime"] != null && ds.Tables[0].Rows[0]["StorageTotalTime"].ToString() != "")
				{
					model.StorageTotalTime = int.Parse(ds.Tables[0].Rows[0]["StorageTotalTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StorageResidueTime"] != null && ds.Tables[0].Rows[0]["StorageResidueTime"].ToString() != "")
				{
					model.StorageResidueTime = int.Parse(ds.Tables[0].Rows[0]["StorageResidueTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StorageTimingProjectID"] != null && ds.Tables[0].Rows[0]["StorageTimingProjectID"].ToString() != "")
				{
					model.StorageTimingProjectID = int.Parse(ds.Tables[0].Rows[0]["StorageTimingProjectID"].ToString());
				}
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public DataSet GetAllTimeByMem(int memid, int projectid)
		{
			string strSql = string.Format("SELECT  ISNULL(SUM(StorageResidueTime),0) AS AllTime FROM MemStorageTiming WHERE StorageTimingMemID = {0} AND StorageTimingProjectID = {1}", memid, projectid);
			return DbHelperSQL.Query(strSql);
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM MemStorageTiming ");
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
			strSql.Append(" * FROM MemStorageTiming ");
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
			strSql.Append("select count(1) FROM MemStorageTiming ");
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
				strSql.Append("order by T.StorageTimingID desc");
			}
			strSql.Append(")AS Row, T.*  from MemStorageTiming T ");
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
			string tableName = "Mem,MemStorageTiming,SysShop,SysUser,MemLevel,TimingProject";
			string[] columns = new string[]
			{
				"MemStorageTiming.*,Mem.MemName,Mem.MemCard,SysShop.ShopName,SysUser.UserName,MemLevel.LevelName,TimingProject.ProjectName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "StorageTimingCreateTime", "StorageTimingID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetTimeTotal(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select isnull(sum(StorageTimingTotalMoney),0) as TotalMoney,isnull(sum(StorageTimingDiscountMoney),0) as DiscountMoney,isnull(sum(StorageTotalTime),0) as TotalTime,");
			strSql.Append(" isnull(sum(StorageResidueTime),0) as RemainTime,isnull(sum(StorageTimingPoint),0) as TotalPoint from MemStorageTiming,Mem,TimingProject");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where MemStorageTiming.StorageTimingMemID=Mem.MemID and MemStorageTiming.StorageTimingProjectID=TimingProject.ProjectID and " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
	}
}
