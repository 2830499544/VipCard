using System;
using System.Text.RegularExpressions;

namespace Chain.Wechat
{
	public class AccessToken
	{
		private static DateTime ExpiresTime;

		private static string Value;

		public static string Get(string appid, string secret, out string errorCode, out string errorMessage)
		{
			errorCode = string.Empty;
			errorMessage = string.Empty;
			string value;
			if (AccessToken.ExpiresTime != DateTime.MinValue && DateTime.Now < AccessToken.ExpiresTime)
			{
				value = AccessToken.Value;
			}
			else
			{
				string accessTokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
				accessTokenUrl = string.Format(accessTokenUrl, appid, secret);
				HttpRequestHelper hrm = new HttpRequestHelper();
				string reponseText = hrm.Reqeust(accessTokenUrl);
				string regexText = "{\"access_token\":\"(.+)\",\"expires_in\":(\\d+)}";
				Match match = Regex.Match(reponseText, regexText);
				if (match.Success)
				{
					AccessToken.Value = match.Groups[1].Value;
					AccessToken.ExpiresTime = DateTime.Now.AddSeconds(Convert.ToDouble(match.Groups[2].Value)).AddMinutes(-10.0);
				}
				else
				{
					regexText = "{\"errcode\":(\\d+),\"errmsg\":\"(.+)\"}";
					match = Regex.Match(reponseText, regexText);
					if (match.Success)
					{
						errorCode = match.Groups[1].Value;
						errorMessage = match.Groups[2].Value;
					}
				}
				value = AccessToken.Value;
			}
			return value;
		}
	}
}
