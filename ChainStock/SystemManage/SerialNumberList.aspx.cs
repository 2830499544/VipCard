using Chain.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
using System.Data;

namespace ChainStock.SystemManage
{
    public partial class SerialNumberList : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetSysUserList("");
                bindsltIsUse();
            }
        }

        void bindsltIsUse()
        {
            sltIsUse.Items.Add(new ListItem("请选择"));
            sltIsUse.Items.Add(new ListItem("是"));
            sltIsUse.Items.Add(new ListItem("否"));
        }


        private void GetSysUserList(string strSql)
        {
            SysSerialNumber sysSerialNumber = new SysSerialNumber();
            int Counts = this.NetPagerParameter.RecordCount;
            
            DataTable db = sysSerialNumber.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
            {
                 strSql
            }).Tables[0];
            this.NetPagerParameter.RecordCount = Counts;
            this.NetPagerParameter.CustomInfoHTML = string.Format("<div class=\"results\"><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[]
            {
                this.NetPagerParameter.CurrentPageIndex,
                this.NetPagerParameter.PageCount,
                this.NetPagerParameter.RecordCount,
                this.NetPagerParameter.PageSize
            });
            this.gvSysUserList.DataSource = db;
            this.gvSysUserList.DataBind();
            PageBase.BindSerialRepeater(this.gvSysUserList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
        }

        protected void btnUserSearch_Click1(object sender, EventArgs e)
        {
            this.NetPagerParameter.CurrentPageIndex = 1;
            int isUse = sltIsUse.Value == "是" ? 1 : 0;
            string serialNumber = txtSerialNumber.Value;
            string shopName = txtShopName.Value;
            string vSql;
            if (sltIsUse.Value=="请选择")
                vSql = string.Format("(ShopName Like '%{0}%' or '{0}'='') and ( SerialNumber Like '%{1}%' or '{1}'='')", shopName,serialNumber,isUse);
            else
                vSql = string.Format("(ShopName Like '%{0}%' or '{0}'='') and ( SerialNumber Like '%{1}%' or '{1}'='') and IsUse={2}", shopName, serialNumber, isUse);
            GetSysUserList(vSql);
        }

        protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.NetPagerParameter.CurrentPageIndex = 1;
            this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
        }

        protected void gvSysUserList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
        }

        protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
        {
            this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
            this.GetSysUserList("");
        }
    }
}