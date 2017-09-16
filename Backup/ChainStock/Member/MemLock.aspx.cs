using ChainStock.Controls;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class MemLock : PageBase
	{
		protected HtmlForm frmMemLock;

		protected Literal ltlTitle;

		protected MemberSearch ucMemberSearch;

		protected HtmlGenericControl MemCurrentState;

		protected HtmlTextArea txtMemLockRemark;

		protected HtmlInputHidden MemCard;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request.QueryString["MemCard"] != null)
			{
				this.MemCard.Value = base.Request.QueryString["MemCard"];
			}
			this.ucMemberSearch.BolSenseICCard = this.curParameter.bolSenseiccard;
			this.ucMemberSearch.BolContactICCard = this.curParameter.bolContacticcard;
		}
	}
}
