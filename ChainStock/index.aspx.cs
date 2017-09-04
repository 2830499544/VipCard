using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock
{
	public class index : Page
	{
		protected Repeater rptFirstMenu;

		protected HtmlGenericControl spRemindCount;

		protected HtmlGenericControl spMessageCount;

		protected HtmlImage spShopPhoto;

		protected HtmlAnchor spUserName;

		protected HtmlInputCheckBox chkIsTel;

		protected HtmlInputCheckBox chkTelNoMember;

		protected Literal litObject;

		private LoginLogic login;

		private Chain.Model.SysUser UserModel
		{
			get
			{
				if (this.login == null)
				{
					this.login = LoginLogic.LoginStatus();
				}
				Chain.Model.SysUser result;
				if (this.login.IsLoggedOn && this.login.LoginUser != null)
				{
					result = this.login.LoginUser;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (PubFunction.ISCheckKey)
			{
				if (PubFunction.IEbrowser)
				{
					this.CreateKeyScript();
				}
				else
				{
					base.Response.Write("<script>location.href='index.aspx'</script>");
				}
			}
			if (this.UserModel != null)
			{
				this.GetFirstMemu();
				Chain.BLL.OnlineMessage bllProposal = new Chain.BLL.OnlineMessage();
				int count = bllProposal.GetRecordCount("IsReply=0 and MessageType=0");
				this.spMessageCount.InnerHtml = count.ToString();
				this.spRemindCount.InnerHtml = this.GetSysRemind(this.UserModel.UserShopID, this.UserModel.UserID).ToString();
				this.spUserName.InnerHtml = this.UserModel.UserName;
				Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(this.UserModel.UserShopID);
				if (modelShop.ShopImageUrl != null && modelShop.ShopImageUrl.ToString() != "")
				{
					this.spShopPhoto.Src = modelShop.ShopImageUrl;
				}
			}
			else
			{
				base.Response.Write("<script>location.href='login.aspx'</script>");
			}
		}

		public void CreateKeyScript()
		{
			StringBuilder strScript = new StringBuilder();
			strScript.Append("<object classid=\"clsid:EA3BA67D-8F11-4936-B01B-760B2E0208F6\" id=\"NT120Client\" name=\"NT120Client\"  style=\"left: 0px; top: 0px\" width=\"0\" height=\"0\"></object>\n");
			strScript.Append("<Script language='JavaScript'>\n");
			strScript.Append("        $(function () {\n");
			strScript.Append("            run();\n");
			strScript.Append("            var interval;\n");
			strScript.Append("            function run() {\n");
			strScript.Append("                interval = setInterval(chat, '25000');\n");
			strScript.Append("            }\n");
			strScript.Append("            function chat() {\n");
			strScript.Append("                var rtn = NT120Client.NTFind();\n");
			strScript.Append("                if (rtn != 0) {\n");
			strScript.Append("                    alert('系统检测到您的加密锁并未插入,或者加密锁已失效！');\n");
			strScript.Append("                    location.href = 'login.aspx?type=loginout';\n");
			strScript.Append("                }\n");
			strScript.Append("                else {\n");
			strScript.Append("                    var state = NT120Client.NTLogin('1234567');\n");
			strScript.Append("                    if (state > 0) {\n");
			strScript.Append("                        alert('当前检测到您的加密锁登录失败,请插入正确的加密锁！');\n");
			strScript.Append("                        location.href = 'login.aspx?type=loginout';\n");
			strScript.Append("                    }\n");
			strScript.Append("                    else {\n");
			strScript.Append("                        doAjax(\"\",\"CheckHardwareID\", { \"Safety\": NT120Client.NTGetHardwareID() }, \"json\",\n");
			strScript.Append("                function (json) {\n");
			strScript.Append("                    if (json > 0) {\n");
			strScript.Append("                        alert('当前检测到您的加密锁认证失败,请插入正确的加密锁！');\n");
			strScript.Append("                        location.href = 'login.aspx?type=loginout';\n");
			strScript.Append("                    }\n");
			strScript.Append("                }\n");
			strScript.Append("                )\n");
			strScript.Append("                    };\n");
			strScript.Append("                    NT120Client.NTLogout();\n");
			strScript.Append("                }\n");
			strScript.Append("            }\n");
			strScript.Append("        });\n");
			strScript.Append("    </script>\n");
			this.litObject.Text = strScript.ToString();
		}

		private int GetSysRemind(int shopID, int userid)
		{
			int sumCount = 0;
			int count = 100000;
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			DataTable dtBirhtday = bllMem.GetBirthdayList(0, shopID, count).Tables[0];
			sumCount += dtBirhtday.Rows.Count;
			DataTable dtMemPastTime = bllMem.GetMemPastTime(" and DATEDIFF(day,getdate(),MemPastTime) = 0 ", shopID, count).Tables[0];
			sumCount += dtMemPastTime.Rows.Count;
			DataTable dtMemPointRest = bllMem.GetMemPointReset(string.Format("MemPoint>0 and DATEDIFF(day,isnull(MemConsumeLastTime,MemCreateTime),getdate()) >= '{0}' and MemShopID = '{1}' ", PubFunction.curParameter.intPointPeriod, shopID), 0, count).Tables[0];
			sumCount += dtMemPointRest.Rows.Count;
			DataTable dtGoods = new Chain.BLL.Goods().GetStockRemind(string.Format("Number < = '{0}' and GoodsType = '0' and ShopID = '{1}'", PubFunction.curParameter.intStockCount, shopID), count).Tables[0];
			sumCount += dtGoods.Rows.Count;
			DataTable dtCustomRemind = new Chain.BLL.SysCustomRemind().GetList("CustomReminder like '%" + PubFunction.UserIDTOName(userid) + "%' and DATEDIFF(day,CustomRemindTime,getdate())<=0 ", count).Tables[0];
			return sumCount + dtCustomRemind.Rows.Count;
		}

		public void GetFirstMemu()
		{
			Chain.BLL.SysGroup bllGroup = new Chain.BLL.SysGroup();
			DataTable dt = bllGroup.GetGroupAuthority("GroupID=" + this.UserModel.UserGroupID + "  and ActionValue=1 and ModuleVisible=1 and ModuleParentID=0 order by ModuleOrder asc").Tables[0];
			this.rptFirstMenu.DataSource = dt;
			this.rptFirstMenu.DataBind();
		}

		protected void rptFirstMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				Repeater rptItem = (Repeater)e.Item.FindControl("rptSecondMenu");
				if (rptItem != null)
				{
					Chain.BLL.SysGroup bllGroup = new Chain.BLL.SysGroup();
					DataTable dt = bllGroup.GetGroupAuthority(string.Concat(new object[]
					{
						"GroupID=",
						this.UserModel.UserGroupID,
						" and  (ActionControl='module' OR ActionControl='page')  and ActionValue=1 and ModuleVisible=1 and ModuleParentID=",
						dr["ModuleID"],
						" order by ModuleOrder asc"
					})).Tables[0];
					rptItem.DataSource = dt;
					rptItem.DataBind();
				}
			}
		}
	}
}
