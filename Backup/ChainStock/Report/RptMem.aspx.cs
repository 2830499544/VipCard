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
	public class RptMem : PageBase
	{
		protected HtmlForm frmRptMem;

		protected Literal ltlTitle;

		protected HtmlInputText txtMemStartTime;

		protected HtmlInputText txtMemEndTime;

		protected HtmlSelect sltShopChart;

		protected HtmlInputHidden HDsltshop;

		protected Button btnRptMemExcel;

		protected HtmlInputText txtQueryMem;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltShop;

		protected HtmlInputText txtDay;

		protected Button btnRptMemQuery;

		protected Label lblMemCount;

		protected Label lblMemMoney;

		protected Label lblMemPoint;

		protected Repeater gvRptMem;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShopChart, true);
				if (PubFunction.curParameter.dataAuthority != 0)
				{
					if (this._UserShopID > 1)
					{
						this.sltShop.Value = this._UserShopID.ToString();
						this.sltShop.Attributes.Add("disabled", "disabled");
					}
				}
				this.txtMemStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtMemEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				this.BindMember();
				this.Get_ParameterList(this.QueryCondition());
			}
		}

		private void BindMember()
		{
			DataTable dtMem = this.GetListAllData();
			this.lblMemCount.Text = dtMem.Rows.Count.ToString();
			if (dtMem.Rows.Count > 0)
			{
				this.lblMemMoney.Text = decimal.Parse(dtMem.Compute("Sum(MemMoney)", "").ToString()).ToString("f2");
				this.lblMemPoint.Text = decimal.Parse(dtMem.Compute("Sum(MemPoint)", "").ToString()).ToString("f2");
			}
		}

		protected void btnRptMemQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.BindMember();
			this.Get_ParameterList(this.QueryCondition());
		}

		private void Get_ParameterList(string strSql)
		{
			Mem bllMem = new Mem();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " and Mem.MemShopID = SysShop.ShopID and Mem.MemLevelID = MemLevel.LevelID and Mem.MemUserID = SysUser.UserID";
			strSql += " and Mem.MemShopID =SysShopMemLevel.ShopID and SysShopMemLevel.MemLevelID=MemLevel.LevelID ";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "MemShopID", strSql);
			DataTable db = bllMem.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvRptMem.DataSource = db;
			this.gvRptMem.DataBind();
			PageBase.BindSerialRepeater(this.gvRptMem, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindMember();
			this.Get_ParameterList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value;
			string strMemLevelID = this.sltMemLevelID.Value;
			string strMemShopID = this.sltShop.Value;
			string strDay = this.txtDay.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1 and MemID >0 ");
			if (strQueryMem != "")
			{
				strSql.AppendFormat(" and (MemCard ='{0}' or MemName like '%{0}%' or MemMobile='{0}' or MemCardNumber = '{0}' )", strQueryMem);
			}
			if (strMemLevelID != "")
			{
				strSql.AppendFormat(" and Mem.MemLevelID='{0}'", int.Parse(strMemLevelID));
			}
			if (strMemShopID != "")
			{
				strSql.AppendFormat(" and MemShopID='{0}'", int.Parse(strMemShopID));
			}
			if (strDay != "")
			{
				strSql.AppendFormat(" and MemConsumeLastTime<=(getdate()-{0})", strDay);
			}
			return strSql.ToString();
		}

		protected void btnRptMemExcel_Click(object sender, EventArgs e)
		{
			DataTable dtMem = this.GetListAllData();
			DataExcelInfo.RptMemReportExcel(dtMem, this._UserName);
		}

		protected DataTable GetListAllData()
		{
			Mem bllMem = new Mem();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += "and Mem.MemShopID = SysShop.ShopID and Mem.MemLevelID = MemLevel.LevelID and Mem.MemUserID = SysUser.UserID";
			strSql += " and Mem.MemShopID =SysShopMemLevel.ShopID and SysShopMemLevel.MemLevelID=MemLevel.LevelID ";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "MemShopID", strSql);
			return bllMem.GetListSP(1000000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindMember();
			this.Get_ParameterList(this.QueryCondition());
		}
	}
}
