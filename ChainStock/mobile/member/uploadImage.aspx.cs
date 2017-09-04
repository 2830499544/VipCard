using Chain.BLL;
using Chain.Model;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.member
{
	public class uploadImage : Page
	{
		protected HtmlForm Form1;

		protected HtmlImage imgShow;

		protected HtmlInputHidden txtMemPhoto;

		protected FileUpload UploadMemPhoto;

		protected Button btn_Upload;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModel(MemID);
					if (modelMem.MemPhoto != null && modelMem.MemPhoto != "")
					{
						this.imgShow.Src = modelMem.MemPhoto.ToString();
					}
					else
					{
						this.imgShow.Src = "images/headimg.jpg";
					}
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		protected void btn_Upload_Click(object sender, EventArgs e)
		{
			if (this.UploadMemPhoto.HasFile)
			{
				string upPath = "../../Upload/MemPhoto/";
				int upLength = 5;
				string upFileType = "|image/bmp|image/x-png|image/pjpeg|image/gif|image/png|image/jpeg|";
				string fileContentType = this.UploadMemPhoto.PostedFile.ContentType;
				if (upFileType.IndexOf(fileContentType.ToLower()) > 0)
				{
					string name = this.UploadMemPhoto.PostedFile.FileName;
					FileInfo file = new FileInfo(name);
					string fileName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + file.Extension;
					string webFilePath = base.Server.MapPath(upPath) + fileName;
					string FilePath = upPath + fileName;
					if (!File.Exists(webFilePath))
					{
						if (this.UploadMemPhoto.FileBytes.Length / 1048576 > upLength)
						{
							base.ClientScript.RegisterStartupScript(base.GetType(), "upfileOK", "alert('大小超出 " + upLength + " M的限制，请处理后再上传！');", true);
						}
						else
						{
							try
							{
								this.UploadMemPhoto.SaveAs(webFilePath);
								this.imgShow.Src = FilePath;
								this.txtMemPhoto.Value = FilePath;
							}
							catch (Exception ex)
							{
								base.ClientScript.RegisterStartupScript(base.GetType(), "upfileOK", "alert('提示：文件上传失败" + ex.Message + "');", true);
							}
						}
					}
					else
					{
						base.ClientScript.RegisterStartupScript(base.GetType(), "upfileOK", "alert('提示：文件已经存在，请重命名后上传');", true);
					}
				}
				else
				{
					base.ClientScript.RegisterStartupScript(base.GetType(), "upfileOK", "alert('提示：文件类型不符" + fileContentType + "');", true);
				}
			}
		}
	}
}
