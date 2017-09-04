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
	public class RptPointExchange : PageBase
	{
		protected HtmlForm frmRptPointExchange;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected HtmlInputText txtMemStartTime;

		protected HtmlInputText txtMemEndTime;

		protected HtmlSelect sltShopChart;

		protected Button btnRptPointExchangeExcel;

		protected HtmlInputText txtQueryMem;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected HtmlSelect selectExchangeType;

		protected HtmlInputText txtGift;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected Button btnRptPointExchangeQuery;

		protected Label lblExchangeNumber;

		protected Label lblExchangePoint;

		protected Repeater r_GiftExChange;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected Label lblPrintTitle;

		protected Label lblPrintFoot;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShopChart, true);
				PubFunction.AgainPrint(ref this.lblPrintTitle, ref this.lblPrintFoot, this._UserShopID);
				this.txtStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtMemStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtMemEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				if (PubFunction.curParameter.dataAuthority != 0)
				{
					if (this._UserShopID > 1)
					{
						this.sltShop.Value = this._UserShopID.ToString();
						this.sltShop.Attributes.Add("disabled", "disabled");
					}
				}
				this.BindExchage();
				this.GetParameterList(this.QueryCondition());
				this.PointNum.Value = PubFunction.GetPointNum("JFDHBB");
			}
		}

		private void BindExchage()
		{
			GiftExchange bllExchage = new GiftExchange();
			string strSql = this.QueryCondition();
			strSql += " and GiftExchange.MemID=Mem.MemID ";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "MemShopID", strSql);
			DataTable dtExchange = bllExchage.GetGiftExchange(strSql).Tables[0];
			this.lblExchangeNumber.Text = dtExchange.Rows[0]["ExchangeAllNumber"].ToString();
			this.lblExchangePoint.Text = dtExchange.Rows[0]["ExchangeAllPoint"].ToString();
		}

		public void GetParameterList(string strSql)
		{
			GiftExchange bllGiftExchange = new GiftExchange();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " and GiftExchange.ExchangeStatus=2 and GiftExchange.MemID=Mem.MemID and GiftExchange.ExchangeUserID = SysUser.UserID and SysUser.UserShopID = SysShop.ShopID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "MemShopID", strSql);
			DataTable db = bllGiftExchange.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindExchage();
			this.GetParameterList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value;
			string strMemLevelID = this.sltMemLevelID.Value;
			string strMemShopID = this.sltShop.Value;
			string strGift = this.txtGift.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" 1=1 ");
			if (strQueryMem != "")
			{
				strSql.AppendFormat("and (MemCard = '{0}' or Mem.MemName like '%{0}%' or MemMobile = '{0}' or MemCardNumber = '{0}' )", strQueryMem);
			}
			if (strMemLevelID != "")
			{
				strSql.AppendFormat("and Mem.MemLevelID={0}", int.Parse(strMemLevelID));
			}
			if (strMemShopID != "")
			{
				strSql.AppendFormat("and GiftExchange.ShopID={0}", int.Parse(strMemShopID));
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
			int intExchangeType = Convert.ToInt32(this.selectExchangeType.Value);
			if (intExchangeType != 0)
			{
				strSql.AppendFormat("and ExchangeType={0}", intExchangeType);
			}
			return strSql.ToString();
		}

		protected void btnRptPointExchangeQuery_Click(object sender, EventArgs e)
		{
			this.BindExchage();
			this.GetParameterList(this.QueryCondition());
		}

		protected void btnRptPointExchangeExcel_Click(object sender, EventArgs e)
		{
			PointExchange bllGiftExchange = new PointExchange();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition() + " and GiftExchange.ExchangeStatus=2 and GiftExchange.MemID=Mem.MemID and GiftExchange.ExchangeUserID = SysUser.UserID and Mem.MemShopID = SysShop.ShopID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "MemShopID", strSql);
			DataTable dtGiftExchange = bllGiftExchange.GetListSP(100000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.RptGiftExchangeExcel(dtGiftExchange, this._UserName);
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindExchage();
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
					DataTable dt = new GiftExchangeDetail().GetGiftExchangeDetailByExchangeID(exchangeID);
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
