using System;

namespace Chain.Model
{
	[Serializable]
	public class SysShopSettlement
	{
		private int _id;

		private DateTime _starttime;

		private DateTime _endtime;

		private decimal _rechargemoney;

		private decimal _drawmoney;

		private decimal _paycard;

		private bool _isfinish;

		private DateTime _finishtime;

		private string _remark;

		private int _outshopid;

		private int _userid;

		private decimal _allexpensemoney;

		private decimal _proportionmoney;

		private decimal _proportion;

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

		public DateTime StartTime
		{
			get
			{
				return this._starttime;
			}
			set
			{
				this._starttime = value;
			}
		}

		public DateTime EndTime
		{
			get
			{
				return this._endtime;
			}
			set
			{
				this._endtime = value;
			}
		}

		public decimal RechargeMoney
		{
			get
			{
				return this._rechargemoney;
			}
			set
			{
				this._rechargemoney = value;
			}
		}

		public decimal DrawMoney
		{
			get
			{
				return this._drawmoney;
			}
			set
			{
				this._drawmoney = value;
			}
		}

		public decimal PayCard
		{
			get
			{
				return this._paycard;
			}
			set
			{
				this._paycard = value;
			}
		}

		public bool IsFinish
		{
			get
			{
				return this._isfinish;
			}
			set
			{
				this._isfinish = value;
			}
		}

		public DateTime FinishTime
		{
			get
			{
				return this._finishtime;
			}
			set
			{
				this._finishtime = value;
			}
		}

		public string Remark
		{
			get
			{
				return this._remark;
			}
			set
			{
				this._remark = value;
			}
		}

		public int OutShopID
		{
			get
			{
				return this._outshopid;
			}
			set
			{
				this._outshopid = value;
			}
		}

		public int UserID
		{
			get
			{
				return this._userid;
			}
			set
			{
				this._userid = value;
			}
		}

		public decimal AllExpenseMoney
		{
			get
			{
				return this._allexpensemoney;
			}
			set
			{
				this._allexpensemoney = value;
			}
		}

		public decimal ProportionMoney
		{
			get
			{
				return this._proportionmoney;
			}
			set
			{
				this._proportionmoney = value;
			}
		}

		public decimal Proportion
		{
			get
			{
				return this._proportion;
			}
			set
			{
				this._proportion = value;
			}
		}
	}
}
