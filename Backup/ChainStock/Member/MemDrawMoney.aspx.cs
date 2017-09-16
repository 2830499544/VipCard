using ChainStock.Controls;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class MemDrawMoney : PageBase
	{
		protected HtmlForm frmDrawMoney;

		protected Literal ltlTitle;

		protected MemberSearch ucMemberSearch;

		protected HtmlGenericControl lblMemDrawMoney;

		protected HtmlGenericControl lblDrawMoneyTime;

		protected HtmlGenericControl spanDrawPercent;

		protected HtmlInputHidden txtDrawPercent;

		protected HtmlInputText txtDrawMoney;

		protected HtmlInputText txtAccountMoney;

		protected HtmlTextArea txtDrawRemark;

		protected HtmlGenericControl lblIsSMS;

		protected HtmlInputCheckBox chkSMS;

		protected HtmlInputCheckBox chkPrint;

		protected HtmlInputCheckBox chkIsSMS;

		protected HtmlInputHidden lblOrderUSers;

		protected HtmlInputHidden MemCard;

		protected Label lblPrintTitle;

		protected Label lblPrintFoot;

		protected HtmlInputHidden PointNum;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request.QueryString["MemCard"] != null)
			{
				this.MemCard.Value = base.Request.QueryString["MemCard"];
			}
			this.lblMemDrawMoney.InnerText = this.curParameter.strMemDrawMoneyPrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
			this.lblDrawMoneyTime.InnerText = DateTime.Now.ToString();
			this.chkIsSMS.Checked = PubFunction.curParameter.bolSms;
			this.chkSMS.Checked = (PubFunction.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByMemWithdraw);
			this.lblIsSMS.Style.Add("display", (PubFunction.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByMemWithdraw) ? "\"\"" : "none");
			this.spanDrawPercent.InnerHtml = PubFunction.curParameter.DrawMoneyPercent.ToString() + " (注：0.5表示10元账户余额可以折现为5元，1表示余额可全额折现)";
			this.txtDrawPercent.Value = PubFunction.curParameter.DrawMoneyPercent.ToString();
			this.ucMemberSearch.BolSenseICCard = this.curParameter.bolSenseiccard;
			this.ucMemberSearch.BolContactICCard = this.curParameter.bolContacticcard;
			this.lblOrderUSers.Value = PubFunction.UserIDTOName(this._UserID);
			this.PointNum.Value = PubFunction.GetPointNum("ZHTX");
			PubFunction.Get_PrintTitle(ref this.lblPrintTitle, ref this.lblPrintFoot, this._UserShopID);
		}
	}
}
