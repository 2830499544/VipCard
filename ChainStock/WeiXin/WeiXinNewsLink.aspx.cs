using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;

namespace ChainStock.WeiXin
{
	public class WeiXinNewsLink : Page
	{
		protected string imgUrl = "";

		protected string divMain = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			string tempNewsID = base.Request["NewsID"];
			if (!string.IsNullOrEmpty(tempNewsID))
			{
				int NewsID;
				if (int.TryParse(tempNewsID, out NewsID))
				{
					Chain.Model.WeiXinNews newsModel = new Chain.BLL.WeiXinNews().GetModel(NewsID);
					if (newsModel != null)
					{
						this.imgUrl = newsModel.NewsUrlFirst;
						this.divMain = newsModel.NewsLinkContent;
					}
				}
			}
		}
	}
}
