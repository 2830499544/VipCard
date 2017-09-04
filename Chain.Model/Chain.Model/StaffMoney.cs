using System;

namespace Chain.Model
{
	[Serializable]
	public class StaffMoney
	{
		private int _staffmoneyid;

		private int _staffid;

		private decimal _stafftotalmoney;

		private string _staffordercode;

		private int _staffmemid;

		private int _staffgoodsid;

		private int _staffshopid;

		private DateTime _staffcreatetime;

		private int _stafforderdetailid;

		private int _stafftype;

		public int StaffMoneyID
		{
			get
			{
				return this._staffmoneyid;
			}
			set
			{
				this._staffmoneyid = value;
			}
		}

		public int StaffID
		{
			get
			{
				return this._staffid;
			}
			set
			{
				this._staffid = value;
			}
		}

		public decimal StaffTotalMoney
		{
			get
			{
				return this._stafftotalmoney;
			}
			set
			{
				this._stafftotalmoney = value;
			}
		}

		public string StaffOrderCode
		{
			get
			{
				return this._staffordercode;
			}
			set
			{
				this._staffordercode = value;
			}
		}

		public int StaffMemID
		{
			get
			{
				return this._staffmemid;
			}
			set
			{
				this._staffmemid = value;
			}
		}

		public int StaffGoodsID
		{
			get
			{
				return this._staffgoodsid;
			}
			set
			{
				this._staffgoodsid = value;
			}
		}

		public int StaffShopID
		{
			get
			{
				return this._staffshopid;
			}
			set
			{
				this._staffshopid = value;
			}
		}

		public DateTime StaffCreateTime
		{
			get
			{
				return this._staffcreatetime;
			}
			set
			{
				this._staffcreatetime = value;
			}
		}

		public int StaffOrderDetailID
		{
			get
			{
				return this._stafforderdetailid;
			}
			set
			{
				this._stafforderdetailid = value;
			}
		}

		public int StaffType
		{
			get
			{
				return this._stafftype;
			}
			set
			{
				this._stafftype = value;
			}
		}
	}
}
