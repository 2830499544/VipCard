using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.ExtraService
{
	public class SendEmail : PageBase
	{
		protected HtmlForm frmSendEmail;

		protected Literal ltlTitle;

		protected HtmlInputButton btnChoose;

		protected HtmlTextArea txtMemEmail;

		protected HtmlTextArea txtCustomEmail;

		protected HtmlInputText txtTitle;

		protected HtmlTextArea txtEmailContent;

		protected HtmlInputButton btnSendEmail;

		protected HtmlInputButton btnReset;

		protected HtmlInputHidden isEmail;

		protected HtmlInputText txtQueryMem;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				if (string.IsNullOrEmpty(PubFunction.curParameter.EmailAdress) || string.IsNullOrEmpty(PubFunction.curParameter.EmailPwd) || string.IsNullOrEmpty(PubFunction.curParameter.EmailSMTP) || string.IsNullOrEmpty(PubFunction.curParameter.EnterpriseEmailPort.ToString()))
				{
					this.isEmail.Value = "1";
					this.btnChoose.Attributes.Add("disabled", "disabled");
					this.txtCustomEmail.Attributes.Add("disabled", "disabled");
					this.txtTitle.Attributes.Add("disabled", "disabled");
					this.txtEmailContent.Attributes.Add("disabled", "disabled");
					this.btnSendEmail.Attributes.Add("disabled", "disabled");
					this.btnReset.Attributes.Add("disabled", "disabled");
				}
			}
		}
	}
}
