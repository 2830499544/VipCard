using Chain.BLL;
using ChainStock.Controls;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.Report
{
	public class RptPointRankList : PageBase
	{
		protected HtmlForm frmPointRankList;

		protected Literal ltlTitle;

		protected Button btnPointRankExcel;

		protected HtmlInputText txtQueryMem;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected Button btnUserSearch;

		protected Repeater gvPointRankList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch2;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				if (PubFunction.curParameter.dataAuthority != 0)
				{
					if (this._UserShopID > 1)
					{
						this.sltShop.Value = this._UserShopID.ToString();
						this.sltShop.Attributes.Add("disabled", "disabled");
					}
				}
				this.Get_ParameterList(this.QueryCondition());
			}
		}

		protected void btnUserSearch_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.Get_ParameterList(this.QueryCondition());
		}

		private void Get_ParameterList(string strSql)
		{
			Mem bllMem = new Mem();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += "and Mem.MemShopID = SysShop.ShopID and Mem.MemLevelID = MemLevel.LevelID and MemPoint>0 ";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "MemShopID", strSql);
			DataTable db = bllMem.GetPointRankList(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvPointRankList.DataSource = db;
			this.gvPointRankList.DataBind();
			PageBase.BindSerialRepeater(this.gvPointRankList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.Get_ParameterList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value;
			string strMemLevelID = this.sltMemLevelID.Value;
			string strMemShopID = this.sltShop.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1");
			if (strQueryMem != "")
			{
				strSql.AppendFormat("and (MemCard ='{0}' or MemName like '%{0}%' or MemMobile='{0}' or MemCardNumber = '{0}' )", strQueryMem);
			}
			if (strMemLevelID != "")
			{
				strSql.AppendFormat("and Mem.MemLevelID={0}", int.Parse(strMemLevelID));
			}
			if (strMemShopID != "")
			{
				strSql.AppendFormat("and MemShopID={0}", int.Parse(strMemShopID));
			}
			return strSql.ToString();
		}

		protected void btnPointRankExcel_Click(object sender, EventArgs e)
		{
			Mem bllMem = new Mem();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += "and Mem.MemShopID = SysShop.ShopID and Mem.MemLevelID = MemLevel.LevelID and MemPoint>0 ";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "MemShopID", strSql);
			DataTable dtPoint = bllMem.GetPointRankList(100000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.PointRankReportExcel(dtPoint, this._UserName);
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Get_ParameterList(this.QueryCondition());
		}
	}
}
