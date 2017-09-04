using System;

namespace Chain.Model
{
	[Serializable]
	public class OnlineMessage
	{
		private int _proposalid;

		private string _proposalcontent;

		private int? _memid;

		private string _memmobile;

		private DateTime? _proposaltime;

		private int isReply;

		private string memcard;

		private string replyContent;

		private DateTime replyTime;

		private int messageType;

		private int isShow;

		private int replyUserID;

		public string MemCard
		{
			get
			{
				return this.memcard;
			}
			set
			{
				this.memcard = value;
			}
		}

		public int IsReply
		{
			get
			{
				return this.isReply;
			}
			set
			{
				this.isReply = value;
			}
		}

		public string ReplyContent
		{
			get
			{
				return this.replyContent;
			}
			set
			{
				this.replyContent = value;
			}
		}

		public DateTime ReplyTime
		{
			get
			{
				return this.replyTime;
			}
			set
			{
				this.replyTime = value;
			}
		}

		public int MessageType
		{
			get
			{
				return this.messageType;
			}
			set
			{
				this.messageType = value;
			}
		}

		public int IsShow
		{
			get
			{
				return this.isShow;
			}
			set
			{
				this.isShow = value;
			}
		}

		public int ReplyUserID
		{
			get
			{
				return this.replyUserID;
			}
			set
			{
				this.replyUserID = value;
			}
		}

		public int MessageID
		{
			get
			{
				return this._proposalid;
			}
			set
			{
				this._proposalid = value;
			}
		}

		public string MessageContent
		{
			get
			{
				return this._proposalcontent;
			}
			set
			{
				this._proposalcontent = value;
			}
		}

		public int? MemID
		{
			get
			{
				return this._memid;
			}
			set
			{
				this._memid = value;
			}
		}

		public DateTime? MessageTime
		{
			get
			{
				return this._proposaltime;
			}
			set
			{
				this._proposaltime = value;
			}
		}
	}
}
