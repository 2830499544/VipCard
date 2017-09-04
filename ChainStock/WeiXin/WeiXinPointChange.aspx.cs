using Chain.BLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.WeiXin
{
	public class WeiXinPointChange : Page
	{
		private PointLog pointLogBll = new PointLog();

		protected string MemWeiXinCard = string.Empty;

		protected string rc = PubFunction.GetDataTimeString();

		protected Repeater Rpt_WeiXinPointChange;

		protected HtmlGenericControl noProduct;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.MemWeiXinCard = base.Request["MemWeiXinCard"];
			this.BindData();
		}

		public void BindData()
		{
			int rowCount = 0;
			string strWhere = "MemWeiXinCard='" + this.MemWeiXinCard + "' and PointLog.PointShopID = SysShop.ShopID and PointLog.PointMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and PointLog.PointUserID = SysUser.UserID";
			DataTable dt = this.pointLogBll.GetListSP(10, 1, out rowCount, new string[]
			{
				strWhere
			}).Tables[0];
			if (dt.Rows.Count > 0)
			{
				this.Rpt_WeiXinPointChange.DataSource = dt;
				this.Rpt_WeiXinPointChange.DataBind();
			}
			else
			{
				this.noProduct.Style.Add("display", "\"\"");
			}
		}
	}
}
