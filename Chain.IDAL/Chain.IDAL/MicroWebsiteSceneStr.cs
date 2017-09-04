using Chain.DBUtility;
using Chain.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
	public class MicroWebsiteSceneStr
	{
		public int Add(Chain.Model.MicroWebsiteSceneStr model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into MicroWebsiteSceneStr(");
			strSql.Append("OppenId,SceneStr)");
			strSql.Append(" values (");
			strSql.Append("@OppenId,@SceneStr)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OppenId", SqlDbType.NVarChar, 500),
				new SqlParameter("@SceneStr", SqlDbType.Int)
			};
			parameters[0].Value = model.OppenId;
			parameters[1].Value = model.SceneStr;
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

		public bool Delete(string OppenId)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("delete from MicroWebsiteSceneStr ");
			strSql.Append(" where OppenId=@OppenId");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OppenId", SqlDbType.NVarChar, 500)
			};
			parameters[0].Value = OppenId;
			int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			return rows > 0;
		}

		public Chain.Model.MicroWebsiteSceneStr GetModel(string OppenId)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" select top 1 *");
			strSql.Append(" from MicroWebsiteSceneStr");
			strSql.Append(" where OppenId=@OppenId");
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@OppenId", SqlDbType.NVarChar, 500)
			};
			parameters[0].Value = OppenId;
			Chain.Model.MicroWebsiteSceneStr model = new Chain.Model.MicroWebsiteSceneStr();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
			Chain.Model.MicroWebsiteSceneStr result;
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

		public Chain.Model.MicroWebsiteSceneStr DataRowToModel(DataRow row)
		{
			Chain.Model.MicroWebsiteSceneStr model = new Chain.Model.MicroWebsiteSceneStr();
			if (row != null)
			{
				if (row["ID"] != null && row["ID"].ToString() != "")
				{
					model.ID = int.Parse(row["ID"].ToString());
				}
				if (row["OppenId"] != null)
				{
					model.OppenId = row["OppenId"].ToString();
				}
				if (row["SceneStr"] != null && row["SceneStr"].ToString() != "")
				{
					model.SceneStr = Convert.ToInt32(row["SceneStr"].ToString());
				}
			}
			return model;
		}
	}
}
