using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Common
{
	public class UserWork : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmTotal;

		protected HtmlInputHidden hdUserID;

		protected HtmlInputHidden hdStartTime;

		protected HtmlInputHidden hdEndTime;

		protected HtmlInputHidden hdShopID;

		protected HtmlInputHidden hdallmoney;

		protected Literal ltlTitle;

		protected HtmlGenericControl lblUserName;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.hdUserID.Value = this._UserID.ToString();
				this.hdShopID.Value = this._UserShopID.ToString();
				this.lblUserName.InnerText = this._UserName;
				SysUserWork bllsysuserwork = new SysUserWork();
				DataTable dtuserwork = bllsysuserwork.GetList(1, string.Format("UserID = {0}", this._UserID), "EedTime DESC").Tables[0];
				DataTable dtyfmoney = bllsysuserwork.GetList(string.Format("ispay = 0 and HandoverUserID = {0}", this._UserID)).Tables[0];
				if (dtuserwork.Rows.Count > 0)
				{
					this.hdStartTime.Value = dtuserwork.Rows[0]["EedTime"].ToString();
				}
				else
				{
					this.hdStartTime.Value = "1949-10-01";
				}
				double summoney = 0.0;
				foreach (DataRow dr in dtyfmoney.Rows)
				{
					summoney += Convert.ToDouble(dr["Arrearage"]);
				}
				this.hdEndTime.Value = DateTime.Now.ToString();
				this.hdallmoney.Value = summoney.ToString();
			}
		}
	}
}
