using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock
{
	public class LeftMenu : PageBase
	{
		protected string MenuString;

		protected Literal litObject;

		protected HtmlForm form1;

		protected HtmlGenericControl div1;

		protected HtmlGenericControl div10;

		protected HtmlGenericControl div3;

		protected HtmlGenericControl div4;

		protected HtmlGenericControl div5;

		protected HtmlGenericControl div11;

		protected HtmlGenericControl div6;

		protected HtmlGenericControl div7;

		protected HtmlGenericControl div8;

		protected HtmlGenericControl div2;

		protected HtmlGenericControl div9;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.div2.Visible = PubFunction.GetPageVisit(this._UserAuthority, 97);
			this.div3.Visible = PubFunction.GetPageVisit(this._UserAuthority, 2);
			this.div4.Visible = PubFunction.GetPageVisit(this._UserAuthority, 67);
			this.div5.Visible = PubFunction.GetPageVisit(this._UserAuthority, 17);
			this.div6.Visible = PubFunction.GetPageVisit(this._UserAuthority, 4);
			this.div7.Visible = PubFunction.GetPageVisit(this._UserAuthority, 14);
			this.div8.Visible = PubFunction.GetPageVisit(this._UserAuthority, 46);
			this.div9.Visible = PubFunction.GetPageVisit(this._UserAuthority, 11);
			this.div10.Visible = PubFunction.GetPageVisit(this._UserAuthority, 42);
			this.div11.Visible = PubFunction.GetPageVisit(this._UserAuthority, 66);
			if (PubFunction.ISCheckKey)
			{
				if (!PubFunction.IEbrowser)
				{
					base.Response.Write("<script>top.location.href='index.aspx'</script>");
				}
			}
		}

		public void SetQuickBtn()
		{
			DataTable dt = PubFunction.GetGroupAuthority(this._UserGroupID);
			this.div2.Visible = PubFunction.GetPageVisit(dt, 97);
			this.div3.Visible = PubFunction.GetPageVisit(dt, 2);
			this.div4.Visible = PubFunction.GetPageVisit(dt, 67);
			this.div5.Visible = PubFunction.GetPageVisit(dt, 17);
			this.div6.Visible = PubFunction.GetPageVisit(dt, 4);
			this.div7.Visible = PubFunction.GetPageVisit(dt, 14);
			this.div8.Visible = PubFunction.GetPageVisit(dt, 46);
			this.div9.Visible = PubFunction.GetPageVisit(dt, 11);
			this.div10.Visible = PubFunction.GetPageVisit(dt, 42);
			this.div11.Visible = PubFunction.GetPageVisit(dt, 66);
		}
	}
}
