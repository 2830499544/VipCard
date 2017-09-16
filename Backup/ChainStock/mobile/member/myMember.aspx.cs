using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.member
{
	public class myMember : Page
	{
		protected HtmlImage imgPhoto;

		protected HtmlGenericControl spMemName;

		protected HtmlGenericControl spLevelName;

		protected HtmlGenericControl spMemCard;

		protected HtmlGenericControl spMemStatus;

		protected HtmlGenericControl spMemMoney;

		protected HtmlGenericControl spMemPoint;

		protected HtmlGenericControl spShopName;

		protected HtmlGenericControl spIdentityCard;

		protected HtmlGenericControl spEmail;

		protected HtmlGenericControl spAddress;

		protected HtmlGenericControl spMobile;

		protected HtmlGenericControl spBirthday;

		protected HtmlGenericControl spCardNumber;

		protected HtmlGenericControl spUserName;

		protected HtmlGenericControl spRecommendCard;

		protected HtmlGenericControl spCreateTime;

		protected HtmlGenericControl spPastTime;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					this.BindMemInfo(MemID);
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		private void BindMemInfo(int MemID)
		{
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			int counts = 0;
			string strSql = " Mem.MemShopID = SysShop.ShopID and Mem.MemLevelID = MemLevel.LevelID and Mem.MemUserID=SysUser.UserID and Mem.MemID=" + MemID;
			DataTable dtMem = bllMem.GetListSP(1, 1, out counts, new string[]
			{
				strSql.ToString()
			}).Tables[0];
			if (dtMem.Rows.Count > 0)
			{
				this.spMemCard.InnerHtml = dtMem.Rows[0]["MemCard"].ToString();
				this.spMemMoney.InnerHtml = decimal.Parse(dtMem.Rows[0]["MemMoney"].ToString()).ToString("#0.00");
				this.spMemPoint.InnerHtml = decimal.Parse(dtMem.Rows[0]["MemPoint"].ToString()).ToString("#0");
				this.spLevelName.InnerHtml = dtMem.Rows[0]["LevelName"].ToString();
				this.spMemName.InnerHtml = ((dtMem.Rows[0]["MemName"].ToString() == "") ? "&nbsp;" : dtMem.Rows[0]["MemName"].ToString());
				this.spMemStatus.InnerHtml = this.GetMemState(int.Parse(dtMem.Rows[0]["MemState"].ToString()));
				this.spShopName.InnerHtml = dtMem.Rows[0]["ShopName"].ToString();
				this.spIdentityCard.InnerHtml = ((dtMem.Rows[0]["MemIdentityCard"].ToString() == "") ? "无" : dtMem.Rows[0]["MemIdentityCard"].ToString());
				this.spEmail.InnerHtml = ((dtMem.Rows[0]["MemEmail"].ToString() == "") ? "无" : dtMem.Rows[0]["MemEmail"].ToString());
				this.spAddress.InnerHtml = this.GetMemAddress(dtMem.Rows[0]["MemProvinceName"].ToString(), dtMem.Rows[0]["MemCityName"].ToString(), dtMem.Rows[0]["MemCountyName"].ToString(), dtMem.Rows[0]["MemVillageName"].ToString(), dtMem.Rows[0]["MemAddress"].ToString());
				this.spMobile.InnerHtml = dtMem.Rows[0]["MemMobile"].ToString();
				this.spBirthday.InnerHtml = DateTime.Parse(dtMem.Rows[0]["MemBirthday"].ToString()).ToString("yyyy-MM-dd");
				this.spCardNumber.InnerHtml = ((dtMem.Rows[0]["MemCardNumber"].ToString() == "") ? "无" : dtMem.Rows[0]["MemCardNumber"].ToString());
				this.spUserName.InnerHtml = dtMem.Rows[0]["UserName"].ToString();
				this.spRecommendCard.InnerHtml = ((dtMem.Rows[0]["MemRecommendID"].ToString() != "0") ? this.GetRecommendCard(int.Parse(dtMem.Rows[0]["MemRecommendID"].ToString())) : "无");
				this.spCreateTime.InnerHtml = DateTime.Parse(dtMem.Rows[0]["MemCreateTime"].ToString()).ToString("yyyy-MM-dd");
				this.spPastTime.InnerHtml = DateTime.Parse(dtMem.Rows[0]["MemPastTime"].ToString()).ToString("yyyy-MM-dd");
				if (dtMem.Rows[0]["MemPhoto"].ToString() != "")
				{
					this.imgPhoto.Src = dtMem.Rows[0]["MemPhoto"].ToString();
				}
				else
				{
					this.imgPhoto.Src = "images/headimg.jpg";
				}
			}
		}

		private string GetMemAddress(string MemProvinceName, string MemCityName, string MemCountyName, string MemVillageName, string MemAddress)
		{
			string address = "无";
			if (MemProvinceName != "")
			{
				address = string.Concat(new string[]
				{
					MemProvinceName,
					"省",
					MemCityName,
					"市",
					MemCountyName,
					MemVillageName,
					MemAddress
				});
			}
			else if (MemAddress != "")
			{
				address = MemAddress;
			}
			return address;
		}

		private string GetRecommendCard(int RecommendID)
		{
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			Chain.Model.Mem modelMem = bllMem.GetModel(RecommendID);
			return modelMem.MemCard;
		}

		private string GetMemState(int memState)
		{
			string strState = "";
			switch (memState)
			{
			case 0:
				strState = "正常";
				break;
			case 1:
				strState = "锁定";
				break;
			case 2:
				strState = "挂失";
				break;
			}
			return strState;
		}
	}
}
