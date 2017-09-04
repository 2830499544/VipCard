using Chain.BLL;
using Chain.Model;
using ChainStock.Controls;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.StockManage
{
	public class GoodsExpense : PageBase
	{
		protected HtmlForm ftmGoodsExpense;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected Pay ucPay;

		protected MemberSearch ucMemberSearch;

		protected HtmlInputHidden MemCard;

		protected HtmlInputButton btnGoodsSearch;

		protected HtmlGenericControl spOrderAccount;

		protected HtmlGenericControl lblExpenseUSer;

		protected HtmlInputText txtExpenseTime;

		protected HtmlSelect sltStaff;

		protected HtmlGenericControl lblTotalNumber;

		protected HtmlGenericControl lblTotalMoney;

		protected HtmlGenericControl lblTotalDiscountMoney;

		protected HtmlGenericControl lblTotalPoint;

		protected HtmlGenericControl lblStaffMoney;

		protected HtmlInputCheckBox chkSMS;

		protected HtmlInputCheckBox chkPrint;

		protected HtmlInputButton btnEntryOrder;

		protected HtmlInputCheckBox chkAllowPwd;

		protected HtmlInputCheckBox chkStaff;

		protected HtmlInputText txtStaffType;

		protected HtmlInputText txtOrderID;

		protected Label lblPrintTitle;

		protected Label lblPrintFoot;

		protected HtmlInputHidden ShopID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.spOrderAccount.InnerText = this.curParameter.strGoodsExpensePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
				if (base.Request.QueryString["MemCard"] != null)
				{
					this.MemCard.Value = base.Request.QueryString["MemCard"];
				}
				this.ucMemberSearch.BolSenseICCard = this.curParameter.bolSenseiccard;
				this.ucMemberSearch.BolContactICCard = this.curParameter.bolContacticcard;
				if (base.Request.QueryString["OID"] != null)
				{
					this.txtOrderID.Value = base.Request.QueryString["OID"];
					Chain.Model.OrderLog modelOrder = new Chain.BLL.OrderLog().GetModel(int.Parse(base.Request.QueryString["OID"]));
					this.txtOrderID.Value = modelOrder.OrderID.ToString();
					this.spOrderAccount.InnerText = modelOrder.OrderAccount;
				}
				this.chkSMS.Checked = (this.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByCommodityConsumption);
				this.chkPrint.Checked = this.curParameter.bolAutoPrint;
				this.lblExpenseUSer.InnerText = this._UserName;
				this.ShopID.Value = this._UserShopID.ToString();
				this.txtExpenseTime.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				this.chkAllowPwd.Checked = this.curParameter.bolPwd;
				this.chkStaff.Checked = this.curParameter.bolStaff;
				this.txtStaffType.Value = this.curParameter.bolStaffType.ToString();
				PubFunction.Get_PrintTitle(ref this.lblPrintTitle, ref this.lblPrintFoot, this._UserShopID);
				PubFunction.BindStaff(this._UserShopID, this.sltStaff, true);
				this.PointNum.Value = PubFunction.GetPointNum("SPXF");
			}
		}
	}
}
