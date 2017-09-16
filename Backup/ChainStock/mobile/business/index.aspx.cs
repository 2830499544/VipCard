using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.business
{
	public class index : Page
	{
		private Chain.BLL.SysUser userBll = new Chain.BLL.SysUser();

		private Chain.BLL.Mem bllMem = new Chain.BLL.Mem();

		private Chain.BLL.MemRecharge bllMemRecharge = new Chain.BLL.MemRecharge();

		protected HtmlGenericControl spShopName;

		protected HtmlGenericControl spMemToday;

		protected HtmlGenericControl spMoneyToday;

		protected HtmlGenericControl liExpense2;

		protected HtmlGenericControl liExpense;

		protected HtmlGenericControl liExpense1;

		protected HtmlGenericControl liExpense3;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.Session["userid"] != null)
			{
				if (this.Session["userid"].ToString() != "")
				{
					Chain.Model.SysUser userModel = this.userBll.GetModel(int.Parse(this.Session["userid"].ToString()));
					Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(userModel.UserShopID);
					string strMemToday = "CONVERT(varchar(10),MemCreateTime,120) = CONVERT(varchar(10),GETDATE(),120) AND MemID > 0";
					string strMoneyToday = "CONVERT(varchar(10),RechargeCreateTime,120) = CONVERT(varchar(10),GETDATE(),120)";
					strMemToday = PubFunction.GetShopAuthority(modelShop.ShopID, "MemShopID", strMemToday);
					strMoneyToday = PubFunction.GetShopAuthority(modelShop.ShopID, "RechargeShopID", strMoneyToday);
					this.spShopName.InnerHtml = modelShop.ShopName;
					this.spMemToday.InnerHtml = this.bllMem.GetRecordCount(strMemToday).ToString();
					this.spMoneyToday.InnerHtml = this.bllMemRecharge.GetRecMoney(strMoneyToday).ToString("F2");
					if (modelShop.ShopType == 3)
					{
						this.liExpense.Visible = true;
						this.liExpense1.Visible = true;
						this.liExpense2.Visible = true;
					}
					else
					{
						this.liExpense2.Visible = false;
						this.liExpense.Visible = false;
						this.liExpense1.Visible = false;
					}
					if (modelShop.ShopType == 2)
					{
						this.liExpense3.Visible = false;
					}
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
			else
			{
				base.Response.Redirect("login.aspx");
			}
		}
	}
}
