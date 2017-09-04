using System;

namespace Chain.Model
{
	[Serializable]
	public class SysRotatePrizeLog
	{
		private string prizeAccount;

		private int prizeLogID;

		private int rotateID;

		private string prizeLevel;

		private int memID;

		private DateTime createTime;

		private int prizeStatus;

		private DateTime giveTime;

		private int giveUserID;

		private string prizeCode;

		public string PrizeAccount
		{
			get
			{
				return this.prizeAccount;
			}
			set
			{
				this.prizeAccount = value;
			}
		}

		public int PrizeLogID
		{
			get
			{
				return this.prizeLogID;
			}
			set
			{
				this.prizeLogID = value;
			}
		}

		public int RotateID
		{
			get
			{
				return this.rotateID;
			}
			set
			{
				this.rotateID = value;
			}
		}

		public string PrizeLevel
		{
			get
			{
				return this.prizeLevel;
			}
			set
			{
				this.prizeLevel = value;
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

		public int PrizeStatus
		{
			get
			{
				return this.prizeStatus;
			}
			set
			{
				this.prizeStatus = value;
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

		public int GiveUserID
		{
			get
			{
				return this.giveUserID;
			}
			set
			{
				this.giveUserID = value;
			}
		}

		public string PrizeCode
		{
			get
			{
				return this.prizeCode;
			}
			set
			{
				this.prizeCode = value;
			}
		}
	}
}
