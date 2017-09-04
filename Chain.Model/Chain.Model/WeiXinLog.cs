using System;

namespace Chain.Model
{
	[Serializable]
	public class WeiXinLog
	{
		private int _weixinlogid;

		private string _memweixincard;

		private string _recordcontent;

		private int _recordcontenttype;

		private string _statuscode;

		private string _randomcode;

		private int _errortimes;

		private DateTime _weixinlogcreatetime;

		public int WeiXinLogID
		{
			get
			{
				return this._weixinlogid;
			}
			set
			{
				this._weixinlogid = value;
			}
		}

		public string MemWeiXinCard
		{
			get
			{
				return this._memweixincard;
			}
			set
			{
				this._memweixincard = value;
			}
		}

		public string RecordContent
		{
			get
			{
				return this._recordcontent;
			}
			set
			{
				this._recordcontent = value;
			}
		}

		public int RecordContentType
		{
			get
			{
				return this._recordcontenttype;
			}
			set
			{
				this._recordcontenttype = value;
			}
		}

		public string StatusCode
		{
			get
			{
				return this._statuscode;
			}
			set
			{
				this._statuscode = value;
			}
		}

		public string RandomCode
		{
			get
			{
				return this._randomcode;
			}
			set
			{
				this._randomcode = value;
			}
		}

		public int ErrorTimes
		{
			get
			{
				return this._errortimes;
			}
			set
			{
				this._errortimes = value;
			}
		}

		public DateTime WeiXinLogCreateTime
		{
			get
			{
				return this._weixinlogcreatetime;
			}
			set
			{
				this._weixinlogcreatetime = value;
			}
		}
	}
}
