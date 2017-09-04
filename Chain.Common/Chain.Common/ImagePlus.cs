using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace Chain.Common
{
	public class ImagePlus
	{
		public static byte[] ImgToByt(Image img)
		{
			if (img == null)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			img.Save(memoryStream, ImageFormat.Jpeg);
			return memoryStream.GetBuffer();
		}

		public static Image BytToImg(byte[] byt)
		{
			if (byt.Length > 0)
			{
				MemoryStream stream = new MemoryStream(byt);
				return Image.FromStream(stream);
			}
			return null;
		}

		public static string UpLoadImage(HttpPostedFile myFile, string sSavePath, string sThumbExtension, int intThumbWidth, int intThumbHeight)
		{
			string text = "";
			string str = "";
			if (myFile == null)
			{
				return "没有选择图片";
			}
			int contentLength = myFile.ContentLength;
			if (contentLength == 0)
			{
				return "没有选择上传图片";
			}
			string text2 = Path.GetExtension(myFile.FileName).ToLower();
			if (text2 != ".jpg" && text2 != ".jpge" && text2 != ".gif" && text2 != ".bmp" && text2 != ".png")
			{
				return "图片格式不正确";
			}
			byte[] array = new byte[contentLength];
			myFile.InputStream.Read(array, 0, contentLength);
			str = DateTime.Now.ToString("ddHHmmssffff") + text2;
			FileStream fileStream = new FileStream(HttpContext.Current.Server.MapPath(sSavePath + str), FileMode.Create, FileAccess.Write);
			fileStream.Write(array, 0, array.Length);
			fileStream.Close();
			try
			{
				using (Image image = Image.FromFile(HttpContext.Current.Server.MapPath(sSavePath + str)))
				{
					int width = image.Width;
					int height = image.Height;
					int num;
					int num2;
					if (width / height <= intThumbWidth / intThumbHeight)
					{
						num = intThumbWidth;
						num2 = intThumbWidth * height / width;
					}
					else
					{
						num = intThumbHeight * width / height;
						num2 = intThumbHeight;
					}
					text = sThumbExtension + str;
					string filename = HttpContext.Current.Server.MapPath(sSavePath) + text;
					using (Image image2 = new Bitmap(num, num2))
					{
						using (Graphics graphics = Graphics.FromImage(image2))
						{
							graphics.InterpolationMode = InterpolationMode.High;
							graphics.SmoothingMode = SmoothingMode.HighQuality;
							graphics.Clear(Color.Black);
							graphics.DrawImage(image, new Rectangle(0, 0, num, num2), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
						}
						using (Image image3 = new Bitmap(intThumbWidth, intThumbHeight))
						{
							using (Graphics graphics2 = Graphics.FromImage(image3))
							{
								graphics2.InterpolationMode = InterpolationMode.High;
								graphics2.SmoothingMode = SmoothingMode.HighQuality;
								graphics2.Clear(Color.Black);
								int srcX = (num - intThumbWidth) / 2;
								int srcY = (num2 - intThumbHeight) / 2;
								graphics2.DrawImage(image2, new Rectangle(0, 0, intThumbWidth, intThumbHeight), srcX, srcY, intThumbWidth, intThumbHeight, GraphicsUnit.Pixel);
								graphics2.Dispose();
								image3.Save(filename, ImageFormat.Jpeg);
							}
						}
					}
				}
			}
			catch
			{
				File.Delete(HttpContext.Current.Server.MapPath(sSavePath + str));
				return "图片格式不正确";
			}
			return text;
		}

		public static void UpLoadExcel(HttpPostedFile myFile, string sSavePath)
		{
			string filename = HttpContext.Current.Server.MapPath("/WebUI/AppData/upload/member/member.xls");
			myFile.SaveAs(filename);
		}

		public static string SaveFile(HttpPostedFile myFile, string savePath, string allowType)
		{
			string fileName = myFile.FileName;
			string text = fileName.Substring(fileName.LastIndexOf(".") + 1);
			Random random = new Random();
			string str = DateTime.Now.Ticks.ToString() + random.Next(100).ToString();
			int num = 4194304;
			if (myFile.ContentLength >= num)
			{
				return "ERROR:上传的文件不能超过4M";
			}
			if (allowType.IndexOf(text) == -1)
			{
				return "ERROR:不允许上传此类文件，请与系统管理员联系";
			}
			if (!Directory.Exists(savePath))
			{
				Directory.CreateDirectory(savePath);
			}
			string text2 = savePath + str + "." + text;
			myFile.SaveAs(text2);
			return "TRUE:[FILENAME]" + fileName + "[SAVEPATH]" + text2;
		}
	}
}
