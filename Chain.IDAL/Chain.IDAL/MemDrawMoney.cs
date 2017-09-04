using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class MemDrawMoney
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("DrawMoneyID", "MemDrawMoney");
		}

		public bool Exists(int DrawMoneyID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemDrawMoney");
			strSql.Append(" where DrawMoneyID=@DrawMoneyID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DrawMoneyID", SqlDbType.Int, 4)
			};
			parameters[0].Value = DrawMoneyID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.MemDrawMoney model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MemDrawMoney(");
			strSql.Append("DrawMoneyMemID,DrawMoneyAccount,DrawMoney,DrawActualMoney,DrawMoneyRemark,DrawMoneyShopID,DrawMoneyUserID,DrawMoneyCreateTime)");
			strSql.Append(" values (");
			strSql.Append("@DrawMoneyMemID,@DrawMoneyAccount,@DrawMoney,@DrawActualMoney,@DrawMoneyRemark,@DrawMoneyShopID,@DrawMoneyUserID,@DrawMoneyCreateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DrawMoneyMemID", SqlDbType.Int, 4),
				new SqlParameter("@DrawMoneyAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@DrawMoney", SqlDbType.Money, 8),
				new SqlParameter("@DrawActualMoney", SqlDbType.Money, 8),
				new SqlParameter("@DrawMoneyRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@DrawMoneyShopID", SqlDbType.Int, 4),
				new SqlParameter("@DrawMoneyUserID", SqlDbType.Int, 4),
				new SqlParameter("@DrawMoneyCreateTime", SqlDbType.DateTime)
			};
			parameters[0].Value = model.DrawMoneyMemID;
			parameters[1].Value = model.DrawMoneyAccount;
			parameters[2].Value = model.DrawMoney;
			parameters[3].Value = model.DrawActualMoney;
			parameters[4].Value = model.DrawMoneyRemark;
			parameters[5].Value = model.DrawMoneyShopID;
			parameters[6].Value = model.DrawMoneyUserID;
			parameters[7].Value = model.DrawMoneyCreateTime;
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

		public bool Update(Chain.Model.MemDrawMoney model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MemDrawMoney set ");
			strSql.Append("DrawMoneyMemID=@DrawMoneyMemID,");
			strSql.Append("DrawMoneyAccount=@DrawMoneyAccount,");
			strSql.Append("DrawMoney=@DrawMoney,");
			strSql.Append("DrawActualMoney=@DrawActualMoney,");
			strSql.Append("DrawMoneyRemark=@DrawMoneyRemark,");
			strSql.Append("DrawMoneyShopID=@DrawMoneyShopID,");
			strSql.Append("DrawMoneyUserID=@DrawMoneyUserID,");
			strSql.Append("DrawMoneyCreateTime=@DrawMoneyCreateTime");
			strSql.Append(" where DrawMoneyID=@DrawMoneyID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DrawMoneyMemID", SqlDbType.Int, 4),
				new SqlParameter("@DrawMoneyAccount", SqlDbType.VarChar, 50),
				new SqlParameter("@DrawMoney", SqlDbType.Money, 8),
				new SqlParameter("@DrawActualMoney", SqlDbType.Money, 8),
				new SqlParameter("@DrawMoneyRemark", SqlDbType.NVarChar, 500),
				new SqlParameter("@DrawMoneyShopID", SqlDbType.Int, 4),
				new SqlParameter("@DrawMoneyUserID", SqlDbType.Int, 4),
				new SqlParameter("@DrawMoneyCreateTime", SqlDbType.DateTime),
				new SqlParameter("@DrawMoneyID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.DrawMoneyMemID;
			parameters[1].Value = model.DrawMoneyAccount;
			parameters[2].Value = model.DrawMoney;
			parameters[3].Value = model.DrawActualMoney;
			parameters[4].Value = model.DrawMoneyRemark;
			parameters[5].Value = model.DrawMoneyShopID;
			parameters[6].Value = model.DrawMoneyUserID;
			parameters[7].Value = model.DrawMoneyCreateTime;
			parameters[8].Value = model.DrawMoneyID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool Delete(int DrawMoneyID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemDrawMoney ");
			strSql.Append(" where DrawMoneyID=@DrawMoneyID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DrawMoneyID", SqlDbType.Int, 4)
			};
			parameters[0].Value = DrawMoneyID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string DrawMoneyIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemDrawMoney ");
			strSql.Append(" where DrawMoneyID in (" + DrawMoneyIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MemDrawMoney GetModel(int DrawMoneyID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 DrawMoneyID,DrawMoneyMemID,DrawMoneyAccount,DrawMoney,DrawActualMoney,DrawMoneyRemark,DrawMoneyShopID,DrawMoneyUserID,DrawMoneyCreateTime from MemDrawMoney ");
			strSql.Append(" where DrawMoneyID=@DrawMoneyID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DrawMoneyID", SqlDbType.Int, 4)
			};
			parameters[0].Value = DrawMoneyID;
			Chain.Model.MemDrawMoney model = new Chain.Model.MemDrawMoney();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MemDrawMoney result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["DrawMoneyID"] != null && ds.Tables[0].Rows[0]["DrawMoneyID"].ToString() != "")
				{
					model.DrawMoneyID = int.Parse(ds.Tables[0].Rows[0]["DrawMoneyID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DrawMoneyMemID"] != null && ds.Tables[0].Rows[0]["DrawMoneyMemID"].ToString() != "")
				{
					model.DrawMoneyMemID = int.Parse(ds.Tables[0].Rows[0]["DrawMoneyMemID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DrawMoneyAccount"] != null && ds.Tables[0].Rows[0]["DrawMoneyAccount"].ToString() != "")
				{
					model.DrawMoneyAccount = ds.Tables[0].Rows[0]["DrawMoneyAccount"].ToString();
				}
				if (ds.Tables[0].Rows[0]["DrawMoney"] != null && ds.Tables[0].Rows[0]["DrawMoney"].ToString() != "")
				{
					model.DrawMoney = decimal.Parse(ds.Tables[0].Rows[0]["DrawMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DrawActualMoney"] != null && ds.Tables[0].Rows[0]["DrawActualMoney"].ToString() != "")
				{
					model.DrawActualMoney = decimal.Parse(ds.Tables[0].Rows[0]["DrawActualMoney"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DrawMoneyRemark"] != null && ds.Tables[0].Rows[0]["DrawMoneyRemark"].ToString() != "")
				{
					model.DrawMoneyRemark = ds.Tables[0].Rows[0]["DrawMoneyRemark"].ToString();
				}
				if (ds.Tables[0].Rows[0]["DrawMoneyShopID"] != null && ds.Tables[0].Rows[0]["DrawMoneyShopID"].ToString() != "")
				{
					model.DrawMoneyShopID = int.Parse(ds.Tables[0].Rows[0]["DrawMoneyShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DrawMoneyUserID"] != null && ds.Tables[0].Rows[0]["DrawMoneyUserID"].ToString() != "")
				{
					model.DrawMoneyUserID = int.Parse(ds.Tables[0].Rows[0]["DrawMoneyUserID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["DrawMoneyCreateTime"] != null && ds.Tables[0].Rows[0]["DrawMoneyCreateTime"].ToString() != "")
				{
					model.DrawMoneyCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["DrawMoneyCreateTime"].ToString());
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
			strSql.Append("select DrawMoneyID,DrawMoneyMemID,DrawMoneyAccount,DrawMoney,DrawActualMoney,DrawMoneyRemark,DrawMoneyShopID,DrawMoneyUserID,DrawMoneyCreateTime ");
			strSql.Append(" FROM MemDrawMoney ");
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
			strSql.Append(" DrawMoneyID,DrawMoneyMemID,DrawMoneyAccount,DrawMoney,DrawActualMoney,DrawMoneyRemark,DrawMoneyShopID,DrawMoneyUserID,DrawMoneyCreateTime ");
			strSql.Append(" FROM MemDrawMoney ");
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
			strSql.Append("select count(1) FROM MemDrawMoney ");
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

		public DataSet GetDataByTime(DateTime starttime, DateTime endtime, string strwhere)
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
			return DbHelperSQL.RunProcedure("MonthDrawMoney", parameters, "#DrawMoneyData");
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
				strSql.Append("order by T.DrawMoneyID desc");
			}
			strSql.Append(")AS Row, T.*  from MemDrawMoney T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public decimal GetDrawMoney(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select sum(DrawActualMoney) from MemDrawMoney,Mem,MemLevel");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			decimal result;
			if (obj == null)
			{
				result = 0m;
			}
			else
			{
				result = Convert.ToDecimal(obj);
			}
			return result;
		}

		public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
		{
			string tableName = "MemDrawMoney,Mem,MemLevel,SysShop,SysUser";
			string[] columns = new string[]
			{
				"MemDrawMoney.*,Mem.*,MemLevel.LevelName,SysShop.ShopName,SysUser.UserName"
			};
			int recordCount = 1;
			DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "DrawMoneyID", false, PageSize, PageIndex, recordCount);
			resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
			return ds;
		}

		public DataSet GetDrawMoneyCount(string strSql)
		{
			StringBuilder strWhere = new StringBuilder();
			strWhere.Append(" select isnull(sum(DrawMoney),0) as DrawMoney,isnull(sum(DrawActualMoney),0) as DrawActualMoney from MemDrawMoney,Mem ");
			if (strSql.Trim() != "")
			{
				strWhere.Append(" where " + strSql);
			}
			return DbHelperSQL.Query(strWhere.ToString());
		}
	}
}
