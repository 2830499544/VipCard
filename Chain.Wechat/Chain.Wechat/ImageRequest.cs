using System;
using System.Xml;

namespace Chain.Wechat
{
	public class ImageRequest : IRequest
	{
		public string PicUrl
		{
			get;
			set;
		}

		public string MediaId
		{
			get;
			set;
		}

		public string MsgId
		{
			get;
			set;
		}

		public ImageRequest(XmlDocument input) : base(input)
		{
			XmlNode root = base.XmlDoc.FirstChild;
			this.PicUrl = root["PicUrl"].InnerText;
			this.MediaId = root["MediaId"].InnerText;
			this.MsgId = root["MsgId"].InnerText;
		}
	}
}
