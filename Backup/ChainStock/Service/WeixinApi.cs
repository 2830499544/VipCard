using Chain.BLL;
using Chain.Common.DEncrypt;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Web.Common;

namespace ChainStock.Service
{
	public class WeixinApi : IHttpHandler
	{
		private HttpRequest Request;

		private HttpResponse Response;

		private HttpServerUtility Server;

		private HttpContext Context;

		protected string token = string.Empty;

		private string postStr = string.Empty;

		private string sendXML = string.Empty;

		private XmlDocument xmlDoc = new XmlDocument();

		private string FromUserName = string.Empty;

		private string MsgType = string.Empty;

		private string Content = string.Empty;

		private Chain.BLL.WeiXinLog weiXinLogBLL = new Chain.BLL.WeiXinLog();

		private Chain.Model.WeiXinLog weiXinLogModel = new Chain.Model.WeiXinLog();

		private Chain.BLL.Mem memBll = new Chain.BLL.Mem();

		private Chain.Model.Mem memModel = new Chain.Model.Mem();

		private Chain.BLL.WeiXinRule weiXinRuleBLL = new Chain.BLL.WeiXinRule();

		private Chain.Model.WeiXinRule ruleModel = new Chain.Model.WeiXinRule();

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public void ProcessRequest(HttpContext context)
		{
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
			try
			{
				Stream s = HttpContext.Current.Request.InputStream;
				byte[] b = new byte[s.Length];
				s.Read(b, 0, (int)s.Length);
				this.postStr = Encoding.UTF8.GetString(b);
				this.token = PubFunction.curParameter.strWeiXinToken;
				this.Log(this.postStr);
				if (this.Request.HttpMethod.ToLower() == "get")
				{
					if (this.Request.QueryString["signature"] != null && !string.IsNullOrEmpty(this.Request.QueryString["signature"].ToString()))
					{
						this.UrlValid();
					}
				}
				else
				{
					this.SendMsg();
				}
			}
			catch (Exception ex)
			{
				this.Log(ex.ToString());
			}
		}

		public void SendMsg()
		{
			this.xmlDoc.LoadXml(this.postStr);
			this.MsgType = this.xmlDoc.GetElementsByTagName("MsgType")[0].InnerText;
			this.FromUserName = this.xmlDoc.GetElementsByTagName("FromUserName")[0].InnerText;
			try
			{
				this.weiXinLogModel = this.weiXinLogBLL.GetModel(this.FromUserName);
				if (this.weiXinLogModel == null)
				{
					this.weiXinLogModel = new Chain.Model.WeiXinLog();
					this.NoLastOperating();
				}
				else
				{
					this.YesLastOperating(this.postStr);
				}
				this.sendXML = ((this.sendXML == "") ? ResponseSendStr.Text(this.postStr, this.weiXinRuleBLL.ErrorStr()) : this.sendXML);
			}
			catch (Exception e)
			{
				this.Log(e.ToString());
			}
			finally
			{
				this.Log(this.sendXML);
			}
			this.Response.Write(this.sendXML);
		}

		private void YesLastOperating(string postStr)
		{
			if (this.MsgType == "event")
			{
				this.AttentionNews(postStr);
			}
			else
			{
				this.weiXinLogModel = this.weiXinLogBLL.GetModel(this.FromUserName);
				string statusCode = this.weiXinLogModel.StatusCode;
				if (statusCode != null)
				{
					if (!(statusCode == "0"))
					{
						if (!(statusCode == "1"))
						{
							if (!(statusCode == "2"))
							{
								if (statusCode == "3")
								{
									this.Do3ForMemberTransformByReplyTelNumberOrMemberCard();
								}
							}
							else
							{
								this.Do2MemberTransform();
							}
						}
						else
						{
							this.Do1ForApplicationMemberByReplyTelNumber();
						}
					}
					else
					{
						this.Do0ForApplicationMember();
					}
				}
			}
		}

		private void NoLastOperating()
		{
			if (this.MsgType == "event")
			{
				this.AttentionNews(this.postStr);
			}
			else if (this.MsgType == "text")
			{
				this.TextNews();
			}
		}

