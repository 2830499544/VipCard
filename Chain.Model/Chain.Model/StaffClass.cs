using System;

namespace Chain.Model
{
	[Serializable]
	public class StaffClass
	{
		private int _classid;

		private string _classname;

		private bool _classtype;

		private decimal _classpercent;

		private int _classshopid;

		private string _classremark;

		public int ClassID
		{
			get
			{
				return this._classid;
			}
			set
			{
				this._classid = value;
			}
		}

		public string ClassName
		{
			get
			{
				return this._classname;
			}
			set
			{
				this._classname = value;
			}
		}

		public bool ClassType
		{
			get
			{
				return this._classtype;
			}
			set
			{
				this._classtype = value;
			}
		}

		public decimal ClassPercent
		{
			get
			{
				return this._classpercent;
			}
			set
			{
				this._classpercent = value;
			}
		}

		public int ClassShopID
		{
			get
			{
				return this._classshopid;
			}
			set
			{
				this._classshopid = value;
			}
		}

		public string ClassRemark
		{
			get
			{
				return this._classremark;
			}
			set
			{
				this._classremark = value;
			}
		}
	}
}
