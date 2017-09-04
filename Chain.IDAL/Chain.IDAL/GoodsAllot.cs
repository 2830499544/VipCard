using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class GoodsAllot
	{
		public bool Exists(int AllotID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from GoodsAllot");
			strSql.Append(" where AllotID=@AllotID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AllotID", SqlDbType.Int, 4)
			};
			parameters[0].Value = AllotID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.GoodsAllot model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into GoodsAllot(");
			strSql.Append("AllotAccount,AllotOutShopID,AllotInShopID,AllotCreateTime,AllotTotalNumber,AllotUserID,AllotRemark,AllotState)");
			strSql.Append(" values (");
			strSql.Append("@AllotAccount,@AllotOutShopID,@AllotInShopID,@AllotCreateTime,@AllotTotalNumber,@AllotUserID,@AllotRemark,@AllotState)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AllotAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@AllotOutShopID", SqlDbType.Int, 4),
				new SqlParameter("@AllotInShopID", SqlDbType.Int, 4),
				new SqlParameter("@AllotCreateTime", SqlDbType.DateTime),
				new SqlParameter("@AllotTotalNumber", SqlDbType.Float, 8),
				new SqlParameter("@AllotUserID", SqlDbType.Int, 4),
				new SqlParameter("@AllotRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@AllotState", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.AllotAccount;
			parameters[1].Value = model.AllotOutShopID;
			parameters[2].Value = model.AllotInShopID;
			parameters[3].Value = model.AllotCreateTime;
			parameters[4].Value = model.AllotTotalNumber;
			parameters[5].Value = model.AllotUserID;
			parameters[6].Value = model.AllotRemark;
			parameters[7].Value = model.Allotstate;
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

		public bool Update(Chain.Model.GoodsAllot model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update GoodsAllot set ");
			strSql.Append("AllotAccount=@AllotAccount,");
			strSql.Append("AllotOutShopID=@AllotOutShopID,");
			strSql.Append("AllotInShopID=@AllotInShopID,");
			strSql.Append("AllotCreateTime=@AllotCreateTime,");
			strSql.Append("AllotTotalNumber=@AllotTotalNumber,");
			strSql.Append("AllotUserID=@AllotUserID,");
			strSql.Append("AllotRemark=@AllotRemark,");
			strSql.Append("AllotState=@AllotState");
			strSql.Append(" where AllotID=@AllotID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AllotAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@AllotOutShopID", SqlDbType.Int, 4),
				new SqlParameter("@AllotInShopID", SqlDbType.Int, 4),
				new SqlParameter("@AllotCreateTime", SqlDbType.DateTime),
				new SqlParameter("@AllotTotalNumber", SqlDbType.Float, 8),
				new SqlParameter("@AllotUserID", SqlDbType.Int, 4),
				new SqlParameter("@AllotRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@AllotState", SqlDbType.Int, 4),
				new SqlParameter("@AllotID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.AllotAccount;
			parameters[1].Value = model.AllotOutShopID;
			parameters[2].Value = model.AllotInShopID;
			parameters[3].Value = model.AllotCreateTime;
			parameters[4].Value = model.AllotTotalNumber;
			parameters[5].Value = model.AllotUserID;
			parameters[6].Value = model.AllotRemark;
			parameters[7].Value = model.Allotstate;
			parameters[8].Value = model.AllotID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int AllotID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsAllot ");
			strSql.Append(" where AllotID=@AllotID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AllotID", SqlDbType.Int, 4)
			};
			parameters[0].Value = AllotID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string AllotIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from GoodsAllot ");
			strSql.Append(" where AllotID in (" + AllotIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.GoodsAllot GetModel(int AllotID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 AllotID,AllotAccount,AllotOutShopID,AllotInShopID,AllotCreateTime,AllotTotalNumber,AllotUserID,AllotRemark,AllotState from GoodsAllot ");
			strSql.Append(" where AllotID=@AllotID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@AllotID", SqlDbType.Int, 4)
			};
			parameters[0].Value = AllotID;
			Chain.Model.GoodsAllot model = new Chain.Model.GoodsAllot();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.GoodsAllot result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["AllotID"] != null && ds.Tables[0].Rows[0]["AllotID"].ToString() != "")
				{
					model.AllotID = int.Parse(ds.Tables[0].Rows[0]["AllotID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AllotAccount"] != null && ds.Tables[0].Rows[0]["AllotAccount"].ToString() != "")
				{
					model.AllotAccount = ds.Tables[0].Rows[0]["AllotAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["AllotOutShopID"] != null && ds.Tables[0].Rows[0]["AllotOutShopID"].ToString() != "")
				{
					model.AllotOutShopID = int.Parse(ds.Tables[0].Rows[0]["AllotOutShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AllotInShopID"] != null && ds.Tables[0].Rows[0]["AllotInShopID"].ToString() != "")
				{
					model.AllotInShopID = int.Parse(ds.Tables[0].Rows[0]["AllotInShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AllotCreateTime"] != null && ds.Tables[0].Rows[0]["AllotCreateTime"].ToString() != "")
				{
					model.AllotCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["AllotCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AllotTotalNumber"] != null && ds.Tables[0].Rows[0]["AllotTotalNumber"].ToString() != "")
				{
					model.AllotTotalNumber = decimal.Parse(ds.Tables[0].Rows[0]["AllotTotalNumber"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AllotUserID"] != null && ds.Tables[0].Rows[0]["AllotUserID"].ToString() != "")
				{
					model.AllotUserID = int.Parse(ds.Tables[0].Rows[0]["AllotUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["AllotRemark"] != null && ds.Tables[0].Rows[0]["AllotRemark"].ToString() != "")
				{
					model.AllotRemark = ds.Tables[0].Rows[0]["AllotRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["AllotState"] != null && ds.Tables[0].Rows[0]["AllotState"].ToString() != "")
				{
					model.Allotstate = int.Parse(ds.Tables[0].Rows[0]["AllotState"].ToString());
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
			strSql.Append("select AllotID,AllotAccount,AllotOutShopID,AllotInShopID,AllotCreateTime,AllotTotalNumber,AllotUserID,AllotRemark,AllotState ");
			strSql.Append(" FROM GoodsAllot ");
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
			strSql.Append(" AllotID,AllotAccount,AllotOutShopID,AllotInShopID,AllotCreateTime,AllotTotalNumber,AllotUserID,AllotRemark,AllotState ");
			strSql.Append(" FROM GoodsAllot ");
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
			strSql.Append("select count(1) FROM GoodsAllot ");
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
				strSql.Append("order by T.AllotID desc");
			}
			strSql.Append(")AS Row, T.*  from GoodsAllot T ");
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
			string tableName = "GoodsAllot,SysShop,SysUser";
			string[] columns = new string[]
			{
				"GoodsAllot.*,SysShop.ShopName,SysUser.UserName,(select ShopName from SysShop where GoodsAllot.AllotOutShopID=SysShop.ShopID) as AllotOutShopName,(select ShopName from SysShop where GoodsAllot.AllotInShopID=SysShop.ShopID) as AllotInShopName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "AllotCreateTime", "AllotID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}
	}
}
