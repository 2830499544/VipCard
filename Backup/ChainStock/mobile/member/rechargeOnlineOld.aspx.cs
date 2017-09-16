using Chain.BLL;
using Chain.Model;
using Chain.Wechat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.member
{
	public class rechargeOnlineOld : Page
	{
		protected HtmlGenericControl spMemCard;

		protected HtmlGenericControl spMemName;

		protected HtmlInputHidden txtPointRate;

		protected HtmlInputHidden txtOpenID;

		protected HtmlInputHidden txtMemID;

		protected HtmlInputHidden txtMaxMoney;

		protected HtmlInputHidden appid;

		protected HtmlInputHidden timestamps;

		protected HtmlInputHidden nonceStrs;

		protected HtmlInputHidden signatures;

		protected HtmlInputHidden signTypes;

		protected HtmlInputHidden paySigns;

		protected HtmlInputHidden package;

		protected HtmlInputHidden txtNow;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				string code = "";
				if (base.Request.QueryString["code"] != null)
				{
					code = base.Request.QueryString["code"].ToString();
				}
				else
				{
					string RechargeUrl = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http://{1}/mobile/member/rechargeOnline.aspx&response_type=code&scope=snsapi_base&state=0#wechat_redirect", PubFunction.curParameter.strWeiXinAppID, PubFunction.curParameter.strDoMain);
					base.Response.Redirect(RechargeUrl);
				}
				if (code != "")
				{
					string getAuthorize = this.GetAuthorize(code);
					getAuthorize = "[" + getAuthorize + "]";
					JavaScriptArray javascript = (JavaScriptArray)JavaScriptConvert.DeserializeObject(getAuthorize);
					JavaScriptObject obj = (JavaScriptObject)javascript[0];
					if (obj["openid"] != null && obj["openid"].ToString() != "")
					{
						string openid = obj["openid"].ToString();
						this.txtOpenID.Value = openid;
					}
				}
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					this.txtMemID.Value = MemID.ToString();
					this.BindMemInfo(MemID);
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
				Chain.Model.SysParameter parameter = new Chain.BLL.SysParameter().GetModel(1);
				this.txtMaxMoney.Value = parameter.Xiane.ToString();
				DateTime now = DateTime.Now;
				this.txtNow.Value = now.ToString();
				DateTime utcnow = DateTime.UtcNow;
				string timestamp = Convert.ToInt64((utcnow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString();
				this.timestamps.Value = timestamp;
				string systemDomain = parameter.SystemDomain;
				this.appid.Value = parameter.WeiXinAppID;
				string nonceStr = Guid.NewGuid().ToString().Replace("-", "");
				this.nonceStrs.Value = nonceStr;
				this.signTypes.Value = "MD5";
				string url = base.Request.Url.ToString();
				Sign sign = new Sign();
				string jsapiTicket = sign.JsapiTicket(parameter.WeiXinAppID, parameter.WeiXinAppSecret);
				if (!(jsapiTicket == ""))
				{
					string signature = sign.ConfigSign(jsapiTicket, nonceStr, timestamp, url);
					this.signatures.Value = signature;
				}
			}
		}

		private void BindMemInfo(int MemID)
		{
			Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModel(MemID);
			this.spMemCard.InnerHtml = modelMem.MemCard;
			this.spMemName.InnerHtml = modelMem.MemName.ToString();
			List<Chain.Model.SysShopMemLevel> list = new Chain.BLL.SysShopMemLevel().GetModelList(string.Format(" ShopID=1 and MemLeveLID={0} ", modelMem.MemLevelID));
			if (list.Count > 0)
			{
				Chain.Model.MemLevel mdlMemLevel = new Chain.BLL.MemLevel().GetModel(modelMem.MemLevelID);
				if (list[0].ClassRechargePointRate > 0m)
				{
					this.txtPointRate.Value = list[0].ClassRechargePointRate.ToString();
				}
				else
				{
					this.txtPointRate.Value = "0";
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
