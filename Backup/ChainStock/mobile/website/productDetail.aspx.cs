using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.website
{
	public class productDetail : Page
	{
		protected HtmlGenericControl spProductDesc;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["ProductID"] != null)
				{
					int ProductID = int.Parse(base.Request.QueryString["ProductID"]);
					Chain.BLL.ProductCenter bllProduct = new Chain.BLL.ProductCenter();
					Chain.Model.ProductCenter modelProduct = bllProduct.GetModel(ProductID);
					this.spProductDesc.InnerHtml = modelProduct.ProductDesc;
				}
			}
		}
	}
}
