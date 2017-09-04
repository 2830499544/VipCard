using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.business
{
	public class goodsConsumption : Page
	{
		private Chain.BLL.SysUser userBll = new Chain.BLL.SysUser();

		private Chain.BLL.GoodsClass goodsClassBll = new Chain.BLL.GoodsClass();

		private Chain.BLL.Goods goodsBll = new Chain.BLL.Goods();

		protected HtmlGenericControl spOrderAccount;

		protected HtmlInputHidden txtMemID;

		protected HtmlAnchor allclass;

		protected Repeater rptGoodsClass;

		protected Repeater rptGoodsList;

		protected HtmlAnchor moreGoods;

		protected HtmlInputHidden hidIsMem;

		protected HtmlInputHidden txtShopID;

		protected HtmlInputHidden hidUserID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["userid"] != null)
				{
					if (this.Session["userid"].ToString() != "")
					{
						Chain.Model.SysUser userModel = this.userBll.GetModel(int.Parse(this.Session["userid"].ToString()));
						Chain.Model.SysShop shopModel = new Chain.BLL.SysShop().GetModel(userModel.UserShopID);
						this.spOrderAccount.InnerText = "wx" + PubFunction.curParameter.strGoodsExpensePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
						this.txtShopID.Value = shopModel.ShopID.ToString();
						this.hidUserID.Value = userModel.UserID.ToString();
						this.rptClassBind(shopModel.ShopID);
						this.prtGoodsBind(shopModel.ShopID);
					}
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		private void rptClassBind(int ShopID)
		{
			DataSet dsGoodsClass = this.goodsClassBll.GetListByShopID(ShopID);
			int count = dsGoodsClass.Tables[0].Rows.Count;
			this.rptGoodsClass.DataSource = dsGoodsClass.Tables[0];
			this.rptGoodsClass.DataBind();
		}

		private void prtGoodsBind(int shopID)
		{
			string strWhere = " 1=1  and GoodsNumber.Number>0 ";
			strWhere = strWhere + " and Goods.GoodsClassID = GoodsClass.ClassID and Goods.GoodsID = GoodsNumber.GoodsID and GoodsNumber.ShopID=" + shopID;
			int resCount = 0;
			if (base.Request.QueryString["classID"] != null)
			{
				string classID = base.Request.QueryString["classID"].ToString();
				strWhere += string.Format(" and (GoodsClass.ClassID={0} or GoodsClass.ParentID={0}) ", classID);
			}
			DataTable dtGoodsList;
			if (base.Request.QueryString["type"] == "all")
			{
				dtGoodsList = this.goodsBll.GetListSP(10000, 1, out resCount, new string[]
				{
					strWhere
				}).Tables[0];
			}
			else
			{
				dtGoodsList = this.goodsBll.GetListSP(100, 1, out resCount, new string[]
				{
					strWhere
				}).Tables[0];
			}
			if (dtGoodsList.Rows.Count > 0)
			{
				this.rptGoodsList.DataSource = dtGoodsList;
				this.rptGoodsList.DataBind();
			}
			if (dtGoodsList.Rows.Count < 10 || base.Request.QueryString["type"] == "all")
			{
				this.moreGoods.Attributes.Add("style", "display:none");
			}
		}

		protected string GetGoodsPrice(string Price, string SalePercet, string MinPercent)
		{
			decimal GoodsPrice = 0m;
			if (Price != "0")
			{
				if (SalePercet != "0")
				{
					GoodsPrice = decimal.Parse(Price) * decimal.Parse(SalePercet);
				}
				else if (MinPercent != "0")
				{
					GoodsPrice = decimal.Parse(MinPercent) * decimal.Parse(Price);
				}
				else
				{
					GoodsPrice = decimal.Parse(Price);
				}
			}
			return GoodsPrice.ToString("0.00");
		}

		protected void rptGoodsClass_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				HtmlControl classname = (HtmlControl)e.Item.FindControl("classname");
				this.allclass.Attributes.Remove("class");
				if (base.Request.QueryString["ClassID"] != null)
				{
					if (dr["ClassID"].ToString() == base.Request.QueryString["ClassID"])
					{
						classname.Attributes.Add("class", "active");
					}
					else
					{
						classname.Attributes.Remove("class");
					}
				}
				else
				{
					this.allclass.Attributes.Add("class", "active");
				}
			}
		}
	}
}
