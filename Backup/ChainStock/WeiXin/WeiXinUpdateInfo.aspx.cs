using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;

namespace ChainStock.WeiXin
{
	public class WeiXinUpdateInfo : Page
	{
		private Chain.BLL.Mem memBll = new Chain.BLL.Mem();

		protected string MemWeiXinCard = string.Empty;

		protected string rc = PubFunction.GetDataTimeString();

		protected Chain.Model.Mem memModel;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.MemWeiXinCard = base.Request["MemWeiXinCard"];
			this.memModel = this.memBll.GetMemByWeiXinCard(this.MemWeiXinCard);
		}
	}
}
