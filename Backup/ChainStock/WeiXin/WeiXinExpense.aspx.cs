using Chain.BLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.WeiXin
{
	public class WeiXinExpense : Page
	{
		private Mem MemBll = new Mem();

		protected string MemWeiXinCard = string.Empty;

		protected string rc = PubFunction.GetDataTimeString();

		protected Repeater Rpt_WeiXinExpense;

		protected HtmlGenericControl noProduct;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.MemWeiXinCard = base.Request["MemWeiXinCard"];
			this.BindData();
		}

		public void BindData()
		{
			int rowCount = 0;
			string strWhere = "MemWeiXinCard='" + this.MemWeiXinCard + "'";
			DataTable dt = this.MemBll.GetMemExpenseToWeiXin(10, 1, out rowCount, new string[]
			{
				strWhere
			}).Tables[0];
			if (dt.Rows.Count > 0)
			{
				this.Rpt_WeiXinExpense.DataSource = dt;
				this.Rpt_WeiXinExpense.DataBind();
			}
			else
			{
				this.noProduct.Style.Add("display", "\"\"");
			}
		}
	}
}
