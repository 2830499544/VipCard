using Chain.BLL;
using Chain.Model;
using ChainStock.Controls;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.PointGift
{
	public class GiftExchange : PageBase
	{
		private Chain.BLL.PointGift bllPg = new Chain.BLL.PointGift();

		private Chain.Model.PointGift modelPg = new Chain.Model.PointGift();

		private Chain.BLL.PointExchange bllPe = new Chain.BLL.PointExchange();

		protected HtmlForm frmGift;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected MemberSearch ucMemberSearch;

		protected HtmlInputButton btnGiftSearch;

		protected HtmlGenericControl lblExchangeTime;

		protected HtmlGenericControl lblExchangeUSer;

		protected HtmlInputHidden MemCard;

		protected HtmlGenericControl lblExchangePrefix;

		protected HtmlGenericControl lblTotalNumber;

		protected HtmlGenericControl lblTotalPoint;

		protected HtmlGenericControl lblIsSMS;

		protected HtmlInputCheckBox chkSMS;

		protected HtmlInputCheckBox chkIsSMS;

		protected HtmlInputCheckBox chkPrint;

		protected Label lblPrintTitle;

		protected Label lblPrintFoot;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.lblExchangeTime.InnerText = DateTime.Now.ToString();
				this.lblExchangeUSer.InnerText = PubFunction.UserIDTOName(this._UserID);
				this.lblExchangePrefix.InnerText = PubFunction.curParameter.strGiftExchangePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
				PubFunction.Get_PrintTitle(ref this.lblPrintTitle, ref this.lblPrintFoot, this._UserShopID);
				this.chkIsSMS.Checked = PubFunction.curParameter.bolSms;
				this.chkSMS.Checked = (PubFunction.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByMemGiftExchange);
				this.lblIsSMS.Style.Add("display", (PubFunction.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByMemGiftExchange) ? "\"\"" : "none");
				this.chkPrint.Checked = PubFunction.curParameter.bolAutoPrint;
				if (base.Request.QueryString["MemCard"] != null)
				{
					this.MemCard.Value = base.Request.QueryString["MemCard"];
				}
				this.ucMemberSearch.BolSenseICCard = this.curParameter.bolSenseiccard;
				this.ucMemberSearch.BolContactICCard = this.curParameter.bolContacticcard;
				this.PointNum.Value = PubFunction.GetPointNum("JFDH");
			}
		}
	}
}
