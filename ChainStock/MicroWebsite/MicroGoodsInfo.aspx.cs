using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.MicroWebsite
{
	public class MicroGoodsInfo : PageBase
	{
		protected HtmlForm frmGoodsInfo;

		protected Literal ltlTitle;

		protected HtmlInputText txtGoodsCode;

		protected HtmlInputHidden txtGoodsID;

		protected HtmlGenericControl lblAutoGoodsCode;

		protected HtmlInputHidden txtCode;

		protected HtmlInputText txtGoodsName;

		protected HtmlSelect sltGoodsClass;

		protected HtmlInputText txtMicroPrice;

		protected HtmlInputText txtMicroSalePrice;

		protected HtmlInputText txtGoodsBidPrice;

		protected HtmlInputText txtGoodsPoint;

		protected HtmlTextArea txtGoodsRemark;

		protected HtmlInputHidden txtUpdateGoodsName;

		protected HtmlImage imgGoodsPhoto;

		protected HtmlInputHidden txtGoodsPhoto;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["MicroGoodsID"] != null)
				{
					this.GetGoodsInfo(int.Parse(base.Request.QueryString["MicroGoodsID"]));
				}
				else
				{
					this.txtCode.Value = DateTime.Now.ToString("yyMMddHHmmss");
					this.BindMicroGoodsClass(this.sltGoodsClass, 1);
				}
			}
		}

		protected void GetGoodsInfo(int goodsID)
		{
			Chain.BLL.MicroWebsiteGoods bllGoods = new Chain.BLL.MicroWebsiteGoods();
			Chain.Model.MicroWebsiteGoods modelGoods = bllGoods.GetModel(goodsID);
			this.txtGoodsID.Value = modelGoods.MicroGoodsID.ToString();
			this.txtGoodsCode.Value = modelGoods.MicroGoodsCode;
			this.txtGoodsName.Value = modelGoods.MicroGoodsName;
			this.BindMicroGoodsClass(this.sltGoodsClass, 1);
			this.sltGoodsClass.Value = modelGoods.MicroGoodsClassID.ToString();
			if (modelGoods.MicroPoint != -1)
			{
				this.txtGoodsPoint.Value = modelGoods.MicroPoint.ToString();
			}
			else
			{
				this.txtGoodsPoint.Value = "";
			}
			this.txtGoodsRemark.Value = modelGoods.MicroGoodsRemark;
			this.txtMicroPrice.Value = decimal.Parse(modelGoods.MicroPrice.ToString()).ToString("0.00");
			this.txtGoodsBidPrice.Value = decimal.Parse(modelGoods.MicroGoodsBidPrice.ToString()).ToString("0.00");
			this.txtMicroSalePrice.Value = decimal.Parse(modelGoods.MicroSalePrice.ToString()).ToString("0.00");
			this.imgGoodsPhoto.Src = modelGoods.MicroGoodsPicture;
			this.txtUpdateGoodsName.Value = modelGoods.MicroGoodsPicture;
		}

		public void BindMicroGoodsClass(HtmlSelect select, int ShopID)
		{
			select.Items.Clear();
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
			DataTable dtGoodsClass = new Chain.BLL.MicroWebsiteGoodsClass().GetList(string.Format("MicroGoodsClassShopID={0}", ShopID)).Tables[0];
			foreach (DataRow dr in dtGoodsClass.Rows)
			{
				select.Items.Add(new ListItem(dr["MicroGoodsClassName"].ToString(), dr["MicroGoodsClassID"].ToString()));
			}
		}
	}
}
