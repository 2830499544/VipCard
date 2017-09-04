using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.ExtraService
{
	public class SmsTemplate : PageBase
	{
		protected HtmlForm frmSmsTemplate;

		protected Literal ltlTitle;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected Button btnSmsTemplateSearch;

		protected HtmlInputText txtTemplateName;

		protected HtmlInputHidden txtTemplateID;

		protected HtmlTextArea txtTemplateContent;

		protected HtmlInputButton btnAddTemplate;

		protected Repeater gvSmsTemplate;

		protected void Page_Load(object sender, EventArgs e)
		{
			PubFunction.BindShopSelect(this._UserShopID, this.sltShop, this._UserShopID, false);
			this.Get_ParameterList(this.QueryCondition());
		}

		private void Get_ParameterList(string strWhere)
		{
			Chain.BLL.SmsTemplate bllSmsTemplate = new Chain.BLL.SmsTemplate();
			int Counts = 1;
			strWhere += "and dbo.SmsTemplate.TemplateShopID=dbo.SysShop.ShopID ";
			DataTable db = bllSmsTemplate.GetListSP(10000, 1, out Counts, new string[]
			{
				strWhere
			}).Tables[0];
			this.gvSmsTemplate.DataSource = db;
			this.gvSmsTemplate.DataBind();
			PageBase.BindSerialRepeater(this.gvSmsTemplate, 0);
		}

		protected void btnSmsTemplateSearch_Click(object sender, EventArgs e)
		{
			this.Get_ParameterList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strWhere = " 1=1 ";
			string strShopID = this.sltShop.Value;
			if (strShopID != "1")
			{
				strWhere += string.Format(" and TemplateShopID in ({0})", this._UserShopID);
			}
			else
			{
				strWhere = strWhere + "and " + PubFunction.GetMemListShopAuthority(this._UserShopID, "TemplateShopID", strWhere);
			}
			return strWhere;
		}
	}
}
