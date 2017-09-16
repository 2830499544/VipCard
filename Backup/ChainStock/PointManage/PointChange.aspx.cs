using ChainStock.Controls;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.PointManage
{
	public class PointChange : PageBase
	{
		protected HtmlForm frmPointChange;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected MemberSearch ucMemberSearch;

		protected HtmlGenericControl lblPointChange;

		protected HtmlGenericControl lblPointChangeTime;

		protected HtmlGenericControl lblPointChangeUser;

		protected HtmlInputText txtChangeNumber;

		protected HtmlTextArea txtChangeRemark;

		protected HtmlInputCheckBox chkSMS;

		protected HtmlInputCheckBox chkPrint;

		protected Label lblPrintTitle;

		protected Label lblPrintFoot;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.chkSMS.Checked = (this.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByMemPointChange);
				this.chkPrint.Checked = this.curParameter.bolAutoPrint;
				this.lblPointChange.InnerText = this.curParameter.strMemPointChangePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
				this.lblPointChangeTime.InnerText = DateTime.Now.ToString();
				this.lblPointChangeUser.InnerText = this._UserName;
				PubFunction.Get_PrintTitle(ref this.lblPrintTitle, ref this.lblPrintFoot, this._UserShopID);
				this.PointNum.Value = PubFunction.GetPointNum("JFBD");
			}
		}
	}
}
