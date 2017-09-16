using Chain.BLL;
using Chain.Common;
using Chain.Model;
using ChainStock;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public abstract class PageBase : Page
{
	public int _UserID;

	public string _UserName;

	public int _UserGroupID;

	public string _UserAccount;

	public int _UserShopID;

	public CurrentParameter curParameter = PubFunction.curParameter;

	public DataTable _UserAuthority;

	public List<ListItem> _ListControl = new List<ListItem>();

	public int _PID;

	private static int ProductDecimalNum
	{
		get
		{
			return PubFunction.ProductDecimalNum;
		}
	}

	protected string PageNameAll
	{
		get
		{
			string result;
			if (base.ClientQueryString != "")
			{
				result = base.Request.CurrentExecutionFilePath + "?" + base.ClientQueryString;
			}
			else
			{
				result = base.Request.CurrentExecutionFilePath;
			}
			return result;
		}
	}

	protected string PageName
	{
		get
		{
			string pn = base.Request.CurrentExecutionFilePath;
			return pn.Substring(pn.LastIndexOf('/') + 1);
		}
	}

	public static string BaseUrl
	{
		get
		{
			string strBaseUrl = "";
			strBaseUrl = strBaseUrl + "http://" + HttpContext.Current.Request.Url.Host;
			if (HttpContext.Current.Request.Url.Port.ToString() != "80")
			{
				strBaseUrl = strBaseUrl + ":" + HttpContext.Current.Request.Url.Port;
			}
			strBaseUrl += HttpContext.Current.Request.ApplicationPath;
			return strBaseUrl + "/";
		}
	}

	protected string DecimalScripttxt
	{
		get
		{
			string min = "1";
			if (Math.Abs(PageBase.ProductDecimalNum) > 1)
			{
				min = "0.";
				for (int i = 1; i < PageBase.ProductDecimalNum; i++)
				{
					min += "0";
				}
				min += "1";
			}
			StringBuilder js_html = new StringBuilder();
			js_html.Append("<script type='text/javascript'>");
			js_html.Append("var _sys_check={reg:function(a){");
			js_html.Append("var b=Math.abs(a);switch(b){case 0:partten =\"^\\\\d+$\"; break;");
			js_html.Append("default: partten = \"^\\\\d+(?:\\\\.\\\\d{1,\"+b+\"})?$\"; break;}return new RegExp(partten);},defval:" + min + "};");
			js_html.Append("var sys_num_checked={product_num_check:function(a){var reg = _sys_check.reg(" + PageBase.ProductDecimalNum.ToString() + ");return reg.test(a);},");
			js_html.Append("product_num:function(a){if(Math.abs(" + PageBase.ProductDecimalNum.ToString() + ")>0){var b=a.replace(/\\s/g,\"\");if((/^\\d+\\.$/).test(b))return b+\"0\";else return a;}else return a;}");
			js_html.Append("};</script>");
			return js_html.ToString();
		}
	}

	public string MoneyFormatString(object value)
	{
		return SysNumerical.GetMoneyFormatString(value);
	}

	public string NumberFromatString(object value)
	{
		return SysNumerical.GetNumberFormatString(value);
	}

	public string getMoneyFormat(object value)
	{
		return string.Format("{0:F2}", value);
	}

	public string getPointNum(object value)
	{
		return string.Format("{0:F0}", value);
	}

	public string getProductNum(object value)
	{
		return string.Format("{0:F0}", value);
	}

	public string GetValueString(object value, string format)
	{
		return string.Format(format, value);
	}

	protected override void OnLoad(EventArgs e)
	{
		this.BaseLoad();
		base.OnLoad(e);
	}

	public void Page_Error(object sender, EventArgs e)
	{
		Exception objErr = base.Server.GetLastError().GetBaseException();
		string err = string.Concat(new string[]
		{
			"Error Caught in PageBase_Error event\r\nError in:",
			base.Request.Url.ToString(),
			"\r\nError Message:",
			objErr.Message.ToString(),
			"\r\nStack Trace:",
			objErr.StackTrace.ToString()
		});
		try
		{
			Chain.Model.SysError mdSysError = new Chain.Model.SysError();
			Chain.BLL.SysError bllSysError = new Chain.BLL.SysError();
			mdSysError.ErrorTime = DateTime.Now;
			mdSysError.ErrorContent = err;
			mdSysError.Ipaddress = PubFunction.ipAdress;
			mdSysError.UserID = this._UserID;
			mdSysError.ShopID = this._UserShopID;
			bllSysError.Add(mdSysError);
		}
		catch
		{
		}
		base.Server.ClearError();
	}

	protected void BaseLoad()
	{
		if (PubFunction.ISCheckKey && !PubFunction.IEbrowser)
		{
			base.Response.Write("<script>top.location.href='../index.aspx'</script>");
			base.Response.End();
		}
		LoginLogic login = LoginLogic.LoginStatus();
		if (login.IsLoggedOn && login.LoginUser != null)
		{
			this._UserID = login.LoginUser.UserID;
			this._UserName = login.LoginUser.UserName;
			this._UserGroupID = login.LoginUser.UserGroupID;
			this._UserShopID = login.LoginUser.UserShopID;
			this._UserAccount = login.LoginUser.UserAccount;
			this._UserAuthority = PubFunction.GetGroupAuthority(this._UserGroupID);
			string Url = HttpContext.Current.Request.Url.AbsolutePath.ToLower();
			if (Url.IndexOf("/") == 0)
			{
				Url = Url.Remove(0, 1);
			}
			this._PID = new Chain.BLL.SysGroupAuthority().getModuleID(Url);
			this._PID = ((this._PID == 0) ? 1 : this._PID);
			string code = this._PID.ToString();
			if (!string.IsNullOrEmpty(code))
			{
				this.Get_PagePermission(this._UserAuthority);
				Literal cl = (Literal)this.FindControl("ltlTitle");
				if (cl != null)
				{
					PubFunction.SetPageNavigation(this._PID, ref cl);
				}
			}
			else
			{
				base.Response.End();
			}
		}
		else
		{
			int TimeOut = 0;
			DateTime arg_195_0 = login.LastUpdateTime;
			if (login.LastUpdateTime.Year >= DateTime.Now.Year - 1)
			{
				TimeOut = Convert.ToInt32((DateTime.Now - login.LastUpdateTime).TotalMinutes);
			}
			base.Response.Write("<script>top.location.href='../Common/Error_TimeOut.aspx?TimeOut=" + TimeOut + "'</script>");
			base.Response.End();
		}
		this.DecimalScript();
		this.SetPrint();
		base.Title = (base.Title += ConfigHelper.GetValue("SystemTitle"));
	}

	public void SetPrint()
	{
		string content = "";
		HtmlGenericControl scriptControl = new HtmlGenericControl("script");
		scriptControl.Attributes.Add("type", "text/javascript");
		HtmlGenericControl scprint = new HtmlGenericControl("script");
		scprint.Attributes.Add("type", "text/javascript");
		scprint.Attributes.Add("src=", "../Scripts/LodopFuncs.js");
		int PointType = this.curParameter.PrintPaperType;
		if (PointType == 0)
		{
			scriptControl.Attributes.Add("Src", "../Scripts/Module/Report/PrintStylus.js");
		}
		if (PointType == 1)
		{
			scriptControl.Attributes.Add("Src", "../Scripts/Module/Report/Print_SanLian.js");
		}
		if (PointType == 2)
		{
			scriptControl.Attributes.Add("Src", "../Scripts/Module/Report/Print.js");
		}
		if (PointType == 3)
		{
			scriptControl.Attributes.Add("Src", "../Scripts/Module/Report/Prints80.js");
		}
		scriptControl.InnerHtml = content;
		base.Header.Controls.Add(scriptControl);
	}

	public void Get_PagePermission(DataTable db)
	{
		DataRow[] drs = db.Select("ModuleID=" + this._PID);
		DataRow[] array = drs;
		for (int i = 0; i < array.Length; i++)
		{
			DataRow drt = array[i];
			if (drt["ActionControl"].ToString() == "page")
			{
				string str = drt["ActionValue"].ToString();
				if (!Convert.ToBoolean(drt["ActionValue"].ToString()))
				{
					base.Response.Write("<script>top.location.href='../Common/Error_TimeOut.aspx'</script>");
					base.Response.End();
				}
			}
			else
			{
				string ControlName = drt["ActionControl"].ToString();
				if (ControlName.IndexOf("_") > 0)
				{
					string[] ListControl = ControlName.Split(new char[]
					{
						'_'
					});
					Control DataListControl = this.FindControl(ListControl[0]);
					if (DataListControl != null)
					{
						string permission_Value = drt["ActionValue"].ToString();
						this._ListControl.Add(new ListItem(ListControl[1].ToString(), permission_Value));
						string text = DataListControl.GetType().ToString();
						if (text != null)
						{
							if (!(text == "System.Web.UI.WebControls.GridView"))
							{
								if (text == "System.Web.UI.WebControls.Repeater")
								{
									Repeater rptDataList = (Repeater)DataListControl;
									rptDataList.ItemDataBound += new RepeaterItemEventHandler(this.rptDataList_ItemDataBound);
								}
							}
							else
							{
								GridView gdDataList = (GridView)DataListControl;
								gdDataList.DataBound += new EventHandler(this.gdDataList_DataBound);
							}
						}
					}
				}
				else
				{
					Control cl = this.FindControl(drt["ActionControl"].ToString());
					if (cl != null)
					{
						cl.Visible = Convert.ToBoolean(drt["ActionValue"].ToString());
					}
				}
			}
		}
	}

	public void rptDataList_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		foreach (ListItem item in this._ListControl)
		{
			Control GridControl = e.Item.FindControl(item.Text);
			if (GridControl != null)
			{
				GridControl.Visible = Convert.ToBoolean(item.Value);
			}
		}
	}

	public void gdDataList_DataBound(object sender, EventArgs e)
	{
		GridView gdList = (GridView)sender;
		for (int i = 0; i < gdList.Rows.Count; i++)
		{
			foreach (ListItem item in this._ListControl)
			{
				Control GridControl = gdList.Rows[i].FindControl(item.Text);
				if (GridControl != null)
				{
					GridControl.Visible = Convert.ToBoolean(item.Value);
				}
			}
		}
	}

	public static void BindNullSGridView(GridView sgv)
	{
		if (sgv != null)
		{
			if (sgv.Rows.Count == 0)
			{
				sgv.AllowSorting = false;
				DataTable dt = new DataTable();
				DataTable dt2 = (DataTable)sgv.DataSource;
				DataRow dr = dt.NewRow();
				dt.Rows.Add(dr);
				sgv.DataSource = dt;
				try
				{
					sgv.DataBind();
				}
				catch
				{
				}
				if (sgv.Rows.Count > 0)
				{
					sgv.Rows[0].Visible = false;
				}
			}
		}
	}

	public static void BindSerialGridView(GridView sgv, bool CheckCell, int StartIndex)
	{
		if (sgv != null)
		{
			if (StartIndex > -1)
			{
				int SerialCell = 0;
				if (CheckCell)
				{
					SerialCell = 1;
				}
				foreach (GridViewRow gvr in sgv.Rows)
				{
					gvr.Cells[SerialCell].Text = (StartIndex + gvr.RowIndex + 1).ToString();
					gvr.Attributes.Add("onmouseover", "temp=this.style.backgroundColor;this.style.backgroundColor='#E1E1FF';this.ForeColor='white'");
					gvr.Attributes.Add("onmouseout", "this.style.backgroundColor=temp");
				}
			}
		}
	}

	public static void BindSerialRepeater(Repeater rpt, int StartIndex)
	{
		if (rpt != null)
		{
			if (StartIndex > -1)
			{
				foreach (RepeaterItem rpi in rpt.Items)
				{
					Label lblNum = (Label)rpi.FindControl("lblNumber");
					lblNum.Text = (StartIndex + rpi.ItemIndex + 1).ToString();
				}
			}
		}
	}

	public string GetWebRootPath()
	{
		string root = "/";
		if ("/" != base.Request.ApplicationPath)
		{
			root = base.Request.Url.AbsoluteUri.Remove(base.Request.Url.AbsoluteUri.LastIndexOf("/")).Replace(base.Request.Url.AbsolutePath.Remove(base.Request.Url.AbsolutePath.LastIndexOf("/")), base.Request.ApplicationPath);
		}
		return root;
	}

	public string GetWebRoot()
	{
		string result;
		if (base.Request.ApplicationPath != "/")
		{
			result = base.Request.ApplicationPath;
		}
		else
		{
			result = "";
		}
		return result;
	}

	public string GetBaseUrl()
	{
		string strApp = this.Context.Request.ApplicationPath;
		if (strApp == "/")
		{
			strApp = "";
		}
		return this.Context.Request.Url.Scheme.ToString() + "://" + this.Context.Request.Url.Authority.ToString() + strApp;
	}

	public static string GetSession(string key)
	{
		return HttpContext.Current.Session[key].ToString();
	}

	public static void SetSession(string key, string str)
	{
		HttpContext.Current.Session.Add(key, str);
	}

	public void OutputWarn(string strWarn)
	{
		this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<Script Language='JavaScript' defer>art.dialog({title: '系统提示',time: 2,content:'" + strWarn + "',close: function () { location.href = location.href; }});</script>");
	}

	public void MessagePageShowError(string strMessage)
	{
		this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<Script Language='JavaScript' defer>art.dialog({title: '系统提示',time: 2,content:'" + strMessage + "',close: function () {  }});</script>");
	}

	public void DecimalScript()
	{
		try
		{
			this.Page.Header.Controls.Add(new LiteralControl(this.DecimalScripttxt));
		}
		catch
		{
		}
	}
}
