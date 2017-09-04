using System;
using System.Xml;

namespace Chain.Wechat
{
	public class VoiceResponse : IResponse
	{
		public string MediaId
		{
			get;
			set;
		}

		public VoiceResponse(IRequest req, string mediaId) : base(req)
		{
			base.Request = req;
			this.MediaId = mediaId;
			XmlNode xml = base.XmlDoc.GetElementsByTagName("xml")[0];
			XmlElement nodeMsgType = base.XmlDoc.CreateElement("MsgType");
			XmlCDataSection xmlCData = base.XmlDoc.CreateCDataSection("text");
			nodeMsgType.AppendChild(xmlCData);
			xml.AppendChild(nodeMsgType);
			XmlElement nodeVoice = base.XmlDoc.CreateElement("Voice");
			XmlElement nodeMediaId = base.XmlDoc.CreateElement("MediaId");
			xmlCData = base.XmlDoc.CreateCDataSection(mediaId);
			nodeMediaId.AppendChild(xmlCData);
			nodeVoice.AppendChild(nodeMediaId);
			xml.AppendChild(nodeVoice);
		}
	}
}
