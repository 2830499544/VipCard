using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class MemCustomField
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("CustomFieldID", "MemCustomField");
		}

		public bool Exists(string CustomField)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemCustomField");
			strSql.Append(" where CustomField = @CustomField ");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CustomField", SqlDbType.VarChar, 50)
			};
			parameters[0].Value = CustomField;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public bool Exists(int CustomFieldID, string CustomFieldName)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from MemCustomField");
			strSql.Append(" where CustomFieldName=@CustomFieldName and  CustomFieldID<>@CustomFieldID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CustomFieldName", SqlDbType.NVarChar, 50),
				new SqlParameter("@CustomFieldID", SqlDbType.Int)
			};
			parameters[0].Value = CustomFieldName;
			parameters[1].Value = CustomFieldID;
			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

		public int Add(Chain.Model.MemCustomField model)
		{
			string strTableName = (model.CustomType == 1) ? "Mem" : "Goods";
			string strColName = model.CustomField;
			string sqlAdd = string.Concat(new string[]
			{
				"Alter Table ",
				strTableName,
				" Add ",
				model.CustomField,
				"  Varchar(255)"
			});
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MemCustomField(");
			strSql.Append("CustomType,CustomField,CustomFieldName,CustomFieldIsNull,CustomFieldIsShow,CustomFieldType,CustomFieldInfo,CustomFieldShopID,CustomFieldCreateTime,CustomFieldUserID)");
			strSql.Append(" values (");
			strSql.Append("@CustomType,@CustomField,@CustomFieldName,@CustomFieldIsNull,@CustomFieldIsShow,@CustomFieldType,@CustomFieldInfo,@CustomFieldShopID,@CustomFieldCreateTime,@CustomFieldUserID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CustomType", SqlDbType.Int),
				new SqlParameter("@CustomField", SqlDbType.VarChar, 50),
				new SqlParameter("@CustomFieldName", SqlDbType.NVarChar, 50),
				new SqlParameter("@CustomFieldIsNull", SqlDbType.Bit, 1),
				new SqlParameter("@CustomFieldIsShow", SqlDbType.Bit, 1),
				new SqlParameter("@CustomFieldType", SqlDbType.VarChar, 50),
				new SqlParameter("@CustomFieldInfo", SqlDbType.VarChar, 500),
				new SqlParameter("@CustomFieldShopID", SqlDbType.Int, 4),
				new SqlParameter("@CustomFieldCreateTime", SqlDbType.DateTime),
				new SqlParameter("@CustomFieldUserID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.CustomType;
			parameters[1].Value = model.CustomField;
			parameters[2].Value = model.CustomFieldName;
			parameters[3].Value = model.CustomFieldIsNull;
			parameters[4].Value = model.CustomFieldIsShow;
			parameters[5].Value = model.CustomFieldType;
			parameters[6].Value = model.CustomFieldInfo;
			parameters[7].Value = model.CustomFieldShopID;
			parameters[8].Value = model.CustomFieldCreateTime;
			parameters[9].Value = model.CustomFieldUserID;
			DbHelperSQL.ExecuteSql(sqlAdd);
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

		public int Update(Chain.Model.MemCustomField model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update MemCustomField set ");
			strSql.Append("CustomType=@CustomType,");
			strSql.Append("CustomField=@CustomField,");
			strSql.Append("CustomFieldName=@CustomFieldName,");
			strSql.Append("CustomFieldIsNull=@CustomFieldIsNull,");
			strSql.Append("CustomFieldIsShow=@CustomFieldIsShow,");
			strSql.Append("CustomFieldType=@CustomFieldType,");
			strSql.Append("CustomFieldInfo=@CustomFieldInfo,");
			strSql.Append("CustomFieldShopID=@CustomFieldShopID,");
			strSql.Append("CustomFieldCreateTime=@CustomFieldCreateTime,");
			strSql.Append("CustomFieldUserID=@CustomFieldUserID");
			strSql.Append(" where CustomFieldID=@CustomFieldID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CustomType", SqlDbType.Int),
				new SqlParameter("@CustomField", SqlDbType.VarChar, 50),
				new SqlParameter("@CustomFieldName", SqlDbType.NVarChar, 50),
				new SqlParameter("@CustomFieldIsNull", SqlDbType.Bit, 1),
				new SqlParameter("@CustomFieldIsShow", SqlDbType.Bit, 1),
				new SqlParameter("@CustomFieldType", SqlDbType.VarChar, 50),
				new SqlParameter("@CustomFieldInfo", SqlDbType.VarChar, 500),
				new SqlParameter("@CustomFieldShopID", SqlDbType.Int, 4),
				new SqlParameter("@CustomFieldCreateTime", SqlDbType.DateTime),
				new SqlParameter("@CustomFieldUserID", SqlDbType.Int, 4),
				new SqlParameter("@CustomFieldID", SqlDbType.Int, 4)
			};
			parameters[0].Value = model.CustomType;
			parameters[1].Value = model.CustomField;
			parameters[2].Value = model.CustomFieldName;
			parameters[3].Value = model.CustomFieldIsNull;
			parameters[4].Value = model.CustomFieldIsShow;
			parameters[5].Value = model.CustomFieldType;
			parameters[6].Value = model.CustomFieldInfo;
			parameters[7].Value = model.CustomFieldShopID;
			parameters[8].Value = model.CustomFieldCreateTime;
			parameters[9].Value = model.CustomFieldUserID;
			parameters[10].Value = model.CustomFieldID;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			int result;
			if (rows > 0)
			{
				result = rows;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public bool Delete(int CustomFieldID)
		{
			Chain.Model.MemCustomField modelCustom = this.GetModel(CustomFieldID);
			string strTableName = (modelCustom.CustomType == 1) ? "Mem" : "Goods";
			string strColName = modelCustom.CustomField;
			string sqlDrop = "Alter Table " + strTableName + " Drop Column " + modelCustom.CustomField;
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemCustomField ");
			strSql.Append(" where CustomFieldID=@CustomFieldID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CustomFieldID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CustomFieldID;
			DbHelperSQL.ExecuteSql(sqlDrop);
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public bool DeleteList(string CustomFieldIDlist)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MemCustomField ");
			strSql.Append(" where CustomFieldID in (" + CustomFieldIDlist + ")  ");
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
			return rows > 0;
		}

		public Chain.Model.MemCustomField GetModel(int CustomFieldID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select  top 1 CustomFieldID,CustomType,CustomField,CustomFieldName,CustomFieldIsNull,CustomFieldIsShow,CustomFieldType,CustomFieldInfo,CustomFieldShopID,CustomFieldCreateTime,CustomFieldUserID from MemCustomField ");
			strSql.Append(" where CustomFieldID=@CustomFieldID");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@CustomFieldID", SqlDbType.Int, 4)
			};
			parameters[0].Value = CustomFieldID;
			Chain.Model.MemCustomField model = new Chain.Model.MemCustomField();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MemCustomField result;
			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["CustomFieldID"] != null && ds.Tables[0].Rows[0]["CustomFieldID"].ToString() != "")
				{
					model.CustomFieldID = int.Parse(ds.Tables[0].Rows[0]["CustomFieldID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CustomType"] != null && ds.Tables[0].Rows[0]["CustomType"].ToString() != "")
				{
					model.CustomType = int.Parse(ds.Tables[0].Rows[0]["CustomType"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CustomField"] != null && ds.Tables[0].Rows[0]["CustomField"].ToString() != "")
				{
					model.CustomField = ds.Tables[0].Rows[0]["CustomField"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CustomFieldName"] != null && ds.Tables[0].Rows[0]["CustomFieldName"].ToString() != "")
				{
					model.CustomFieldName = ds.Tables[0].Rows[0]["CustomFieldName"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CustomFieldIsNull"] != null && ds.Tables[0].Rows[0]["CustomFieldIsNull"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["CustomFieldIsNull"].ToString() == "1" || ds.Tables[0].Rows[0]["CustomFieldIsNull"].ToString().ToLower() == "true")
					{
						model.CustomFieldIsNull = true;
					}
					else
					{
						model.CustomFieldIsNull = false;
					}
				}
				if (ds.Tables[0].Rows[0]["CustomFieldIsShow"] != null && ds.Tables[0].Rows[0]["CustomFieldIsShow"].ToString() != "")
				{
					if (ds.Tables[0].Rows[0]["CustomFieldIsShow"].ToString() == "1" || ds.Tables[0].Rows[0]["CustomFieldIsShow"].ToString().ToLower() == "true")
					{
						model.CustomFieldIsShow = true;
					}
					else
					{
						model.CustomFieldIsShow = false;
					}
				}
				if (ds.Tables[0].Rows[0]["CustomFieldType"] != null && ds.Tables[0].Rows[0]["CustomFieldType"].ToString() != "")
				{
					model.CustomFieldType = ds.Tables[0].Rows[0]["CustomFieldType"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CustomFieldInfo"] != null && ds.Tables[0].Rows[0]["CustomFieldInfo"].ToString() != "")
				{
					model.CustomFieldInfo = ds.Tables[0].Rows[0]["CustomFieldInfo"].ToString();
				}
				if (ds.Tables[0].Rows[0]["CustomFieldShopID"] != null && ds.Tables[0].Rows[0]["CustomFieldShopID"].ToString() != "")
				{
					model.CustomFieldShopID = int.Parse(ds.Tables[0].Rows[0]["CustomFieldShopID"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CustomFieldCreateTime"] != null && ds.Tables[0].Rows[0]["CustomFieldCreateTime"].ToString() != "")
				{
					model.CustomFieldCreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CustomFieldCreateTime"].ToString());
				}
				if (ds.Tables[0].Rows[0]["CustomFieldUserID"] != null && ds.Tables[0].Rows[0]["CustomFieldUserID"].ToString() != "")
				{
					model.CustomFieldUserID = int.Parse(ds.Tables[0].Rows[0]["CustomFieldUserID"].ToString());
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
			strSql.Append("select CustomFieldID,CustomType,CustomField,CustomFieldName,CustomFieldIsNull,CustomFieldIsShow,CustomFieldType,CustomFieldInfo,CustomFieldShopID,CustomFieldCreateTime,CustomFieldUserID ");
			strSql.Append(" FROM MemCustomField ");
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
			strSql.Append(" CustomFieldID,CustomType,CustomField,CustomFieldName,CustomFieldIsNull,CustomFieldIsShow,CustomFieldType,CustomFieldInfo,CustomFieldShopID,CustomFieldCreateTime,CustomFieldUserID ");
			strSql.Append(" FROM MemCustomField ");
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
			strSql.Append("select count(1) FROM MemCustomField ");
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
				strSql.Append("order by T.CustomFieldID desc");
			}
			strSql.Append(")AS Row, T.*  from MemCustomField T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		public DataSet GetAllCustom()
		{
			string strSql = "select MemCustomField.* ,(select ShopName from SysShop where MemCustomField.CustomFieldShopID = SysShop.ShopID) as ShopName,(select UserName from SysUser where MemCustomField.CustomFieldUserID = SysUser.UserID) as UserName from MemCustomField";
			return DbHelperSQL.Query(strSql);
		}

		public int UpdateCustomFieldShow(string CustomFieldID, bool IsShow)
		{
			StringBuilder sbSql = new StringBuilder();
			sbSql.Append("update MemCustomField set CustomFieldIsShow='" + IsShow + "'");
			if (CustomFieldID != "" && CustomFieldID != null)
			{
				sbSql.Append(" where CustomFieldID=" + int.Parse(CustomFieldID));
			}
			return DbHelperSQL.ExecuteSql(sbSql.ToString());
		}
	}
}
