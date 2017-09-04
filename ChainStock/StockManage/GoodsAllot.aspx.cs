using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.StockManage
{
	public class GoodsAllot : PageBase
	{
		protected HtmlForm frmGoodsAllot;

		protected Literal ltlTitle;

		protected HtmlInputButton btnGoodsSearch;

		protected HtmlSelect sltOutShopID;

		protected HtmlGenericControl spGoodsAccounte;

		protected HtmlGenericControl lblUSer;

		protected HtmlInputText txtCreateTime;

		protected HtmlSelect sltInShopID;

		protected HtmlGenericControl lblTotalNumber;

		protected HtmlInputText txtExRemark;

		protected HtmlInputHidden HidAllotID;

		protected HtmlInputHidden HidExit;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindShopSelect(1, this.sltOutShopID, true);
				this.sltOutShopID.Value = this._UserShopID.ToString();
				PubFunction.BindShopSelect(this._UserShopID, this.sltInShopID, true);
				this.lblUSer.InnerText = this._UserName;
				this.spGoodsAccounte.InnerText = this.curParameter.strGoodsAllotPrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
				this.txtCreateTime.Value = DateTime.Now.ToString();
				if (base.Request.QueryString["Type"] != null)
				{
					int type = Convert.ToInt32(base.Request.QueryString["Type"]);
					if (type == 1)
					{
						this.sltInShopID.Value = this._UserShopID.ToString();
						this.sltInShopID.Attributes.Add("disabled", "disabled");
					}
					if (type == 2)
					{
						this.sltOutShopID.Value = this._UserShopID.ToString();
						this.sltOutShopID.Attributes.Add("disabled", "disabled");
					}
				}
				if (base.Request.QueryString["AllotID"] != null)
				{
					int AllotID = Convert.ToInt32(base.Request.QueryString["AllotID"]);
					Chain.Model.GoodsAllot modelAllot = new Chain.BLL.GoodsAllot().GetModel(AllotID);
					this.spGoodsAccounte.InnerText = modelAllot.AllotAccount;
					this.txtExRemark.Value = modelAllot.AllotRemark;
					this.HidAllotID.Value = AllotID.ToString();
					this.HidExit.Value = "2";
					this.sltOutShopID.Value = modelAllot.AllotOutShopID.ToString();
					this.sltOutShopID.Attributes.Add("disabled", "disabled");
					this.sltInShopID.Value = modelAllot.AllotInShopID.ToString();
					this.sltInShopID.Attributes.Add("disabled", "disabled");
				}
			}
		}
	}
}
