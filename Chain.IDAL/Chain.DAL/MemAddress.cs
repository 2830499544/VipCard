using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class MemAddress
	{
		public DataSet GetAlbumInfo(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "MemAddress,SysUser";
			string[] columns = new string[]
			{
				"ID,AlbumName,AlbumPhoto,AlbumDesc,AlbumCreateTime,UserName,AlbumRemark"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public bool Exists(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemAddress");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int UpdateDefaultAddressByID(int ID, int MemID, int IsDefault)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update  MemAddress set IsDefault=@IsDefault");
			strSql.Append(" where ID=@ID and MemID=@MemID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4),
				new SqlParameter("@IsDefault", SqlDbType.Int, 4),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			parameters[1].Value = IsDefault;
			parameters[2].Value = MemID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public int UpdateDefaultAddressByMemID(int MemID, int IsDefault)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update  MemAddress set IsDefault=@IsDefault ");
			strSql.Append(" where MemID=@MemID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@IsDefault", SqlDbType.Int, 4),
				new SqlParameter("@MemID", SqlDbType.Int, 4)
			};
			parameters[0].Value = IsDefault;
			parameters[1].Value = MemID;
			return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.MemAddress model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MemAddress(");
			strSql.Append("MemID,MemName,MemMobile,MemProvince,MemCity,MemCounty,MemVillage,IsDefault,MemDetailAddress)");
			strSql.Append(" values (");
			strSql.Append("@MemID,@MemName,@MemMobile,@MemProvince,@MemCity,@MemCounty,@MemVillage,@IsDefault,@MemDetailAddress)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@MemName", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemMobile", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemProvince", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemCity", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemCounty", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemVillage", SqlDbType.NVarChar, 50),
				new SqlParameter("@IsDefault", SqlDbType.Int, 4),
				new SqlParameter("@MemDetailAddress", SqlDbType.NVarChar, 500)
			};
			parameters[0].Value = model.MemID;
			parameters[1].Value = model.MemName;
			parameters[2].Value = model.MemMobile;
			parameters[3].Value = model.MemProvince;
			parameters[4].Value = model.MemCity;
			parameters[5].Value = model.MemCounty;
			parameters[6].Value = model.MemVillage;
			parameters[7].Value = model.IsDefault;
			parameters[8].Value = model.MemDetailAddress;
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

		public bool Update(Chain.Model.MemAddress model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MemAddress set ");
			strSql.Append("MemDetailAddress=@MemDetailAddress,");
			strSql.Append("MemID=@MemID,");
			strSql.Append("MemName=@MemName,");
			strSql.Append("MemMobile=@MemMobile,");
			strSql.Append("MemProvince=@MemProvince,");
			strSql.Append("MemCity=@MemCity,");
			strSql.Append("MemCounty=@MemCounty,");
			strSql.Append("MemVillage=@MemVillage,");
			strSql.Append("IsDefault=@IsDefault");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MemID", SqlDbType.Int, 4),
				new SqlParameter("@MemName", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemMobile", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemProvince", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemCity", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemCounty", SqlDbType.NVarChar, 50),
				new SqlParameter("@MemVillage", SqlDbType.NVarChar, 50),
				new SqlParameter("@IsDefault", SqlDbType.Int, 4),
				new SqlParameter("@ID", SqlDbType.Int, 4),
				new SqlParameter("@MemDetailAddress", SqlDbType.NVarChar, 500)
			};
			parameters[0].Value = model.MemID;
			parameters[1].Value = model.MemName;
			parameters[2].Value = model.MemMobile;
			parameters[3].Value = model.MemProvince;
			parameters[4].Value = model.MemCity;
			parameters[5].Value = model.MemCounty;
			parameters[6].Value = model.MemVillage;
			parameters[7].Value = model.IsDefault;
			parameters[8].Value = model.ID;
			parameters[9].Value = model.MemDetailAddress;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemAddress ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string AlbumIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemAddress ");
			strSql.Append(" where ID in (" + AlbumIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MemAddress GetModel(int ID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 MemDetailAddress, ID,MemID,MemName,MemMobile,MemProvince,MemCity,MemCounty,MemVillage,IsDefault from MemAddress ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4)
			};
			parameters[0].Value = ID;
			Chain.Model.MemAddress model = new Chain.Model.MemAddress();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MemAddress result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["MemDetailAddress"] != null && ds.Tables[0].Rows[0]["MemDetailAddress"].ToString() != "")
				{
					model.MemDetailAddress = ds.Tables[0].Rows[0]["MemDetailAddress"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MemCounty"] != null && ds.Tables[0].Rows[0]["MemCounty"].ToString() != "")
				{
					model.MemCounty = ds.Tables[0].Rows[0]["MemCounty"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MemVillage"] != null && ds.Tables[0].Rows[0]["MemVillage"].ToString() != "")
				{
					model.MemVillage = ds.Tables[0].Rows[0]["MemVillage"].ToString();
				}
				if (ds.Tables[0].Rows[0]["IsDefault"] != null && ds.Tables[0].Rows[0]["IsDefault"].ToString() != "")
				{
					model.IsDefault = int.Parse(ds.Tables[0].Rows[0]["IsDefault"].ToString());
				}
				if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
				{
					model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemID"] != null && ds.Tables[0].Rows[0]["MemID"].ToString() != "")
				{
					model.MemID = int.Parse(ds.Tables[0].Rows[0]["MemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MemName"] != null && ds.Tables[0].Rows[0]["MemName"].ToString() != "")
				{
					model.MemName = ds.Tables[0].Rows[0]["MemName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MemMobile"] != null && ds.Tables[0].Rows[0]["MemMobile"].ToString() != "")
				{
					model.MemMobile = ds.Tables[0].Rows[0]["MemMobile"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MemCity"] != null && ds.Tables[0].Rows[0]["MemCity"].ToString() != "")
				{
					model.MemCity = ds.Tables[0].Rows[0]["MemCity"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MemProvince"] != null && ds.Tables[0].Rows[0]["MemProvince"].ToString() != "")
				{
					model.MemProvince = ds.Tables[0].Rows[0]["MemProvince"].ToString();
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
			strSql.Append("select ID,MemID,MemName,MemMobile,MemProvince,MemCity,MemCounty,MemVillage,IsDefault,MemDetailAddress ");
			strSql.Append(" FROM MemAddress ");
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
			strSql.Append(" ID,AlbumName,AlbumPhoto,AlbumDesc,AlbumCreateTime ");
			strSql.Append(" FROM MemAddress ");
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
			strSql.Append("select count(1) FROM MemAddress ");
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
			strSql.Append(")AS Row, T.*  from MemAddress T ");
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
