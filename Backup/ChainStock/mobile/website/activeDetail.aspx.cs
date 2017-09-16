using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.website
{
	public class activeDetail : Page
	{
		protected HtmlGenericControl spMemLevel;

		protected HtmlGenericControl spPromotionsDesc;

		protected Repeater rptShop;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["PromotionsID"] != null)
				{
					int ProductID = int.Parse(base.Request.QueryString["PromotionsID"]);
					Chain.BLL.Promotions bllPromotions = new Chain.BLL.Promotions();
					Chain.Model.Promotions modelPromotions = bllPromotions.GetModel(ProductID);
					this.spPromotionsDesc.InnerHtml = modelPromotions.PromotionsDesc;
					string shopnameLsit = modelPromotions.ShopList;
					string[] shopList = shopnameLsit.Split(new char[]
					{
						','
					});
					string sql = " ShopType=3 ";
					if (shopList.Length > 0)
					{
						string shopidList = "( ";
						for (int i = 0; i < shopList.Length; i++)
						{
							shopidList = shopidList + shopList[i] + ",";
						}
						shopidList = shopidList.Substring(0, shopidList.Length - 1);
						shopidList += ")";
						sql = sql + " and ShopID in " + shopidList;
					}
					if (modelPromotions.PromotionsMemLevel == -1)
					{
						this.spMemLevel.InnerHtml = "所有会员";
					}
					else
					{
						Chain.BLL.MemLevel bllLevel = new Chain.BLL.MemLevel();
						this.spMemLevel.InnerHtml = bllLevel.GetModel(modelPromotions.PromotionsMemLevel).LevelName;
					}
					this.rptShopBind(sql);
				}
			}
		}

		private void rptShopBind(string sql)
		{
			Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
			this.rptShop.DataSource = bllShop.GetList(sql).Tables[0];
			this.rptShop.DataBind();
		}

		protected string BindAddress(object shopID)
		{
			Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
			Chain.Model.SysShop modelShop = bllSysShop.GetModel(int.Parse(shopID.ToString()));
			int ProvinceID = modelShop.ShopProvince;
			string province = "";
			string city = "";
			string county = "";
			if (ProvinceID != 0)
			{
				province = PubFunction.SysAreaName(ProvinceID) + "省";
			}
			int CityID = modelShop.ShopCity;
			if (CityID != 0)
			{
				city = PubFunction.SysAreaName(CityID) + "市";
			}
			int CountyID = modelShop.ShopCounty;
			if (CountyID != 0)
			{
				county = PubFunction.SysAreaName(CountyID);
			}
			return province + city + county + modelShop.ShopAddress;
		}
	}
}
