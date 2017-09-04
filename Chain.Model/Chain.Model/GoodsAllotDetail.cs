using System;

namespace Chain.Model
{
	[Serializable]
	public class GoodsAllotDetail
	{
		private int _allotdetailid;

		private int _allotdetailallotid;

		private int _allotdetailgoodsid;

		private decimal _allotdetailnumber;

		public int AllotDetailID
		{
			get
			{
				return this._allotdetailid;
			}
			set
			{
				this._allotdetailid = value;
			}
		}

		public int AllotDetailAllotID
		{
			get
			{
				return this._allotdetailallotid;
			}
			set
			{
				this._allotdetailallotid = value;
			}
		}

		public int AllotDetailGoodsID
		{
			get
			{
				return this._allotdetailgoodsid;
			}
			set
			{
				this._allotdetailgoodsid = value;
			}
		}

		public decimal AllotDetailNumber
		{
			get
			{
				return this._allotdetailnumber;
			}
			set
			{
				this._allotdetailnumber = value;
			}
		}
	}
}
