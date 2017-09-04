using System;

namespace Chain.Model
{
	[Serializable]
	public class MemDrawMoney
	{
		private int _drawmoneyid;

		private int _drawmoneymemid;

		private string _drawmoneyaccount;

		private decimal _drawmoney;

		private decimal _drawactualmoney;

		private string _drawmoneyremark;

		private int _drawmoneyshopid;

		private int _drawmoneyuserid;

		private DateTime _drawmoneycreatetime;

		public int DrawMoneyID
		{
			get
			{
				return this._drawmoneyid;
			}
			set
			{
				this._drawmoneyid = value;
			}
		}

		public int DrawMoneyMemID
		{
			get
			{
				return this._drawmoneymemid;
			}
			set
			{
				this._drawmoneymemid = value;
			}
		}

		public string DrawMoneyAccount
		{
			get
			{
				return this._drawmoneyaccount;
			}
			set
			{
				this._drawmoneyaccount = value;
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

		public decimal DrawActualMoney
		{
			get
			{
				return this._drawactualmoney;
			}
			set
			{
				this._drawactualmoney = value;
			}
		}

		public string DrawMoneyRemark
		{
			get
			{
				return this._drawmoneyremark;
			}
			set
			{
				this._drawmoneyremark = value;
			}
		}

		public int DrawMoneyShopID
		{
			get
			{
				return this._drawmoneyshopid;
			}
			set
			{
				this._drawmoneyshopid = value;
			}
		}

		public int DrawMoneyUserID
		{
			get
			{
				return this._drawmoneyuserid;
			}
			set
			{
				this._drawmoneyuserid = value;
			}
		}

		public DateTime DrawMoneyCreateTime
		{
			get
			{
				return this._drawmoneycreatetime;
			}
			set
			{
				this._drawmoneycreatetime = value;
			}
		}
	}
}
