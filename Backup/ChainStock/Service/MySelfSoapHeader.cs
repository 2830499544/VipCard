using Chain.Common;
using Chain.Common.DEncrypt;
using System;
using System.Web.Services.Protocols;

namespace ChainStock.Service
{
	public class MySelfSoapHeader : SoapHeader
	{
		private bool _serializeRequired;

		private string name;

		private string pwd;

		private static bool IsCanAnonymityWs;

		private static string WebServiceName;

		private static string WebServicePwd;

		public bool SerializeRequired
		{
			get
			{
				return this._serializeRequired;
			}
			set
			{
				this._serializeRequired = value;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public string Pwd
		{
			get
			{
				return this.pwd;
			}
			set
			{
				this.pwd = value;
			}
		}

		static MySelfSoapHeader()
		{
			MySelfSoapHeader.IsCanAnonymityWs = Convert.ToBoolean(DESEncrypt.Decrypt(ConfigHelper.GetValue("IsCanAnonymityWs")));
			MySelfSoapHeader.WebServiceName = ConfigHelper.GetValue("WebServiceName");
			MySelfSoapHeader.WebServicePwd = ConfigHelper.GetValue("WebServicePwd");
		}

		public bool CheckUser()
		{
			return MySelfSoapHeader.IsCanAnonymityWs || (!(this.Name != MySelfSoapHeader.WebServiceName) && !(this.Pwd != MySelfSoapHeader.WebServicePwd));
		}
	}
}
