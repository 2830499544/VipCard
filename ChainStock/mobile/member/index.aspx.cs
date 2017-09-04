using Chain.BLL;
using Chain.Model;
using Chain.Wechat;
using Newtonsoft.Json;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.member
{
	public class index : Page
	{
		protected HtmlImage imgPhoto;

		protected HtmlGenericControl spMemName;

		protected HtmlGenericControl spLevelName;

		protected HtmlGenericControl spMemCard;

		protected HtmlGenericControl spMemState;

		protected HtmlGenericControl spMemMoney;

		protected HtmlGenericControl spMemPoint;

		protected HtmlInputHidden txtUrl;

		protected HtmlInputHidden txtLinkUrl;

		protected HtmlInputHidden txtRechargeUrl;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemWeiXinCard"] != null)
				{
					string openid = this.Session["MemWeiXinCard"].ToString();
					int MemID = new Chain.BLL.Mem().GetMemIDByWhere("MemWeiXinCard='" + openid + "'");
					if (MemID != 0)
					{
						this.Session["MemID"] = MemID.ToString();
						this.Session["MemWeiXinCard"] = openid;
						this.BindMemInfo(MemID);
					}
					else
					{
						base.Response.Redirect("login.aspx");
					}
				}
				else if (this.Session["MemID"] == null)
				{
					if (base.Request.QueryString["state"] != null)
					{
						string code = "";
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
								string openid = obj["openid"].ToString();
								int MemID = new Chain.BLL.Mem().GetMemIDByWhere("MemWeiXinCard='" + openid + "'");
								if (MemID != 0)
								{
									this.Session["MemID"] = MemID.ToString();
									this.Session["MemWeiXinCard"] = openid;
									this.BindMemInfo(MemID);
								}
								else
								{
									base.Response.Redirect("login.aspx");
								}
							}
						}
					}
					else if (base.Request.QueryString["state"] == null)
					{
						string url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http://{1}/mobile/member/index.aspx&response_type=code&scope=snsapi_base&state=0#wechat_redirect", PubFunction.curParameter.strWeiXinAppID, PubFunction.curParameter.strDoMain);
						base.Response.Redirect(url);
					}
				}
				else
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					this.BindMemInfo(MemID);
				}
			}
		}

		private void BindMemInfo(int MemID)
		{
			Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModel(MemID);
			this.spMemCard.InnerHtml = modelMem.MemCard;
			this.spMemMoney.InnerHtml = modelMem.MemMoney.ToString("#0.00");
			this.spMemPoint.InnerHtml = modelMem.MemPoint.ToString("#0");
			this.spMemName.InnerHtml = ((modelMem.MemName.ToString() == "") ? "&nbsp;" : modelMem.MemName.ToString());
			if (modelMem.MemPhoto != null && modelMem.MemPhoto != "")
			{
				this.imgPhoto.Src = modelMem.MemPhoto.ToString();
			}
			else
			{
				this.imgPhoto.Src = "images/headimg.jpg";
			}
			this.spLevelName.InnerHtml = new Chain.BLL.MemLevel().GetNameByID(modelMem.MemLevelID);
			this.spMemState.InnerHtml = this.GetMemState(modelMem.MemState);
			string url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http://{1}/mobile/member/binding.aspx&response_type=code&scope=snsapi_userinfo&state=0#wechat_redirect", PubFunction.curParameter.strWeiXinAppID, PubFunction.curParameter.strDoMain);
			this.txtUrl.Value = url;
			string RechargeUrl = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri=http://{1}/mobile/member/rechargeOnline.aspx&response_type=code&scope=snsapi_base&state=0#wechat_redirect", PubFunction.curParameter.strWeiXinAppID, PubFunction.curParameter.strDoMain);
			this.txtRechargeUrl.Value = RechargeUrl;
		}

		public string GetAuthorize(string code)
		{
			string templateUrl = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
			templateUrl = string.Format(templateUrl, PubFunction.curParameter.strWeiXinAppID, PubFunction.curParameter.strWeiXinAppSecret, code);
			HttpRequestHelper hrh = new HttpRequestHelper();
			return hrh.Reqeust(templateUrl);
		}

		private string GetMemState(int memState)
		{
			string strState = "";
			switch (memState)
			{
			case 0:
				strState = "正常";
				break;
			case 1:
				strState = "锁定";
				break;
			case 2:
				strState = "挂失";
				break;
			}
			return strState;
		}
	}
}
