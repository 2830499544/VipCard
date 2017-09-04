using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock
{
	public class index1 : Page
	{
		protected HtmlForm form1;

		protected Literal Literal1;

		protected void Page_Load(object sender, EventArgs e)
		{
			HttpBrowserCapabilities browser = base.Request.Browser;
			string browserType = browser.Browser.ToString();
			if (PubFunction.ISCheckKey)
			{
				if (browserType.ToUpper().Contains("IE") || browser.Type.Contains("Mozilla11") || browser.Type.Contains("InternetExplorer"))
				{
					PubFunction.IEbrowser = true;
					base.Response.Redirect("login.aspx");
				}
				else
				{
					PubFunction.IEbrowser = false;
					this.Literal1.Text = "为保证系统安全锁能正常使用，请使用IE浏览器访问本系统！";
				}
			}
			else
			{
				base.Response.Redirect("login.aspx");
			}
		}
	}
}
