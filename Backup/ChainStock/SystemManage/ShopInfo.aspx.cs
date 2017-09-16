using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class ShopInfo : PageBase
	{
		protected HtmlForm frmShop;

		protected Image imgTitle;

		protected Label lblFrmTitle;

		protected HtmlInputText txtShopName;

		protected HtmlInputHidden txtShopID;

		protected HtmlInputText txtShopContactMan;

		protected HtmlInputText txtShopTelephone;

		protected HtmlSelect sltShopAreaID;

		protected HtmlInputText txtAreaName;

		protected HtmlInputText txtShopPrintTitle;

		protected HtmlInputText txtShopPrintFoot;

		protected HtmlInputRadioButton radChooseYes;

		protected HtmlInputRadioButton radChooseNo;

		protected HtmlInputText txtShopAddress;

		protected HtmlTextArea txtShopRemark;

		protected HiddenField HidSid;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["SID"] != null)
				{
					this.HidSid.Value = base.Request.QueryString["SID"];
				}
				PubFunction.BindShopArea(this.sltShopAreaID, true);
				this.Set_ShopInfo();
			}
		}

		public void Set_ShopInfo()
		{
			if (this.HidSid.Value != "")
			{
				Chain.Model.SysShop shop = new Chain.BLL.SysShop().GetModel(int.Parse(this.HidSid.Value));
				this.lblFrmTitle.Text = this.lblFrmTitle.Text + "--" + shop.ShopName + "  商家编辑";
				int AreaID = shop.ShopAreaID;
				Chain.Model.SysArea area = new Chain.BLL.SysArea().GetModel(AreaID);
				this.txtShopName.Value = shop.ShopName;
				this.txtShopID.Value = shop.ShopID.ToString();
				this.txtShopContactMan.Value = shop.ShopContactMan;
				this.txtShopTelephone.Value = shop.ShopTelephone;
				this.txtShopAddress.Value = shop.ShopAddress;
				this.txtShopRemark.Value = shop.ShopRemark;
				this.txtShopPrintTitle.Value = shop.ShopPrintTitle;
				this.txtShopPrintFoot.Value = shop.ShopPrintFoot;
				if (shop.ShopState)
				{
					this.radChooseYes.Checked = true;
				}
				else
				{
					this.radChooseNo.Checked = true;
				}
			}
			else
			{
				this.lblFrmTitle.Text = this.lblFrmTitle.Text + "--  商家设置";
			}
		}
	}
}
