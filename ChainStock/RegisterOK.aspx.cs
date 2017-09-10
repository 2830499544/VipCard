using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChainStock
{
    public partial class RegisterOK : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ( !IsPostBack)
            {
                txtShopName.InnerText = string.Format("企业名称:{0}", Request["ShopName"]);
                txtShopCode.InnerText = string.Format("企业代码:{0}", Request["ShopCode"]);
                txtUserName.InnerText = "用户名:Admin";
                txtPassword.InnerText = string.Format("密码:{0}", Request["Password"]);
            }
        }
    }
}