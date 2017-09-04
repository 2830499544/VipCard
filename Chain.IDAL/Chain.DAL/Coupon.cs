using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class Coupon
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID", "Coupon");
		}

		public bool Exists(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from Coupon");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.Coupon model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Coupon(");
			strSql.Append("IsGet,CouponTitle,CouponType,CouponNumber,CouponPredictNu,CouponEffective,CouponStart,CouponEnd,CouponDayNum,CouponMinMoney,CouponContent,CouponYF,CouponSY,CouponShopID)");
			strSql.Append(" values (");
			strSql.Append("@IsGet,@CouponTitle,@CouponType,@CouponNumber,@CouponPredictNu,@CouponEffective,@CouponStart,@CouponEnd,@CouponDayNum,@CouponMinMoney,@CouponContent,@CouponYF,@CouponSY,@CouponShopID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CouponTitle", SqlDbType.NVarChar, 50),
				new SqlParameter("@CouponType", SqlDbType.Int, 4),
				new SqlParameter("@CouponNumber", SqlDbType.Float, 8),
				new SqlParameter("@CouponPredictNu", SqlDbType.Int, 4),
				new SqlParameter("@CouponEffective", SqlDbType.Int, 4),
				new SqlParameter("@CouponStart", SqlDbType.DateTime),
				new SqlParameter("@CouponEnd", SqlDbType.DateTime),
				new SqlParameter("@CouponDayNum", SqlDbType.Int, 4),
				new SqlParameter("@CouponMinMoney", SqlDbType.Money, 8),
				new SqlParameter("@CouponContent", SqlDbType.NVarChar, 500),
				new SqlParameter("@CouponYF", SqlDbType.Int, 4),
				new SqlParameter("@CouponSY", SqlDbType.Int, 4),
				new SqlParameter("@CouponShopID", SqlDbType.Int, 4),
				new SqlParameter("@IsGet", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.CouponTitle;
			parameters[1].Value = model.CouponType;
			parameters[2].Value = model.CouponNumber;
			parameters[3].Value = model.CouponPredictNu;
			parameters[4].Value = model.CouponEffective;
			parameters[5].Value = model.CouponStart;
			parameters[6].Value = model.CouponEnd;
			parameters[7].Value = model.CouponDayNum;
			parameters[8].Value = model.CouponMinMoney;
			parameters[9].Value = model.CouponContent;
			parameters[10].Value = model.CouponYF;
			parameters[11].Value = model.CouponSY;
			parameters[12].Value = model.CouponShopID;
			parameters[13].Value = model.IsGet;
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

		public bool Update(Chain.Model.Coupon model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update Coupon set ");
			strSql.Append("CouponTitle=@CouponTitle,");
			strSql.Append("CouponType=@CouponType,");
			strSql.Append("CouponNumber=@CouponNumber,");
			strSql.Append("CouponPredictNu=@CouponPredictNu,");
			strSql.Append("CouponEffective=@CouponEffective,");
			strSql.Append("CouponStart=@CouponStart,");
			strSql.Append("CouponEnd=@CouponEnd,");
			strSql.Append("CouponDayNum=@CouponDayNum,");
			strSql.Append("CouponMinMoney=@CouponMinMoney,");
			strSql.Append("CouponContent=@CouponContent,");
			strSql.Append("CouponYF=@CouponYF,");
			strSql.Append("CouponSY=@CouponSY");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CouponTitle", SqlDbType.NVarChar, 50),
				new SqlParameter("@CouponType", SqlDbType.Int, 4),
				new SqlParameter("@CouponNumber", SqlDbType.Float, 8),
				new SqlParameter("@CouponPredictNu", SqlDbType.Int, 4),
				new SqlParameter("@CouponEffective", SqlDbType.Int, 4),
				new SqlParameter("@CouponStart", SqlDbType.DateTime),
				new SqlParameter("@CouponEnd", SqlDbType.DateTime),
				new SqlParameter("@CouponDayNum", SqlDbType.Int, 4),
				new SqlParameter("@CouponMinMoney", SqlDbType.Money, 8),
				new SqlParameter("@CouponContent", SqlDbType.NVarChar, 500),
				new SqlParameter("@CouponYF", SqlDbType.Int, 4),
				new SqlParameter("@CouponSY", SqlDbType.Int, 4),
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.CouponTitle;
			parameters[1].Value = model.CouponType;
			parameters[2].Value = model.CouponNumber;
			parameters[3].Value = model.CouponPredictNu;
			parameters[4].Value = model.CouponEffective;
			parameters[5].Value = model.CouponStart;
			parameters[6].Value = model.CouponEnd;
			parameters[7].Value = model.CouponDayNum;
			parameters[8].Value = model.CouponMinMoney;
			parameters[9].Value = model.CouponContent;
			parameters[10].Value = model.CouponYF;
			parameters[11].Value = model.CouponSY;
			parameters[12].Value = model.ID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Coupon ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string IDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Coupon ");
			strSql.Append(" where ID in (" + IDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.Coupon GetModel(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 ID,CouponTitle,CouponType,CouponNumber,CouponPredictNu,CouponEffective,CouponStart,CouponEnd,CouponDayNum,CouponMinMoney,CouponContent,CouponYF,CouponSY from Coupon ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			Chain.Model.Coupon model = new Chain.Model.Coupon();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.Coupon result;
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

		public Chain.Model.Coupon DataRowToModel(DataRow row)
		{
			Chain.Model.Coupon model = new Chain.Model.Coupon();
			if (row != null)
			{
				if (row["ID"] != null && row["ID"].ToString() != "")
				{
					model.ID = int.Parse(row["ID"].ToString());
				}
				if (row["CouponTitle"] != null)
				{
					model.CouponTitle = row["CouponTitle"].ToString();
				}
				if (row["CouponType"] != null && row["CouponType"].ToString() != "")
				{
					model.CouponType = int.Parse(row["CouponType"].ToString());
				}
				if (row["CouponNumber"] != null && row["CouponNumber"].ToString() != "")
				{
					model.CouponNumber = decimal.Parse(row["CouponNumber"].ToString());
				}
				if (row["CouponPredictNu"] != null && row["CouponPredictNu"].ToString() != "")
				{
					model.CouponPredictNu = int.Parse(row["CouponPredictNu"].ToString());
				}
				if (row["CouponEffective"] != null && row["CouponEffective"].ToString() != "")
				{
					model.CouponEffective = int.Parse(row["CouponEffective"].ToString());
				}
				if (row["CouponStart"] != null && row["CouponStart"].ToString() != "")
				{
					model.CouponStart = new DateTime?(DateTime.Parse(row["CouponStart"].ToString()));
				}
				if (row["CouponEnd"] != null && row["CouponEnd"].ToString() != "")
				{
					model.CouponEnd = new DateTime?(DateTime.Parse(row["CouponEnd"].ToString()));
				}
				if (row["CouponDayNum"] != null && row["CouponDayNum"].ToString() != "")
				{
					model.CouponDayNum = int.Parse(row["CouponDayNum"].ToString());
				}
				if (row["CouponMinMoney"] != null && row["CouponMinMoney"].ToString() != "")
				{
					model.CouponMinMoney = decimal.Parse(row["CouponMinMoney"].ToString());
				}
				if (row["CouponContent"] != null)
				{
					model.CouponContent = row["CouponContent"].ToString();
				}
				if (row["CouponYF"] != null && row["CouponYF"].ToString() != "")
				{
					model.CouponYF = int.Parse(row["CouponYF"].ToString());
				}
				if (row["CouponSY"] != null && row["CouponSY"].ToString() != "")
				{
					model.CouponSY = int.Parse(row["CouponSY"].ToString());
				}
			}
			return model;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select IsGet,ID,CouponTitle,CouponType,CouponNumber,CouponPredictNu,CouponEffective,CouponStart,CouponEnd,CouponDayNum,CouponMinMoney,CouponContent,CouponYF,CouponSY ");
			strSql.Append(" FROM Coupon ");
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
			strSql.Append(" ID,CouponTitle,CouponType,CouponNumber,CouponPredictNu,CouponEffective,CouponStart,CouponEnd,CouponDayNum,CouponMinMoney,CouponContent,CouponYF,CouponSY ");
			strSql.Append(" FROM Coupon ");
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
			strSql.Append("select count(1) FROM Coupon ");
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from Coupon T ");
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
			string tableName = "Coupon";
			string[] columns = new string[]
			{
				"* "
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
