using System;

namespace Chain.Model
{
	[Serializable]
	public class SysShopPointSettlement
	{
		private int _id;

		private DateTime _starttime;

		private DateTime _endtime;

		private bool _isfinish;

		private DateTime _finishtime;

		private string _remark;

		private int _outshopid;

		private int _userid;

		private int rechargePoint;

		private int deductionPoint;

		private int givePoint;

		private int returnPoint;

		private int fanliPoint;

		private int returnOrderPoint;

		private int drawPoint;

		public int RechargePoint
		{
			get
			{
				return this.rechargePoint;
			}
			set
			{
				this.rechargePoint = value;
			}
		}

		public int DeductionPoint
		{
			get
			{
				return this.deductionPoint;
			}
			set
			{
				this.deductionPoint = value;
			}
		}

		public int GivePoint
		{
			get
			{
				return this.givePoint;
			}
			set
			{
				this.givePoint = value;
			}
		}

		public int ReturnPoint
		{
			get
			{
				return this.returnPoint;
			}
			set
			{
				this.returnPoint = value;
			}
		}

		public int FanliPoint
		{
			get
			{
				return this.fanliPoint;
			}
			set
			{
				this.fanliPoint = value;
			}
		}

		public int ReturnOrderPoint
		{
			get
			{
				return this.returnOrderPoint;
			}
			set
			{
				this.returnOrderPoint = value;
			}
		}

		public int DrawPoint
		{
			get
			{
				return this.drawPoint;
			}
			set
			{
				this.drawPoint = value;
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
	}
}
