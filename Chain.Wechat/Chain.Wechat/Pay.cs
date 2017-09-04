using System;
using System.Text;
using System.Xml;

namespace Chain.Wechat
{
	public class Pay
	{
		public string UnitePay(string appid, string attach, string body, string mch_id, string nonce_str, string notify_url, string openid, string out_trade_no, string spbill_create_ip, string time_expire, string time_start, string total_fee, string trade_type, string uniteSign)
		{
			string prepay_id = "";
			string result;
			try
			{
				string requestUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";
				StringBuilder strb = new StringBuilder();
				strb.Append("<xml>");
				if (trade_type.Equals("JSAPI"))
				{
					strb.AppendFormat("<appid>{0}</appid>", appid);
					strb.AppendFormat("<attach>{0}</attach>", attach);
					strb.AppendFormat("<body>{0}</body>", body);
					strb.AppendFormat("<mch_id>{0}</mch_id>", mch_id);
					strb.AppendFormat("<nonce_str>{0}</nonce_str>", nonce_str);
					strb.AppendFormat("<notify_url>{0}</notify_url>", notify_url);
					strb.AppendFormat("<openid>{0}</openid>", openid);
					strb.AppendFormat("<out_trade_no>{0}</out_trade_no>", out_trade_no);
					strb.AppendFormat("<spbill_create_ip>{0}</spbill_create_ip>", spbill_create_ip);
					strb.AppendFormat("<time_expire>{0}</time_expire>", time_expire);
					strb.AppendFormat("<time_start>{0}</time_start>", time_start);
					strb.AppendFormat("<total_fee>{0}</total_fee>", total_fee);
					strb.AppendFormat("<trade_type>{0}</trade_type>", trade_type);
					strb.AppendFormat("<sign>{0}</sign>", uniteSign);
				}
				strb.Append("</xml>");
				HttpRequestHelper hrh = new HttpRequestHelper();
				string str = hrh.Reqeust(requestUrl, strb.ToString());
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(str);
				XmlNode root = doc.FirstChild;
				prepay_id = root["prepay_id"].InnerText;
				result = prepay_id;
			}
			catch
			{
				result = prepay_id;
			}
			return result;
		}
	}
}
