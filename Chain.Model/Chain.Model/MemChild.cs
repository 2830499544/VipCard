using System;

namespace Chain.Model
{
	[Serializable]
	public class MemChild
	{
		private int childID;

		private string childCard;

		private string childName;

		private string childMobile;

		private int memID;

		private int childStatus;

		private DateTime createTime;

		public int ChildID
		{
			get
			{
				return this.childID;
			}
			set
			{
				this.childID = value;
			}
		}

		public string ChildCard
		{
			get
			{
				return this.childCard;
			}
			set
			{
				this.childCard = value;
			}
		}

		public string ChildName
		{
			get
			{
				return this.childName;
			}
			set
			{
				this.childName = value;
			}
		}

		public string ChildMobile
		{
			get
			{
				return this.childMobile;
			}
			set
			{
				this.childMobile = value;
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

		public int ChildStatus
		{
			get
			{
				return this.childStatus;
			}
			set
			{
				this.childStatus = value;
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
