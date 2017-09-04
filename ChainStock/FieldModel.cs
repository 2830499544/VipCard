using System;

public class FieldModel
{
	public string FiledName;

	public string Field;

	public string FieldType;

	public bool FieldIsNull;

	public FieldModel(string filedName, string field, string fieldType, bool fieldIsNull)
	{
		this.FiledName = filedName;
		this.Field = field;
		this.FieldType = fieldType;
		this.FieldIsNull = fieldIsNull;
	}
}
