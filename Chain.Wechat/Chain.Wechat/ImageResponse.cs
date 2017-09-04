using System;
using System.Xml;

namespace Chain.Wechat
{
	public class ImageResponse : IResponse
	{
		public string MediaId
		{
			get;
			set;
		}

		public ImageResponse(IRequest req, string mediaId) : base(req)
		{
			base.Request = req;
			this.MediaId = mediaId;
			XmlNode xml = base.XmlDoc.GetElementsByTagName("xml")[0];
			XmlElement nodeMsgType = base.XmlDoc.CreateElement("MsgType");
			XmlCDataSection xmlCData = base.XmlDoc.CreateCDataSection("text");
			nodeMsgType.AppendChild(xmlCData);
			xml.AppendChild(nodeMsgType);
			XmlElement nodeImage = base.XmlDoc.CreateElement("Image");
			XmlElement nodeMediaId = base.XmlDoc.CreateElement("MediaId");
			xmlCData = base.XmlDoc.CreateCDataSection(mediaId);
			nodeMediaId.AppendChild(xmlCData);
			nodeImage.AppendChild(nodeMediaId);
			xml.AppendChild(nodeImage);
		}
	}
}
