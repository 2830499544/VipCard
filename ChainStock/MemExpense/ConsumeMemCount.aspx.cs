namespace ChainStock.MemExpense
{
    using Chain.BLL;
    using Chain.Model;
    using ChainStock.Controls;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class ConsumeMemCount : PageBase
    {
        protected HtmlInputButton btnGoodsSearch;
        protected HtmlInputCheckBox chkAllowPwd;
        protected HtmlInputCheckBox chkPrint;
        protected HtmlInputCheckBox chkSMS;
        protected HtmlInputCheckBox chkStaff;
        protected HtmlForm ftmConsumeMemCount;
        protected HtmlGenericControl lblExpenseUSer;
        protected Label lblPrintFoot;
        protected Label lblPrintTitle;
        protected HtmlGenericControl lblStaffMoney;
        protected HtmlGenericControl lblTotalDiscountMoney;
        protected HtmlGenericControl lblTotalMoney;
        protected HtmlGenericControl lblTotalNumber;
        protected HtmlGenericControl lblTotalPoint;
        protected Literal ltlTitle;
        protected HtmlInputHidden MemCard;
        protected HtmlInputHidden PointNum;
        protected HtmlInputHidden ShopID;
        protected HtmlGenericControl spOrderAccount;
        protected HtmlInputText txtExpenseTime;
        protected HtmlInputText txtOrderID;
        protected HtmlInputText txtStaffType;
        protected MemberSearch ucMemberSearch;
        protected Pay ucPay;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.spOrderAccount.InnerText = base.curParameter.strMemCountExpensePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
                if (base.Request.QueryString["MemCard"] != null)
                {
                    this.MemCard.Value = base.Request.QueryString["MemCard"];
                }
                if (base.Request.QueryString["OID"] != null)
                {
                    this.txtOrderID.Value = base.Request.QueryString["OID"];
                    Chain.Model.OrderLog modelOrder = new Chain.BLL.OrderLog().GetModel(int.Parse(base.Request.QueryString["OID"]));
                    this.spOrderAccount.InnerText = modelOrder.OrderAccount;
                }
                this.ucMemberSearch.BolSenseICCard = base.curParameter.bolSenseiccard;
                this.ucMemberSearch.BolContactICCard = base.curParameter.bolContacticcard;
                this.chkSMS.Checked = base.curParameter.bolMoneySms && base.curParameter.bolAutoSendSMSByCommodityConsumption;
                this.chkPrint.Checked = base.curParameter.bolAutoPrint;
                this.lblExpenseUSer.InnerText = base._UserName;
                this.ShopID.Value = this._UserShopID.ToString();
                this.txtExpenseTime.Value = DateTime.Now.ToString();
                this.chkAllowPwd.Checked = base.curParameter.bolPwd;
                this.chkStaff.Checked = base.curParameter.bolStaff;
                this.txtStaffType.Value = base.curParameter.bolStaffType.ToString();
                PubFunction.Get_PrintTitle(ref this.lblPrintTitle, ref this.lblPrintFoot, base._UserShopID);
                this.PointNum.Value = PubFunction.GetPointNum("JCXF");
            }
        }
    }
}

