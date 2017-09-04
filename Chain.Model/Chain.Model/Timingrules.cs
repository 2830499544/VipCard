using System;

namespace Chain.Model
{
	[Serializable]
	public class Timingrules
	{
		private int _rulesid;

		private string _rulesname;

		private int _rulesinterval;

		private decimal _rulesunitprice;

		private int _rulesexceedtime;

		private DateTime _rulesaddtime;

		private int _rulesshopid;

		private int _rulesuserid;

		private string _rulesremark;

		public int RulesID
		{
			get
			{
				return this._rulesid;
			}
			set
			{
				this._rulesid = value;
			}
		}

		public string RulesName
		{
			get
			{
				return this._rulesname;
			}
			set
			{
				this._rulesname = value;
			}
		}

		public int RulesInterval
		{
			get
			{
				return this._rulesinterval;
			}
			set
			{
				this._rulesinterval = value;
			}
		}

		public decimal RulesUnitPrice
		{
			get
			{
				return this._rulesunitprice;
			}
			set
			{
				this._rulesunitprice = value;
			}
		}

		public int RulesExceedTime
		{
			get
			{
				return this._rulesexceedtime;
			}
			set
			{
				this._rulesexceedtime = value;
			}
		}

		public DateTime RulesAddTime
		{
			get
			{
				return this._rulesaddtime;
			}
			set
			{
				this._rulesaddtime = value;
			}
		}

		public int RulesShopID
		{
			get
			{
				return this._rulesshopid;
			}
			set
			{
				this._rulesshopid = value;
			}
		}

		public int RulesUserID
		{
			get
			{
				return this._rulesuserid;
			}
			set
			{
				this._rulesuserid = value;
			}
		}

		public string RulesRemark
		{
			get
			{
				return this._rulesremark;
			}
			set
			{
				this._rulesremark = value;
			}
		}
	}
}
