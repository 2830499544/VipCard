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
	public class RptPointChange : PageBase
	{
		protected HtmlForm frmRptPointChange;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected HtmlInputText txtMemStartTime;

		protected HtmlInputText txtMemEndTime;

		protected HtmlSelect sltShopChart;

		protected HtmlInputHidden HDsltshop;

		protected Button btnRptPointChangeExcel;

		protected HtmlInputText txtQueryMem;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltShop;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected HtmlSelect sltPoint;

		protected HtmlInputText txtPoint;

		protected Button btnRptPointChangeQuery;

		protected Label lblChangePoint;

		protected Repeater gvRptPointChange;

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
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				PubFunction.AgainPrint(ref this.lblPrintTitle, ref this.lblPrintFoot, this._UserShopID);
				this.txtPoint.Value = "0";
				this.txtStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtMemStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtMemEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				if (PubFunction.curParameter.dataAuthority != 0)
				{
					if (this._UserShopID > 1)
					{
						this.sltShop.Value = this._UserShopID.ToString();
					}
				}
				this.BindPointChange();
				this.Get_ParameterList(this.QueryCondition());
				this.PointNum.Value = PubFunction.GetPointNum("JFBDBB");
			}
		}

		private void BindPointChange()
		{
			PointLog bllPoint = new PointLog();
			string strSql = this.QueryCondition();
			strSql += " and PointLog.PointMemID = Mem.MemID ";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "PointShopID", strSql);
			int intPoint = bllPoint.GetPointChange(strSql);
			this.lblChangePoint.Text = intPoint.ToString();
		}

		private void Get_ParameterList(string strSql)
		{
			PointLog bllPointLog = new PointLog();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += "and PointLog.PointShopID = SysShop.ShopID and PointLog.PointMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and PointLog.PointUserID = SysUser.UserID";
			DataTable db = bllPointLog.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvRptPointChange.DataSource = db;
			this.gvRptPointChange.DataBind();
			PageBase.BindSerialRepeater(this.gvRptPointChange, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindPointChange();
			this.Get_ParameterList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value;
			string strMemLevelID = this.sltMemLevelID.Value;
			string strMemShopID = this.sltShop.Value;
			string strPointSymbols = this.sltPoint.Value;
			string strPoint = (this.txtPoint.Value.Trim() != "") ? this.txtPoint.Value.Trim() : "0";
			decimal intPoint = decimal.Parse(strPoint);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1");
			if (strQueryMem != "")
			{
				strSql.AppendFormat("and (MemCard = '{0}' or MemName like '%{0}%' or MemMobile = '{0}' or MemCardNumber = '{0}' )", strQueryMem);
			}
			if (strMemLevelID != "")
			{
				strSql.AppendFormat("and Mem.MemLevelID={0}", int.Parse(strMemLevelID));
			}
			if (strMemShopID != "")
			{
				string strwhere = PubFunction.GetMemListShopAuthority(int.Parse(strMemShopID), "PointShopID", " 1=1 ");
				strSql.AppendFormat(" and {0}", strwhere);
			}
			else
			{
				string strwhere = PubFunction.GetMemListShopAuthority(this._UserShopID, "PointShopID", " 1=1 ");
				strSql.AppendFormat(" and {0}", strwhere);
			}
			if (intPoint != 0m)
			{
				strSql.AppendFormat("and PointNumber" + strPointSymbols + "{0}", intPoint);
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat("and PointCreateTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat("and PointCreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			return strSql.ToString();
		}

		protected void btnRptPointChangeQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.BindPointChange();
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void btnRptPointChangeExcel_Click(object sender, EventArgs e)
		{
			PointLog bllPointLog = new PointLog();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += "and PointLog.PointShopID = SysShop.ShopID and PointLog.PointMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and PointLog.PointUserID = SysUser.UserID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "PointShopID", strSql);
			DataTable dtPointLog = bllPointLog.GetListSP(100000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.PointChangeReportExcel(dtPointLog, this._UserName);
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindPointChange();
			this.Get_ParameterList(this.QueryCondition());
		}
	}
}
