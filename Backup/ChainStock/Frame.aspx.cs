using Chain.Common;
using System;
using System.Web.UI;

namespace ChainStock
{
	public class Frame : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			base.Title = ConfigHelper.GetValue("SystemTitle");
		}
	}
}
