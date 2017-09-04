using System;

namespace Chain.Model
{
	[Serializable]
	public class Proposal
	{
		private int _proposalid;

		private string _proposalcontent;

		private int? _memid;

		private string _memmobile;

		private DateTime? _proposaltime;

		public int ProposalID
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

		public string ProposalContent
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

		public string MemMobile
		{
			get
			{
				return this._memmobile;
			}
			set
			{
				this._memmobile = value;
			}
		}

		public DateTime? ProposalTime
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
