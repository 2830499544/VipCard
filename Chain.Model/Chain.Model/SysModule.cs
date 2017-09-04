using System;

namespace Chain.Model
{
	[Serializable]
	public class SysModule
	{
		private int _moduleid;

		private string _modulecaption;

		private string _modulelink;

		private int _moduleparentid;

		private int _moduleorder = 0;

		private bool _modulevisible = true;

		private string _moduleicopath;

		private string _moduleremark;

		public int ModuleID
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

		public string ModuleCaption
		{
			get
			{
				return this._modulecaption;
			}
			set
			{
				this._modulecaption = value;
			}
		}

		public string ModuleLink
		{
			get
			{
				return this._modulelink;
			}
			set
			{
				this._modulelink = value;
			}
		}

		public int ModuleParentID
		{
			get
			{
				return this._moduleparentid;
			}
			set
			{
				this._moduleparentid = value;
			}
		}

		public int ModuleOrder
		{
			get
			{
				return this._moduleorder;
			}
			set
			{
				this._moduleorder = value;
			}
		}

		public bool ModuleVisible
		{
			get
			{
				return this._modulevisible;
			}
			set
			{
				this._modulevisible = value;
			}
		}

		public string ModuleIcoPath
		{
			get
			{
				return this._moduleicopath;
			}
			set
			{
				this._moduleicopath = value;
			}
		}

		public string ModuleRemark
		{
			get
			{
				return this._moduleremark;
			}
			set
			{
				this._moduleremark = value;
			}
		}
	}
}
