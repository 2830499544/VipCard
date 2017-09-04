namespace ChainStock.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public class FindMember : UserControl
    {
        protected HtmlInputButton btnContactReadCard;
        protected HtmlInputButton btnSenseReadCard;
        protected HtmlInputHidden txtIsCanSlotCard;
        public string WebRoot;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtIsCanSlotCard.Value = PubFunction.curParameter.IsMustSlotCard ? "1" : "0";
            PageBase curPage = (PageBase) this.Page;
            this.WebRoot = curPage.GetWebRoot();
            this.btnSenseReadCard.Visible = curPage.curParameter.bolSenseiccard;
            this.btnContactReadCard.Visible = curPage.curParameter.bolContacticcard;
        }
    }
}

