using System;

namespace Chain.Model
{
	[Serializable]
	public class PointLog
	{
		private int _pointid;

		private int _pointmemid;

		private int _pointnumber;

		private int _pointchangetype;

		private string _pointremark;

		private int _pointshopid;

		private DateTime _pointcreatetime;

		private int _pointuserid;

		private string _pointordercode;

		private int pointgivememid;

		public int PointID
		{
			get
			{
				return this._pointid;
			}
			set
			{
				this._pointid = value;
			}
		}

		public int PointMemID
		{
			get
			{
				return this._pointmemid;
			}
			set
			{
				this._pointmemid = value;
			}
		}

		public int PointNumber
		{
			get
			{
				return this._pointnumber;
			}
			set
			{
				this._pointnumber = value;
			}
		}

		public int PointChangeType
		{
			get
			{
				return this._pointchangetype;
			}
			set
			{
				this._pointchangetype = value;
			}
		}

		public string PointRemark
		{
			get
			{
				return this._pointremark;
			}
			set
			{
				this._pointremark = value;
			}
		}

		public int PointShopID
		{
			get
			{
				return this._pointshopid;
			}
			set
			{
				this._pointshopid = value;
			}
		}

		public DateTime PointCreateTime
		{
			get
			{
				return this._pointcreatetime;
			}
			set
			{
				this._pointcreatetime = value;
			}
		}

		public int PointUserID
		{
			get
			{
				return this._pointuserid;
			}
			set
			{
				this._pointuserid = value;
			}
		}

		public string PointOrderCode
		{
			get
			{
				return this._pointordercode;
			}
			set
			{
				this._pointordercode = value;
			}
		}

		public int PointGiveMemID
		{
			get
			{
				return this.pointgivememid;
			}
			set
			{
				this.pointgivememid = value;
			}
		}
	}
}
