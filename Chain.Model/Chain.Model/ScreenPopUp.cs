using System;

namespace Chain.Model
{
	[Serializable]
	public class ScreenPopUp
	{
		private int _callerid;

		private int _callermemid;

		private string _callermobile;

		private string _callerismem;

		private string _callerstate;

		private string _callerduration;

		private string _callerremark;

		private DateTime _callercreatetime;

		private int _calleruserid;

		private int _callershopid;

		public int CallerID
		{
			get
			{
				return this._callerid;
			}
			set
			{
				this._callerid = value;
			}
		}

		public int CallerMemID
		{
			get
			{
				return this._callermemid;
			}
			set
			{
				this._callermemid = value;
			}
		}

		public string CallerMobile
		{
			get
			{
				return this._callermobile;
			}
			set
			{
				this._callermobile = value;
			}
		}

		public string CallerIsMem
		{
			get
			{
				return this._callerismem;
			}
			set
			{
				this._callerismem = value;
			}
		}

		public string CallerState
		{
			get
			{
				return this._callerstate;
			}
			set
			{
				this._callerstate = value;
			}
		}

		public string CallerDuration
		{
			get
			{
				return this._callerduration;
			}
			set
			{
				this._callerduration = value;
			}
		}

		public string CallerRemark
		{
			get
			{
				return this._callerremark;
			}
			set
			{
				this._callerremark = value;
			}
		}

		public DateTime CallerCreateTime
		{
			get
			{
				return this._callercreatetime;
			}
			set
			{
				this._callercreatetime = value;
			}
		}

		public int CallerUserID
		{
			get
			{
				return this._calleruserid;
			}
			set
			{
				this._calleruserid = value;
			}
		}

		public int CallerShopID
		{
			get
			{
				return this._callershopid;
			}
			set
			{
				this._callershopid = value;
			}
		}
	}
}
