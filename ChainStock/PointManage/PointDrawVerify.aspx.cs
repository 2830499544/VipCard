using Chain.BLL;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.PointManage
{
	public class PointDrawVerify : PageBase
	{
		protected HtmlForm frmExchangeVerify;

		protected Literal ltlTitle;

		protected HtmlInputText txtExchangeRemark;

		protected HtmlInputText txtDrawType;

		protected HtmlInputText txtShopSmsAccount;

		protected HtmlSelect sltShop;

		protected HtmlSelect sltCzlx;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected Button btnShopSmsQuery;

		protected Repeater rptExchangeVerify;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected string strPid = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["PID"] != null)
				{
					string pid = base.Request.QueryString["PID"];
					this.strPid = pid;
					if (pid == "149")
					{
						this.txtDrawType.Value = "2";
						PubFunction.BindShopSelectNew(this._UserShopID, this.sltShop, true, "2");
					}
					else if (pid == "150")
					{
						this.txtDrawType.Value = "3";
						this.ltlTitle.Text = "主页 > 商家管理 >商家积分提现记录 ";
						PubFunction.BindShopSelectNew(this._UserShopID, this.sltShop, true, "3");
					}
					else
					{
						PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
					}
				}
				this.BindExchangeList(this.QueryCondition());
				this.txtStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
			}
		}

		protected void BindExchangeList(string strSql)
		{
			Chain.BLL.PointDraw draw = new Chain.BLL.PointDraw();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql = strSql + " and  PointDraw.DrawShopID=SysShop.ShopID  and PointDraw.DrawCreateUserID=SysUser.UserID  and SysShop.ShopType=" + this.txtDrawType.Value;
			DataTable dtGiftExchange = draw.GetVerifyListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.rptExchangeVerify.DataSource = dtGiftExchange;
			this.rptExchangeVerify.DataBind();
			PageBase.BindSerialRepeater(this.rptExchangeVerify, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindExchangeList(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindExchangeList(this.QueryCondition());
		}

		protected void btnPointDrawQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.BindExchangeList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strMemShopID = this.sltShop.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" 1=1  ");
			if (strMemShopID != "")
			{
				string str = PubFunction.GetMemListShopAuthority(int.Parse(strMemShopID), "DrawShopID", strSql.ToString());
				strSql.AppendFormat(" and {0} ", str);
			}
			else
			{
				string str = PubFunction.GetMemListShopAuthority(this._UserShopID, "DrawShopID", strSql.ToString());
				strSql.AppendFormat(" and {0} ", str);
			}
			if (this.txtShopSmsAccount.Value.Trim() != "")
			{
				strSql.AppendFormat(" and DrawAccount='{0}'", this.txtShopSmsAccount.Value.Trim());
			}
			if (this.sltCzlx.Value != "")
			{
				strSql.AppendFormat(" and DrawStatus = '{0}'", this.sltCzlx.Value);
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat(" and DrawCreateTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat(" and DrawCreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			return strSql.ToString();
		}

		protected void rptMemGiftList_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				Repeater rptItem = (Repeater)e.Item.FindControl("rptExchangeVerifyDetail");
				Label lblStatus = (Label)e.Item.FindControl("lblDrawStatus");
				HtmlTableCell td = e.Item.FindControl("tdHandle") as HtmlTableCell;
				if (lblStatus != null)
				{
					string text = dr["DrawStatus"].ToString();
					if (text != null)
					{
						if (!(text == "0"))
						{
							if (!(text == "1"))
							{
								if (text == "2")
								{
									lblStatus.Text = "退回";
								}
							}
							else
							{
								lblStatus.Text = "通过审核";
								lblStatus.ForeColor = Color.Blue;
							}
						}
						else
						{
							lblStatus.Text = "待审核";
							lblStatus.ForeColor = Color.Red;
						}
					}
				}
				SysGroup bllSysGroup = new SysGroup();
				DataTable dt = bllSysGroup.GetSysGroupByParentID(1);
				DataRow[] Row = dt.Select(string.Concat(new object[]
				{
					"GroupID=",
					this._UserGroupID,
					" or ParentGroupID=",
					this._UserGroupID
				}));
				if (Row.Length <= 0)
				{
					td.Style.Add("display", "none");
				}
			}
		}
	}
}
