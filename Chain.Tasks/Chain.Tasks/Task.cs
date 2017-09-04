using System;
using System.Xml;

namespace Chain.Tasks
{
	public class Task
	{
		private ITask _task;

		private bool _enabled;

		private readonly bool _stopOnError;

		private readonly string _name;

		private readonly Type _taskType;

		private readonly XmlNode _configNode;

		private DateTime _lastStarted;

		private DateTime _lastSuccess;

		private DateTime _lastEnd;

		private bool _isRunning;

		public string Name
		{
			get
			{
				return this._name;
			}
		}

		public Task(Type taskType, XmlNode configNode)
		{
			this._enabled = true;
			this._configNode = configNode;
			this._taskType = taskType;
			if (configNode.Attributes["enabled"] != null && !bool.TryParse(configNode.Attributes["enabled"].Value, out this._enabled))
			{
				this._enabled = true;
			}
			if (configNode.Attributes["stopOnError"] != null && !bool.TryParse(configNode.Attributes["stopOnError"].Value, out this._stopOnError))
			{
				this._stopOnError = true;
			}
			if (configNode.Attributes["name"] != null)
			{
				this._name = configNode.Attributes["name"].Value;
			}
		}

		private ITask createTask()
		{
			if (this._enabled && this._task == null)
			{
				if (this._taskType != null)
				{
					this._task = (Activator.CreateInstance(this._taskType) as ITask);
				}
				this._enabled = (this._task != null);
			}
			return this._task;
		}

		public void Execute()
		{
			this._isRunning = true;
			try
			{
				ITask task = this.createTask();
				if (task != null)
				{
					this._lastStarted = DateTime.Now;
					task.Execute(this._configNode);
					this._lastEnd = (this._lastSuccess = DateTime.Now);
				}
			}
			catch (Exception)
			{
				this._enabled = !this._stopOnError;
				this._lastEnd = DateTime.Now;
			}
			this._isRunning = false;
		}
	}
}
