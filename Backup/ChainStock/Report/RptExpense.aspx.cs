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
	public class RptExpense : PageBase
	{
		protected HtmlForm frmRptExpense;

		protected Image imgTitle;

		protected Label lblFrmTitle;

		protected HtmlInputText txtQueryMem;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected HtmlSelect sltTotalMoney;

		protected HtmlInputText txtTotalMoney;

		protected Button btnRptExpenseQuery;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltShop;

		protected HtmlSelect sltDiscountMoney;

		protected HtmlInputText txtDiscountMoney;

		protected Button btnRptExpenseExcel;

		protected Label lblToTalToday;

		protected Label lblToTalWeek;

		protected Label lblToTalMonth;

		protected Label lblTotal;

		protected Label lblDiscountToday;

		protected Label lblDiscountWeek;

		protected Label lblDiscountMonth;

		protected Label lblDiscountTotal;

		protected GridView gvRptExpense;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				this.txtTotalMoney.Value = "0";
				this.txtDiscountMoney.Value = "0";
				this.Get_ParameterList(this.QueryCondition());
			}
		}

		private void BindExpenseMoney()
		{
			OrderLog bllOrder = new OrderLog();
			string sbWhere = this.QueryCondition();
			sbWhere += "and OrderLog.OrderShopID = SysShop.ShopID and OrderLog.OrderMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and OrderLog.OrderUserID = SysUser.UserID";
			sbWhere = PubFunction.GetShopAuthority(this._UserShopID, "OrderShopID", sbWhere);
			string strMoney = bllOrder.GetTotalMoney(sbWhere + " and datediff(day,OrderCreateTime,getdate())=0").ToString("f2");
			this.lblToTalToday.Text = strMoney;
			strMoney = bllOrder.GetTotalMoney(sbWhere + " and datediff(week,OrderCreateTime,getdate())=0").ToString("f2");
			this.lblToTalWeek.Text = strMoney;
			strMoney = bllOrder.GetTotalMoney(sbWhere + " and datediff(month,OrderCreateTime,getdate())=0").ToString("f2");
			this.lblToTalMonth.Text = strMoney;
			strMoney = bllOrder.GetTotalMoney(sbWhere).ToString("f2");
			this.lblTotal.Text = strMoney;
			strMoney = bllOrder.GetDiscountMoney(sbWhere + " and datediff(day,OrderCreateTime,getdate())=0").ToString("f2");
			this.lblDiscountToday.Text = strMoney;
			strMoney = bllOrder.GetDiscountMoney(sbWhere + " and datediff(week,OrderCreateTime,getdate())=0").ToString("f2");
			this.lblDiscountWeek.Text = strMoney;
			strMoney = bllOrder.GetDiscountMoney(sbWhere + " and datediff(month,OrderCreateTime,getdate())=0").ToString("f2");
			this.lblDiscountMonth.Text = strMoney;
			strMoney = bllOrder.GetDiscountMoney(sbWhere).ToString("f2");
			this.lblDiscountTotal.Text = strMoney;
		}

		private void Get_ParameterList(string strSql)
		{
			OrderLog bllOrder = new OrderLog();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += "and OrderLog.OrderShopID = SysShop.ShopID and OrderLog.OrderMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and OrderLog.OrderUserID = SysUser.UserID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "OrderShopID", strSql);
			DataTable db = bllOrder.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvRptExpense.DataSource = db;
			this.gvRptExpense.DataBind();
			PageBase.BindSerialGridView(this.gvRptExpense, false, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
			PageBase.BindNullSGridView(this.gvRptExpense);
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
			string strTotalSymbols = this.sltTotalMoney.Value;
			string strTotalMoney = (this.txtTotalMoney.Value.Trim() != "") ? this.txtTotalMoney.Value.Trim() : "0";
			decimal dclTotalMoney = decimal.Parse(strTotalMoney);
			string strDiscountSymbols = this.sltDiscountMoney.Value;
			string strDiscountMoney = (this.txtDiscountMoney.Value.Trim() != "") ? this.txtDiscountMoney.Value.Trim() : "0";
			decimal dclDiscountMoney = decimal.Parse(strDiscountMoney);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1");
			if (strQueryMem != "")
			{
				strSql.AppendFormat("and (MemCard = '{0}' or MemName like '%{0}%' or MemMobile = '{0}' or MemCardNumber = '{0}')", strQueryMem);
			}
			if (strMemLevelID != "")
			{
				strSql.AppendFormat("and Mem.MemLevelID={0}", int.Parse(strMemLevelID));
			}
			if (strMemShopID != "")
			{
				strSql.AppendFormat("and OrderShopID={0}", int.Parse(strMemShopID));
			}
			if (dclTotalMoney != 0m)
			{
				strSql.AppendFormat("and OrderTotalMoney" + strTotalSymbols + "{0}", dclTotalMoney);
			}
			if (dclDiscountMoney != 0m)
			{
				strSql.AppendFormat("and OrderDiscountMoney" + strDiscountSymbols + "{0}", dclDiscountMoney);
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat("and OrderCreateTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat("and OrderCreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			return strSql.ToString();
		}

		protected void btnRptExpenseQuery_Click(object sender, EventArgs e)
		{
			this.BindExpenseMoney();
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void btnRptExpenseExcel_Click(object sender, EventArgs e)
		{
			OrderLog bllOrder = new OrderLog();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += "and OrderLog.OrderShopID = SysShop.ShopID and OrderLog.OrderMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and OrderLog.OrderUserID = SysUser.UserID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "OrderShopID", strSql);
			DataTable dtExpense = bllOrder.GetListSP(100000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.RptExpenseReportExcel(dtExpense, this._UserName);
		}

		protected void gvRptExpense_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				if (e.Row.Cells[2].Text == "0")
				{
					e.Row.Cells[2].Text = "无";
				}
				e.Row.Cells[7].Style.Add("word-break", "break-all");
			}
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Get_ParameterList(this.QueryCondition());
		}
	}
}
