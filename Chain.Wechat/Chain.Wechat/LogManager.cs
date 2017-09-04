using System;
using System.Collections.Generic;

namespace Chain.Wechat
{
	public class LogManager
	{
		private static Dictionary<string, ILog> Message = new Dictionary<string, ILog>();

		public static void Add(string key, ILog obj)
		{
			LogManager.Remove(key);
			LogManager.Message.Add(key, obj);
		}

		public static void Remove(string key)
		{
			if (LogManager.Message.ContainsKey(key))
			{
				LogManager.Message.Remove(key);
			}
		}

		public static bool ContainsKey(string key)
		{
			return LogManager.Message.ContainsKey(key);
		}

		public static ILog Get(string key)
		{
			ILog result;
			if (LogManager.Message.ContainsKey(key))
			{
				result = LogManager.Message[key];
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
