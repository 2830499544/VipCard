using Chain.BLL;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.WeiXin
{
	public class WeiXinPointExchange : Page
	{
		private Chain.BLL.PointGift pointGiftBll = new Chain.BLL.PointGift();

		protected string MemWeiXinCard = string.Empty;

		protected string rc = PubFunction.GetDataTimeString();

		protected Repeater Rpt_WeiXinPointExchange;

		protected HtmlGenericControl noProduct;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.MemWeiXinCard = base.Request["MemWeiXinCard"];
			if (!string.IsNullOrEmpty(this.MemWeiXinCard))
			{
				this.BindData();
			}
		}

		protected void Rpt_WeiXinPointExchange_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				HtmlAnchor linkimg = (HtmlAnchor)e.Item.FindControl("linkimg");
				if (linkimg != null)
				{
					linkimg.HRef = "WeiXinGiftShow.aspx?GiftID=" + linkimg.HRef + "&MemWeiXinCard=" + this.MemWeiXinCard;
				}
			}
		}

		private void BindData()
		{
			string strWhere = string.Empty;
			if (PubFunction.curParameter.bolGiftShare)
			{
				strWhere = "PointGift.GiftClassID=GiftClass.GiftClassID and PointGift.GiftShopID=SysShop.ShopID";
			}
			else
			{
				strWhere = "PointGift.GiftClassID=GiftClass.GiftClassID and PointGift.GiftShopID=SysShop.ShopID and SysShop.ShopID=1";
			}
			int count;
			DataTable dt = this.pointGiftBll.GetListSP(10, 1, false, out count, new string[]
			{
				strWhere
			}).Tables[0];
			if (dt.Rows.Count > 0)
			{
				this.Rpt_WeiXinPointExchange.DataSource = dt;
				this.Rpt_WeiXinPointExchange.DataBind();
			}
			else
			{
				this.noProduct.Style.Add("display", "\"\"");
			}
		}

		protected string GetPhoto(string Photo)
		{
			string path = "http://" + PubFunction.curParameter.strDoMain;
			string strPhoto = "http://" + PubFunction.curParameter.strDoMain + Photo;
			return File.Exists(base.Server.MapPath(Photo)) ? strPhoto : "/Upload/MemPhoto/nophoto.gif";
		}
	}
}
