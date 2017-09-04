using Chain.BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.SystemManage
{
	public class ReturnPointLog : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmShopPoint;

		protected HtmlInputHidden hdShopID;

		protected Literal ltlTitle;

		protected HtmlInputText txtShopPointAccount;

		protected HtmlSelect sltShop;

		protected HtmlInputText txtReturnStartTime;

		protected HtmlInputText txtReturnEndTime;

		protected Button btnShopPointQuery;

		protected Repeater gvShopPointList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.txtReturnStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtReturnEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				if (base.Request.QueryString["PID"] != null)
				{
					string pid = base.Request.QueryString["PID"].ToString();
				}
				this.GetShopPointList(this.QueryCondition());
				PubFunction.BindShopSelectNew(this._UserShopID, this.sltShop, true, "3");
			}
		}

		protected void btnShopPointQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.GetShopPointList(this.QueryCondition());
		}

		private void GetShopPointList(string strSql)
		{
			Chain.BLL.ReturnPointLog bllShopPointLog = new Chain.BLL.ReturnPointLog();
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
			StringBuilder strSql = new StringBuilder();
			strSql.AppendFormat(" SysShop.ShopID=ReturnPointLog.ReturnShopID ", new object[0]);
			string strMemShopID = this.sltShop.Value;
			if (strMemShopID != "")
			{
				strSql.AppendFormat("and SysShop.ShopID={0}", int.Parse(strMemShopID));
			}
			if (this.txtShopPointAccount.Value.Trim() != "")
			{
				strSql.AppendFormat(" and OrderAccount='{0}' ", this.txtShopPointAccount.Value.Trim());
			}
			if (this.txtReturnStartTime.Value != "")
			{
				strSql.AppendFormat("and CreateTime>='{0}' ", this.txtReturnStartTime.Value);
			}
			if (this.txtReturnEndTime.Value != "")
			{
				strSql.AppendFormat("and CreateTime<='{0}'", PubFunction.TimeEndDay(this.txtReturnEndTime.Value));
			}
			return strSql.ToString();
		}
	}
}
