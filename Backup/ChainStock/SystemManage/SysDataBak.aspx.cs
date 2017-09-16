using Chain.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class SysDataBak : PageBase
	{
		private const int GB = 1073741824;

		private const int MB = 1048576;

		private const int KB = 1024;

		protected HtmlHead Head1;

		protected HtmlForm form1;

		protected Literal ltlTitle;

		protected HtmlInputButton btnAll;

		protected HtmlInputButton btnRestore;

		protected HtmlInputText txtBakName;

		protected HtmlInputButton btnBakUp;

		protected GridView gdvBakList;

		protected HiddenField hidCount;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.txtBakName.Value = new Chain.BLL.SysParameter().GetDataBaseName();
				this.GetBakList();
			}
		}

		public void GetBakList()
		{
			string basePath = base.Server.MapPath("../AppData/DataBase/");
			if (!Directory.Exists(basePath))
			{
				Directory.CreateDirectory(basePath);
			}
			DirectoryInfo dir = new DirectoryInfo(basePath);
			StringBuilder sbFile = new StringBuilder();
			FileInfo[] files = dir.GetFiles("*.bak");
			string AutobasePath = base.Server.MapPath("../AppData/AutoDataBase/");
			if (!Directory.Exists(AutobasePath))
			{
				Directory.CreateDirectory(AutobasePath);
			}
			DirectoryInfo Autodir = new DirectoryInfo(AutobasePath);
			FileInfo[] Autofiles = Autodir.GetFiles("*.bak");
			List<FileInfo> fileList = new List<FileInfo>();
			FileInfo[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				FileInfo item = array[i];
				fileList.Add(item);
			}
			array = Autofiles;
			for (int i = 0; i < array.Length; i++)
			{
				FileInfo item = array[i];
				fileList.Add(item);
			}
			fileList.Sort((FileInfo f1, FileInfo f2) => f2.LastWriteTime.CompareTo(f1.LastWriteTime));
			this.hidCount.Value = files.Length.ToString();
			this.gdvBakList.DataSource = fileList;
			this.gdvBakList.DataBind();
			PageBase.BindSerialGridView(this.gdvBakList, false, 0);
		}

		protected void gdvBakList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				FileInfo file = (FileInfo)e.Row.DataItem;
				string FileName = file.FullName;
				e.Row.Cells[1].Text = FileName.Substring(file.FullName.LastIndexOf("\\") + 1);
				e.Row.Cells[3].Text = this.ByteConversionGBMBKB(file.Length);
				if (file.Name.Contains("Aoto"))
				{
					LinkButton lbtn_RDB = (LinkButton)e.Row.FindControl("lbtn_RDB");
					lbtn_RDB.CommandArgument = string.Format("A|{0}", e.Row.Cells[1].Text);
					LinkButton lbtn_del = (LinkButton)e.Row.FindControl("lbtn_del");
					lbtn_del.CommandArgument = string.Format("A|{0}", e.Row.Cells[1].Text);
					HtmlAnchor dloadfile = (HtmlAnchor)e.Row.FindControl("dloadfile");
					dloadfile.HRef = "/AppData/AutoDataBase/" + e.Row.Cells[1].Text;
				}
				else
				{
					LinkButton lbtn_RDB = (LinkButton)e.Row.FindControl("lbtn_RDB");
					lbtn_RDB.CommandArgument = string.Format("S|{0}", e.Row.Cells[1].Text);
					LinkButton lbtn_del = (LinkButton)e.Row.FindControl("lbtn_del");
					lbtn_del.CommandArgument = string.Format("S|{0}", e.Row.Cells[1].Text);
					object s = e.Row.FindControl("dloadfile");
					HtmlAnchor dloadfile = (HtmlAnchor)e.Row.FindControl("dloadfile");
					dloadfile.HRef = "/AppData/DataBase/" + e.Row.Cells[1].Text;
				}
			}
		}

		protected void GV_CommandItem(object sender, GridViewCommandEventArgs e)
		{
			string[] info = e.CommandArgument.ToString().Split(new char[]
			{
				'|'
			});
			string text = e.CommandName.ToString();
			if (text != null)
			{
				if (text == "RETDB")
				{
					bool flag = this.RETB_SQL(info[0], info[1]);
					if (flag)
					{
						base.OutputWarn("还原成功！");
					}
					else
					{
						base.OutputWarn("还原失败！");
					}
					return;
				}
				if (text == "DELDB")
				{
					text = info[0];
					if (text != null)
					{
						string fileinfopath;
						if (!(text == "A"))
						{
							if (!(text == "S"))
							{
								goto IL_E3;
							}
							fileinfopath = base.Server.MapPath("../AppData/DataBase/");
						}
						else
						{
							fileinfopath = base.Server.MapPath("../AppData/AutoDataBase/");
						}
						if (File.Exists(fileinfopath + info[1]))
						{
							File.Delete(fileinfopath + info[1]);
							base.OutputWarn("删除成功！");
						}
						else
						{
							base.OutputWarn("文件不存在！");
						}
					}
					IL_E3:
					return;
				}
			}
			base.OutputWarn("您未提交命令！");
		}

		private bool RETB_SQL(string type, string file)
		{
			bool result;
			try
			{
				string fileinfopath = string.Empty;
				if (type != null)
				{
					if (!(type == "A"))
					{
						if (!(type == "S"))
						{
							goto IL_53;
						}
						fileinfopath = base.Server.MapPath("../AppData/DataBase/");
					}
					else
					{
						fileinfopath = base.Server.MapPath("../AppData/AutoDataBase/");
					}
					if (File.Exists(fileinfopath + file))
					{
						Chain.BLL.SysParameter par = new Chain.BLL.SysParameter();
						try
						{
							par.ReductionDataBakUp(fileinfopath + file);
						}
						catch (Exception e)
						{
							this.Log(e.ToString());
							result = false;
							return result;
						}
					}
					result = true;
					return result;
				}
				IL_53:
				result = false;
			}
			catch (Exception e)
			{
				PubFunction.SaveSysLog(-1, -1, "系统标志", e.ToString(), -1, DateTime.Now, PubFunction.ipAdress);
				result = false;
			}
			return result;
		}

		public void Log(string logText)
		{
			string fileName = string.Concat(new object[]
			{
				DateTime.Now.ToShortDateString().Replace("-", ""),
				"_",
				DateTime.Now.Hour,
				"_",
				DateTime.Now.Minute / 10
			});
			string fullPath = base.Server.MapPath("~/Upload/" + fileName + ".txt");
			logText = DateTime.Now.ToString() + "\r\n" + logText;
			File.AppendAllText(fullPath, logText + "\r\n\r\n");
		}

		public string ByteConversionGBMBKB(long KSize)
		{
			string result;
			if (KSize / 1073741824L >= 1L)
			{
				result = Math.Round((double)((float)KSize / 1.07374182E+09f), 2).ToString() + "GB";
			}
			else if (KSize / 1048576L >= 1L)
			{
				result = Math.Round((double)((float)KSize / 1048576f), 2).ToString() + "MB";
			}
			else if (KSize / 1024L >= 1L)
			{
				result = Math.Round((double)((float)KSize / 1024f), 2).ToString() + "KB";
			}
			else
			{
				result = KSize.ToString() + "Byte";
			}
			return result;
		}
	}
}
