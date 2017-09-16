using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.member
{
	public class modifyPassword : Page
	{
		protected HtmlInputText mobile;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					this.BindMemInfo(MemID);
					this.mobile.Attributes.Add("disabled", "disabled");
				}
			}
		}

		private void BindMemInfo(int MemID)
		{
			Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModel(MemID);
			this.mobile.Value = modelMem.MemMobile;
		}
	}
}
