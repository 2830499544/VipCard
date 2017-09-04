using System;
using System.Xml;

namespace Chain.Tasks
{
	public class TaskConfig
	{
		private static bool _initialized;

		private static XmlNode _scheduleTasks;

		public static XmlNode ScheduleTasks
		{
			get
			{
				return TaskConfig._scheduleTasks;
			}
			set
			{
				TaskConfig._scheduleTasks = value;
			}
		}

		public static void Init(string strPath)
		{
			if (!TaskConfig._initialized)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(strPath + "/Task.xml");
				TaskConfig._scheduleTasks = xmlDocument.SelectSingleNode("TaskConfig/ScheduleTasks");
				TaskConfig._initialized = true;
			}
		}
	}
}
