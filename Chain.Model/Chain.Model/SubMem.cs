using System;

namespace Chain.Model
{
	[Serializable]
	public class SubMem
	{
		private int _id;

		private string _subCardNumber;

		private int _memID;

		private string _memCard;

		private string _subName;

		private string _subMemMobile;

		private bool _isUsed;

		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		public string SubCardNumber
		{
			get
			{
				return this._subCardNumber;
			}
			set
			{
				this._subCardNumber = value;
			}
		}

		public int MemID
		{
			get
			{
				return this._memID;
			}
			set
			{
				this._memID = value;
			}
		}

		public string MemCard
		{
			get
			{
				return this._memCard;
			}
			set
			{
				this._memCard = value;
			}
		}

		public string SubName
		{
			get
			{
				return this._subName;
			}
			set
			{
				this._subName = value;
			}
		}

		public string SubMemMobile
		{
			get
			{
				return this._subMemMobile;
			}
			set
			{
				this._subMemMobile = value;
			}
		}

		public bool IsUsed
		{
			get
			{
				return this._isUsed;
			}
			set
			{
				this._isUsed = value;
			}
		}
	}
}
