using System;
using System.Text;
using System.Xml;

namespace Chain.Wechat
{
	public class CheckOrder
	{
		public XmlNode GetCheckOrder(string appid, string mch_id, string transaction_id, string out_trade_no, string nonce_str, string sign)
		{
			string requestUrl = "https://api.mch.weixin.qq.com/pay/orderquery";
			StringBuilder strb = new StringBuilder();
			strb.Append("<xml>");
			strb.AppendFormat("<appid>{0}</appid>", appid);
			strb.AppendFormat("<mch_id>{0}</mch_id>", mch_id);
			strb.AppendFormat("<nonce_str>{0}</nonce_str>", nonce_str);
			strb.AppendFormat("<out_trade_no>{0}</out_trade_no>", out_trade_no);
			strb.AppendFormat("<sign>{0}</sign>", sign);
			strb.AppendFormat("<transaction_id>{0}</transaction_id>", transaction_id);
			strb.Append("</xml>");
			HttpRequestHelper hrh = new HttpRequestHelper();
			string str = hrh.Reqeust(requestUrl, strb.ToString());
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(str);
			return doc.FirstChild;
		}
	}
}
