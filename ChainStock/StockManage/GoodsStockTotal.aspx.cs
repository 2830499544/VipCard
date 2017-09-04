using Chain.BLL;
using ChainStock.Controls;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.StockManage
{
	public class GoodsStockTotal : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmPointRankList;

		protected Literal ltlTitle;

		protected Button btnOut;

		protected HtmlInputText txtQuery;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected HtmlSelect sltGoodsClass;

		protected Button btnStockQuery;

		protected Repeater gvPointRankList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, this._UserShopID, this._UserShopID != 1);
				PubFunction.BindGoodsClass(this.sltGoodsClass, this._UserShopID);
				this.Get_ParameterList(this.QueryCondition());
			}
		}

		private void Get_ParameterList(string strSql)
		{
			GoodsNumber bllGoodsNumber = new GoodsNumber();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "ShopID", strSql);
			DataTable db = bllGoodsNumber.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvPointRankList.DataSource = db;
			this.gvPointRankList.DataBind();
		}

		protected void btnStockQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.Get_ParameterList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string spbm = this.txtQuery.Value.Trim();
			string shopid = this.sltShop.Value;
			string intGoodsClass = this.sltGoodsClass.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1 and GoodsType=0 ");
			if (spbm != "")
			{
				strSql.AppendFormat(" and (GoodsCode = '{0}' or Name = '{1}' or NameCode = '{2}')", spbm, spbm, spbm);
			}
			if (shopid != "")
			{
				strSql.AppendFormat(" and ShopID = '{0}'", shopid);
			}
			if (intGoodsClass != "")
			{
				strSql.AppendFormat(" and GoodsClassID in ({0}) ", PubFunction.GetClassID(Convert.ToInt32(intGoodsClass)));
			}
			return strSql.ToString();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void btnOut_Click(object sender, EventArgs e)
		{
			string strSql = this.QueryCondition();
			GoodsNumber bllGoodsNumber = new GoodsNumber();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "ShopID", strSql);
			DataTable db = bllGoodsNumber.GetListSP(100000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.GoodsStockTotalExcel(db, this._UserName);
		}
	}
}
