using System;

namespace Chain.Model
{
	[Serializable]
	public class TimingCategory
	{
		private int _categoryid;

		private string _categoryname;

		private int _categoryfatherid;

		private int _categoryshopid;

		private int _categoryuserid;

		private string _categoryrremark;

		public int CategoryID
		{
			get
			{
				return this._categoryid;
			}
			set
			{
				this._categoryid = value;
			}
		}

		public string CategoryName
		{
			get
			{
				return this._categoryname;
			}
			set
			{
				this._categoryname = value;
			}
		}

		public int CategoryFatherID
		{
			get
			{
				return this._categoryfatherid;
			}
			set
			{
				this._categoryfatherid = value;
			}
		}

		public int CategoryShopID
		{
			get
			{
				return this._categoryshopid;
			}
			set
			{
				this._categoryshopid = value;
			}
		}

		public int CategoryUserID
		{
			get
			{
				return this._categoryuserid;
			}
			set
			{
				this._categoryuserid = value;
			}
		}

		public string CategoryrRemark
		{
			get
			{
				return this._categoryrremark;
			}
			set
			{
				this._categoryrremark = value;
			}
		}
	}
}
