using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.member
{
	public class moneyregion : Page
	{
		protected HtmlGenericControl spText;

		protected HtmlInputText txtMoneyRegion;

		protected HtmlInputHidden txtMemID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					this.txtMemID.Value = MemID.ToString();
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		public string BindPhoto(object photo)
		{
			string strPhoto;
			if (photo != null && photo.ToString() != "")
			{
				strPhoto = photo.ToString();
			}
			else
			{
				strPhoto = "../website/images/head.png";
			}
			return strPhoto;
		}

		public string BindTime(object time)
		{
			string strTime = "";
			if (time != null)
			{
				strTime = DateTime.Parse(time.ToString()).ToString("MM月dd日 HH:mm");
			}
			return strTime;
		}

		public string BindMobile(object mobile)
		{
			string strMobile = "";
			if (mobile != null)
			{
				strMobile = mobile.ToString();
				if (strMobile != "")
				{
					strMobile = strMobile.Substring(0, 3) + "****" + strMobile.Substring(6, 4);
				}
			}
			return strMobile;
		}
	}
}
