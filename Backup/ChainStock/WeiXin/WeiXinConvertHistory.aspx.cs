using Chain.BLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.WeiXin
{
	public class WeiXinConvertHistory : Page
	{
		protected Repeater Rpt_GiftExchange;

		protected HtmlGenericControl noProduct;

		private GiftExchange giftExchangeBll = new GiftExchange();

		protected string MemWeiXinCard = string.Empty;

		protected string rc = PubFunction.GetDataTimeString();

		protected void Page_Load(object sender, EventArgs e)
		{
			this.MemWeiXinCard = base.Request["MemWeiXinCard"];
			this.BindData();
		}

		public void BindData()
		{
			int rowCount = 0;
			string strWhere = "GiftExchange.MemID=Mem.MemID and MemWeiXinCard='" + this.MemWeiXinCard + "'";
			DataTable dt = this.giftExchangeBll.GetListGiftExchangeSPForWeiXin(10, 1, out rowCount, new string[]
			{
				strWhere
			}).Tables[0];
			if (dt.Rows.Count > 0)
			{
				this.Rpt_GiftExchange.DataSource = dt;
				this.Rpt_GiftExchange.DataBind();
			}
			else
			{
				this.noProduct.Style.Add("display", "\"\"");
			}
		}
	}
}
