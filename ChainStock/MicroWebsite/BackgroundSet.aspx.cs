using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.MicroWebsite
{
	public class BackgroundSet : PageBase
	{
		protected HtmlForm frmMemberExplanation;

		protected Literal ltlTitle;

		protected HtmlImage imgBackgroundPhoto;

		protected HtmlInputHidden txtUpdateBackgroundName;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.imgBackgroundPhoto.Src = "../Upload/WeiXin/Images/wxindex_bg.png";
				this.txtUpdateBackgroundName.Value = "../Upload/WeiXin/Images/wxindex_bg.png";
			}
		}
	}
}
