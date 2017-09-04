using System;

namespace Chain.Model
{
	[Serializable]
	public class WeiXinMoney
	{
		private int createUserID;

		private DateTime createTime;

		private int moneyID;

		private string moneyTitle;

		private string imageUrl;

		private string moneyDesc;

		private string moneyWish;

		private DateTime startTime;

		private DateTime endTime;

		private decimal totalMoney;

		private int moneyType;

		private decimal startMoney;

		private decimal endMoney;

		private decimal fixedMoney;

		private int maxCount;

		private int startType;

		private decimal moneyRate;

		private decimal giveMoney;

		private string moneyRegion;

		private string querySql;

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

		public string MoneyTitle
		{
			get
			{
				return this.moneyTitle;
			}
			set
			{
				this.moneyTitle = value;
			}
		}

		public string ImageUrl
		{
			get
			{
				return this.imageUrl;
			}
			set
			{
				this.imageUrl = value;
			}
		}

		public string MoneyDesc
		{
			get
			{
				return this.moneyDesc;
			}
			set
			{
				this.moneyDesc = value;
			}
		}

		public string MoneyWish
		{
			get
			{
				return this.moneyWish;
			}
			set
			{
				this.moneyWish = value;
			}
		}

		public DateTime StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				this.startTime = value;
			}
		}

		public DateTime EndTime
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
			}
		}

		public decimal TotalMoney
		{
			get
			{
				return this.totalMoney;
			}
			set
			{
				this.totalMoney = value;
			}
		}

		public int MoneyType
		{
			get
			{
				return this.moneyType;
			}
			set
			{
				this.moneyType = value;
			}
		}

		public decimal StartMoney
		{
			get
			{
				return this.startMoney;
			}
			set
			{
				this.startMoney = value;
			}
		}

		public decimal EndMoney
		{
			get
			{
				return this.endMoney;
			}
			set
			{
				this.endMoney = value;
			}
		}

		public decimal FixedMoney
		{
			get
			{
				return this.fixedMoney;
			}
			set
			{
				this.fixedMoney = value;
			}
		}

		public int MaxCount
		{
			get
			{
				return this.maxCount;
			}
			set
			{
				this.maxCount = value;
			}
		}

		public int StartType
		{
			get
			{
				return this.startType;
			}
			set
			{
				this.startType = value;
			}
		}

		public decimal MoneyRate
		{
			get
			{
				return this.moneyRate;
			}
			set
			{
				this.moneyRate = value;
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

		public string MoneyRegion
		{
			get
			{
				return this.moneyRegion;
			}
			set
			{
				this.moneyRegion = value;
			}
		}

		public string QuerySql
		{
			get
			{
				return this.querySql;
			}
			set
			{
				this.querySql = value;
			}
		}
	}
}
