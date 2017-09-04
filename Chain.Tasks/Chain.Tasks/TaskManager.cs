using System;
using System.Collections.Generic;
using System.Xml;

namespace Chain.Tasks
{
	public class TaskManager
	{
		private static readonly TaskManager _taskManager = new TaskManager();

		private readonly List<TaskThread> _taskThreads = new List<TaskThread>();

		public static TaskManager Instance
		{
			get
			{
				return TaskManager._taskManager;
			}
		}

		public void Initialize(XmlNode node)
		{
			this._taskThreads.Clear();
			foreach (XmlNode xmlNode in node.ChildNodes)
			{
				if (xmlNode.Name.ToLower() == "thread")
				{
					TaskThread taskThread = new TaskThread(xmlNode);
					this._taskThreads.Add(taskThread);
					foreach (XmlNode xmlNode2 in xmlNode.ChildNodes)
					{
						if (xmlNode2.Name.ToLower() == "task")
						{
							XmlAttribute xmlAttribute = xmlNode2.Attributes["type"];
							Type type = Type.GetType(xmlAttribute.Value);
							if (type != null)
							{
								Task task = new Task(type, xmlNode2);
								taskThread.AddTask(task);
							}
						}
					}
				}
			}
		}

		public void Start()
		{
			foreach (TaskThread current in this._taskThreads)
			{
				current.InitTimer();
			}
		}

		public void Stop()
		{
			foreach (TaskThread current in this._taskThreads)
			{
				current.Dispose();
			}
		}
	}
}
