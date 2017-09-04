using System;
using System.Xml;

namespace Chain.Wechat
{
	public class VideoRequest : IRequest
	{
		public string MediaId
		{
			get;
			set;
		}

		public string ThumbMediaId
		{
			get;
			set;
		}

		public string MsgId
		{
			get;
			set;
		}

		public VideoRequest(XmlDocument input) : base(input)
		{
			XmlNode root = base.XmlDoc.FirstChild;
			this.MediaId = root["MediaId"].InnerText;
			this.ThumbMediaId = root["ThumbMediaId"].InnerText;
			this.MsgId = root["MsgId"].InnerText;
		}
	}
}
