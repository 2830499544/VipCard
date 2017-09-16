using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock
{
	public class FirstPage : Page
	{
		private LoginLogic login;

		protected HtmlImage spShopPhoto;

		protected HtmlGenericControl spShopName;

		protected HtmlGenericControl spGroupName;

		protected HtmlGenericControl spShopContactMan;

		protected HtmlGenericControl spShopTel;

		protected HtmlGenericControl spShopAddress;

		protected HtmlImage imgMemberRate;

		protected HtmlAnchor spMemberRate;

		protected HtmlGenericControl spMemToday;

		protected HtmlGenericControl spMemYesterday;

		protected HtmlImage imgMoneyRate;

		protected HtmlAnchor spMoneyRate;

		protected HtmlGenericControl spMoneyToday;

		protected HtmlGenericControl spMoneyYesterday;

		protected HtmlImage imgCashRate;

		protected HtmlAnchor spCashRate;

		protected HtmlGenericControl spGetMoneyToday;

		protected HtmlGenericControl spGetMoneyYesterday;

		protected HtmlInputText txtMemStartTime;

		protected HtmlInputText txtMemEndTime;

		protected HtmlInputHidden sltShop;

		protected Repeater rptNotice;

		private Chain.Model.SysUser UserModel
		{
			get
			{
				if (this.login == null)
				{
					this.login = LoginLogic.LoginStatus();
				}
				Chain.Model.SysUser result;
				if (this.login.IsLoggedOn && this.login.LoginUser != null)
				{
					result = this.login.LoginUser;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				Chain.BLL.SysNotice bllNotice = new Chain.BLL.SysNotice();
				DataTable dtNotice = bllNotice.GetList(5, "", "SysNoticeTime desc").Tables[0];
				this.rptNotice.DataSource = dtNotice;
				this.rptNotice.DataBind();
				this.txtMemStartTime.Value = DateTime.Now.AddDays(-20.0).ToString("yyyy-MM-dd");
				this.txtMemEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				int shopID = this.UserModel.UserShopID;
				this.sltShop.Value = shopID.ToString();
				Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(this.UserModel.UserShopID);
				if (modelShop.ShopImageUrl != null && modelShop.ShopImageUrl.ToString() != "")
				{
					this.spShopPhoto.Src = modelShop.ShopImageUrl;
				}
				this.spShopName.InnerHtml = modelShop.ShopName;
				this.spShopContactMan.InnerHtml = modelShop.ShopContactMan;
				this.spShopTel.InnerHtml = modelShop.ShopTelephone;
				this.spShopAddress.InnerHtml = this.BindAddress(this.UserModel.UserShopID);
				Chain.BLL.SysGroup bllGroup = new Chain.BLL.SysGroup();
				Chain.Model.SysGroup modelGroup = bllGroup.GetModel(this.UserModel.UserGroupID);
				this.spGroupName.InnerHtml = modelGroup.GroupName;
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.BLL.MemRecharge bllMemRecharge = new Chain.BLL.MemRecharge();
				Chain.BLL.OrderLog bllOrderLog = new Chain.BLL.OrderLog();
				Chain.BLL.MemCount bllMemCount = new Chain.BLL.MemCount();
				string strMemToday = "CONVERT(varchar(10),MemCreateTime,120) = CONVERT(varchar(10),GETDATE(),120) AND MemID > 0";
				string strMemYesterday = "CONVERT(varchar(10),MemCreateTime,120) = CONVERT(varchar(10), DATEADD(day,-1,GETDATE()),120) AND MemID > 0";
				string strMoneyToday = "CONVERT(varchar(10),RechargeCreateTime,120) = CONVERT(varchar(10),GETDATE(),120)";
				string strMoneyYesterday = "CONVERT(varchar(10),RechargeCreateTime,120) = CONVERT(varchar(10), DATEADD(day,-1,GETDATE()),120)";
				string strGetMoneyToday = "CONVERT(varchar(10),OrderCreateTime,120) = CONVERT(varchar(10),GETDATE(),120)";
				string strGetMoneyYesterday = "CONVERT(varchar(10),OrderCreateTime,120) = CONVERT(varchar(10), DATEADD(day,-1,GETDATE()),120)";
				string strGetCountMoneyToday = "CONVERT(varchar(10),CountCreateTime,120) = CONVERT(varchar(10),GETDATE(),120)";
				string strGetCountMoneyYesterday = "CONVERT(varchar(10),CountCreateTime,120) = CONVERT(varchar(10), DATEADD(day,-1,GETDATE()),120)";
				if (modelShop.ShopID > 1)
				{
					strMemToday += string.Format(" AND MemShopID = {0}", shopID);
					strMemYesterday += string.Format(" AND MemShopID = {0}", shopID);
					strMoneyToday += string.Format(" AND RechargeShopID = {0}", shopID);
					strMoneyYesterday += string.Format(" AND RechargeShopID = {0}", shopID);
					strGetMoneyToday += string.Format(" AND OrderShopID = {0}", shopID);
					strGetMoneyYesterday += string.Format(" AND OrderShopID = {0}", shopID);
					strGetCountMoneyToday += string.Format(" AND CountShopID = {0}", shopID);
					strGetCountMoneyYesterday += string.Format(" AND CountShopID = {0}", shopID);
				}
				this.spMemToday.InnerHtml = bllMem.GetRecordCount(strMemToday).ToString();
				this.spMemYesterday.InnerHtml = bllMem.GetRecordCount(strMemYesterday).ToString();
				int memtoday = int.Parse(this.spMemToday.InnerHtml);
				int memyesterday = int.Parse(this.spMemYesterday.InnerHtml);
				decimal rate;
				if (memtoday > memyesterday)
				{
					this.imgMemberRate.Src = "images/icon (18).png";
					if (memyesterday != 0)
					{
						rate = memtoday - memyesterday / memyesterday;
					}
					else
					{
						rate = 1m;
					}
				}
				else
				{
					if (memyesterday != 0)
					{
						rate = memyesterday - memtoday / memyesterday;
					}
					else
					{
						rate = 0m;
					}
					this.imgMemberRate.Src = "images/icon (1).png";
				}
				this.spMemberRate.InnerHtml = (rate * 100m).ToString("F1") + "%";
				this.spMoneyToday.InnerHtml = bllMemRecharge.GetRecMoney(strMoneyToday).ToString("F2");
				this.spMoneyYesterday.InnerHtml = bllMemRecharge.GetRecMoney(strMoneyYesterday).ToString("F2");
				decimal moneytoday = decimal.Parse(this.spMoneyToday.InnerHtml);
				decimal moneyyesterday = decimal.Parse(this.spMoneyYesterday.InnerHtml);
				if (moneytoday > moneyyesterday)
				{
					this.imgMoneyRate.Src = "images/icon (18).png";
					if (moneyyesterday != 0m)
					{
						rate = moneytoday - moneyyesterday / moneyyesterday;
					}
					else
					{
						rate = 1m;
					}
				}
				else
				{
					if (moneyyesterday != 0m)
					{
						rate = moneyyesterday - moneytoday / moneyyesterday;
					}
					else
					{
						rate = 0m;
					}
					this.imgMoneyRate.Src = "images/icon (1).png";
				}
				this.spMoneyRate.InnerHtml = (rate * 100m).ToString("F1") + "%";
				this.spGetMoneyToday.InnerHtml = (bllOrderLog.GetTotalCash(strGetMoneyToday) + bllMemCount.GetTotalCash(strGetCountMoneyToday) + Convert.ToDecimal(this.spMoneyToday.InnerHtml)).ToString("F2");
				this.spGetMoneyYesterday.InnerHtml = (bllOrderLog.GetTotalCash(strGetMoneyYesterday) + bllMemCount.GetTotalCash(strGetCountMoneyYesterday) + Convert.ToDecimal(this.spMoneyYesterday.InnerHtml)).ToString("F2");
				decimal cashtoday = decimal.Parse(this.spGetMoneyToday.InnerHtml);
				decimal cashyesterday = decimal.Parse(this.spGetMoneyYesterday.InnerHtml);
				if (moneytoday > cashyesterday)
				{
					this.imgCashRate.Src = "images/icon (18).png";
					if (cashyesterday != 0m)
					{
						rate = cashtoday - cashyesterday / cashyesterday;
					}
					else
					{
						rate = 1m;
					}
				}
				else
				{
					if (cashyesterday != 0m)
					{
						rate = cashyesterday - cashtoday / cashyesterday;
					}
					else
					{
						rate = 0m;
					}
					this.imgCashRate.Src = "images/icon (1).png";
				}
				this.spCashRate.InnerHtml = (rate * 100m).ToString("F1") + "%";
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
