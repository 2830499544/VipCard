using System;
using System.Xml;

namespace Chain.Wechat
{
	public class LocationRequest : IRequest
	{
		public string Location_X
		{
			get;
			set;
		}

		public string Location_Y
		{
			get;
			set;
		}

		public string Scale
		{
			get;
			set;
		}

		public string Label
		{
			get;
			set;
		}

		public string MsgId
		{
			get;
			set;
		}

		public LocationRequest(XmlDocument input) : base(input)
		{
			XmlNode root = base.XmlDoc.FirstChild;
			this.Location_X = root["Location_X"].InnerText;
			this.Location_Y = root["Location_Y"].InnerText;
			this.Scale = root["Scale"].InnerText;
			this.Label = root["Label"].InnerText;
			this.MsgId = root["MsgId"].InnerText;
		}
	}
}
