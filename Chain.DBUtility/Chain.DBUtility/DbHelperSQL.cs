using SQLDMO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Chain.DBUtility
{
	public abstract class DbHelperSQL
	{
		public static string connectionString = PubConstant.ConnectionString;

		public DbHelperSQL()
		{
		}

		public static string ProcessSql(string inputSql)
		{
			string pattern = "#(\\d{4}-\\d{1,2}-\\d{1,2}[^#]*)#";
			return Regex.Replace(inputSql, pattern, "'$1'");
		}

		private static string getCondition(string Sqlwhere, string[] condition)
		{
			if (condition != null && condition.Length > 0)
			{
				for (int i = 0; i < condition.Length; i++)
				{
					if (!(condition[i] == ""))
					{
						Sqlwhere += condition[i];
						if (i < condition.Length - 1)
						{
							Sqlwhere += " AND ";
						}
					}
				}
			}
			return DbHelperSQL.ProcessSql(Sqlwhere);
		}

		private static string getColumns(string SqlColumns, string[] column)
		{
			for (int i = 0; i < column.Length; i++)
			{
				SqlColumns += column[i];
				if (i < column.Length - 1)
				{
					SqlColumns += ",";
				}
			}
			return SqlColumns;
		}

		public static bool ColumnExists(string tableName, string columnName)
		{
			string sQLString = string.Concat(new string[]
			{
				"select count(1) from syscolumns where [id]=object_id('",
				tableName,
				"') and [name]='",
				columnName,
				"'"
			});
			object single = DbHelperSQL.GetSingle(sQLString);
			return single != null && Convert.ToInt32(single) > 0;
		}

		public static int GetMaxID(string FieldName, string TableName)
		{
			string sQLString = "select max(" + FieldName + ")+1 from " + TableName;
			object single = DbHelperSQL.GetSingle(sQLString);
			int result;
			if (single == null)
			{
				result = 1;
			}
			else
			{
				result = int.Parse(single.ToString());
			}
			return result;
		}

		public static bool Exists(string strSql)
		{
			strSql = DbHelperSQL.ProcessSql(strSql);
			object single = DbHelperSQL.GetSingle(strSql);
			int num;
			if (object.Equals(single, null) || object.Equals(single, DBNull.Value))
			{
				num = 0;
			}
			else
			{
				num = int.Parse(single.ToString());
			}
			return num != 0;
		}

		public static bool TabExists(string TableName)
		{
			string sQLString = "select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
			object single = DbHelperSQL.GetSingle(sQLString);
			int num;
			if (object.Equals(single, null) || object.Equals(single, DBNull.Value))
			{
				num = 0;
			}
			else
			{
				num = int.Parse(single.ToString());
			}
			return num != 0;
		}

		public static bool Exists(string strSql, params SqlParameter[] cmdParms)
		{
			strSql = DbHelperSQL.ProcessSql(strSql);
			object single = DbHelperSQL.GetSingle(strSql, cmdParms);
			int num;
			if (object.Equals(single, null) || object.Equals(single, DBNull.Value))
			{
				num = 0;
			}
			else
			{
				num = int.Parse(single.ToString());
			}
			return num != 0;
		}

		public static int ExecuteSql(string SQLString)
		{
			SQLString = DbHelperSQL.ProcessSql(SQLString);
			int result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(SQLString, sqlConnection))
				{
					try
					{
						sqlConnection.Open();
						int num = sqlCommand.ExecuteNonQuery();
						result = num;
					}
					catch (SqlException ex)
					{
						throw ex;
					}
					finally
					{
						sqlConnection.Close();
					}
				}
			}
			return result;
		}

		public static bool ExecuteSqlTran(ArrayList SQLStringList)
		{
			bool result = false;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = sqlConnection;
				SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
				sqlCommand.Transaction = sqlTransaction;
				try
				{
					for (int i = 0; i < SQLStringList.Count; i++)
					{
						string text = DbHelperSQL.ProcessSql((string)SQLStringList[i]);
						if (text.Trim().Length > 1)
						{
							sqlCommand.CommandText = text;
							int num = sqlCommand.ExecuteNonQuery();
						}
					}
					sqlTransaction.Commit();
					result = true;
				}
				catch
				{
					sqlTransaction.Rollback();
					result = false;
				}
				finally
				{
					sqlConnection.Close();
					sqlTransaction.Dispose();
				}
			}
			return result;
		}

		public static int ExecuteSql(string SQLString, string content)
		{
			SQLString = DbHelperSQL.ProcessSql(SQLString);
			int result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				SqlCommand sqlCommand = new SqlCommand(SQLString, sqlConnection);
				SqlParameter sqlParameter = new SqlParameter("@content", SqlDbType.NText);
				sqlParameter.Value = content;
				sqlCommand.Parameters.Add(sqlParameter);
				try
				{
					sqlConnection.Open();
					int num = sqlCommand.ExecuteNonQuery();
					result = num;
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				finally
				{
					sqlCommand.Dispose();
					sqlConnection.Close();
				}
			}
			return result;
		}

		public static object ExecuteSqlGet(string SQLString, string content)
		{
			SQLString = DbHelperSQL.ProcessSql(SQLString);
			object result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				SqlCommand sqlCommand = new SqlCommand(SQLString, sqlConnection);
				SqlParameter sqlParameter = new SqlParameter("@content", SqlDbType.NText);
				sqlParameter.Value = content;
				sqlCommand.Parameters.Add(sqlParameter);
				try
				{
					sqlConnection.Open();
					object obj = sqlCommand.ExecuteScalar();
					if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
					{
						result = null;
					}
					else
					{
						result = obj;
					}
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				finally
				{
					sqlCommand.Dispose();
					sqlConnection.Close();
				}
			}
			return result;
		}

		public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
		{
			strSQL = DbHelperSQL.ProcessSql(strSQL);
			int result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection);
				SqlParameter sqlParameter = new SqlParameter("@fs", SqlDbType.Image);
				sqlParameter.Value = fs;
				sqlCommand.Parameters.Add(sqlParameter);
				try
				{
					sqlConnection.Open();
					int num = sqlCommand.ExecuteNonQuery();
					result = num;
				}
				catch (SqlException ex)
				{
					throw ex;
				}
				finally
				{
					sqlCommand.Dispose();
					sqlConnection.Close();
				}
			}
			return result;
		}

		public static object GetSingle(string SQLString)
		{
			SQLString = DbHelperSQL.ProcessSql(SQLString);
			object result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(SQLString, sqlConnection))
				{
					try
					{
						sqlConnection.Open();
						object obj = sqlCommand.ExecuteScalar();
						if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
						{
							result = null;
						}
						else
						{
							result = obj;
						}
					}
					catch (SqlException ex)
					{
						throw ex;
					}
					finally
					{
						sqlConnection.Close();
					}
				}
			}
			return result;
		}

		public static object GetSingle(string SQLString, int Times)
		{
			SQLString = DbHelperSQL.ProcessSql(SQLString);
			object result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(SQLString, sqlConnection))
				{
					try
					{
						sqlConnection.Open();
						sqlCommand.CommandTimeout = Times;
						object obj = sqlCommand.ExecuteScalar();
						if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
						{
							result = null;
						}
						else
						{
							result = obj;
						}
					}
					catch (SqlException ex)
					{
						throw ex;
					}
					finally
					{
						sqlConnection.Close();
					}
				}
			}
			return result;
		}

		public static SqlDataReader ExecuteReader(string strSQL)
		{
			strSQL = DbHelperSQL.ProcessSql(strSQL);
			SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString);
			SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection);
			SqlDataReader result;
			try
			{
				sqlConnection.Open();
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				result = sqlDataReader;
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			return result;
		}

		public static DataSet Query(string SQLString)
		{
			SQLString = DbHelperSQL.ProcessSql(SQLString);
			DataSet result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				DataSet dataSet = new DataSet();
				try
				{
					sqlConnection.Open();
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SQLString, sqlConnection);
					sqlDataAdapter.Fill(dataSet, "ds");
				}
				catch (SqlException ex)
				{
					throw new Exception(ex.Message);
				}
				finally
				{
					sqlConnection.Close();
				}
				result = dataSet;
			}
			return result;
		}

		public static DataSet Query(string SQLString, int Times)
		{
			SQLString = DbHelperSQL.ProcessSql(SQLString);
			DataSet result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				DataSet dataSet = new DataSet();
				try
				{
					sqlConnection.Open();
					new SqlDataAdapter(SQLString, sqlConnection)
					{
						SelectCommand = 
						{
							CommandTimeout = Times
						}
					}.Fill(dataSet, "ds");
				}
				catch (SqlException ex)
				{
					throw new Exception(ex.Message);
				}
				finally
				{
					sqlConnection.Close();
				}
				result = dataSet;
			}
			return result;
		}

		public static int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
		{
			SQLString = DbHelperSQL.ProcessSql(SQLString);
			int result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				using (SqlCommand sqlCommand = new SqlCommand())
				{
					try
					{
						DbHelperSQL.PrepareCommand(sqlCommand, sqlConnection, null, SQLString, cmdParms);
						int num = sqlCommand.ExecuteNonQuery();
						sqlCommand.Parameters.Clear();
						result = num;
					}
					catch (SqlException ex)
					{
						throw ex;
					}
					finally
					{
						sqlConnection.Close();
						sqlCommand.Dispose();
					}
				}
			}
			return result;
		}

		public static bool ExecuteSqlTranPara(ArrayList SQLStringList)
		{
			bool result = false;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				sqlConnection.Open();
				using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
				{
					SqlCommand sqlCommand = new SqlCommand();
					try
					{
						foreach (DictionaryEntry dictionaryEntry in SQLStringList)
						{
							string cmdText = DbHelperSQL.ProcessSql(dictionaryEntry.Key.ToString());
							SqlParameter[] cmdParms = (SqlParameter[])dictionaryEntry.Value;
							DbHelperSQL.PrepareCommand(sqlCommand, sqlConnection, sqlTransaction, cmdText, cmdParms);
							int num = sqlCommand.ExecuteNonQuery();
							sqlCommand.Parameters.Clear();
						}
						sqlTransaction.Commit();
						result = true;
					}
					catch
					{
						sqlTransaction.Rollback();
						result = false;
						throw;
					}
					finally
					{
						sqlConnection.Close();
						sqlTransaction.Dispose();
						sqlCommand.Dispose();
					}
				}
			}
			return result;
		}

		public static int ExecuteSqlTran(List<CommandInfo> cmdList)
		{
			int result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				sqlConnection.Open();
				using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
				{
					SqlCommand sqlCommand = new SqlCommand();
					try
					{
						int num = 0;
						foreach (CommandInfo current in cmdList)
						{
							string cmdText = DbHelperSQL.ProcessSql(current.CommandText);
							SqlParameter[] cmdParms = (SqlParameter[])current.Parameters;
							DbHelperSQL.PrepareCommand(sqlCommand, sqlConnection, sqlTransaction, cmdText, cmdParms);
							if (current.EffentNextType == EffentNextType.WhenHaveContine || current.EffentNextType == EffentNextType.WhenNoHaveContine)
							{
								if (current.CommandText.ToLower().IndexOf("count(") == -1)
								{
									sqlTransaction.Rollback();
									result = 0;
									return result;
								}
								object obj = sqlCommand.ExecuteScalar();
								if (obj == null && obj == DBNull.Value)
								{
								}
								bool flag = Convert.ToInt32(obj) > 0;
								if (current.EffentNextType == EffentNextType.WhenHaveContine && !flag)
								{
									sqlTransaction.Rollback();
									result = 0;
									return result;
								}
								if (current.EffentNextType == EffentNextType.WhenNoHaveContine && flag)
								{
									sqlTransaction.Rollback();
									result = 0;
									return result;
								}
							}
							else
							{
								int num2 = sqlCommand.ExecuteNonQuery();
								num += num2;
								if (current.EffentNextType == EffentNextType.ExcuteEffectRows && num2 == 0)
								{
									sqlTransaction.Rollback();
									result = 0;
									return result;
								}
								sqlCommand.Parameters.Clear();
							}
						}
						sqlTransaction.Commit();
						result = num;
					}
					catch
					{
						sqlTransaction.Rollback();
						throw;
					}
					finally
					{
						sqlConnection.Close();
						sqlCommand.Dispose();
						sqlTransaction.Dispose();
					}
				}
			}
			return result;
		}

		public static void ExecuteSqlTranWithIndentity(List<CommandInfo> SQLStringList)
		{
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				sqlConnection.Open();
				using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
				{
					SqlCommand sqlCommand = new SqlCommand();
					try
					{
						int num = 0;
						foreach (CommandInfo current in SQLStringList)
						{
							string cmdText = DbHelperSQL.ProcessSql(current.CommandText);
							SqlParameter[] array = (SqlParameter[])current.Parameters;
							SqlParameter[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								SqlParameter sqlParameter = array2[i];
								if (sqlParameter.Direction == ParameterDirection.InputOutput)
								{
									sqlParameter.Value = num;
								}
							}
							DbHelperSQL.PrepareCommand(sqlCommand, sqlConnection, sqlTransaction, cmdText, array);
							int num2 = sqlCommand.ExecuteNonQuery();
							array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								SqlParameter sqlParameter = array2[i];
								if (sqlParameter.Direction == ParameterDirection.Output)
								{
									num = Convert.ToInt32(sqlParameter.Value);
								}
							}
							sqlCommand.Parameters.Clear();
						}
						sqlTransaction.Commit();
					}
					catch
					{
						sqlTransaction.Rollback();
						throw;
					}
					finally
					{
						sqlConnection.Close();
						sqlCommand.Dispose();
						sqlTransaction.Dispose();
					}
				}
			}
		}

		public static void ExecuteSqlTranWithIndentity(Hashtable SQLStringList)
		{
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				sqlConnection.Open();
				using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
				{
					SqlCommand sqlCommand = new SqlCommand();
					try
					{
						int num = 0;
						foreach (DictionaryEntry dictionaryEntry in SQLStringList)
						{
							string cmdText = DbHelperSQL.ProcessSql(dictionaryEntry.Key.ToString());
							SqlParameter[] array = (SqlParameter[])dictionaryEntry.Value;
							SqlParameter[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								SqlParameter sqlParameter = array2[i];
								if (sqlParameter.Direction == ParameterDirection.InputOutput)
								{
									sqlParameter.Value = num;
								}
							}
							DbHelperSQL.PrepareCommand(sqlCommand, sqlConnection, sqlTransaction, cmdText, array);
							int num2 = sqlCommand.ExecuteNonQuery();
							array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								SqlParameter sqlParameter = array2[i];
								if (sqlParameter.Direction == ParameterDirection.Output)
								{
									num = Convert.ToInt32(sqlParameter.Value);
								}
							}
							sqlCommand.Parameters.Clear();
						}
						sqlTransaction.Commit();
					}
					catch
					{
						sqlTransaction.Rollback();
						throw;
					}
					finally
					{
						sqlConnection.Close();
						sqlCommand.Dispose();
						sqlTransaction.Dispose();
					}
				}
			}
		}

		public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
		{
			SQLString = DbHelperSQL.ProcessSql(SQLString);
			object result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				using (SqlCommand sqlCommand = new SqlCommand())
				{
					try
					{
						DbHelperSQL.PrepareCommand(sqlCommand, sqlConnection, null, SQLString, cmdParms);
						object obj = sqlCommand.ExecuteScalar();
						sqlCommand.Parameters.Clear();
						if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
						{
							result = null;
						}
						else
						{
							result = obj;
						}
					}
					catch (SqlException ex)
					{
						throw ex;
					}
					finally
					{
						sqlConnection.Close();
						sqlCommand.Dispose();
					}
				}
			}
			return result;
		}

		public static SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
		{
			SQLString = DbHelperSQL.ProcessSql(SQLString);
			SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString);
			SqlCommand sqlCommand = new SqlCommand();
			SqlDataReader result;
			try
			{
				DbHelperSQL.PrepareCommand(sqlCommand, conn, null, SQLString, cmdParms);
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				sqlCommand.Parameters.Clear();
				result = sqlDataReader;
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			return result;
		}

		public static DataSet Query(string SQLString, params SqlParameter[] cmdParms)
		{
			SQLString = DbHelperSQL.ProcessSql(SQLString);
			DataSet result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				SqlCommand sqlCommand = new SqlCommand();
				DbHelperSQL.PrepareCommand(sqlCommand, sqlConnection, null, SQLString, cmdParms);
				using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
				{
					DataSet dataSet = new DataSet();
					try
					{
						sqlDataAdapter.Fill(dataSet, "ds");
						sqlCommand.Parameters.Clear();
					}
					catch (SqlException ex)
					{
						throw new Exception(ex.Message);
					}
					finally
					{
						sqlConnection.Close();
						sqlDataAdapter.Dispose();
					}
					result = dataSet;
				}
			}
			return result;
		}

		private static SqlParameter[] PrepareCommand(SqlParameter[] cmdParms)
		{
			SqlParameter[] array = new SqlParameter[cmdParms.Length];
			int num = 0;
			for (int i = 0; i < cmdParms.Length; i++)
			{
				SqlParameter sqlParameter = cmdParms[i];
				if ((sqlParameter.Direction == ParameterDirection.InputOutput || sqlParameter.Direction == ParameterDirection.Input) && sqlParameter.Value == null)
				{
					sqlParameter.Value = DBNull.Value;
				}
				array[num] = sqlParameter;
				num++;
			}
			return array;
		}

		private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
		{
			if (conn.State != ConnectionState.Open)
			{
				conn.Open();
			}
			cmd.Connection = conn;
			cmd.CommandText = cmdText;
			if (trans != null)
			{
				cmd.Transaction = trans;
			}
			cmd.CommandType = CommandType.Text;
			if (cmdParms != null)
			{
				for (int i = 0; i < cmdParms.Length; i++)
				{
					SqlParameter sqlParameter = cmdParms[i];
					if ((sqlParameter.Direction == ParameterDirection.InputOutput || sqlParameter.Direction == ParameterDirection.Input) && sqlParameter.Value == null)
					{
						sqlParameter.Value = DBNull.Value;
					}
					cmd.Parameters.Add(sqlParameter);
				}
			}
		}

		public static DataSet ExecuteProcedure(string procedure, SqlParameter[] parameter)
		{
			DataSet result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				sqlConnection.Open();
				DataSet dataSet = new DataSet();
				using (SqlCommand sqlCommand = new SqlCommand())
				{
					sqlCommand.Connection = sqlConnection;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.CommandText = procedure;
					if (parameter != null)
					{
						sqlCommand.Parameters.AddRange(DbHelperSQL.PrepareCommand(parameter));
					}
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(dataSet);
					}
					sqlCommand.Parameters.Clear();
					sqlConnection.Close();
				}
				result = dataSet;
			}
			return result;
		}

		public static DataSet ExecuteProcedure(string procedure, SqlParameter[] parameter, string conString)
		{
			DataSet result;
			using (SqlConnection sqlConnection = new SqlConnection(conString))
			{
				sqlConnection.Open();
				DataSet dataSet = new DataSet();
				using (SqlCommand sqlCommand = new SqlCommand())
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
					{
						sqlCommand.Connection = sqlConnection;
						sqlCommand.CommandType = CommandType.StoredProcedure;
						sqlCommand.CommandText = procedure;
						if (parameter != null)
						{
							sqlCommand.Parameters.AddRange(DbHelperSQL.PrepareCommand(parameter));
						}
						sqlDataAdapter.Fill(dataSet);
						sqlCommand.Parameters.Clear();
						sqlConnection.Close();
					}
				}
				result = dataSet;
			}
			return result;
		}

		public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
		{
			SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString);
			sqlConnection.Open();
			SqlCommand sqlCommand = DbHelperSQL.BuildQueryCommand(sqlConnection, storedProcName, parameters);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			return sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
		}

		public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
		{
			DataSet result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				DataSet dataSet = new DataSet();
				sqlConnection.Open();
				new SqlDataAdapter
				{
					SelectCommand = DbHelperSQL.BuildQueryCommand(sqlConnection, storedProcName, parameters)
				}.Fill(dataSet, tableName);
				sqlConnection.Close();
				result = dataSet;
			}
			return result;
		}



        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.SelectCommand.CommandTimeout = Times;
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }


		private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
		{
			SqlCommand sqlCommand = new SqlCommand(storedProcName, connection);
			sqlCommand.CommandTimeout = 360;
			sqlCommand.CommandType = CommandType.StoredProcedure;
			for (int i = 0; i < parameters.Length; i++)
			{
				SqlParameter sqlParameter = (SqlParameter)parameters[i];
				if (sqlParameter != null)
				{
					if ((sqlParameter.Direction == ParameterDirection.InputOutput || sqlParameter.Direction == ParameterDirection.Input) && sqlParameter.Value == null)
					{
						sqlParameter.Value = DBNull.Value;
					}
					sqlCommand.Parameters.Add(sqlParameter);
				}
			}
			return sqlCommand;
		}

		public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
		{
			int result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				sqlConnection.Open();
				SqlCommand sqlCommand = DbHelperSQL.BuildIntCommand(sqlConnection, storedProcName, parameters);
				rowsAffected = sqlCommand.ExecuteNonQuery();
				int num = (int)sqlCommand.Parameters["ReturnValue"].Value;
				result = num;
			}
			return result;
		}

		private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
		{
			SqlCommand sqlCommand = DbHelperSQL.BuildQueryCommand(connection, storedProcName, parameters);
			sqlCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
			return sqlCommand;
		}

		public static DataSet GetTable(string TableName, string[] column, string[] condition, string IndexColumn, bool IsAsc, int PageSize, int Page, int RecordCount)
		{
			string columns = DbHelperSQL.getColumns("", column);
			string condition2 = DbHelperSQL.getCondition("", condition);
			return DbHelperSQL.ExecuteProcedure("CP_PageNew", new List<SqlParameter>
			{
				new SqlParameter
				{
					ParameterName = "@TableName",
					Value = TableName,
					SqlDbType = SqlDbType.NVarChar,
					Size = 500
				},
				new SqlParameter
				{
					ParameterName = "@ReturnFields",
					Value = columns,
					SqlDbType = SqlDbType.NVarChar,
					Size = 4000
				},
				new SqlParameter
				{
					ParameterName = "@OrderFields",
					Value = IndexColumn,
					SqlDbType = SqlDbType.NVarChar,
					Size = 500
				},
				new SqlParameter
				{
					ParameterName = "@PageSize",
					Value = PageSize,
					SqlDbType = SqlDbType.Int,
					Size = 4
				},
				new SqlParameter
				{
					ParameterName = "@PageIndex",
					Value = Page,
					SqlDbType = SqlDbType.Int,
					Size = 4
				},
				new SqlParameter
				{
					ParameterName = "@PKField",
					Value = IndexColumn,
					SqlDbType = SqlDbType.NVarChar,
					Size = 255
				},
				new SqlParameter
				{
					ParameterName = "@IsDesc",
					Value = (IsAsc ? 0 : 1),
					SqlDbType = SqlDbType.Bit
				},
				new SqlParameter
				{
					ParameterName = "@Where",
					Value = condition2,
					SqlDbType = SqlDbType.NVarChar,
					Size = 4000
				}
			}.ToArray());
		}

		public static DataSet GetTable(string TableName, string[] column, string[] condition, string IndexColumn, string KeyField, bool IsAsc, int PageSize, int Page, int RecordCount)
		{
			string columns = DbHelperSQL.getColumns("", column);
			string condition2 = DbHelperSQL.getCondition("", condition);
			return DbHelperSQL.ExecuteProcedure("CP_PageNew", new List<SqlParameter>
			{
				new SqlParameter
				{
					ParameterName = "@TableName",
					Value = TableName,
					SqlDbType = SqlDbType.NVarChar,
					Size = 500
				},
				new SqlParameter
				{
					ParameterName = "@ReturnFields",
					Value = columns,
					SqlDbType = SqlDbType.NVarChar,
					Size = 4000
				},
				new SqlParameter
				{
					ParameterName = "@OrderFields",
					Value = IndexColumn,
					SqlDbType = SqlDbType.NVarChar,
					Size = 500
				},
				new SqlParameter
				{
					ParameterName = "@PageSize",
					Value = PageSize,
					SqlDbType = SqlDbType.Int,
					Size = 4
				},
				new SqlParameter
				{
					ParameterName = "@PageIndex",
					Value = Page,
					SqlDbType = SqlDbType.Int,
					Size = 4
				},
				new SqlParameter
				{
					ParameterName = "@PKField",
					Value = KeyField,
					SqlDbType = SqlDbType.NVarChar,
					Size = 255
				},
				new SqlParameter
				{
					ParameterName = "@IsDesc",
					Value = (IsAsc ? 0 : 1),
					SqlDbType = SqlDbType.Bit
				},
				new SqlParameter
				{
					ParameterName = "@Where",
					Value = condition2,
					SqlDbType = SqlDbType.NVarChar,
					Size = 4000
				}
			}.ToArray());
		}

		public static DataSet GetTable(string[] column, string[] condition, string IndexColumn, bool IsAsc, int PageSize, int Page, string procName)
		{
			string columns = DbHelperSQL.getColumns("", column);
			string condition2 = DbHelperSQL.getCondition("", condition);
			return DbHelperSQL.ExecuteProcedure(procName, new List<SqlParameter>
			{
				new SqlParameter
				{
					ParameterName = "@ReturnFields",
					Value = columns,
					SqlDbType = SqlDbType.VarChar,
					Size = 500
				},
				new SqlParameter
				{
					ParameterName = "@OrderFields",
					Value = IndexColumn,
					SqlDbType = SqlDbType.VarChar,
					Size = 255
				},
				new SqlParameter
				{
					ParameterName = "@PKField",
					Value = IndexColumn,
					SqlDbType = SqlDbType.VarChar,
					Size = 255
				},
				new SqlParameter
				{
					ParameterName = "@IsDesc",
					Value = (IsAsc ? 0 : 1),
					SqlDbType = SqlDbType.Bit
				},
				new SqlParameter
				{
					ParameterName = "@Where",
					Value = condition2,
					SqlDbType = SqlDbType.VarChar,
					Size = 1000
				},
				new SqlParameter
				{
					ParameterName = "@PageSize",
					Value = PageSize,
					SqlDbType = SqlDbType.Int
				},
				new SqlParameter
				{
					ParameterName = "@PageIndex",
					Value = Page,
					SqlDbType = SqlDbType.Int
				}
			}.ToArray());
		}

		public static string GetDataBaseName()
		{
			string result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				string database = sqlConnection.Database;
				sqlConnection.Close();
				result = database;
			}
			return result;
		}

		public static bool DataBaseBackup(string fullName)
		{
			bool result;
			using (SqlConnection sqlConnection = new SqlConnection(DbHelperSQL.connectionString))
			{
				string cmdText = string.Concat(new string[]
				{
					"use master;backup database [",
					sqlConnection.Database,
					"] to disk='",
					fullName,
					"'"
				});
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
				sqlCommand.ExecuteNonQuery();
				sqlConnection.Close();
				sqlCommand.Dispose();
				result = true;
			}
			return result;
		}

		public static bool DataBaseRecover(string fullName)
		{
			string sQLString = string.Format("SELECT physical_name,name,file_id FROM master.sys.master_files WHERE database_id=DB_ID('{0}');", new SqlConnection(DbHelperSQL.connectionString).Database.Trim());
			object[] array = new object[6];
			array[0] = new SqlConnection(DbHelperSQL.connectionString).Database.Trim();
			array[1] = fullName;
			bool flag = false;
			using (DataTable dataTable = DbHelperSQL.Query(sQLString).Tables[0])
			{
				if (dataTable.Rows.Count == 2)
				{
					array[2] = dataTable.Rows[0]["name"];
					array[3] = dataTable.Rows[0]["physical_name"];
					array[4] = dataTable.Rows[1]["name"];
					array[5] = dataTable.Rows[1]["physical_name"];
					flag = true;
				}
				else
				{
					sQLString = string.Format("RESTORE FILELISTONLY FROM DISK='{0}';", fullName);
					using (DataTable dataTable2 = DbHelperSQL.Query(sQLString).Tables[0])
					{
						if (dataTable2.Rows.Count == 2)
						{
							array[2] = dataTable2.Rows[0]["LogicalName"];
							array[3] = dataTable2.Rows[0]["PhysicalName"];
							array[4] = dataTable2.Rows[1]["LogicalName"];
							array[5] = dataTable2.Rows[1]["PhysicalName"];
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				DbHelperSQL.ExecuteSql(string.Format("RESTORE DATABASE {0} FROM DISK = '{1}' WITH MOVE '{2}' TO '{3}',MOVE '{4}' TO '{5}',STATS = 10, REPLACE;", array));
			}
			return flag;
		}

		public static bool RestoreDB(string fullName)
		{
			bool result = false;
			SQLServerClass sQLServerClass = new SQLServerClass();
			try
			{
				string value = Regex.Match(DbHelperSQL.connectionString, "server=(.*?);").Groups[1].Value;
				string value2 = Regex.Match(DbHelperSQL.connectionString, "database=(.*?);").Groups[1].Value;
				string value3 = Regex.Match(DbHelperSQL.connectionString, "uid=(.*?);").Groups[1].Value;
				string value4 = Regex.Match(DbHelperSQL.connectionString, "pwd=(.*?);").Groups[1].Value;
				sQLServerClass.LoginSecure = false;
				sQLServerClass.Connect(value, value3, value4);
				QueryResults queryResults = sQLServerClass.EnumProcesses(-1);
				int num = -1;
				int num2 = -1;
				for (int i = 1; i <= queryResults.Columns; i++)
				{
					string text = queryResults.get_ColumnName(i);
					if (text.ToUpper().Trim() == "SPID")
					{
						num = i;
					}
					else if (text.ToUpper().Trim() == "DBNAME")
					{
						num2 = i;
					}
					if (num != -1 && num2 != -1)
					{
						break;
					}
				}
				for (int i = 1; i <= queryResults.Rows; i++)
				{
					int columnLong = queryResults.GetColumnLong(i, num);
					string columnString = queryResults.GetColumnString(i, num2);
					if (columnString.ToUpper() == value2.ToUpper())
					{
						sQLServerClass.KillProcess(columnLong);
					}
				}
				new RestoreClass
				{
					Action = SQLDMO_RESTORE_TYPE.SQLDMORestore_Database,
					Database = value2,
					Files = fullName,
					FileNumber = 1,
					ReplaceDatabase = true
				}.SQLRestore(sQLServerClass);
				result = true;
			}
			catch
			{
				result = false;
			}
			finally
			{
				sQLServerClass.DisConnect();
			}
			return result;
		}
	}
}
