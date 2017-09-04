using System;

namespace Chain.Model
{
	[Serializable]
	public class SysUserWork
	{
		private int _sysuserworkid;

		private int _userid;

		private DateTime _starttime;

		private DateTime _eedtime;

		private int _addnewuser;

		private decimal _cardmoney;

		private decimal _expensesummoneys;

		private decimal _expensebinkmoneys;

		private decimal _expensecouponmoneys;

		private decimal _srechargemoney;

		private decimal _frechargemoney;

		private decimal _rechargebank;

		private decimal _frechargegivemoney;

		private decimal _allmoneys;

		private decimal _sjmoneys;

		private int _handoveruserid;

		private decimal _arrearage;

		private bool _ispay;

		private string _remark;

		public int SysUserWorkID
		{
			get
			{
				return this._sysuserworkid;
			}
			set
			{
				this._sysuserworkid = value;
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

		public DateTime EedTime
		{
			get
			{
				return this._eedtime;
			}
			set
			{
				this._eedtime = value;
			}
		}

		public int AddNewUser
		{
			get
			{
				return this._addnewuser;
			}
			set
			{
				this._addnewuser = value;
			}
		}

		public decimal CardMoney
		{
			get
			{
				return this._cardmoney;
			}
			set
			{
				this._cardmoney = value;
			}
		}

		public decimal ExpenseSumMoneys
		{
			get
			{
				return this._expensesummoneys;
			}
			set
			{
				this._expensesummoneys = value;
			}
		}

		public decimal ExpenseBinkMoneys
		{
			get
			{
				return this._expensebinkmoneys;
			}
			set
			{
				this._expensebinkmoneys = value;
			}
		}

		public decimal ExpenseCouponMoneys
		{
			get
			{
				return this._expensecouponmoneys;
			}
			set
			{
				this._expensecouponmoneys = value;
			}
		}

		public decimal SRechargeMoney
		{
			get
			{
				return this._srechargemoney;
			}
			set
			{
				this._srechargemoney = value;
			}
		}

		public decimal FRechargeMoney
		{
			get
			{
				return this._frechargemoney;
			}
			set
			{
				this._frechargemoney = value;
			}
		}

		public decimal RechargeBank
		{
			get
			{
				return this._rechargebank;
			}
			set
			{
				this._rechargebank = value;
			}
		}

		public decimal FRechargeGiveMoney
		{
			get
			{
				return this._frechargegivemoney;
			}
			set
			{
				this._frechargegivemoney = value;
			}
		}

		public decimal AllMoneys
		{
			get
			{
				return this._allmoneys;
			}
			set
			{
				this._allmoneys = value;
			}
		}

		public decimal sjMoneys
		{
			get
			{
				return this._sjmoneys;
			}
			set
			{
				this._sjmoneys = value;
			}
		}

		public int HandoverUserID
		{
			get
			{
				return this._handoveruserid;
			}
			set
			{
				this._handoveruserid = value;
			}
		}

		public decimal Arrearage
		{
			get
			{
				return this._arrearage;
			}
			set
			{
				this._arrearage = value;
			}
		}

		public bool Ispay
		{
			get
			{
				return this._ispay;
			}
			set
			{
				this._ispay = value;
			}
		}

		public string remark
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
	}
}
