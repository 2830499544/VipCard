using Chain.BLL;
using ChainStock.Controls;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.SystemManage
{
	public class StaffClassList : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmStaffClassList;

		protected Literal ltlTitle;

		protected HtmlInputText txtClassName;

		protected HtmlInputHidden txtClassID;

		protected HtmlSelect sltClassShopID;

		protected HtmlInputRadioButton radTypeYes;

		protected HtmlInputRadioButton radTypeNo;

		protected HtmlInputText txtClassPercent;

		protected HtmlTextArea txtClassRemark;

		protected HtmlInputButton btnClassAdd;

		protected HtmlInputHidden HDsltshop;

		protected HtmlSelect sltShop;

		protected Button btnStaffClassQuery;

		protected Repeater gvStaffClassList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindShopSelect(this._UserShopID, this.sltClassShopID, false);
				this.sltClassShopID.Value = this._UserShopID.ToString();
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				this.GetStaffClassList(this.QueryCondition());
			}
		}

		private void GetStaffClassList(string strSql)
		{
			StaffClass bllStaffClass = new StaffClass();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "ClassShopID", strSql);
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
			this.gvStaffClassList.DataSource = dtStaffClass;
			this.gvStaffClassList.DataBind();
			PageBase.BindSerialRepeater(this.gvStaffClassList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
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
			strSql.Append(" SysShop.ShopID=StaffClass.ClassShopID ");
			if (strMemShopID != "")
			{
				strSql.AppendFormat(" and ClassShopID={0}", int.Parse(strMemShopID));
			}
			return strSql.ToString();
		}

		protected void btnStaffClassQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.GetStaffClassList(this.QueryCondition());
		}
	}
}
