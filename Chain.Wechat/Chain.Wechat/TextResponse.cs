using System;
using System.Xml;

namespace Chain.Wechat
{
	public class TextResponse : IResponse
	{
		public string Content
		{
			get;
			set;
		}

		public TextResponse(IRequest req, string content) : base(req)
		{
			base.Request = req;
			this.Content = content;
			XmlNode xml = base.XmlDoc.GetElementsByTagName("xml")[0];
			XmlElement nodeMsgType = base.XmlDoc.CreateElement("MsgType");
			XmlCDataSection xmlCData = base.XmlDoc.CreateCDataSection("text");
			nodeMsgType.AppendChild(xmlCData);
			xml.AppendChild(nodeMsgType);
			XmlElement nodeContent = base.XmlDoc.CreateElement("Content");
			xmlCData = base.XmlDoc.CreateCDataSection(content);
			nodeContent.AppendChild(xmlCData);
			xml.AppendChild(nodeContent);
		}

		public TextResponse(IRequest req) : this(req, "")
		{
		}
	}
}
