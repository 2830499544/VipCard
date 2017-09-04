using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace ChainStock.Service
{
	public class xhEditorUpload : IHttpHandler
	{
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			LoginLogic status = LoginLogic.LoginStatus();
			if (status.IsLoggedOn && status.LoginUser != null)
			{
				context.Response.Charset = "UTF-8";
				string inputname = "filedata";
				string attachdir = "/upload/xhUpload";
				int dirtype = 2;
				int maxattachsize = 2097152;
				string upext = "txt,rar,zip,jpg,jpeg,gif,png,swf,wmv,avi,wma,mp3,mid";
				int msgtype = 2;
				string immediate = context.Request.QueryString["immediate"];
				string localname = "";
				string disposition = context.Request.ServerVariables["HTTP_CONTENT_DISPOSITION"];
				string err = "";
				string msg = "''";
				byte[] file;
				if (disposition != null)
				{
					file = context.Request.BinaryRead(context.Request.TotalBytes);
					localname = context.Server.UrlDecode(Regex.Match(disposition, "filename=\"(.+?)\"").Groups[1].Value);
				}
				else
				{
					HttpFileCollection filecollection = context.Request.Files;
					HttpPostedFile postedfile = filecollection.Get(inputname);
					localname = postedfile.FileName;
					file = new byte[postedfile.ContentLength];
					Stream stream = postedfile.InputStream;
					stream.Read(file, 0, postedfile.ContentLength);
					stream.Close();
				}
				if (file.Length == 0)
				{
					err = "无数据提交";
				}
				else if (file.Length > maxattachsize)
				{
					err = "文件大小超过" + maxattachsize + "字节";
				}
				else
				{
					string extension = this.GetFileExt(localname);
					if (("," + upext + ",").IndexOf("," + extension + ",") < 0)
					{
						err = "上传文件扩展名必需为：" + upext;
					}
					else
					{
						string attach_subdir;
						switch (dirtype)
						{
						case 2:
							attach_subdir = "month_" + DateTime.Now.ToString("yyMM");
							break;
						case 3:
							attach_subdir = "ext_" + extension;
							break;
						default:
							attach_subdir = "day_" + DateTime.Now.ToString("yyMMdd");
							break;
						}
						string attach_dir = attachdir + "/" + attach_subdir + "/";
						Random random = new Random(DateTime.Now.Millisecond);
						string filename = string.Concat(new object[]
						{
							DateTime.Now.ToString("yyyyMMddhhmmss"),
							random.Next(10000),
							".",
							extension
						});
						string target = attach_dir + filename;
						try
						{
							this.CreateFolder(context.Server.MapPath(attach_dir));
							FileStream fs = new FileStream(context.Server.MapPath(target), FileMode.Create, FileAccess.Write);
							fs.Write(file, 0, file.Length);
							fs.Flush();
							fs.Close();
						}
						catch (Exception ex)
						{
							err = ex.Message.ToString();
						}
						if (immediate == "1")
						{
							target = "!" + target;
						}
						target = this.jsonString(target);
						if (msgtype == 1)
						{
							msg = "'" + target + "'";
						}
						else
						{
							msg = string.Concat(new string[]
							{
								"{'url':'",
								target,
								"','localname':'",
								this.jsonString(localname),
								"','id':'1'}"
							});
						}
					}
				}
				context.Response.Write(string.Concat(new string[]
				{
					"{'err':'",
					this.jsonString(err),
					"','msg':",
					msg,
					"}"
				}));
			}
		}

		private string jsonString(string str)
		{
			str = str.Replace("\\", "\\\\");
			str = str.Replace("/", "\\/");
			str = str.Replace("'", "\\'");
			return str;
		}

		private string GetFileExt(string FullPath)
		{
			string result;
			if (FullPath != "")
			{
				result = FullPath.Substring(FullPath.LastIndexOf('.') + 1).ToLower();
			}
			else
			{
				result = "";
			}
			return result;
		}

		private void CreateFolder(string FolderPath)
		{
			if (!Directory.Exists(FolderPath))
			{
				Directory.CreateDirectory(FolderPath);
			}
		}
	}
}
