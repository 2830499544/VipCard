using System;

namespace Chain.Common
{
	public class ValCodeModel
	{
		private DateTime _createdate;

		private string _code;

		private double _failure = 180.0;

		public string valCode
		{
			get
			{
				return this._code;
			}
			set
			{
				this._code = value;
			}
		}

		public DateTime CreateDate
		{
			get
			{
				return this._createdate;
			}
			set
			{
				this._createdate = value;
			}
		}

		public double Failure
		{
			get
			{
				return this._failure;
			}
			set
			{
				this._failure = value;
			}
		}

		public bool CodeFailure
		{
			get
			{
				return (DateTime.Now - this._createdate).TotalSeconds > this._failure;
			}
		}
	}
}
