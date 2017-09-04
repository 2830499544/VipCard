using System;

namespace Chain.Model
{
	[Serializable]
	public class ProductClass
	{
		private int _classid;

		private string _classname;

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
