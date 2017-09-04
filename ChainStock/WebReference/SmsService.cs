using ChainStock.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

namespace ChainStock.WebReference
{
	[GeneratedCode("System.Web.Services", "4.6.1055.0"), DesignerCategory("code"), DebuggerStepThrough, WebServiceBinding(Name = "SmsServiceSoap", Namespace = "http://tempuri.org/")]
	public class SmsService : SoapHttpClientProtocol
	{
		private SendOrPostCallback SendMessageOperationCompleted;

		private SendOrPostCallback GetBalanceOperationCompleted;

		private SendOrPostCallback RegisterExOperationCompleted;

		private SendOrPostCallback RegisterZsOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;

		public event SendMessageCompletedEventHandler SendMessageCompleted;

		public event GetBalanceCompletedEventHandler GetBalanceCompleted;

		public event RegisterExCompletedEventHandler RegisterExCompleted;

		public event RegisterZsCompletedEventHandler RegisterZsCompleted;

		public new string Url
		{
			get
			{
				return base.Url;
			}
			set
			{
				if (this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value))
				{
					base.UseDefaultCredentials = false;
				}
				base.Url = value;
			}
		}

		public new bool UseDefaultCredentials
		{
			get
			{
				return base.UseDefaultCredentials;
			}
			set
			{
				base.UseDefaultCredentials = value;
				this.useDefaultCredentialsSetExplicitly = true;
			}
		}

		public SmsService()
		{
			this.Url = Settings.Default.ChainStock_WebReference_SmsService;
			if (this.IsLocalFileSystemWebService(this.Url))
			{
				this.UseDefaultCredentials = true;
				this.useDefaultCredentialsSetExplicitly = false;
			}
			else
			{
				this.useDefaultCredentialsSetExplicitly = true;
			}
		}

