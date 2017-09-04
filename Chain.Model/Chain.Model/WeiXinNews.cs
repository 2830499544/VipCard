using System;

namespace Chain.Model
{
	[Serializable]
	public class WeiXinNews
	{
		private int _newsid;

		private int _newsruleid;

		private string _newstitle;

		private string _newsdesc;

		private string _newsurlfirst;

		private string _newsurlsecond;

		private string _newslinkcontent;

		private DateTime _newscreatetime;

		public int NewsID
		{
			get
			{
				return this._newsid;
			}
			set
			{
				this._newsid = value;
			}
		}

		public int NewsRuleID
		{
			get
			{
				return this._newsruleid;
			}
			set
			{
				this._newsruleid = value;
			}
		}

		public string NewsTitle
		{
			get
			{
				return this._newstitle;
			}
			set
			{
				this._newstitle = value;
			}
		}

		public string NewsDesc
		{
			get
			{
				return this._newsdesc;
			}
			set
			{
				this._newsdesc = value;
			}
		}

		public string NewsUrlFirst
		{
			get
			{
				return this._newsurlfirst;
			}
			set
			{
				this._newsurlfirst = value;
			}
		}

		public string NewsUrlSecond
		{
			get
			{
				return this._newsurlsecond;
			}
			set
			{
				this._newsurlsecond = value;
			}
		}

		public string NewsLinkContent
		{
			get
			{
				return this._newslinkcontent;
			}
			set
			{
				this._newslinkcontent = value;
			}
		}

		public DateTime NewsCreateTime
		{
			get
			{
				return this._newscreatetime;
			}
			set
			{
				this._newscreatetime = value;
			}
		}
	}
}
