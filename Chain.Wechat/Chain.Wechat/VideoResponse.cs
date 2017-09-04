using System;
using System.Xml;

namespace Chain.Wechat
{
	public class VideoResponse : IResponse
	{
		public string MediaId
		{
			get;
			set;
		}

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

		public VideoResponse(IRequest req, string mediaId, string title, string description) : base(req)
		{
			base.Request = req;
			this.MediaId = mediaId;
			this.Title = title;
			this.Description = description;
			XmlNode xml = base.XmlDoc.GetElementsByTagName("xml")[0];
			XmlElement nodeMsgType = base.XmlDoc.CreateElement("MsgType");
			XmlCDataSection xmlCData = base.XmlDoc.CreateCDataSection("text");
			nodeMsgType.AppendChild(xmlCData);
			xml.AppendChild(nodeMsgType);
			XmlElement nodeVideo = base.XmlDoc.CreateElement("Video");
			XmlElement nodeMediaId = base.XmlDoc.CreateElement("MediaId");
			xmlCData = base.XmlDoc.CreateCDataSection(mediaId);
			nodeMediaId.AppendChild(xmlCData);
			nodeVideo.AppendChild(nodeMediaId);
			XmlElement nodeTitle = base.XmlDoc.CreateElement("Title");
			xmlCData = base.XmlDoc.CreateCDataSection(mediaId);
			nodeTitle.AppendChild(xmlCData);
			nodeVideo.AppendChild(nodeTitle);
			XmlElement nodeDescription = base.XmlDoc.CreateElement("Description");
			xmlCData = base.XmlDoc.CreateCDataSection(mediaId);
			nodeDescription.AppendChild(xmlCData);
			nodeVideo.AppendChild(nodeDescription);
			xml.AppendChild(nodeVideo);
		}
	}
}
