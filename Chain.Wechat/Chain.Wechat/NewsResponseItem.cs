using System;
using System.Xml;

namespace Chain.Wechat
{
	public class NewsResponseItem
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

		public string PicUrl
		{
			get;
			set;
		}

		public string Url
		{
			get;
			set;
		}

		public XmlDocument XmlDoc
		{
			get;
			set;
		}

		public NewsResponseItem(string title, string description, string picUrl, string url)
		{
			this.Title = title;
			this.Description = description;
			this.PicUrl = picUrl;
			this.Url = url;
			this.XmlDoc = new XmlDocument();
			XmlElement xml = this.XmlDoc.CreateElement("item");
			XmlElement nodeTitle = this.XmlDoc.CreateElement("Title");
			XmlCDataSection xmlCData = this.XmlDoc.CreateCDataSection(title);
			nodeTitle.AppendChild(xmlCData);
			xml.AppendChild(nodeTitle);
			XmlElement nodeDescription = this.XmlDoc.CreateElement("Description");
			xmlCData = this.XmlDoc.CreateCDataSection(description);
			nodeDescription.AppendChild(xmlCData);
			xml.AppendChild(nodeDescription);
			XmlElement nodePicUrl = this.XmlDoc.CreateElement("PicUrl");
			xmlCData = this.XmlDoc.CreateCDataSection(picUrl);
			nodePicUrl.AppendChild(xmlCData);
			xml.AppendChild(nodePicUrl);
			XmlElement nodeUrl = this.XmlDoc.CreateElement("Url");
			xmlCData = this.XmlDoc.CreateCDataSection(url);
			nodeUrl.AppendChild(xmlCData);
			xml.AppendChild(nodeUrl);
			this.XmlDoc.AppendChild(xml);
		}
	}
}
