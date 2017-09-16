using Chain.BLL;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.member
{
	public class memsign : Page
	{
		protected HtmlGenericControl spPoint;

		protected HtmlGenericControl spSignInfo;

		protected HtmlInputHidden txtMemID;

		protected HtmlInputHidden txtImageUrl;

		protected HtmlInputHidden txtGetMoneyID;

		protected HtmlInputHidden MemCount;

		protected HtmlInputHidden MaxCount;

		protected HtmlInputHidden MoneyRate;

		protected HtmlInputHidden MemtotalCount;

		protected HtmlInputHidden TotalMoney;

		protected HtmlInputHidden GiveMoney;

		protected HtmlInputHidden StartMoney;

		protected HtmlInputHidden EndMoney;

		protected HtmlInputHidden MoneyType;

		protected HtmlInputHidden FixedMoney;

		protected HtmlInputHidden IsWin;

		protected HtmlInputHidden IsSuccess;

		protected HtmlInputHidden OpenID;

		protected HtmlInputHidden IsOwn;

		protected HtmlInputHidden txtMsg;

		protected HtmlInputHidden IsSign;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int memID = int.Parse(this.Session["MemID"].ToString());
					this.txtMemID.Value = memID.ToString();
					this.spPoint.InnerHtml = PubFunction.curParameter.intSignInPoint.ToString();
					MemSign bllSign = new MemSign();
					int count = bllSign.GetRecordCount("MemID=" + memID + " and convert(char(10),SignTime,121)=convert(char(10),getdate(),121)");
					if (count > 0)
					{
						this.spSignInfo.InnerHtml = "今日已签到";
						this.IsSign.Value = "1";
					}
					else
					{
						this.spSignInfo.InnerHtml = "今日签到";
						this.IsSign.Value = "0";
					}
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		private void rptSignListBind()
		{
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
