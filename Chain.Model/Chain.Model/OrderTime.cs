using System;

namespace Chain.Model
{
	[Serializable]
	public class OrderTime
	{
		private int _ordertimeid;

		private string _ordertimecode;

		private int _ordermemid;

		private string _ordertoken;

		private DateTime _ordermarchtime;

		private bool _orderstate;

		private DateTime _orderouttime;

		private int _orderstartuserid;

		private int _orderenduserid;

		private DateTime _ordercreatetime;

		private int _ordershopid;

		private int _orderprojectid;

		private int _orderrulesid;

		private string _orderremark;

		private decimal _orderpredicttime;

		public int OrderTimeID
		{
			get
			{
				return this._ordertimeid;
			}
			set
			{
				this._ordertimeid = value;
			}
		}

		public string OrderTimeCode
		{
			get
			{
				return this._ordertimecode;
			}
			set
			{
				this._ordertimecode = value;
			}
		}

		public int OrderMemID
		{
			get
			{
				return this._ordermemid;
			}
			set
			{
				this._ordermemid = value;
			}
		}

		public string OrderToken
		{
			get
			{
				return this._ordertoken;
			}
			set
			{
				this._ordertoken = value;
			}
		}

		public DateTime OrderMarchTime
		{
			get
			{
				return this._ordermarchtime;
			}
			set
			{
				this._ordermarchtime = value;
			}
		}

		public bool OrderState
		{
			get
			{
				return this._orderstate;
			}
			set
			{
				this._orderstate = value;
			}
		}

		public DateTime OrderOutTime
		{
			get
			{
				return this._orderouttime;
			}
			set
			{
				this._orderouttime = value;
			}
		}

		public int OrderStartUserID
		{
			get
			{
				return this._orderstartuserid;
			}
			set
			{
				this._orderstartuserid = value;
			}
		}

		public int OrderEndUserID
		{
			get
			{
				return this._orderenduserid;
			}
			set
			{
				this._orderenduserid = value;
			}
		}

		public DateTime OrderCreateTime
		{
			get
			{
				return this._ordercreatetime;
			}
			set
			{
				this._ordercreatetime = value;
			}
		}

		public int OrderShopID
		{
			get
			{
				return this._ordershopid;
			}
			set
			{
				this._ordershopid = value;
			}
		}

		public int OrderRulesID
		{
			get
			{
				return this._orderrulesid;
			}
			set
			{
				this._orderrulesid = value;
			}
		}

		public int OrderProjectID
		{
			get
			{
				return this._orderprojectid;
			}
			set
			{
				this._orderprojectid = value;
			}
		}

		public string OrderRemark
		{
			get
			{
				return this._orderremark;
			}
			set
			{
				this._orderremark = value;
			}
		}

		public decimal OrderPredictTime
		{
			get
			{
				return this._orderpredicttime;
			}
			set
			{
				this._orderpredicttime = value;
			}
		}
	}
}
