using Chain.BLL;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.member
{
	public class bill : Page
	{
		protected Repeater rptBill;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					this.GetMemBillList(MemID);
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		private void GetMemBillList(int MemID)
		{
			Mem bllMem = new Mem();
			this.rptBill.DataSource = bllMem.GetMemBillList(" OrderMemID=" + MemID + " order by OrderCreateTime desc");
			this.rptBill.DataBind();
		}
	}
}
