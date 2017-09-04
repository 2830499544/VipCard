using System;

namespace Chain.Model
{
	[Serializable]
	public class MemTransferLog
	{
		private int _id;

		private string _transferAccount;

		private int _transferFromMemID;

		private int _transferToMemID;

		private decimal _transferMoney;

		private string _transferRemark;

		private DateTime _transferCreateTime;

		private int _userID;

		private decimal elseMoney;

		private decimal totalMoney;

		public decimal ElseMoney
		{
			get
			{
				return this.elseMoney;
			}
			set
			{
				this.elseMoney = value;
			}
		}

		public decimal TotalMoney
		{
			get
			{
				return this.totalMoney;
			}
			set
			{
				this.totalMoney = value;
			}
		}

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

		public string TransferAccount
		{
			get
			{
				return this._transferAccount;
			}
			set
			{
				this._transferAccount = value;
			}
		}

		public int TransferFromMemID
		{
			get
			{
				return this._transferFromMemID;
			}
			set
			{
				this._transferFromMemID = value;
			}
		}

		public int TransferToMemID
		{
			get
			{
				return this._transferToMemID;
			}
			set
			{
				this._transferToMemID = value;
			}
		}

		public decimal TransferMoney
		{
			get
			{
				return this._transferMoney;
			}
			set
			{
				this._transferMoney = value;
			}
		}

		public string TransferRemark
		{
			get
			{
				return this._transferRemark;
			}
			set
			{
				this._transferRemark = value;
			}
		}

		public DateTime TransferCreateTime
		{
			get
			{
				return this._transferCreateTime;
			}
			set
			{
				this._transferCreateTime = value;
			}
		}

		public int UserID
		{
			get
			{
				return this._userID;
			}
			set
			{
				this._userID = value;
			}
		}
	}
}
