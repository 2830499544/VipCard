using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.member
{
	public class rotateregion : Page
	{
		protected HtmlGenericControl spText;

		protected HtmlInputText txtRotateRegion;

		protected HtmlInputHidden txtMemID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					this.txtMemID.Value = MemID.ToString();
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}
	}
}
