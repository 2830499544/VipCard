using Chain.BLL;
using Chain.Model;
using System;
using System.Xml;

namespace Chain.Wechat
{
	public class MenuRequest : EventRequest
	{
		public string EventKey
		{
			get;
			set;
		}

		public MenuRequest(XmlDocument input) : base(input)
		{
			XmlNode root = base.XmlDoc.FirstChild;
			this.EventKey = root["EventKey"].InnerText;
			base.RequestEvent += new IRequest.RequestEventHandler(this.MenuReqeustEventHandler);
		}

		public string MenuReqeustEventHandler()
		{
			LogManager.Remove(base.FromUserName);
			string result;
			if (base.Event == "CLICK")
			{
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem mem = bllMem.GetMemByWeiXinCard(base.FromUserName);
				string eventKey = this.EventKey;
				if (eventKey != null)
				{
					if (!(eventKey == "1"))
					{
						if (!(eventKey == "2"))
						{
							if (!(eventKey == "3"))
							{
								if (!(eventKey == "4"))
								{
									if (eventKey == "5")
									{
										if (mem == null)
										{
											result = new TextResponse(this, "您目前还不是微会员!").Result();
											return result;
										}
										if (mem.MemWeiXinCard == "")
										{
											result = new TextResponse(this, "此微信没有绑定账号!").Result();
											return result;
										}
										Chain.BLL.MicroWebsiteSceneStr bll = new Chain.BLL.MicroWebsiteSceneStr();
										bll.Delete(base.FromUserName);
										Chain.BLL.Mem membll = new Chain.BLL.Mem();
										Chain.Model.Mem memmodel = membll.GetMemWeiXinCardModel(base.FromUserName, "MemWeiXinCard");
										if (memmodel != null)
										{
											memmodel.MemWeiXinCard = null;
											membll.Update(memmodel);
										}
										result = new TextResponse(this, "解绑成功!").Result();
										return result;
									}
								}
								else
								{
									if (mem != null)
									{
										switch (new BusinessLogic().MemSign(mem))
										{
										case -3:
											result = new TextResponse(this, "您今日已经签到过，重复签到不会再次赠送积分!").Result();
											return result;
										case -2:
											result = new TextResponse(this, "对不起，签到送积分活动未开启!").Result();
											return result;
										case -1:
											result = new TextResponse(this, "您还未申请或绑定会员卡!").Result();
											return result;
										case 1:
											mem = bllMem.GetMemByWeiXinCard(base.FromUserName);
											result = new BusinessLogic().GetMemCardResponse(this, mem, "今日签到成功!");
											return result;
										}
										result = new TextResponse(this, "系统出现未知错误!").Result();
										return result;
									}
									result = new TextResponse(this, "您还未申请或绑定会员卡!").Result();
									return result;
								}
							}
							else
							{
								if (mem == null)
								{
									result = new TextResponse(this, "您目前还不是微会员,只有微会员才可以访问微网站!").Result();
									return result;
								}
								Chain.Model.SysParameter modelSysPara = new Chain.BLL.SysParameter().GetModel(1);
								string title = string.Format("点击进入{0}微网站", modelSysPara.WeiXinShopName);
								result = new BusinessLogic().GetMemCardResponse(this, mem, title);
								return result;
							}
						}
						else
						{
							if (mem != null)
							{
								result = new TextResponse(this, "您已经绑定了微信会员!").Result();
								return result;
							}
							TransformEntryNumberLog log = new TransformEntryNumberLog(0);
							LogManager.Add(base.FromUserName, log);
							result = new TextResponse(this, "您已进入转微信会员模式,请发送您的卡号!").Result();
							return result;
						}
					}
					else
					{
						if (mem != null)
						{
							result = new TextResponse(this, "您已是微信会员!").Result();
							return result;
						}
						RegisterEntryNumberLog log2 = new RegisterEntryNumberLog(0);
						LogManager.Add(base.FromUserName, log2);
						result = new TextResponse(this, "您已进入会员申请模式,请发送您的手机号码!").Result();
						return result;
					}
				}
				string responseText = new BusinessLogic().GetResponseByRule(this, this.EventKey);
				if (!string.IsNullOrEmpty(responseText))
				{
					result = responseText;
				}
				else
				{
					result = string.Empty;
				}
			}
			else
			{
				if (base.Event == "VIEW")
				{
				}
				result = string.Empty;
			}
			return result;
		}
	}
}
