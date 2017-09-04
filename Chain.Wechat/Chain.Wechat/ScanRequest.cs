using Chain.BLL;
using Chain.Model;
using System;
using System.Xml;

namespace Chain.Wechat
{
	public class ScanRequest : IRequest
	{
		public string Event
		{
			get;
			set;
		}

		public string EventKey
		{
			get;
			set;
		}

		public string Ticket
		{
			get;
			set;
		}

		public ScanRequest(XmlDocument input) : base(input)
		{
			XmlNode root = base.XmlDoc.FirstChild;
			this.EventKey = root["EventKey"].InnerText;
			this.Event = root["Event"].InnerText;
			base.RequestEvent += new IRequest.RequestEventHandler(this.ScanRequestEventHandler);
		}

		public string ScanRequestEventHandler()
		{
			string @event = this.Event;
			string result;
			if (@event != null)
			{
				if (@event == "subscribe")
				{
					Chain.Model.MicroWebsiteSceneStr model = new Chain.Model.MicroWebsiteSceneStr();
					Chain.BLL.MicroWebsiteSceneStr bll = new Chain.BLL.MicroWebsiteSceneStr();
					string[] sz = this.EventKey.Split(new char[]
					{
						'_'
					});
					model.SceneStr = Convert.ToInt32(sz[1]);
					model.OppenId = base.FromUserName;
					bll.Add(model);
					Chain.BLL.Mem membll = new Chain.BLL.Mem();
					Chain.Model.Mem memmodel = new Chain.BLL.Mem().GetMemWeiXinCardModel(base.FromUserName, "MemWeiXinCard");
					if (memmodel != null)
					{
						memmodel.MemAttention = 1;
						membll.Update(memmodel);
					}
					result = new TextResponse(this, new BusinessLogic().GetWelcomeText()).Result();
					return result;
				}
				if (@event == "SCAN")
				{
					result = new TextResponse(this, new BusinessLogic().GetWelcomeText()).Result();
					return result;
				}
			}
			result = string.Empty;
			return result;
		}
	}
}
