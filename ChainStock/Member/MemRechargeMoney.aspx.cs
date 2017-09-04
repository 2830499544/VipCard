using ChainStock.Controls;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class MemRechargeMoney : PageBase
	{
		protected HtmlForm frmRechargeMoney;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected MemberSearch ucMemberSearch;

		protected HtmlGenericControl lblAccount;

		protected HtmlGenericControl lblRechargeTime;

		protected HtmlGenericControl lblRechargeUSer;

		protected HtmlInputText txtMoney;

		protected HtmlInputText txtGiveMoney;

		protected HtmlInputText txtTotal;

		protected HtmlInputText txtPoint;

		protected HtmlTextArea txtRechangeRemark;

		protected HtmlGenericControl lblIsSMS;

		protected HtmlInputCheckBox chkSMS;

		protected HtmlInputCheckBox chkIsSMS;

		protected HtmlInputCheckBox chkPrint;

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
				this.lblAccount.InnerText = this.curParameter.strMemRechargePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
				this.lblRechargeTime.InnerText = DateTime.Now.ToString();
				this.lblRechargeUSer.InnerText = PubFunction.UserIDTOName(this._UserID);
				PubFunction.Get_PrintTitle(ref this.lblPrintTitle, ref this.lblPrintFoot, this._UserShopID);
				this.chkSMS.Checked = (PubFunction.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByMemRecharge);
				this.lblIsSMS.Style.Add("display", (this.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByMemRecharge) ? "\"\"" : "none");
				this.chkIsSMS.Checked = PubFunction.curParameter.bolSms;
				this.chkPrint.Checked = PubFunction.curParameter.bolAutoPrint;
				this.PointNum.Value = PubFunction.GetPointNum("HYCZ");
				this.ucMemberSearch.BolSenseICCard = this.curParameter.bolSenseiccard;
				this.ucMemberSearch.BolContactICCard = this.curParameter.bolContacticcard;
			}
		}
	}
}
