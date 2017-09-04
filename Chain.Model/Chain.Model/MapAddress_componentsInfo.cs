using System;

namespace Chain.Model
{
	[Serializable]
	public class MapAddress_componentsInfo
	{
		private string province;

		private string city;

		private string district;

		private string street;

		private string street_number;

		public string Province
		{
			get
			{
				return this.province;
			}
			set
			{
				this.province = value;
			}
		}

		public string City
		{
			get
			{
				return this.city;
			}
			set
			{
				this.city = value;
			}
		}

		public string District
		{
			get
			{
				return this.district;
			}
			set
			{
				this.district = value;
			}
		}

		public string Street
		{
			get
			{
				return this.street;
			}
			set
			{
				this.street = value;
			}
		}

		public string Street_number
		{
			get
			{
				return this.street_number;
			}
			set
			{
				this.street_number = value;
			}
		}
	}
}
