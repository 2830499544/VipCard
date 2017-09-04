using Chain.DBUtility;
using Chain.Model;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class CouponList
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("CID", "CouponList");
		}

		public DataSet GetCouponDetailNew(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select Coupon.IsGet, CID,CouPonID,CouPon,CouponList.CouPonYF,CouponList.CouPonSY,CouPonMID,ConPonSendTime,ConPonUseTime,CouPonOrderAccount");
			strSql.Append(",ID,CouponTitle,CouponType,CouponNumber,CouponPredictNu,CouponEffective,CouponStart,CouponEnd,CouponDayNum,CouponMinMoney,CouponContent ");
			strSql.Append(" FROM CouponList,Coupon ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where  " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public bool Exists(int CID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from CouponList");
			strSql.Append(" where CID=@CID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool SendCoupon(int MemID, int CID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update CouponList set CouPonYF='True',CouPonMID=@CouPonMID,ConPonSendTime=getdate() where CID=@CID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CouPonMID", SqlDbType.Int, 4),
				new SqlParameter("@CID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MemID;
			parameters[1].Value = CID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public int GetMemCouponID(int CouPonID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select top 1 CID from  CouponList where CouPonYF='False' and CouPonID=@CouPonID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CouPonID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CouPonID;
			object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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

		public int Add(Chain.Model.CouponList model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into CouponList(");
			strSql.Append("CouPonID,CouPon,CouPonYF,CouPonSY)");
			strSql.Append(" values (");
			strSql.Append("@CouPonID,@CouPon,@CouPonYF,@CouPonSY)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CouPonID", SqlDbType.Int, 4),
				new SqlParameter("@CouPon", SqlDbType.NVarChar, 50),
				new SqlParameter("@CouPonYF", SqlDbType.Bit, 1),
				new SqlParameter("@CouPonSY", SqlDbType.Bit, 1)
			};
			parameters[0].Value = model.CouPonID;
			parameters[1].Value = model.CouPon;
			parameters[2].Value = false;
			parameters[3].Value = false;
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

		public bool Update(Chain.Model.CouponList model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update CouponList set ");
			strSql.Append("CouPonID=@CouPonID,");
			strSql.Append("CouPonYF=@CouPonYF,");
			strSql.Append("CouPonSY=@CouPonSY,");
			strSql.Append("CouPonMID=@CouPonMID,");
			strSql.Append("ConPonUseTime=@ConPonUseTime,");
			strSql.Append("CouPonOrderAccount=@CouPonOrderAccount ");
			strSql.Append(" where CID=@CID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CouPonID", SqlDbType.Int, 4),
				new SqlParameter("@CouPonYF", SqlDbType.Bit, 1),
				new SqlParameter("@CouPonSY", SqlDbType.Bit, 1),
				new SqlParameter("@CouPonMID", SqlDbType.Int, 4),
				new SqlParameter("@ConPonUseTime", SqlDbType.DateTime),
				new SqlParameter("@CouPonOrderAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@CID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.CouPonID;
			parameters[1].Value = model.CouPonYF;
			parameters[2].Value = model.CouPonSY;
			parameters[3].Value = model.CouPonMID;
			parameters[4].Value = model.ConPonUseTime;
			parameters[5].Value = model.CouPonOrderAccount;
			parameters[6].Value = model.CID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int CID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from CouponList ");
			strSql.Append(" where CID=@CID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string CIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from CouponList ");
			strSql.Append(" where CID in (" + CIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.CouponList GetModel(string CouPon)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 * from CouponList ");
			strSql.Append(" where CouPon=@CouPon");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CouPon", SqlDbType.NVarChar, 50)
			};
			parameters[0].Value = CouPon;
			Chain.Model.CouponList model = new Chain.Model.CouponList();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.CouponList result;
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

		public Chain.Model.CouponList DataRowToModel(DataRow row)
		{
			Chain.Model.CouponList model = new Chain.Model.CouponList();
			if (row != null)
			{
				if (row["CID"] != null && row["CID"].ToString() != "")
				{
					model.CID = int.Parse(row["CID"].ToString());
				}
				if (row["CouPonID"] != null && row["CouPonID"].ToString() != "")
				{
					model.CouPonID = int.Parse(row["CouPonID"].ToString());
				}
				if (row["CouPon"] != null)
				{
					model.CouPon = row["CouPon"].ToString();
				}
				if (row["CouPonYF"] != null && row["CouPonYF"].ToString() != "")
				{
					if (row["CouPonYF"].ToString() == "1" || row["CouPonYF"].ToString().ToLower() == "true")
					{
						model.CouPonYF = true;
					}
					else
					{
						model.CouPonYF = false;
					}
				}
				if (row["CouPonSY"] != null && row["CouPonSY"].ToString() != "")
				{
					if (row["CouPonSY"].ToString() == "1" || row["CouPonSY"].ToString().ToLower() == "true")
					{
						model.CouPonSY = true;
					}
					else
					{
						model.CouPonSY = false;
					}
				}
				if (row["CouPonMID"] != null && row["CouPonMID"].ToString() != "")
				{
					model.CouPonMID = int.Parse(row["CouPonMID"].ToString());
				}
				if (row["ConPonSendTime"] != null && row["ConPonSendTime"].ToString() != "")
				{
					model.ConPonSendTime = DateTime.Parse(row["ConPonSendTime"].ToString());
				}
				if (row["ConPonUseTime"] != null && row["ConPonUseTime"].ToString() != "")
				{
					model.ConPonUseTime = DateTime.Parse(row["ConPonUseTime"].ToString());
				}
				if (row["CouPonOrderAccount"] != null)
				{
					model.CouPonOrderAccount = row["CouPonOrderAccount"].ToString();
				}
			}
			return model;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select CID,CouPonID,CouPon,CouPonYF,CouPonSY,CouPonMID,ConPonSendTime,ConPonUseTime,CouPonOrderAccount ");
			strSql.Append(" FROM CouponList ");
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
			strSql.Append(" CID,CouPonID,CouPon,CouPonYF,CouPonSY,CouPonMID,ConPonSendTime,ConPonUseTime,CouPonOrderAccount ");
			strSql.Append(" FROM CouponList ");
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
			strSql.Append("select count(1) FROM CouponList ");
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

		public DataSet GetListSP(int PageSize, int PageIndex, string[] strWhere, out int resCount)
		{
			string tableName = " V_CouponList ";
			string[] columns = new string[]
			{
				"* "
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "CID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetMemInfoListSP(int PageSize, int PageIndex, string[] strWhere, out int resCount)
		{
			string tableName = " CouponList,Mem ";
			string[] columns = new string[]
			{
				" CouponList.* "
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "CID", true, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetCouponDetail(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select CID,CouPonID,CouPon,CouponList.CouPonYF,CouponList.CouPonSY,CouPonMID,ConPonSendTime,ConPonUseTime,CouPonOrderAccount");
			strSql.Append(",ID,CouponTitle,CouponType,CouponNumber,CouponPredictNu,CouponEffective,CouponStart,CouponEnd,CouponDayNum,CouponMinMoney,CouponContent,MemName ");
			strSql.Append(" FROM CouponList,Coupon,Mem ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where Mem.MemID=CouponList.CouPonMID and " + strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		public bool DataUpdateTran(ArrayList sqlArray)
		{
			return DbHelperSQL.ExecuteSqlTran(sqlArray);
		}
	}
}
