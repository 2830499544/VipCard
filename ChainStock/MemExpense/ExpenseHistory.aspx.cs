using Chain.BLL;
using ChainStock.Controls;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.MemExpense
{
	public class ExpenseHistory : PageBase
	{
		protected HtmlForm frmExpHistory;

		protected HtmlInputHidden txtUser;

		protected HtmlInputHidden txtShopid;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected HtmlInputText txtQueryMem;

		protected Button Button1;

		protected Button btnExpenseExcel;

		protected HtmlInputText txtOrderAccount;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected HtmlSelect sltConsumptionShop;

		protected HtmlSelect sltDiscountMoney;

		protected HtmlInputText txtDiscountMoney;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltOrderType;

		protected HtmlSelect sltTotalMoney;

		protected HtmlInputText txtTotalMoney;

		protected HtmlInputText txtRemark;

		protected Repeater rptExpenseHistory;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected Label lblPrintTitle;

		protected Label lblPrintFoot;

		protected QuickSearch QuickSearch1;

		private OrderLog bllOrderLog = new OrderLog();

		private OrderDetail bllOrderDetail = new OrderDetail();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				this.sltMemLevelID.Items.Insert(1, new ListItem("散 客", "-1"));
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				this.txtTotalMoney.Value = "0";
				this.txtDiscountMoney.Value = "0";
				PubFunction.BindShopSelect(this._UserShopID, this.sltConsumptionShop, true);
				this.txtStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString();
				this.txtEndTime.Value = DateTime.Now.ToString();
				this.txtUser.Value = this._UserName;
				this.txtShopid.Value = this._UserShopID.ToString();
				PubFunction.Get_PrintTitle(ref this.lblPrintTitle, ref this.lblPrintFoot, this._UserShopID);
				if (PubFunction.curParameter.dataAuthority != 0)
				{
					if (this._UserShopID > 1)
					{
						this.sltConsumptionShop.Value = this._UserShopID.ToString();
						this.sltConsumptionShop.Attributes.Add("disabled", "disabled");
					}
				}
				this.BindExpenseHistory(this.QueryCondition());
				this.PointNum.Value = PubFunction.GetPointNum("XFJL");
			}
		}

		private void BindExpenseHistory(string strSql)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " and OrderType <> 3 ";
			strSql += " and OrderLog.OrderShopID = SysShop.ShopID and OrderLog.OrderMemID = Mem.MemID  and OrderLog.OrderUserID = SysUser.UserID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "OrderShopID", strSql);
			DataTable dtExpenseHistory = this.bllOrderLog.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.rptExpenseHistory.DataSource = dtExpenseHistory;
			this.rptExpenseHistory.DataBind();
			PageBase.BindSerialRepeater(this.rptExpenseHistory, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindExpenseHistory(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value;
			string strMemShopID = this.sltShop.Value;
			string strOrderType = this.sltOrderType.Value;
			string strTotalSymbols = this.sltTotalMoney.Value;
			string strSltLevelID = this.sltMemLevelID.Value;
			string strRemark = PubFunction.RemoveSpace(this.txtRemark.Value);
			string strTotalMoney = (this.txtTotalMoney.Value.Trim() != "") ? this.txtTotalMoney.Value.Trim() : "0";
			decimal dclTotalMoney = decimal.Parse(strTotalMoney);
			string strDiscountSymbols = this.sltDiscountMoney.Value;
			string strDiscountMoney = (this.txtDiscountMoney.Value.Trim() != "") ? this.txtDiscountMoney.Value.Trim() : "0";
			decimal dclDiscountMoney = decimal.Parse(strDiscountMoney);
			string strOrderAccount = PubFunction.RemoveSpace(this.txtOrderAccount.Value);
			string strOrderShopID = this.sltConsumptionShop.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1");
			if (strQueryMem != "")
			{
				strSql.AppendFormat(" and (MemCard = '{0}' or MemName like '%{0}%' or MemMobile = '{0}' or MemCardNumber = '{0}' )", strQueryMem);
			}
			if (strMemShopID != "")
			{
				strSql.AppendFormat(" and MemShopID={0}", int.Parse(strMemShopID));
			}
			if (strOrderType != "")
			{
				if (int.Parse(strOrderType) != -1)
				{
					strSql.AppendFormat(" and OrderType={0}", int.Parse(strOrderType));
				}
				else
				{
					strSql.AppendFormat(" and OrderMemID={0}", 0);
				}
			}
			if (dclTotalMoney != 0m)
			{
				strSql.AppendFormat(" and OrderTotalMoney" + strTotalSymbols + "{0}", dclTotalMoney);
			}
			if (dclDiscountMoney != 0m)
			{
				strSql.AppendFormat(" and OrderDiscountMoney" + strDiscountSymbols + "{0}", dclDiscountMoney);
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat(" and OrderCreateTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat(" and OrderCreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			if (strSltLevelID != "")
			{
				strSql.AppendFormat(" and MemLevelID={0}", int.Parse(strSltLevelID));
			}
			if (strRemark != "")
			{
				strSql.AppendFormat(" and OrderRemark like '%{0}%' ", strRemark);
			}
			if (strOrderAccount != "")
			{
				strSql.AppendFormat(" and OrderAccount='{0}'", strOrderAccount);
			}
			if (strOrderShopID != "")
			{
				strSql.AppendFormat(" and OrderShopID={0}", int.Parse(strOrderShopID));
			}
			return strSql.ToString();
		}

		protected bool GetExpenseIsReturn(string strAccount, int OrderType)
		{
			bool result;
			if (OrderType == 7)
			{
				result = false;
			}
			else
			{
				DataTable dt = new OrderLog().GetList(" OldAccount='" + strAccount + "' and OrderType=6 ").Tables[0];
				result = (dt.Rows.Count <= 0);
			}
			return result;
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

		public bool GetPrint(int OrderType)
		{
			return OrderType < 3 || OrderType == 7;
		}

		protected string GetMemCard(string strMemCard)
		{
			string memCard;
			if (strMemCard == "0")
			{
				memCard = "无卡号";
			}
			else
			{
				memCard = strMemCard;
			}
			return memCard;
		}

		protected string GetOrderType(int intOrderType)
		{
			string strOrderType = "";
			switch (intOrderType)
			{
			case 0:
				strOrderType = "快速消费";
				break;
			case 1:
				strOrderType = "计时消费";
				break;
			case 2:
				strOrderType = "商品消费";
				break;
			case 4:
				strOrderType = "消费撤单";
				break;
			case 6:
				strOrderType = "消费退货";
				break;
			case 7:
				strOrderType = "计次消费";
				break;
			}
			return strOrderType;
		}

		protected void btnRptExpenseQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.BindExpenseHistory(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindExpenseHistory(this.QueryCondition());
		}

		protected void rptExpenseHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				if (int.Parse(dr["OrderType"].ToString()) > 1)
				{
					Repeater rptItem = (Repeater)e.Item.FindControl("rptExpenseDetail");
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
		}

		protected void btnExpenseExcel_Click(object sender, EventArgs e)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += " and OrderType<>3 ";
			strSql += " and OrderLog.OrderShopID = SysShop.ShopID and OrderLog.OrderMemID = Mem.MemID  and OrderLog.OrderUserID = SysUser.UserID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "OrderShopID", strSql);
			DataTable dtExpenseHistory = this.bllOrderLog.GetListSP(1000000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.ExpenseHistory(dtExpenseHistory, this._UserName);
		}
	}
}
