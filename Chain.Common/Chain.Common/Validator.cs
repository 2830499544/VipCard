using System;
using System.Text.RegularExpressions;

namespace Chain.Common
{
	public class Validator
	{
		public static bool IsNumber(string strln)
		{
			return Regex.IsMatch(strln, "^([0]|([1-9]+\\d{0,}?))(.[\\d]+)?$");
		}

		public static bool IsDateTime(string strln)
		{
			if (strln == null)
			{
				return false;
			}
			string pattern = "[1-2]{1}[0-9]{3}((-|\\/|\\.){1}(([0]?[1-9]{1})|(1[0-2]{1}))((-|\\/|\\.){1}((([0]?[1-9]{1})|([1-2]{1}[0-9]{1})|(3[0-1]{1})))( (([0-1]{1}[0-9]{1})|2[0-3]{1}):([0-5]{1}[0-9]{1}):([0-5]{1}[0-9]{1})(\\.[0-9]{3})?)?)?)?$";
			if (Regex.IsMatch(strln, pattern))
			{
				int num;
				int num2;
				int num3;
				if (-1 != (num = strln.IndexOf("-")))
				{
					num2 = strln.IndexOf("-", num + 1);
					num3 = strln.IndexOf(":");
				}
				else
				{
					num = strln.IndexOf("/");
					num2 = strln.IndexOf("/", num + 1);
					num3 = strln.IndexOf(":");
				}
				if (-1 == num2)
				{
					return true;
				}
				if (-1 == num3)
				{
					num3 = strln.Length + 3;
				}
				int num4 = Convert.ToInt32(strln.Substring(0, num));
				int num5 = Convert.ToInt32(strln.Substring(num + 1, num2 - num - 1));
				int num6 = Convert.ToInt32(strln.Substring(num2 + 1, num3 - num2 - 4));
				if ((num5 < 8 && 1 == num5 % 2) || (num5 > 8 && num5 % 2 == 0))
				{
					if (num6 < 32)
					{
						return true;
					}
				}
				else if (num5 != 2)
				{
					if (num6 < 31)
					{
						return true;
					}
				}
				else if (num4 % 400 == 0 || (num4 % 4 == 0 && 0 < num4 % 100))
				{
					if (num6 < 30)
					{
						return true;
					}
				}
				else if (num6 < 29)
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsMobile(string strln)
		{
			return Regex.IsMatch(strln, "^1[0123456789]\\d{9}$", RegexOptions.IgnoreCase);
		}

		public static bool IsIDCard(string strln)
		{
			if (strln.Length == 18)
			{
				return Validator.IsIDCard18(strln);
			}
			return strln.Length == 15 && Validator.IsIDCard15(strln);
		}

		public static bool IsIDCard18(string strln)
		{
			long num = 0L;
			if (!long.TryParse(strln.Remove(17), out num) || (double)num < Math.Pow(10.0, 16.0) || !long.TryParse(strln.Replace('x', '0').Replace('X', '0'), out num))
			{
				return false;
			}
			string text = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
			if (text.IndexOf(strln.Remove(2)) == -1)
			{
				return false;
			}
			string s = strln.Substring(6, 8).Insert(6, "-").Insert(4, "-");
			DateTime dateTime = default(DateTime);
			if (!DateTime.TryParse(s, out dateTime))
			{
				return false;
			}
			string[] array = "1,0,x,9,8,7,6,5,4,3,2".Split(new char[]
			{
				','
			});
			string[] array2 = "7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2".Split(new char[]
			{
				','
			});
			char[] array3 = strln.Remove(17).ToCharArray();
			int num2 = 0;
			for (int i = 0; i < 17; i++)
			{
				num2 += int.Parse(array2[i]) * int.Parse(array3[i].ToString());
			}
			int num3 = -1;
			Math.DivRem(num2, 11, out num3);
			return !(array[num3] != strln.Substring(17, 1).ToLower());
		}

		public static bool IsIDCard15(string strln)
		{
			long num = 0L;
			if (!long.TryParse(strln, out num) || (double)num < Math.Pow(10.0, 14.0))
			{
				return false;
			}
			string text = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
			if (text.IndexOf(strln.Remove(2)) == -1)
			{
				return false;
			}
			string s = strln.Substring(6, 6).Insert(4, "-").Insert(2, "-");
			DateTime dateTime = default(DateTime);
			return DateTime.TryParse(s, out dateTime);
		}

		public static bool IsPhone(string strln)
		{
			return Regex.IsMatch(strln, "(^(\\d{2,4}[-_－—]?)?\\d{3,8}([-_－—]?\\d{3,8})?([-_－—]?\\d{1,7})?$)|(^0?1[35]\\d{9}$)");
		}

		public static bool IsEmail(string strln)
		{
			return Regex.IsMatch(strln, "^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$");
		}

		public static bool IsFax(string strln)
		{
			return Regex.IsMatch(strln, "^[+]{0,1}(\\d){1,3}[ ]?([-]?((\\d)|[ ]){1,12})+$");
		}

		public static bool IsOnllyChinese(string strln)
		{
			return Regex.IsMatch(strln, "^[\\u4e00-\\u9fa5]+$");
		}

		public static bool IsMoney(string strln)
		{
			return Regex.IsMatch(strln, "[0-9]+\\.?[0-9]*$");
		}
	}
}
