using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Chain.Common
{
	public static class INIFile
	{
		public static string path = AppDomain.CurrentDomain.BaseDirectory + "\\config.ini";

		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string defVal, byte[] retVal, int size, string filePath);

		public static void IniWriteValue(string Section, string Key, string Value)
		{
			INIFile.WritePrivateProfileString(Section, Key, Value, INIFile.path);
		}

		public static string IniReadValue(string Section, string Key)
		{
			StringBuilder stringBuilder = new StringBuilder(5000);
			INIFile.GetPrivateProfileString(Section, Key, "", stringBuilder, 5000, INIFile.path);
			return stringBuilder.ToString();
		}

		public static byte[] IniReadValues(string section, string key)
		{
			byte[] array = new byte[5000];
			INIFile.GetPrivateProfileString(section, key, "", array, 5000, INIFile.path);
			return array;
		}

		public static void ClearAllSection()
		{
			INIFile.IniWriteValue(null, null, null);
		}

		public static void ClearSection(string Section)
		{
			INIFile.IniWriteValue(Section, null, null);
		}
	}
}
