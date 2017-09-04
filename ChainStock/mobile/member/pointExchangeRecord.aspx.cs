using Chain.BLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.member
{
	public class pointExchangeRecord : Page
	{
		protected Repeater rptExchange;

		protected HtmlAnchor moreExchange;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				GiftExchange bllExchange = new GiftExchange();
				DataTable dt;
				if (base.Request.QueryString["type"] == "all")
				{
					dt = bllExchange.GetList(" 1=1 ").Tables[0];
				}
				else
				{
					dt = bllExchange.GetList(10, " 1=1 ", "ApplicationTime").Tables[0];
				}
				this.rptExchange.DataSource = dt;
				this.rptExchange.DataBind();
				if (dt.Rows.Count < 10 || base.Request.QueryString["type"] == "all")
				{
					this.moreExchange.Attributes.Add("style", "display:none");
				}
			}
		}

		protected void rptExchange_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				Repeater rptItem = (Repeater)e.Item.FindControl("rptExchangeDetail");
				if (rptItem != null)
				{
					string strSql = "  PointGift.GiftID=GiftExchangeDetail.ExchangeGiftID and ExchangeID=" + dr["ExchangeID"].ToString();
					GiftExchangeDetail bllExchange = new GiftExchangeDetail();
					DataTable dt = bllExchange.GetList(strSql).Tables[0];
					rptItem.DataSource = dt;
					rptItem.DataBind();
				}
			}
		}
	}
}
