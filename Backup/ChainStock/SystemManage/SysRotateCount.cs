using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.SystemManage
{
	public class SysRotateCount : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmStaffClassList;

		protected Literal ltlTitle;

		protected HtmlInputText txtCostAmount;

		protected HtmlInputHidden txtID;

		protected HtmlInputHidden txtRotateID;

		protected HtmlInputText txtRotateCount;

		protected HtmlInputText txtRotateStartTime;

		protected HtmlInputText txtRotateEndTime;

		protected HtmlInputButton btnSysRotateCountAdd;

		protected Repeater gvSysRotateCountList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["RotateID"] != null)
				{
					this.txtRotateID.Value = base.Request.QueryString["RotateID"];
				}
				this.GetStaffClassList(this.QueryCondition());
			}
		}

		private void GetStaffClassList(string strSql)
		{
			Chain.BLL.SysRotateCount bllStaffClass = new Chain.BLL.SysRotateCount();
			int Counts = this.NetPagerParameter.RecordCount;
			DataTable dtStaffClass = bllStaffClass.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvSysRotateCountList.DataSource = dtStaffClass;
			this.gvSysRotateCountList.DataBind();
			PageBase.BindSerialRepeater(this.gvSysRotateCountList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.GetStaffClassList(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.GetStaffClassList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strSql = " 1=1 ";
			if (this.txtRotateID.Value != "")
			{
				strSql = strSql + " and RotateID=" + int.Parse(base.Request.QueryString["RotateID"]);
			}
			return strSql;
		}

		protected void btnStaffClassQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.GetStaffClassList(this.QueryCondition());
		}
	}
}
