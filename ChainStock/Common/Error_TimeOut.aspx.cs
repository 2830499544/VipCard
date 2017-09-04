using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Common
{
	public class Error_TimeOut : Page
	{
		protected HtmlForm form1;

		protected Literal ErrInfo;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["TimeOut"] != null && Convert.ToInt32(base.Request["TimeOut"]) > 0)
			{
				this.ErrInfo.Text = "您已经超过" + base.Request["TimeOut"].ToString() + "分钟未进行操作&nbsp;&nbsp;为保证系统安全请重新登录";
			}
			else
			{
				this.ErrInfo.Text = "权限不足或系统登陆状态失效&nbsp;&nbsp;为保证系统安全请重新登录";
			}
		}
	}
}
