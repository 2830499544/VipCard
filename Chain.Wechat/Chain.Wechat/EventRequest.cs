using Chain.BLL;
using Chain.Model;
using System;
using System.Xml;

namespace Chain.Wechat
{
	public class EventRequest : IRequest
	{
		public string Event
		{
			get;
			set;
		}

		public EventRequest(XmlDocument input) : base(input)
		{
			XmlNode root = base.XmlDoc.FirstChild;
			this.Event = root["Event"].InnerText;
			base.RequestEvent += new IRequest.RequestEventHandler(this.EventReqeustEventHandler);
		}

		public string EventReqeustEventHandler()
		{
			Chain.BLL.Mem membll = new Chain.BLL.Mem();
			Chain.BLL.MicroWebsiteSceneStr bll = new Chain.BLL.MicroWebsiteSceneStr();
			string @event = this.Event;
			string result;
			if (@event != null)
			{
				if (@event == "subscribe")
				{
					Chain.Model.Mem memmodel = membll.GetMemWeiXinCardModel(base.FromUserName, "MemWeiXinCard");
					if (memmodel != null)
					{
						memmodel.MemAttention = 1;
						membll.Update(memmodel);
					}
					result = new TextResponse(this, new BusinessLogic().GetWelcomeText()).Result();
					return result;
				}
				if (@event == "unsubscribe")
				{
					bll.Delete(base.FromUserName);
					Chain.Model.Mem memmodels = membll.GetMemWeiXinCardModel(base.FromUserName, "MemWeiXinCards");
					if (memmodels != null)
					{
						memmodels.MemAttention = 2;
						membll.Update(memmodels);
					}
					result = string.Empty;
					return result;
				}
			}
			result = string.Empty;
			return result;
		}
	}
}
