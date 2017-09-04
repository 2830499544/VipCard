using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.StockManage
{
	public class GoodsIn : PageBase
	{
		protected HtmlForm frmGoodsIn;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected HtmlInputButton btnGoodsSearch;

		protected HtmlGenericControl spGoodsAccounte;

		protected HtmlGenericControl lblUSer;

		protected HtmlInputText txtCreteTime;

		protected HtmlSelect sltShop;

		protected HtmlGenericControl lblTotalNumber;

		protected HtmlGenericControl lblTotalMoney;

		protected HtmlInputCheckBox chkPrint;

		protected Label lblPrintTitle;

		protected Label lblPrintFoot;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, this._UserShopID, this._UserShopID != 1);
				this.chkPrint.Checked = this.curParameter.bolAutoPrint;
				this.sltShop.Value = this._UserShopID.ToString();
				this.lblUSer.InnerText = this._UserName;
				this.spGoodsAccounte.InnerText = this.curParameter.strGoodsInPrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
				this.txtCreteTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				PubFunction.Get_PrintTitle(ref this.lblPrintTitle, ref this.lblPrintFoot, this._UserShopID);
				if (this._UserShopID != 1)
				{
					this.sltShop.Attributes.Add("disabled", "disabled");
				}
				this.PointNum.Value = PubFunction.GetPointNum("SPRK");
			}
		}
	}
}
