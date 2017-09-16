using Chain.BLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.website
{
	public class productShow : Page
	{
		protected Repeater rptProductClass;

		protected Repeater rptProduct;

		protected HtmlAnchor moreProduct;

		protected HtmlInputHidden txtClassID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.rptProductClassBind();
				ProductCenter bllProduct = new ProductCenter();
				string strSql = "1=1";
				if (base.Request.QueryString["ClassID"] != null)
				{
					strSql = strSql + " and ClassID=" + base.Request.QueryString["ClassID"];
					this.txtClassID.Value = base.Request.QueryString["ClassID"];
				}
				DataTable dt;
				if (base.Request.QueryString["type"] == "all")
				{
					dt = bllProduct.GetList(strSql).Tables[0];
				}
				else
				{
					dt = bllProduct.GetList(20, strSql, "ProductCreateTime").Tables[0];
				}
				this.rptProduct.DataSource = dt;
				this.rptProduct.DataBind();
				if (dt.Rows.Count < 20 || base.Request.QueryString["type"] == "all")
				{
					this.moreProduct.Attributes.Add("style", "display:none");
				}
				else
				{
					this.moreProduct.Attributes.Add("style", "");
				}
			}
		}

		private void rptProductClassBind()
		{
			this.rptProductClass.DataSource = new ProductClass().GetList("1=1").Tables[0];
			this.rptProductClass.DataBind();
		}
	}
}
