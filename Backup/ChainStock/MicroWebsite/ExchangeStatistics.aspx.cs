using Chain.BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.MicroWebsite
{
	public class ExchangeStatistics : PageBase
	{
		protected HtmlForm frmExchangeStatistics;

		protected Literal ltlTitle;

		protected HtmlInputText txtQueryMem;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected HtmlInputText txtGift;

		protected HtmlSelect sltMemLevelID;

		protected Button btnRptPointExchangeQuery;

		protected Repeater r_GiftExChange;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				this.txtStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString();
				this.txtEndTime.Value = DateTime.Now.ToString();
				this.GetParameterList(this.QueryCondition());
			}
		}

		public void GetParameterList(string strSql)
		{
			MicroWebsiteGiftExchange MicroWebsiteGiftExchangeBll = new MicroWebsiteGiftExchange();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " and MicroWebsiteGiftExchange.ExchangeStatus=2 and MicroWebsiteGiftExchange.MemID=Mem.MemID and MicroWebsiteGiftExchange.ExchangeUserID = SysUser.UserID and SysUser.UserShopID = SysShop.ShopID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "MemShopID", strSql);
			DataTable db = MicroWebsiteGiftExchangeBll.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.r_GiftExChange.DataSource = db;
			this.r_GiftExChange.DataBind();
			PageBase.BindSerialRepeater(this.r_GiftExChange, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value;
			string strMemLevelID = this.sltMemLevelID.Value;
			string strGift = this.txtGift.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" 1=1 ");
			if (strQueryMem != "")
			{
				strSql.AppendFormat("and (MemCard like '%{0}%' or Mem.MemName like '%{0}%' or MemMobile like '%{0}%')", strQueryMem);
			}
			if (strMemLevelID != "")
			{
				strSql.AppendFormat("and Mem.MemLevelID={0}", int.Parse(strMemLevelID));
			}
			if (strGift != "")
			{
				DataTable dt = new GiftExchange().GetExchangeIDByGiftNameOrGiftCode(string.Format("(PointGift.GiftName like '%{0}%' or PointGift.GiftCode like '%{0}%')", strGift));
				if (dt.Rows.Count > 0)
				{
					StringBuilder temp = new StringBuilder();
					temp.Append("and ExchangeID in (" + dt.Rows[0][0]);
					for (int i = 1; i < dt.Rows.Count; i++)
					{
						temp.Append("," + dt.Rows[i][0]);
					}
					temp.Append(")");
					strSql.Append(temp.ToString());
				}
				else
				{
					strSql.Append("and ExchangeID in (-1)");
				}
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat("and ExchangeTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat("and ExchangeTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			return strSql.ToString();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.GetParameterList(this.QueryCondition());
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.GetParameterList(this.QueryCondition());
		}

		protected void btnRptPointExchangeQuery_Click(object sender, EventArgs e)
		{
			this.GetParameterList(this.QueryCondition());
		}

		protected void r_GiftExChange_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Repeater Repeater = (Repeater)e.Item.FindControl("Repeater1");
				if (Repeater != null)
				{
					int exchangeID = Convert.ToInt32(((DataRowView)e.Item.DataItem)["ExchangeID"]);
					DataTable dt = new MicroWebsiteGiftExchangeDetail().GetGiftExchangeDetailByExchangeID(exchangeID);
					Repeater.DataSource = dt;
					Repeater.DataBind();
					foreach (RepeaterItem item in Repeater.Items)
					{
						Label lblNum = (Label)item.FindControl("lblNum");
						lblNum.Text = (item.ItemIndex + 1).ToString();
					}
				}
			}
		}
	}
}
