using Chain.Wechat;
using System;
using System.IO;
using System.Text;
using System.Web;
using Tencent;

namespace ChainStock.Service
{
	public class WeiXinHandler : IHttpHandler
	{
		private HttpRequest Request;

		private HttpResponse Response;

		private HttpServerUtility Server;

		private HttpContext Context;

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			try
			{
				DateTime start = DateTime.Now;
				context.Response.Buffer = true;
				context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0);
				context.Response.AddHeader("pragma", "no-cache");
				context.Response.AddHeader("cache-control", "");
				context.Response.CacheControl = "no-cache";
				context.Response.ContentType = "text/plain";
				this.Request = context.Request;
				this.Response = context.Response;
				this.Server = context.Server;
				this.Context = context;
				this.Log(this.GetAllParameter());
				string signature = this.Request.QueryString["signature"];
				string timestamp = this.Request.QueryString["timestamp"];
				string nonce = this.Request.QueryString["nonce"];
				string encrypt_type = "";
				string msg_signature = "";
				if (this.Request.QueryString["encrypt_type"] != null)
				{
					encrypt_type = this.Request.QueryString["encrypt_type"];
				}
				if (this.Request.QueryString["msg_signature"] != null)
				{
					msg_signature = this.Request.QueryString["msg_signature"];
				}
				string token = PubFunction.curParameter.strWeiXinToken;
				string encodingAESKey = PubFunction.curParameter.strWeiXinEncodingAESKey;
				string appid = PubFunction.curParameter.strWeiXinAppID;
				string appSecret = PubFunction.curParameter.strWeiXinAppSecret;
				if (this.Request.HttpMethod.ToLower() == "get")
				{
					string echostr = this.Request.QueryString["echostr"].ToString();
					int temp = WXBizMsgCrypt.VerifySignature(token, timestamp, nonce, signature);
					if (temp == 0)
					{
						this.Response.Write(echostr);
					}
					else
					{
						this.Log(string.Concat(new object[]
						{
							"验证消息真实性错误；代码：",
							temp,
							"，信息：",
							Enum.GetName(typeof(WXBizMsgCrypt.WXBizMsgCryptErrorCode), temp)
						}));
						this.Response.Write("");
					}
				}
				else
				{
					Stream s = HttpContext.Current.Request.InputStream;
					byte[] b = new byte[s.Length];
					s.Read(b, 0, (int)s.Length);
					string postData = Encoding.UTF8.GetString(b);
					this.Log("接收消息：" + postData);
					if (PubFunction.curParameter.strDoMain.IndexOf("localhost") >= 0 || PubFunction.curParameter.strDoMain.IndexOf("192.168.0.") >= 0)
					{
						token = "QDG6eK";
						encodingAESKey = "jWmYm7qr5nMoAUwZRjGtBxmz3KA1tkAj3ykkR6q2B2C";
						appid = "wx5823bf96d3bd56c7";
					}
					WXBizMsgCrypt wxbmc = new WXBizMsgCrypt(token, encodingAESKey, appid);
					string requestData = "";
					if (encrypt_type == "aes")
					{
						int ret = wxbmc.DecryptMsg(msg_signature, timestamp, nonce, postData, ref requestData);
						if (ret != 0)
						{
							this.Log(string.Concat(new object[]
							{
								"解密微信消息错误；代码：",
								ret,
								"，信息：",
								Enum.GetName(typeof(WXBizMsgCrypt.WXBizMsgCryptErrorCode), ret)
							}));
							this.Response.Write("");
							return;
						}
					}
					else
					{
						requestData = postData;
					}
					this.Log("解密消息：" + requestData);
					IRequest wxRequest = ReqeustManager.GetRequest(requestData);
					string responseData = wxRequest.Response();
					this.Log("回复消息：" + responseData);
					string encryptResponseData = "";
					if (encrypt_type == "aes")
					{
						int ret = wxbmc.EncryptMsg(responseData, timestamp, nonce, ref encryptResponseData);
						if (ret != 0)
						{
							this.Log(string.Concat(new object[]
							{
								"加密微信消息错误；代码：",
								ret,
								"，信息：",
								Enum.GetName(typeof(WXBizMsgCrypt.WXBizMsgCryptErrorCode), ret)
							}));
							this.Response.Write("");
							return;
						}
					}
					else
					{
						encryptResponseData = responseData;
					}
					string time = (DateTime.Now - start).TotalMilliseconds + "毫秒";
					this.Log("加密消息：" + encryptResponseData + "\r\n执行时间：" + time);
					this.Response.Write(encryptResponseData);
				}
			}
			catch (Exception ex)
			{
				this.Log(ex.ToString());
			}
		}

		public void Log(string logText)
		{
			try
			{
				string fileName = string.Concat(new object[]
				{
					DateTime.Now.ToShortDateString().Replace("-", ""),
					"_",
					DateTime.Now.Hour,
					"_",
					DateTime.Now.Minute / 10
				});
				string fullPath = this.Server.MapPath("~/Upload/Log/" + fileName + ".txt");
				logText = DateTime.Now.ToString() + "\r\n" + logText;
				File.AppendAllText(fullPath, logText + "\r\n\r\n");
			}
			catch (Exception ex)
			{
				PubFunction.SaveSysLog(1, 4, "微信接口错误", ex.ToString() + "  " + ex.StackTrace.ToString(), 1, DateTime.Now, PubFunction.ipAdress);
			}
		}

		private string GetAllParameter()
		{
			string Para = string.Empty;
			Para += "{QueryString}";
			for (int i = 0; i < this.Request.QueryString.AllKeys.Length; i++)
			{
				string text = Para;
				Para = string.Concat(new string[]
				{
					text,
					this.Request.QueryString.Keys[i],
					":",
					this.Request.QueryString[this.Request.QueryString.Keys[i]].ToString(),
					";"
				});
			}
			Para += "{Form}";
			for (int i = 0; i < this.Request.Form.AllKeys.Length; i++)
			{
				string text = Para;
				Para = string.Concat(new string[]
				{
					text,
					this.Request.Form.Keys[i],
					":",
					this.Request.Form[this.Request.Form.Keys[i]].ToString(),
					";"
				});
			}
			return Para;
		}
	}
}
