using System;

public class SqlMapModel
{
	private string _sqlcolumnname;

	private Type _datatype;

	private bool _nullvalue = true;

	public string SqlColumnName
	{
		get
		{
			return this._sqlcolumnname;
		}
	}

	public Type SqlDataType
	{
		get
		{
			return this._datatype;
		}
	}

	public bool SqlNullValue
	{
		get
		{
			return this._nullvalue;
		}
	}

	public SqlMapModel(string sqlColumnName, Type t, bool sqlNullValue)
	{
		this._datatype = t;
		this._sqlcolumnname = sqlColumnName;
		this._nullvalue = sqlNullValue;
	}

	public SqlMapModel(string sqlColumnName, Type t)
	{
		this._datatype = t;
		this._sqlcolumnname = sqlColumnName;
	}
}
