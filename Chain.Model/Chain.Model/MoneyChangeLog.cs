using System;

namespace Chain.Model
{
	[Serializable]
	public class MoneyChangeLog
	{
		private int _moneychangeid;

		private int _moneychangememid;

		private int _moneychangeuserid;

		private int _moneychangetype;

		private string _moneychangeaccount;

		private decimal _moneychangemoney;

		private decimal _moneychangecash;

		private decimal _moneychangebalance;

		private decimal _moneychangeunionpay;

		private decimal _memmoney;

		private decimal _moneychangegivemoney;

		private DateTime _moneychangecreatetime;

		public int MoneyChangeID
		{
			get
			{
				return this._moneychangeid;
			}
			set
			{
				this._moneychangeid = value;
			}
		}

		public int MoneyChangeMemID
		{
			get
			{
				return this._moneychangememid;
			}
			set
			{
				this._moneychangememid = value;
			}
		}

		public int MoneyChangeUserID
		{
			get
			{
				return this._moneychangeuserid;
			}
			set
			{
				this._moneychangeuserid = value;
			}
		}

		public int MoneyChangeType
		{
			get
			{
				return this._moneychangetype;
			}
			set
			{
				this._moneychangetype = value;
			}
		}

		public string MoneyChangeAccount
		{
			get
			{
				return this._moneychangeaccount;
			}
			set
			{
				this._moneychangeaccount = value;
			}
		}

		public decimal MoneyChangeMoney
		{
			get
			{
				return this._moneychangemoney;
			}
			set
			{
				this._moneychangemoney = value;
			}
		}

		public decimal MoneyChangeCash
		{
			get
			{
				return this._moneychangecash;
			}
			set
			{
				this._moneychangecash = value;
			}
		}

		public decimal MoneyChangeBalance
		{
			get
			{
				return this._moneychangebalance;
			}
			set
			{
				this._moneychangebalance = value;
			}
		}

		public decimal MoneyChangeUnionPay
		{
			get
			{
				return this._moneychangeunionpay;
			}
			set
			{
				this._moneychangeunionpay = value;
			}
		}

		public decimal MemMoney
		{
			get
			{
				return this._memmoney;
			}
			set
			{
				this._memmoney = value;
			}
		}

		public decimal MoneyChangeGiveMoney
		{
			get
			{
				return this._moneychangegivemoney;
			}
			set
			{
				this._moneychangegivemoney = value;
			}
		}

		public DateTime MoneyChangeCreateTime
		{
			get
			{
				return this._moneychangecreatetime;
			}
			set
			{
				this._moneychangecreatetime = value;
			}
		}
	}
}
