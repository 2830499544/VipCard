using System;

namespace Chain.Model
{
	[Serializable]
	public class MemAddress
	{
		private int id;

		private int memID;

		private string memName;

		private string memMobile;

		private string memProvince;

		private string memCity;

		private string memCounty;

		private string memVillage;

		private string memDetailAddress;

		private int isDefault;

		public int ID
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		public int MemID
		{
			get
			{
				return this.memID;
			}
			set
			{
				this.memID = value;
			}
		}

		public string MemName
		{
			get
			{
				return this.memName;
			}
			set
			{
				this.memName = value;
			}
		}

		public string MemMobile
		{
			get
			{
				return this.memMobile;
			}
			set
			{
				this.memMobile = value;
			}
		}

		public string MemProvince
		{
			get
			{
				return this.memProvince;
			}
			set
			{
				this.memProvince = value;
			}
		}

		public string MemCity
		{
			get
			{
				return this.memCity;
			}
			set
			{
				this.memCity = value;
			}
		}

		public string MemCounty
		{
			get
			{
				return this.memCounty;
			}
			set
			{
				this.memCounty = value;
			}
		}

		public string MemVillage
		{
			get
			{
				return this.memVillage;
			}
			set
			{
				this.memVillage = value;
			}
		}

		public string MemDetailAddress
		{
			get
			{
				return this.memDetailAddress;
			}
			set
			{
				this.memDetailAddress = value;
			}
		}

		public int IsDefault
		{
			get
			{
				return this.isDefault;
			}
			set
			{
				this.isDefault = value;
			}
		}
	}
}
