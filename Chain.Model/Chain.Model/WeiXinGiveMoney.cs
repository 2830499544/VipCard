using System;

namespace Chain.Model
{
	[Serializable]
	public class WeiXinGiveMoney
	{
		private int id;

		private int moneyID;

		private int memID;

		private decimal giveMoney;

		private DateTime giveTime;

		private int isWin;

		public int Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		public int MoneyID
		{
			get
			{
				return this.moneyID;
			}
			set
			{
				this.moneyID = value;
			}
		}

		public int MemID
		{
			get
			{
				return this.memID;
			}
			set
			{
				this.memID = value;
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

		public DateTime GiveTime
		{
			get
			{
				return this.giveTime;
			}
			set
			{
				this.giveTime = value;
			}
		}

		public int IsWin
		{
			get
			{
				return this.isWin;
			}
			set
			{
				this.isWin = value;
			}
		}
	}
}
