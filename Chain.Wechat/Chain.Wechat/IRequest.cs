using System;
using System.Xml;

namespace Chain.Wechat
{
	public class IRequest
	{
		public delegate string RequestEventHandler();

		public event IRequest.RequestEventHandler RequestEvent;

		public string ToUserName
		{
			get;
			set;
		}

		public string FromUserName
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

		public XmlDocument XmlDoc
		{
			get;
			set;
		}

		public IRequest(XmlDocument input)
		{
			this.XmlDoc = input;
			XmlNode root = this.XmlDoc.FirstChild;
			this.ToUserName = root["ToUserName"].InnerText;
			this.FromUserName = root["FromUserName"].InnerText;
			this.CreateTime = root["ToUserName"].InnerText;
			this.MsgType = root["MsgType"].InnerText;
		}

		public virtual string Response()
		{
			string result;
			if (this.RequestEvent != null)
			{
				result = this.RequestEvent();
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
