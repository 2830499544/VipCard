using Chain.BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.SystemManage
{
	public class ShopSendCard : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmShopList;

		protected Literal ltlTitle;

		protected HtmlInputHidden txtShopType;

		protected HtmlSelect sltShop;

		protected Button btnShopBuyCardQuery;

		protected Repeater gvShopBuyCard;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request["ShopType"] != null)
				{
					this.txtShopType.Value = base.Request["ShopType"].ToString();
					PubFunction.BindShopSelectNew(this._UserShopID, this.sltShop, true, base.Request["ShopType"].ToString());
				}
				else
				{
					PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				}
				this.GetShopBuyCardRecordList(this.QueryCondition());
			}
		}

		protected void btnShopBuyCardQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.GetShopBuyCardRecordList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strMemShopID = this.sltShop.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" SysShopBuyCard.BuyCardShopid = SysShop.ShopID AND SysShopBuyCard.UserID = SysUser.UserID ");
			if (strMemShopID != "")
			{
				strSql.AppendFormat(" and BuyCardShopid={0}", int.Parse(strMemShopID));
			}
			if (base.Request["ShopType"] != null)
			{
				strSql.AppendFormat(" and SysShop.ShopType={0}", base.Request["ShopType"]);
			}
			return strSql.ToString();
		}

		private void GetShopBuyCardRecordList(string StrWhere)
		{
			SysShopBuyCard bllSysShopBuyCard = new SysShopBuyCard();
			int Counts = this.NetPagerParameter.RecordCount;
			DataTable db = bllSysShopBuyCard.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
			{
				StrWhere
			}).Tables[0];
			this.NetPagerParameter.RecordCount = Counts;
			this.NetPagerParameter.CustomInfoHTML = string.Format("<div><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[]
			{
				this.NetPagerParameter.CurrentPageIndex,
				this.NetPagerParameter.PageCount,
				this.NetPagerParameter.RecordCount,
				this.NetPagerParameter.PageSize
			});
			this.gvShopBuyCard.DataSource = db;
			this.gvShopBuyCard.DataBind();
			PageBase.BindSerialRepeater(this.gvShopBuyCard, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.GetShopBuyCardRecordList(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.GetShopBuyCardRecordList(this.QueryCondition());
		}
	}
}
