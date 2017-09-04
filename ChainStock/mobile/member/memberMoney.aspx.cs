using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.member
{
	public class memberMoney : Page
	{
		protected HtmlGenericControl spMemName;

		protected HtmlGenericControl spElseMoney;

		protected HtmlGenericControl spMoney;

		protected HtmlGenericControl spRate;

		protected HtmlGenericControl spMaxMoney;

		protected HtmlInputHidden txtMemID;

		protected HtmlInputHidden txtGiveMemID;

		protected HtmlInputHidden txtRate;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					this.txtMemID.Value = MemID.ToString();
					this.BindMemInfo(MemID);
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		private void BindMemInfo(int MemID)
		{
			Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModel(MemID);
			decimal maxmoney = 0m;
			if (modelMem.MemMoney > 0m)
			{
				maxmoney = modelMem.MemMoney * decimal.Parse(0.99.ToString());
			}
			this.spMaxMoney.InnerHtml = maxmoney.ToString("#0.00");
			this.txtRate.Value = "0.01";
			this.spRate.InnerHtml = PubFunction.curParameter.dclGiveMemMoneyRate.ToString() + "%";
			this.txtRate.Value = (PubFunction.curParameter.dclGiveMemMoneyRate / 100m).ToString();
		}
	}
}
