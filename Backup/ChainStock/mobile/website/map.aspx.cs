using Chain.BLL;
using Chain.Model;
using Chain.Wechat;
using System;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.website
{
	public class map : Page
	{
		protected HtmlGenericControl spAddress;

		protected HtmlGenericControl spLat;

		protected HtmlGenericControl spLng;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["ShopID"] != null)
				{
					int shopID = int.Parse(base.Request.QueryString["ShopID"].ToString());
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
					this.spAddress.InnerHtml = province + city + county + modelShop.ShopAddress;
					JavaScriptSerializer jsonReader = new JavaScriptSerializer();
					HttpRequestHelper httpRequest = new HttpRequestHelper();
					string jsonString = httpRequest.Reqeust("http://apis.map.qq.com/ws/geocoder/v1/?address=" + this.spAddress.InnerHtml + "&key=FMXBZ-MQALQ-ZCT5V-GB46R-G62I2-6MF7X");
					MapAddressInfo MapInfo = jsonReader.Deserialize<MapAddressInfo>(jsonString);
					int status = MapInfo.Status;
					if (status == 0)
					{
						this.spLat.InnerHtml = MapInfo.Result.Location.Lat.ToString();
						this.spLng.InnerHtml = MapInfo.Result.Location.Lng.ToString();
					}
				}
			}
		}
	}
}
