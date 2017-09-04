using System;
using System.Xml;

namespace Chain.Wechat
{
	public class VoiceRequest : IRequest
	{
		public string MediaId
		{
			get;
			set;
		}

		public string Format
		{
			get;
			set;
		}

		public string MsgId
		{
			get;
			set;
		}

		public VoiceRequest(XmlDocument input) : base(input)
		{
			XmlNode root = base.XmlDoc.FirstChild;
			this.MediaId = root["MediaId"].InnerText;
			this.Format = root["Format"].InnerText;
			this.MsgId = root["MsgId"].InnerText;
		}
	}
}
