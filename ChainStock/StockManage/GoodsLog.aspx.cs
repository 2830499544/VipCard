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
	public class GoodsLog : PageBase
	{
		protected HtmlForm form1;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected Button btnRptExpenseExcel;

		protected HtmlInputHidden txtUser;

		protected HtmlInputText txtQuery;

		protected HtmlSelect sltUserID;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected HtmlSelect sltChangeType;

		protected Button btnGoodsLogQuery;

		protected Repeater rptGoodsLog;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected Label lblPrintTitle;

		protected Label lblPrintFoot;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindUserSelect(this._UserShopID, this.sltUserID, true, false);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, this._UserShopID, this._UserShopID != 1);
				this.BingChangeType();
				this.Get_ParameterList(this.QueryCondition());
				this.txtUser.Value = this._UserName;
				PubFunction.Get_PrintTitle(ref this.lblPrintTitle, ref this.lblPrintFoot, this._UserShopID);
			}
			this.PointNum.Value = PubFunction.GetPointNum("CKRKML");
		}

		private void Get_ParameterList(string strSql)
		{
			Chain.BLL.GoodsLog bllGoodsLog = new Chain.BLL.GoodsLog();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " and GoodsLog.ShopID>0";
			strSql += " and GoodsLog.ShopID = SysShop.ShopID and GoodsLog.UserID = SysUser.UserID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "GoodsLog.ShopID", strSql);
			DataTable db = bllGoodsLog.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.rptGoodsLog.DataSource = db;
			this.rptGoodsLog.DataBind();
			PageBase.BindSerialRepeater(this.rptGoodsLog, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.Get_ParameterList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strQuery = PubFunction.RemoveSpace(this.txtQuery.Value);
			string strUserlID = this.sltUserID.Value;
			string strShopID = this.sltShop.Value;
			int intChangeType = int.Parse(this.sltChangeType.Value);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1");
			if (strQuery != "")
			{
				strSql.AppendFormat(" and ((GoodsAccount like '%{0}%') or (id in( select goodslogid from goodslogdetail where \r\n                                    goodsid in(select goodsid from goods where name like '%{0}%'))))", strQuery);
			}
			if (strShopID != "")
			{
				strSql.AppendFormat(" and (GoodsLog.ShopID={0} or ChangeShopID={0})", int.Parse(strShopID));
			}
			if (strUserlID != "")
			{
				strSql.AppendFormat(" and GoodsLog.UserID={0}", int.Parse(strUserlID));
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat(" and CreateTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				try
				{
					DateTime txtEndTimes = DateTime.Parse(Convert.ToDateTime(this.txtEndTime.Value).ToString("yyyy-MM-dd") + " 23:59:59");
					strSql.AppendFormat(" and CreateTime<='{0}'", txtEndTimes);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			if (intChangeType != -1)
			{
				strSql.AppendFormat(" and Type = {0}", intChangeType);
			}
			return strSql.ToString();
		}

		protected string GetType(int Type)
		{
			string strType = "";
			switch (Type)
			{
			case 1:
				strType = "商品入库";
				break;
			case 2:
				strType = "商品销售出库";
				break;
			case 3:
				strType = "商品挂单出库";
				break;
			case 4:
				strType = "撤销订单入库";
				break;
			case 5:
				strType = "商品撤单入库";
				break;
			case 7:
				strType = "商品退货入库";
				break;
			case 8:
				strType = "商品编辑库存入库";
				break;
			case 9:
				strType = "商品编辑库存出库";
				break;
			case 10:
				strType = "商品入库撤单";
				break;
			case 11:
				strType = "商品入库(已撤单)";
				break;
			case 12:
				strType = "商品调拨出库";
				break;
			case 13:
				strType = "商品调拨入库";
				break;
			case 14:
				strType = "商品库存批量导入";
				break;
			}
			return strType;
		}

		private void BingChangeType()
		{
			this.sltChangeType.Items.Add(new ListItem("===== 请选择 =====", "-1"));
			this.sltChangeType.Items.Add(new ListItem("商品入库", "1"));
			this.sltChangeType.Items.Add(new ListItem("商品销售出库", "2"));
			this.sltChangeType.Items.Add(new ListItem("商品挂单出库", "3"));
			this.sltChangeType.Items.Add(new ListItem("撤销订单入库", "4"));
			this.sltChangeType.Items.Add(new ListItem("商品撤单入库", "5"));
			this.sltChangeType.Items.Add(new ListItem("商品退货入库", "7"));
			this.sltChangeType.Items.Add(new ListItem("商品编辑库存入库", "8"));
			this.sltChangeType.Items.Add(new ListItem("商品编辑库存出库", "9"));
			this.sltChangeType.Items.Add(new ListItem("商品入库撤单", "10"));
			this.sltChangeType.Items.Add(new ListItem("商品入库(已撤单)", "11"));
			this.sltChangeType.Items.Add(new ListItem("商品调拨出库", "12"));
			this.sltChangeType.Items.Add(new ListItem("商品调拨入库", "13"));
			this.sltChangeType.Items.Add(new ListItem("商品库存批量导入", "14"));
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void btnGoodsLogQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void btnRptExpenseExcel_Click(object sender, EventArgs e)
		{
			Chain.BLL.GoodsLog bllGoodsLog = new Chain.BLL.GoodsLog();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			string strAgent = "";
			strSql += " and GoodsLog.ShopID>0";
			strSql += " and GoodsLog.ShopID = SysShop.ShopID and GoodsLog.UserID = SysUser.UserID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "GoodsLog.ShopID", strSql);
			DataTable db = bllGoodsLog.GetListSP(100000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.GoodsLogExcel(db, this._UserName, strAgent);
		}

		protected void rptGoodsLog_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				Repeater rptItem = (Repeater)e.Item.FindControl("rptGoodsLogDetail");
				if (rptItem != null)
				{
					int Count = this.NetPagerParameter.RecordCount;
					string strSql = "  GoodsLogDetail.GoodsID=Goods.GoodsID ";
					strSql = strSql + " and GoodsLogDetail.GoodsLogID=" + dr["ID"];
					GoodsLogDetail bllGoodsLogDetail = new GoodsLogDetail();
					DataTable dtDetail = bllGoodsLogDetail.GetListSP(strSql).Tables[0];
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
}
