using Chain.BLL;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.PointManage
{
	public class PointDraw : PageBase
	{
		protected HtmlForm frmPointChange;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected HtmlInputText txtDrawType;

		protected HtmlGenericControl lblDrawAccount;

		protected HtmlInputText txtShopID;

		protected HtmlGenericControl lblDrawTime;

		protected HtmlGenericControl lblDrawUser;

		protected HtmlInputText txtDrawUserID;

		protected HtmlInputText txtTotalPoint;

		protected HtmlGenericControl lbl_PointDrawPercent;

		protected HtmlInputText txtDrawPoint;

		protected HtmlInputText txtDrawAmount;

		protected HtmlTextArea txtDrawRemark;

		protected HtmlInputCheckBox chkSMS;

		protected HtmlInputCheckBox chkPrint;

		protected Label lblPrintTitle;

		protected Label lblPrintFoot;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["PID"] != null)
				{
					int pid = int.Parse(base.Request.QueryString["PID"]);
					if (pid == 147)
					{
						this.txtDrawType.Value = "2";
					}
					else if (pid == 148)
					{
						this.txtDrawType.Value = "3";
						this.ltlTitle.Text = "主页  >  商家管理  >  商家积分申请提现";
					}
				}
				this.chkSMS.Checked = (this.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByMemPointChange);
				this.chkPrint.Checked = this.curParameter.bolAutoPrint;
				this.lblDrawAccount.InnerText = this.curParameter.strMemPointChangePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
				this.lblDrawTime.InnerText = DateTime.Now.ToString();
				this.lblDrawUser.InnerText = this._UserName;
				this.txtDrawUserID.Value = this._UserID.ToString();
				PubFunction.Get_PrintTitle(ref this.lblPrintTitle, ref this.lblPrintFoot, this._UserShopID);
				SysShop shop = new SysShop();
				Chain.BLL.PointDraw bllPointDraw = new Chain.BLL.PointDraw();
				if (this._UserShopID == 1)
				{
					this.txtTotalPoint.Value = "0";
				}
				else
				{
					int point = shop.GetShopPointByShopid(this._UserShopID, int.Parse(this.txtDrawType.Value));
					int drawPoint = bllPointDraw.GetShopPointDraw(this._UserShopID);
					if (drawPoint >= point)
					{
						this.txtTotalPoint.Value = "0";
					}
					else
					{
						this.txtTotalPoint.Value = (point - drawPoint).ToString();
					}
				}
				this.txtShopID.Value = this._UserShopID.ToString();
				this.lbl_PointDrawPercent.InnerText = PubFunction.curParameter.PointDrawPercent.ToString();
				this.PointNum.Value = PubFunction.GetPointNum("JFBD");
			}
		}
	}
}
