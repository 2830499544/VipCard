using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.website
{
	public class queryStore : Page
	{
		protected HtmlInputText txtKey;

		protected HtmlSelect sltProvince;

		protected HtmlSelect sltCity;

		protected HtmlSelect sltCounty;

		protected HtmlInputHidden txtPID;

		protected HtmlInputHidden txtCID;

		protected HtmlInputHidden txtCYID;

		protected Repeater rptShop;

		protected HtmlAnchor moreShop;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindSysAreaNew(this.sltProvince, 0);
				Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
				DataTable dt = bllShop.GetList("ShopID>0").Tables[0];
				string strSql = "ShopID>0 and ShopType=3  ";
				if (base.Request.QueryString["key"] != null)
				{
					string key = base.Request.QueryString["key"].ToString();
					if (key != "")
					{
						strSql = strSql + " and ShopName like'%" + key + "%'";
						this.txtKey.Value = key;
					}
				}
				if (base.Request.QueryString["pid"] != null)
				{
					if (base.Request.QueryString["pid"] != "")
					{
						strSql = strSql + " and ShopProvince=" + base.Request.QueryString["pid"];
						PubFunction.BindSysAreaNew(this.sltCity, int.Parse(base.Request.QueryString["pid"]));
					}
					this.txtPID.Value = base.Request.QueryString["pid"];
					this.sltProvince.Value = base.Request.QueryString["pid"];
				}
				if (base.Request.QueryString["cid"] != null)
				{
					if (base.Request.QueryString["cid"] != "")
					{
						strSql = strSql + " and ShopCity=" + base.Request.QueryString["cid"];
						PubFunction.BindSysAreaNew(this.sltCounty, int.Parse(base.Request.QueryString["cid"]));
					}
					this.txtCID.Value = base.Request.QueryString["cid"];
					this.sltCity.Value = base.Request.QueryString["cid"];
				}
				if (base.Request.QueryString["cyid"] != null)
				{
					if (base.Request.QueryString["cyid"] != "")
					{
						strSql = strSql + " and ShopCounty=" + base.Request.QueryString["cyid"];
					}
					this.txtCYID.Value = base.Request.QueryString["cyid"];
					this.sltCounty.Value = base.Request.QueryString["cyid"];
				}
				if (base.Request.QueryString["type"] == "all")
				{
					dt = bllShop.GetList(strSql).Tables[0];
				}
				else
				{
					dt = bllShop.GetList(10, strSql, "ShopCreateTime").Tables[0];
				}
				this.rptShop.DataSource = dt;
				this.rptShop.DataBind();
				if (dt.Rows.Count < 10 || base.Request.QueryString["type"] == "all")
				{
					this.moreShop.Attributes.Add("style", "display:none");
				}
			}
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