		public void DrawImage(Chain.Model.Mem mem, string FromUserName)
		{
			Bitmap smallWeiXin = new Bitmap(130, 130);
			Image weixinImg = QRCodeImage.CreateQRCode(mem.MemCard);
			Graphics g = Graphics.FromImage(smallWeiXin);
			g.DrawImage(weixinImg, new Point(-35, -35));
			string savePath = this.Server.MapPath("~/Upload/WeiXin/Images/" + FromUserName + ".jpg");
			Bitmap bigWeiXin = new Bitmap(200, 200);
			g = Graphics.FromImage(bigWeiXin);
			g.DrawImage(smallWeiXin, new Rectangle(0, 0, 200, 200), new Rectangle(0, 0, 130, 130), GraphicsUnit.Pixel);
			string bg = this.Server.MapPath("~/Upload/WeiXin/Images/bg.jpg");
			Image bgImg = Image.FromFile(bg, true);
			Bitmap bmp = new Bitmap(bgImg.Width, bgImg.Height, PixelFormat.Format32bppArgb);
			g = Graphics.FromImage(bmp);
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.CompositingQuality = CompositingQuality.HighQuality;
			g.DrawImage(bgImg, new Rectangle(0, 0, bgImg.Width, bgImg.Height), new Rectangle(0, 0, bgImg.Width, bgImg.Height), GraphicsUnit.Pixel);
			g.DrawImage(bigWeiXin, new RectangleF(50f, 60f, 200f, 200f), new RectangleF(0f, 0f, 200f, 200f), GraphicsUnit.Pixel);
			g.DrawString("NO:" + mem.MemCard, new Font("微软雅黑", 34f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, new PointF(280f, 70f));
			g.DrawString("积分:" + mem.MemCard, new Font("微软雅黑", 30f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, new PointF(280f, 115f));
			g.DrawString("余额:" + mem.MemMoney.ToString("F2"), new Font("微软雅黑", 30f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, new PointF(280f, 160f));
			string pastStr = (mem.MemPastTime.ToString("yyyy-MM-dd") == "2900-01-01") ? "有效期:永久有效" : ("有效期:" + mem.MemPastTime.ToString("yyyy-MM-dd"));
			g.DrawString(pastStr, new Font("微软雅黑", 30f, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.White, new PointF(280f, 205f));
			bmp.Save(savePath, ImageFormat.Jpeg);
		}

		public int MemRegister(bool isSMSVcode)
		{
			Chain.Model.Mem memModel = new Chain.Model.Mem();
			if (!isSMSVcode)
			{
				memModel.MemCard = this.Content;
			}
			else
			{
				memModel.MemCard = this.weiXinLogModel.RecordContent;
			}
			memModel.MemPassword = DESEncrypt.Encrypt("");
			memModel.MemName = "";
			memModel.MemSex = true;
			memModel.MemIdentityCard = "";
			if (!isSMSVcode)
			{
				memModel.MemMobile = this.Content;
			}
			else
			{
				memModel.MemMobile = this.weiXinLogModel.RecordContent;
			}
			memModel.MemPhoto = "";
			memModel.MemBirthdayType = true;
			memModel.MemBirthday = Convert.ToDateTime("1900-1-1 0:00:00");
			memModel.MemIsPast = false;
			memModel.MemPastTime = Convert.ToDateTime("2900-1-1 0:00:00");
			memModel.MemPoint = 0;
			memModel.MemPointAutomatic = true;
			memModel.MemMoney = 0m;
			memModel.MemEmail = "";
			memModel.MemAddress = "";
			memModel.MemState = 0;
			memModel.MemRecommendID = 0;
			memModel.MemLevelID = 0;
			memModel.MemShopID = 1;
			memModel.MemCreateTime = DateTime.Now;
			memModel.MemRemark = "";
			memModel.MemUserID = 1;
			memModel.MemTelePhone = "";
			memModel.MemWeiXinCard = this.FromUserName;
			memModel.MemQRCode = "";
			memModel.MemProvince = "";
			memModel.MemCity = "";
			memModel.MemCounty = "";
			memModel.MemVillage = "";
			return this.memBll.Add(memModel);
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
			catch (Exception ex_C1)
			{
			}
		}

		public List<Chain.Model.WeiXinNews> GetModel(string NewsTitle, string NewsDesc, string NewsUrlFirst, string NewsUrlSecond)
		{
			return new List<Chain.Model.WeiXinNews>
			{
				new Chain.Model.WeiXinNews
				{
					NewsTitle = NewsTitle,
					NewsDesc = NewsDesc,
					NewsUrlFirst = NewsUrlFirst,
					NewsUrlSecond = NewsUrlSecond
				}
			};
		}

		protected void UrlValid()
		{
			string signature = this.Request.QueryString["signature"].ToString();
			string timestamp = this.Request.QueryString["timestamp"].ToString();
			string nonce = this.Request.QueryString["nonce"].ToString();
			string echostr = this.Request.QueryString["echostr"].ToString();
			List<string> list = new List<string>();
			list.Add(this.token);
			list.Add(timestamp);
			list.Add(nonce);
			list.Sort();
			string newSignature = "";
			foreach (string item in list)
			{
				newSignature += item;
			}
			byte[] StrRes = Encoding.UTF8.GetBytes(newSignature);
			HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
			StrRes = iSHA.ComputeHash(StrRes);
			StringBuilder EnText = new StringBuilder();
			byte[] array = StrRes;
			for (int i = 0; i < array.Length; i++)
			{
				byte iByte = array[i];
				EnText.AppendFormat("{0:x2}", iByte);
			}
			if (signature == EnText.ToString())
			{
				this.Response.Write(echostr);
			}
			else
			{
				this.Response.Write("error");
			}
			PubFunction.SaveSysLog(1, 4, "微信接口验证", string.Format("signature={0}&timestamp={1}&nonce={2}&echostr={3}&newSignature={4}&StrRes={5}", new object[]
			{
				signature,
				timestamp,
				nonce,
				echostr,
				newSignature,
				EnText.ToString()
			}), 1, DateTime.Now, PubFunction.ipAdress);
		}

		private void AttentionNews(string postStr)
		{
			string Event = this.xmlDoc.GetElementsByTagName("Event")[0].InnerText;
			if (Event == "subscribe")
			{
				StringBuilder tempSb = new StringBuilder();
				tempSb.AppendLine(PubFunction.curParameter.strWeiXinSalutatory).AppendLine().AppendLine(this.weiXinRuleBLL.AttentionStr());
				this.sendXML = ResponseSendStr.Text(postStr, tempSb.ToString());
			}
			else if (Event == "CLICK")
			{
				this.MenuClick();
			}
		}

		private void TextNews()
		{
			this.Content = this.xmlDoc.GetElementsByTagName("Content")[0].InnerText.Trim();
			if (this.Content == "1")
			{
				this.ApplicationMemberCard();
			}
			else if (this.Content == "2")
			{
				this.ConvertWeiXinMember(this.postStr);
			}
			else
			{
				this.UnknownTextNewsHandle(this.postStr, this.Content);
			}
		}

		private void ApplicationMemberCard()
		{
			Chain.Model.Mem mem = this.memBll.GetMemByWeiXinCard(this.FromUserName);
			if (mem == null)
			{
				this.sendXML = ResponseSendStr.Text(this.postStr, "您已进入会员申请模式," + this.weiXinRuleBLL.Reply1());
				this.GiveWeiXinLodAssignment("", 0, "0", "", 0);
				this.weiXinLogBLL.Add(this.weiXinLogModel);
			}
			else
			{
				string NewsDesc = string.IsNullOrEmpty(PubFunction.curParameter.strWeiXinShopName) ? "" : PubFunction.curParameter.strWeiXinShopName;
				NewsDesc = ((NewsDesc == "") ? "您的微信会员卡" : ("您的" + NewsDesc + "微信会员卡"));
				this.Card(mem, "会员卡查询", NewsDesc);
			}
		}

		private void UnknownTextNewsHandle(string postStr, string Content)
		{
			this.ruleModel = this.weiXinRuleBLL.GetModelByNewsRuleID(Content);
			if (this.ruleModel != null)
			{
				if (this.ruleModel.RuleNewsType == "text")
				{
					this.sendXML = ResponseSendStr.Text(postStr, this.ruleModel.RuleContent);
				}
				else if (this.ruleModel.RuleNewsType == "news")
				{
					List<Chain.Model.WeiXinNews> newsList = new Chain.BLL.WeiXinNews().GetModelList("NewsRuleID=" + this.ruleModel.RuleID);
					this.sendXML = ResponseSendStr.News(postStr, newsList);
				}
			}
		}

		private void GiveWeiXinLodAssignment(string RecordContent, int RecordContentType, string StatusCode, string RandomCode, int ErrorTimes)
		{
			this.weiXinLogModel.MemWeiXinCard = this.FromUserName;
			this.weiXinLogModel.RecordContent = RecordContent;
			this.weiXinLogModel.RecordContentType = RecordContentType;
			this.weiXinLogModel.StatusCode = StatusCode;
			this.weiXinLogModel.RandomCode = RandomCode;
			this.weiXinLogModel.ErrorTimes = ErrorTimes;
			this.weiXinLogModel.WeiXinLogCreateTime = DateTime.Now;
		}

		private void ConvertWeiXinMember(string postStr)
		{
			Chain.Model.Mem mem = this.memBll.GetMemByWeiXinCard(this.FromUserName);
			if (mem != null)
			{
				this.sendXML = ResponseSendStr.Text(postStr, "您已是微信会员");
			}
			else
			{
				this.sendXML = ResponseSendStr.Text(postStr, "您已进入转微信会员模式," + this.weiXinRuleBLL.Reply2());
				this.GiveWeiXinLodAssignment("", 0, "2", "", 0);
				this.weiXinLogBLL.Add(this.weiXinLogModel);
			}
		}

		private void Do0ForApplicationMember()
		{
			this.Content = this.xmlDoc.GetElementsByTagName("Content")[0].InnerText.Trim();
			if (Regex.IsMatch(this.Content, "^1[3|4|5|8]\\d{9}$"))
			{
				Chain.Model.Mem mem = this.memBll.GetModelByMemMobile(this.Content);
				if (mem != null)
				{
					this.sendXML = ResponseSendStr.Text(this.postStr, "该手机号已被注册,请重新输入一个新的手机号");
				}
				else if (PubFunction.curParameter.bolWeiXinSMSVcode)
				{
					if (PubFunction.curParameter.bolSms)
					{
						if (SMSInfo.GetBalance(false) == "0")
						{
							this.sendXML = ResponseSendStr.Text(this.postStr, "商家短信不足,发送短信验证码失败,请与商家联系");
							this.weiXinLogBLL.Delete(this.FromUserName);
						}
						else
						{
							string smsVCode = this.GetRandomCode();
							string NewsDesc = string.IsNullOrEmpty(PubFunction.curParameter.strWeiXinShopName) ? "" : PubFunction.curParameter.strWeiXinShopName;
							NewsDesc = string.Concat(new string[]
							{
								"温馨提示,欢迎您注册",
								NewsDesc,
								"微信会员,您的",
								NewsDesc,
								"微信会员短信验证码是：",
								smsVCode
							});
							this.sendXML = ResponseSendStr.Text(this.postStr, "请发送短信验证码");
							SMSInfo.Send_GXSMS(false, this.Content, NewsDesc, "");
							Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
							modelSms.SmsMemID = 0;
							modelSms.SmsMobile = this.Content;
							modelSms.SmsContent = NewsDesc;
							modelSms.SmsTime = DateTime.Now;
							modelSms.SmsShopID = 1;
							modelSms.SmsUserID = 1;
							modelSms.SmsAmount = PubFunction.GetSmsAmount(NewsDesc);
							modelSms.SmsAllAmount = modelSms.SmsAmount;
							Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
							bllSms.Add(modelSms);
							this.GiveWeiXinLodAssignment(this.Content, 2, "1", smsVCode, 0);
							this.weiXinLogBLL.Update(this.weiXinLogModel);
						}
					}
					else
					{
						this.sendXML = ResponseSendStr.Text(this.postStr, "系统短信功能暂未开启,发送短信验证码失败,请与商家联系");
						this.weiXinLogBLL.Delete(this.FromUserName);
					}
				}
				else
				{
					int isOk = this.MemRegister(false);
					if (isOk > 0)
					{
						this.DoCard("恭喜您会员卡办理成功");
					}
					else
					{
						string strErr = "系统错误，会员办卡失败！";
						int num = isOk;
						if (num != -6)
						{
							switch (num)
							{
							case -2:
								strErr = "手机号码重复，会员办卡失败！";
								break;
							case -1:
								strErr = "会员卡号重复，会员办卡失败！";
								break;
							}
						}
						else
						{
							strErr = "卡面号重复，会员办卡失败！";
						}
						this.sendXML = ResponseSendStr.Text(this.postStr, strErr);
					}
				}
			}
			else
			{
				this.GiveWeiXinLodAssignment("", 0, "0", "", this.weiXinLogModel.ErrorTimes + 1);
				this.ErrorNewsHandle("由于您的误操作次数过多,会员申请模式已退出", "您发送的手机号格式有误,请重新发送");
			}
		}

		private void Do1ForApplicationMemberByReplyTelNumber()
		{
			this.Content = this.xmlDoc.GetElementsByTagName("Content")[0].InnerText.Trim();
			if (!PubFunction.curParameter.bolWeiXinSMSVcode || this.weiXinLogModel.RandomCode == this.Content)
			{
				int isOk = this.MemRegister(true);
				if (isOk > 0)
				{
					this.DoCard("恭喜您会员卡办理成功");
				}
				else
				{
					string strErr = "系统错误，会员办卡失败！";
					int num = isOk;
					if (num != -6)
					{
						switch (num)
						{
						case -2:
							strErr = "手机号码重复，会员办卡失败！";
							break;
						case -1:
							strErr = "会员卡号重复，会员办卡失败！";
							break;
						}
					}
					else
					{
						strErr = "卡面号重复，会员办卡失败！";
					}
					this.sendXML = ResponseSendStr.Text(this.postStr, strErr);
				}
			}
			else
			{
				this.GiveWeiXinLodAssignment(this.weiXinLogModel.RecordContent, this.weiXinLogModel.RecordContentType, this.weiXinLogModel.StatusCode, this.weiXinLogModel.RandomCode, this.weiXinLogModel.ErrorTimes + 1);
				this.ErrorNewsHandle("由于您的误操作次数过多,会员申请模式已退出", "您发送的短信验证码有误,请重新发送");
			}
		}

		private void Do2MemberTransform()
		{
			this.Content = this.xmlDoc.GetElementsByTagName("Content")[0].InnerText.Trim();
			this.memModel = this.memBll.GetModelByMemCard(this.Content);
			if (this.memModel != null)
			{
				this.sendXML = ResponseSendStr.Text(this.postStr, "请发送您的会员卡密码,如果密码为空请回复 # 号");
				this.GiveWeiXinLodAssignment(this.memModel.MemCard, 1, "3", "", 0);
				this.weiXinLogBLL.Update(this.weiXinLogModel);
			}
			else
			{
				Chain.Model.Mem mem = this.memBll.GetModelByMemMobile(this.Content);
				if (mem != null)
				{
					if (PubFunction.curParameter.bolWeiXinSMSVcode)
					{
						if (PubFunction.curParameter.bolSms)
						{
							if (SMSInfo.GetBalance(false) == "0")
							{
								string strTemplet = string.Format("由于本系统短信剩余条数不足,无法给您发送短信验证码;\r\n您的会员卡号为:{0}****{1},请发送您的会员卡密码,如果密码为空请回复 # 号", mem.MemCard.Substring(0, 1), mem.MemCard.Substring(mem.MemCard.Length - 1, 1));
								this.sendXML = ResponseSendStr.Text(this.postStr, strTemplet);
								this.GiveWeiXinLodAssignment(mem.MemCard, 1, "3", "", 0);
								this.weiXinLogBLL.Update(this.weiXinLogModel);
							}
							else
							{
								string smsVCode = this.GetRandomCode();
								string NewsDesc = string.IsNullOrEmpty(PubFunction.curParameter.strWeiXinShopName) ? "" : PubFunction.curParameter.strWeiXinShopName;
								NewsDesc = string.Concat(new string[]
								{
									"温馨提示,欢迎您申请从商家会员转",
									NewsDesc,
									"微信会员,您的",
									NewsDesc,
									"微信会员短信验证码是：",
									smsVCode
								});
								this.sendXML = ResponseSendStr.Text(this.postStr, "请发送短信验证码");
								SMSInfo.Send_GXSMS(false, this.Content, NewsDesc, "");
								Chain.Model.SmsLog modelSms = new Chain.Model.SmsLog();
								modelSms.SmsMemID = Convert.ToInt32(mem.MemID);
								modelSms.SmsMobile = this.Content;
								modelSms.SmsContent = NewsDesc;
								modelSms.SmsTime = DateTime.Now;
								modelSms.SmsShopID = 1;
								modelSms.SmsUserID = 1;
								modelSms.SmsAmount = PubFunction.GetSmsAmount(NewsDesc);
								modelSms.SmsAllAmount = modelSms.SmsAmount;
								Chain.BLL.SmsLog bllSms = new Chain.BLL.SmsLog();
								bllSms.Add(modelSms);
								this.GiveWeiXinLodAssignment(this.Content, 2, "3", smsVCode, 0);
								this.weiXinLogBLL.Update(this.weiXinLogModel);
							}
						}
						else
						{
							string strTemplet = string.Format("由于本系统短信功能暂未开启,无法给您发送短信验证码;\r\n您的会员卡号为:{0}****{1},请发送您的会员卡密码,如果密码为空请回复 # 号", mem.MemCard.Substring(0, 1), mem.MemCard.Substring(mem.MemCard.Length - 1, 1));
							this.sendXML = ResponseSendStr.Text(this.postStr, strTemplet);
							this.GiveWeiXinLodAssignment(mem.MemCard, 1, "3", "", 0);
							this.weiXinLogBLL.Update(this.weiXinLogModel);
						}
					}
					else
					{
						this.memModel = this.memBll.GetMemInfoByMobile(this.Content);
						this.memModel.MemWeiXinCard = this.FromUserName;
						this.memBll.Update(this.memModel);
						this.DoCard("恭喜您已转移成微信会员");
					}
				}
				else
				{
					this.GiveWeiXinLodAssignment(this.weiXinLogModel.RecordContent, this.weiXinLogModel.RecordContentType, this.weiXinLogModel.StatusCode, this.weiXinLogModel.RandomCode, this.weiXinLogModel.ErrorTimes + 1);
					this.ErrorNewsHandle("由于您的错误操作次数过多,转微信会员模式已退出", "您发送的手机号或卡号在系统中没找着,请重新发送");
				}
			}
		}

		private void Do3ForMemberTransformByReplyTelNumberOrMemberCard()
		{
			string Content = this.xmlDoc.GetElementsByTagName("Content")[0].InnerText.Trim();
			if (this.weiXinLogModel.RecordContentType == 1)
			{
				string pwd = DESEncrypt.Encrypt((Content == "#") ? "" : ((Content == "＃") ? "" : Content));
				this.memModel = this.memBll.GetModelByMemCard(this.weiXinLogModel.RecordContent);
				if (pwd == this.memModel.MemPassword)
				{
					this.memModel.MemWeiXinCard = this.weiXinLogModel.MemWeiXinCard;
					this.memBll.Update(this.memModel);
					this.DoCard("恭喜您已转移成微信会员");
				}
				else
				{
					this.GiveWeiXinLodAssignment(this.weiXinLogModel.RecordContent, 1, "3", "", this.weiXinLogModel.ErrorTimes + 1);
					this.ErrorNewsHandle("由于您的错误操作次数过多,转微信会员模式已退出", "您发送的会员卡密码不正确,请重新发送");
				}
			}
			else if (!PubFunction.curParameter.bolWeiXinSMSVcode || this.weiXinLogModel.RandomCode == Content)
			{
				this.memModel = this.memBll.GetMemInfoByMobile(this.weiXinLogModel.RecordContent);
				this.memModel.MemWeiXinCard = this.weiXinLogModel.MemWeiXinCard;
				this.memBll.Update(this.memModel);
				this.DoCard("恭喜您已转移成微信会员");
			}
			else
			{
				this.GiveWeiXinLodAssignment(this.weiXinLogModel.RecordContent, 2, this.weiXinLogModel.StatusCode, this.weiXinLogModel.RandomCode, this.weiXinLogModel.ErrorTimes + 1);
				this.ErrorNewsHandle("由于您的错误操作次数过多,转微信会员模式已退出", "您发送的短信验证码有误,请重新发送");
			}
		}

		private string GetRandomCode()
		{
			Random random = new Random();
			string result = string.Empty;
			for (int i = 0; i < 4; i++)
			{
				result += random.Next(0, 10);
			}
			return result;
		}

		private void Card(Chain.Model.Mem mem, string NewsTitle, string NewsDesc)
		{
			this.DrawImage(mem, this.FromUserName);
			string NewsUrlFirst = string.Concat(new object[]
			{
				"http://",
				PubFunction.curParameter.strDoMain,
				"/Upload/WeiXin/Images/",
				this.FromUserName,
				".jpg?id=",
				Guid.NewGuid()
			});
			string NewsUrlSecond = "http://" + PubFunction.curParameter.strDoMain + "/ReceptionPage/index.aspx?MemWeiXinCard=" + this.FromUserName;
			List<Chain.Model.WeiXinNews> newsList = this.GetModel(NewsTitle, NewsDesc, NewsUrlFirst, NewsUrlSecond);
			this.sendXML = ResponseSendStr.News(this.postStr, newsList);
		}

		private void ErrorNewsHandle(string errorTimesExceed, string errorTimesNoExceed)
		{
			if (this.weiXinLogModel.ErrorTimes >= 3)
			{
				this.sendXML = ResponseSendStr.Text(this.postStr, errorTimesExceed);
				this.weiXinLogBLL.Delete(this.FromUserName);
			}
			else
			{
				this.sendXML = ResponseSendStr.Text(this.postStr, errorTimesNoExceed);
				this.weiXinLogBLL.Update(this.weiXinLogModel);
			}
		}

		private void DoCard(string NewsTitle)
		{
			Chain.Model.Mem mem = this.memBll.GetMemByWeiXinCard(this.FromUserName);
			string NewsDesc = string.IsNullOrEmpty(PubFunction.curParameter.strWeiXinShopName) ? "" : PubFunction.curParameter.strWeiXinShopName;
			NewsDesc = ((NewsDesc == "") ? "您的微信会员卡" : ("您的" + NewsDesc + "微信会员卡"));
			this.Card(mem, NewsTitle, NewsDesc);
			this.weiXinLogBLL.Delete(this.FromUserName);
		}

		private void WZZ()
		{
			if (this.memBll.GetMemByWeiXinCard(this.FromUserName) == null)
			{
				this.sendXML = ResponseSendStr.Text(this.postStr, "您目前还不是微会员,只有微会员才可以访问微网站");
			}
			else
			{
				string xml = File.ReadAllText(this.Server.MapPath("WWZ.xml"));
				XmlDocument xmlWZZDoc = new XmlDocument();
				xmlWZZDoc.LoadXml(xml);
				string NewsTitle = xmlWZZDoc.GetElementsByTagName("NewsTitle")[0].InnerText;
				string NewsDesc = xmlWZZDoc.GetElementsByTagName("NewsDesc")[0].InnerText;
				string NewsUrlFirst = string.Concat(new object[]
				{
					"http://",
					PubFunction.curParameter.strDoMain,
					xmlWZZDoc.GetElementsByTagName("NewsUrlFirst")[0].InnerText,
					"?id=",
					Guid.NewGuid()
				});
				string NewsUrlSecond = string.Concat(new string[]
				{
					"http://",
					PubFunction.curParameter.strDoMain,
					xmlWZZDoc.GetElementsByTagName("NewsUrlSecond")[0].InnerText,
					"?MemWeiXinCard=",
					this.FromUserName
				});
				List<Chain.Model.WeiXinNews> newsList = this.GetModel(NewsTitle, NewsDesc, NewsUrlFirst, NewsUrlSecond);
				this.sendXML = ResponseSendStr.News(this.postStr, newsList);
			}
		}

		private void MenuClick()
		{
			this.weiXinLogBLL.Delete(this.FromUserName);
			string EventKey = this.xmlDoc.GetElementsByTagName("EventKey")[0].InnerText.Trim();
			if (EventKey == "1")
			{
				this.ApplicationMemberCard();
			}
			else if (EventKey == "2")
			{
				this.ConvertWeiXinMember(this.postStr);
			}
			else if (EventKey == "3")
			{
				this.WZZ();
			}
			else
			{
				this.UnknownTextNewsHandle(this.postStr, EventKey);
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
