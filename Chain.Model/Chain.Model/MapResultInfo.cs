using System;

namespace Chain.Model
{
	[Serializable]
	public class MapResultInfo
	{
		private string title;

		private MapLocationInfo location;

		private MapAddress_componentsInfo address_components;

		private float similarity;

		private float deviation;

		private int reliability;

		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		public MapLocationInfo Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		public MapAddress_componentsInfo Address_components
		{
			get
			{
				return this.address_components;
			}
			set
			{
				this.address_components = value;
			}
		}

		public float Similarity
		{
			get
			{
				return this.similarity;
			}
			set
			{
				this.similarity = value;
			}
		}

		public float Deviation
		{
			get
			{
				return this.deviation;
			}
			set
			{
				this.deviation = value;
			}
		}

		public int Reliability
		{
			get
			{
				return this.reliability;
			}
			set
			{
				this.reliability = value;
			}
		}
	}
}
