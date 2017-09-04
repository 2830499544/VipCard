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
	public class RptMemStorageTiming : PageBase
	{
		private MemStorageTiming bllMemStorageTiming = new MemStorageTiming();

		protected HtmlHead Head1;

		protected HtmlForm frmRptMemCount;

		protected Literal ltlTitle;

		protected Button btnMemCountExcel;

		protected HtmlInputText txtQueryMem;

		protected HtmlInputText txtCountAccount;

		protected HtmlInputText txtRemark;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected HtmlInputText txtProjectName;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected Button btnRptMemCountQuery;

		protected Label lblMoney;

		protected Label lblDiscountMoney;

		protected Label lblTotalPoint;

		protected Label lblToalTime;

		protected Label lblRemainTime;

		protected Repeater rptMemStorageTimingList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
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
				this.BindStorageTiming();
				this.BindStorageTimingList(this.QueryCondition());
			}
		}

		private void BindStorageTiming()
		{
			MemStorageTiming bllTime = new MemStorageTiming();
			string strSql = this.QueryCondition();
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "StorageTimingShopID", strSql);
			DataTable dtTime = bllTime.GetTimeTotal(strSql).Tables[0];
			this.lblMoney.Text = decimal.Parse(dtTime.Rows[0]["TotalMoney"].ToString()).ToString("0.00");
			this.lblDiscountMoney.Text = decimal.Parse(dtTime.Rows[0]["DiscountMoney"].ToString()).ToString("0.00");
			this.lblToalTime.Text = int.Parse(dtTime.Rows[0]["TotalTime"].ToString()).ToString();
			this.lblRemainTime.Text = int.Parse(dtTime.Rows[0]["RemainTime"].ToString()).ToString();
			this.lblTotalPoint.Text = int.Parse(dtTime.Rows[0]["TotalPoint"].ToString()).ToString();
		}

		private void BindStorageTimingList(string strSql)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " AND MemStorageTiming.StorageTimingMemID = Mem.MemID \r\n                         AND MemStorageTiming.StorageTimingShopID = SysShop.ShopID \r\n                         AND MemStorageTiming.StorageTimingUserID = SysUser.UserID \r\n                         AND MemStorageTiming.StorageTimingProjectID = TimingProject.ProjectID \r\n                         AND Mem.MemLevelID = MemLevel.LevelID ";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "StorageTimingShopID", strSql);
			DataTable dtMemCount = this.bllMemStorageTiming.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.rptMemStorageTimingList.DataSource = dtMemCount;
			this.rptMemStorageTimingList.DataBind();
			PageBase.BindSerialRepeater(this.rptMemStorageTimingList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindStorageTiming();
			this.BindStorageTimingList(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindStorageTiming();
			this.BindStorageTimingList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value;
			string strMemLevelID = this.sltMemLevelID.Value;
			string strMemShopID = this.sltShop.Value;
			string strRemark = PubFunction.RemoveSpace(this.txtRemark.Value);
			string strCountAccount = PubFunction.RemoveSpace(this.txtCountAccount.Value);
			string strProjectName = this.txtProjectName.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1 ");
			if (strQueryMem != "")
			{
				strSql.AppendFormat(" and (Mem.MemCard = '{0}' or Mem.MemName like '%{0}%' or Mem.MemMobile = '{0}' or Mem.MemCardNumber = '{0}' )", strQueryMem);
			}
			if (strMemLevelID != "")
			{
				strSql.AppendFormat(" and Mem.MemLevelID={0}", int.Parse(strMemLevelID));
			}
			if (strMemShopID != "")
			{
				strSql.AppendFormat(" and MemStorageTiming.StorageTimingShopID={0}", int.Parse(strMemShopID));
			}
			if (strCountAccount != "")
			{
				strSql.AppendFormat(" and MemStorageTiming.StorageTimingAccount='{0}'", strCountAccount);
			}
			if (strRemark != "")
			{
				strSql.AppendFormat(" and MemStorageTiming.StorageTimingRemark like '%{0}%' ", strRemark);
			}
			if (strProjectName != "")
			{
				strSql.AppendFormat(" and TimingProject.ProjectName like '%{0}%'", strProjectName);
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat(" and MemStorageTiming.StorageTimingCreateTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat(" and MemStorageTiming.StorageTimingCreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			return strSql.ToString();
		}

		protected void btnRptMemCountQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.BindStorageTiming();
			this.BindStorageTimingList(this.QueryCondition());
		}

		protected void btnMemCountExcel_Click(object sender, EventArgs e)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += " AND MemStorageTiming.StorageTimingMemID = Mem.MemID \r\n                         AND MemStorageTiming.StorageTimingShopID = SysShop.ShopID \r\n                         AND MemStorageTiming.StorageTimingUserID = SysUser.UserID \r\n                         AND MemStorageTiming.StorageTimingProjectID = TimingProject.ProjectID \r\n                         AND Mem.MemLevelID = MemLevel.LevelID ";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "StorageTimingShopID", strSql);
			DataTable dtMemCount = this.bllMemStorageTiming.GetListSP(10000000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.MemStorageTimingExcel(dtMemCount, this._UserName);
		}
	}
}
