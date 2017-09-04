using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;

namespace ChainStock.WeiXin
{
	public class WeiXinGiftConvertList : Page
	{
		protected string MemWeiXinCard = string.Empty;

		protected Chain.Model.PointGift giftModel = null;

		protected Chain.Model.Mem mem = null;

		protected int Num;

		protected int GiftID;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.MemWeiXinCard = base.Request["MemWeiXinCard"];
			this.GiftID = int.Parse(base.Request["GiftID"]);
			this.Num = int.Parse(base.Request["Num"]);
			this.mem = new Chain.BLL.Mem().GetMemByWeiXinCard(this.MemWeiXinCard);
			this.giftModel = new Chain.BLL.PointGift().GetModel(this.GiftID);
		}
	}
}
