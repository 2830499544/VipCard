using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainPoint.PointManage
{
	public class PointRate : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmPointRate;

		protected Literal ltlTitle;

		protected CheckBox chkPointLevel;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.chkPointLevel.Checked = this.curParameter.chkPointLevel;
		}
	}
}
