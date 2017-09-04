using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.website
{
	public class newsDetail : Page
	{
		private Chain.BLL.News bllNews = new Chain.BLL.News();

		protected HtmlGenericControl spNewsName;

		protected HtmlGenericControl spNewsCreateTime;

		protected HtmlGenericControl spNewsDesc;

		protected Repeater rptNews;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["NewsID"] != null)
				{
					int ProductID = int.Parse(base.Request.QueryString["NewsID"]);
					Chain.Model.News modelNews = this.bllNews.GetModel(ProductID);
					this.spNewsDesc.InnerHtml = modelNews.NewsDesc;
					this.spNewsCreateTime.InnerHtml = modelNews.NewsCreateTime.ToString("yyyy-MM-dd HH:mm:ss");
					this.spNewsName.InnerText = modelNews.NewsName;
				}
				this.rptNewsBind();
			}
		}

		private void rptNewsBind()
		{
			DataTable dt = this.bllNews.GetList(4, " IsRecommend=1 ", "NewsCreateTime").Tables[0];
			this.rptNews.DataSource = dt;
			this.rptNews.DataBind();
		}
	}
}
