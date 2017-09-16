using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.MicroWebsite
{
	public class RechargeRule : PageBase
	{
		protected HtmlForm frmRechargeRule;

		protected Literal ltlTitle;

		protected HtmlInputHidden txtRuleID;

		protected HtmlInputButton btnRechargeRuleAdd;

		protected Repeater gvRechargeRuleList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		private Chain.BLL.RechargeRule RechargeRuleBll = new Chain.BLL.RechargeRule();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.Get_ParameterList();
			}
		}

		private void Get_ParameterList()
		{
			int Counts;
			DataTable dt = this.RechargeRuleBll.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
			{
				" CreateUserID=SysUser.UserID "
			}).Tables[0];
			this.NetPagerParameter.RecordCount = Counts;
			this.NetPagerParameter.CustomInfoHTML = string.Format("<div class=\"results\"><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[]
			{
				this.NetPagerParameter.CurrentPageIndex,
				this.NetPagerParameter.PageCount,
				this.NetPagerParameter.RecordCount,
				this.NetPagerParameter.PageSize
			});
			this.gvRechargeRuleList.DataSource = dt;
			this.gvRechargeRuleList.DataBind();
			PageBase.BindSerialRepeater(this.gvRechargeRuleList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Get_ParameterList();
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.Get_ParameterList();
		}
	}
}
