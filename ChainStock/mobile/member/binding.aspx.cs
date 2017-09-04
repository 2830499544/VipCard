using Chain.BLL;
using Chain.Model;
using Chain.Wechat;
using Newtonsoft.Json;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.member
{
	public class binding : Page
	{
		protected HtmlInputText txtMemCard;

		protected HtmlImage headimgurl;

		protected HtmlGenericControl spNickName;

		protected HtmlAnchor bindCardOk;

		protected HtmlAnchor cancelBindCard;

		protected HtmlInputHidden txtMemID;

		protected HtmlInputHidden txtOpenID;

		protected void Page_Load(object sender, EventArgs e)
		{
			string code = "";
			string openid = "";
			string access_token = "";
			if (base.Request.QueryString["code"] != null)
			{
				code = base.Request.QueryString["code"].ToString();
			}
			else
			{
				string url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http://{1}/mobile/member/binding.aspx&response_type=code&scope=snsapi_userinfo&state=0#wechat_redirect", PubFunction.curParameter.strWeiXinAppID, PubFunction.curParameter.strDoMain);
				base.Response.Redirect(url);
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
					this.txtOpenID.Value = openid;
				}
				if (obj["access_token"] != null && obj["access_token"].ToString() != "")
				{
					access_token = obj["access_token"].ToString();
				}
				string getUserInformation = this.GetUserInformation(access_token, openid, "zh_CN");
				getUserInformation = "[" + getUserInformation + "]";
				JavaScriptArray javascriptinfo = (JavaScriptArray)JavaScriptConvert.DeserializeObject(getUserInformation);
				JavaScriptObject info = (JavaScriptObject)javascriptinfo[0];
				if (info["nickname"] != null && info["nickname"].ToString() != "")
				{
					this.spNickName.InnerHtml = info["nickname"].ToString();
				}
				if (info["headimgurl"] != null && info["headimgurl"].ToString() != "")
				{
					this.headimgurl.Src = info["headimgurl"].ToString();
				}
				int MemID = new Chain.BLL.Mem().GetMemIDByWhere("MemWeiXinCard='" + openid + "'");
				if (MemID != 0)
				{
					this.bindCardOk.Visible = false;
					this.cancelBindCard.Visible = true;
				}
				else
				{
					this.bindCardOk.Visible = true;
					this.cancelBindCard.Visible = false;
				}
			}
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModel(MemID);
					this.txtMemID.Value = MemID.ToString();
				}
				else
				{
					base.Response.Redirect("login.aspx");
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

		public string GetUserInformation(string ACCESS_TOKEN, string OPENID, string zh_CN)
		{
			string templateUrl = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang={2}";
			templateUrl = string.Format(templateUrl, ACCESS_TOKEN, OPENID, zh_CN);
			HttpRequestHelper hrh = new HttpRequestHelper();
			return hrh.Reqeust(templateUrl);
		}
	}
}
