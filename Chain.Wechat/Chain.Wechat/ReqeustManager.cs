using System;
using System.Xml;

namespace Chain.Wechat
{
	public class ReqeustManager
	{
		public static IRequest GetRequest(string postData)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(postData);
			XmlNode root = doc.FirstChild;
			string innerText = root["MsgType"].InnerText;
			IRequest result;
			switch (innerText)
			{
			case "text":
				result = new TextRequest(doc);
				return result;
			case "image":
				result = new ImageRequest(doc);
				return result;
			case "voice":
				result = new VoiceRequest(doc);
				return result;
			case "video":
				result = new VideoRequest(doc);
				return result;
			case "location":
				result = new LocationRequest(doc);
				return result;
			case "link":
				result = new LinkRequest(doc);
				return result;
			case "event":
			{
				string eventType = root["Event"].InnerText;
				if (root["EventKey"] != null && root["EventKey"].InnerText != "" && (eventType == "subscribe" || eventType == "SCAN"))
				{
					result = new ScanRequest(doc);
					return result;
				}
				if (eventType == "subscribe" || eventType == "unsubscribe")
				{
					result = new EventRequest(doc);
					return result;
				}
				if (eventType == "LOCATION")
				{
					result = new LocationEventRequest(doc);
					return result;
				}
				if (eventType == "CLICK" || eventType == "VIEW")
				{
					result = new MenuRequest(doc);
					return result;
				}
				result = new EventRequest(doc);
				return result;
			}
			}
			result = new IRequest(doc);
			return result;
		}
	}
}
