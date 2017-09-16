using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;

public class ResponseSendStr
{
	public static string Text(string postStr, string Content)
	{
		if (Content.EndsWith("\r\n"))
		{
			Content = Content.TrimEnd(new char[]
			{
				'\r',
				'\n'
			});
		}
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(postStr);
		string ToUserName = xmlDoc.GetElementsByTagName("ToUserName")[0].InnerText;
		string FromUserName = xmlDoc.GetElementsByTagName("FromUserName")[0].InnerText;
		xmlDoc = new XmlDocument();
		XmlElement xml = xmlDoc.CreateElement("xml");
		XmlElement eToUserName = xmlDoc.CreateElement("ToUserName");
		XmlCDataSection xmlCData = xmlDoc.CreateCDataSection(FromUserName);
		eToUserName.AppendChild(xmlCData);
		xml.AppendChild(eToUserName);
		XmlElement eFromUserName = xmlDoc.CreateElement("FromUserName");
		xmlCData = xmlDoc.CreateCDataSection(ToUserName);
		eFromUserName.AppendChild(xmlCData);
		xml.AppendChild(eFromUserName);
		XmlElement eCreateTime = xmlDoc.CreateElement("CreateTime");
		eCreateTime.InnerText = DateTime.Now.Ticks.ToString();
		xml.AppendChild(eCreateTime);
		XmlElement eMsgType = xmlDoc.CreateElement("MsgType");
		xmlCData = xmlDoc.CreateCDataSection(MsgType.text.ToString());
		eMsgType.AppendChild(xmlCData);
		xml.AppendChild(eMsgType);
		XmlElement eContent = xmlDoc.CreateElement("Content");
		xmlCData = xmlDoc.CreateCDataSection(Content);
		eContent.AppendChild(xmlCData);
		xml.AppendChild(eContent);
		xmlDoc.AppendChild(xml);
		return xmlDoc.InnerXml;
	}

	public static string News(string postStr, List<WeiXinNews> WeiXinNewsList)
	{
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(postStr);
		string ToUserName = xmlDoc.GetElementsByTagName("ToUserName")[0].InnerText;
		string FromUserName = xmlDoc.GetElementsByTagName("FromUserName")[0].InnerText;
		xmlDoc = new XmlDocument();
		XmlElement xml = xmlDoc.CreateElement("xml");
		XmlElement eToUserName = xmlDoc.CreateElement("ToUserName");
		XmlCDataSection xmlCData = xmlDoc.CreateCDataSection(FromUserName);
		eToUserName.AppendChild(xmlCData);
		xml.AppendChild(eToUserName);
		XmlElement eFromUserName = xmlDoc.CreateElement("FromUserName");
		xmlCData = xmlDoc.CreateCDataSection(ToUserName);
		eFromUserName.AppendChild(xmlCData);
		xml.AppendChild(eFromUserName);
		XmlElement eCreateTime = xmlDoc.CreateElement("CreateTime");
		eCreateTime.InnerText = DateTime.Now.Ticks.ToString();
		xml.AppendChild(eCreateTime);
		XmlElement eMsgType = xmlDoc.CreateElement("MsgType");
		xmlCData = xmlDoc.CreateCDataSection(MsgType.news.ToString());
		eMsgType.AppendChild(xmlCData);
		xml.AppendChild(eMsgType);
		if (WeiXinNewsList != null)
		{
			XmlElement eArticleCount = xmlDoc.CreateElement("ArticleCount");
			eArticleCount.InnerText = WeiXinNewsList.Count.ToString();
			xml.AppendChild(eArticleCount);
			XmlElement eArticles = xmlDoc.CreateElement("Articles");
			foreach (WeiXinNews item in WeiXinNewsList)
			{
				XmlElement eitem = xmlDoc.CreateElement("item");
				XmlElement eTitle = xmlDoc.CreateElement("Title");
				xmlCData = xmlDoc.CreateCDataSection(item.NewsTitle);
				eTitle.AppendChild(xmlCData);
				eitem.AppendChild(eTitle);
				XmlElement eDescription = xmlDoc.CreateElement("Description");
				xmlCData = xmlDoc.CreateCDataSection(item.NewsDesc);
				eDescription.AppendChild(xmlCData);
				eitem.AppendChild(eDescription);
				XmlElement ePicUrl = xmlDoc.CreateElement("PicUrl");
				xmlCData = xmlDoc.CreateCDataSection(item.NewsUrlFirst);
				ePicUrl.AppendChild(xmlCData);
				eitem.AppendChild(ePicUrl);
				XmlElement eUrl = xmlDoc.CreateElement("Url");
				xmlCData = xmlDoc.CreateCDataSection(item.NewsUrlSecond);
				eUrl.AppendChild(xmlCData);
				eitem.AppendChild(eUrl);
				eArticles.AppendChild(eitem);
			}
			xml.AppendChild(eArticles);
		}
		xmlDoc.AppendChild(xml);
		return xmlDoc.InnerXml;
	}

	public static string Menu(DataTable dtMenu)
	{
		StringBuilder flag = new StringBuilder("{\"button\":[");
		DataRow[] ID = dtMenu.Select("parentMenuID=0", "MenuID asc");
		for (int MemID = 0; MemID < ID.Length; MemID++)
		{
			DataRow[] CouponBll = dtMenu.Select("parentMenuID=" + ID[MemID]["MenuID"], "MenuID asc");
			if (CouponBll.Length > 0)
			{
				StringBuilder CouponModel = new StringBuilder();
				CouponModel.Append("{");
				CouponModel.AppendFormat("\"name\":\"{0}\",", ID[MemID]["MenuName"]);
				CouponModel.Append("\"sub_button\":[");
				for (int CouponListBll = 0; CouponListBll < CouponBll.Length; CouponListBll++)
				{
					StringBuilder strWhere = new StringBuilder();
					strWhere.Append("{");
					string row = (Convert.ToInt32(CouponBll[CouponListBll]["MenuType"]) == 1) ? "click" : "view";
					string strSql = CouponBll[CouponListBll]["MenuName"].ToString();
					string arraySql = (CouponBll[CouponListBll]["MenuKey"].ToString() != "") ? "key" : "url";
					string strMenuKeyOrUrl = (arraySql == "key") ? CouponBll[CouponListBll]["MenuKey"].ToString() : CouponBll[CouponListBll]["MenuUrl"].ToString();
					strWhere.AppendFormat("\"type\":\"{0}\",", row);
					strWhere.AppendFormat("\"name\":\"{0}\",", strSql);
					strWhere.AppendFormat("\"{0}\":\"{1}\"", arraySql, strMenuKeyOrUrl);
					strWhere.Append("}");
					if (CouponListBll != CouponBll.Length - 1)
					{
						strWhere.Append(",");
					}
					CouponModel.Append(strWhere);
				}
				CouponModel.Append("]}");
				if (MemID != ID.Length - 1)
				{
					CouponModel.Append(",");
				}
				flag.Append(CouponModel);
			}
		}
		flag.Append("]}");
		return flag.ToString();
	}
}
