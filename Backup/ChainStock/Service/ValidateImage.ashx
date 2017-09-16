<%@ WebHandler Language="C#" Class="ValidateImage" %>

  
  using System;
  using System.Web;
  using System.Web.SessionState;
  using System.Drawing;
  using System.Drawing.Imaging;
  using System.Text;
  
 /// <summary>
 /// 验证码
 /// </summary>
 public class ValidateImage : IHttpHandler, IRequiresSessionState
 {
     int intLength = 4;               //长度
     string strIdentify = "IdentifyCode"; //随机字串存储键值，以便存储到Session中
     
     public ValidateImage()
     {        
     }
 
     /// <summary>
     ///  生成验证图片核心代码
     /// </summary>
     /// <param name="hc"></param>
     public void ProcessRequest(HttpContext hc)
     {
         //设置输出流图片格式
         if (hc.Session["valcode"]!=null){
             hc.Session["valcode"] = null;
         }
         hc.Response.ContentType = "image/gif";         
         Bitmap b = new Bitmap(70, 30);
         Graphics g = Graphics.FromImage(b);
         g.FillRectangle(new SolidBrush(Color.YellowGreen), 0, 0, 70, 30);
         Font font = new Font(FontFamily.GenericSerif, 24, FontStyle.Bold, GraphicsUnit.Pixel);
         Random r = new Random();
 
         //合法随机显示字符列表
         string strLetters = "1234567890";// "abcdefghijklmnopqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ23456789";
         StringBuilder s = new StringBuilder();
         
         //将随机生成的字符串绘制到图片上
         for (int i = 0; i < intLength; i++)
         {
             s.Append(strLetters.Substring(r.Next(0, strLetters.Length - 1), 1));
             g.DrawString(s[s.Length - 1].ToString(), font, new SolidBrush(Color.Blue), i * 15, r.Next(0, 8));
         }
 
         ////生成干扰线条
         Pen pen = new Pen(new SolidBrush(Color.Blue), 2);
         for (int i = 0; i < 5; i++)
         {
             g.DrawLine(pen, new Point(r.Next(0, 100), r.Next(0, 59)), new Point(r.Next(0, 199), r.Next(0, 59)));
         }
         b.Save(hc.Response.OutputStream, ImageFormat.Gif);
        // hc.Session[strIdentify] = s.ToString(); //先保存在Session中，验证与用户输入是否一致
         Chain.Common.ValCodeModel CMVc = new Chain.Common.ValCodeModel();
         CMVc.valCode = s.ToString();
         CMVc.Failure = 3 * 60;
         CMVc.CreateDate = DateTime.Now;
         hc.Session["valcode"] = CMVc;
         //hc.Response.SetCookie(new HttpCookie("identifyCode", s.ToString()));
         hc.Response.End();
    
     }
     
     /// <summary>
     /// 表示此类实例是否可以被多个请求共用(重用可以提高性能)
     /// </summary>
     public bool IsReusable
     {
         get
        {
             return true;
         }
     }
}