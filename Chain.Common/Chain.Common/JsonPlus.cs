using System;
using System.Data;
using System.Reflection;
using System.Text;

namespace Chain.Common
{
	public class JsonPlus
	{
		public static string ToJson(object jsonObject)
		{
			string text = "{";
			PropertyInfo[] properties = jsonObject.GetType().GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				object obj = (properties[i].GetGetMethod().Invoke(jsonObject, null) == null) ? "" : properties[i].GetGetMethod().Invoke(jsonObject, null);
				string text2 = obj.ToString();
				string text3 = text;
				text = string.Concat(new string[]
				{
					text3,
					"\"",
					properties[i].Name,
					"\":\"",
					text2,
					"\","
				});
			}
			text = text.Substring(0, text.Length - 1);
			return text + "}";
		}

		public static DataTable DoubleAuotesEscape(DataTable dt)
		{
			if (dt != null && dt.Rows.Count > 0)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					for (int j = 0; j < dt.Columns.Count; j++)
					{
						if (dt.Rows[i][j] != null)
						{
							try
							{
								dt.Rows[i][j] = JsonPlus.StringFormat(dt.Rows[i][j].ToString(), dt.Columns[j].DataType);
							}
							catch
							{
							}
						}
					}
				}
			}
			return dt;
		}

        private static string StringFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = StringToJson(str);
                return str;
            }
            if ((type != typeof(DateTime)) && (type == typeof(bool)))
            {
            }
            return str;
        }

		private static string StringToJson(string s)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < s.Length)
			{
				char c = s.ToCharArray()[i];
				char c2 = c;
				if (c2 <= '"')
				{
					switch (c2)
					{
					case '\b':
						stringBuilder.Append("\\b");
						break;
					case '\t':
						stringBuilder.Append("\\t");
						break;
					case '\n':
						stringBuilder.Append("\\n");
						break;
					case '\v':
						goto IL_C0;
					case '\f':
						stringBuilder.Append("\\f");
						break;
					case '\r':
						stringBuilder.Append("\\r");
						break;
					default:
						if (c2 != '"')
						{
							goto IL_C0;
						}
						stringBuilder.Append("\\\"");
						break;
					}
				}
				else if (c2 != '/')
				{
					if (c2 != '\\')
					{
						goto IL_C0;
					}
					stringBuilder.Append("\\\\");
				}
				else
				{
					stringBuilder.Append("\\/");
				}
				IL_C8:
				i++;
				continue;
				IL_C0:
				stringBuilder.Append(c);
				goto IL_C8;
			}
			return stringBuilder.ToString();
		}

		public static string ToJson(DataTable dt, string fields)
		{
			dt = JsonPlus.DoubleAuotesEscape(dt);
			if (fields == "")
			{
				foreach (DataColumn dataColumn in dt.Columns)
				{
					if (fields != "")
					{
						fields += ",";
					}
					fields += dataColumn.ColumnName;
				}
			}
			string[] array = fields.Split(",".ToCharArray());
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			if (dt.Rows.Count > 0)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					stringBuilder.Append("{");
					for (int j = 0; j < array.Length; j++)
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							array[j],
							"\":\"",
							dt.Rows[i][array[j]].ToString(),
							"\""
						}));
						if (j < array.Length - 1)
						{
							stringBuilder.Append(",");
						}
					}
					stringBuilder.Append("}");
					if (i < dt.Rows.Count - 1)
					{
						stringBuilder.Append(",");
					}
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		public static string ToJson(DataRow[] drs, string fields)
		{
			if (fields == "")
			{
				foreach (DataColumn dataColumn in drs[0].Table.Columns)
				{
					if (fields != "")
					{
						fields += ",";
					}
					fields += dataColumn.ColumnName;
				}
			}
			string[] array = fields.Split(",".ToCharArray());
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			if (drs.Length > 0)
			{
				for (int i = 0; i < drs.Length; i++)
				{
					stringBuilder.Append("{");
					for (int j = 0; j < array.Length; j++)
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"\"",
							array[j],
							"\":\"",
							JsonPlus.StringFormat(drs[i][array[j]].ToString(), drs[i][array[j]].GetType()),
							"\""
						}));
						if (j < array.Length - 1)
						{
							stringBuilder.Append(",");
						}
					}
					stringBuilder.Append("}");
					if (i < drs.Length - 1)
					{
						stringBuilder.Append(",");
					}
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		public static string ToJson(DataRow row, string fields)
		{
			if (fields == "")
			{
				foreach (DataColumn dataColumn in row.Table.Columns)
				{
					if (fields != "")
					{
						fields += ",";
					}
					fields += dataColumn.ColumnName;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = fields.Split(",".ToCharArray());
			stringBuilder.Append("{");
			for (int i = 0; i < array.Length; i++)
			{
				if (stringBuilder.ToString() != "{")
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(string.Concat(new string[]
				{
					"\"",
					array[i],
					"\":\"",
					JsonPlus.StringFormat(row[array[i]].ToString(), row[array[i]].GetType()),
					"\""
				}));
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
