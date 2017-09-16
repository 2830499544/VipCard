using ChainStock.Controls;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class MemRechargeCount : PageBase
	{
		protected HtmlForm frmMemRechargeCount;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected Pay ucPay;

		protected MemberSearch ucMemberSearch;

		protected HtmlInputButton btnServiceSearch;

		protected HtmlGenericControl spOrderAccount;

		protected HtmlGenericControl lblOrderUSer;

		protected HtmlInputText txtOrderTime;

		protected HtmlSelect sltStaff;

		protected HtmlGenericControl lblTotalNumber;

		protected HtmlGenericControl lblTotalMoney;

		protected HtmlGenericControl lblTotalPoint;

		protected HtmlGenericControl lblStaffMoney;

		protected HtmlGenericControl lblIsSms;

		protected HtmlInputCheckBox chkSMS;

		protected HtmlGenericControl lblIsPrint;

		protected HtmlInputCheckBox chkPrint;

		protected HtmlInputCheckBox chkAllowPwd;

		protected Label lblPrintTitle;

		protected Label lblPrintFoot;

		protected HtmlInputCheckBox chkStaff;

		protected HtmlInputText txtStaffType;

		protected HtmlInputHidden MemCard;

		protected HtmlInputHidden ShopID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["MemCard"] != null)
				{
					this.MemCard.Value = base.Request.QueryString["MemCard"];
				}
				this.chkAllowPwd.Checked = this.curParameter.bolPwd;
				PubFunction.Get_PrintTitle(ref this.lblPrintTitle, ref this.lblPrintFoot, this._UserShopID);
				this.lblOrderUSer.InnerText = this._UserName;
				this.spOrderAccount.InnerText = this.curParameter.strMemCountPrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
				this.txtOrderTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				this.ShopID.Value = this._UserShopID.ToString();
				this.chkSMS.Checked = (this.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByMemRedTimes);
				this.lblIsSms.Style.Add("display", (this.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByMemRedTimes) ? "\"\"" : "none");
				this.lblIsPrint.Visible = this.curParameter.bolAutoPrint;
				this.chkPrint.Checked = this.curParameter.bolAutoPrint;
				this.ucMemberSearch.BolSenseICCard = this.curParameter.bolSenseiccard;
				this.ucMemberSearch.BolContactICCard = this.curParameter.bolContacticcard;
				this.PointNum.Value = PubFunction.GetPointNum("HYCC");
				PubFunction.BindStaff(this._UserShopID, this.sltStaff, true);
				this.chkStaff.Checked = this.curParameter.bolStaff;
				this.txtStaffType.Value = this.curParameter.bolStaffType.ToString();
			}
		}
	}
}
