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
	public class RptMemCount : PageBase
	{
		private MemCount bllMemCount = new MemCount();

		protected HtmlForm frmRptMemCount;

		protected Literal ltlTitle;

		protected Button btnMemCountExcel;

		protected HtmlInputText txtQueryMem;

		protected HtmlInputText txtCountAccount;

		protected HtmlInputText txtRemark;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected HtmlSelect sltMoney;

		protected HtmlInputText txtMoney;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected Button btnRptMemCountQuery;

		protected Label lblMoney;

		protected Label lblDiscountMoney;

		protected Label lblTotalPoint;

		protected Label lblToalCount;

		protected Label lblRemainCount;

		protected Repeater rptMemCountList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				this.txtMoney.Value = "0";
				this.txtStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				if (PubFunction.curParameter.dataAuthority != 0)
				{
					if (this._UserShopID > 1)
					{
						this.sltShop.Value = this._UserShopID.ToString();
						this.sltShop.Attributes.Add("disabled", "disabled");
					}
				}
				this.BindMemCountList();
				this.BindMemCountList(this.QueryCondition());
			}
		}

		private void BindMemCountList()
		{
			MemCount bllCount = new MemCount();
			MemCountDetail bllCountDetail = new MemCountDetail();
			string strSql = this.QueryCondition();
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "CountShopID", strSql);
			DataTable dtMoney = bllCount.GetCountMoney(strSql).Tables[0];
			this.lblMoney.Text = decimal.Parse(dtMoney.Rows[0]["TotalMoney"].ToString()).ToString("0.00");
			this.lblDiscountMoney.Text = decimal.Parse(dtMoney.Rows[0]["DiscountMoney"].ToString()).ToString("0.00");
			this.lblTotalPoint.Text = int.Parse(dtMoney.Rows[0]["TotalPoint"].ToString()).ToString();
			DataTable dtNumber = bllCount.GetCountNumber(strSql).Tables[0];
			this.lblToalCount.Text = dtNumber.Rows[0]["TotalNumber"].ToString();
			this.lblRemainCount.Text = dtNumber.Rows[0]["RemainCount"].ToString();
		}

		private void BindMemCountList(string strSql)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " and Mem.MemID = MemCount.CountMemID and MemCount.CountShopID = SysShop.ShopID and Mem.MemUserID = SysUser.UserID and Mem.MemLevelID=MemLevel.LevelID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "CountShopID", strSql);
			DataTable dtMemCount = this.bllMemCount.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.rptMemCountList.DataSource = dtMemCount;
			this.rptMemCountList.DataBind();
			PageBase.BindSerialRepeater(this.rptMemCountList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindMemCountList();
			this.BindMemCountList(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindMemCountList();
			this.BindMemCountList(this.QueryCondition());
		}

		protected void rptMemCountList_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				Repeater rptItem = (Repeater)e.Item.FindControl("rptMemCountDetailList");
				if (rptItem != null)
				{
					MemCountDetail bllMemCountDetail = new MemCountDetail();
					int Count = this.NetPagerParameter.RecordCount;
					string strSql = " CountDetailCountID=" + dr["CountID"].ToString();
					DataTable dtMemCountDetail = bllMemCountDetail.GetList(strSql).Tables[0];
					rptItem.DataSource = dtMemCountDetail;
					rptItem.DataBind();
					foreach (RepeaterItem rp in rptItem.Items)
					{
						Label lblDetailNum = (Label)rp.FindControl("lblDetailNumber");
						lblDetailNum.Text = (rp.ItemIndex + 1).ToString();
					}
				}
			}
		}

		protected string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value;
			string strMemLevelID = this.sltMemLevelID.Value;
			string strMemShopID = this.sltShop.Value;
			string strMoneySymbols = this.sltMoney.Value;
			string strMoney = (this.txtMoney.Value.Trim() != "") ? this.txtMoney.Value.Trim() : "0";
			decimal dclMoney = decimal.Parse(strMoney);
			string strRemark = PubFunction.RemoveSpace(this.txtRemark.Value);
			string strCountAccount = PubFunction.RemoveSpace(this.txtCountAccount.Value);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1 ");
			if (strQueryMem != "")
			{
				strSql.AppendFormat(" and (MemCard = '{0}' or MemName like '%{0}%' or MemMobile = '{0}' or MemCardNumber = '{0}' )", strQueryMem);
			}
			if (strMemLevelID != "")
			{
				strSql.AppendFormat(" and MemLevelID={0}", int.Parse(strMemLevelID));
			}
			if (strMemShopID != "")
			{
				strSql.AppendFormat(" and CountShopID={0}", int.Parse(strMemShopID));
			}
			if (dclMoney != 0m)
			{
				strSql.AppendFormat(" and CountDiscountMoney" + strMoneySymbols + "{0}", dclMoney);
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat(" and MemCount.CountCreateTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat(" and MemCount.CountCreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			if (strRemark != "")
			{
				strSql.AppendFormat(" and CountRemark like '%{0}%' ", strRemark);
			}
			if (strCountAccount != "")
			{
				strSql.AppendFormat(" and CountAccount='{0}'", strCountAccount);
			}
			return strSql.ToString();
		}

		protected void btnRptMemCountQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.BindMemCountList();
			this.BindMemCountList(this.QueryCondition());
		}

		protected void btnMemCountExcel_Click(object sender, EventArgs e)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += " and Mem.MemID = MemCount.CountMemID and MemCount.CountShopID = SysShop.ShopID and Mem.MemUserID = SysUser.UserID and Mem.MemLevelID=MemLevel.LevelID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "CountShopID", strSql);
			DataTable dtMemCount = this.bllMemCount.GetListSP(10000000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.MemCount(dtMemCount, this._UserName);
		}
	}
}
