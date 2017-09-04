using System;

public class SmsTemplateParameter
{
	public string strCardID = "";

	public string strName = "";

	public decimal dclTempMoney = 0m;

	public decimal dclMoney = 0m;

	public int intTempPoint = 0;

	public int intPoint = 0;

	public int OldLevelID = 0;

	public int NewLevelID = 0;

	public DateTime MemBirthday = Convert.ToDateTime("1900-01-01");

	public DateTime MemPastTime = Convert.ToDateTime("2900-01-01");

	public string CountItemsString = string.Empty;
}
