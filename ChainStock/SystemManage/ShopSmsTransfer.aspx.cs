using Chain.BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.SystemManage
{
	public class ShopSmsTransfer : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmShopSms;

		protected HtmlInputHidden hdShopID;

		protected Literal ltlTitle;

		protected HtmlInputText txtShopSmsAccount;

		protected HtmlSelect sltShop;

		protected HtmlSelect sltCzlx;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected Button btnShopSmsQuery;

		protected Repeater gvShopSmsList;

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
					if (pid == "159")
					{
						PubFunction.BindShopSelectByShopType(this._UserShopID, this.sltShop, true, 2);
					}
					else if (pid == "160")
					{
						PubFunction.BindShopSelectByShopType(this._UserShopID, this.sltShop, true, 3);
					}
					else if (pid == "158")
					{
						PubFunction.BindShopSelectByShopType(this._UserShopID, this.sltShop, true, 1);
					}
					else
					{
						PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
					}
					PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
					this.sltShop.Items.Remove(new ListItem("深圳-运营商", "1"));
					this.GetStaffClassList(this.QueryCondition());
				}
			}
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

		private void GetStaffClassList(string strSql)
		{
			SysShopCmsLog bllStaffClass = new SysShopCmsLog();
			int Counts = this.NetPagerParameter.RecordCount;
			DataTable dtStaffClass = bllStaffClass.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvShopSmsList.DataSource = dtStaffClass;
			this.gvShopSmsList.DataBind();
			PageBase.BindSerialRepeater(this.gvShopSmsList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.GetStaffClassList(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.GetStaffClassList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strMemShopID = this.sltShop.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" SysShopCmsLog.UserID = SysUser.UserID AND SysShopCmsLog.OutShopID = SysShop.ShopID ");
			if (strMemShopID != "")
			{
				strSql.AppendFormat(" and OutShopID={0}", int.Parse(strMemShopID));
			}
			if (this.txtShopSmsAccount.Value.Trim() != "")
			{
				strSql.AppendFormat(" and ShopCmsAccount='{0}'", this.txtShopSmsAccount.Value.Trim());
			}
			if (this.sltCzlx.Value != "")
			{
				strSql.AppendFormat(" and ShopCmsType = '{0}'", this.sltCzlx.Value);
			}
			if (base.Request.QueryString["PID"] != null)
			{
				string pid = base.Request.QueryString["PID"].ToString();
				if (pid == "159")
				{
					strSql.AppendFormat(" and SysShop.ShopType = 2", new object[0]);
				}
				if (pid == "160")
				{
					strSql.AppendFormat(" and SysShop.ShopType = 3", new object[0]);
				}
				if (pid == "158")
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

		protected void btnShopSmsQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.GetStaffClassList(this.QueryCondition());
		}

		protected void gvShopSmsList_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				Label lblShopCmsType = e.Item.FindControl("lblShopCmsType") as Label;
				switch (Convert.ToInt32(dr["ShopCmsType"]))
				{
				case 0:
					lblShopCmsType.Text = "总店给联盟商充值短信";
					break;
				case 1:
					lblShopCmsType.Text = "总店给联盟商扣除短信";
					break;
				case 2:
					lblShopCmsType.Text = "联盟商给商家充值短信";
					break;
				case 3:
					lblShopCmsType.Text = "联盟商给商家扣除短信";
					break;
				case 4:
					lblShopCmsType.Text = "商家使用短信";
					break;
				}
			}
		}
	}
}
