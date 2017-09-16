using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.MicroWebsite
{
	public class ProductCenterInfo : PageBase
	{
		protected HtmlForm frmGoodsInfo;

		protected Literal ltlTitle;

		protected HtmlInputText txtProductName;

		protected HtmlSelect sltClassID;

		protected HtmlImage imgProductPhoto;

		protected HtmlTextArea txtProductDesc;

		protected HtmlInputText txtProductRemark;

		protected HtmlInputHidden txtProductID;

		protected HtmlInputHidden txtUpdateProductName;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["ProductID"] != null)
				{
					int ProductID = int.Parse(base.Request.QueryString["ProductID"]);
					Chain.BLL.ProductCenter bllProduct = new Chain.BLL.ProductCenter();
					Chain.Model.ProductCenter modelProduct = bllProduct.GetModel(ProductID);
					this.txtProductDesc.Value = modelProduct.ProductDesc;
					this.txtProductName.Value = modelProduct.ProductName;
					this.txtProductRemark.Value = modelProduct.ProductRemark;
					this.imgProductPhoto.Src = modelProduct.ProductPhoto.ToString();
					this.txtUpdateProductName.Value = modelProduct.ProductPhoto;
					this.txtProductID.Value = ProductID.ToString();
					this.sltClassID.Value = modelProduct.ClassID.ToString();
					this.ltlTitle.Text = "微官网   >   产品管理   >   产品编辑 ";
				}
				else
				{
					this.ltlTitle.Text = "微官网   >   产品管理   >   产品新增 ";
				}
				this.BindClassInfo(this.sltClassID);
			}
		}

		public void BindClassInfo(HtmlSelect select)
		{
			select.Items.Clear();
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
			DataTable dtGoodsClass = new Chain.BLL.ProductClass().GetList(" 1=1 ").Tables[0];
			foreach (DataRow dr in dtGoodsClass.Rows)
			{
				select.Items.Add(new ListItem(dr["ClassName"].ToString(), dr["ClassID"].ToString()));
			}
		}
	}
}
