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
	public class RptMemExpense : PageBase
	{
		private Mem bllMem = new Mem();

		private DateTime timeStart = DateTime.Now;

		private DateTime timeEnd = DateTime.Now;

		protected HtmlForm frmMemExpense;

		protected Literal ltlTitle;

		protected HtmlInputText txtMemStartTime;

		protected HtmlInputText txtMemEndTime;

		protected HtmlInputText txtMemInfo;

		protected HtmlSelect sltShopChart;

		protected Button btnMemExpenseExcel;

		protected HtmlInputText txtQueryMem;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected Button btnMemExpenseSearch;

		protected Label lblTotalNumber;

		protected Label lblTotalMoney;

		protected HtmlGenericControl report;

		protected Repeater rptMemExpenseHistory;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.txtStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShopChart, true);
				this.txtMemStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtMemEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				if (PubFunction.curParameter.dataAuthority != 0)
				{
					if (this._UserShopID > 1)
					{
						this.sltShop.Value = this._UserShopID.ToString();
					}
				}
				this.Get_ParameterList(this.QueryCondition());
				this.BindMemExpenseMoney();
			}
		}

		private void BindMemExpenseMoney()
		{
			OrderLog bllOrder = new OrderLog();
			string sbWhere = this.QueryCondition();
			sbWhere += " and OrderLog.OrderMemID=Mem.MemID";
			if (this.txtStartTime.Value != "")
			{
				this.timeStart = DateTime.Parse(this.txtStartTime.Value);
			}
			object obj = sbWhere;
			sbWhere = string.Concat(new object[]
			{
				obj,
				" and OrderLog.OrderCreateTime>='",
				this.timeStart,
				"'"
			});
			if (this.txtEndTime.Value != "")
			{
				this.timeEnd = Convert.ToDateTime(PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			obj = sbWhere;
			sbWhere = string.Concat(new object[]
			{
				obj,
				" and OrderLog.OrderCreateTime<='",
				this.timeEnd,
				"'"
			});
			if (this._UserShopID != 1)
			{
				sbWhere = PubFunction.GetMemListShopAuthority(this._UserShopID, "MemShopID", sbWhere);
			}
			DataTable dtMem = bllOrder.GetMemExpenseMoney(sbWhere).Tables[0];
			this.lblTotalNumber.Text = dtMem.Rows[0]["TotalNumber"].ToString();
			this.lblTotalMoney.Text = decimal.Parse(dtMem.Rows[0]["TotalMoney"].ToString()).ToString("0.00");
		}

		private void Get_ParameterList(string strSql)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " and Mem.MemShopID=SysShop.ShopID ";
			StringBuilder strSb = new StringBuilder();
			strSb.Append(" 1=1 ");
			if (this.txtStartTime.Value != "")
			{
				this.timeStart = DateTime.Parse(this.txtStartTime.Value);
				strSb.AppendFormat(" and OrderLog.OrderCreateTime>='{0}' ", this.timeStart);
			}
			if (this.txtEndTime.Value != "")
			{
				strSb.AppendFormat(" and OrderLog.OrderCreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			strSql = strSql + " and (select isnull(sum(OrderDiscountMoney),0) from OrderLog where Mem.MemID=OrderLog.OrderMemID and OrderType <> 4 and OrderType <> 5 and OrderType <> 3 and " + strSb.ToString() + ")>0 ";
			DataTable dtExpenseHistory = this.bllMem.GetMemExpense(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, strSb.ToString(), new string[]
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
			this.rptMemExpenseHistory.DataSource = dtExpenseHistory;
			this.rptMemExpenseHistory.DataBind();
			PageBase.BindSerialRepeater(this.rptMemExpenseHistory, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindMemExpenseMoney();
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void BtnMemExpenseExcel_Click(object sender, EventArgs e)
		{
			Mem member = new Mem();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += " and Mem.MemShopID=SysShop.ShopID ";
			StringBuilder strSb = new StringBuilder();
			StringBuilder strDetail = new StringBuilder();
			strSb.Append(" 1=1 ");
			strDetail.Append(" 1=1 ");
			if (this.txtStartTime.Value != "")
			{
				strSb.AppendFormat(" and OrderLog.OrderCreateTime>='{0}'", this.txtStartTime.Value);
				strDetail.AppendFormat(" and OrderCreateTime>='{0}'", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSb.AppendFormat(" and OrderLog.OrderCreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
				strDetail.AppendFormat(" and OrderCreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			DataTable dtMemExpense = this.bllMem.GetMemExpense(1000000, 1, out Counts, strSb.ToString(), new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.MemExpenseReportExcel(dtMemExpense, this._UserName, strDetail.ToString());
		}

		protected string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value;
			string strMemShopID = this.sltShop.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" 1=1 and MemID>0 and Mem.MemConsumeCount>0");
			if (strQueryMem != "")
			{
				strSql.AppendFormat(" and (MemCard ='{0}' or MemName like '%{0}%' or MemMobile='{0}' or MemCardNumber = '{0}' )", strQueryMem);
			}
			if (strMemShopID != "")
			{
				string strwhere = PubFunction.GetMemListShopAuthority(int.Parse(strMemShopID), "MemShopID", " 1=1 ");
				strSql.AppendFormat(" and {0}", strwhere);
			}
			else
			{
				string strwhere = PubFunction.GetMemListShopAuthority(this._UserShopID, "MemShopID", " 1=1 ");
				strSql.AppendFormat(" and {0}", strwhere);
			}
			return strSql.ToString();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindMemExpenseMoney();
			this.Get_ParameterList(this.QueryCondition());
		}

		protected string GetOrderType(int intOrderType)
		{
			string strOrderType = "";
			switch (intOrderType)
			{
			case 0:
				strOrderType = "快速消费";
				break;
			case 1:
				strOrderType = "计时消费";
				break;
			case 2:
				strOrderType = "商品消费";
				break;
			case 3:
				strOrderType = "商品挂单";
				break;
			case 4:
				strOrderType = "消费撤单";
				break;
			case 6:
				strOrderType = "消费退货";
				break;
			case 7:
				strOrderType = "计次消费";
				break;
			}
			return strOrderType;
		}

		protected void BtnMemExpenseSearch_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.BindMemExpenseMoney();
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void rptMemExpenseHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				Repeater rptItem = (Repeater)e.Item.FindControl("rptMemExpenseDetail");
				if (rptItem != null)
				{
					int Count = this.NetPagerParameter.RecordCount;
					string strSql = " MemID=" + dr["MemID"].ToString();
					StringBuilder strSb = new StringBuilder();
					strSb.Append(" and OrderType <> 4 and OrderType <> 5");
					if (this.txtStartTime.Value != "")
					{
						strSb.AppendFormat(" and OrderCreateTime>='{0}'", this.txtStartTime.Value);
					}
					if (this.txtEndTime.Value != "")
					{
						strSb.AppendFormat(" and OrderCreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
					}
					strSql += strSb;
					DataTable dt = this.bllMem.GetMemExpenseDetail(strSql).Tables[0];
					rptItem.DataSource = dt;
					rptItem.DataBind();
					foreach (RepeaterItem rp in rptItem.Items)
					{
						Label lblDetailNum = (Label)rp.FindControl("lblDetailNumber");
						lblDetailNum.Text = (rp.ItemIndex + 1).ToString();
					}
				}
			}
		}
	}
}
