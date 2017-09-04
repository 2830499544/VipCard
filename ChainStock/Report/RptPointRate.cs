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
	public class RptPointRate : PageBase
	{
		protected HtmlForm form1;

		protected HiddenField hidLevel;

		protected Literal ltlTitle;

		protected Button btnRptPointRateExcel;

		protected HtmlInputText txtQueryMem;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected Button btnRptPointRateQuery;

		protected Literal lblToday;

		protected Literal lblWeek;

		protected Literal lblMonth;

		protected Literal lblTotal;

		protected Repeater rptRptPointRate;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch1;

		public static string MemCardId;

		private StringBuilder strwhere = new StringBuilder();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				int year = DateTime.Today.Year;
				int month = DateTime.Today.Month;
				int day = DateTime.Today.Day;
				this.txtStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				if (PubFunction.curParameter.dataAuthority == 1)
				{
					if (this._UserShopID > 1)
					{
						this.sltShop.Value = this._UserShopID.ToString();
						this.sltShop.Attributes.Add("disabled", "disabled");
					}
				}
				this.BindPointChange();
				this.Get_ParameterList(this.QueryCondition());
			}
		}

		private void BindPointChange()
		{
			PointRate bllPointRate = new PointRate();
			this.hidLevel.Value = bllPointRate.GetPointRate().PointRateLevel.ToString();
			string sbWhere = this.QueryCondition();
			sbWhere = sbWhere.Replace("POINTLOG.PointChangeType =9", "(POINTLOG.PointChangeType =9 or POINTLOG.PointChangeType =12)");
			sbWhere += "  and PointLog.PointShopID = SysShop.ShopID and PointLog.PointMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and PointLog.PointUserID = SysUser.UserID";
			sbWhere = PubFunction.GetShopAuthority(this._UserShopID, "PointShopID", sbWhere);
			int intPoint = bllPointRate.GetPointRateNumber(sbWhere + " and datediff(day,PointCreateTime,getdate())=0");
			string strPoint = string.Format("{0}", intPoint);
			this.lblToday.Text = strPoint;
			intPoint = bllPointRate.GetPointRateNumber(sbWhere + " and datediff(week,PointCreateTime,getdate())=0");
			strPoint = string.Format("{0}", intPoint);
			this.lblWeek.Text = strPoint;
			intPoint = bllPointRate.GetPointRateNumber(sbWhere + " and datediff(month,PointCreateTime,getdate())=0");
			strPoint = string.Format("{0}", intPoint);
			this.lblMonth.Text = strPoint;
			intPoint = bllPointRate.GetPointRateNumber(sbWhere);
			strPoint = string.Format("{0}", intPoint);
			this.lblTotal.Text = strPoint;
		}

		private void Get_ParameterList(string strSql)
		{
			string strStartTime = this.txtStartTime.Value;
			string strEntTime = this.txtEndTime.Value + " 23:59:59";
			StringBuilder strTime = new StringBuilder();
			if (strStartTime != "")
			{
				this.strwhere.AppendFormat(" and POINTCREATETIME>='{0}'", strStartTime);
				strTime.AppendFormat(" and PointLOG.POINTCREATETIME>='{0}' ", strStartTime);
			}
			if (strEntTime != "")
			{
				this.strwhere.AppendFormat(" and POINTCREATETIME<='{0}'", strEntTime);
				strTime.AppendFormat(" and PointLOG.POINTCREATETIME<='{0}'", strEntTime);
			}
			PointRate bllPointRate = new PointRate();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += "and MEM.MEMID<>0 AND MEM.MEMSHOPID=SYSSHOP.SHOPID";
			strSql += " AND MEM.MEMLEVELID=MEMLEVEL.LEVELID";
			strSql += " AND MEM.MEMUSERID=SYSUSER.USERID";
			if (RptPointRate.MemCardId != null)
			{
				strSql += string.Format(" and Mem.MemCard={0}", RptPointRate.MemCardId);
			}
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "MemShopID", strSql);
			DataTable db = bllPointRate.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, strTime.ToString(), new string[]
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
			this.rptRptPointRate.DataSource = db;
			this.rptRptPointRate.DataBind();
			PageBase.BindSerialRepeater(this.rptRptPointRate, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.Get_ParameterList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value;
			string strMemLevelID = this.sltMemLevelID.Value;
			string strMemShopID = this.sltShop.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" POINTLOG.PointChangeType =9 ");
			if (strQueryMem != "")
			{
				strSql.AppendFormat("and (MEM.MemCard like '%{0}%' or MEM.MemName like '%{0}%' or MEM.MemMobile like '%{0}%' or MEM.MemCardNumber like '%{0}%' )", strQueryMem);
			}
			if (strMemLevelID != "")
			{
				strSql.AppendFormat("and MEM.MemLevelID={0}", int.Parse(strMemLevelID));
			}
			if (strMemShopID != "")
			{
				strSql.AppendFormat("and MEM.MEMShopID={0}", int.Parse(strMemShopID));
			}
			return strSql.ToString();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void btnRptPointRateQuery_Click(object sender, EventArgs e)
		{
			this.strwhere = new StringBuilder();
			this.BindPointChange();
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void btnRptPointRateExcel_Click(object sender, EventArgs e)
		{
			string strStartTime = this.txtStartTime.Value;
			string strEntTime = this.txtEndTime.Value;
			StringBuilder strTime = new StringBuilder();
			string strSql = this.QueryCondition();
			if (strStartTime != "")
			{
				this.strwhere.AppendFormat(" and POINTCREATETIME>='{0}'", strStartTime);
				strTime.AppendFormat(" and PointLOG.POINTCREATETIME>='{0}' ", strStartTime);
			}
			if (strEntTime != "")
			{
				this.strwhere.AppendFormat(" and POINTCREATETIME<='{0}'", strEntTime);
				strTime.AppendFormat(" and PointLOG.POINTCREATETIME<='{0}'", strEntTime);
			}
			PointRate bllPointRate = new PointRate();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += "and MEM.MEMID<>0 AND MEM.MEMSHOPID=SYSSHOP.SHOPID";
			strSql += " AND MEM.MEMLEVELID=MEMLEVEL.LEVELID";
			strSql += " AND MEM.MEMUSERID=SYSUSER.USERID";
			if (RptPointRate.MemCardId != null)
			{
				strSql += string.Format(" and Mem.MemCard={0}", RptPointRate.MemCardId);
			}
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "MemShopID", strSql);
			DataTable db = bllPointRate.GetListSP(Counts, this.NetPagerParameter.CurrentPageIndex, out Counts, strTime.ToString(), new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.PointRateExcel(db, this._UserName, this.strwhere.ToString());
		}

		protected void rptRptPointRate_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				Repeater rptItem = (Repeater)e.Item.FindControl("rptPointDetails");
				if (rptItem != null)
				{
					PointRate bllPointRate = new PointRate();
					int memID = Convert.ToInt32(dr["MemID"]);
					DataTable dtPointDetails = bllPointRate.GetMemDetailByMemCard(memID, this.strwhere.ToString()).Tables[0];
					rptItem.DataSource = dtPointDetails;
					rptItem.DataBind();
					foreach (RepeaterItem rp in rptItem.Items)
					{
						Label lblNum = (Label)rp.FindControl("lblDetails");
						lblNum.Text = (rp.ItemIndex + 1).ToString();
					}
				}
			}
		}
	}
}
