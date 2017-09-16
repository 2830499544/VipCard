using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.ExtraService
{
	public class SmsShopReport : PageBase
	{
		protected HtmlForm frmSmsShopReport;

		protected Literal ltlTitle;

		protected Label lblMonthNumber;

		protected Label lblTotalNumber;

		protected HtmlGenericControl TongZhiduanxin;

		protected Label lblNotificationSMSBalance;

		protected HtmlGenericControl Yinxiaoduanxin;

		protected Label lblMarketingSMSBalance;

		protected Repeater gvSmsShopReport;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.Get_ParameterList();
			}
		}

		private void Get_ParameterList()
		{
			SmsLog bllSms = new SmsLog();
			string strSql = PubFunction.GetShopAuthority(this._UserShopID, "ShopID", "1=1");
			this.lblMonthNumber.Text = bllSms.GetSmsMonthNumber(strSql).ToString();
			this.lblTotalNumber.Text = bllSms.GetSmsTotalNumber(strSql).ToString();
			try
			{
				SysParameter sds = new SysParameter();
				if (sds.GetModel(1).Sms)
				{
					this.lblNotificationSMSBalance.Text = SMSInfo.GetBalance(false);
				}
				else
				{
					this.TongZhiduanxin.Attributes.Add("style", "display:none");
				}
				if (sds.GetModel(1).MarketingSMS)
				{
					this.lblMarketingSMSBalance.Text = SMSInfo.GetBalance(true);
				}
				else
				{
					this.Yinxiaoduanxin.Attributes.Add("style", "display:none");
				}
			}
			catch
			{
			}
			int Counts = this.NetPagerParameter.RecordCount;
			DataTable db = bllSms.GetSmsShopReport(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvSmsShopReport.DataSource = db;
			this.gvSmsShopReport.DataBind();
			PageBase.BindSerialRepeater(this.gvSmsShopReport, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.Get_ParameterList();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Get_ParameterList();
		}
	}
}
