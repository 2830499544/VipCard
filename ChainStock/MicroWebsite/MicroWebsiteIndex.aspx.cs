using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.MicroWebsite
{
	public class MicroWebsiteIndex : PageBase
	{
		protected Random r = new Random();

		private MerchantSite merchantSiteBll = new MerchantSite();

		protected HtmlForm frmMicroWebsiteIndex;

		protected Literal ltlTitle;

		protected HtmlInputText txtMerchantDesc;

		protected HtmlTextArea txtMerchantRemark;

		protected HtmlImage imgModulePhoto;

		protected Repeater gvwMicroWebsiteIndex;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindMicroWebsiteIndex();
			}
		}

		private void BindMicroWebsiteIndex()
		{
			DataTable dt = this.merchantSiteBll.GetAllList().Tables[0];
			this.gvwMicroWebsiteIndex.DataSource = dt;
			this.gvwMicroWebsiteIndex.DataBind();
			PageBase.BindSerialRepeater(this.gvwMicroWebsiteIndex, 0);
		}
	}
}
