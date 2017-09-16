using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Common
{
	public class TodayRemind : PageBase
	{
		protected HtmlForm frmRemind;

		protected HtmlInputHidden txthidden;

		protected HtmlInputHidden txtGoods;

		protected Literal ltlTitle;

		protected HtmlAnchor MemBirthdaySendSMS;

		protected HtmlAnchor MemPastTimeSendSMS;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!PubFunction.curParameter.bolSms)
			{
				this.MemBirthdaySendSMS.Style.Add("display", "none");
				this.MemPastTimeSendSMS.Style.Add("display", "none");
			}
			else
			{
				this.MemBirthdaySendSMS.Style.Add("display", "");
				this.MemPastTimeSendSMS.Style.Add("display", "");
			}
			this.txthidden.Value = this.curParameter.intPointPeriod.ToString();
			this.txtGoods.Value = this.curParameter.intStockCount.ToString();
		}
	}
}
