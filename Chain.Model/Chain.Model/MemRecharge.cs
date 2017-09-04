using System;

namespace Chain.Model
{
	[Serializable]
	public class MemRecharge
	{
		private int _rechargeid;

		private int _rechargememid;

		private string _rechargeaccount;

		private int _rechargetype;

		private decimal _rechargemoney;

		private decimal _rechargegive;

		private string _rechargeremark;

		private int _rechargeshopid;

		private DateTime _rechargecreatetime;

		private int _rechargeuserid;

		private decimal _rechargecardbalance;

		private bool _rechargeisapprove;

		private int _rechargepoint;

		public int RechargeID
		{
			get
			{
				return this._rechargeid;
			}
			set
			{
				this._rechargeid = value;
			}
		}

		public int RechargeMemID
		{
			get
			{
				return this._rechargememid;
			}
			set
			{
				this._rechargememid = value;
			}
		}

		public string RechargeAccount
		{
			get
			{
				return this._rechargeaccount;
			}
			set
			{
				this._rechargeaccount = value;
			}
		}

		public int RechargeType
		{
			get
			{
				return this._rechargetype;
			}
			set
			{
				this._rechargetype = value;
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

		public decimal RechargeGive
		{
			get
			{
				return this._rechargegive;
			}
			set
			{
				this._rechargegive = value;
			}
		}

		public string RechargeRemark
		{
			get
			{
				return this._rechargeremark;
			}
			set
			{
				this._rechargeremark = value;
			}
		}

		public int RechargeShopID
		{
			get
			{
				return this._rechargeshopid;
			}
			set
			{
				this._rechargeshopid = value;
			}
		}

		public DateTime RechargeCreateTime
		{
			get
			{
				return this._rechargecreatetime;
			}
			set
			{
				this._rechargecreatetime = value;
			}
		}

		public int RechargeUserID
		{
			get
			{
				return this._rechargeuserid;
			}
			set
			{
				this._rechargeuserid = value;
			}
		}

		public decimal RechargeCardBalance
		{
			get
			{
				return this._rechargecardbalance;
			}
			set
			{
				this._rechargecardbalance = value;
			}
		}

		public bool RechargeIsApprove
		{
			get
			{
				return this._rechargeisapprove;
			}
			set
			{
				this._rechargeisapprove = value;
			}
		}

		public int RechargePoint
		{
			get
			{
				return this._rechargepoint;
			}
			set
			{
				this._rechargepoint = value;
			}
		}
	}
}
