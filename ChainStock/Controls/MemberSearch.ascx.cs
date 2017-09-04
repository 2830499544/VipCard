using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Controls
{
	public class MemberSearch : UserControl
	{
		protected Literal litControl;

		protected HtmlInputHidden txtIsCanSlotCard;

		protected HtmlInputButton btnSenseReadCard;

		protected HtmlInputButton btnContactReadCard;

		public string WebRoot;

		private bool bolSenseICCard = false;

		private bool bolContactICCard = false;

		public bool BolSenseICCard
		{
			get
			{
				return this.bolSenseICCard;
			}
			set
			{
				this.bolSenseICCard = value;
			}
		}

		public bool BolContactICCard
		{
			get
			{
				return this.bolContactICCard;
			}
			set
			{
				this.bolContactICCard = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.txtIsCanSlotCard.Value = (PubFunction.curParameter.IsMustSlotCard ? "1" : "0");
			this.WebRoot = ((PageBase)this.Page).GetWebRoot();
			if (!this.BolSenseICCard)
			{
				this.btnSenseReadCard.Attributes.Add("style", "display:none");
			}
			else
			{
				HtmlGenericControl obj = new HtmlGenericControl("object");
				obj.InnerHtml = "<object style='display:none;' id='CardReader' classid='clsid:27DD3937-FA54-45B2-9A51-64D58826AC01'></object>";
				this.Parent.Page.Header.Controls.Add(obj);
			}
		}
	}
}
