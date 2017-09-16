using ChainStock.Controls;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class MemChangePwd : PageBase
	{
		protected HtmlForm frmChangePwd;

		protected HtmlInputHidden hdisOldPwd;

		protected Literal ltlTitle;

		protected MemberSearch ucMemberSearch;

		protected HtmlTableRow trOldPwd;

		protected HtmlInputCheckBox RegNullPwd;

		protected HtmlTextArea txtChangePwdRemark;

		protected HtmlInputCheckBox chkIsOldPwd;

		protected HtmlInputHidden MemCard;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["MemCard"] != null)
				{
					this.MemCard.Value = base.Request.QueryString["MemCard"];
				}
				this.RegNullPwd.Checked = PubFunction.curParameter.RegNullPwd;
				if (!PubFunction.curParameter.IsEditPwdNeedOldPwd)
				{
					this.trOldPwd.Attributes.Add("style", "display:none;");
				}
				this.hdisOldPwd.Value = PubFunction.curParameter.IsEditPwdNeedOldPwd.ToString();
				this.ucMemberSearch.BolSenseICCard = this.curParameter.bolSenseiccard;
				this.ucMemberSearch.BolContactICCard = this.curParameter.bolContacticcard;
			}
		}
	}
}
