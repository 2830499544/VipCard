using System;

namespace Chain.Model
{
	[Serializable]
	public class SysGroupAuthority
	{
		private int _gaid;

		private int? _groupid;

		private int? _moduleid;

		private int? _actionid;

		private bool _actionvalue;

		public int GAID
		{
			get
			{
				return this._gaid;
			}
			set
			{
				this._gaid = value;
			}
		}

		public int? GroupID
		{
			get
			{
				return this._groupid;
			}
			set
			{
				this._groupid = value;
			}
		}

		public int? ModuleID
		{
			get
			{
				return this._moduleid;
			}
			set
			{
				this._moduleid = value;
			}
		}

		public int? ActionID
		{
			get
			{
				return this._actionid;
			}
			set
			{
				this._actionid = value;
			}
		}

		public bool ActionValue
		{
			get
			{
				return this._actionvalue;
			}
			set
			{
				this._actionvalue = value;
			}
		}
	}
}
