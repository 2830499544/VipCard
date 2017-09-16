using ChainStock.Controls;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Report
{
	public class RptTotal : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmTotal;

		protected Literal ltlTitle;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected HtmlSelect sltShop;

		protected HtmlSelect sltUserID;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
			PubFunction.BindUserSelect(this._UserShopID, this.sltUserID, true, false);
			if (PubFunction.curParameter.dataAuthority != 0)
			{
				if (this._UserShopID > 1)
				{
					this.sltShop.Value = this._UserShopID.ToString();
					this.sltShop.Attributes.Add("disabled", "disabled");
				}
			}
		}
	}
}
