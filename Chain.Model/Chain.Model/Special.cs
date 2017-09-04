using System;

namespace Chain.Model
{
	[Serializable]
	public class Special
	{
		private int _specialid;

		private string _specialname;

		private decimal _specialrecharge;

		private decimal _specialgive;

		private DateTime _specialtime;

		private string _sremark;

		private int _specialuser;

		private int _type;

		private DateTime _startTime;

		private DateTime _endTime;

		private string _week;

		private string _month;

		public int SpecialID
		{
			get
			{
				return this._specialid;
			}
			set
			{
				this._specialid = value;
			}
		}

		public string SpecialName
		{
			get
			{
				return this._specialname;
			}
			set
			{
				this._specialname = value;
			}
		}

		public decimal SpecialRecharge
		{
			get
			{
				return this._specialrecharge;
			}
			set
			{
				this._specialrecharge = value;
			}
		}

		public decimal SpecialGive
		{
			get
			{
				return this._specialgive;
			}
			set
			{
				this._specialgive = value;
			}
		}

		public DateTime SpecialTime
		{
			get
			{
				return this._specialtime;
			}
			set
			{
				this._specialtime = value;
			}
		}

		public string Sremark
		{
			get
			{
				return this._sremark;
			}
			set
			{
				this._sremark = value;
			}
		}

		public int SpecialUser
		{
			get
			{
				return this._specialuser;
			}
			set
			{
				this._specialuser = value;
			}
		}

		public int Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		public DateTime StartTime
		{
			get
			{
				return this._startTime;
			}
			set
			{
				this._startTime = value;
			}
		}

		public DateTime EndTime
		{
			get
			{
				return this._endTime;
			}
			set
			{
				this._endTime = value;
			}
		}

		public string Week
		{
			get
			{
				return this._week;
			}
			set
			{
				this._week = value;
			}
		}

		public string Month
		{
			get
			{
				return this._month;
			}
			set
			{
				this._month = value;
			}
		}
	}
}
