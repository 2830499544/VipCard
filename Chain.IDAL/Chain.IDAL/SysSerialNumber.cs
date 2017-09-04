using Chain.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Chain.IDAL
{
    public class SysSerialNumber
    {
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select *From V_SysSerialNumber");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetListSP(int PageSize, int PageIndex, out int resCount, params string[] strWhere)
        {
            string tableName = "V_SysSerialNumber";
            string[] columns = new string[]
            {
                "V_SysSerialNumber.*"
            };
            int recordCount = 1;
            DataSet ds = DbHelperSQL.GetTable(tableName, columns, strWhere, "ID", true, PageSize, PageIndex, recordCount);
            resCount = int.Parse(ds.Tables[1].Rows[0][0].ToString());
            return ds;
        }


        public int Update_Lock(int ID,int IsLock)
        {
            string strSql = string.Format("Update SysSerialNumber Set IsLock=@IsLock Where ID=@ID");
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@IsLock", SqlDbType.Int, 4),
                new SqlParameter("@ID", SqlDbType.Int, 4),
            };
            parameters[0].Value = IsLock;
            parameters[1].Value = ID;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            int result;
            if (rows > 0)
            {
                result = 1;
            }
            else
            {
                result = -3;
            }
            return result;
        }


        public int Update_Card(int ID,int IsCard)
        {
            string strSql = string.Format("Update SysSerialNumber Set IsCard=@IsCard Where ID=@ID");
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@IsCard", SqlDbType.Int, 4),
                new SqlParameter("@ID", SqlDbType.Int, 4),
            };
            parameters[0].Value = IsCard;
            parameters[1].Value = ID;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            int result;
            if (rows > 0)
            {
                result = 1;
            }
            else
            {
                result = -3;
            }
            return result;
        }

        public int Insert_SN(string SN,int IsLock)
        {
            string strSql = string.Format("Insert SysSerialNumber (SerialNumber,IsLock) Values(@SerialNumber,@IsLock)");
            SqlParameter[] parameters = new SqlParameter[]
           {
                new SqlParameter("@SerialNumber", SqlDbType.VarChar, 255),
                new SqlParameter("@IsLock", SqlDbType.Int, 4),
           };
            parameters[0].Value = SN;
            parameters[1].Value = IsLock;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            int result;
            if (rows > 0)
            {
                result = 1;
            }
            else
            {
                result = -3;
            }
            return result;

        }
    }
}
