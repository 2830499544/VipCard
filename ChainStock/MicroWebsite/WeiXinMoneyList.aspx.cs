using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.MicroWebsite
{
	public class WeiXinMoneyList : PageBase
	{
		private WeiXinMoney bllMoney = new WeiXinMoney();

		protected HtmlForm frmPromotions;

		protected Literal ltlTitle;

		protected HtmlSelect sltPromotionsLevel;

		protected Repeater rptMoneyList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.Get_ParameterList();
			}
		}

		public decimal BindMoney(object moneyID)
		{
			WeiXinGiveMoney bllWeiXinGiveMoney = new WeiXinGiveMoney();
			return bllWeiXinGiveMoney.GetMoneySum(" MoneyID=" + int.Parse(moneyID.ToString()));
		}

		private void Get_ParameterList()
		{
			int Counts;
			DataTable dt = this.bllMoney.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[0]).Tables[0];
			this.NetPagerParameter.RecordCount = Counts;
			this.NetPagerParameter.CustomInfoHTML = string.Format("<div class=\"results\"><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[]
			{
				this.NetPagerParameter.CurrentPageIndex,
				this.NetPagerParameter.PageCount,
				this.NetPagerParameter.RecordCount,
				this.NetPagerParameter.PageSize
			});
			this.rptMoneyList.DataSource = dt;
			this.rptMoneyList.DataBind();
			PageBase.BindSerialRepeater(this.rptMoneyList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
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
