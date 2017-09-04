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
	public class RptMoneyExchange : PageBase
	{
		protected HtmlForm frm;

		protected Literal ltlTitle;

		protected Button btnMoneyExchangeExcel;

		protected HtmlInputText txtQueryMem;

		protected HtmlInputText txtMoneyExchangeCode;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected HtmlSelect sltdMoneyChangeType;

		protected Button btnMoneyExchangeSearch;

		protected Repeater gvMoneyExchange;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				this.BindMoneyChangeType(this.sltdMoneyChangeType, true);
				this.BindMoneyChange();
				this.Get_ParameterList(this.QueryCondition());
			}
		}

		private void BindMoneyChange()
		{
		}

		private void Get_ParameterList(string strSql)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "UserShopID", strSql);
			MoneyChangeLog moneyChangeLogBll = new MoneyChangeLog();
			DataTable db = moneyChangeLogBll.GetMoneyChangeLog(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvMoneyExchange.DataSource = db;
			this.gvMoneyExchange.DataBind();
			PageBase.BindSerialRepeater(this.gvMoneyExchange, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindMoneyChange();
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindMoneyChange();
			this.Get_ParameterList(this.QueryCondition());
		}

		private string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value.Trim();
			string shopID = this.sltShop.Value;
			string startTime = this.txtStartTime.Value;
			string endTime = this.txtEndTime.Value;
			string changeAccount = this.txtMoneyExchangeCode.Value.Trim();
			string changeType = this.sltdMoneyChangeType.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1 ");
			if (!string.IsNullOrEmpty(strQueryMem))
			{
				strSql.AppendFormat("and (MemCard ='{0}' or MemName like '%{0}%' or MemMobile='{0}' or MemCardNumber = '{0}' ) ", strQueryMem);
			}
			if (!string.IsNullOrEmpty(shopID))
			{
				strSql.AppendFormat("and ShopID={0} ", shopID);
			}
			if (!string.IsNullOrEmpty(startTime))
			{
				strSql.AppendFormat("and MoneyChangeCreateTime>='{0}' ", startTime);
			}
			if (!string.IsNullOrEmpty(endTime))
			{
				strSql.AppendFormat("and MoneyChangeCreateTime<='{0}' ", Convert.ToDateTime(endTime).AddDays(1.0).ToString());
			}
			if (!string.IsNullOrEmpty(changeAccount))
			{
				strSql.AppendFormat("and MoneyChangeAccount='{0}' ", changeAccount);
			}
			if (!string.IsNullOrEmpty(changeType))
			{
				strSql.AppendFormat("and MoneyChangeType={0} ", changeType);
			}
			strSql.Append("and MoneyChangeLog.MoneyChangeMemID=Mem.MemID and MoneyChangeLog.MoneyChangeUserID=SysUser.UserID and SysUser.UserShopID=SysShop.ShopID ");
			return strSql.ToString();
		}

		private void BindMoneyChangeType(HtmlSelect select, bool bolAddEmpty)
		{
			select.Items.Clear();
			if (bolAddEmpty)
			{
				select.Items.Add(new ListItem("===== 请选择 =====", ""));
			}
			select.Items.Add(new ListItem("会员充值", "1"));
			select.Items.Add(new ListItem("充值撤单", "2"));
			select.Items.Add(new ListItem("快速消费", "3"));
			select.Items.Add(new ListItem("快速消费撤单", "4"));
			select.Items.Add(new ListItem("初始充值", "5"));
			select.Items.Add(new ListItem("会员导入", "6"));
			select.Items.Add(new ListItem("账户提现", "7"));
			select.Items.Add(new ListItem("会员充次", "8"));
			select.Items.Add(new ListItem("计时消费", "9"));
			select.Items.Add(new ListItem("计时消费撤单", "10"));
			select.Items.Add(new ListItem("商品消费撤单", "11"));
			select.Items.Add(new ListItem("商品消费", "12"));
			select.Items.Add(new ListItem("挂单结算", "13"));
			select.Items.Add(new ListItem("商品退货", "14"));
			select.Items.Add(new ListItem("会员充时", "15"));
			select.Items.Add(new ListItem("会员转账", "16"));
		}

		protected void btnMoneyExchangeSearch_Click(object sender, EventArgs e)
		{
			this.BindMoneyChange();
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void btnMoneyExchangeExcel_Click(object sender, EventArgs e)
		{
			string strSql = this.QueryCondition();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "UserShopID", strSql);
			MoneyChangeLog moneyChangeLogBll = new MoneyChangeLog();
			DataTable db = moneyChangeLogBll.GetMoneyChangeLog(100000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.MoneyChangeLogReportExcel(db, this._UserName);
		}

		protected string GetExchangeType(object obj)
		{
			string result;
			switch (Convert.ToInt32(obj))
			{
			case 1:
				result = "会员充值";
				break;
			case 2:
				result = "充值撤单";
				break;
			case 3:
				result = "快速消费";
				break;
			case 4:
				result = "快速消费撤单";
				break;
			case 5:
				result = "初始充值";
				break;
			case 6:
				result = "会员导入";
				break;
			case 7:
				result = "账户提现";
				break;
			case 8:
				result = "会员充次";
				break;
			case 9:
				result = "计时消费";
				break;
			case 10:
				result = "计时消费撤单";
				break;
			case 11:
				result = "商品消费撤单";
				break;
			case 12:
				result = "商品消费";
				break;
			case 13:
				result = "挂单结算";
				break;
			case 14:
				result = "商品退货";
				break;
			case 15:
				result = "会员充时";
				break;
			case 16:
				result = "会员转账";
				break;
			default:
				result = "未知类型";
				break;
			}
			return result;
		}
	}
}
