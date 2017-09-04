using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.PointManage
{
	public class ExchangeVerify : PageBase
	{
		protected HtmlForm frmExchangeVerify;

		protected Literal ltlTitle;

		protected HtmlInputText txtExchangeRemark;

		protected Repeater rptExchangeVerify;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.BindExchangeList();
		}

		protected void BindExchangeList()
		{
			GiftExchange bllGiftExchange = new GiftExchange();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = " GiftExchange.MemID=Mem.MemID and ExchangeStatus=1 ";
			if (this._UserShopID != 1)
			{
				strSql = PubFunction.GetShopAuthority(this._UserShopID, "ShopID", strSql);
			}
			DataTable dtGiftExchange = bllGiftExchange.GetVerifyListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.rptExchangeVerify.DataSource = dtGiftExchange;
			this.rptExchangeVerify.DataBind();
			PageBase.BindSerialRepeater(this.rptExchangeVerify, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindExchangeList();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindExchangeList();
		}

		protected void rptMemGiftList_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				Repeater rptItem = (Repeater)e.Item.FindControl("rptExchangeVerifyDetail");
				if (rptItem != null)
				{
					GiftExchangeDetail bllGiftDetail = new GiftExchangeDetail();
					int Count = this.NetPagerParameter.RecordCount;
					string strSql = " GiftExchangeDetail.ExchangeGiftID=PointGift.GiftID ";
					strSql = strSql + " and GiftExchangeDetail.ExchangeID=" + dr["ExchangeID"].ToString();
					DataTable dtGiftDetail = bllGiftDetail.GetList(strSql).Tables[0];
					rptItem.DataSource = dtGiftDetail;
					rptItem.DataBind();
					foreach (RepeaterItem rp in rptItem.Items)
					{
						Label lblDetailNum = (Label)rp.FindControl("lblDetailNumber");
						lblDetailNum.Text = (rp.ItemIndex + 1).ToString();
					}
				}
				Label lblStatus = (Label)e.Item.FindControl("lblExchangeStatus");
				if (lblStatus != null)
				{
					string text = dr["ExchangeStatus"].ToString();
					if (text != null)
					{
						if (!(text == "1"))
						{
							if (!(text == "2"))
							{
								if (text == "3")
								{
									lblStatus.Text = "退回";
								}
							}
							else
							{
								lblStatus.Text = "通过审核";
							}
						}
						else
						{
							lblStatus.Text = "待审核";
						}
					}
				}
			}
		}
	}
}
