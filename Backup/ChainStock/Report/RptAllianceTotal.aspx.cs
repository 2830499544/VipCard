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
	public class RptAllianceTotal : PageBase
	{
		protected HtmlForm frmRptPointChange;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected HtmlInputText txtMemStartTime;

		protected HtmlInputText txtMemEndTime;

		protected HtmlSelect sltShopChart;

		protected HtmlInputHidden HDsltshop;

		protected Button btSerch;

		protected Button btnRptPointChangeExcel;

		protected HtmlSelect sltShop;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected Button btnRptPointChangeQuery;

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
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
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
				this.Get_ParameterList(this.QueryCondition());
				this.PointNum.Value = PubFunction.GetPointNum("JFBDBB");
			}
		}

		private void BindPointChange()
		{
		}

		private void Get_ParameterList(string strSql)
		{
			SysShop bllPointLog = new SysShop();
			int Counts = this.NetPagerParameter.RecordCount;
			DataTable db = bllPointLog.GetAllianceListShop(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			string strMemShopID = this.sltShop.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" ShopID>1 and  ShopType=2");
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat(" and ShopCreateTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat(" and ShopCreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			return strSql.ToString();
		}

		protected string BindShopName(object shopid)
		{
			SysShop Shop = new SysShop();
			string shopname = "";
			if (shopid != null)
			{
				shopname = Shop.GetShopNameByShopid(shopid.ToString());
			}
			return shopname;
		}

		protected void btnRptPointChangeQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.BindPointChange();
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void btnRptPointChangeExcel_Click(object sender, EventArgs e)
		{
			SysShop bllPointLog = new SysShop();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			DataTable dt = bllPointLog.GetAllianceListShop(100000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.AllianceListShopReportExcel(dt, this._UserName);
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
