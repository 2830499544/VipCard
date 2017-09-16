using Chain.BLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.website
{
	public class news : Page
	{
		protected Repeater rptNews;

		protected HtmlAnchor moreNews;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				News bllNews = new News();
				DataTable dt;
				if (base.Request.QueryString["type"] == "all")
				{
					dt = bllNews.GetList(" 1=1 ").Tables[0];
				}
				else
				{
					dt = bllNews.GetList(10, " 1=1 ", "NewsCreateTime").Tables[0];
				}
				this.rptNews.DataSource = dt;
				this.rptNews.DataBind();
				if (dt.Rows.Count < 10 || base.Request.QueryString["type"] == "all")
				{
					this.moreNews.Attributes.Add("style", "display:none");
				}
			}
		}
	}
}
