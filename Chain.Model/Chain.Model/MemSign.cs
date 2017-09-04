using System;

namespace Chain.Model
{
	[Serializable]
	public class MemSign
	{
		private int id;

		private int memID;

		private DateTime signTime;

		private int givePoint;

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

		public DateTime SignTime
		{
			get
			{
				return this.signTime;
			}
			set
			{
				this.signTime = value;
			}
		}

		public int GivePoint
		{
			get
			{
				return this.givePoint;
			}
			set
			{
				this.givePoint = value;
			}
		}
	}
}
