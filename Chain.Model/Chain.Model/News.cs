using System;

namespace Chain.Model
{
	[Serializable]
	public class News
	{
		private int newsID;

		private string newsName;

		private string newsPhoto;

		private string newsDesc;

		private DateTime newsCreateTime;

		private int createUserID;

		private string newsRemark;

		private int isRecommend;

		public int NewsID
		{
			get
			{
				return this.newsID;
			}
			set
			{
				this.newsID = value;
			}
		}

		public string NewsName
		{
			get
			{
				return this.newsName;
			}
			set
			{
				this.newsName = value;
			}
		}

		public string NewsPhoto
		{
			get
			{
				return this.newsPhoto;
			}
			set
			{
				this.newsPhoto = value;
			}
		}

		public string NewsDesc
		{
			get
			{
				return this.newsDesc;
			}
			set
			{
				this.newsDesc = value;
			}
		}

		public DateTime NewsCreateTime
		{
			get
			{
				return this.newsCreateTime;
			}
			set
			{
				this.newsCreateTime = value;
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

		public string NewsRemark
		{
			get
			{
				return this.newsRemark;
			}
			set
			{
				this.newsRemark = value;
			}
		}

		public int IsRecommend
		{
			get
			{
				return this.isRecommend;
			}
			set
			{
				this.isRecommend = value;
			}
		}
	}
}
