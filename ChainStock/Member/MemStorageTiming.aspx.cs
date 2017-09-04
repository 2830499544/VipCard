using ChainStock.Controls;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class MemStorageTiming : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmExpense;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected MemberSearch ucMemberSearch;

		protected Pay ucPay;

		protected HtmlInputHidden MemCard;

		protected HtmlInputCheckBox chkAllowPwd;

		protected Label lblPrintTitle;

		protected Label lblPrintFoot;

		protected HtmlGenericControl lblOrderAccount;

		protected HtmlGenericControl lblOrderCreateTime;

		protected HtmlGenericControl lblOrderUSer;

		protected HtmlInputText txtRechargeTime;

		protected HtmlInputText txtExpMoney;

		protected HtmlInputText txtDiscountMoney;

		protected HtmlInputText txtExpPoint;

		protected HtmlTextArea txtExpRemark;

		protected HtmlGenericControl lblIsSMS;

		protected HtmlInputCheckBox chkSMS;

		protected HtmlInputCheckBox chkIsSMS;

		protected HtmlInputCheckBox chkPrint;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.lblOrderAccount.InnerText = this.curParameter.StorageTimingPrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
				this.lblOrderCreateTime.InnerText = DateTime.Now.ToString();
				this.lblOrderUSer.InnerText = this._UserName;
				PubFunction.Get_PrintTitle(ref this.lblPrintTitle, ref this.lblPrintFoot, this._UserShopID);
				this.lblIsSMS.Style.Add("display", (this.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByTimingConsumption) ? "\"\"" : "none");
				this.chkIsSMS.Checked = this.curParameter.bolSms;
				this.chkPrint.Checked = this.curParameter.bolAutoPrint;
				this.chkAllowPwd.Checked = this.curParameter.bolPwd;
				this.chkSMS.Checked = (this.curParameter.bolMoneySms && this.curParameter.IsAutoSendSMSByStorageTiming);
				this.ucMemberSearch.BolSenseICCard = this.curParameter.bolSenseiccard;
				this.ucMemberSearch.BolContactICCard = this.curParameter.bolContacticcard;
				this.PointNum.Value = PubFunction.GetPointNum("HYCS");
			}
		}
	}
}
