using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.Controls
{
	public class Pay : UserControl
	{
		protected HtmlTableRow trByCard;

		protected HtmlInputCheckBox chkCard;

		protected HtmlTableRow trByCash;

		protected HtmlInputCheckBox chkCash;

		protected HtmlTableRow trByBink;

		protected HtmlInputCheckBox chkBink;

		protected HtmlTableRow trByPoint;

		protected HtmlInputCheckBox chkPoint;

		protected HtmlInputHidden txtPointUsePercent;

		protected HtmlInputHidden txtPointDiscountPercent;

		protected HtmlTableRow trPayCoupon;

		public string WebRoot;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.WebRoot = ((PageBase)this.Page).GetWebRoot();
			if (!PubFunction.curParameter.bolIsPayCard)
			{
				this.trByCard.Style.Add("display", "none");
			}
			if (!PubFunction.curParameter.bolIsPayCash)
			{
				this.trByCash.Style.Add("display", "none");
			}
			if (!PubFunction.curParameter.bolIsPayBink)
			{
				this.trByBink.Style.Add("display", "none");
			}
			if (!PubFunction.curParameter.bolIsPayCoupon)
			{
				this.trPayCoupon.Style.Add("display", "none");
			}
			if (!PubFunction.curParameter.bolIsPayPoint)
			{
				this.trByPoint.Style.Add("display", "none");
			}
			else
			{
				this.txtPointUsePercent.Value = PubFunction.curParameter.PointUsePercent.ToString();
				this.txtPointDiscountPercent.Value = PubFunction.curParameter.PointDiscountPercent.ToString();
			}
		}
	}
}
