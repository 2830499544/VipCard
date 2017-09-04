using System;

namespace Chain.Model
{
	[Serializable]
	public class SysGroup
	{
		private int _groupid;

		private string _groupname;

		private string _groupremark;

		private int _parentgroupid;

		private string _parentidstr;

		private bool _editable;

		private int _createuserid;

		private int _grouptype;

		public int GroupType
		{
			get
			{
				return this._grouptype;
			}
			set
			{
				this._grouptype = value;
			}
		}

		public int GroupID
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

		public string GroupName
		{
			get
			{
				return this._groupname;
			}
			set
			{
				this._groupname = value;
			}
		}

		public string GroupRemark
		{
			get
			{
				return this._groupremark;
			}
			set
			{
				this._groupremark = value;
			}
		}

		public int ParentGroupID
		{
			get
			{
				return this._parentgroupid;
			}
			set
			{
				this._parentgroupid = value;
			}
		}

		public string ParentIDStr
		{
			get
			{
				return this._parentidstr;
			}
			set
			{
				this._parentidstr = value;
			}
		}

		public bool IsPublic
		{
			get
			{
				return this._editable;
			}
			set
			{
				this._editable = value;
			}
		}

		public int CreateUserID
		{
			get
			{
				return this._createuserid;
			}
			set
			{
				this._createuserid = value;
			}
		}
	}
}
