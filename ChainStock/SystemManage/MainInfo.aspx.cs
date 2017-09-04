using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class MainInfo : PageBase
	{
		protected HtmlForm frmGoodsInfo;

		protected Literal ltlTitle;

		protected HtmlInputText txtShopName;

		protected HtmlInputHidden txtShopID;

		protected HtmlInputText txtShopContactMan;

		protected HtmlInputText txtShopTelephone;

		protected HtmlInputText txtShopSmsName;

		protected HtmlInputText txtShopPrintTitle;

		protected HtmlInputText txtShopPrintFoot;

		protected HtmlTableRow trSettlement;

		protected HtmlInputText txtSettlementInterval;

		protected HtmlInputText txtShopProportion;

		protected HtmlInputText txtTotalRate;

		protected HtmlTableRow trAlliance;

		protected HtmlInputRadioButton rdislms;

		protected HtmlInputRadioButton rdisnotlms;

		protected HtmlTableRow trSltShop;

		protected HtmlSelect sltShopList;

		protected HtmlTableRow trShopSms;

		protected HtmlInputRadioButton rbbzfs;

		protected HtmlInputRadioButton rbkytz;

		protected HtmlTableRow trShopPoint;

		protected HtmlInputRadioButton rdbzxf;

		protected HtmlInputRadioButton rdjfgl;

		protected HtmlInputRadioButton rdtz;

		protected HtmlInputRadioButton radChooseYes;

		protected HtmlInputRadioButton radChooseNo;

		protected HtmlSelect sltProvince;

		protected HtmlSelect sltCity;

		protected HtmlSelect sltCounty;

		protected HtmlInputText txtShopAddress;

		protected HtmlTextArea txtShopRemark;

		protected HtmlInputHidden txtShopType;

		protected HtmlInputCheckBox chkSMS;

		protected HtmlInputHidden hdShopID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.txtShopID.Value = "1";
				Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
				Chain.Model.SysShop modelShop = bllSysShop.GetModel(1);
				this.txtShopName.Value = modelShop.ShopName;
				PubFunction.BindProvinceSelect(this.sltProvince);
				this.txtShopType.Value = "1";
				this.bindSltList();
				this.sltShopList.SelectedIndex = 0;
				this.trSltShop.Attributes.Add("style", "display:none;");
				this.trShopSms.Attributes.Add("style", "display:none;");
				this.trShopPoint.Attributes.Add("style", "display:none;");
			}
		}

		public void bindSltList()
		{
			this.hdShopID.Value = this._UserShopID.ToString();
			Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
			string strWhere = "ShopType=1 ";
			DataTable dtSysShop = bllSysShop.GetList(strWhere).Tables[0];
			foreach (DataRow dr in dtSysShop.Rows)
			{
				this.sltShopList.Items.Add(new ListItem(dr["ShopName"].ToString(), dr["ShopID"].ToString()));
			}
			if (this._UserShopID > 1)
			{
				this.sltShopList.Value = this._UserShopID.ToString();
				this.sltShopList.Attributes.Add("disabled", "disabled");
			}
		}
	}
}
