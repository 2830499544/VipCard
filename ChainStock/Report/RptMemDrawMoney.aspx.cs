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
	public class RptMemDrawMoney : PageBase
	{
		protected HtmlForm frmMemDrawMoney;

		protected Literal ltlTitle;

		protected HtmlInputText txtMemStartTime;

		protected HtmlInputText txtMemEndTime;

		protected HtmlSelect sltShopChart;

		protected HtmlInputHidden HDsltshop;

		protected Button btnMemDrawMoneyExcel;

		protected HtmlInputText txtQueryMem;

		protected HtmlInputText txtRechargeAccount;

		protected HtmlInputText txtRemark;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltShop;

		protected HtmlSelect sltMoney;

		protected HtmlInputText txtDrawMoney;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected Button btnRptMemDrawQuery;

		protected Label lblDrawTotalMoney;

		protected Label lblDrawActualMoney;

		protected HtmlGenericControl report;

		protected Repeater gvRptMemDrawMoney;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShopChart, true);
				this.txtDrawMoney.Value = "0";
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
				this.BindDrawMoney();
				this.Get_ParameterList(this.QueryCondition());
			}
		}

		private void BindDrawMoney()
		{
			MemDrawMoney bllDrawMoney = new MemDrawMoney();
			string strSql = this.QueryCondition();
			strSql += " and MemDrawMoney.DrawMoneyMemID=Mem.MemID ";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "DrawMoneyShopID", strSql);
			DataTable dtDrawMoney = bllDrawMoney.GetDrawMoneyCount(strSql).Tables[0];
			this.lblDrawTotalMoney.Text = decimal.Parse(dtDrawMoney.Rows[0]["DrawMoney"].ToString()).ToString("0.00");
			this.lblDrawActualMoney.Text = decimal.Parse(dtDrawMoney.Rows[0]["DrawActualMoney"].ToString()).ToString("0.00");
		}

		private void Get_ParameterList(string strSql)
		{
			MemDrawMoney bllDrawMoney = new MemDrawMoney();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += "and MemDrawMoney.DrawMoneyShopID = SysShop.ShopID and MemDrawMoney.DrawMoneyMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and MemDrawMoney.DrawMoneyUserID = SysUser.UserID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "DrawMoneyShopID", strSql);
			DataTable db = bllDrawMoney.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvRptMemDrawMoney.DataSource = db;
			this.gvRptMemDrawMoney.DataBind();
			PageBase.BindSerialRepeater(this.gvRptMemDrawMoney, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindDrawMoney();
			this.Get_ParameterList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value;
			string strMemLevelID = this.sltMemLevelID.Value;
			string strMemShopID = this.sltShop.Value;
			string strMoneySymbols = this.sltMoney.Value;
			string strMoney = (this.txtDrawMoney.Value.Trim() != "") ? this.txtDrawMoney.Value.Trim() : "0";
			decimal dclMoney = decimal.Parse(strMoney);
			string strRechargeAccount = PubFunction.RemoveSpace(this.txtRechargeAccount.Value);
			string strRemark = PubFunction.RemoveSpace(this.txtRemark.Value);
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
				strSql.AppendFormat("and DrawMoneyShopID={0}", int.Parse(strMemShopID));
			}
			if (dclMoney != 0m)
			{
				strSql.AppendFormat("and DrawMoney" + strMoneySymbols + "{0}", dclMoney);
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat("and DrawMoneyCreateTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat("and DrawMoneyCreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			if (strRemark != "")
			{
				strSql.AppendFormat(" and DrawMoneyRemark like '%{0}%' ", strRemark);
			}
			if (strRechargeAccount != "")
			{
				strSql.AppendFormat(" and DrawMoneyAccount='{0}'", strRechargeAccount);
			}
			return strSql.ToString();
		}

		protected void btnRptMemDrawQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.BindDrawMoney();
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindDrawMoney();
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void btnMemDrawMoneyExcel_Click(object sender, EventArgs e)
		{
			MemDrawMoney bllDrawMoney = new MemDrawMoney();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += "and MemDrawMoney.DrawMoneyShopID = SysShop.ShopID and MemDrawMoney.DrawMoneyMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and MemDrawMoney.DrawMoneyUserID = SysUser.UserID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "DrawMoneyShopID", strSql);
			DataTable db = bllDrawMoney.GetListSP(1000000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.MemDrawMoney(db, this._UserName);
		}
	}
}
