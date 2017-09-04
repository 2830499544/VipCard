using Chain.BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.ExtraService
{
	public class ScreenPopUp : PageBase
	{
		protected HtmlForm frmScreenPopUp;

		protected Literal ltlTitle;

		protected HtmlSelect sltCallerIsMem;

		protected HtmlSelect sltCallerState;

		protected Button btnCallerQuery;

		protected Repeater gvScreenPopUp;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.GetScreenPopUpList(this.QueryCondition());
			}
		}

		public void GetScreenPopUpList(string strSql)
		{
			Chain.BLL.ScreenPopUp bllScreenPopUp = new Chain.BLL.ScreenPopUp();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "CallerShopID", strSql);
			DataTable dt = bllScreenPopUp.GetScreenPopUpList(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvScreenPopUp.DataSource = dt;
			this.gvScreenPopUp.DataBind();
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.GetScreenPopUpList(this.QueryCondition());
		}

		protected void btnCallerQuery_Click(object sender, EventArgs e)
		{
			this.GetScreenPopUpList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strCallerIsMem = this.sltCallerIsMem.Value;
			string strCallerState = this.sltCallerState.Value;
			if (strCallerIsMem == "1")
			{
				strCallerIsMem = "会员";
			}
			else if (strCallerIsMem == "2")
			{
				strCallerIsMem = "非会员";
			}
			if (strCallerState == "1")
			{
				strCallerState = "未接来电";
			}
			else if (strCallerState == "2")
			{
				strCallerState = "已接来电";
			}
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" 1=1 ");
			if (strCallerIsMem != "-1")
			{
				strSql.AppendFormat(" and CallerIsMem='{0}'", strCallerIsMem);
			}
			if (strCallerState != "-1")
			{
				strSql.AppendFormat(" and CallerState='{0}'", strCallerState);
			}
			return strSql.ToString();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.GetScreenPopUpList(this.QueryCondition());
		}
	}
}
