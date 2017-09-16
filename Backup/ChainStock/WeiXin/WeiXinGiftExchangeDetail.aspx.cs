using Chain.BLL;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChainStock.WeiXin
{
	public class WeiXinGiftExchangeDetail : Page
	{
		protected Repeater Rpt_WeiXinGiftExchangeDetail;

		protected void Page_Load(object sender, EventArgs e)
		{
			string MemWeiXinCard = base.Request["MemWeiXinCard"];
			int ExchangeID = int.Parse(base.Request["ExchangeID"]);
			this.Rpt_WeiXinGiftExchangeDetail.DataSource = new GiftExchangeDetail().GetWeiXinList(ExchangeID);
			this.Rpt_WeiXinGiftExchangeDetail.DataBind();
		}
	}
}
