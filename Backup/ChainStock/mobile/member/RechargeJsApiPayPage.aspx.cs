using Chain.Wechat;
using Newtonsoft.Json;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using WxPayAPI;

namespace ChainStock.mobile.member
{
	public class RechargeJsApiPayPage : Page
	{
		protected HtmlForm Form1;

		protected HtmlGenericControl sp_out_trade_no;

		public static string wxJsApiParam
		{
			get;
			set;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				string code = "";
				string openid = "";
				if (base.Request.QueryString["code"] != null)
				{
					code = base.Request.QueryString["code"].ToString();
				}
				if (code != "")
				{
					string getAuthorize = this.GetAuthorize(code);
					getAuthorize = "[" + getAuthorize + "]";
					JavaScriptArray javascript = (JavaScriptArray)JavaScriptConvert.DeserializeObject(getAuthorize);
					JavaScriptObject obj = (JavaScriptObject)javascript[0];
					if (obj["openid"] != null && obj["openid"].ToString() != "")
					{
						openid = obj["openid"].ToString();
					}
				}
				string total_fee = base.Request.QueryString["state"];
				if (string.IsNullOrEmpty(openid) || string.IsNullOrEmpty(total_fee))
				{
					base.Response.Write("<span style='color:#FF0000;font-size:20px'>页面传参出错,请返回重试</span>");
				}
				else
				{
					string money = base.Request["money"].ToString();
					string givemoney = base.Request["givemoney"].ToString();
					string orderAccount = base.Request["OrderAccount"];
					int MemID = int.Parse(base.Request["MemID"].ToString());
					int point = int.Parse(base.Request["point"].ToString());
					string attach = string.Format("{0},{1},{2},{3},{4}", new object[]
					{
						MemID,
						money,
						givemoney,
						orderAccount,
						point
					});
					JsApiPay jsApiPay = new JsApiPay(this);
					jsApiPay.openid = openid;
					jsApiPay.total_fee = int.Parse(total_fee);
					jsApiPay.out_trade_no = orderAccount;
					jsApiPay.body = "微信充值";
					jsApiPay.notify_url = "http://" + PubFunction.curParameter.strDoMain + "/mobile/member/RechargeResultNotifyPage.aspx";
					jsApiPay.spbill_create_ip = PubFunction.ipAdress;
					jsApiPay.attach = attach;
					this.sp_out_trade_no.InnerHtml = jsApiPay.out_trade_no;
					try
					{
						WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
						RechargeJsApiPayPage.wxJsApiParam = jsApiPay.GetJsApiParameters();
						if (!(unifiedOrderResult.GetValue("return_code").ToString() == "SUCCESS"))
						{
							base.Response.Write("<span style='color:#FF0000;font-size:20px'>下单失败!</span>");
						}
					}
					catch (Exception ex)
					{
						base.Response.Write(string.Concat(new string[]
						{
							"<span style='color:#FF0000;font-size:20px'>下单失败，请返回重试",
							ex.ToString(),
							"openid:",
							openid,
							"</span>"
						}));
					}
				}
			}
		}

		public string GetAuthorize(string code)
		{
			string templateUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
			templateUrl = string.Format(templateUrl, PubFunction.curParameter.strWeiXinAppID, PubFunction.curParameter.strWeiXinAppSecret, code);
			HttpRequestHelper hrh = new HttpRequestHelper();
			return hrh.Reqeust(templateUrl);
		}
	}
}
