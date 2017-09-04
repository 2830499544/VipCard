using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.ExtraService
{
	public class SmsShopReportDetail : PageBase
	{
		protected HtmlForm frmSmsShopReportDetail;

		protected Literal ltlTitle;

		protected Repeater gvSmsShopReportDetail;

		private string strShopID = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["ShopID"] != null)
				{
					this.strShopID = base.Request.QueryString["ShopID"].ToString();
				}
			}
			this.Get_ParameterList(this.strShopID);
		}

		private void Get_ParameterList(string strShopID)
		{
			SmsLog bllSms = new SmsLog();
			DataTable db = bllSms.GetSmsShopReportDetail(strShopID).Tables[0];
			this.gvSmsShopReportDetail.DataSource = db;
			this.gvSmsShopReportDetail.DataBind();
			PageBase.BindSerialRepeater(this.gvSmsShopReportDetail, 0);
		}
	}
}
