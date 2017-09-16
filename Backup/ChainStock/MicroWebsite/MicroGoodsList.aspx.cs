using Chain.BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.MicroWebsite
{
	public class MicroGoodsList : PageBase
	{
		protected HtmlForm frmGoodsList;

		protected Literal ltlTitle;

		protected Button btnOut;

		protected HtmlInputText txtQueryGoods;

		protected HtmlSelect sltGoodsPriceSymbols;

		protected HtmlInputText txtGoodsPrice;

		protected HtmlSelect sltMicroSalePriceSymbols;

		protected HtmlInputText txtMicroSalePrice;

		protected HtmlSelect sltGoodsClass;

		protected Button btnGoodsListQuery;

		protected Repeater gvGoodsList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindMicroGoodsClass(this.sltGoodsClass, 1);
				this.GetGoodsList(this.QueryCondition());
			}
		}

		private void GetGoodsList(string strSql)
		{
			MicroWebsiteGoods bllGoods = new MicroWebsiteGoods();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " and MicroWebsiteGoods.MicroGoodsClassID = MicroWebsiteGoodsClass.MicroGoodsClassID";
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
			string strGoodsClass = this.sltGoodsClass.Value;
			string strMicroSalePriceSymbols = this.sltMicroSalePriceSymbols.Value;
			string strtxtMicroSalePrice = PubFunction.RemoveSpace(this.txtMicroSalePrice.Value);
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" 1=1 ");
			if (strQueryGoods != "")
			{
				strSql.AppendFormat("and (MicroGoodsName like '%{0}%' or MicroGoodsCode like '%{0}%')", strQueryGoods);
			}
			if (strGoodsPrice != "")
			{
				strSql.AppendFormat(" and MicroPrice" + strPriceSymbols + "{0} ", decimal.Parse(strGoodsPrice));
			}
			if (strtxtMicroSalePrice != "")
			{
				strSql.AppendFormat(" and MicroSalePrice" + strMicroSalePriceSymbols + "{0} ", decimal.Parse(strtxtMicroSalePrice));
			}
			if (strGoodsClass != "")
			{
				strSql.AppendFormat(" and MicroGoodsClassID.MicroGoodsClassID={0}", strGoodsClass);
			}
			return strSql.ToString();
		}

		protected void btnGoodsListQuery_Click(object sender, EventArgs e)
		{
			string strGoodsPrice = PubFunction.RemoveSpace(this.txtGoodsPrice.Value);
			string strSalePrice = PubFunction.RemoveSpace(this.txtMicroSalePrice.Value);
			if (strGoodsPrice != "")
			{
				try
				{
					if (decimal.Parse(strGoodsPrice) < 0m)
					{
						base.OutputWarn("用于查询的商品售价输入只能大于0");
						return;
					}
				}
				catch
				{
					base.OutputWarn("用于查询的商品售价输入只能为数字");
					return;
				}
			}
			if (strSalePrice != "")
			{
				try
				{
					if (decimal.Parse(strSalePrice) < 0m)
					{
						base.OutputWarn("用于查询的商品原价输入只能大于0");
						return;
					}
				}
				catch
				{
					base.OutputWarn("用于查询的商品原价输入只能为数字");
					return;
				}
			}
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.GetGoodsList(this.QueryCondition());
		}

		protected void btnOut_Click(object sender, EventArgs e)
		{
			MicroWebsiteGoods bllGoods = new MicroWebsiteGoods();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += " and MicroWebsiteGoods.MicroGoodsClassID = MicroWebsiteGoodsClass.MicroGoodsClassID";
			DataTable dtGoods = bllGoods.GetListSP(100000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.MicroGoodsListExcel(dtGoods, this._UserName);
		}

		public void BindMicroGoodsClass(HtmlSelect select, int ShopID)
		{
			select.Items.Clear();
			select.Items.Add(new ListItem("===== 请选择 =====", ""));
			DataTable dtGoodsClass = new MicroWebsiteGoodsClass().GetList(string.Format("MicroGoodsClassShopID={0}", ShopID)).Tables[0];
			foreach (DataRow dr in dtGoodsClass.Rows)
			{
				select.Items.Add(new ListItem(dr["MicroGoodsClassName"].ToString(), dr["MicroGoodsClassID"].ToString()));
			}
		}
	}
}
