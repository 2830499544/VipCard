using Chain.BLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.WeiXin
{
	public class WeiXinRecharge : Page
	{
		protected Repeater rpt_Recharge;

		protected HtmlGenericControl noProduct;

		private MemRecharge memRechargeBll = new MemRecharge();

		protected string MemWeiXinCard = string.Empty;

		protected string rc = PubFunction.GetDataTimeString();

		protected void Page_Load(object sender, EventArgs e)
		{
			this.MemWeiXinCard = base.Request["MemWeiXinCard"];
			this.BindData();
		}

		public void BindData()
		{
			int rowCount = 0;
			string strWhere = "MemWeiXinCard='" + this.MemWeiXinCard + "' and MemRecharge.RechargeShopID = SysShop.ShopID and MemRecharge.RechargeMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and MemRecharge.RechargeUserID = SysUser.UserID";
			DataTable dt = this.memRechargeBll.GetListSP(10, 1, out rowCount, new string[]
			{
				strWhere
			}).Tables[0];
			if (dt.Rows.Count > 0)
			{
				this.rpt_Recharge.DataSource = dt;
				this.rpt_Recharge.DataBind();
			}
			else
			{
				this.noProduct.Style.Add("display", "\"\"");
			}
		}
	}
}
