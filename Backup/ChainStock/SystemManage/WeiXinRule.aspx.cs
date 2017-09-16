using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class WeiXinRule : PageBase
	{
		protected HtmlForm frmWeiXinRule;

		protected HtmlInputHidden txtNewsID;

		protected HtmlInputHidden txtRuleID;

		protected Literal ltlTitle;

		protected HtmlTextArea txtNewsDesc;

		protected HtmlTextArea txtNoticeDetail;

		protected HtmlInputText txtSystemDomain;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.txtSystemDomain.Value = PubFunction.curParameter.strDoMain;
				if (!string.IsNullOrEmpty(base.Request["NewsID"]))
				{
					this.txtNewsID.Value = base.Request["NewsID"].ToString();
				}
				else
				{
					this.txtNewsID.Value = "0";
					if (!string.IsNullOrEmpty(base.Request["RuleID"]))
					{
						this.txtRuleID.Value = base.Request["RuleID"].ToString();
					}
					else
					{
						this.txtRuleID.Value = "";
					}
				}
			}
		}
	}
}
