using System;

namespace Chain.Model
{
	[Serializable]
	public class SysArea
	{
		private int _id;

		private int _pid;

		private string _name;

		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		public int PID
		{
			get
			{
				return this._pid;
			}
			set
			{
				this._pid = value;
			}
		}

		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
	}
}
