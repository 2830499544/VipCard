using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.business
{
	public class bossCenter : Page
	{
		protected HtmlGenericControl title;

		protected HtmlGenericControl spTime;

		protected HtmlInputHidden txtType;

		protected HtmlInputHidden txtShopID;

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
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
				if (base.Request.QueryString["type"] != null)
				{
					this.txtType.Value = base.Request.QueryString["Type"];
				}
				else
				{
					this.txtType.Value = "1";
				}
			}
		}
	}
}
