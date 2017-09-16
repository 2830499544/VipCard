using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.StockManage
{
	public class GoodsAdd : PageBase
	{
		protected HtmlForm frmGoodsAdd;

		protected Literal ltlTitle;

		protected Repeater rptSyncShopList;

		protected HtmlInputText txtGoodsCode;

		protected HtmlInputHidden txtGoodsID;

		protected HtmlInputHidden txtCode;

		protected HtmlInputCheckBox chkService;

		protected HtmlGenericControl lblShowSync;

		protected HtmlInputCheckBox chkSyncOtherShop;

		protected HtmlGenericControl lblShowSyncPartial;

		protected HtmlInputCheckBox chkSyncPartialShop;

		protected HtmlInputText txtGoodsName;

		protected HtmlInputText txtGoodsNameCode;

		protected HtmlSelect sltGoodsClass;

		protected HtmlSelect sltjldw;

		protected HtmlInputText txtGoodsPrice;

		protected HtmlInputText txtGoodsBidPrice;

		protected HtmlInputText txtGoodsSalePercent;

		protected HtmlInputText txtGoodsMinPercent;

		protected HtmlTableRow trCommission;

		protected HtmlSelect sltCommissionType;

		protected HtmlInputText txtCommissionNumber;

		protected HtmlSelect sltShopList;

		protected HtmlInputHidden txtShopID;

		protected HtmlInputText txtGoodsPoint;

		protected HtmlTableRow trGoodsNumber;

		protected HtmlInputText txtGoodsNumber;

		protected HtmlInputHidden hdShopID;

		protected HtmlTextArea txtGoodsRemark;

		protected HtmlGenericControl tbCustomField;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.txtCode.Value = DateTime.Now.ToString("yyMMddHHmmss");
				PubFunction.BindGoodsClass(this.sltGoodsClass, this._UserShopID);
				this.sltGoodsClass.Value = "1";
				if (!string.IsNullOrEmpty(this.curParameter.strUnitList))
				{
					string[] strdwlist = this.curParameter.strUnitList.Split(new char[]
					{
						'|'
					});
					string[] array = strdwlist;
					for (int i = 0; i < array.Length; i++)
					{
						string strdw = array[i];
						this.sltjldw.Items.Add(new ListItem(strdw, strdw));
					}
				}
				else
				{
					this.sltjldw.Items.Add(new ListItem("个", "个"));
				}
				if (base.Request.QueryString["GoodsID"] != null)
				{
					this.GetGoodsInfo(int.Parse(base.Request.QueryString["GoodsID"]));
					this.GetGoodsNumber(int.Parse(base.Request.QueryString["GoodsID"]), int.Parse(base.Request.QueryString["ShopID"]));
					this.ltlTitle.Text = "主页   >   商品管理   >   编辑商品 ";
				}
				else
				{
					PubFunction.BindAddCustomFields(this.tbCustomField, "goods");
				}
				if (base.Request.QueryString["GoodsID"] != null)
				{
					Chain.Model.Goods goods = new Chain.BLL.Goods().GetModel(int.Parse(base.Request.QueryString["GoodsID"]));
					PubFunction.BindShopSelect(this._UserShopID, this.sltShopList, goods.CreateShopID, true);
					this.txtShopID.Value = goods.CreateShopID.ToString();
				}
				else
				{
					PubFunction.BindShopSelect(this._UserShopID, this.sltShopList, this._UserShopID, true);
					this.txtShopID.Value = this._UserShopID.ToString();
					this.hdShopID.Value = this._UserShopID.ToString();
				}
				this.bindSyncShopList();
				if (PubFunction.curParameter.bolStaff)
				{
					this.trCommission.Attributes.Add("style", "");
				}
				else
				{
					this.trCommission.Attributes.Add("style", "display:none");
				}
			}
		}

		protected void bindSyncShopList()
		{
			Chain.BLL.SysShop bllSS = new Chain.BLL.SysShop();
			string sqlStr = "ShopID>0 and ShopID<>" + this._UserShopID;
			sqlStr = PubFunction.GetShopAuthority(this._UserShopID, "ShopID", sqlStr);
			DataTable dt = bllSS.GetList(sqlStr).Tables[0];
			if (dt.Rows.Count > 0)
			{
				this.rptSyncShopList.DataSource = dt;
				this.rptSyncShopList.DataBind();
			}
			else
			{
				this.lblShowSync.Visible = false;
				this.lblShowSyncPartial.Visible = false;
			}
		}

		protected void GetGoodsNumber(int goodsID, int shopID)
		{
			this.trGoodsNumber.Attributes.CssStyle.Add("display", "");
			Chain.Model.GoodsNumber ModelNumber = new Chain.Model.GoodsNumber();
			Chain.BLL.GoodsNumber bllNumber = new Chain.BLL.GoodsNumber();
			DataTable dt = new DataTable();
			if (shopID != 0)
			{
				dt = bllNumber.GetList(string.Concat(new object[]
				{
					" GoodsID=",
					goodsID,
					" and ShopID=",
					shopID
				})).Tables[0];
			}
			else
			{
				dt = bllNumber.GetList(string.Concat(new object[]
				{
					" GoodsID=",
					goodsID,
					" and ShopID=",
					this._UserShopID
				})).Tables[0];
			}
			this.txtGoodsNumber.Value = dt.Rows[0]["Number"].ToString();
			this.hdShopID.Value = dt.Rows[0]["ShopID"].ToString();
		}

		protected void GetGoodsInfo(int goodsID)
		{
			Chain.BLL.Goods bllGoods = new Chain.BLL.Goods();
			Chain.Model.Goods modelGoods = bllGoods.GetModel(goodsID);
			DataTable dtGoods = bllGoods.GetItemAll(goodsID).Tables[0];
			this.txtGoodsID.Value = modelGoods.GoodsID.ToString();
			this.txtGoodsCode.Value = modelGoods.GoodsCode;
			this.txtCode.Value = modelGoods.GoodsCode;
			if (modelGoods.GoodsType == 1)
			{
				this.chkService.Checked = true;
			}
			this.txtGoodsName.Value = modelGoods.Name;
			this.txtGoodsNameCode.Value = modelGoods.NameCode;
			PubFunction.BindGoodsClass(this.sltGoodsClass, modelGoods.CreateShopID);
			this.sltGoodsClass.Value = modelGoods.GoodsClassID.ToString();
			this.sltjldw.Value = modelGoods.Unit;
			this.sltCommissionType.Value = modelGoods.CommissionType.ToString();
			this.txtCommissionNumber.Value = modelGoods.CommissionNumber.ToString();
			this.txtGoodsPrice.Value = Math.Round(modelGoods.Price, 2).ToString();
			if (modelGoods.Point != -1)
			{
				this.txtGoodsPoint.Value = modelGoods.Point.ToString();
			}
			else
			{
				this.txtGoodsPoint.Value = "";
			}
			this.txtGoodsBidPrice.Value = Math.Round(modelGoods.GoodsBidPrice, 2).ToString();
			this.txtGoodsMinPercent.Value = modelGoods.MinPercent.ToString();
			this.txtGoodsRemark.Value = modelGoods.GoodsRemark;
			this.txtGoodsSalePercent.Value = modelGoods.SalePercet.ToString();
			PubFunction.BindEditCustomFields(this.tbCustomField, "goods", dtGoods.Rows[0]);
			PubFunction.BindShopSelect(this._UserShopID, this.sltShopList, modelGoods.CreateShopID, this._UserShopID != 1);
			this.txtShopID.Value = modelGoods.CreateShopID.ToString();
			this.txtShopID.Value = modelGoods.CreateShopID.ToString();
		}
	}
}
