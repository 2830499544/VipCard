<%@ WebHandler Language="C#" Class="upload" %>

using System;
using System.Web;

public class upload : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        if (context.Request.Files["WeiXin"] != null)
        {
            try
            {
                HttpPostedFile file = context.Request.Files["WeiXin"];
                string expandName = System.IO.Path.GetExtension(file.FileName);

                string fileName = DateTime.Now.ToString("ddHHmmssffff") + expandName;
                string fullPath = context.Server.MapPath("~/Upload/WeiXin/Images/" + fileName);
                file.SaveAs(fullPath);
                context.Response.Write(fileName);
            }
            catch
            {
                context.Response.Write("0");
            }
        }
       if (context.Request["WebImage"] != null)
        { 
            HttpPostedFile file = context.Request.Files["FileData"];
            string fileName = context.Request["WebImage"] + ".png";
            string fullPath = context.Server.MapPath("~/Upload/WebImage/" + fileName);
            file.SaveAs(fullPath);
            string virtualPath = context.Request["folder"] + "/" + fileName;
            context.Response.Write(virtualPath);           
        }
    
        //else
        //{
        //    HttpPostedFile file = context.Request.Files["FileData"];
        //    string virtualPath = context.Request["folder"] + "/" + DateTime.Now.ToString("yyyyMM") + "/";
        //    string uploadpath = context.Server.MapPath(virtualPath);
        //    string fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));
        //    string fileName = DateTime.Now.ToString("ddHHmmssffff") + fileExt;

        //    if (file != null)
        //    {
        //        if (!System.IO.Directory.Exists(uploadpath))
        //        {
        //            System.IO.Directory.CreateDirectory(uploadpath);
        //        }

        //        string imageSrc = Chain.Common.ImagePlus.UpLoadImage(file, virtualPath, "s", 150, 150);
        //        //context.Response.Write(virtualPath + fileName);  
        //        context.Response.Write(virtualPath + imageSrc);
        //    }
        //    else
        //    {
        //        context.Response.Write("0");
        //    }
        //}
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}