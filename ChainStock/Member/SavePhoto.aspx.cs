using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.Member
{
	public class SavePhoto : Page
	{
		private string PhotoName = "";

		protected HtmlForm form1;

		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.ContentType = "text/plain";
			if (base.Request.Form["PHeight"] != null && base.Request.Form["PWidth"] != null && base.Request.Form["strBMP"] != null)
			{
				try
				{
					int height = int.Parse(base.Request.Form["PHeight"].ToString());
					int width = int.Parse(base.Request.Form["PWidth"].ToString());
					string strBmp = base.Request.Form["strBMP"].ToString();
					this.PhotoName = DateTime.Now.ToString("ddHHmmssffff") + ".jpg";
					this.SaveBmp(this.BuildBitmap(width, height, strBmp), base.Server.MapPath("..\\\\Upload\\\\MemPhoto\\\\" + DateTime.Now.ToString("yyyyMM") + "\\"));
					base.Response.SetCookie(new HttpCookie("PhotoName", DateTime.Now.ToString("yyyyMM") + "/" + this.PhotoName));
					base.Response.Write("RetMsg=true");
					base.Response.End();
				}
				catch
				{
				}
			}
		}

		public Bitmap BuildBitmap(int width, int height, string strBmp)
		{
			Bitmap tmpBmp = new Bitmap(width, height, PixelFormat.Format32bppRgb);
			string[] StmpBmp = strBmp.Split(new char[]
			{
				','
			});
			int pos = 0;
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					tmpBmp.SetPixel(x, y, Color.FromArgb(int.Parse(StmpBmp[pos], NumberStyles.HexNumber)));
					pos++;
				}
			}
			return tmpBmp;
		}

		public void SaveBmp(Bitmap bmp, string filePath)
		{
			if (!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}
			bmp.Save(filePath + this.PhotoName, ImageFormat.Bmp);
		}
	}
}
