using Chain.BLL;
using ChainStock.Controls;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.Report
{
	public class RptUserWork : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmPointRankList;

		protected Literal ltlTitle;

		protected Button btnRptUserWorkExcel;

		protected HtmlInputText txtUserName;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected Button btnUserSearch;

		protected Repeater gvPointRankList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch2;

		private SysUser bllsysuser = new SysUser();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				if (PubFunction.curParameter.dataAuthority != 0)
				{
					if (this._UserShopID > 1)
					{
						this.sltShop.Value = this._UserShopID.ToString();
						this.sltShop.Attributes.Add("disabled", "disabled");
					}
				}
				this.Get_ParameterList(this.QueryCondition());
			}
		}

		private void Get_ParameterList(string strSql)
		{
			SysUserWork bllsysuserwork = new SysUserWork();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += "and SysUserWork.userID = SysUser.userid and SysUser.UserShopID = SysShop.ShopID ";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "UserShopID", strSql);
			DataTable db = bllsysuserwork.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvPointRankList.DataSource = db;
			this.gvPointRankList.DataBind();
			PageBase.BindSerialRepeater(this.gvPointRankList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void btnUserSearch_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void btnPointRankExcel_Click(object sender, EventArgs e)
		{
			SysUserWork bllsysuserwork = new SysUserWork();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += "and SysUserWork.userID = SysUser.userid and SysUser.UserShopID = SysShop.ShopID ";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "UserShopID", strSql);
			DataTable dtPoint = bllsysuserwork.GetListSP(100000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.PointUserWorkExcel(dtPoint, this._UserName);
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Get_ParameterList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string username = this.txtUserName.Value.Trim();
			string strUserShopID = this.sltShop.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1");
			if (username != "")
			{
				strSql.AppendFormat("and (UserName = '{0}' or UserNumber = '{1}')", username, username);
			}
			if (strUserShopID != "")
			{
				strSql.AppendFormat("and UserShopID={0}", int.Parse(strUserShopID));
			}
			return strSql.ToString();
		}

		public string GetUserName(int userID)
		{
			return this.bllsysuser.GetModel(userID).UserName;
		}
	}
}
