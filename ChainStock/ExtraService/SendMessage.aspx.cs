using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.ExtraService
{
	public class SendMessage : PageBase
	{
		protected HtmlForm frmSendMessage;

		protected Literal ltlTitle;

		protected HtmlTextArea txtMemReceiver;

		protected HtmlSelect sltMemSmsTemplate;

		protected HtmlTextArea txtMemContent;

		protected HtmlTextArea txtCustomReceiver;

		protected FileUpload btnUploadMobile;

		protected Button btnImport;

		protected HtmlSelect sltCustomSmsTemplate;

		protected HtmlTextArea txtCustomContent;

		protected HtmlInputText txtQueryMem;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected CheckBox chkSms;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (!string.IsNullOrEmpty(base.Request["modiles"]))
				{
					this.txtMemReceiver.Value = base.Request.QueryString["modiles"];
				}
				else
				{
					this.txtMemReceiver.Attributes.Add("display", "none");
				}
				this.chkSms.Checked = this.curParameter.bolSms;
				PubFunction.BindSmsTemplate(this.sltCustomSmsTemplate);
				PubFunction.BindSmsTemplate(this.sltMemSmsTemplate);
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
			}
		}

		protected void btnImport_Click(object sender, EventArgs e)
		{
			string strMobileList = "";
			string strUploadPath = this.btnUploadMobile.FileName;
			string strSavePath = base.Server.MapPath("~/Upload/MobileNumber/Mobile.txt");
			if (strUploadPath == "")
			{
				base.OutputWarn("请先点击浏览按钮选择一个有手机号码的记事本文件，再导入号码。");
			}
			else
			{
				string strFileFix = strUploadPath.Substring(strUploadPath.LastIndexOf(".") + 1, strUploadPath.Length - strUploadPath.LastIndexOf(".") - 1).ToLower();
				if (strFileFix != "txt")
				{
					base.OutputWarn("选择文件的文件格式错误，请重新选择记事本文件。");
				}
				else
				{
					this.btnUploadMobile.PostedFile.SaveAs(strSavePath);
				}
			}
			if (File.Exists(strSavePath))
			{
				using (StreamReader myreader = File.OpenText(strSavePath))
				{
					string strContent = myreader.ReadToEnd();
					strContent = strContent.Replace("\r\n", ",");
					string[] strMobile = strContent.Split(new char[]
					{
						','
					});
					for (int i = 0; i < strMobile.Length; i++)
					{
						Match j = Regex.Match(strMobile[i], "^[1][3,4,5,8]\\d{9}");
						if (j.Success && strMobileList.IndexOf(j.Value) < 0)
						{
							strMobileList = strMobileList + j.Value + ",";
						}
					}
					this.txtCustomReceiver.Value = ((strMobileList.Length > 1) ? strMobileList.Remove(strMobileList.Length - 1) : "");
				}
			}
			else
			{
				base.OutputWarn("导入号码失败，请重新导入。");
			}
		}
	}
}
