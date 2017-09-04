using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class WeiXinConfig : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmWeiXinRuleList;

		protected HtmlInputText txtSystemDomain;

		protected Literal ltlTitle;

		protected HtmlGenericControl step1;

		protected HtmlSelect txtWeixXinType;

		protected HtmlSelect txtWeiXinVerified;

		protected HtmlSelect selPay;

		protected HtmlInputText txtMchId;

		protected TextBox txtAPI;

		protected HtmlInputText txtXiane;

		protected HtmlInputText txtWeiXinToken;

		protected HtmlInputButton btnCreateRandomToken;

		protected HtmlInputText txtEncodingAESKey;

		protected HtmlInputButton btnCreateRandomAESKey;

		protected HtmlInputText txtWeiXinShopName;

		protected HtmlTextArea txtWeiXinSalutatory;

		protected HtmlInputButton Step1Next;

		protected Label lblMessage;

		protected HtmlGenericControl spanUrl;

		protected HtmlGenericControl spanToken;

		protected HtmlGenericControl spanEncodingAESKey;

		protected HtmlInputText txtAppId;

		protected HtmlInputText txtAppSecret;

		protected HtmlInputText txtSignInPoint;

		protected HtmlInputCheckBox chkWeiXinSMSVcode;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				Chain.Model.SysParameter mdlSysParameter = new Chain.BLL.SysParameter().GetModel(1);
				this.selPay.Value = mdlSysParameter.Pay.ToString();
				this.txtMchId.Value = mdlSysParameter.MchId;
				this.txtAPI.Attributes.Add("value", mdlSysParameter.Api);
				this.txtXiane.Value = Convert.ToInt32(mdlSysParameter.Xiane).ToString();
				this.txtWeixXinType.Value = (this.curParameter.bolWeiXinType ? "1" : "0");
				this.txtWeiXinVerified.Value = (this.curParameter.bolWeiXinVerified ? "1" : "0");
				this.txtSignInPoint.Value = this.curParameter.intSignInPoint.ToString();
				this.txtWeiXinToken.Value = this.curParameter.strWeiXinToken;
				this.txtEncodingAESKey.Value = this.curParameter.strWeiXinEncodingAESKey;
				this.txtWeiXinShopName.Value = this.curParameter.strWeiXinShopName;
				this.txtWeiXinSalutatory.Value = this.curParameter.strWeiXinSalutatory;
				this.txtAppId.Value = this.curParameter.strWeiXinAppID;
				this.txtAppSecret.Value = this.curParameter.strWeiXinAppSecret;
				this.chkWeiXinSMSVcode.Checked = this.curParameter.bolWeiXinSMSVcode;
				this.spanUrl.InnerText = base.Request.Url.AbsoluteUri.Substring(0, base.Request.Url.AbsoluteUri.IndexOf(base.Request.Url.AbsolutePath)) + "/Service/WeiXinHandler.ashx";
				this.spanToken.InnerText = PubFunction.curParameter.strWeiXinToken;
				this.spanEncodingAESKey.InnerText = PubFunction.curParameter.strWeiXinEncodingAESKey;
			}
			if (base.Request["weixin"] != null)
			{
				if (base.Request["weixin"] != "0")
				{
					this.SetState("disabled");
				}
			}
			else if (PubFunction.curParameter.istry != 1)
			{
				this.SetState("disabled");
			}
		}

		public void SetState(string disabled)
		{
			this.txtWeixXinType.Attributes.Add("disabled", disabled);
			this.txtWeiXinVerified.Attributes.Add("disabled", disabled);
			this.txtSignInPoint.Attributes.Add("disabled", disabled);
			this.txtWeiXinToken.Attributes.Add("disabled", disabled);
			this.txtEncodingAESKey.Attributes.Add("disabled", disabled);
			this.txtWeiXinShopName.Attributes.Add("disabled", disabled);
			this.txtWeiXinSalutatory.Attributes.Add("disabled", disabled);
			this.txtAppId.Attributes.Add("disabled", disabled);
			this.txtAppSecret.Attributes.Add("disabled", disabled);
			this.chkWeiXinSMSVcode.Attributes.Add("disabled", disabled);
			this.Step1Next.Attributes.Add("disabled", disabled);
			this.btnCreateRandomAESKey.Attributes.Add("disabled", disabled);
			this.btnCreateRandomToken.Attributes.Add("disabled", disabled);
			if (disabled != "")
			{
				this.lblMessage.Visible = true;
			}
		}
	}
}
