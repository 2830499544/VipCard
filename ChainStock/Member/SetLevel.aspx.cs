using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class SetLevel : PageBase
	{
		protected HtmlForm frmSetLevel;

		protected Literal ltlTitle;

		protected HtmlInputButton btnAddLevel;

		protected Repeater gvMemLevel;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Get_ParameterList();
		}

		private void Get_ParameterList()
		{
			MemLevel bllMemLevel = new MemLevel();
			DataTable dtMemLevel = bllMemLevel.GetLists(" SysShopMemLevel.ShopID=" + this._UserShopID + " ").Tables[0];
			this.gvMemLevel.DataSource = dtMemLevel;
			this.gvMemLevel.DataBind();
			PageBase.BindSerialRepeater(this.gvMemLevel, 0);
		}

		protected string GetLevellLock(bool bolLevellLock)
		{
			string result;
			if (bolLevellLock)
			{
				result = "等级锁定";
			}
			else
			{
				result = "正常升级";
			}
			return result;
		}
	}
}
