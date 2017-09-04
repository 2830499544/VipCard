using System;
using System.Xml;

namespace Chain.Wechat
{
	public class IResponse
	{
		public string FromUserName
		{
			get;
			set;
		}

		public string ToUserName
		{
			get;
			set;
		}

		public string CreateTime
		{
			get;
			set;
		}

		public string MsgType
		{
			get;
			set;
		}

		public IRequest Request
		{
			get;
			set;
		}

		public XmlDocument XmlDoc
		{
			get;
			set;
		}

		protected IResponse(IRequest req)
		{
			this.Request = req;
			this.XmlDoc = new XmlDocument();
			XmlElement xml = this.XmlDoc.CreateElement("xml");
			XmlElement nodeToUserName = this.XmlDoc.CreateElement("ToUserName");
			XmlCDataSection xmlCData = this.XmlDoc.CreateCDataSection(req.FromUserName);
			nodeToUserName.AppendChild(xmlCData);
			xml.AppendChild(nodeToUserName);
			XmlElement nodeFromUserName = this.XmlDoc.CreateElement("FromUserName");
			xmlCData = this.XmlDoc.CreateCDataSection(req.ToUserName);
			nodeFromUserName.AppendChild(xmlCData);
			xml.AppendChild(nodeFromUserName);
			XmlElement nodeCreateTime = this.XmlDoc.CreateElement("CreateTime");
			nodeCreateTime.InnerText = DateTime.Now.Ticks.ToString();
			xml.AppendChild(nodeCreateTime);
			this.XmlDoc.AppendChild(xml);
		}

		public string Result()
		{
			return this.XmlDoc.InnerXml;
		}
	}
}
