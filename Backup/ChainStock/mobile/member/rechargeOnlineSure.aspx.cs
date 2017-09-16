using Chain.BLL;
using Chain.Model;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.member
{
	public class rechargeOnlineSure : Page
	{
		protected HtmlGenericControl spOrderAccount;

		protected HtmlGenericControl spMemCard;

		protected HtmlGenericControl spMemName;

		protected HtmlGenericControl spMoney;

		protected HtmlGenericControl spGiveMoney;

		protected HtmlGenericControl spPoint;

		protected HtmlInputHidden txtMemID;

		protected HtmlInputHidden txtUrl;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					this.txtMemID.Value = MemID.ToString();
					Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
					Chain.Model.Mem modelMem = bllMem.GetModel(MemID);
					this.spMemName.InnerHtml = modelMem.MemName;
					this.spMemCard.InnerHtml = modelMem.MemCard;
					this.spOrderAccount.InnerHtml = "wxcz" + DateTime.Now.ToString("yyyyMMddHHmmssffff");
					if (base.Request["money"] != null)
					{
						this.spMoney.InnerHtml = base.Request["money"].ToString();
					}
					if (base.Request["givemoney"] != null)
					{
						this.spGiveMoney.InnerHtml = base.Request["givemoney"].ToString();
					}
					if (base.Request["point"] != null)
					{
						this.spPoint.InnerHtml = base.Request["point"].ToString();
					}
					decimal amount = decimal.Parse(this.spMoney.InnerHtml) + decimal.Parse(this.spGiveMoney.InnerHtml);
					string total_fee = double.Parse((amount * 100m).ToString("#0")).ToString();
					string url = string.Concat(new string[]
					{
						"http://",
						PubFunction.curParameter.strDoMain,
						"/mobile/member/RechargeJsApiPayPage.aspx?MemID=",
						this.txtMemID.Value,
						"&OrderAccount=",
						this.spOrderAccount.InnerHtml,
						"&money=",
						this.spMoney.InnerHtml,
						"&givemoney=",
						this.spGiveMoney.InnerHtml,
						"&point=",
						this.spPoint.InnerHtml
					});
					string lineLinkurl = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state={2}#wechat_redirect", PubFunction.curParameter.strWeiXinAppID, rechargeOnlineSure.UrlEncode(url), total_fee);
					this.txtUrl.Value = lineLinkurl;
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		public static string UrlEncode(string str)
		{
			StringBuilder sb = new StringBuilder();
			byte[] byStr = Encoding.UTF8.GetBytes(str);
			for (int i = 0; i < byStr.Length; i++)
			{
				sb.Append("%" + Convert.ToString(byStr[i], 16));
			}
			return sb.ToString();
		}
	}
}
