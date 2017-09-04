using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.DAL
{
	public class MerchantSite
	{
		public bool Exists(int MerchantID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MerchantSite");
			strSql.Append(" where MerchantID=@MerchantID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MerchantID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MerchantID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.MerchantSite model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MerchantSite(");
			strSql.Append("MerchantDesc,MerchantPhoto,MerchantRemark)");
			strSql.Append(" values (");
			strSql.Append("@MerchantDesc,@MerchantPhoto,@MerchantRemark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MerchantDesc", SqlDbType.NVarChar, 50),
				new SqlParameter("@MerchantPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@MerchantRemark", SqlDbType.NVarChar, 500)
			};
			parameters[0].Value = model.MerchantDesc;
			parameters[1].Value = model.MerchantPhoto;
			parameters[2].Value = model.MerchantRemark;
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

		public bool Update(Chain.Model.MerchantSite model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MerchantSite set ");
			strSql.Append("MerchantDesc=@MerchantDesc,");
			strSql.Append("MerchantPhoto=@MerchantPhoto,");
			strSql.Append("MerchantRemark=@MerchantRemark");
			strSql.Append(" where MerchantID=@MerchantID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MerchantDesc", SqlDbType.NVarChar, 50),
				new SqlParameter("@MerchantPhoto", SqlDbType.NVarChar, 200),
				new SqlParameter("@MerchantRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@MerchantID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.MerchantDesc;
			parameters[1].Value = model.MerchantPhoto;
			parameters[2].Value = model.MerchantRemark;
			parameters[3].Value = model.MerchantID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int MerchantID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MerchantSite ");
			strSql.Append(" where MerchantID=@MerchantID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MerchantID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MerchantID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string MerchantIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MerchantSite ");
			strSql.Append(" where MerchantID in (" + MerchantIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MerchantSite GetModel(int MerchantID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 MerchantID,MerchantDesc,MerchantPhoto,MerchantRemark from MerchantSite ");
			strSql.Append(" where MerchantID=@MerchantID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MerchantID", SqlDbType.Int, 4)
			};
			parameters[0].Value = MerchantID;
			Chain.Model.MerchantSite model = new Chain.Model.MerchantSite();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MerchantSite result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["MerchantID"] != null && ds.Tables[0].Rows[0]["MerchantID"].ToString() != "")
				{
					model.MerchantID = int.Parse(ds.Tables[0].Rows[0]["MerchantID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["MerchantDesc"] != null && ds.Tables[0].Rows[0]["MerchantDesc"].ToString() != "")
				{
					model.MerchantDesc = ds.Tables[0].Rows[0]["MerchantDesc"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MerchantPhoto"] != null && ds.Tables[0].Rows[0]["MerchantPhoto"].ToString() != "")
				{
					model.MerchantPhoto = ds.Tables[0].Rows[0]["MerchantPhoto"].ToString();
				}
				if (ds.Tables[0].Rows[0]["MerchantRemark"] != null && ds.Tables[0].Rows[0]["MerchantRemark"].ToString() != "")
				{
					model.MerchantRemark = ds.Tables[0].Rows[0]["MerchantRemark"].ToString();
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
			strSql.Append("select MerchantID,MerchantDesc,MerchantPhoto,MerchantRemark ");
			strSql.Append(" FROM MerchantSite ");
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
			strSql.Append(" MerchantID,MerchantDesc,MerchantPhoto,MerchantRemark ");
			strSql.Append(" FROM MerchantSite ");
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
			strSql.Append("select count(1) FROM MerchantSite ");
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
				strSql.Append("order by T.MerchantID desc");
			}
			strSql.Append(")AS Row, T.*  from MerchantSite T ");
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
