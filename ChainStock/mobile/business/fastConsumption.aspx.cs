using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.business
{
	public class fastConsumption : Page
	{
		protected HtmlGenericControl spOrderAccount;

		protected HtmlGenericControl spMemName;

		protected HtmlInputText money;

		protected HtmlGenericControl discountMoney;

		protected HtmlGenericControl point;

		protected HtmlInputHidden txtUserID;

		protected HtmlInputHidden txtMemID;

		protected HtmlInputHidden txtLevelID;

		protected HtmlInputHidden txtShopID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["userid"] != null)
				{
					this.spOrderAccount.InnerHtml = "wxKS" + DateTime.Now.ToString("yyMMddHHmmssffff");
					Chain.BLL.SysUser bllUser = new Chain.BLL.SysUser();
					Chain.Model.SysUser modelUser = bllUser.GetModel(int.Parse(this.Session["userid"].ToString()));
					Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(modelUser.UserShopID);
					this.txtShopID.Value = modelUser.UserShopID.ToString();
					this.txtUserID.Value = modelUser.UserID.ToString();
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}
	}
}
