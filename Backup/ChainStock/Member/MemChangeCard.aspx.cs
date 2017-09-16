using ChainStock.Controls;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class MemChangeCard : PageBase
	{
		protected HtmlForm frmChangeCard;

		protected Literal ltlTitle;

		protected MemberSearch ucMemberSearch;

		protected HtmlTableRow trOldPwd;

		protected HtmlInputButton btnSenseICCardInit;

		protected HtmlInputButton btnContactICCardInit;

		protected HtmlInputButton btnSendSenseICCard;

		protected HtmlInputButton btnContactICCard;

		protected HtmlTableRow trNewPwd;

		protected HtmlInputCheckBox RegNullPwd;

		protected HtmlGenericControl lblchkPwd;

		protected HtmlInputCheckBox chkPwd;

		protected HtmlTableRow trReNewPwd;

		protected HtmlTextArea txtCgCardRemark;

		protected HtmlInputHidden MemCard;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["MemCard"] != null)
				{
					this.MemCard.Value = base.Request.QueryString["MemCard"];
				}
				if (!PubFunction.curParameter.bolPwd)
				{
					this.trOldPwd.Visible = PubFunction.curParameter.bolPwd;
				}
				this.RegNullPwd.Checked = PubFunction.curParameter.RegNullPwd;
				if (!this.curParameter.bolSenseiccard)
				{
					this.btnSendSenseICCard.Style.Add("display", "none");
					this.btnSenseICCardInit.Style.Add("display", "none");
				}
				if (!this.curParameter.bolContacticcard)
				{
					this.btnContactICCard.Style.Add("display", "none");
					this.btnContactICCardInit.Style.Add("display", "none");
				}
				this.ucMemberSearch.BolSenseICCard = this.curParameter.bolSenseiccard;
				this.ucMemberSearch.BolContactICCard = this.curParameter.bolContacticcard;
			}
		}
	}
}
