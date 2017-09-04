using Chain.BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.Report
{
	public class RptMemTransferMoney : PageBase
	{
		protected HtmlForm frmMemTransferMoney;

		protected Literal ltlTitle;

		protected HtmlInputText txtMemStartTime;

		protected HtmlInputText txtMemEndTime;

		protected HtmlSelect sltShopChart;

		protected HtmlInputHidden HDsltshop;

		protected Button btnMemDrawMoneyExcel;

		protected HtmlInputText txtQueryMem;

		protected HtmlInputText txtRechargeAccount;

		protected HtmlInputText txtRemark;

		protected Button btnRptMemDrawQuery;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected HtmlSelect sltMoney;

		protected HtmlInputText txtDrawMoney;

		protected Label lblTotalMoney;

		protected HtmlGenericControl report;

		protected Repeater gvRptMemTransferMoney;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.txtDrawMoney.Value = "0";
				this.txtStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtMemStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtMemEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				this.BindDrawMoney();
				this.Get_ParameterList(this.QueryCondition());
			}
		}

		private void BindDrawMoney()
		{
			MemTransferLog bllMemTransferLog = new MemTransferLog();
			string strSql = this.QueryCondition();
			DataTable dtMemTransferLog = bllMemTransferLog.GetMoneyCount(strSql).Tables[0];
			this.lblTotalMoney.Text = decimal.Parse(dtMemTransferLog.Rows[0]["TransferMoney"].ToString()).ToString("0.00");
		}

		private void Get_ParameterList(string strSql)
		{
			MemTransferLog bllMemTransferLog = new MemTransferLog();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " and  MemTransferLog.UserID = SysUser.UserID";
			DataTable db = bllMemTransferLog.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvRptMemTransferMoney.DataSource = db;
			this.gvRptMemTransferMoney.DataBind();
			PageBase.BindSerialRepeater(this.gvRptMemTransferMoney, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindDrawMoney();
			this.Get_ParameterList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value;
			string strMoneySymbols = this.sltMoney.Value;
			string strMoney = (this.txtDrawMoney.Value.Trim() != "") ? this.txtDrawMoney.Value.Trim() : "0";
			decimal dclMoney = decimal.Parse(strMoney);
			string strRechargeAccount = PubFunction.RemoveSpace(this.txtRechargeAccount.Value);
			string strRemark = PubFunction.RemoveSpace(this.txtRemark.Value);
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" 1=1 ");
			if (strQueryMem != "")
			{
				strSql.AppendFormat("and (MemCard = '{0}' or MemName like '%{0}%' or MemMobile = '{0}' or MemCardNumber = '{0}' )", strQueryMem);
			}
			if (dclMoney != 0m)
			{
				strSql.AppendFormat("and TransferMoney" + strMoneySymbols + "{0}", dclMoney);
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat("and TransferCreateTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat("and TransferCreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			if (strRemark != "")
			{
				strSql.AppendFormat(" and TransferRemark like '%{0}%' ", strRemark);
			}
			if (strRechargeAccount != "")
			{
				strSql.AppendFormat(" and TransferAccount='{0}'", strRechargeAccount);
			}
			return strSql.ToString();
		}

		protected void btnRptMemDrawQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.BindDrawMoney();
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindDrawMoney();
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void btnMemTransferMoneyExcel_Click(object sender, EventArgs e)
		{
			MemTransferLog bllMemTransferLog = new MemTransferLog();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += " and  MemTransferLog.UserID = SysUser.UserID";
			DataTable db = bllMemTransferLog.GetListSP(1000000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.MemTransferMoney(db, this._UserName);
		}
	}
}
