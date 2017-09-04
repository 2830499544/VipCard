using Chain.BLL;
using Chain.Model;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Web.Common;

namespace ChainStock.WeiXin
{
	public class MemberManipulate : Page
	{
		protected HtmlImage weixinCardImg;

		protected HtmlAnchor weixinUpdateInfo;

		protected HtmlAnchor weixinRecharge;

		protected HtmlAnchor weixinExpense;

		protected HtmlAnchor weixinPointChange;

		protected HtmlAnchor weixinConvertHistory;

		protected HtmlAnchor weixinPointExchange;

		protected HtmlAnchor weixinLookCoupon;

		protected void Page_Load(object sender, EventArgs e)
		{
			string MemWeiXinCard = base.Request["MemWeiXinCard"];
			if (!string.IsNullOrEmpty(MemWeiXinCard))
			{
				Chain.Model.Mem mem = new Chain.BLL.Mem().GetMemByWeiXinCard(MemWeiXinCard);
				if (mem != null)
				{
					this.weixinRecharge.HRef = "WeiXinRecharge.aspx?MemWeiXinCard=" + MemWeiXinCard + "&rc=" + PubFunction.GetDataTimeString();
					this.weixinCardImg.Src = "../Upload/WeiXin/Images/" + MemWeiXinCard + ".jpg?rc=" + PubFunction.GetDataTimeString();
					this.weixinExpense.HRef = "WeiXinExpense.aspx?MemWeiXinCard=" + MemWeiXinCard + "&rc=" + PubFunction.GetDataTimeString();
					this.weixinPointChange.HRef = "WeiXinPointChange.aspx?MemWeiXinCard=" + MemWeiXinCard + "&rc=" + PubFunction.GetDataTimeString();
					this.weixinUpdateInfo.HRef = "WeiXinUpdateInfo.aspx?MemWeiXinCard=" + MemWeiXinCard + "&rc=" + PubFunction.GetDataTimeString();
					this.weixinPointExchange.HRef = "WeiXinPointExchange.aspx?MemWeiXinCard=" + MemWeiXinCard + "&rc=" + PubFunction.GetDataTimeString();
					this.weixinConvertHistory.HRef = "WeiXinConvertHistory.aspx?MemWeiXinCard=" + MemWeiXinCard + "&rc=" + PubFunction.GetDataTimeString();
					this.weixinLookCoupon.HRef = "WeiXinCoupon.aspx?MemWeiXinCard=" + MemWeiXinCard + "&rc=" + PubFunction.GetDataTimeString();
					this.DrawImage(mem, MemWeiXinCard);
				}
			}
		}

		protected string GetCardDescription()
		{
			string Title = new Chain.BLL.WeiXinRule().GetCardDesc().Trim();
			return Title + "微信会员中心";
		}

		public void DrawImage(Chain.Model.Mem mem, string FromUserName)
		{
			Bitmap smallWeiXin = new Bitmap(130, 130);
			Image weixinImg = QRCodeImage.CreateQRCode(mem.MemCard);
			Graphics g = Graphics.FromImage(smallWeiXin);
			g.DrawImage(weixinImg, new Point(-35, -35));
			string savePath = base.Server.MapPath("~/Upload/WeiXin/Images/" + FromUserName + ".jpg");
			Bitmap bigWeiXin = new Bitmap(200, 200);
			g = Graphics.FromImage(bigWeiXin);
			g.DrawImage(smallWeiXin, new Rectangle(0, 0, 200, 200), new Rectangle(0, 0, 130, 130), GraphicsUnit.Pixel);
			string bg = base.Server.MapPath("~/Upload/WeiXin/Images/bg.jpg");
			Bitmap result = new Bitmap(bg);
			g = Graphics.FromImage(result);
			g.DrawImage(bigWeiXin, new RectangleF(50f, 60f, 200f, 200f), new RectangleF(0f, 0f, 200f, 200f), GraphicsUnit.Pixel);
			g.DrawString("NO:" + mem.MemCard, new Font("微软雅黑", 34f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, new PointF(280f, 70f));
			g.DrawString("积分:" + mem.MemCard, new Font("微软雅黑", 30f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, new PointF(280f, 115f));
			g.DrawString("余额:" + mem.MemMoney.ToString("F2"), new Font("微软雅黑", 30f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, new PointF(280f, 160f));
			string pastStr = (mem.MemPastTime.ToString("yyyy-MM-dd") == "2900-01-01") ? "有效期:永久有效" : ("有效期:" + mem.MemPastTime.ToString("yyyy-MM-dd"));
			g.DrawString(pastStr, new Font("微软雅黑", 30f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, new PointF(280f, 205f));
			result.Save(savePath, ImageFormat.Jpeg);
		}
	}
}
