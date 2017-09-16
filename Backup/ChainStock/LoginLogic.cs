using Chain.BLL;
using Chain.Common.DEncrypt;
using Chain.Model;
using System;
using System.Web;

namespace ChainStock
{
	public class LoginLogic
	{
		private const string CodeName = "LoginState";

		private const string TestCookie = "TestCookie";

		private const int LoginStatusExpires = 6;

		private const int CookieExpires = 48;

		private bool _isLoggedOn = false;

		private Chain.Model.SysUser _loginUser;

		private DateTime _lastUpdateTime;

		public bool IsLoggedOn
		{
			get
			{
				return this._isLoggedOn;
			}
			private set
			{
				this._isLoggedOn = value;
			}
		}

		public Chain.Model.SysUser LoginUser
		{
			get
			{
				return this._loginUser;
			}
			private set
			{
				this._loginUser = value;
			}
		}

		public DateTime LastUpdateTime
		{
			get
			{
				return this._lastUpdateTime;
			}
			private set
			{
				this._lastUpdateTime = value;
			}
		}

		public static LoginLogic LoginStatus()
		{
			LoginLogic login = new LoginLogic();
			login.CheckLoginStatus();
			return login;
		}

		public bool Login(string Account, string Pwd)
		{
			Chain.BLL.SysUser bll = new Chain.BLL.SysUser();
			Chain.Model.SysUser model = bll.CheckUserLogin(Account, DESEncrypt.Encrypt(Pwd));
			bool result;
			if (model != null && !model.UserLock)
			{
				Chain.Model.SysShop shop = new Chain.BLL.SysShop().GetModel(model.UserShopID);
				if (shop != null && !shop.ShopState)
				{
					this.IsLoggedOn = true;
					this.LoginUser = model;
					this._lastUpdateTime = DateTime.Now;
					string code = this.Encode(model, this._lastUpdateTime);
					this.LoginIn(code);
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public bool LoginOut()
		{
			this.SetCookie("LoginState", string.Empty, -1);
			HttpContext.Current.Session["LoginState"] = null;
			return true;
		}

		public void CreateTestCookie()
		{
			this.SetCookie("TestCookie", string.Empty, 96);
		}

		private bool CheckTestCookie()
		{
			HttpRequest request = HttpContext.Current.Request;
			return request.Cookies["TestCookie"] != null;
		}

		private bool CheckLoginStatus()
		{
			string code = this.GetCookie("LoginState");
			if (string.IsNullOrEmpty(code) && HttpContext.Current.Session["LoginState"] != null)
			{
				code = HttpContext.Current.Session["LoginState"].ToString();
			}
			bool result;
			if (!string.IsNullOrEmpty(code))
			{
				Chain.Model.SysUser model = this.Decode(code, out this._lastUpdateTime);
				if (model != null && this._lastUpdateTime > DateTime.Now.AddHours(-6.0))
				{
					this.IsLoggedOn = true;
					this.LoginUser = model;
					if (this._lastUpdateTime.AddMinutes(30.0) < DateTime.Now)
					{
						this._lastUpdateTime = DateTime.Now;
						code = this.Encode(model, this._lastUpdateTime);
						this.LoginIn(code);
					}
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		private void LoginIn(string Code)
		{
			if (this.CheckTestCookie())
			{
				this.CreateTestCookie();
				this.SetCookie("LoginState", Code, 48);
				PubFunction.SaveSysLog(this.LoginUser.UserID, 4, "系统登录", string.Concat(new string[]
				{
					"用户登录系统，账号：[",
					this.LoginUser.UserAccount,
					"] 姓名：[",
					this.LoginUser.UserName,
					"]"
				}), this.LoginUser.UserShopID, DateTime.Now, PubFunction.ipAdress);
			}
			else
			{
				HttpContext.Current.Session["LoginState"] = Code;
				PubFunction.SaveSysLog(this.LoginUser.UserID, 4, "系统登录", string.Concat(new string[]
				{
					"用户登录系统，账号：[",
					this.LoginUser.UserAccount,
					"] 姓名：[",
					this.LoginUser.UserName,
					"]"
				}), this.LoginUser.UserShopID, DateTime.Now, PubFunction.ipAdress);
			}
		}

		private string Encode(Chain.Model.SysUser User, DateTime UpdateTime)
		{
			string code = string.Format("{0}|{1}|{2}|{3}|{4}|{5}", new object[]
			{
				User.UserID,
				User.UserAccount,
				User.UserName,
				User.UserGroupID,
				User.UserShopID,
				UpdateTime
			});
			return DESEncrypt.Encrypt(code);
		}

		private Chain.Model.SysUser Decode(string Code, out DateTime CheckTime)
		{
			Chain.Model.SysUser model = new Chain.Model.SysUser();
			CheckTime = DateTime.Now.AddDays(-1.0);
			Code = DESEncrypt.Decrypt(Code);
			string[] temp = Code.Split(new char[]
			{
				'|'
			});
			Chain.Model.SysUser result;
			if (temp.Length == 6)
			{
				model.UserID = Convert.ToInt32(temp[0]);
				model.UserAccount = temp[1];
				model.UserName = temp[2];
				model.UserGroupID = Convert.ToInt32(temp[3]);
				model.UserShopID = Convert.ToInt32(temp[4]);
				CheckTime = Convert.ToDateTime(temp[5]);
				result = model;
			}
			else
			{
				result = null;
			}
			return result;
		}

		private void SetCookie(string Name, string Value, int Expires)
		{
			HttpCookie cookie = new HttpCookie(Name, Value);
			cookie.Expires = DateTime.Now.AddHours((double)Expires);
			HttpContext.Current.Response.Cookies.Add(cookie);
		}

		private string GetCookie(string Name)
		{
			string result;
			if (HttpContext.Current.Request.Cookies[Name] != null)
			{
				result = HttpContext.Current.Request.Cookies[Name].Value;
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
