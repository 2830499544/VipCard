using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.ExtraService
{
	public class ChooseMember : PageBase
	{
		protected HtmlForm frmChooseMember;

		protected Image imgTitle;

		protected Label lblFrmTitle;

		protected HtmlInputText txtQueryMem;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltShop;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
			}
		}
	}
}
