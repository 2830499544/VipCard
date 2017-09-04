using System;

public static class SysNumerical
{
	private static string moneyformat = "{0:C2}";

	private static string numberfromat = "{0:N2}";

	public static string GetMoneyFormatString(object value)
	{
		string result;
		if (value == null)
		{
			result = string.Empty;
		}
		else if (string.IsNullOrEmpty(value.ToString().Trim()))
		{
			result = string.Empty;
		}
		else
		{
			decimal _val = 0m;
			if (!decimal.TryParse(value.ToString().Trim(), out _val))
			{
				throw new Exception("abnormality occurs in the format change");
			}
			result = string.Format(SysNumerical.moneyformat, _val);
		}
		return result;
	}

	public static string GetNumberFormatString(object value)
	{
		string result;
		if (value == null)
		{
			result = string.Empty;
		}
		else if (string.IsNullOrEmpty(value.ToString().Trim()))
		{
			result = string.Empty;
		}
		else
		{
			decimal _val = 0m;
			if (!decimal.TryParse(value.ToString().Trim(), out _val))
			{
				throw new Exception("abnormality occurs in the format change");
			}
			result = string.Format(SysNumerical.numberfromat, _val);
		}
		return result;
	}

	public static decimal getDecimalPoints(decimal money, decimal Proportion, bool _decimal)
	{
		return SysNumerical.getPoints(money, Proportion, 1m, _decimal);
	}

	public static int getIntPoints(decimal money, decimal Proportion, bool _decimal)
	{
		return (int)SysNumerical.getPoints(money, Proportion, 1m, _decimal);
	}

	public static decimal getPoints(decimal money, decimal MoneyModulus, decimal PointModulus, bool _decimal)
	{
		if (MoneyModulus == 0m)
		{
			throw new Exception("moneymodulus can not be zero");
		}
		if (MoneyModulus < 0m)
		{
			throw new Exception("moneymodulus base must be greater than zero");
		}
		if (PointModulus < 0m)
		{
			throw new Exception("pointmodulus base must be greater than zero");
		}
		decimal result;
		if (money == 0m || money < MoneyModulus || PointModulus == 0m)
		{
			result = 0m;
		}
		else
		{
			decimal points;
			if (!_decimal)
			{
				points = PointModulus * ((money - money % MoneyModulus) / MoneyModulus);
			}
			else
			{
				points = PointModulus * (money / MoneyModulus);
			}
			result = points;
		}
		return result;
	}
}
