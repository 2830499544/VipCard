using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class MemberSMSRemindLog
	{
		public DataSet GetMemBirthdayed()
		{
			string strSql = "select * from MemberSMSRemindLog where datepart(year,MemberSMSRemindTime)=datepart(year,getdate()) and MemberSMSRemindType=1";
			return DbHelperSQL.Query(strSql);
		}

		public DataSet GetMemPasted()
		{
			string strSql = "select * from MemberSMSRemindLog where datepart(year,MemberSMSRemindTime)=datepart(year,getdate()) and MemberSMSRemindType=2";
			return DbHelperSQL.Query(strSql);
		}

		public bool DeleteMemByMemID(int MemID)
		{
			string strSql = "delete from MemberSMSRemindLog where MemberSMSRemindMemID=@MemID";
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MemID;
			int rows = DbHelperSQL.ExecuteSql(strSql, parameters);
			return rows > 0;
		}

		public bool Exists(int MemberSMSRemindID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemberSMSRemindLog");
			strSql.Append(" where MemberSMSRemindID=@MemberSMSRemindID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemberSMSRemindID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MemberSMSRemindID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.MemberSMSRemindLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MemberSMSRemindLog(");
			strSql.Append("MemberSMSRemindMemID,MemberSMSRemindMobile,MemberSMSRemindContent,MemberSMSRemindShopID,MemberSMSRemindTime,MemberSMSRemindUserID,MemberSMSRemindAmount,MemberSMSRemindAllAmount,MemberSMSRemindType,MemberSMSRemindBirthday)");
			strSql.Append(" values (");
			strSql.Append("@MemberSMSRemindMemID,@MemberSMSRemindMobile,@MemberSMSRemindContent,@MemberSMSRemindShopID,@MemberSMSRemindTime,@MemberSMSRemindUserID,@MemberSMSRemindAmount,@MemberSMSRemindAllAmount,@MemberSMSRemindType,@MemberSMSRemindBirthday)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemberSMSRemindMemID", SqlDbType.Int, 4),
				new SqlParameter("@MemberSMSRemindMobile", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemberSMSRemindContent", SqlDbType.NVarChar, 1000),
				new SqlParameter("@MemberSMSRemindShopID", SqlDbType.Int, 4),
				new SqlParameter("@MemberSMSRemindTime", SqlDbType.DateTime),
				new SqlParameter("@MemberSMSRemindUserID", SqlDbType.Int, 4),
				new SqlParameter("@MemberSMSRemindAmount", SqlDbType.Int, 4),
				new SqlParameter("@MemberSMSRemindAllAmount", SqlDbType.Int, 4),
				new SqlParameter("@MemberSMSRemindType", SqlDbType.Int, 4),
				new SqlParameter("@MemberSMSRemindBirthday", SqlDbType.DateTime)
			};
			parameters[0].Value = model.MemberSMSRemindMemID;
			parameters[1].Value = model.MemberSMSRemindMobile;
			parameters[2].Value = model.MemberSMSRemindContent;
			parameters[3].Value = model.MemberSMSRemindShopID;
			parameters[4].Value = model.MemberSMSRemindTime;
			parameters[5].Value = model.MemberSMSRemindUserID;
			parameters[6].Value = model.MemberSMSRemindAmount;
			parameters[7].Value = model.MemberSMSRemindAllAmount;
			parameters[8].Value = model.MemberSMSRemindType;
			parameters[9].Value = model.MemberSMSRemindBirthday;
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

		public bool Update(Chain.Model.MemberSMSRemindLog model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MemberSMSRemindLog set ");
			strSql.Append("MemberSMSRemindMemID=@MemberSMSRemindMemID,");
			strSql.Append("MemberSMSRemindMobile=@MemberSMSRemindMobile,");
			strSql.Append("MemberSMSRemindContent=@MemberSMSRemindContent,");
			strSql.Append("MemberSMSRemindShopID=@MemberSMSRemindShopID,");
			strSql.Append("MemberSMSRemindTime=@MemberSMSRemindTime,");
			strSql.Append("MemberSMSRemindUserID=@MemberSMSRemindUserID,");
			strSql.Append("MemberSMSRemindAmount=@MemberSMSRemindAmount,");
			strSql.Append("MemberSMSRemindAllAmount=@MemberSMSRemindAllAmount,");
			strSql.Append("MemberSMSRemindType=@MemberSMSRemindType,");
			strSql.Append("MemberSMSRemindBirthday=@MemberSMSRemindBirthday");
			strSql.Append(" where MemberSMSRemindID=@MemberSMSRemindID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemberSMSRemindMemID", SqlDbType.Int, 4),
				new SqlParameter("@MemberSMSRemindMobile", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemberSMSRemindContent", SqlDbType.NVarChar, 1000),
				new SqlParameter("@MemberSMSRemindShopID", SqlDbType.Int, 4),
				new SqlParameter("@MemberSMSRemindTime", SqlDbType.DateTime),
				new SqlParameter("@MemberSMSRemindUserID", SqlDbType.Int, 4),
				new SqlParameter("@MemberSMSRemindAmount", SqlDbType.Int, 4),
				new SqlParameter("@MemberSMSRemindAllAmount", SqlDbType.Int, 4),
				new SqlParameter("@MemberSMSRemindType", SqlDbType.Int, 4),
				new SqlParameter("@MemberSMSRemindBirthday", SqlDbType.DateTime),
				new SqlParameter("@MemberSMSRemindID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MemberSMSRemindMemID;
			parameters[1].Value = model.MemberSMSRemindMobile;
			parameters[2].Value = model.MemberSMSRemindContent;
			parameters[3].Value = model.MemberSMSRemindShopID;
			parameters[4].Value = model.MemberSMSRemindTime;
			parameters[5].Value = model.MemberSMSRemindUserID;
			parameters[6].Value = model.MemberSMSRemindAmount;
			parameters[7].Value = model.MemberSMSRemindAllAmount;
			parameters[8].Value = model.MemberSMSRemindType;
			parameters[9].Value = model.MemberSMSRemindBirthday;
			parameters[10].Value = model.MemberSMSRemindID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int MemberSMSRemindID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemberSMSRemindLog ");
			strSql.Append(" where MemberSMSRemindID=@MemberSMSRemindID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemberSMSRemindID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MemberSMSRemindID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string MemberSMSRemindIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemberSMSRemindLog ");
			strSql.Append(" where MemberSMSRemindID in (" + MemberSMSRemindIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MemberSMSRemindLog GetModel(int MemberSMSRemindID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 MemberSMSRemindID,MemberSMSRemindMemID,MemberSMSRemindMobile,MemberSMSRemindContent,MemberSMSRemindShopID,MemberSMSRemindTime,MemberSMSRemindUserID,MemberSMSRemindAmount,MemberSMSRemindAllAmount,MemberSMSRemindType,MemberSMSRemindBirthday from MemberSMSRemindLog ");
			strSql.Append(" where MemberSMSRemindID=@MemberSMSRemindID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemberSMSRemindID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MemberSMSRemindID;
			Chain.Model.MemberSMSRemindLog model = new Chain.Model.MemberSMSRemindLog();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MemberSMSRemindLog result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["MemberSMSRemindID"] != null && ds.Tables[0].Rows[0]["MemberSMSRemindID"].ToString() != "")
				{
					model.MemberSMSRemindID = int.Parse(ds.Tables[0].Rows[0]["MemberSMSRemindID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemberSMSRemindMemID"] != null && ds.Tables[0].Rows[0]["MemberSMSRemindMemID"].ToString() != "")
				{
					model.MemberSMSRemindMemID = int.Parse(ds.Tables[0].Rows[0]["MemberSMSRemindMemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemberSMSRemindMobile"] != null && ds.Tables[0].Rows[0]["MemberSMSRemindMobile"].ToString() != "")
				{
					model.MemberSMSRemindMobile = ds.Tables[0].Rows[0]["MemberSMSRemindMobile"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MemberSMSRemindContent"] != null && ds.Tables[0].Rows[0]["MemberSMSRemindContent"].ToString() != "")
				{
					model.MemberSMSRemindContent = ds.Tables[0].Rows[0]["MemberSMSRemindContent"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MemberSMSRemindShopID"] != null && ds.Tables[0].Rows[0]["MemberSMSRemindShopID"].ToString() != "")
				{
					model.MemberSMSRemindShopID = int.Parse(ds.Tables[0].Rows[0]["MemberSMSRemindShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemberSMSRemindTime"] != null && ds.Tables[0].Rows[0]["MemberSMSRemindTime"].ToString() != "")
				{
					model.MemberSMSRemindTime = DateTime.Parse(ds.Tables[0].Rows[0]["MemberSMSRemindTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemberSMSRemindUserID"] != null && ds.Tables[0].Rows[0]["MemberSMSRemindUserID"].ToString() != "")
				{
					model.MemberSMSRemindUserID = int.Parse(ds.Tables[0].Rows[0]["MemberSMSRemindUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemberSMSRemindAmount"] != null && ds.Tables[0].Rows[0]["MemberSMSRemindAmount"].ToString() != "")
				{
					model.MemberSMSRemindAmount = int.Parse(ds.Tables[0].Rows[0]["MemberSMSRemindAmount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemberSMSRemindAllAmount"] != null && ds.Tables[0].Rows[0]["MemberSMSRemindAllAmount"].ToString() != "")
				{
					model.MemberSMSRemindAllAmount = int.Parse(ds.Tables[0].Rows[0]["MemberSMSRemindAllAmount"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemberSMSRemindType"] != null && ds.Tables[0].Rows[0]["MemberSMSRemindType"].ToString() != "")
				{
					model.MemberSMSRemindType = int.Parse(ds.Tables[0].Rows[0]["MemberSMSRemindType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemberSMSRemindBirthday"] != null && ds.Tables[0].Rows[0]["MemberSMSRemindBirthday"].ToString() != "")
				{
					model.MemberSMSRemindBirthday = DateTime.Parse(ds.Tables[0].Rows[0]["MemberSMSRemindBirthday"].ToString());
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
			strSql.Append("select MemberSMSRemindID,MemberSMSRemindMemID,MemberSMSRemindMobile,MemberSMSRemindContent,MemberSMSRemindShopID,MemberSMSRemindTime,MemberSMSRemindUserID,MemberSMSRemindAmount,MemberSMSRemindAllAmount,MemberSMSRemindType,MemberSMSRemindBirthday ");
			strSql.Append(" FROM MemberSMSRemindLog ");
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
			strSql.Append(" MemberSMSRemindID,MemberSMSRemindMemID,MemberSMSRemindMobile,MemberSMSRemindContent,MemberSMSRemindShopID,MemberSMSRemindTime,MemberSMSRemindUserID,MemberSMSRemindAmount,MemberSMSRemindAllAmount,MemberSMSRemindType,MemberSMSRemindBirthday ");
			strSql.Append(" FROM MemberSMSRemindLog ");
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
			strSql.Append("select count(1) FROM MemberSMSRemindLog ");
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
				strSql.Append("order by T.MemberSMSRemindID desc");
			}
			strSql.Append(")AS Row, T.*  from MemberSMSRemindLog T ");
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
