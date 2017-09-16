using Chain.Common;
using System;
using System.Web.UI;

namespace ChainStock
{
	public class Main : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			switch (PubFunction.curParameter.istry)
			{
			case 0:
				if (PubFunction.curParameter.UsingUnion)
				{
					base.Title = "商家联盟会员消费管理系统_商家联盟会员积分管理系统_异业联盟会员消费管理系统 ";
				}
				else
				{
					base.Title = "智络连锁店会员管理系统_连锁店会员积分系统_专业版在线免费试用地址 ";
				}
				break;
			case 1:
				base.Title = ConfigHelper.GetValue("SystemTitle");
				break;
			case 2:
				if (PubFunction.curParameter.UsingUnion)
				{
					base.Title = "商家联盟会员消费管理系统 ";
				}
				else
				{
					base.Title = "连锁会员管理系统 专业版 ";
				}
				break;
			}
		}
	}
}
