using System;

namespace Chain.Model
{
	[Serializable]
	public class MapLocationInfo
	{
		private float lng;

		private float lat;

		public float Lng
		{
			get
			{
				return this.lng;
			}
			set
			{
				this.lng = value;
			}
		}

		public float Lat
		{
			get
			{
				return this.lat;
			}
			set
			{
				this.lat = value;
			}
		}
	}
}
