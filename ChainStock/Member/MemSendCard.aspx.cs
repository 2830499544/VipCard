using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class MemSendCard : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmDataExcelIn;

		protected Literal ltMainIndex;

		protected Literal ltlTitle;

		protected HtmlGenericControl pw1;

		protected HtmlInputHidden YesOrNoPwd;

		protected HtmlGenericControl pw2;

		protected HtmlSelect sltMemLevelID;

		protected HtmlTextArea txtDelCard;

		protected HtmlTextArea txtRemark;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, false);
				if (PubFunction.curParameter.RegNullPwd)
				{
					this.pw1.Attributes.Add("style", "display:none;");
					this.pw2.Attributes.Add("style", "display:none;");
					this.YesOrNoPwd.Value = "1";
				}
			}
		}
	}
}
