using Chain.Common;
using Chain.DBUtility;
using System;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock
{
	public class Login : Page
	{
		protected Literal litObject;

		protected HtmlGenericControl loginBody;

		protected HtmlAnchor imgLogo;

		protected HtmlInputText txtAccount;

		protected HtmlInputHidden txtIsType;

		protected HtmlInputHidden txtISCheckKey;

		protected HtmlInputPassword txtPassword;

		protected HtmlInputButton btnSubmit;

		protected HtmlAnchor aSelf;

		protected HtmlGenericControl divFoot;

		protected HtmlGenericControl spCompName;

		protected HtmlGenericControl spBeiAn;

		protected HtmlAnchor aBeiAnName;

		protected HtmlGenericControl spEdition;

		protected HtmlGenericControl spDesc;

		protected HtmlGenericControl pHelpInfo;

		protected void Page_Load(object sender, EventArgs e)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			string title = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyTitleAttribute))).Title;
			string company = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute))).Company;
			string desc = ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyDescriptionAttribute))).Description;
			LoginLogic login = new LoginLogic();
			if (base.Request["type"] != null)
			{
				login.LoginOut();
			}
			login.CreateTestCookie();
			this.aSelf.HRef = "http://" + PubFunction.curParameter.SelfDoMain;
			this.spEdition.InnerText = string.Concat(new string[]
			{
				assembly.GetName().Name,
				" ",
				assembly.GetName().Version.ToString(),
				" ",
				title
			});
			this.spDesc.InnerText = desc;
			this.txtIsType.Value = PubFunction.curParameter.istry.ToString();
			this.txtISCheckKey.Value = (PubFunction.ISCheckKey ? "1" : "0");
			if (PubFunction.ISCheckKey)
			{
				HttpBrowserCapabilities browser = base.Request.Browser;
				string browserType = browser.Browser.ToString();
				if (browserType.ToUpper().Contains("IE") || browser.Type.Contains("Mozilla11") || browser.Type.Contains("InternetExplorer"))
				{
					PubFunction.IEbrowser = true;
					this.CreateKeyScript();
					this.loginBody.Attributes.Add("onload", "setTimeout('Check_NT120Setup()',1000)");
					this.btnSubmit.Attributes.Add("onclick", "javascript:LoginSystem();");
				}
				else
				{
					PubFunction.IEbrowser = false;
					base.Response.Redirect("index.aspx");
				}
			}
			else
			{
				this.btnSubmit.Attributes.Add("onclick", "javascript:Login_Submit();");
			}
			string domain = PubConstant.DoMain;
			if (base.Request.Url.ToString().IndexOf("localhost:") == -1)
			{
				if (domain == "" || base.Request.Url.ToString().IndexOf(domain) == -1)
				{
					base.Response.Redirect("Error.html");
				}
			}
			this.aBeiAnName.InnerText = PubFunction.curParameter.RegisterNumber;
			if (string.IsNullOrEmpty(PubFunction.curParameter.RegisterNumber))
			{
				this.spBeiAn.Visible = false;
			}
			base.Title = ConfigHelper.GetValue("SystemTitle");
			switch (PubFunction.curParameter.istry)
			{
			case 0:
			{
				HtmlMeta metaDescription = new HtmlMeta();
				metaDescription.Name = "description";
				metaDescription.Content = "★ 智络连锁店会员积分系统—由智络科技专门针对商家会员积分需求，进行独立设计开发一款会员积分系统，系统能轻松帮助商家对店里会员进行积分管理服务，系统通用性极强。可服务于连锁美容、美发、超市、便利店、汽车美容、母婴文具、休闲会员、培训机构等连锁商家。是连锁商家管理进行积分管理的必备系统。";
				base.Header.Controls.AddAt(4, metaDescription);
				break;
			}
			case 1:
				this.spCompName.Visible = false;
				this.pHelpInfo.Visible = false;
				break;
			case 2:
				this.spCompName.Visible = false;
				this.pHelpInfo.Visible = false;
				break;
			}
		}

		public void CreateKeyScript()
		{
			StringBuilder strScript = new StringBuilder();
			strScript.AppendLine("<object classid=\"clsid:EA3BA67D-8F11-4936-B01B-760B2E0208F6\" id=\"NT120Client\" name=\"NT120Client\"  style=\"left: 0px; top: 0px\" width=\"0\" height=\"0\"></object>");
			strScript.AppendLine("<Script language='JavaScript'>");
			strScript.AppendLine("function Check_NT120Setup() {");
			strScript.AppendLine("    try { var checkstate=1;");
			strScript.AppendLine("       var rtn = NT120Client.NTFind(); if (rtn != 0) {");
			strScript.AppendLine("            alert('系统检测到您的加密锁并未插入,或者加密锁已失效！');");
			strScript.AppendLine("            checkstate=0;");
			strScript.AppendLine("        } else {");
			strScript.AppendLine("            var state = NT120Client.NTLogin('1234567');");
			strScript.AppendLine("            if (state > 0) {");
			strScript.AppendLine("                alert('当前检测到您的加密锁登录失败,请插入正确的加密锁！');");
			strScript.AppendLine("                checkstate=0 ;");
			strScript.AppendLine("            }");
			strScript.AppendLine("            else {");
			strScript.AppendLine("              doAjax(");
			strScript.AppendLine("                  \"\",");
			strScript.AppendLine("                  \"CheckHardwareID\",");
			strScript.AppendLine("                  {\"Safety\":NT120Client.NTGetHardwareID()},");
			strScript.AppendLine("                  \"json\",");
			strScript.AppendLine("                  function (json) {");
			strScript.AppendLine("                    if (json > 0) {");
			strScript.AppendLine("                        alert('当前检测到您的加密锁认证失败,请插入正确的加密锁！');");
			strScript.AppendLine("                        checkstate=0");
			strScript.AppendLine("                    }");
			strScript.AppendLine("                });");
			strScript.AppendLine("            }");
			strScript.AppendLine("        }");
			strScript.AppendLine(" return checkstate;");
			strScript.AppendLine("    } catch (ef) {");
			strScript.AppendLine("        alert('未找到加密锁驱动程序，请先安装驱动！'); location.href ='AppDriver/NTInstall.aspx'; checkstate=0; return checkstate;");
			strScript.AppendLine("    }");
			strScript.AppendLine("}");
			strScript.AppendLine("function LoginSystem() {");
			strScript.AppendLine("   if(Check_NT120Setup()>0){Login_Submit();}");
			strScript.AppendLine("}");
			strScript.AppendLine("    </script>");
			this.litObject.Text = strScript.ToString();
		}
	}
}
