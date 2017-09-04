using System;
using System.Xml;

namespace Chain.Wechat
{
	public class LinkRequest : IRequest
	{
		public string Title
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string Url
		{
			get;
			set;
		}

		public string MsgId
		{
			get;
			set;
		}

		public LinkRequest(XmlDocument input) : base(input)
		{
			XmlNode root = base.XmlDoc.FirstChild;
			this.Title = root["Title"].InnerText;
			this.Description = root["Description"].InnerText;
			this.Url = root["Url"].InnerText;
			this.MsgId = root["MsgId"].InnerText;
		}
	}
}
