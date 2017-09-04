using System;

namespace Chain.Model
{
	[Serializable]
	public class WeiXinRule
	{
		private int _ruleid;

		private string _rulenumber;

		private string _rulenewstype;

		private string _ruledesc;

		private string _rulecontent;

		private int _ruleuserid;

		private DateTime _rulecreatetime;

		public int RuleID
		{
			get
			{
				return this._ruleid;
			}
			set
			{
				this._ruleid = value;
			}
		}

		public string RuleNUmber
		{
			get
			{
				return this._rulenumber;
			}
			set
			{
				this._rulenumber = value;
			}
		}

		public string RuleNewsType
		{
			get
			{
				return this._rulenewstype;
			}
			set
			{
				this._rulenewstype = value;
			}
		}

		public string RuleDesc
		{
			get
			{
				return this._ruledesc;
			}
			set
			{
				this._ruledesc = value;
			}
		}

		public string RuleContent
		{
			get
			{
				return this._rulecontent;
			}
			set
			{
				this._rulecontent = value;
			}
		}

		public int RuleUserID
		{
			get
			{
				return this._ruleuserid;
			}
			set
			{
				this._ruleuserid = value;
			}
		}

		public DateTime RuleCreateTime
		{
			get
			{
				return this._rulecreatetime;
			}
			set
			{
				this._rulecreatetime = value;
			}
		}
	}
}
