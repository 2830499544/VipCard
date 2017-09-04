using System;

namespace Chain.Model
{
	[Serializable]
	public class SysError
	{
		private int _id;

		private string _errorcontent;

		private DateTime _errortime;

		private string _ipaddress;

		private string _errortype;

		private int _userid;

		private int _shopid;

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

		public string ErrorContent
		{
			get
			{
				return this._errorcontent;
			}
			set
			{
				this._errorcontent = value;
			}
		}

		public DateTime ErrorTime
		{
			get
			{
				return this._errortime;
			}
			set
			{
				this._errortime = value;
			}
		}

		public string Ipaddress
		{
			get
			{
				return this._ipaddress;
			}
			set
			{
				this._ipaddress = value;
			}
		}

		public string ErrorType
		{
			get
			{
				return this._errortype;
			}
			set
			{
				this._errortype = value;
			}
		}

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

		public int ShopID
		{
			get
			{
				return this._shopid;
			}
			set
			{
				this._shopid = value;
			}
		}
	}
}
