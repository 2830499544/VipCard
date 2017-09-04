using Chain.BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.StockManage
{
	public class GoodsAllotState : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm form1;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected HtmlInputHidden HiddenShopID;

		protected HtmlInputHidden HiddenType;

		protected HtmlInputText txtQuery;

		protected HtmlSelect sltUserID;

		protected HtmlSelect SelAllotZT;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected Button btnGoodsLogQuery;

		protected Repeater rptGoodsLog;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected Label lblPrintTitle;

		protected Label lblPrintFoot;

		public int shopi;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindUserSelect(this._UserShopID, this.sltUserID, true, false);
				this.Get_ParameterList(this.QueryCondition());
				this.shopi = this._UserShopID;
			}
		}

		private void Get_ParameterList(string strSql)
		{
			Chain.BLL.GoodsAllot BLLAllot = new Chain.BLL.GoodsAllot();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " and GoodsAllot.AllotOutShopID > 0";
			object obj = strSql;
			strSql = string.Concat(new object[]
			{
				obj,
				" and (GoodsAllot.AllotOutShopID=",
				this._UserShopID,
				" or  GoodsAllot.AllotInShopID=",
				this._UserShopID,
				")"
			});
			strSql += " and  GoodsAllot.AllotOutShopID = SysShop.ShopID and GoodsAllot.AllotUserID = SysUser.UserID";
			DataTable db = BLLAllot.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			string IntAllotZT = this.SelAllotZT.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1");
			if (strQuery != "")
			{
				strSql.AppendFormat(" and ((AllotAccount like '%{0}%') or (AllotID in( select AllotDetailAllotID from GoodsAllotDetail where AllotDetailGoodsID in(select goodsid from goods where name like '%{0}%'))))", strQuery);
			}
			if (strUserlID != "")
			{
				strSql.AppendFormat(" and GoodsAllot.AllotUserID={0}", int.Parse(strUserlID));
			}
			if (IntAllotZT != "0")
			{
				strSql.AppendFormat(" and GoodsAllot.AllotState={0}", int.Parse(IntAllotZT));
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat(" and AllotCreateTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat(" and AllotCreateTime<='{0}'", DateTime.Parse(this.txtEndTime.Value + " 23:59:59"));
			}
			return strSql.ToString();
		}

		protected string GetStateType(int Type)
		{
			string strType = "";
			switch (Type)
			{
			case 1:
				strType = "已申请";
				break;
			case 2:
				strType = "已发货";
				break;
			case 3:
				strType = "已收货";
				break;
			case 4:
				strType = "已撤单";
				break;
			}
			return strType;
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Get_ParameterList(" 1=1 ");
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
					string strSql = "  GoodsAllotDetail.AllotDetailGoodsID=Goods.GoodsID ";
					strSql = strSql + " and GoodsAllotDetail.AllotDetailAllotID=" + dr["AllotID"];
					GoodsAllotDetail BLLAllotDetail = new GoodsAllotDetail();
					DataTable dtDetail = BLLAllotDetail.GetListSP(strSql).Tables[0];
					rptItem.DataSource = dtDetail;
					rptItem.DataBind();
					foreach (RepeaterItem rp in rptItem.Items)
					{
						Label lblDetailNum = (Label)rp.FindControl("lblDetailNumber");
						lblDetailNum.Text = (rp.ItemIndex + 1).ToString();
					}
				}
				Panel PLfahuo = (Panel)e.Item.FindControl("PLfahuo");
				Panel PLshouhuo = (Panel)e.Item.FindControl("PLshouhuo");
				Panel PanCedans = (Panel)e.Item.FindControl("PanCedans");
				if (dr["AllotOutShopID"].ToString() == this._UserShopID.ToString() && dr["AllotState"].ToString() == "1")
				{
					PLfahuo.Visible = true;
				}
				else
				{
					PLfahuo.Visible = false;
				}
				if (dr["AllotInShopID"].ToString() == this._UserShopID.ToString() && dr["AllotState"].ToString() == "2")
				{
					PLshouhuo.Visible = true;
				}
				else
				{
					PLshouhuo.Visible = false;
				}
				if (dr["AllotState"].ToString() == "4")
				{
					PanCedans.Visible = false;
				}
			}
		}
	}
}
