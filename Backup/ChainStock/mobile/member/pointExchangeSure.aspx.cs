using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.member
{
	public class pointExchangeSure : Page
	{
		protected HtmlGenericControl txtGiftList;

		protected HtmlAnchor addNewAddress;

		protected HtmlGenericControl memname;

		protected HtmlGenericControl mobile;

		protected HtmlGenericControl address;

		protected HtmlGenericControl spOrderAccount;

		protected HtmlInputHidden txtMemID;

		protected HtmlInputHidden txtAddressID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					this.GetMemBillList(MemID);
					this.spOrderAccount.InnerHtml = DateTime.Now.ToString("yyMMddHHmmssffff");
					this.txtMemID.Value = MemID.ToString();
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		private void GetMemBillList(int MemID)
		{
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			Chain.Model.Mem modelMem = bllMem.GetModel(MemID);
			Chain.BLL.MemAddress bllAddress = new Chain.BLL.MemAddress();
			DataTable dt;
			if (base.Request.QueryString["AddressID"] == null)
			{
				dt = bllAddress.GetList(" MemID=" + MemID + " and IsDefault=1 ").Tables[0];
			}
			else
			{
				dt = bllAddress.GetList(string.Concat(new object[]
				{
					" MemID=",
					MemID,
					" and ID=",
					base.Request.QueryString["AddressID"]
				})).Tables[0];
			}
			if (dt.Rows.Count == 0)
			{
				this.addNewAddress.Visible = true;
			}
			else
			{
				this.txtAddressID.Value = dt.Rows[0]["ID"].ToString();
				this.addNewAddress.Visible = false;
				this.memname.InnerHtml = dt.Rows[0]["MemName"].ToString();
				this.mobile.InnerHtml = dt.Rows[0]["MemMobile"].ToString();
				this.address.InnerHtml = this.GetMemAddress(dt.Rows[0]["MemProvince"], dt.Rows[0]["MemCity"], dt.Rows[0]["MemCounty"], dt.Rows[0]["MemVillage"], dt.Rows[0]["MemDetailAddress"]);
			}
		}

		public string GetMemAddress(object province, object city, object county, object village, object address)
		{
			Chain.BLL.SysArea bllArea = new Chain.BLL.SysArea();
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
