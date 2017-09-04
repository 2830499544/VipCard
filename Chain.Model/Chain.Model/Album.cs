using System;

namespace Chain.Model
{
	[Serializable]
	public class Album
	{
		private int albumID;

		private string albumName;

		private string albumPhoto;

		private string albumDesc;

		private DateTime albumCreateTime;

		private int createUserID;

		private string albumRemark;

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

		public string AlbumName
		{
			get
			{
				return this.albumName;
			}
			set
			{
				this.albumName = value;
			}
		}

		public string AlbumPhoto
		{
			get
			{
				return this.albumPhoto;
			}
			set
			{
				this.albumPhoto = value;
			}
		}

		public string AlbumDesc
		{
			get
			{
				return this.albumDesc;
			}
			set
			{
				this.albumDesc = value;
			}
		}

		public DateTime AlbumCreateTime
		{
			get
			{
				return this.albumCreateTime;
			}
			set
			{
				this.albumCreateTime = value;
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

		public string AlbumRemark
		{
			get
			{
				return this.albumRemark;
			}
			set
			{
				this.albumRemark = value;
			}
		}
	}
}
