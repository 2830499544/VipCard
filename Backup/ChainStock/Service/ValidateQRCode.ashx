<%@ WebHandler Language="C#" Class="ValidateQRCode" %>

  
using System;
using System.Web;
using System.Web.SessionState;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using Web.Common;



/// <summary>
/// 验证码
/// </summary>
public class ValidateQRCode : IHttpHandler, IRequiresSessionState
{

    public ValidateQRCode()
    {
    }

    /// <summary>
    ///  生成验证图片核心代码
    /// </summary>
    /// <param name="hc"></param>
    public void ProcessRequest(HttpContext hc)
    {
        //设置输出流图片格式
        hc.Response.ContentType = "image/gif";

        Bitmap b = Web.Common.QRCodeImage.CreateQRCode(hc.Request["content"].ToString());
        b.Save(hc.Response.OutputStream, ImageFormat.Gif);
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