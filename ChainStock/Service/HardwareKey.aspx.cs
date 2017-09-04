using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Service
{
	public class HardwareKey : Page
	{
		protected HtmlForm form1;

		protected Panel Panel1;

		protected TextBox TextBox1;

		protected Button Button1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request["key"] != null & base.Request["key"] == "zhiluo")
				{
					this.Panel1.Visible = true;
				}
			}
		}
	}
}
