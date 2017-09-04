using System;

namespace Chain.Model
{
	[Serializable]
	public class MemberExplanation
	{
		private int _memberexplanationid;

		private string _memberexplanationdesc;

		private DateTime _memberexplanationtime;

		public int MemberExplanationID
		{
			get
			{
				return this._memberexplanationid;
			}
			set
			{
				this._memberexplanationid = value;
			}
		}

		public string MemberExplanationDesc
		{
			get
			{
				return this._memberexplanationdesc;
			}
			set
			{
				this._memberexplanationdesc = value;
			}
		}

		public DateTime MemberExplanationTime
		{
			get
			{
				return this._memberexplanationtime;
			}
			set
			{
				this._memberexplanationtime = value;
			}
		}
	}
}
