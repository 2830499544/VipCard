using System;

namespace Chain.Model
{
	[Serializable]
	public class GoodsClass
	{
		private int _classid;

		private string _classname;

		private string _classremark;

		private int _parentid;

		private int _createshopid;

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

		public int ParentID
		{
			get
			{
				return this._parentid;
			}
			set
			{
				this._parentid = value;
			}
		}

		public int CreateShopID
		{
			get
			{
				return this._createshopid;
			}
			set
			{
				this._createshopid = value;
			}
		}
	}
}
