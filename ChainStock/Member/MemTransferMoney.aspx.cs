using ChainStock.Controls;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class MemTransferMoney : PageBase
	{
		protected HtmlForm frmTransferMoney;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected MemberSearch ucMemberSearch;

		protected HtmlGenericControl lblAccount;

		protected HtmlGenericControl lblTransferTime;

		protected HtmlGenericControl lblTransferUSer;

		protected HtmlInputText txtTransferMemCard;

		protected HtmlInputText txtMoney;

		protected HtmlInputPassword txtPassword;

		protected HtmlTextArea txtTransferRemark;

		protected HtmlGenericControl lblIsSMS;

		protected HtmlInputCheckBox chkSMS;

		protected HtmlInputCheckBox chkIsSMS;

		protected Label lblPrintTitle;

		protected Label lblPrintFoot;

		protected HtmlInputHidden MemCard;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["MemCard"] != null)
				{
					this.MemCard.Value = base.Request.QueryString["MemCard"];
				}
				this.lblAccount.InnerText = "zz" + DateTime.Now.ToString("yyMMddHHmmssffff");
				this.lblTransferTime.InnerText = DateTime.Now.ToString();
				this.lblTransferUSer.InnerText = PubFunction.UserIDTOName(this._UserID);
				PubFunction.Get_PrintTitle(ref this.lblPrintTitle, ref this.lblPrintFoot, this._UserShopID);
				this.chkSMS.Checked = (PubFunction.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByMemRecharge);
				this.lblIsSMS.Style.Add("display", (this.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByMemRecharge) ? "\"\"" : "none");
				this.chkIsSMS.Checked = PubFunction.curParameter.bolSms;
				this.ucMemberSearch.BolSenseICCard = this.curParameter.bolSenseiccard;
				this.ucMemberSearch.BolContactICCard = this.curParameter.bolContacticcard;
			}
		}
	}
}
