using System;

namespace Chain.Model
{
	[Serializable]
	public class RechargeRule
	{
		private int ruleID;

		private decimal rechargeMoney;

		private decimal giveMoney;

		private string ruleDesc;

		private int createUserID;

		private DateTime createTime;

		public int RuleID
		{
			get
			{
				return this.ruleID;
			}
			set
			{
				this.ruleID = value;
			}
		}

		public decimal RechargeMoney
		{
			get
			{
				return this.rechargeMoney;
			}
			set
			{
				this.rechargeMoney = value;
			}
		}

		public decimal GiveMoney
		{
			get
			{
				return this.giveMoney;
			}
			set
			{
				this.giveMoney = value;
			}
		}

		public string RuleDesc
		{
			get
			{
				return this.ruleDesc;
			}
			set
			{
				this.ruleDesc = value;
			}
		}

		public int CreateUserID
		{
			get
			{
				return this.createUserID;
			}
			set
			{
				this.createUserID = value;
			}
		}

		public DateTime CreateTime
		{
			get
			{
				return this.createTime;
			}
			set
			{
				this.createTime = value;
			}
		}
	}
}
