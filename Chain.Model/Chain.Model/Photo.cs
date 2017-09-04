using System;

namespace Chain.Model
{
	[Serializable]
	public class Photo
	{
		private int albumID;

		private int photoID;

		private string photoName;

		private string photoPhoto;

		private string photoDesc;

		private DateTime photoCreateTime;

		private int createUserID;

		private string photoRemark;

		public int AlbumID
		{
			get
			{
				return this.albumID;
			}
			set
			{
				this.albumID = value;
			}
		}

		public int PhotoID
		{
			get
			{
				return this.photoID;
			}
			set
			{
				this.photoID = value;
			}
		}

		public string PhotoName
		{
			get
			{
				return this.photoName;
			}
			set
			{
				this.photoName = value;
			}
		}

		public string PhotoPhoto
		{
			get
			{
				return this.photoPhoto;
			}
			set
			{
				this.photoPhoto = value;
			}
		}

		public string PhotoDesc
		{
			get
			{
				return this.photoDesc;
			}
			set
			{
				this.photoDesc = value;
			}
		}

		public DateTime PhotoCreateTime
		{
			get
			{
				return this.photoCreateTime;
			}
			set
			{
				this.photoCreateTime = value;
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

		public string PhotoRemark
		{
			get
			{
				return this.photoRemark;
			}
			set
			{
				this.photoRemark = value;
			}
		}
	}
}
