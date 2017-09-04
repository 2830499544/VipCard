using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.SystemManage
{
	public class SysNoticeShowList : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmNoticeList;

		protected Image imgTitle;

		protected Label lblFrmTitle;

		protected HtmlGenericControl spNoticeCode;

		protected HtmlGenericControl spRelaseName;

		protected HtmlGenericControl spRelaseTime;

		protected HtmlInputButton btnNoticeSave;

		protected HtmlInputButton btnNoticeReset;

		protected GridView gvwNoticeList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.Get_SysNoticeList();
				this.spNoticeCode.InnerText = DateTime.Now.ToString("yyMMddHHmmss");
				this.spRelaseName.InnerText = PubFunction.UserIDTOName(this._UserID);
				this.spRelaseTime.InnerText = DateTime.Now.ToShortDateString();
			}
		}

		private void Get_SysNoticeList()
		{
			Chain.BLL.SysNotice bllNotice = new Chain.BLL.SysNotice();
			int Counts = this.NetPagerParameter.RecordCount;
			DataTable db = bllNotice.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
			{
				""
			}).Tables[0];
			this.NetPagerParameter.RecordCount = Counts;
			this.NetPagerParameter.CustomInfoHTML = string.Format("<div class=\"results\"><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[]
			{
				this.NetPagerParameter.CurrentPageIndex,
				this.NetPagerParameter.PageCount,
				this.NetPagerParameter.RecordCount,
				this.NetPagerParameter.PageSize
			});
			this.gvwNoticeList.DataSource = db;
			this.gvwNoticeList.DataBind();
			PageBase.BindSerialGridView(this.gvwNoticeList, false, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.Get_SysNoticeList();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Get_SysNoticeList();
		}
	}
}
