using Chain.BLL;
using ChainStock.Controls;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.StockManage
{
	public class RptEmptyBills : PageBase
	{
		protected HtmlForm ftmEmptyBills;

		protected Literal ltlTitle;

		protected Button btnRptEmptyBillsExcel;

		protected HtmlInputHidden txtUser;

		protected HtmlInputText txtQueryMem;

		protected HtmlInputText txtOrderAccount;

		protected HtmlInputText txtRemark;

		protected HtmlSelect sltMemLevelID;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected Button btnRptEmptyBillsQuery;

		protected Repeater rptEmptyBillsList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected HtmlInputCheckBox chkAllowPwd;

		protected Pay ucPay;

		protected QuickSearch QuickSearch1;

		private OrderLog bllOrderLog = new OrderLog();

		private OrderDetail bllOrderDetail = new OrderDetail();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				this.txtStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				this.chkAllowPwd.Checked = PubFunction.curParameter.bolPwd;
				this.BindEmptyBillsList(this.QueryCondition());
				if (PubFunction.curParameter.dataAuthority == 1)
				{
					if (this._UserShopID > 1)
					{
						this.sltShop.Value = this._UserShopID.ToString();
						this.sltShop.Attributes.Add("disabled", "disabled");
					}
				}
			}
		}

		private void BindEmptyBillsList(string strSql)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " and OrderType=3";
			strSql += " and Mem.MemID = OrderLog.OrderMemID and OrderLog.OrderShopID = SysShop.ShopID and OrderLog.OrderUserID = SysUser.UserID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "OrderShopID", strSql);
			DataTable dtEmptyBills = this.bllOrderLog.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.rptEmptyBillsList.DataSource = dtEmptyBills;
			this.rptEmptyBillsList.DataBind();
			PageBase.BindSerialRepeater(this.rptEmptyBillsList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindEmptyBillsList(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindEmptyBillsList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value;
			string strMemLevelID = this.sltMemLevelID.Value;
			string strMemShopID = this.sltShop.Value;
			string strRemark = PubFunction.RemoveSpace(this.txtRemark.Value);
			string strOrderAccount = PubFunction.RemoveSpace(this.txtOrderAccount.Value);
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" 1=1 ");
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
				strSql.AppendFormat(" and OrderShopID={0}", int.Parse(strMemShopID));
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat(" and OrderCreateTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat(" and OrderCreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			if (strRemark != "")
			{
				strSql.AppendFormat(" and OrderRemark like '%{0}%' ", strRemark);
			}
			if (strOrderAccount != "")
			{
				strSql.AppendFormat(" and OrderAccount='{0}'", strOrderAccount);
			}
			return strSql.ToString();
		}

		protected void btnRptEmptyBillsQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.BindEmptyBillsList(this.QueryCondition());
		}

		protected void rptEmptyBillsList_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				Repeater rptItem = (Repeater)e.Item.FindControl("rptEmptyBillsDetailList");
				if (rptItem != null)
				{
					int Count = this.NetPagerParameter.RecordCount;
					string strSql = " OrderDetail.OrderID=OrderLog.OrderID and OrderDetail.GoodsID=Goods.GoodsID";
					strSql = strSql + " and OrderDetail.OrderID=" + dr["OrderID"];
					DataTable dtDetail = this.bllOrderDetail.GetListSP(strSql).Tables[0];
					rptItem.DataSource = dtDetail;
					rptItem.DataBind();
					foreach (RepeaterItem rp in rptItem.Items)
					{
						Label lblDetailNum = (Label)rp.FindControl("lblDetailNumber");
						lblDetailNum.Text = (rp.ItemIndex + 1).ToString();
					}
				}
			}
		}

		protected string GetGoodsType(int intGoodsType, float strNumber)
		{
			string strGoodsType = "";
			switch (intGoodsType)
			{
			case 0:
				strGoodsType = "普通商品";
				break;
			case 1:
				strGoodsType = "服务项目";
				break;
			}
			return strGoodsType;
		}

		protected void btnRptEmptyBillsExcel_Click(object sender, EventArgs e)
		{
			OrderLog bllOrderLog = new OrderLog();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += " and OrderType=3";
			strSql += " and Mem.MemID = OrderLog.OrderMemID and OrderLog.OrderShopID = SysShop.ShopID and Mem.MemUserID = SysUser.UserID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "OrderShopID", strSql);
			DataTable dtEmptyBills = bllOrderLog.GetListSP(100000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.EmptyBillsExcel(dtEmptyBills, this._UserName);
		}
	}
}
