using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.Member
{
	public class Timingrules : PageBase
	{
		private Chain.BLL.Timingrules bllTimingrules = new Chain.BLL.Timingrules();

		protected HtmlForm form1;

		protected HtmlInputText txtRulesName;

		protected HtmlInputText txtRulesInterval;

		protected HtmlInputText txtRulesUnitPrice;

		protected HtmlInputText txtRulesExceedTime;

		protected Literal ltlTitle;

		protected HtmlInputButton btnAddTimingRules;

		protected Repeater gvTimingrules;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.GetMemList();
			}
		}

		private void GetMemList()
		{
			string strSql = string.Format("Timingrules.RulesUserID = SysUser.UserID AND RulesShopID = {0}", this._UserShopID);
			int Counts = this.NetPagerParameter.RecordCount;
			DataTable dtMem = this.bllTimingrules.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			this.NetPagerParameter.RecordCount = Counts;
			this.NetPagerParameter.CustomInfoHTML = string.Format("<div class=\"results\"><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[]
			{
				this.NetPagerParameter.CurrentPageIndex,
				this.NetPagerParameter.PageCount,
				this.NetPagerParameter.RecordCount,
				this.NetPagerParameter.PageSize
			});
			this.gvTimingrules.DataSource = dtMem;
			this.gvTimingrules.DataBind();
			PageBase.BindSerialRepeater(this.gvTimingrules, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.GetMemList();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.GetMemList();
		}
	}
}
