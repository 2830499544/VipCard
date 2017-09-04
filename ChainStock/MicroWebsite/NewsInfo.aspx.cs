using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.MicroWebsite
{
	public class NewsInfo : PageBase
	{
		protected HtmlForm frmGoodsInfo;

		protected Literal ltlTitle;

		protected HtmlInputText txtNewsName;

		protected HtmlInputCheckBox cbkIsRecommend;

		protected HtmlImage imgNewsPhoto;

		protected HtmlTextArea txtNewsDesc;

		protected HtmlInputText txtNewsRemark;

		protected HtmlInputHidden txtNewsID;

		protected HtmlInputHidden txtUpdateNewsName;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["NewsID"] != null)
				{
					int NewsID = int.Parse(base.Request.QueryString["NewsID"]);
					Chain.BLL.News bllNews = new Chain.BLL.News();
					Chain.Model.News modelNews = bllNews.GetModel(NewsID);
					this.txtNewsDesc.Value = modelNews.NewsDesc;
					this.txtNewsName.Value = modelNews.NewsName;
					this.txtNewsRemark.Value = modelNews.NewsRemark;
					this.imgNewsPhoto.Src = modelNews.NewsPhoto.ToString();
					this.txtUpdateNewsName.Value = modelNews.NewsPhoto;
					if (modelNews.IsRecommend == 1)
					{
						this.cbkIsRecommend.Checked = true;
					}
					else
					{
						this.cbkIsRecommend.Checked = false;
					}
					this.txtNewsID.Value = NewsID.ToString();
					this.ltlTitle.Text = "微官网   >   动态管理   >   动态编辑 ";
				}
				else
				{
					this.ltlTitle.Text = "微官网   >   动态管理   >   动态新增 ";
				}
			}
		}
	}
}
