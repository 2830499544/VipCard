using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml;

namespace Chain.Tasks
{
	public class TaskThread : IDisposable
	{
		private Timer _timer;

		private bool _isRunning;

		private bool _disposed;

		private DateTime _started;

		public readonly Dictionary<string, Task> _tasks;

		public readonly int _seconds;

		public int Interval
		{
			get
			{
				return this._seconds * 1000;
			}
		}

		public TaskThread()
		{
			this._tasks = new Dictionary<string, Task>();
			this._seconds = 600;
		}

		public TaskThread(XmlNode node)
		{
			this._tasks = new Dictionary<string, Task>();
			this._seconds = 600;
			this._isRunning = false;
			if (node.Attributes["seconds"] != null && !int.TryParse(node.Attributes["seconds"].Value, out this._seconds))
			{
				this._seconds = 600;
			}
		}

		public void AddTask(Task task)
		{
			if (!this._tasks.ContainsKey(task.Name))
			{
				this._tasks.Add(task.Name, task);
			}
		}

		private void Run()
		{
			this._started = DateTime.Now;
			this._isRunning = true;
			foreach (Task current in this._tasks.Values)
			{
				current.Execute();
			}
			this._isRunning = false;
		}

		private void TimerHandler(object state)
		{
			this._timer.Change(-1, -1);
			this.Run();
			this._timer.Change(this.Interval, this.Interval);
		}

		public void InitTimer()
		{
			if (this._timer == null)
			{
				this._timer = new Timer(new TimerCallback(this.TimerHandler), null, this.Interval, this.Interval);
			}
		}

		public void Dispose()
		{
			if (this._timer != null && !this._disposed)
			{
				lock (this)
				{
					this._timer.Dispose();
					this._timer = null;
					this._disposed = true;
				}
			}
		}
	}
}