        [SoapDocumentMethod("http://tempuri.org/SendMessage", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string SendMessage(int intSmsType, string strSmsSeries, string strSmsSerialPwd, string strSmsMobileList, string strSmsContent, string strSmsTime)
		{
            object[] results = base.Invoke("SendMessage", new object[]
			{
				intSmsType,
				strSmsSeries,
				strSmsSerialPwd,
				strSmsMobileList,
				strSmsContent,
				strSmsTime
			});
			return (string)results[0];
		}

		public void SendMessageAsync(int intSmsType, string strSmsSeries, string strSmsSerialPwd, string strSmsMobileList, string strSmsContent, string strSmsTime)
		{
			this.SendMessageAsync(intSmsType, strSmsSeries, strSmsSerialPwd, strSmsMobileList, strSmsContent, strSmsTime, null);
		}

		public void SendMessageAsync(int intSmsType, string strSmsSeries, string strSmsSerialPwd, string strSmsMobileList, string strSmsContent, string strSmsTime, object userState)
		{
			if (this.SendMessageOperationCompleted == null)
			{
				this.SendMessageOperationCompleted = new SendOrPostCallback(this.OnSendMessageOperationCompleted);
			}
			base.InvokeAsync("SendMessage", new object[]
			{
				intSmsType,
				strSmsSeries,
				strSmsSerialPwd,
				strSmsMobileList,
				strSmsContent,
				strSmsTime
			}, this.SendMessageOperationCompleted, userState);
		}

		private void OnSendMessageOperationCompleted(object arg)
		{
			if (this.SendMessageCompleted != null)
			{
				InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
				this.SendMessageCompleted(this, new SendMessageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/GetBalance", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string GetBalance(int intSmsType, string strSmsSeries, string strSmsSerialPwd)
		{
			object[] results = base.Invoke("GetBalance", new object[]
			{
				intSmsType,
				strSmsSeries,
				strSmsSerialPwd
			});
			return (string)results[0];
		}

		public void GetBalanceAsync(int intSmsType, string strSmsSeries, string strSmsSerialPwd)
		{
			this.GetBalanceAsync(intSmsType, strSmsSeries, strSmsSerialPwd, null);
		}

		public void GetBalanceAsync(int intSmsType, string strSmsSeries, string strSmsSerialPwd, object userState)
		{
			if (this.GetBalanceOperationCompleted == null)
			{
				this.GetBalanceOperationCompleted = new SendOrPostCallback(this.OnGetBalanceOperationCompleted);
			}
			base.InvokeAsync("GetBalance", new object[]
			{
				intSmsType,
				strSmsSeries,
				strSmsSerialPwd
			}, this.GetBalanceOperationCompleted, userState);
		}

		private void OnGetBalanceOperationCompleted(object arg)
		{
			if (this.GetBalanceCompleted != null)
			{
				InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
				this.GetBalanceCompleted(this, new GetBalanceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/RegisterEx", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string RegisterEx(string softwareSerialNo, string key, string serialpass)
		{
			object[] results = base.Invoke("RegisterEx", new object[]
			{
				softwareSerialNo,
				key,
				serialpass
			});
			return (string)results[0];
		}

		public void RegisterExAsync(string softwareSerialNo, string key, string serialpass)
		{
			this.RegisterExAsync(softwareSerialNo, key, serialpass, null);
		}

		public void RegisterExAsync(string softwareSerialNo, string key, string serialpass, object userState)
		{
			if (this.RegisterExOperationCompleted == null)
			{
				this.RegisterExOperationCompleted = new SendOrPostCallback(this.OnRegisterExOperationCompleted);
			}
			base.InvokeAsync("RegisterEx", new object[]
			{
				softwareSerialNo,
				key,
				serialpass
			}, this.RegisterExOperationCompleted, userState);
		}

		private void OnRegisterExOperationCompleted(object arg)
		{
			if (this.RegisterExCompleted != null)
			{
				InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
				this.RegisterExCompleted(this, new RegisterExCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
			}
		}

		[SoapDocumentMethod("http://tempuri.org/RegisterZs", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		public string RegisterZs(string sn, string pwd, string province, string city, string trade, string entname, string linkman, string phone, string mobile, string email, string fax, string address, string postcode, string sign)
		{
			object[] results = base.Invoke("RegisterZs", new object[]
			{
				sn,
				pwd,
				province,
				city,
				trade,
				entname,
				linkman,
				phone,
				mobile,
				email,
				fax,
				address,
				postcode,
				sign
			});
			return (string)results[0];
		}

		public void RegisterZsAsync(string sn, string pwd, string province, string city, string trade, string entname, string linkman, string phone, string mobile, string email, string fax, string address, string postcode, string sign)
		{
			this.RegisterZsAsync(sn, pwd, province, city, trade, entname, linkman, phone, mobile, email, fax, address, postcode, sign, null);
		}

		public void RegisterZsAsync(string sn, string pwd, string province, string city, string trade, string entname, string linkman, string phone, string mobile, string email, string fax, string address, string postcode, string sign, object userState)
		{
			if (this.RegisterZsOperationCompleted == null)
			{
				this.RegisterZsOperationCompleted = new SendOrPostCallback(this.OnRegisterZsOperationCompleted);
			}
			base.InvokeAsync("RegisterZs", new object[]
			{
				sn,
				pwd,
				province,
				city,
				trade,
				entname,
				linkman,
				phone,
				mobile,
				email,
				fax,
				address,
				postcode,
				sign
			}, this.RegisterZsOperationCompleted, userState);
		}

		private void OnRegisterZsOperationCompleted(object arg)
		{
			if (this.RegisterZsCompleted != null)
			{
				InvokeCompletedEventArgs invokeArgs = (InvokeCompletedEventArgs)arg;
				this.RegisterZsCompleted(this, new RegisterZsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
			}
		}

		public new void CancelAsync(object userState)
		{
			base.CancelAsync(userState);
		}

		private bool IsLocalFileSystemWebService(string url)
		{
			bool result;
			if (url == null || url == string.Empty)
			{
				result = false;
			}
			else
			{
				Uri wsUri = new Uri(url);
				result = (wsUri.Port >= 1024 && string.Compare(wsUri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0);
			}
			return result;
		}
	}
}
