using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.Controls
{
	public class QuickSearch : UserControl
	{
		protected HtmlSelect sltAlliance;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden txtShopType;

		protected HtmlInputHidden txtShopID;

		private LoginLogic login;

		private Chain.Model.SysUser UserModel
		{
			get
			{
				if (this.login == null)
				{
					this.login = LoginLogic.LoginStatus();
				}
				Chain.Model.SysUser result;
				if (this.login.IsLoggedOn && this.login.LoginUser != null)
				{
					result = this.login.LoginUser;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.txtShopID.Value = this.UserModel.UserShopID.ToString();
				Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
				Chain.Model.SysShop modelShop = bllShop.GetModel(this.UserModel.UserShopID);
				this.txtShopType.Value = modelShop.ShopType.ToString();
				PubFunction.BindAllianceListSelect(this.UserModel.UserShopID, this.sltAlliance, true);
				int FatherShopID = -1;
				if (this.sltAlliance.Value != "")
				{
					FatherShopID = int.Parse(this.sltAlliance.Value);
				}
				PubFunction.BindShopListSelect(this.UserModel.UserShopID, FatherShopID, this.sltShop, true);
			}
		}
	}
}
