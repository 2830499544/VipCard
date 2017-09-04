using System;

namespace Chain.Model
{
	[Serializable]
	public class ReturnPointLog
	{
		private int logID;

		private int memID;

		private string orderAccount;

		private int totalPoint;

		private string remark;

		private int alliancePoint;

		private int cardShopPoint;

		private int zbPoint;

		private int returnShopID;

		private DateTime createTime;

		public int LogID
		{
			get
			{
				return this.logID;
			}
			set
			{
				this.logID = value;
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

		public string OrderAccount
		{
			get
			{
				return this.orderAccount;
			}
			set
			{
				this.orderAccount = value;
			}
		}

		public int TotalPoint
		{
			get
			{
				return this.totalPoint;
			}
			set
			{
				this.totalPoint = value;
			}
		}

		public string Remark
		{
			get
			{
				return this.remark;
			}
			set
			{
				this.remark = value;
			}
		}

		public int AlliancePoint
		{
			get
			{
				return this.alliancePoint;
			}
			set
			{
				this.alliancePoint = value;
			}
		}

		public int CardShopPoint
		{
			get
			{
				return this.cardShopPoint;
			}
			set
			{
				this.cardShopPoint = value;
			}
		}

		public int ZbPoint
		{
			get
			{
				return this.zbPoint;
			}
			set
			{
				this.zbPoint = value;
			}
		}

		public int ReturnShopID
		{
			get
			{
				return this.returnShopID;
			}
			set
			{
				this.returnShopID = value;
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
