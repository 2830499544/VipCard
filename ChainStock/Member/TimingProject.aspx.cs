using Chain.BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.Member
{
	public class TimingProject : PageBase
	{
		protected HtmlForm form1;

		protected HtmlSelect sltProjectRulesID;

		protected Literal ltlTitle;

		protected HtmlInputButton btnTimingProject;

		protected HtmlInputText txtQuerProjectName;

		protected Button btnUserSearch;

		protected Repeater gvTimingProject;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		private Chain.BLL.TimingProject bllTimingProject = new Chain.BLL.TimingProject();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.bindTimingrules();
				this.GetMemList(this.QueryCondition());
			}
		}

		private void bindTimingrules()
		{
			Chain.BLL.Timingrules bllTimingrules = new Chain.BLL.Timingrules();
			DataTable dtTimingrules = bllTimingrules.GetList(string.Format("RulesShopID = {0}", this._UserShopID)).Tables[0];
			this.sltProjectRulesID.Items.Add(new ListItem("请选择", ""));
			foreach (DataRow dr in dtTimingrules.Rows)
			{
				this.sltProjectRulesID.Items.Add(new ListItem(dr["RulesName"].ToString(), dr["RulesID"].ToString()));
			}
		}

		public string QueryCondition()
		{
			StringBuilder sbdSql = new StringBuilder();
			sbdSql.Append("1 = 1");
			if (this.txtQuerProjectName.Value != "")
			{
				sbdSql.AppendFormat(" AND TimingProject.ProjectName = '{0}'", this.txtQuerProjectName.Value);
			}
			return sbdSql.ToString();
		}

		private void GetMemList(string strSql)
		{
			strSql += string.Format(" AND TimingProject.ProjectRulesID = Timingrules.RulesID AND ProjectUserID = SysUser.UserID AND ProjectShopID = {0}", this._UserShopID);
			int Counts = this.NetPagerParameter.RecordCount;
			DataTable dtTimingProject = this.bllTimingProject.GetListSP(0, this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvTimingProject.DataSource = dtTimingProject;
			this.gvTimingProject.DataBind();
			PageBase.BindSerialRepeater(this.gvTimingProject, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.GetMemList(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.GetMemList(this.QueryCondition());
		}

		protected void btnUserSearch_Click1(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.GetMemList(this.QueryCondition());
		}
	}
}
