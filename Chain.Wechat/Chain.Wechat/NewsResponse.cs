using System;
using System.Collections.Generic;
using System.Xml;

namespace Chain.Wechat
{
	public class NewsResponse : IResponse
	{
		public int ArticleCount
		{
			get;
			set;
		}

		public List<NewsResponseItem> List
		{
			get;
			set;
		}

		public NewsResponse(IRequest req, List<NewsResponseItem> list) : base(req)
		{
			base.Request = req;
			this.List = list;
			if (list != null)
			{
				this.ArticleCount = list.Count;
			}
			else
			{
				this.ArticleCount = 0;
			}
			XmlNode xml = base.XmlDoc.GetElementsByTagName("xml")[0];
			XmlElement nodeMsgType = base.XmlDoc.CreateElement("MsgType");
			XmlCDataSection xmlCData = base.XmlDoc.CreateCDataSection("news");
			nodeMsgType.AppendChild(xmlCData);
			xml.AppendChild(nodeMsgType);
			XmlElement nodeContent = base.XmlDoc.CreateElement("ArticleCount");
			xmlCData = base.XmlDoc.CreateCDataSection(list.Count.ToString());
			nodeContent.AppendChild(xmlCData);
			xml.AppendChild(nodeContent);
			XmlElement nodeArticles = base.XmlDoc.CreateElement("Articles");
			foreach (NewsResponseItem newResponseItem in list)
			{
				XmlNode node = newResponseItem.XmlDoc.FirstChild;
				XmlNode newItem = base.XmlDoc.ImportNode(node, true);
				nodeArticles.AppendChild(newItem);
			}
			xml.AppendChild(nodeArticles);
		}
	}
}
