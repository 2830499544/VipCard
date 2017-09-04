using System;

namespace Chain.Model
{
	[Serializable]
	public class PointDraw
	{
		private int drawID;

		private string drawAccount;

		private int drawShopID;

		private int drawPoint;

		private decimal drawAmount;

		private int drawStatus;

		private DateTime drawCreateTime;

		private int drawCreateUserID;

		private DateTime drawVerifyTime;

		private int drawVerifyUserID;

		private string drawRemark;

		public int DrawID
		{
			get
			{
				return this.drawID;
			}
			set
			{
				this.drawID = value;
			}
		}

		public string DrawAccount
		{
			get
			{
				return this.drawAccount;
			}
			set
			{
				this.drawAccount = value;
			}
		}

		public int DrawShopID
		{
			get
			{
				return this.drawShopID;
			}
			set
			{
				this.drawShopID = value;
			}
		}

		public int DrawPoint
		{
			get
			{
				return this.drawPoint;
			}
			set
			{
				this.drawPoint = value;
			}
		}

		public decimal DrawAmount
		{
			get
			{
				return this.drawAmount;
			}
			set
			{
				this.drawAmount = value;
			}
		}

		public int DrawStatus
		{
			get
			{
				return this.drawStatus;
			}
			set
			{
				this.drawStatus = value;
			}
		}

		public DateTime DrawCreateTime
		{
			get
			{
				return this.drawCreateTime;
			}
			set
			{
				this.drawCreateTime = value;
			}
		}

		public int DrawCreateUserID
		{
			get
			{
				return this.drawCreateUserID;
			}
			set
			{
				this.drawCreateUserID = value;
			}
		}

		public DateTime DrawVerifyTime
		{
			get
			{
				return this.drawVerifyTime;
			}
			set
			{
				this.drawVerifyTime = value;
			}
		}

		public int DrawVerifyUserID
		{
			get
			{
				return this.drawVerifyUserID;
			}
			set
			{
				this.drawVerifyUserID = value;
			}
		}

		public string DrawRemark
		{
			get
			{
				return this.drawRemark;
			}
			set
			{
				this.drawRemark = value;
			}
		}
	}
}
