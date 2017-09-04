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
	public class ShopList : PageBase
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

		protected HtmlTableRow tr1;

		protected HtmlInputText txtTotalRate;

		protected HtmlInputRadioButton radRechargeYes;

		protected HtmlInputRadioButton radRechargeNo;

		protected HtmlTableRow trShopRecharge;

		protected HtmlInputText txtRechargeMaxMoney;

		protected HtmlInputRadioButton radMainShopYes;

		protected HtmlInputRadioButton radMainShopNo;

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

		protected HtmlImage imgShopPhoto;

		protected HtmlInputHidden txtShopPhoto;

		protected HtmlTextArea txtShopRemark;

		protected HtmlInputCheckBox chkSMS;

		protected HtmlTextArea txtRemark;

		protected HtmlInputText txtShopType;

		protected HtmlInputButton btnShopAdd;

		protected HtmlInputButton btnMainShopAdd;

		protected HtmlInputButton btnShopBuyCard;

		protected HtmlInputButton btnSettlement;

		protected HtmlInputButton btnPointRecord;

		protected HtmlInputButton btnMsgRecord;

		protected TextBox txtSearch;

		protected Button btnSearch;

		protected HtmlSelect sltShopState;

		protected Button btnShopSearch;

		protected Repeater gvShopList;

		protected Repeater gvShopListProfession;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		public bool isopen = PubFunction.curParameter.bolIsSettlement;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.sltShopState.Value = "0";
				this.chkSMS.Checked = PubFunction.curParameter.bolSms;
				this.bindSltList();
				this.txtIsSendCard.Value = (PubFunction.curParameter.bolIsSendCard ? "1" : "0");
				this.txtSmsManage.Value = (PubFunction.curParameter.bolShopSmsManage ? "1" : "0");
				this.txtPointManage.Value = (PubFunction.curParameter.bolShopPointManage ? "1" : "0");
				this.txtIsSettlement.Value = (PubFunction.curParameter.bolIsSettlement ? "1" : "0");
				this.union.Value = PubFunction.curParameter.UsingUnion.ToString();
				this.txtShopType.Value = "3";
				this.NewMethod();
				PubFunction.BindProvinceSelect(this.sltProvince);
				Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
				int count = bllShop.GetRecordCount("IsMain=1");
				if (this._UserGroupID == 1 && count == 0)
				{
					this.btnMainShopAdd.Visible = true;
				}
				else
				{
					this.btnMainShopAdd.Visible = false;
				}
			}
		}

		private void NewMethod()
		{
			if (!PubFunction.curParameter.UsingUnion)
			{
				this.txtSettlementInterval.Value = "36500";
				this.txtTotalRate.Value = "0";
				this.trSettlement.Attributes.Add("style", "display:none;");
				this.sltShopList.SelectedIndex = 0;
				this.trSltShop.Attributes.Add("style", "display:none;");
				this.trShopSms.Attributes.Add("style", "display:none;");
				this.trShopPoint.Attributes.Add("style", "display:none;");
				this.trShopRecharge.Attributes.Add("style", "display:none;");
				this.BindShopList(" ShopState=0 ");
			}
			else
			{
				StringBuilder strSql = new StringBuilder();
				if (this._UserShopID == 1)
				{
					strSql.Append("  and ShopState=0  or (FatherShopID = 0 and shopid>1)  ");
				}
				else
				{
					strSql.AppendFormat(" and ShopState=0  and (FatherShopID = {0} or ShopID={0})", this._UserShopID);
				}
				this.BindShopList(strSql.ToString());
				Chain.Model.SysShop shop = new Chain.BLL.SysShop().GetModel(this._UserShopID);
				if (shop.ShopType == 3)
				{
					this.btnShopAdd.Visible = false;
				}
			}
		}

		public void bindSltList()
		{
			this.hdShopID.Value = this._UserShopID.ToString();
			Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
			string strWhere = "ShopType=2";
			this.sltShopList.Items.Add(new ListItem("===选择联盟商===", "0"));
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

		protected string BindAddress(object shopID)
		{
			Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
			Chain.Model.SysShop modelShop = bllSysShop.GetModel(int.Parse(shopID.ToString()));
			int ProvinceID = modelShop.ShopProvince;
			string province = "";
			string city = "";
			string county = "";
			if (ProvinceID != 0)
			{
				province = PubFunction.SysAreaName(ProvinceID) + "省";
			}
			int CityID = modelShop.ShopCity;
			if (CityID != 0)
			{
				city = PubFunction.SysAreaName(CityID) + "市";
			}
			int CountyID = modelShop.ShopCounty;
			if (CountyID != 0)
			{
				county = PubFunction.SysAreaName(CountyID);
			}
			return province + city + county + modelShop.ShopAddress;
		}

		protected string BindShopName(object shopid)
		{
			Chain.BLL.SysShop Shop = new Chain.BLL.SysShop();
			string shopname = "";
			if (shopid != null)
			{
				if (shopid.ToString() != "")
				{
					shopname = Shop.GetShopNameByShopid(shopid.ToString());
				}
			}
			return shopname;
		}

		protected void btnShopSearch_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.BindShopList(this.GetSearch());
		}

		private void BindShopList(string strWhere)
		{
			Chain.BLL.SysShop Shop = new Chain.BLL.SysShop();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = " ShopID>0 and ShopType=3 ";
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
				this.gvShopList.DataSource = db;
				this.gvShopList.DataBind();
				PageBase.BindSerialRepeater(this.gvShopList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
			}
			else
			{
				this.gvShopList.Visible = false;
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
				strScarch += string.Format(" and (ShopName like '%{0}%' or ShopTelephone like '%{0}%' or ShopContactMan like '%{0}%') ", this.txtSearch.Text.Trim());
			}
			if (this.sltShopState.Value != "")
			{
				strScarch += string.Format(" and  ShopState={0}", this.sltShopState.Value);
			}
			return strScarch;
		}

		protected void gvShopList_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				HtmlAnchor hyShopPointEdit = (HtmlAnchor)e.Item.FindControl("hyShopPointEdit");
				HtmlAnchor hyShopSmsEdit = (HtmlAnchor)e.Item.FindControl("hyShopSmsEdit");
				if (this._UserShopID == 1)
				{
					string s = dr["IsMain"].ToString();
					if (dr["IsMain"].ToString() == "False")
					{
						hyShopPointEdit.Visible = false;
						hyShopPointEdit.Attributes.Add("style", "display:none");
					}
					hyShopSmsEdit.Visible = false;
					hyShopSmsEdit.Attributes.Add("style", "display:none");
				}
			}
		}
	}
}
