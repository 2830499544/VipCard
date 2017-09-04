using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.business
{
	public class timesConsumption : Page
	{
		protected HtmlGenericControl spOrderAccount;

		protected HtmlGenericControl spMemName;

		protected HtmlInputHidden txtMemID;

		protected HtmlInputHidden txtShopID;

		protected HtmlInputHidden txtUserID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["userid"] != null)
				{
					Chain.BLL.SysUser bllUser = new Chain.BLL.SysUser();
					Chain.Model.SysUser modelUser = bllUser.GetModel(int.Parse(this.Session["userid"].ToString()));
					Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(modelUser.UserShopID);
					this.txtShopID.Value = modelUser.UserShopID.ToString();
					this.txtUserID.Value = modelUser.UserID.ToString();
					this.spOrderAccount.InnerHtml = "wxJC" + DateTime.Now.ToString("yyMMddHHmmssffff");
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}
	}
}
