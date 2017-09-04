using System;

namespace Chain.Model
{
	[Serializable]
	public class SysRotateCount
	{
		private int id;

		private int rotateCount;

		private decimal costAmount;

		private DateTime startTime;

		private DateTime endTime;

		private int rotateID;

		public int ID
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

		public int RotateCount
		{
			get
			{
				return this.rotateCount;
			}
			set
			{
				this.rotateCount = value;
			}
		}

		public decimal CostAmount
		{
			get
			{
				return this.costAmount;
			}
			set
			{
				this.costAmount = value;
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
	}
}
