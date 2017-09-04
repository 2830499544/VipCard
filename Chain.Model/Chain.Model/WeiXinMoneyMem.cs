using System;

namespace Chain.Model
{
	[Serializable]
	public class WeiXinMoneyMem
	{
		private int id;

		private int moneyID;

		private int memID;

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
	}
}
