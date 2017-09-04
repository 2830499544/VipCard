using System;

namespace Chain.Model
{
	[Serializable]
	public class GiftClass
	{
		private int _giftclassid;

		private string _giftclassname;

		private string _giftclassremark;

		private int _giftparentid;

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

		public string GiftClassName
		{
			get
			{
				return this._giftclassname;
			}
			set
			{
				this._giftclassname = value;
			}
		}

		public string GiftClassRemark
		{
			get
			{
				return this._giftclassremark;
			}
			set
			{
				this._giftclassremark = value;
			}
		}

		public int GiftParentID
		{
			get
			{
				return this._giftparentid;
			}
			set
			{
				this._giftparentid = value;
			}
		}
	}
}
