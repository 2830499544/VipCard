using System;

namespace Chain.Model
{
	[Serializable]
	public class SysModuleAction
	{
		private int _actionid;

		private string _actioncaption;

		private string _actioncontrol;

		private string _actionremark;

		private int? _actionmoduleid;

		public int ActionID
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

		public string ActionCaption
		{
			get
			{
				return this._actioncaption;
			}
			set
			{
				this._actioncaption = value;
			}
		}

		public string ActionControl
		{
			get
			{
				return this._actioncontrol;
			}
			set
			{
				this._actioncontrol = value;
			}
		}

		public string ActionRemark
		{
			get
			{
				return this._actionremark;
			}
			set
			{
				this._actionremark = value;
			}
		}

		public int? ActionModuleID
		{
			get
			{
				return this._actionmoduleid;
			}
			set
			{
				this._actionmoduleid = value;
			}
		}
	}
}
