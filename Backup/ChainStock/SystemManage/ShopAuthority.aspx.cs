using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class ShopAuthority : PageBase
	{
		protected HtmlForm form1;

		protected Literal ltlTitle;

		protected Repeater gvShopAuthorityList;

		protected HiddenField ShopID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["Sid"] != null)
				{
					this.ShopID.Value = base.Request.QueryString["Sid"];
					this.Get_ParameterList();
					this.Get_ShopAuthority();
				}
				else
				{
					base.Response.Write("<script>history.back();</script>");
				}
			}
		}

		private void Get_ParameterList()
		{
			SysShop shop = new SysShop();
			int Counts = 0;
			DataTable db = shop.GetListS(1000, 1, out Counts, new string[]
			{
				"ShopID>0 and ShopID !=" + this.ShopID.Value
			}).Tables[0];
			this.gvShopAuthorityList.DataSource = db;
			this.gvShopAuthorityList.DataBind();
			PageBase.BindSerialRepeater(this.gvShopAuthorityList, 0);
		}

		public void Get_ShopAuthority()
		{
			DataTable db = new SysShopAuthority().GetList(" ShopAuthorityShopID=" + this.ShopID.Value).Tables[0];
			if (db.Rows.Count > 0)
			{
				string[] Authority = db.Rows[0]["ShopAuthorityData"].ToString().Split(new char[]
				{
					','
				});
				for (int i = 0; i < this.gvShopAuthorityList.Items.Count; i++)
				{
					HtmlInputCheckBox chk = this.gvShopAuthorityList.Items[i].FindControl("chkitem") as HtmlInputCheckBox;
					string[] array = Authority;
					for (int j = 0; j < array.Length; j++)
					{
						string s = array[j];
						if (chk.Value == s)
						{
							chk.Checked = true;
							break;
						}
					}
				}
			}
		}
	}
}
