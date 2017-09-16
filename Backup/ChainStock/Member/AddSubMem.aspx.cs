using ChainStock.Controls;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class AddSubMem : PageBase
	{
		protected HtmlForm frmTransferMoney;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected MemberSearch ucMemberSearch;

		protected HtmlInputButton btnSubMemAdd;

		protected HtmlInputText txtUserType;

		protected HtmlInputText txtSubCardNumber;

		protected HtmlInputText txtSubName;

		protected HtmlInputText txtSubMemMobile;

		protected HtmlInputRadioButton rdusedT;

		protected HtmlInputRadioButton rdusedF;

		protected HtmlInputHidden MemCard;

		protected HtmlInputHidden SubMemID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["MemCard"] != null)
				{
					this.MemCard.Value = base.Request.QueryString["MemCard"];
				}
			}
		}
	}
}
