using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.MicroWebsite
{
	public class MemberExplanation : PageBase
	{
		protected HtmlForm frmMemberExplanation;

		protected Literal ltlTitle;

		protected HtmlInputButton btnMemberExplanationAdd;

		protected Repeater gvwMemberExplanation;

		private Chain.BLL.MemberExplanation MemberExplanationBll = new Chain.BLL.MemberExplanation();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				DataTable dt = this.MemberExplanationBll.GetAllList().Tables[0];
				this.gvwMemberExplanation.DataSource = dt;
				this.gvwMemberExplanation.DataBind();
				PageBase.BindSerialRepeater(this.gvwMemberExplanation, 0);
			}
		}
	}
}
