using Chain.BLL;
using Chain.Model;
using ChainStock.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.StockManage
{
	public class GoodsList : PageBase
	{
		private int _curShopID;

		protected HtmlForm frmGoodsList;

		protected Literal ltlTitle;

		protected Button btnOut;

		protected Button btnCopy;

		protected HtmlInputText txtQueryGoods;

		protected HtmlSelect sltCustomField;

		protected HtmlInputText txtCustomField;

		protected HtmlInputHidden txtGoodsClass;

		protected HtmlSelect sltGoodsType;

		protected HtmlSelect sltGoodsPriceSymbols;

		protected HtmlInputText txtGoodsPrice;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected Button btnGoodsListQuery;

		protected Literal ltlHeader;

		protected Repeater gvGoodsList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch2;

		public int CurShopID
		{
			get
			{
				if (this._curShopID == 0)
				{
					this._curShopID = this._UserShopID;
					if (!string.IsNullOrEmpty(this.sltShop.Value))
					{
						this._curShopID = int.Parse(this.sltShop.Value);
					}
				}
				return this._curShopID;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindCustomFieldSelect(this._UserShopID, this.sltCustomField, true, 2);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, this._UserShopID, this._UserShopID != 1);
				this.GetGoodsList(this.QueryCondition());
				Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(this._UserShopID);
				int count = new Chain.BLL.SysShop().GetRecordCount("ShopID>0 and ShopType=3 and IsMain=0");
				if (modelShop.IsMain && count > 0)
				{
					this.btnCopy.Visible = true;
				}
				else
				{
					this.btnCopy.Visible = false;
				}
			}
		}

		private void GetGoodsList(string strSql)
		{
			Chain.BLL.Goods bllGoods = new Chain.BLL.Goods();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql = strSql + " and Goods.GoodsClassID = GoodsClass.ClassID and Goods.GoodsID = GoodsNumber.GoodsID and GoodsNumber.ShopID=" + this.sltShop.Value;
			DataTable dtGoods = bllGoods.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvGoodsList.DataSource = dtGoods;
			this.gvGoodsList.DataBind();
			PageBase.BindSerialRepeater(this.gvGoodsList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
			Chain.BLL.MemCustomField bllCustom = new Chain.BLL.MemCustomField();
			List<Chain.Model.MemCustomField> fieldlist = bllCustom.GetModelList("CustomType=2 and CustomFieldIsShow=1");
			if (fieldlist.Count > 0)
			{
				StringBuilder strHeader = new StringBuilder();
				StringBuilder strHtml = new StringBuilder();
				for (int i = 0; i < this.gvGoodsList.Items.Count; i++)
				{
					Literal ltlGoodsID = (Literal)this.gvGoodsList.Items[i].FindControl("ltlGoodsID");
					Literal ltlHtml = (Literal)this.gvGoodsList.Items[i].FindControl("ltlHtml");
					int GoodsID = Convert.ToInt32(ltlGoodsID.Text);
					DataRow[] drGoods = dtGoods.Select(string.Format(" GoodsID = {0}", GoodsID));
					strHtml.Length = 0;
					foreach (Chain.Model.MemCustomField mdCustomField in fieldlist)
					{
						if (i == 0)
						{
							strHeader.AppendFormat("<th>{0}</th>", mdCustomField.CustomFieldName);
						}
						strHtml.AppendFormat("<td>{0}</td>", drGoods[0][mdCustomField.CustomField]);
					}
					ltlHtml.Text = strHtml.ToString();
				}
				this.ltlHeader.Text = strHeader.ToString();
			}
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.GetGoodsList(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.GetGoodsList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strQueryGoods = this.txtQueryGoods.Value;
			string strPriceSymbols = this.sltGoodsPriceSymbols.Value;
			string strGoodsPrice = PubFunction.RemoveSpace(this.txtGoodsPrice.Value);
			string strGoodsClass = (base.Request["sltGoodsClass"] == null) ? "" : base.Request["sltGoodsClass"].Trim();
			this.txtGoodsClass.Value = strGoodsClass;
			string strCustom = this.sltCustomField.Value;
			string strCustomField = this.txtCustomField.Value;
			string strGoodsType = this.sltGoodsType.Value;
			string strShopId = this.sltShop.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" Goods.GoodsID >0 ");
			if (strQueryGoods != "")
			{
				strSql.AppendFormat("and (Name like '%{0}%' or NameCode like '%{0}%' or GoodsCode like '%{0}%')", strQueryGoods);
			}
			if (strGoodsPrice != "")
			{
				strSql.AppendFormat(" and Price" + strPriceSymbols + "{0} ", decimal.Parse(strGoodsPrice));
			}
			if (strCustom != "")
			{
				strSql.AppendFormat(" and {0} = '{1}'", strCustom, strCustomField);
			}
			if (strGoodsType != "")
			{
				strSql.AppendFormat(" and GoodsType = {0}", int.Parse(strGoodsType));
			}
			if (strGoodsClass != "")
			{
				Chain.Model.GoodsClass model = new Chain.BLL.GoodsClass().GetModel(int.Parse(strGoodsClass));
				if (model.ParentID == 0)
				{
					string strClass = int.Parse(strGoodsClass) + ",";
					DataTable dt = new Chain.BLL.GoodsClass().GetList("ParentID=" + int.Parse(strGoodsClass)).Tables[0];
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						strClass = strClass + dt.Rows[i]["ClassID"] + ",";
					}
					int length = strClass.Length;
					strClass = strClass.Substring(0, length - 1);
					strSql.AppendFormat(" and GoodsClassID in ({0})", strClass);
				}
				else
				{
					strSql.AppendFormat(" and GoodsClassID={0}", int.Parse(strGoodsClass));
				}
			}
			if (strShopId != "")
			{
				strSql.AppendFormat(" and GoodsNumber.ShopID={0}", int.Parse(strShopId));
			}
			return strSql.ToString();
		}

		protected void btnGoodsListQuery_Click(object sender, EventArgs e)
		{
			string strGoodsPrice = PubFunction.RemoveSpace(this.txtGoodsPrice.Value);
			if (strGoodsPrice != "")
			{
				try
				{
					if (decimal.Parse(strGoodsPrice) < 0m)
					{
						base.OutputWarn("用于查询的商品单价输入只能大于0");
						return;
					}
				}
				catch
				{
					base.OutputWarn("用于查询的商品单价输入只能为数字");
					return;
				}
			}
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.GetGoodsList(this.QueryCondition());
		}

		protected void btnOut_Click(object sender, EventArgs e)
		{
			Chain.BLL.Goods bllGoods = new Chain.BLL.Goods();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += " and Goods.GoodsClassID = GoodsClass.ClassID and Goods.GoodsID = GoodsNumber.GoodsID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "ShopID", strSql);
			DataTable dtGoods = bllGoods.GetListSP(100000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.GoodsListExcel(dtGoods, this._UserName);
		}

		protected void btnCopy_Click(object sender, EventArgs e)
		{
			Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
			Chain.BLL.GoodsClassAuthority bllGoodsClassAuthority = new Chain.BLL.GoodsClassAuthority();
			Chain.BLL.GoodsClass bllGoodsClass = new Chain.BLL.GoodsClass();
			DataTable dtShop = bllShop.GetList("ShopID>0 and ShopType=3 and IsMain=0 ").Tables[0];
			DataTable dtGoodsClass = bllGoodsClass.GetList("CreateShopID=" + this._UserShopID).Tables[0];
			for (int i = 0; i < dtShop.Rows.Count; i++)
			{
				int ShopID = int.Parse(dtShop.Rows[i]["ShopID"].ToString());
				for (int j = 0; j < dtGoodsClass.Rows.Count; j++)
				{
					int ClassID = int.Parse(dtGoodsClass.Rows[j]["ClassID"].ToString());
					Chain.Model.GoodsClassAuthority modelGoodsClassAuthority = new Chain.Model.GoodsClassAuthority();
					modelGoodsClassAuthority.ClassID = ClassID;
					modelGoodsClassAuthority.ShopID = ShopID;
					int count = bllGoodsClassAuthority.GetRecordCount(string.Concat(new object[]
					{
						"ShopID=",
						ShopID,
						" and ClassID=",
						ClassID
					}));
					if (count == 0)
					{
						bllGoodsClassAuthority.Add(modelGoodsClassAuthority);
					}
				}
			}
			Chain.BLL.GoodsClassDiscount bllGoodsClassDiscount = new Chain.BLL.GoodsClassDiscount();
			DataTable dtClassDiscount = bllGoodsClassDiscount.GetList("DiscountShopID=" + this._UserShopID).Tables[0];
			for (int i = 0; i < dtShop.Rows.Count; i++)
			{
				int ShopID = int.Parse(dtShop.Rows[i]["ShopID"].ToString());
				for (int j = 0; j < dtClassDiscount.Rows.Count; j++)
				{
					Chain.Model.GoodsClassDiscount modelGoodsClassDiscount = new Chain.Model.GoodsClassDiscount();
					int ClassID = int.Parse(dtClassDiscount.Rows[j]["GoodsClassID"].ToString());
					int MemLevelID = int.Parse(dtClassDiscount.Rows[j]["MemLevelID"].ToString());
					modelGoodsClassDiscount.GoodsClassID = ClassID;
					modelGoodsClassDiscount.MemLevelID = MemLevelID;
					modelGoodsClassDiscount.DiscountShopID = ShopID;
					modelGoodsClassDiscount.ClassDiscountPercent = int.Parse(dtClassDiscount.Rows[j]["ClassDiscountPercent"].ToString());
					modelGoodsClassDiscount.ClassPointPercent = int.Parse(dtClassDiscount.Rows[j]["ClassPointPercent"].ToString());
					DataTable dtShopGoodsClassDiscount = bllGoodsClassDiscount.GetList(string.Concat(new object[]
					{
						"MemLevelID=",
						MemLevelID,
						" and DiscountShopID=",
						ShopID,
						" and GoodsClassID=",
						ClassID
					})).Tables[0];
					if (dtShopGoodsClassDiscount.Rows.Count == 0)
					{
						bllGoodsClassDiscount.Add(modelGoodsClassDiscount);
					}
					else
					{
						modelGoodsClassDiscount.ClassDiscountID = int.Parse(dtShopGoodsClassDiscount.Rows[0]["ClassDiscountID"].ToString());
						bllGoodsClassDiscount.Update(modelGoodsClassDiscount);
					}
				}
			}
			Chain.BLL.Goods bllGoods = new Chain.BLL.Goods();
			DataTable dtGoods = bllGoods.GetGoodsList("CreateShopID=" + this._UserShopID).Tables[0];
			Chain.BLL.GoodsNumber bllGoodsNumber = new Chain.BLL.GoodsNumber();
			for (int i = 0; i < dtShop.Rows.Count; i++)
			{
				int ShopID = int.Parse(dtShop.Rows[i]["ShopID"].ToString());
				for (int j = 0; j < dtGoods.Rows.Count; j++)
				{
					int GoodsID = int.Parse(dtGoods.Rows[j]["GoodsID"].ToString());
					int count = bllGoodsNumber.GetRecordCount(string.Concat(new object[]
					{
						"GoodsID=",
						GoodsID,
						" and ShopID=",
						ShopID
					}));
					Chain.Model.GoodsNumber modelGoodsNumber = new Chain.Model.GoodsNumber();
					modelGoodsNumber.GoodsID = GoodsID;
					modelGoodsNumber.Number = 0m;
					modelGoodsNumber.ShopID = ShopID;
					if (count == 0)
					{
						bllGoodsNumber.Add(modelGoodsNumber);
					}
				}
			}
			this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<Script Language='JavaScript' defer>art.dialog({title: '系统提示',time: 2,content:'同步成功',close: function () { location.href = 'GoodsList.aspx?PID=62';  }});</script>");
		}
	}
}
