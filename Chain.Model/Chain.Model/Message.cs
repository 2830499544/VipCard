using System;

namespace Chain.Model
{
	[Serializable]
	public class Message
	{
		private int _messageid;

		private int _messagememid;

		private string _messagecontent;

		private DateTime _messagetime;

		private int _messageisreply;

		private string _messagereplycontent;

		private DateTime _messagereplytime;

		private int _messagereplyuserid;

		public int MessageID
		{
			get
			{
				return this._messageid;
			}
			set
			{
				this._messageid = value;
			}
		}

		public int MessageMemID
		{
			get
			{
				return this._messagememid;
			}
			set
			{
				this._messagememid = value;
			}
		}

		public string MessageContent
		{
			get
			{
				return this._messagecontent;
			}
			set
			{
				this._messagecontent = value;
			}
		}

		public DateTime MessageTime
		{
			get
			{
				return this._messagetime;
			}
			set
			{
				this._messagetime = value;
			}
		}

		public int MessageIsReply
		{
			get
			{
				return this._messageisreply;
			}
			set
			{
				this._messageisreply = value;
			}
		}

		public string MessageReplyContent
		{
			get
			{
				return this._messagereplycontent;
			}
			set
			{
				this._messagereplycontent = value;
			}
		}

		public DateTime MessageReplyTime
		{
			get
			{
				return this._messagereplytime;
			}
			set
			{
				this._messagereplytime = value;
			}
		}

		public int MessageReplyUserID
		{
			get
			{
				return this._messagereplyuserid;
			}
			set
			{
				this._messagereplyuserid = value;
			}
		}
	}
}
