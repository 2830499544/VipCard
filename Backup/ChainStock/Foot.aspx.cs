using Chain.BLL;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock
{
	public class Foot : Page
	{
		protected HtmlForm form1;

		protected HtmlGenericControl divMessage;

		protected HtmlGenericControl spMessageCount;

		protected HtmlGenericControl spFoot;

		protected void Page_Load(object sender, EventArgs e)
		{
			switch (PubFunction.curParameter.istry)
			{
			case 0:
				this.spFoot.Style.Add("display", "");
				break;
			case 1:
				this.spFoot.Style.Add("display", "none");
				break;
			case 2:
				this.spFoot.Style.Add("display", "none");
				break;
			}
			OnlineMessage bllProposal = new OnlineMessage();
			int count = bllProposal.GetRecordCount("IsReply=0 and MessageType=0");
			this.spMessageCount.InnerHtml = count.ToString();
			if (count == 0)
			{
				this.divMessage.Visible = false;
			}
		}
	}
}
