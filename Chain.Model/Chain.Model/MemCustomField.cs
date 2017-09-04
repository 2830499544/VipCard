using System;

namespace Chain.Model
{
	[Serializable]
	public class MemCustomField
	{
		private int _customfieldid;

		private int _customtype;

		private string _customfield;

		private string _customfieldname;

		private bool _customfieldisnull;

		private bool _customfieldisshow;

		private string _customfieldtype;

		private string _customfieldinfo;

		private int _customfieldshopid;

		private DateTime _customfieldcreatetime;

		private int _customfielduserid;

		public int CustomFieldID
		{
			get
			{
				return this._customfieldid;
			}
			set
			{
				this._customfieldid = value;
			}
		}

		public int CustomType
		{
			get
			{
				return this._customtype;
			}
			set
			{
				this._customtype = value;
			}
		}

		public string CustomField
		{
			get
			{
				return this._customfield;
			}
			set
			{
				this._customfield = value;
			}
		}

		public string CustomFieldName
		{
			get
			{
				return this._customfieldname;
			}
			set
			{
				this._customfieldname = value;
			}
		}

		public bool CustomFieldIsNull
		{
			get
			{
				return this._customfieldisnull;
			}
			set
			{
				this._customfieldisnull = value;
			}
		}

		public bool CustomFieldIsShow
		{
			get
			{
				return this._customfieldisshow;
			}
			set
			{
				this._customfieldisshow = value;
			}
		}

		public string CustomFieldType
		{
			get
			{
				return this._customfieldtype;
			}
			set
			{
				this._customfieldtype = value;
			}
		}

		public string CustomFieldInfo
		{
			get
			{
				return this._customfieldinfo;
			}
			set
			{
				this._customfieldinfo = value;
			}
		}

		public int CustomFieldShopID
		{
			get
			{
				return this._customfieldshopid;
			}
			set
			{
				this._customfieldshopid = value;
			}
		}

		public DateTime CustomFieldCreateTime
		{
			get
			{
				return this._customfieldcreatetime;
			}
			set
			{
				this._customfieldcreatetime = value;
			}
		}

		public int CustomFieldUserID
		{
			get
			{
				return this._customfielduserid;
			}
			set
			{
				this._customfielduserid = value;
			}
		}
	}
}
