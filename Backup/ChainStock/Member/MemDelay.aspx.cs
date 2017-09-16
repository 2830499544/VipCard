using ChainStock.Controls;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class MemDelay : PageBase
	{
		protected HtmlForm frmMemDelay;

		protected Literal ltlTitle;

		protected MemberSearch ucMemberSearch;

		protected HtmlInputCheckBox chkIsPast;

		protected HtmlTextArea txDelayRemark;

		protected HtmlInputHidden MemCard;

		protected HtmlInputHidden IsYongJiu;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["MemCard"] != null)
				{
					this.MemCard.Value = base.Request.QueryString["MemCard"];
				}
				this.chkIsPast.Checked = PubFunction.curParameter.bolPastTime;
				this.ucMemberSearch.BolSenseICCard = this.curParameter.bolSenseiccard;
				this.ucMemberSearch.BolContactICCard = this.curParameter.bolContacticcard;
			}
		}
	}
}
