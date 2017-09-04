using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class common_Exec : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string todo = !string.IsNullOrEmpty(Request["todo"]) ? Request["todo"].ToString().ToLower().Trim() : "";
        if (todo == "shortcut")
        {
            Response.Charset = "GB2312";//设置输出流的字符集-中文
            Response.ContentType = "application/octet-stream;";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("连锁会员管理系统_专业版", System.Text.Encoding.UTF8) + ".url;");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流的字符集
            Response.Write("[InternetShortcut]\n");
            Response.Write("IconFile=http://" + Request.ServerVariables["SERVER_NAME"].ToString() + "/favicon.ico\n");
            Response.Write("URL=http://" + Request.ServerVariables["SERVER_NAME"].ToString() + "\n");
            Response.Write("IDList={000214A0-0000-0000-C000-000000000046}\n");
            Response.Write("Prop3=19,2");
            Response.End();
        }
    }
}
