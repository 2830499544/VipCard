using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.SystemManage
{
	public class AllianceList : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmShopList;

		protected HtmlInputHidden txtIsSendCard;

		protected HtmlInputHidden txtSmsManage;

		protected HtmlInputHidden txtPointManage;

		protected HtmlInputHidden txtIsSettlement;

		protected HtmlInputHidden hdShopID;

		protected HtmlInputHidden union;

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

		protected HtmlInputText txtRechargeProportion;

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

		protected HtmlInputCheckBox chkSMS;

		protected HtmlTextArea txtRemark;

		protected HtmlInputText txtShopType;

		protected HtmlInputButton btnShopAdd;

		protected HtmlInputButton btnShopBuyCard;

		protected TextBox txtSearch;

		protected Button btnSearch;

		protected Repeater gvShopListUnion;

		protected Repeater gvShopListProfession;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		public bool isopen = PubFunction.curParameter.bolIsSettlement;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.chkSMS.Checked = PubFunction.curParameter.bolSms;
				this.bindSltList();
				this.txtIsSendCard.Value = (PubFunction.curParameter.bolIsSendCard ? "1" : "0");
				this.txtSmsManage.Value = (PubFunction.curParameter.bolShopSmsManage ? "1" : "0");
				this.txtPointManage.Value = (PubFunction.curParameter.bolShopPointManage ? "1" : "0");
				this.txtIsSettlement.Value = (PubFunction.curParameter.bolIsSettlement ? "1" : "0");
				this.union.Value = PubFunction.curParameter.UsingUnion.ToString();
				this.txtShopType.Value = "2";
				this.NewMethod();
				PubFunction.BindProvinceSelect(this.sltProvince);
			}
		}

		private void NewMethod()
		{
			if (!PubFunction.curParameter.UsingUnion)
			{
				this.sltShopList.SelectedIndex = 0;
				this.trSltShop.Attributes.Add("style", "display:none;");
				this.trShopSms.Attributes.Add("style", "display:none;");
				this.trShopPoint.Attributes.Add("style", "display:none;");
				this.BindShopList("");
			}
			else
			{
				StringBuilder strSql = new StringBuilder();
				if (this._UserShopID == 1)
				{
					strSql.Append(" and FatherShopID = 1");
				}
				else
				{
					strSql.AppendFormat(" and (FatherShopID = {0} or ShopID={0})", this._UserShopID);
				}
				this.BindShopList(strSql.ToString());
				Chain.Model.SysShop shop = new Chain.BLL.SysShop().GetModel(this._UserShopID);
				if (!shop.IsAllianceProgram && shop.ShopID != 1)
				{
					this.btnShopAdd.Visible = false;
				}
			}
		}

		protected string BindShopName(object shopid)
		{
			Chain.BLL.SysShop Shop = new Chain.BLL.SysShop();
			string shopname = "";
			if (shopid != null)
			{
				shopname = Shop.GetShopNameByShopid(shopid.ToString());
			}
			return shopname;
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

		private void BindShopList(string strWhere)
		{
			Chain.BLL.SysShop Shop = new Chain.BLL.SysShop();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = " ShopID>0 and ShopType=2";
			strSql += strWhere;
			DataTable db = Shop.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			this.NetPagerParameter.RecordCount = Counts;
			this.NetPagerParameter.CustomInfoHTML = string.Format("<div class=\"results\"><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[]
			{
				this.NetPagerParameter.CurrentPageIndex,
				this.NetPagerParameter.PageCount,
				this.NetPagerParameter.RecordCount,
				this.NetPagerParameter.PageSize
			});
			if (PubFunction.curParameter.UsingUnion)
			{
				this.gvShopListProfession.Visible = false;
				this.gvShopListUnion.DataSource = db;
				this.gvShopListUnion.DataBind();
				PageBase.BindSerialRepeater(this.gvShopListUnion, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
			}
			else
			{
				this.gvShopListUnion.Visible = false;
				this.gvShopListProfession.DataSource = db;
				this.gvShopListProfession.DataBind();
				PageBase.BindSerialRepeater(this.gvShopListProfession, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
			}
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindShopList(this.GetSearch());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindShopList(this.GetSearch());
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			this.BindShopList(this.GetSearch());
		}

		public string GetSearch()
		{
			string strScarch = "";
			if (this.txtSearch.Text.Trim() != "")
			{
				strScarch = string.Format(" and (ShopName like '%{0}%' or ShopTelephone like '%{0}%' or ShopContactMan like '%{0}%') ", this.txtSearch.Text.Trim());
			}
			return strScarch;
		}
	}
}
