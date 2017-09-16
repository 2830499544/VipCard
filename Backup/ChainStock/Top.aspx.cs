using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock
{
	public class Top : PageBase
	{
		protected string MainMenuString;

		protected string ChildMenuString;

		protected Literal litObject;

		protected HtmlForm form1;

		protected HtmlInputCheckBox chkIsTel;

		protected HtmlInputCheckBox chkTelNoMember;

		protected HtmlGenericControl lblUserName;

		protected HtmlGenericControl lblUserShop;

		protected HtmlGenericControl lblUserGroup;

		protected HtmlInputPassword txtPwd;

		protected HtmlInputPassword txtNewPwd;

		protected HtmlInputPassword txtNewRePwd;

		protected HtmlInputHidden UseriD;

		protected HtmlImage topLogo;

		protected HtmlAnchor EditUser;

		protected HtmlAnchor topCaller;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.GetModule();
			if (PubFunction.ISCheckKey)
			{
				if (PubFunction.IEbrowser)
				{
					this.CreateKeyScript();
				}
				else
				{
					base.Response.Write("<script>top.location.href='index.aspx'</script>");
				}
			}
			this.EditUser.InnerText = this._UserName;
			this.lblUserName.InnerText = this._UserName;
			this.UseriD.Value = this._UserID.ToString();
			this.lblUserShop.InnerText = PubFunction.ShopIDToName(this._UserShopID);
			this.lblUserShop.InnerText = PubFunction.ShopIDToName(this._UserShopID);
			this.lblUserGroup.InnerText = PubFunction.GroupIDToName(this._UserShopID);
			this.chkIsTel.Checked = PubFunction.curParameter.bolTel;
			this.chkTelNoMember.Checked = PubFunction.curParameter.bolTelNoMember;
			switch (PubFunction.curParameter.istry)
			{
			case 1:
				this.EditUser.Attributes.Add("onclick", "EditUser()");
				break;
			}
			this.GetModule();
		}

		public void SetCallerDisplay()
		{
		}

		public void GetModule()
		{
			DataTable dbModule = PubFunction.GetGroupAuthority(this._UserGroupID);
			DataRow[] dr = dbModule.Select("(ActionControl='module' OR ActionControl='page') and ActionValue=1 and ModuleVisible=1");
			DataTable db = dbModule.Clone();
			DataRow[] array = dr;
			for (int i = 0; i < array.Length; i++)
			{
				DataRow drt = array[i];
				db.Rows.Add(drt.ItemArray);
			}
			this.BindTreeMenu(db);
		}

		public void BindTreeMenu(DataTable db)
		{
			StringBuilder sbMain = new StringBuilder();
			StringBuilder sbChild = new StringBuilder();
			DataRow[] drs = db.Select("ModuleParentID=0  and ActionValue=1", "ModuleOrder desc");
			for (int i = 0; i < drs.Length; i++)
			{
				DataRow[] drs2 = db.Select(" ActionValue=1 and ModuleParentID=" + drs[i]["ModuleID"].ToString(), "ModuleOrder");
				sbChild.AppendFormat("<li id=\"menu{0}\" style=\"position:absolute;top:0;\">\n", i);
				for (int x = 0; x < drs2.Length; x++)
				{
					if (drs2[x]["ModuleID"].ToString() != "42")
					{
						sbChild.AppendFormat("<a href=\"{0}\" target=\"mainFrame\" >{1}</a>\n", drs2[x]["ModuleLink"].ToString(), drs2[x]["ModuleCaption"].ToString());
						if (x != drs2.Length - 1)
						{
							sbChild.Append("<b>|</b>");
						}
					}
				}
				sbChild.Append("</li>\n");
				if (i != 0)
				{
					sbMain.Append("<li class=\"s\"></li>\n");
				}
				if (i == drs.Length - 1)
				{
					sbMain.AppendFormat("<li class=\"m on\" target=\"menu{0}\">\n", i);
				}
				else
				{
					sbMain.AppendFormat("<li class=\"m\" target=\"menu{0}\">\n", i);
				}
				sbMain.AppendFormat(" <h3><a href=\"#\">{0}</a></h3>\n", drs[i]["ModuleCaption"].ToString());
				sbMain.Append("</li>\n");
			}
			this.MainMenuString = sbMain.ToString();
			this.ChildMenuString = sbChild.ToString();
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
			strScript.Append("                    top.location.href = 'login.aspx?type=loginout';\n");
			strScript.Append("                }\n");
			strScript.Append("                else {\n");
			strScript.Append("                    var state = NT120Client.NTLogin('1234567');\n");
			strScript.Append("                    if (state > 0) {\n");
			strScript.Append("                        alert('当前检测到您的加密锁登录失败,请插入正确的加密锁！');\n");
			strScript.Append("                        top.location.href = 'login.aspx?type=loginout';\n");
			strScript.Append("                    }\n");
			strScript.Append("                    else {\n");
			strScript.Append("                        doAjax(\"\",\"CheckHardwareID\", { \"Safety\": NT120Client.NTGetHardwareID() }, \"json\",\n");
			strScript.Append("                function (json) {\n");
			strScript.Append("                    if (json > 0) {\n");
			strScript.Append("                        alert('当前检测到您的加密锁认证失败,请插入正确的加密锁！');\n");
			strScript.Append("                        top.location.href = 'login.aspx?type=loginout';\n");
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
	}
}
