using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class SysNotice : PageBase
	{
		public string NoticeCode;

		public string RelaseName;

		public string RelaseTime;

		protected HtmlForm frmSysNotice;

		protected Literal ltlTitle;

		protected HtmlGenericControl spNoticeCode;

		protected HtmlInputHidden NoticeID;

		protected HtmlGenericControl spRelaseName;

		protected HtmlGenericControl spRelaseTime;

		protected HtmlInputText txtNoticeTitle;

		protected HtmlTextArea txtNoticeDetail;

		protected HtmlInputButton btnNoticeSave;

		protected HtmlInputButton btnNoticeReset;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["NoticeID"] != "0")
				{
					string strNoticeID = base.Request.QueryString["NoticeID"].ToString();
					Chain.Model.SysNotice modelNotice = new Chain.BLL.SysNotice().GetModel(Convert.ToInt32(strNoticeID));
					this.spNoticeCode.InnerText = modelNotice.SysNoticeCode;
					this.spRelaseName.InnerText = modelNotice.SysNotieceName;
					this.NoticeID.Value = strNoticeID;
					this.spRelaseTime.InnerText = modelNotice.SysNoticeTime.ToString();
					this.txtNoticeTitle.Value = modelNotice.SysNoticeTitle;
					this.txtNoticeDetail.Value = modelNotice.SysNoticeDetail;
				}
				else
				{
					this.spNoticeCode.InnerText = DateTime.Now.ToString("yyMMddHHmmss");
					this.spRelaseName.InnerText = PubFunction.UserIDTOName(this._UserID);
					this.spRelaseTime.InnerText = DateTime.Now.ToShortDateString();
				}
			}
		}
	}
}
