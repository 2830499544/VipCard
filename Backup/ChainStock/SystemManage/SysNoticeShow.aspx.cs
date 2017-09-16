using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class SysNoticeShow : Page
	{
		private int intNoticeID = 0;

		protected HtmlForm frmNoticeShow;

		protected HtmlGenericControl spNoticeTitle;

		protected HtmlGenericControl spRelaseName;

		protected HtmlGenericControl spRelaseTime;

		protected Literal ltNoticeDetail;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["NoticeID"] != "")
			{
				this.intNoticeID = int.Parse(base.Request["NoticeID"]);
			}
			this.GetNoticeInfo();
		}

		public void GetNoticeInfo()
		{
			if (this.intNoticeID != 0)
			{
				Chain.Model.SysNotice modelNotice = new Chain.BLL.SysNotice().GetModel(this.intNoticeID);
				this.spRelaseName.InnerHtml = modelNotice.SysNotieceName;
				this.spRelaseTime.InnerHtml = modelNotice.SysNoticeTime.ToShortDateString();
				this.spNoticeTitle.InnerHtml = modelNotice.SysNoticeTitle;
				this.ltNoticeDetail.Text = modelNotice.SysNoticeDetail;
			}
		}
	}
}
