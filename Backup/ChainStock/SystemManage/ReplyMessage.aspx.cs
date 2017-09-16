using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.SystemManage
{
	public class ReplyMessage : PageBase
	{
		protected HtmlForm frmReply;

		protected HtmlInputHidden txtMemID;

		protected Literal ltlTitle;

		protected Repeater rptMsgList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		private int memID;

		private Chain.BLL.Message bllMessage = new Chain.BLL.Message();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				try
				{
					this.memID = int.Parse(base.Request.QueryString["MemID"].ToString());
					this.txtMemID.Value = this.memID.ToString();
					this.bindrpt();
				}
				catch
				{
					base.Response.Redirect("Message.aspx?PID=89");
				}
			}
		}

		private void bindrpt()
		{
			int Counts = this.NetPagerParameter.RecordCount;
			DataTable dtMessList = this.bllMessage.GetListSPInfo(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
			{
				string.Format("MessageMemID= {0} and [Message].MessageMemID = Mem.MemID ", this.txtMemID.Value)
			}).Tables[0];
			this.NetPagerParameter.RecordCount = Counts;
			this.NetPagerParameter.CustomInfoHTML = string.Format("<div class=\"results\"><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[]
			{
				this.NetPagerParameter.CurrentPageIndex,
				this.NetPagerParameter.PageCount,
				this.NetPagerParameter.RecordCount,
				this.NetPagerParameter.PageSize
			});
			this.rptMsgList.DataSource = dtMessList;
			this.rptMsgList.DataBind();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.bindrpt();
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.bindrpt();
		}

		public string BindHtml(int MessageID, int MessageMemID, string MessageContent)
		{
			StringBuilder sbHtml = new StringBuilder();
			Chain.Model.Message mdMessage = this.bllMessage.GetModel(MessageID);
			if (mdMessage.MessageIsReply == 1)
			{
				sbHtml.AppendFormat("<a href=\"javascript:void();\" onclick=\"DelMsg('{0}','{1}')\"><img src=\"../images/Gift/del.png\" alt=\"删除\" title=\"删除\" /></a>", MessageID, MessageMemID);
			}
			else
			{
				sbHtml.AppendFormat("<a href=\"javascript:void();\" onclick=\"ReplyMsg('{0}','{1}')\"><img src=\"../images/Gift/Reply.png\" alt=\"回复\" title=\"回复\" /></a>", MessageContent, MessageID);
			}
			return sbHtml.ToString();
		}
	}
}
