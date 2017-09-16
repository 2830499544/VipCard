using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class DataExcelInOfNPOI
{
	public static DataTable getExcelDataTable(string filePath, Dictionary<string, SqlMapModel> ColumnsMap, ref string err)
	{
		DataTable datatable = null;
		HSSFWorkbook xlsTables = DataExcelInOfNPOI.getWorkSheet(filePath);
		ISheet nowsheet = xlsTables.GetSheetAt(0);
		bool flag = DataExcelInOfNPOI.WriteData(nowsheet, ColumnsMap, ref datatable, ref err);
		if (flag)
		{
			datatable.Columns.Add("Error", typeof(string));
		}
		return datatable;
	}

	public static DataTable getExcelDataTable(string filePath, Dictionary<string, SqlMapModel> ColumnsMap)
	{
		DataTable datatable = null;
		HSSFWorkbook xlsTables = DataExcelInOfNPOI.getWorkSheet(filePath);
		ISheet nowsheet = xlsTables.GetSheetAt(0);
		bool flag = DataExcelInOfNPOI.WriteData(nowsheet, ColumnsMap, ref datatable);
		if (flag)
		{
			datatable.Columns.Add("Error", typeof(string));
		}
		return datatable;
	}

	private static bool WriteData(ISheet sheet, Dictionary<string, SqlMapModel> ColumnsMap, ref DataTable datatable, ref string err)
	{
		bool write = false;
		datatable = new DataTable();
		bool writestart = false;
		Dictionary<string, Type> dic = new Dictionary<string, Type>();
		bool result;
		for (int rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
		{
			IRow nowRow = sheet.GetRow(rowIndex);
			DataRow dr = datatable.NewRow();
			if (nowRow != null)
			{
				if (!writestart)
				{
					writestart = DataExcelInOfNPOI.CheckTable(nowRow, ColumnsMap, out datatable, out dic, ref err);
					if (!string.IsNullOrEmpty(err))
					{
						result = false;
						return result;
					}
				}
				else
				{
					bool row_emty = true;
					for (int cellIndex = 0; cellIndex < (int)nowRow.LastCellNum; cellIndex++)
					{
						ICell nowCell = nowRow.GetCell(cellIndex);
						if (row_emty)
						{
							row_emty = (nowCell == null || string.IsNullOrEmpty(nowCell.ToString()));
						}
						if (nowCell != null)
						{
							if (!write)
							{
								write = true;
							}
							if (dic[datatable.Columns[cellIndex].ColumnName] == typeof(DateTime))
							{
								try
								{
									dr[cellIndex] = nowCell.DateCellValue;
								}
								catch
								{
									dr[cellIndex] = nowCell;
								}
							}
							else
							{
								dr[cellIndex] = nowCell;
							}
						}
						else
						{
							dr[cellIndex] = nowCell;
						}
					}
					if (!row_emty)
					{
						datatable.Rows.Add(dr);
					}
				}
			}
		}
		result = write;
		return result;
	}

	private static bool WriteData(ISheet sheet, Dictionary<string, SqlMapModel> ColumnsMap, ref DataTable datatable)
	{
		bool write = false;
		datatable = new DataTable();
		bool writestart = false;
		Dictionary<string, Type> dic = new Dictionary<string, Type>();
		for (int rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
		{
			IRow nowRow = sheet.GetRow(rowIndex);
			DataRow dr = datatable.NewRow();
			if (nowRow != null)
			{
				if (!writestart)
				{
					writestart = DataExcelInOfNPOI.CheckTable(nowRow, ColumnsMap, out datatable, out dic);
				}
				else
				{
					bool row_emty = true;
					for (int cellIndex = 0; cellIndex < (int)nowRow.LastCellNum; cellIndex++)
					{
						ICell nowCell = nowRow.GetCell(cellIndex);
						if (row_emty)
						{
							row_emty = (nowCell == null || string.IsNullOrEmpty(nowCell.ToString()));
						}
						if (nowCell != null)
						{
							if (!write)
							{
								write = true;
							}
							if (dic[datatable.Columns[cellIndex].ColumnName] == typeof(DateTime))
							{
								try
								{
									dr[cellIndex] = nowCell.DateCellValue.ToShortDateString();
								}
								catch
								{
									dr[cellIndex] = nowCell;
								}
							}
							else
							{
								dr[cellIndex] = nowCell;
							}
						}
						else
						{
							dr[cellIndex] = nowCell;
						}
					}
					if (!row_emty)
					{
						datatable.Rows.Add(dr);
					}
				}
			}
		}
		return write;
	}

	private static bool xlstop(IRow ColumnsNameRow)
	{
		Regex regx = new Regex("\\s");
		bool result;
		for (int i = 0; i < (int)ColumnsNameRow.LastCellNum; i++)
		{
			ICell icell = ColumnsNameRow.GetCell(i);
			if (string.IsNullOrEmpty(regx.Replace(icell.ToString(), "")))
			{
				result = false;
				return result;
			}
		}
		result = true;
		return result;
	}

	private static bool CheckTable(IRow ColumnsNameRow, Dictionary<string, SqlMapModel> ColumnsMap, out DataTable datatable, out Dictionary<string, Type> dic, ref string err)
	{
		dic = new Dictionary<string, Type>();
		datatable = new DataTable();
		Regex regx = new Regex("\\s");
		bool flag = false;
		err = "";
		bool result;
		if (!DataExcelInOfNPOI.xlstop(ColumnsNameRow))
		{
			result = false;
		}
		else if ((int)ColumnsNameRow.LastCellNum == ColumnsMap.Count)
		{
			for (int i = 0; i < (int)ColumnsNameRow.LastCellNum; i++)
			{
				ICell icell = ColumnsNameRow.GetCell(i);
				string tValue = regx.Replace(icell.ToString(), "");
				if (Regex.IsMatch(tValue, "^\\*"))
				{
					tValue = tValue.Remove(0, 1);
				}
				if (!ColumnsMap.ContainsKey(tValue))
				{
					err = "系统发现异常列名(" + tValue + ")";
					dic = new Dictionary<string, Type>();
					datatable = new DataTable();
					result = false;
					return result;
				}
				if (!flag)
				{
					flag = true;
				}
				SqlMapModel SqlColumn = ColumnsMap[tValue];
				datatable.Columns.Add(SqlColumn.SqlColumnName, typeof(string));
				dic.Add(SqlColumn.SqlColumnName, SqlColumn.SqlDataType);
			}
			result = true;
		}
		else
		{
			StringBuilder Sb = new StringBuilder();
			if ((int)ColumnsNameRow.LastCellNum > ColumnsMap.Count)
			{
				Sb.AppendFormat("您选择的文件的列{0},多出系统要求({1}){2}列，参考列{3}", new object[]
				{
					ColumnsNameRow.LastCellNum,
					ColumnsMap.Count,
					(int)ColumnsNameRow.LastCellNum - ColumnsMap.Count,
					"{"
				});
				for (int i = 0; i < (int)ColumnsNameRow.LastCellNum; i++)
				{
					ICell icell = ColumnsNameRow.GetCell(i);
					string tValue = regx.Replace(icell.ToString(), "");
					if (Regex.IsMatch(tValue, "^\\*"))
					{
						tValue = tValue.Remove(0, 1);
					}
					if (!ColumnsMap.ContainsKey(tValue))
					{
						Sb.AppendFormat("{0},", icell.ToString());
					}
				}
				err = string.Format("{0}{1};", Sb.ToString().Trim(new char[]
				{
					','
				}), "}");
			}
			else
			{
				Sb.AppendFormat("您选择的文件的({0}列),有{2}列系统要求({1})的列未提供，参考列{3}", new object[]
				{
					ColumnsNameRow.LastCellNum,
					ColumnsMap.Count,
					Math.Abs((int)ColumnsNameRow.LastCellNum - ColumnsMap.Count),
					"{"
				});
				for (int i = 0; i < (int)ColumnsNameRow.LastCellNum; i++)
				{
					ICell icell = ColumnsNameRow.GetCell(i);
					string tValue = regx.Replace(icell.ToString(), "");
					if (Regex.IsMatch(tValue, "^\\*"))
					{
						tValue = tValue.Remove(0, 1);
					}
					if (ColumnsMap.ContainsKey(tValue))
					{
						ColumnsMap.Remove(tValue);
					}
					foreach (string j in ColumnsMap.Keys)
					{
						Sb.AppendFormat("{0},", j);
					}
				}
				err = string.Format("{0}{1};", Sb.ToString().Trim(new char[]
				{
					','
				}), "}");
			}
			result = false;
		}
		return result;
	}

	private static bool CheckTable(IRow ColumnsNameRow, Dictionary<string, SqlMapModel> ColumnsMap, out DataTable datatable, out Dictionary<string, Type> dic)
	{
		dic = new Dictionary<string, Type>();
		datatable = new DataTable();
		Regex regx = new Regex("\\s");
		bool flag = false;
		bool result;
		if (!DataExcelInOfNPOI.xlstop(ColumnsNameRow))
		{
			result = false;
		}
		else if ((int)ColumnsNameRow.LastCellNum == ColumnsMap.Count)
		{
			for (int i = 0; i < (int)ColumnsNameRow.LastCellNum; i++)
			{
				ICell icell = ColumnsNameRow.GetCell(i);
				string tValue = regx.Replace(icell.ToString(), "");
				if (Regex.IsMatch(tValue, "^\\*"))
				{
					tValue = tValue.Remove(0, 1);
				}
				if (!ColumnsMap.ContainsKey(tValue))
				{
					dic = new Dictionary<string, Type>();
					datatable = new DataTable();
					result = false;
					return result;
				}
				if (!flag)
				{
					flag = true;
				}
				SqlMapModel SqlColumn = ColumnsMap[tValue];
				datatable.Columns.Add(SqlColumn.SqlColumnName, typeof(string));
				dic.Add(SqlColumn.SqlColumnName, SqlColumn.SqlDataType);
			}
			result = true;
		}
		else
		{
			result = false;
		}
		return result;
	}

	private static HSSFWorkbook getWorkSheet(string FilePath)
	{
		FileStream filestream = File.OpenRead(FilePath);
		return new HSSFWorkbook(filestream);
	}

	public static bool CopyDataIntoSql(string ConnectionString, DataTable datatable, string SqltableName)
	{
		bool result;
		try
		{
			SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(ConnectionString, SqlBulkCopyOptions.UseInternalTransaction);
			for (int i = 0; i < datatable.Columns.Count; i++)
			{
				sqlbulkcopy.ColumnMappings.Add(datatable.Columns[i].ColumnName, datatable.Columns[i].ColumnName);
			}
			sqlbulkcopy.WriteToServer(datatable);
			result = true;
			return result;
		}
		catch
		{
		}
		result = false;
		return result;
	}
}
