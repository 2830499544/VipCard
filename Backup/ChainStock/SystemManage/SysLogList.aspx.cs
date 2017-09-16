using Chain.BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.SystemManage
{
	public class SysLogList : PageBase
	{
		protected HtmlForm form1;

		protected Literal ltlTitle;

		protected HtmlSelect sltCleanTime;

		protected HtmlInputButton btnCleanSysLog;

		protected HtmlInputText txtkeywords;

		protected HtmlSelect sltSysShopID;

		protected HtmlSelect sltSysUserID;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected Button btnMemListQuery;

		protected Repeater gvwSysLogList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindShopSelect(this._UserShopID, this.sltSysShopID, true);
				PubFunction.BindUserSelect(this._UserShopID, this.sltSysUserID, true, false);
				this.Get_SysLogList(this.Condition());
				this.drpPageSize.SelectedIndex = 1;
			}
		}

		private void Get_SysLogList(string strSql)
		{
			SysLog bllSysLog = new SysLog();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += "and SysLog.LogUserID = SysUser.UserID and SysLog.LogShopID = SysShop.ShopID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "LogShopID", strSql);
			DataTable db = bllSysLog.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvwSysLogList.DataSource = db;
			this.gvwSysLogList.DataBind();
			PageBase.BindSerialRepeater(this.gvwSysLogList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.Get_SysLogList(this.Condition());
		}

		protected void btnMemListQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.Get_SysLogList(this.Condition());
		}

		protected string Condition()
		{
			string strShopID = this.sltSysShopID.Value;
			string strUserID = this.sltSysUserID.Value;
			string KeyWord = this.txtkeywords.Value.Trim();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1");
			if (KeyWord != "" && KeyWord != null)
			{
				strSql.AppendFormat(" and LogDetail like '%{0}%' ", KeyWord);
			}
			if (strShopID != null && strShopID != "")
			{
				strSql.AppendFormat(" and LogShopID = {0}", int.Parse(strShopID));
			}
			if (strUserID != null && strUserID != "")
			{
				strSql.AppendFormat(" and LogUserID ={0}", int.Parse(strUserID));
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat("and LogCreateTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat("and LogCreateTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			return strSql.ToString();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Get_SysLogList(this.Condition());
		}
	}
}
