using Chain.BLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.website
{
	public class active : Page
	{
		protected Repeater rptActive;

		protected HtmlAnchor moreActive;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				Promotions bllPromotions = new Promotions();
				DataTable dt;
				if (base.Request.QueryString["type"] == "all")
				{
					dt = bllPromotions.GetList(" 1=1 ").Tables[0];
				}
				else
				{
					dt = bllPromotions.GetList(10, " 1=1 ", "PromotionsTime").Tables[0];
				}
				this.rptActive.DataSource = dt;
				this.rptActive.DataBind();
				if (dt.Rows.Count < 10 || base.Request.QueryString["type"] == "all")
				{
					this.moreActive.Attributes.Add("style", "display:none");
				}
				else
				{
					this.moreActive.Attributes.Add("style", "");
				}
			}
		}
	}
}
