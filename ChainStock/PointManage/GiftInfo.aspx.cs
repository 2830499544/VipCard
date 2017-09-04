using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.PointGift
{
	public class GiftInfo : PageBase
	{
		private Chain.Model.PointGift modelPg = new Chain.Model.PointGift();

		private Chain.BLL.PointGift bllPg = new Chain.BLL.PointGift();

		protected HtmlForm frmGiftAdd;

		protected Image imgTitle;

		protected Label lblFrmTitle;

		protected HtmlInputText txtGiftName;

		protected HtmlInputHidden txtGiftID;

		protected HtmlInputText txtGiftCode;

		protected HtmlInputText txtGiftStockNumber;

		protected HtmlInputHidden txtGiftExchangeNumber;

		protected HtmlInputText txtGiftExchangePoint;

		protected HtmlTextArea txtGiftRemark;

		protected HtmlImage imgGiftPhoto;

		protected HtmlInputHidden txtGiftPhoto;

		protected HiddenField HidGiftID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["GiftID"] != "")
				{
					this.HidGiftID.Value = base.Request.QueryString["GiftID"];
				}
				this.Set_GiftInfo();
			}
		}

		public void Set_GiftInfo()
		{
			if (this.HidGiftID.Value != "")
			{
				this.modelPg = this.bllPg.GetModel(int.Parse(this.HidGiftID.Value));
				Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(this.modelPg.GiftShopID);
				this.lblFrmTitle.Text = this.lblFrmTitle.Text + "--" + this.modelPg.GiftName + "  礼品编辑";
				this.txtGiftID.Value = this.modelPg.GiftID.ToString();
				this.txtGiftName.Value = this.modelPg.GiftName;
				this.txtGiftCode.Value = this.modelPg.GiftCode;
				this.txtGiftStockNumber.Value = this.modelPg.GiftStockNumber.ToString();
				this.txtGiftExchangePoint.Value = this.modelPg.GiftExchangePoint.ToString();
				this.txtGiftExchangeNumber.Value = this.modelPg.GiftExchangeNumber.ToString();
				this.txtGiftRemark.Value = this.modelPg.GiftRemark;
			}
			else
			{
				this.lblFrmTitle.Text = this.lblFrmTitle.Text + "--  礼品新增";
			}
		}
	}
}
