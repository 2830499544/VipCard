using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.MicroWebsite
{
	public class WeiXinGiveMoneyList : PageBase
	{
		protected HtmlForm frmPromotions;

		protected Literal ltlTitle;

		protected HtmlSelect sltPromotionsLevel;

		protected Button btnGoodsExpenseExcel;

		protected Repeater rptMoneyList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		private WeiXinGiveMoney bllMoney = new WeiXinGiveMoney();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.Get_ParameterList();
			}
		}

		private void Get_ParameterList()
		{
			string strSql = " Mem.MemID=WeiXinGiveMoney.MemID and Mem.MemShopID=SysShop.ShopID";
			if (base.Request.QueryString["MoneyID"] != null)
			{
				strSql = strSql + "  and IsWin=1 and MoneyID=" + int.Parse(base.Request.QueryString["MoneyID"]);
			}
			if (base.Request.QueryString["MemID"] != null)
			{
				strSql = strSql + " and IsWin=1 and WeiXinGiveMoney.MemID=" + int.Parse(base.Request.QueryString["MemID"]);
			}
			int Counts;
			DataTable dt = this.bllMoney.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.rptMoneyList.DataSource = dt;
			this.rptMoneyList.DataBind();
			PageBase.BindSerialRepeater(this.rptMoneyList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void btnWeiXinGiveMoneyExcel_Click(object sender, EventArgs e)
		{
			string strSql = " Mem.MemID=WeiXinGiveMoney.MemID and Mem.MemShopID=SysShop.ShopID";
			if (base.Request.QueryString["MoneyID"] != null)
			{
				strSql = strSql + "  and IsWin=1 and MoneyID=" + int.Parse(base.Request.QueryString["MoneyID"]);
			}
			if (base.Request.QueryString["MemID"] != null)
			{
				strSql = strSql + " and IsWin=1 and WeiXinGiveMoney.MemID=" + int.Parse(base.Request.QueryString["MemID"]);
			}
			int Counts = this.NetPagerParameter.RecordCount;
			DataTable db = this.bllMoney.GetListSP(1000000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.WeiXinGiveMoneyExport(db, this._UserName);
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
