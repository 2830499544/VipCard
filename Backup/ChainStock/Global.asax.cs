using Chain.BLL;
using Chain.Common;
using Chain.Common.DEncrypt;
using Chain.Model;
using Chain.Tasks;
using System;
using System.Collections;
using System.Data;
using System.Web;

namespace ChainStock
{
	public class Global : HttpApplication
	{
		public Hashtable HardwareID = new Hashtable();

		protected void Application_Start(object sender, EventArgs e)
		{
			Hashtable Online = new Hashtable();
			base.Context.Cache.Insert("Online", Online);
			base.Context.Cache["count"] = 0;
			string SafetyVerification = ConfigHelper.GetValue("SafetyVerification");
			SafetyVerification = DEncrypt.EncryptDecrypt(SafetyVerification);
			SafetyVerification = DEncrypt.EncryptDecrypt(SafetyVerification);
			try
			{
				PubFunction.ISCheckKey = bool.Parse(SafetyVerification);
			}
			catch
			{
				PubFunction.ISCheckKey = true;
			}
			if (PubFunction.ISCheckKey)
			{
				string SafetyKey = INIFile.IniReadValue("SysConfig", "license");
				string[] Hardware = SafetyKey.Split(new char[]
				{
					'-'
				});
				for (int i = 0; i < Hardware.Length; i++)
				{
					this.HardwareID.Add(i, Hardware[i].ToString());
				}
				base.Application["HardwareID"] = this.HardwareID;
			}
			DataTable db = PubFunction.GetUserAuthority(1);
			for (int i = 0; i < db.Rows.Count; i++)
			{
				string f = db.Rows[i]["ActionControl"].ToString();
				string h = db.Rows[i]["ActionControl"].ToString();
				if (db.Rows[i]["ActionControl"].ToString() != "page" && db.Rows[i]["ActionControl"].ToString() != "module")
				{
					db.Rows[i]["ActionValue"] = "false";
				}
			}
			base.Application["Authority"] = db;
			TaskConfig.Init(base.Server.MapPath("~"));
			TaskManager.Instance.Initialize(TaskConfig.ScheduleTasks);
			TaskManager.Instance.Start();
		}

		protected void Session_Start(object sender, EventArgs e)
		{
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			Exception objErr = base.Server.GetLastError().GetBaseException();
			string err = string.Concat(new string[]
			{
				"会员系统 Caught in Application_Error event\r\nError in:",
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
				LoginLogic login = LoginLogic.LoginStatus();
				if (login.IsLoggedOn && login.LoginUser != null)
				{
					mdSysError.UserID = login.LoginUser.UserID;
					mdSysError.ShopID = login.LoginUser.UserShopID;
				}
				bllSysError.Add(mdSysError);
			}
			catch
			{
			}
		}

		protected void Session_End(object sender, EventArgs e)
		{
			this.LogoutCache();
			if (base.Context != null)
			{
				base.Context.Cache["count"] = (int)base.Context.Cache["count"] - 1;
			}
		}

		protected void Application_End(object sender, EventArgs e)
		{
		}

		public void LogoutCache()
		{
			Hashtable Online = (base.Context != null) ? ((Hashtable)base.Context.Cache["Online"]) : null;
			if (Online != null)
			{
				if (Online[base.Session.SessionID] != null)
				{
					Online.Remove(base.Session.SessionID);
				}
				base.Context.Cache["Online"] = Online;
			}
		}
	}
}
