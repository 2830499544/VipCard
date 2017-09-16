using Chain.BLL;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.member
{
	public class memAddressList : Page
	{
		protected Repeater rptMemAddress;

		protected HtmlGenericControl txtGiftList;

		protected HtmlInputHidden txtMemID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					MemAddress bllAddress = new MemAddress();
					this.txtMemID.Value = MemID.ToString();
					this.rptMemAddress.DataSource = bllAddress.GetList("MemID=" + MemID);
					this.rptMemAddress.DataBind();
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		public string GetMemAddress(object province, object city, object county, object village, object address)
		{
			SysArea bllArea = new SysArea();
			return string.Concat(new string[]
			{
				bllArea.GetNameByID(int.Parse(province.ToString())),
				"省",
				bllArea.GetNameByID(int.Parse(city.ToString())),
				"市",
				bllArea.GetNameByID(int.Parse(county.ToString())),
				bllArea.GetNameByID(int.Parse(village.ToString())),
				address.ToString()
			});
		}
	}
}
