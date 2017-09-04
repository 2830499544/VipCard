using Newtonsoft.Json;
using System;
using System.Text;
using System.Web.Security;

namespace Chain.Wechat
{
	public class Sign
	{
		private static DateTime ExpiresTime;

		private static string Value;

		public string ApplyForSrefundSign(string appid, string mch_id, string nonce_str, string op_user_id, string out_refund_no, string out_trade_no, string refund_fee, string total_fee, string key)
		{
			string sign = "";
			string result;
			try
			{
				StringBuilder strb = new StringBuilder();
				strb.AppendFormat("appid={0}&", appid);
				strb.AppendFormat("mch_id={0}&", mch_id);
				strb.AppendFormat("nonce_str={0}&", nonce_str);
				strb.AppendFormat("op_user_id={0}&", op_user_id);
				strb.AppendFormat("out_refund_no={0}&", out_refund_no);
				strb.AppendFormat("out_trade_no={0}&", out_trade_no);
				strb.AppendFormat("refund_fee={0}&", refund_fee);
				strb.AppendFormat("total_fee={0}&", total_fee);
				strb.AppendFormat("key={0}", key);
				sign = FormsAuthentication.HashPasswordForStoringInConfigFile(strb.ToString(), "MD5").ToUpper();
				result = sign;
			}
			catch
			{
				result = sign;
			}
			return result;
		}

		public string UniteSign(string appid, string attach, string body, string mch_id, string nonce_str, string notify_url, string openid, string out_trade_no, string spbill_create_ip, string time_expire, string time_start, string total_fee, string trade_type, string key)
		{
			string sign = "";
			string result;
			try
			{
				StringBuilder strb = new StringBuilder();
				if (trade_type.Equals("JSAPI"))
				{
					strb.AppendFormat("appid={0}&", appid);
					strb.AppendFormat("attach={0}&", attach);
					strb.AppendFormat("body={0}&", body);
					strb.AppendFormat("mch_id={0}&", mch_id);
					strb.AppendFormat("nonce_str={0}&", nonce_str);
					strb.AppendFormat("notify_url={0}&", notify_url);
					strb.AppendFormat("openid={0}&", openid);
					strb.AppendFormat("out_trade_no={0}&", out_trade_no);
					strb.AppendFormat("spbill_create_ip={0}&", spbill_create_ip);
					strb.AppendFormat("time_expire={0}&", time_expire);
					strb.AppendFormat("time_start={0}&", time_start);
					strb.AppendFormat("total_fee={0}&", total_fee);
					strb.AppendFormat("trade_type={0}&", trade_type);
				}
				strb.AppendFormat("key={0}", key);
				sign = FormsAuthentication.HashPasswordForStoringInConfigFile(strb.ToString(), "MD5").ToUpper();
				result = sign;
			}
			catch
			{
				result = sign;
			}
			return result;
		}

		public string ChooseWXPaySign(string appId, string nonceStr, string package, string signType, string timeStamp, string key)
		{
			string sign = "";
			string result;
			try
			{
				StringBuilder strSB = new StringBuilder();
				strSB.AppendFormat("appId={0}&", appId);
				strSB.AppendFormat("nonceStr={0}&", nonceStr);
				strSB.AppendFormat("package={0}&", package);
				strSB.AppendFormat("signType={0}&", signType);
				strSB.AppendFormat("timeStamp={0}&", timeStamp);
				strSB.AppendFormat("key={0}", key);
				sign = FormsAuthentication.HashPasswordForStoringInConfigFile(strSB.ToString(), "MD5").ToUpper();
				result = sign;
			}
			catch
			{
				result = sign;
			}
			return result;
		}

		public string ConfigSign(string jsapi_ticket, string noncestr, string timestamp, string url)
		{
			string sign = "";
			string result;
			try
			{
				StringBuilder strSB = new StringBuilder();
				strSB.AppendFormat("jsapi_ticket={0}&", jsapi_ticket);
				strSB.AppendFormat("noncestr={0}&", noncestr);
				strSB.AppendFormat("timestamp={0}&", timestamp);
				strSB.AppendFormat("url={0}", url);
				sign = FormsAuthentication.HashPasswordForStoringInConfigFile(strSB.ToString(), "SHA1").ToLower();
				result = sign;
			}
			catch
			{
				result = sign;
			}
			return result;
		}

		public string JsapiTicket(string strWeiXinAppID, string strWeiXinAppSecret)
		{
			string value;
			try
			{
				if (Sign.ExpiresTime != DateTime.MinValue && DateTime.Now < Sign.ExpiresTime)
				{
					if (Sign.Value != "")
					{
						value = Sign.Value;
						return value;
					}
				}
				string errorCode;
				string errorMessage;
				string accessToken = AccessToken.Get(strWeiXinAppID, strWeiXinAppSecret, out errorCode, out errorMessage);
				HttpRequestHelper hr = new HttpRequestHelper();
				string url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", accessToken);
				string reponseText = hr.Reqeust(url);
				reponseText = "[" + reponseText + "]";
				JavaScriptArray javascript = (JavaScriptArray)JavaScriptConvert.DeserializeObject(reponseText);
				JavaScriptObject obj = (JavaScriptObject)javascript[0];
				Sign.Value = obj["ticket"].ToString();
				Sign.ExpiresTime = DateTime.Now.AddSeconds(Convert.ToDouble(obj["expires_in"])).AddMinutes(-10.0);
				value = Sign.Value;
			}
			catch
			{
				value = Sign.Value;
			}
			return value;
		}

		public string OrderTrackingSign(string appid, string mch_id, string nonce_str, string out_trade_no, string transaction_id, string key)
		{
			string sign = "";
			string result;
			try
			{
				StringBuilder strSBs = new StringBuilder();
				strSBs.AppendFormat("appid={0}&", appid);
				strSBs.AppendFormat("mch_id={0}&", mch_id);
				strSBs.AppendFormat("nonce_str={0}&", nonce_str);
				strSBs.AppendFormat("out_trade_no={0}&", out_trade_no);
				strSBs.AppendFormat("transaction_id={0}&", transaction_id);
				strSBs.AppendFormat("key={0}", key);
				sign = FormsAuthentication.HashPasswordForStoringInConfigFile(strSBs.ToString(), "MD5").ToUpper();
				result = sign;
			}
			catch
			{
				result = sign;
			}
			return result;
		}
	}
}
