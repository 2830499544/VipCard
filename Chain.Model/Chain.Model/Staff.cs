using System;

namespace Chain.Model
{
	[Serializable]
	public class Staff
	{
		private int _staffid;

		private string _staffnumber;

		private string _staffname;

		private bool _staffsex;

		private string _staffmobile;

		private string _staffaddress;

		private int _staffclassid;

		private string _staffremark;

		public int StaffID
		{
			get
			{
				return this._staffid;
			}
			set
			{
				this._staffid = value;
			}
		}

		public string StaffNumber
		{
			get
			{
				return this._staffnumber;
			}
			set
			{
				this._staffnumber = value;
			}
		}

		public string StaffName
		{
			get
			{
				return this._staffname;
			}
			set
			{
				this._staffname = value;
			}
		}

		public bool StaffSex
		{
			get
			{
				return this._staffsex;
			}
			set
			{
				this._staffsex = value;
			}
		}

		public string StaffMobile
		{
			get
			{
				return this._staffmobile;
			}
			set
			{
				this._staffmobile = value;
			}
		}

		public string StaffAddress
		{
			get
			{
				return this._staffaddress;
			}
			set
			{
				this._staffaddress = value;
			}
		}

		public int StaffClassID
		{
			get
			{
				return this._staffclassid;
			}
			set
			{
				this._staffclassid = value;
			}
		}

		public string StaffRemark
		{
			get
			{
				return this._staffremark;
			}
			set
			{
				this._staffremark = value;
			}
		}
	}
}
