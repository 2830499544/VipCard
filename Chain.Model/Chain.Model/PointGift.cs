using System;

namespace Chain.Model
{
	[Serializable]
	public class PointGift
	{
		private int _giftid;

		private string _giftname;

		private string _giftcode;

		private int _giftclassid;

		private string _giftphoto;

		private int _giftexchangepoint;

		private int _giftstocknumber;

		private int _giftexchangenumber;

		private int _giftshopid;

		private string _giftremark;

		public int GiftID
		{
			get
			{
				return this._giftid;
			}
			set
			{
				this._giftid = value;
			}
		}

		public string GiftName
		{
			get
			{
				return this._giftname;
			}
			set
			{
				this._giftname = value;
			}
		}

		public string GiftCode
		{
			get
			{
				return this._giftcode;
			}
			set
			{
				this._giftcode = value;
			}
		}

		public int GiftClassID
		{
			get
			{
				return this._giftclassid;
			}
			set
			{
				this._giftclassid = value;
			}
		}

		public string GiftPhoto
		{
			get
			{
				return this._giftphoto;
			}
			set
			{
				this._giftphoto = value;
			}
		}

		public int GiftExchangePoint
		{
			get
			{
				return this._giftexchangepoint;
			}
			set
			{
				this._giftexchangepoint = value;
			}
		}

		public int GiftStockNumber
		{
			get
			{
				return this._giftstocknumber;
			}
			set
			{
				this._giftstocknumber = value;
			}
		}

		public int GiftExchangeNumber
		{
			get
			{
				return this._giftexchangenumber;
			}
			set
			{
				this._giftexchangenumber = value;
			}
		}

		public int GiftShopID
		{
			get
			{
				return this._giftshopid;
			}
			set
			{
				this._giftshopid = value;
			}
		}

		public string GiftRemark
		{
			get
			{
				return this._giftremark;
			}
			set
			{
				this._giftremark = value;
			}
		}
	}
}
