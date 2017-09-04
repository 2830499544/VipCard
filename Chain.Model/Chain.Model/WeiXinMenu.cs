using System;

namespace Chain.Model
{
	[Serializable]
	public class WeiXinMenu
	{
		private int _menuid;

		private string _menuname;

		private int _menutype;

		private string _menukey;

		private string _menuurl;

		private int _parentmenuid;

		public int MenuID
		{
			get
			{
				return this._menuid;
			}
			set
			{
				this._menuid = value;
			}
		}

		public string MenuName
		{
			get
			{
				return this._menuname;
			}
			set
			{
				this._menuname = value;
			}
		}

		public int MenuType
		{
			get
			{
				return this._menutype;
			}
			set
			{
				this._menutype = value;
			}
		}

		public string MenuKey
		{
			get
			{
				return this._menukey;
			}
			set
			{
				this._menukey = value;
			}
		}

		public string MenuUrl
		{
			get
			{
				return this._menuurl;
			}
			set
			{
				this._menuurl = value;
			}
		}

		public int parentMenuID
		{
			get
			{
				return this._parentmenuid;
			}
			set
			{
				this._parentmenuid = value;
			}
		}
	}
}
