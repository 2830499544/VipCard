using Chain.BLL;
using Chain.Model;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace ChainStock.Service
{
	public class MicroWebsiteUpload : IHttpHandler
	{
		private HttpContext currentContext;

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			this.currentContext = context;
			string savePath = string.Empty;
			string saveName = string.Empty;
			try
			{
				if (context.Request.Files["MicroWebsiteIndex"] != null)
				{
					Match match = Regex.Match(context.Request["name"], "(/.*/)(.*)");
					HttpPostedFile sourse = context.Request.Files["MicroWebsiteIndex"];
					savePath = match.Groups[1].Value;
					saveName = match.Groups[2].Value;
					this.SaveImg(sourse, savePath, saveName);
					context.Response.Write("1");
				}
				else if (context.Request.Files["Cert"] != null)
				{
					HttpPostedFile sourse = context.Request.Files["Cert"];
					savePath = "~/cert/";
					saveName = "apiclient_cert.p12";
					this.SaveImg(sourse, savePath, saveName);
					context.Response.Write(saveName);
				}
				else if (context.Request.Files["ShopPhoto"] != null)
				{
					HttpPostedFile sourse = context.Request.Files["ShopPhoto"];
					savePath = "~/Upload/ShopPhoto/";
					saveName = DateTime.Now.ToString("yyMMddhhmmssffff") + Path.GetExtension(sourse.FileName);
					this.SaveImg(sourse, savePath, saveName);
					context.Response.Write(saveName);
				}
				else if (context.Request.Files["MicroWebsiteNews"] != null)
				{
					HttpPostedFile sourse = context.Request.Files["MicroWebsiteNews"];
					savePath = "~/Upload/MicroWebsite/MicroWebsiteNews/";
					saveName = DateTime.Now.ToString("yyMMddhhmmssffff") + Path.GetExtension(sourse.FileName);
					this.SaveImg(sourse, savePath, saveName);
					context.Response.Write(saveName);
				}
				else if (context.Request.Files["MicroWebsiteAlbum"] != null)
				{
					HttpPostedFile sourse = context.Request.Files["MicroWebsiteAlbum"];
					savePath = "~/Upload/MicroWebsite/MicroWebsiteAlbum/";
					saveName = DateTime.Now.ToString("yyMMddhhmmssffff") + Path.GetExtension(sourse.FileName);
					this.SaveImg(sourse, savePath, saveName);
					context.Response.Write(saveName);
				}
				else if (context.Request.Files["MicroWebsitePhoto"] != null)
				{
					HttpPostedFile sourse = context.Request.Files["MicroWebsitePhoto"];
					savePath = "~/Upload/MicroWebsite/MicroWebsitePhoto/";
					saveName = DateTime.Now.ToString("yyMMddhhmmssffff") + Path.GetExtension(sourse.FileName);
					this.SaveImg(sourse, savePath, saveName);
					context.Response.Write(saveName);
				}
				else if (context.Request.Files["MicroWebsitePromotions"] != null)
				{
					HttpPostedFile sourse = context.Request.Files["MicroWebsitePromotions"];
					savePath = "~/Upload/MicroWebsite/MicroWebsitePromotions/";
					saveName = DateTime.Now.ToString("yyMMddhhmmssffff") + Path.GetExtension(sourse.FileName);
					this.SaveImg(sourse, savePath, saveName);
					context.Response.Write(saveName);
				}
				else if (context.Request.Files["MicroWebsiteBackground"] != null)
				{
					HttpPostedFile sourse = context.Request.Files["MicroWebsiteBackground"];
					savePath = "~/Upload/WeiXin/Images/";
					saveName = "wxindex_bg.png";
					this.SaveImg(sourse, savePath, saveName);
					context.Response.Write(saveName);
				}
				else if (context.Request.Files["IndexLogo"] != null)
				{
					HttpPostedFile sourse = context.Request.Files["IndexLogo"];
					savePath = "~/images/";
					saveName = "logo.png";
					this.SaveImg(sourse, savePath, saveName);
					context.Response.Write(saveName);
				}
				else if (context.Request.Files["MemPhoto"] != null)
				{
					HttpPostedFile sourse = context.Request.Files["MemPhoto"];
					savePath = "~/Upload/MemPhoto/";
					saveName = DateTime.Now.ToString("yyMMddhhmmssffff") + Path.GetExtension(sourse.FileName);
					this.SaveImg(sourse, savePath, saveName);
					context.Response.Write(saveName);
				}
				else if (context.Request.Files["MicroWebsiteProductCenter"] != null)
				{
					HttpPostedFile sourse = context.Request.Files["MicroWebsiteProductCenter"];
					savePath = "~/Upload/MicroWebsite/MicroWebsiteProductCenter/";
					saveName = DateTime.Now.ToString("yyMMddhhmmssffff") + Path.GetExtension(sourse.FileName);
					this.SaveImg(sourse, savePath, saveName);
					context.Response.Write(saveName);
				}
				else if (context.Request.Files["MicroWebsiteSymbol"] != null)
				{
					HttpPostedFile sourse = context.Request.Files["MicroWebsiteSymbol"];
					savePath = "~/Upload/MicroWebsite/MicroWebsiteSymbol/";
					saveName = DateTime.Now.ToString("yyMMddhhmmssffff") + Path.GetExtension(sourse.FileName);
					this.SaveImg(sourse, savePath, saveName);
					context.Response.Write(saveName);
				}
				else if (context.Request.Files["MicroGoods"] != null)
				{
					HttpPostedFile sourse = context.Request.Files["MicroGoods"];
					savePath = "~/Upload/MicroWebsite/MicroGoods/";
					saveName = DateTime.Now.ToString("yyMMddhhmmssffff") + Path.GetExtension(sourse.FileName);
					this.SaveImg(sourse, savePath, saveName);
					context.Response.Write(saveName);
				}
				else if (context.Request["type"] != null && context.Request["type"] == "index")
				{
					int MerchantID = Convert.ToInt32(context.Request["MerchantID"]);
					this.DrawImage(MerchantID);
				}
				else if (context.Request.Files["WeiXinPhoto"] != null)
				{
					HttpPostedFile sourse = context.Request.Files["WeiXinPhoto"];
					savePath = "~/Upload/WeiXin/Images/";
					string name = context.Request["name"];
					if (!string.IsNullOrEmpty(name))
					{
						string text = name;
						if (text != null)
						{
							if (!(text == "1"))
							{
								if (!(text == "2"))
								{
									if (text == "3")
									{
										saveName = "memCard.jpg";
									}
								}
								else
								{
									saveName = "wwz.jpg";
								}
							}
							else
							{
								saveName = "bg.jpg";
							}
						}
					}
					this.SaveImg(sourse, savePath, saveName);
					context.Response.Write(saveName);
				}
				else
				{
					context.Response.Write("0");
				}
			}
			catch (Exception e)
			{
				Chain.Model.SysError err = new Chain.Model.SysError();
				err.UserID = 0;
				err.ShopID = 0;
				err.ErrorTime = DateTime.Now;
				err.ErrorContent = e.ToString();
				err.Ipaddress = PubFunction.ipAdress;
				new Chain.BLL.SysError().Add(err);
				throw e;
			}
		}

		private void SaveImg(HttpPostedFile sourse, string savePath, string saveName)
		{
			sourse.SaveAs(this.currentContext.Server.MapPath(savePath) + saveName);
		}

		private void DrawImage(int MerchantID)
		{
			Chain.BLL.MerchantSite MerchantSiteBll = new Chain.BLL.MerchantSite();
			Chain.Model.MerchantSite MerchantSiteModel = MerchantSiteBll.GetModel(MerchantID);
			string imagePath = this.currentContext.Server.MapPath(MerchantSiteModel.MerchantPhoto);
			using (Bitmap smallImage = new Bitmap(imagePath))
			{
				Graphics g = Graphics.FromImage(smallImage);
				this.currentContext.Response.ContentType = "image/jpeg";
				smallImage.Save(this.currentContext.Response.OutputStream, ImageFormat.Jpeg);
			}
		}
	}
}
