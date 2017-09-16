using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.Controls
{
	public class SysArea : UserControl
	{
		public bool _needBind = true;

		public HtmlSelect sltProvince;

		public HtmlSelect sltCity;

		public HtmlSelect sltCounty;

		public HtmlSelect sltVillage;

		public bool NeedBind
		{
			get
			{
				return this._needBind;
			}
			set
			{
				this._needBind = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.NeedBind)
				{
					PubFunction.BindProvinceSelect(this.sltProvince);
				}
			}
		}
	}
}
