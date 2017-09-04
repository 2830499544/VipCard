using Chain.BLL;
using Chain.Model;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.WeiXin
{
	public class WeiXinGiftShow : Page
	{
		protected HtmlInputText txtConvertNumber;

		protected Chain.Model.PointGift giftModel;

		protected string MemWeiXinCard = string.Empty;

		protected string GiftID = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.MemWeiXinCard = base.Request["MemWeiXinCard"];
			this.GiftID = base.Request["GiftID"];
			this.giftModel = new Chain.BLL.PointGift().GetModel(int.Parse(this.GiftID));
		}

		protected string GetPhoto()
		{
			string path = "http://" + PubFunction.curParameter.strDoMain;
			string strPhoto = "http://" + PubFunction.curParameter.strDoMain + this.giftModel.GiftPhoto;
			return File.Exists(base.Server.MapPath(this.giftModel.GiftPhoto)) ? strPhoto : "/Upload/MemPhoto/nophoto.gif";
		}
	}
}
