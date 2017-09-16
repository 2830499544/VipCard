using Chain.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.MicroWebsite
{
	public class GoodsAudit : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmMicroExpHistory;

		protected HtmlInputHidden txtUser;

		protected HtmlInputHidden txtShopid;

		protected Literal ltlTitle;

		protected Button btnExpenseExcel;

		protected HtmlInputText txtQueryMem;

		protected HtmlSelect sltMemLevelID;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected HtmlInputText txtOrderAccount;

		protected HtmlSelect sltStatus;

		protected Button Button1;

		protected Repeater rptExpenseHistory;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected HtmlInputCheckBox chkSMS;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				this.sltMemLevelID.Items.Insert(1, new ListItem("散 客", "-1"));
				this.txtStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtUser.Value = this._UserName;
				this.txtShopid.Value = this._UserShopID.ToString();
				this.BindStatus();
				this.BindExpenseHistory(this.QueryCondition());
				this.chkSMS.Checked = (this.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByCommodityConsumption);
			}
		}

		private void BindExpenseHistory(string strSql)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " and MicroWebsiteOrderLog.MicroOrderShopID = SysShop.ShopID and MicroWebsiteOrderLog.MicroOrderMemID = Mem.MemID and MicroOrderStatus !=2 ";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "MicroOrderShopID", strSql);
			DataTable dtExpenseHistory = new MicroWebsiteOrderLog().GetListSP1(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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

		protected string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value;
			string strSltLevelID = this.sltMemLevelID.Value;
			string strOrderAccount = PubFunction.RemoveSpace(this.txtOrderAccount.Value);
			string sltStatusValue = this.sltStatus.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1");
			if (strQueryMem != "")
			{
				strSql.AppendFormat(" and (MemCard like '%{0}%' or MemName like '%{0}%' or MemMobile like '%{0}%')", strQueryMem);
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat(" and MicroOrderCreateTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat(" and MicroOrderCreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			if (strSltLevelID != "")
			{
				strSql.AppendFormat(" and MemLevelID={0}", int.Parse(strSltLevelID));
			}
			if (strOrderAccount != "")
			{
				strSql.AppendFormat(" and MicroOrderAccount='{0}'", strOrderAccount);
			}
			if (sltStatusValue != "0")
			{
				strSql.AppendFormat(" and MicroOrderStatus={0}", sltStatusValue);
			}
			return strSql.ToString();
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

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindExpenseHistory(this.QueryCondition());
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
				Repeater rptItem = (Repeater)e.Item.FindControl("rptExpenseDetail");
				if (rptItem != null)
				{
					int Count = this.NetPagerParameter.RecordCount;
					string strSql = " MicroWebsiteOrderLogDetail.MicroOrderID=MicroWebsiteOrderLog.MicroOrderID and MicroWebsiteOrderLogDetail.MicroGoodsID=MicroWebsiteGoods.MicroGoodsID";
					strSql = strSql + " and MicroWebsiteOrderLogDetail.MicroOrderID=" + dr["MicroOrderID"];
					DataTable dtDetail = new MicroWebsiteOrderLogDetail().GetListSP(strSql).Tables[0];
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

		protected void btnExpenseExcel_Click(object sender, EventArgs e)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += " and MicroWebsiteOrderLog.MicroOrderShopID = SysShop.ShopID and MicroWebsiteOrderLog.MicroOrderMemID = Mem.MemID and MicroOrderStatus !=2 ";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "MicroOrderShopID", strSql);
			DataTable dtExpenseHistory = new MicroWebsiteOrderLog().GetListSP1(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.MicroExpenseHistory(dtExpenseHistory, this._UserName);
		}

		private void BindStatus()
		{
			List<ListItem> listItem = new List<ListItem>
			{
				new ListItem("==== 请选择 ==== ", "0"),
				new ListItem("未支付", "1"),
				new ListItem("已支付", "4"),
				new ListItem("已退回", "3")
			};
			this.sltStatus.Items.AddRange(listItem.ToArray());
		}

		protected string GetTypeStr(object objMicroOrderStatus)
		{
			string result;
			switch (Convert.ToInt32(objMicroOrderStatus))
			{
			case 1:
				result = "未支付";
				break;
			case 2:
				result = "已审核";
				break;
			case 3:
				result = "已退回";
				break;
			case 4:
				result = "已支付";
				break;
			default:
				result = "未知类型";
				break;
			}
			return result;
		}
	}
}
