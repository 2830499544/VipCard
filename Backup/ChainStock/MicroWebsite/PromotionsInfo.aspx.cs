using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.MicroWebsite
{
	public class PromotionsInfo : PageBase
	{
		protected HtmlForm frmGoodsInfo;

		protected Literal ltlTitle;

		protected Repeater rptShop;

		protected HtmlInputText txtPromotionsTitle;

		protected HtmlImage imgPromotionsPhoto;

		protected HtmlSelect sltPromotionsLevel;

		protected HtmlTextArea txtShopNameList;

		protected HtmlTextArea txtShopList;

		protected HtmlTextArea txtPromotionsDesc;

		protected HtmlInputText txtPromotionsRemark;

		protected HtmlInputHidden txtPromotionsID;

		protected HtmlInputHidden txtUpdatePromotionsName;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindPreferentialObject();
				this.rptShopBind();
				if (base.Request.QueryString["PromotionsID"] != null)
				{
					int PromotionsID = int.Parse(base.Request.QueryString["PromotionsID"]);
					Chain.BLL.Promotions bllPromotions = new Chain.BLL.Promotions();
					Chain.Model.Promotions modelPromotions = bllPromotions.GetModel(PromotionsID);
					this.txtPromotionsDesc.Value = modelPromotions.PromotionsDesc;
					this.txtPromotionsID.Value = PromotionsID.ToString();
					this.txtPromotionsRemark.Value = modelPromotions.PromotionsRemark;
					this.txtUpdatePromotionsName.Value = modelPromotions.PromotionsPhoto;
					this.imgPromotionsPhoto.Src = modelPromotions.PromotionsPhoto;
					this.txtPromotionsTitle.Value = modelPromotions.PromotionsTitle;
					this.txtShopList.Value = modelPromotions.ShopList;
					string[] shopList = this.txtShopList.Value.Split(new char[]
					{
						','
					});
					string shopname = "";
					for (int i = 0; i < shopList.Length; i++)
					{
						Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
						Chain.Model.SysShop modelShop = bllShop.GetModel(int.Parse(shopList[i]));
						shopname = shopname + modelShop.ShopName + ";";
					}
					shopname.TrimEnd(new char[]
					{
						';'
					});
					this.txtShopNameList.Value = shopname;
					this.ltlTitle.Text = "微官网   >    优惠活动   >   优惠活动编辑 ";
				}
				else
				{
					this.ltlTitle.Text = "微官网   >   优惠活动   >   优惠活动新增 ";
				}
			}
		}

		private void rptShopBind()
		{
			this.rptShop.DataSource = new Chain.BLL.SysShop().GetList(" shopid>0").Tables[0];
			this.rptShop.DataBind();
		}

		public void BindPreferentialObject()
		{
			DataTable dt = new Chain.BLL.MemLevel().GetList("").Tables[0];
			this.sltPromotionsLevel.Items.Add(new ListItem("=== 所有会员 ===", "-1"));
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				this.sltPromotionsLevel.Items.Add(new ListItem(dt.Rows[i]["LevelName"].ToString(), dt.Rows[i]["LevelID"].ToString()));
			}
		}
	}
}
