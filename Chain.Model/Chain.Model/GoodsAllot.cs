using System;

namespace Chain.Model
{
	[Serializable]
	public class GoodsAllot
	{
		private int _allotid;

		private string _allotaccount;

		private int _allotoutshopid;

		private int _allotinshopid;

		private DateTime _allotcreatetime;

		private decimal _allottotalnumber;

		private int _allotuserid;

		private string _allotremark;

		private int _allotstate;

		public int AllotID
		{
			get
			{
				return this._allotid;
			}
			set
			{
				this._allotid = value;
			}
		}

		public string AllotAccount
		{
			get
			{
				return this._allotaccount;
			}
			set
			{
				this._allotaccount = value;
			}
		}

		public int AllotOutShopID
		{
			get
			{
				return this._allotoutshopid;
			}
			set
			{
				this._allotoutshopid = value;
			}
		}

		public int AllotInShopID
		{
			get
			{
				return this._allotinshopid;
			}
			set
			{
				this._allotinshopid = value;
			}
		}

		public DateTime AllotCreateTime
		{
			get
			{
				return this._allotcreatetime;
			}
			set
			{
				this._allotcreatetime = value;
			}
		}

		public decimal AllotTotalNumber
		{
			get
			{
				return this._allottotalnumber;
			}
			set
			{
				this._allottotalnumber = value;
			}
		}

		public int AllotUserID
		{
			get
			{
				return this._allotuserid;
			}
			set
			{
				this._allotuserid = value;
			}
		}

		public string AllotRemark
		{
			get
			{
				return this._allotremark;
			}
			set
			{
				this._allotremark = value;
			}
		}

		public int Allotstate
		{
			get
			{
				return this._allotstate;
			}
			set
			{
				this._allotstate = value;
			}
		}
	}
}
