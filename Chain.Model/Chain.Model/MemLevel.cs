using System;

namespace Chain.Model
{
	[Serializable]
	public class MemLevel
	{
		private int _levelid;

		private string _levelname;

		private int _levelpoint;

		private decimal _leveldiscountpercent;

		private decimal _levelpointpercent;

		private int _startpoint;

		private int _endpoint;

		private bool _levelllock;

		private int _levelRechargePointRate;

		public int LevelRechargePointRate
		{
			get
			{
				return this._levelRechargePointRate;
			}
			set
			{
				this._levelRechargePointRate = value;
			}
		}

		public int LevelID
		{
			get
			{
				return this._levelid;
			}
			set
			{
				this._levelid = value;
			}
		}

		public string LevelName
		{
			get
			{
				return this._levelname;
			}
			set
			{
				this._levelname = value;
			}
		}

		public int LevelPoint
		{
			get
			{
				return this._levelpoint;
			}
			set
			{
				this._levelpoint = value;
			}
		}

		public decimal LevelDiscountPercent
		{
			get
			{
				return this._leveldiscountpercent;
			}
			set
			{
				this._leveldiscountpercent = value;
			}
		}

		public decimal LevelPointPercent
		{
			get
			{
				return this._levelpointpercent;
			}
			set
			{
				this._levelpointpercent = value;
			}
		}

		public bool LevellLock
		{
			get
			{
				return this._levelllock;
			}
			set
			{
				this._levelllock = value;
			}
		}
	}
}
