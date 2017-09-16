using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.business
{
	public class memberCard : Page
	{
		private Chain.BLL.SysUser userBll = new Chain.BLL.SysUser();

		protected HtmlForm frmMemRegister;

		protected HtmlSelect sltMemLevelID;

		protected HtmlInputHidden txtMemRecommendID;

		protected HtmlInputHidden txtMemRecommendName;

		protected HtmlInputHidden sltShop;

		protected HtmlInputHidden sltMemUserID;

		protected HtmlInputHidden txtMemCreateTime;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.Session["userid"] != null)
			{
				if (this.Session["userid"].ToString() != "")
				{
					Chain.Model.SysUser userModel = this.userBll.GetModel(int.Parse(this.Session["userid"].ToString()));
					Chain.Model.SysShop shopModel = new Chain.BLL.SysShop().GetModel(userModel.UserShopID);
					PubFunction.BindMemLevelSelect(this.sltMemLevelID, false);
					this.sltShop.Value = shopModel.ShopID.ToString();
					this.sltMemUserID.Value = userModel.UserID.ToString();
					this.txtMemCreateTime.Value = DateTime.Now.ToString();
				}
			}
			else
			{
				base.Response.Redirect("login.aspx");
			}
		}
	}
}
