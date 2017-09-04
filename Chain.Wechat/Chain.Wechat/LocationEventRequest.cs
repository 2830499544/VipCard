using System;
using System.Xml;

namespace Chain.Wechat
{
	public class LocationEventRequest : EventRequest
	{
		public double Latitude
		{
			get;
			set;
		}

		public double Longitude
		{
			get;
			set;
		}

		public double Precision
		{
			get;
			set;
		}

		public LocationEventRequest(XmlDocument input) : base(input)
		{
			XmlNode root = base.XmlDoc.FirstChild;
			this.Latitude = double.Parse(root["Latitude"].InnerText);
			this.Longitude = double.Parse(root["Longitude"].InnerText);
			this.Precision = double.Parse(root["Precision"].InnerText);
		}
	}
}
