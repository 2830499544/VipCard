using Chain.BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.SystemManage
{
	public class ShopPointTransfer : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmShopPoint;

		protected HtmlInputHidden hdShopID;

		protected Literal ltlTitle;

		protected HtmlInputText txtShopPointAccount;

		protected HtmlSelect sltShop;

		protected HtmlSelect sltCzlx;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected Button btnShopPointQuery;

		protected Repeater gvShopPointList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.txtStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				if (base.Request.QueryString["PID"] != null)
				{
					string pid = base.Request.QueryString["PID"].ToString();
					if (pid == "151")
					{
						PubFunction.BindShopSelectByShopType(this._UserShopID, this.sltShop, true, 2);
					}
					else if (pid == "152")
					{
						PubFunction.BindShopSelectByShopType(this._UserShopID, this.sltShop, true, 3);
					}
					else if (pid == "154")
					{
						PubFunction.BindShopSelectByShopType(this._UserShopID, this.sltShop, true, 1);
					}
					else
					{
						PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
					}
				}
				this.GetShopPointList(this.QueryCondition());
			}
		}

		protected void btnShopPointQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.GetShopPointList(this.QueryCondition());
		}

		private void GetShopPointList(string strSql)
		{
			SysShopPointLog bllShopPointLog = new SysShopPointLog();
			int Counts = this.NetPagerParameter.RecordCount;
			DataTable dtShopPoint = bllShopPointLog.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvShopPointList.DataSource = dtShopPoint;
			this.gvShopPointList.DataBind();
			PageBase.BindSerialRepeater(this.gvShopPointList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.GetShopPointList(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.GetShopPointList(this.QueryCondition());
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

		protected string QueryCondition()
		{
			string strMemShopID = this.sltShop.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" SysShopPointLog.UserID = SysUser.UserID AND SysShopPointLog.OutShopID = SysShop.ShopID ");
			if (strMemShopID != "")
			{
				strSql.AppendFormat(" and SysShopPointLog.OutShopID in ({0})", int.Parse(strMemShopID));
			}
			else
			{
				strSql.AppendFormat(PubFunction.GetShopAuthority(this._UserShopID, "SysShopPointLog.OutShopID", ""), new object[0]);
			}
			if (this.sltCzlx.Value != "")
			{
				strSql.AppendFormat(" and ShopPointType = '{0}'", this.sltCzlx.Value);
			}
			if (this.txtShopPointAccount.Value.Trim() != "")
			{
				strSql.AppendFormat(" and ShopPointAccount='{0}'", this.txtShopPointAccount.Value.Trim());
			}
			if (base.Request.QueryString["PID"] != null)
			{
				string pid = base.Request.QueryString["PID"].ToString();
				if (pid == "151")
				{
					strSql.AppendFormat(" and SysShop.ShopType = 2", new object[0]);
				}
				if (pid == "152")
				{
					strSql.AppendFormat(" and SysShop.ShopType = 3", new object[0]);
				}
				if (pid == "154")
				{
					strSql.AppendFormat(" and SysShop.ShopType = 1", new object[0]);
				}
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat("and CreateTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat("and CreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			return strSql.ToString();
		}

		protected void gvShopPointList_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				Label lblShopPointype = e.Item.FindControl("lblShopPointType") as Label;
				switch (Convert.ToInt32(dr["ShopPointType"]))
				{
				case 0:
					lblShopPointype.Text = "充值积分";
					break;
				case 1:
					lblShopPointype.Text = "扣除积分";
					break;
				case 2:
					lblShopPointype.Text = "发放积分";
					break;
				case 3:
					lblShopPointype.Text = "返利积分";
					break;
				case 4:
					lblShopPointype.Text = "回收积分";
					break;
				case 5:
					lblShopPointype.Text = "积分使用";
					break;
				case 6:
					lblShopPointype.Text = "积分提现";
					break;
				case 7:
					lblShopPointype.Text = "撤单返还积分";
					break;
				}
			}
		}
	}
}
