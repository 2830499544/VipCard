using System;

namespace Chain.Model
{
	[Serializable]
	public class SysUser
	{
		private int _userid;

		private string _useraccount;

		private string _username;

		private string _userpassword;

		private int _usershopid;

		private int _usergroupid;

		private bool _userlock = false;

		private string _userremark;

		private DateTime _usercreatetime;

		private string _usertelephont;

		private string _usernumber;

		public int UserID
		{
			get
			{
				return this._userid;
			}
			set
			{
				this._userid = value;
			}
		}

		public string UserAccount
		{
			get
			{
				return this._useraccount;
			}
			set
			{
				this._useraccount = value;
			}
		}

		public string UserName
		{
			get
			{
				return this._username;
			}
			set
			{
				this._username = value;
			}
		}

		public string UserPassword
		{
			get
			{
				return this._userpassword;
			}
			set
			{
				this._userpassword = value;
			}
		}

		public int UserShopID
		{
			get
			{
				return this._usershopid;
			}
			set
			{
				this._usershopid = value;
			}
		}

		public int UserGroupID
		{
			get
			{
				return this._usergroupid;
			}
			set
			{
				this._usergroupid = value;
			}
		}

		public bool UserLock
		{
			get
			{
				return this._userlock;
			}
			set
			{
				this._userlock = value;
			}
		}

		public string UserRemark
		{
			get
			{
				return this._userremark;
			}
			set
			{
				this._userremark = value;
			}
		}

		public DateTime UserCreateTime
		{
			get
			{
				return this._usercreatetime;
			}
			set
			{
				this._usercreatetime = value;
			}
		}

		public string UserTelephone
		{
			get
			{
				return this._usertelephont;
			}
			set
			{
				this._usertelephont = value;
			}
		}

		public string UserNumber
		{
			get
			{
				return this._usernumber;
			}
			set
			{
				this._usernumber = value;
			}
		}
	}
}
