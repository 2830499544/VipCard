using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class Staff
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("StaffID", "Staff");
		}

		public bool Exists(int StaffID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from Staff");
			strSql.Append(" where StaffID=@StaffID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StaffID", SqlDbType.Int, 4)
			};
			parameters[0].Value = StaffID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(int intStaffID, string strStaffNumber)
		{
			string strSql = "select StaffID,StaffNumber from Staff where StaffID not in ({0}) and (StaffNumber = '{1}')";
			strSql = string.Format(strSql, intStaffID, strStaffNumber);
			DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
			return dt.Rows.Count > 0;
		}

		public int Add(Chain.Model.Staff model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into Staff(");
			strSql.Append("StaffNumber,StaffName,StaffSex,StaffMobile,StaffAddress,StaffClassID,StaffRemark)");
			strSql.Append(" values (");
			strSql.Append("@StaffNumber,@StaffName,@StaffSex,@StaffMobile,@StaffAddress,@StaffClassID,@StaffRemark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StaffNumber", SqlDbType.VarChar, 50),
				new SqlParameter("@StaffName", SqlDbType.NVarChar, 50),
				new SqlParameter("@StaffSex", SqlDbType.Bit, 1),
				new SqlParameter("@StaffMobile", SqlDbType.NVarChar, 50),
				new SqlParameter("@StaffAddress", SqlDbType.NVarChar, 500),
				new SqlParameter("@StaffClassID", SqlDbType.Int, 4),
				new SqlParameter("@StaffRemark", SqlDbType.NVarChar, 500)
			};
			parameters[0].Value = model.StaffNumber;
			parameters[1].Value = model.StaffName;
			parameters[2].Value = model.StaffSex;
			parameters[3].Value = model.StaffMobile;
			parameters[4].Value = model.StaffAddress;
			parameters[5].Value = model.StaffClassID;
			parameters[6].Value = model.StaffRemark;
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

		public int Update(Chain.Model.Staff model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update Staff set ");
			strSql.Append("StaffNumber=@StaffNumber,");
			strSql.Append("StaffName=@StaffName,");
			strSql.Append("StaffSex=@StaffSex,");
			strSql.Append("StaffMobile=@StaffMobile,");
			strSql.Append("StaffAddress=@StaffAddress,");
			strSql.Append("StaffClassID=@StaffClassID,");
			strSql.Append("StaffRemark=@StaffRemark");
			strSql.Append(" where StaffID=@StaffID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StaffNumber", SqlDbType.VarChar, 50),
				new SqlParameter("@StaffName", SqlDbType.NVarChar, 50),
				new SqlParameter("@StaffSex", SqlDbType.Bit, 1),
				new SqlParameter("@StaffMobile", SqlDbType.NVarChar, 50),
				new SqlParameter("@StaffAddress", SqlDbType.NVarChar, 500),
				new SqlParameter("@StaffClassID", SqlDbType.Int, 4),
				new SqlParameter("@StaffRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@StaffID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.StaffNumber;
			parameters[1].Value = model.StaffName;
			parameters[2].Value = model.StaffSex;
			parameters[3].Value = model.StaffMobile;
			parameters[4].Value = model.StaffAddress;
			parameters[5].Value = model.StaffClassID;
			parameters[6].Value = model.StaffRemark;
			parameters[7].Value = model.StaffID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public bool Delete(int StaffID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Staff ");
			strSql.Append(" where StaffID=@StaffID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StaffID", SqlDbType.Int, 4)
			};
			parameters[0].Value = StaffID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string StaffIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from Staff ");
			strSql.Append(" where StaffID in (" + StaffIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.Staff GetModel(int StaffID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 StaffID,StaffNumber,StaffName,StaffSex,StaffMobile,StaffAddress,StaffClassID,StaffRemark from Staff ");
			strSql.Append(" where StaffID=@StaffID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@StaffID", SqlDbType.Int, 4)
			};
			parameters[0].Value = StaffID;
			Chain.Model.Staff model = new Chain.Model.Staff();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.Staff result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["StaffID"] != null && ds.Tables[0].Rows[0]["StaffID"].ToString() != "")
				{
					model.StaffID = int.Parse(ds.Tables[0].Rows[0]["StaffID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StaffNumber"] != null && ds.Tables[0].Rows[0]["StaffNumber"].ToString() != "")
				{
					model.StaffNumber = ds.Tables[0].Rows[0]["StaffNumber"].ToString();
				}
				if (ds.Tables[0].Rows[0]["StaffName"] != null && ds.Tables[0].Rows[0]["StaffName"].ToString() != "")
				{
					model.StaffName = ds.Tables[0].Rows[0]["StaffName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["StaffSex"] != null && ds.Tables[0].Rows[0]["StaffSex"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["StaffSex"].ToString() == "1" || ds.Tables[0].Rows[0]["StaffSex"].ToString().ToLower() == "true")
					{
						model.StaffSex = true;
					}
					else
					{
						model.StaffSex = false;
					}
				}
				if (ds.Tables[0].Rows[0]["StaffMobile"] != null && ds.Tables[0].Rows[0]["StaffMobile"].ToString() != "")
				{
					model.StaffMobile = ds.Tables[0].Rows[0]["StaffMobile"].ToString();
				}
				if (ds.Tables[0].Rows[0]["StaffAddress"] != null && ds.Tables[0].Rows[0]["StaffAddress"].ToString() != "")
				{
					model.StaffAddress = ds.Tables[0].Rows[0]["StaffAddress"].ToString();
				}
				if (ds.Tables[0].Rows[0]["StaffClassID"] != null && ds.Tables[0].Rows[0]["StaffClassID"].ToString() != "")
				{
					model.StaffClassID = int.Parse(ds.Tables[0].Rows[0]["StaffClassID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["StaffRemark"] != null && ds.Tables[0].Rows[0]["StaffRemark"].ToString() != "")
				{
					model.StaffRemark = ds.Tables[0].Rows[0]["StaffRemark"].ToString();
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
			strSql.Append("select StaffID,StaffNumber,StaffName,StaffSex,StaffMobile,StaffAddress,StaffClassID,ClassName,ClassPercent,ShopName,StaffRemark ");
			strSql.Append(" FROM Staff,SysShop,StaffClass ");
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
			strSql.Append(" StaffID,StaffNumber,StaffName,StaffSex,StaffMobile,StaffAddress,StaffClassID,StaffRemark ");
			strSql.Append(" FROM Staff ");
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
			strSql.Append("select count(1) FROM Staff ");
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
				strSql.Append("order by T.StaffID desc");
			}
			strSql.Append(")AS Row, T.*  from Staff T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, string strTime, params string[] strWhere)
		{
			string tableName = "Staff,StaffClass,SysShop";
			string[] columns = new string[]
			{
				"Staff.*,StaffClass.ClassName,SysShop.ShopName,SysShop.ShopID,(select ISNULL(sum(StaffTotalMoney),0) from StaffMoney where Staff.StaffID=StaffMoney.StaffID and " + strTime + ") as TotalMoney"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ShopName", "StaffID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
