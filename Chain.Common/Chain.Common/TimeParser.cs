using System;
using System.Globalization;

namespace Chain.Common
{
	public class TimeParser
	{
		public static int SecondToMinute(int Second)
		{
			decimal d = Second / 60m;
			return Convert.ToInt32(Math.Ceiling(d));
		}

		public static int GetMonthLastDate(int year, int month)
		{
			DateTime dateTime = new DateTime(year, month, new GregorianCalendar().GetDaysInMonth(year, month));
			return dateTime.Day;
		}

		public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
		{
			string result = null;
			try
			{
				TimeSpan timeSpan = DateTime2 - DateTime1;
				if (timeSpan.Days >= 1)
				{
					result = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
				}
				else if (timeSpan.Hours > 1)
				{
					result = timeSpan.Hours.ToString() + "小时前";
				}
				else
				{
					result = timeSpan.Minutes.ToString() + "分钟前";
				}
			}
			catch
			{
			}
			return result;
		}

		public static int[] GetLunisolarDate(DateTime date)
		{
			ChineseLunisolarCalendar chineseLunisolarCalendar = new ChineseLunisolarCalendar();
			int[] array = new int[3];
			int year = chineseLunisolarCalendar.GetYear(date);
			int month = chineseLunisolarCalendar.GetMonth(date);
			int dayOfMonth = chineseLunisolarCalendar.GetDayOfMonth(date);
			int leapMonth = chineseLunisolarCalendar.GetLeapMonth(year);
			if (leapMonth > 0)
			{
				if (month == leapMonth)
				{
					array[0] = month - 1;
					array[1] = dayOfMonth;
					array[2] = year;
				}
				else if (month > leapMonth)
				{
					array[0] = month - 1;
					array[1] = dayOfMonth;
					array[2] = year;
				}
				else
				{
					array[0] = month;
					array[1] = dayOfMonth;
					array[2] = year;
				}
			}
			else
			{
				array[0] = month;
				array[1] = dayOfMonth;
				array[2] = year;
			}
			return array;
		}
	}
}
