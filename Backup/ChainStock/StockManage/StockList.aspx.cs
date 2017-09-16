using Chain.BLL;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.StockManage
{
	public class StockList : PageBase
	{
		private GoodsClass bllGoodsClass = new GoodsClass();

		private Goods bllGoods = new Goods();

		protected HtmlHead Head1;

		protected HtmlForm frmStaffList;

		protected HtmlInputHidden otherStock;

		protected Label lbValue;

		protected Literal ltlTitle;

		protected HtmlInputText txtQuery;

		protected Button btnUserSearch;

		protected TreeView tvGoodsClass;

		protected Repeater rptStockList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected HtmlInputHidden shopID;

		public string GoodsClassID
		{
			get
			{
				return (this.lbValue.Attributes["GoodsClassID"] == null) ? "" : this.lbValue.Attributes["GoodsClassID"].ToString();
			}
			set
			{
				if (this.lbValue.Attributes["GoodsClassID"] == null)
				{
					this.lbValue.Attributes.Add("GoodsClassID", value);
				}
				else
				{
					this.lbValue.Attributes["GoodsClassID"] = value;
				}
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				bool hasAuthority = PubFunction.GetControlVisit(PubFunction.GetGroupAuthority(this._UserGroupID), "StockList_aStock", 65);
				this.otherStock.Value = (hasAuthority ? "1" : "0");
				this.bindtree();
				this.Get_ParameterList(this.QueryCondition());
			}
		}

		private void Get_ParameterList(string strSql)
		{
			GoodsNumber bllGoodsNumber = new GoodsNumber();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += string.Format("and GoodsType=0 and ShopID = '{0}'", this._UserShopID);
			DataTable db = this.bllGoods.GetGoodsList(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.rptStockList.DataSource = db;
			this.rptStockList.DataBind();
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			string intGoodsClass = this.tvGoodsClass.SelectedValue;
			string strSql = this.QueryCondition();
			if (intGoodsClass != "0" && intGoodsClass != "")
			{
				strSql += string.Format(" and GoodsClassID in ({0}) ", PubFunction.GetClassID(Convert.ToInt32(intGoodsClass)));
			}
			this.Get_ParameterList(strSql);
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			string intGoodsClass = this.tvGoodsClass.SelectedValue;
			string strSql = this.QueryCondition();
			if (intGoodsClass != "0" && intGoodsClass != "")
			{
				strSql += string.Format(" and GoodsClassID in ({0}) ", PubFunction.GetClassID(Convert.ToInt32(intGoodsClass)));
			}
			this.Get_ParameterList(strSql);
		}

		public void bindtree()
		{
			TreeNode tmpNode = new TreeNode();
			tmpNode.Value = "0";
			tmpNode.Text = "<b style='font-size:14px;color:#303030;font-weight:bolder;'>所有分类</b>";
			this.tvGoodsClass.Nodes.Add(tmpNode);
			DataTable dtgdclass = new DataTable();
			dtgdclass = this.bllGoodsClass.GetListByShopID(this._UserShopID).Tables[0];
			this.bingsonnodes(dtgdclass, "0", tmpNode);
		}

		public void bingsonnodes(DataTable dtnode, string ParentID, TreeNode tNode)
		{
			DataRow[] drnode = dtnode.Select(string.Format(" ParentID = '{0}'", ParentID));
			DataRow[] array = drnode;
			for (int i = 0; i < array.Length; i++)
			{
				DataRow dt = array[i];
				TreeNode tmsonnode = new TreeNode();
				tmsonnode.Value = dt["ClassID"].ToString();
				tmsonnode.Text = dt["ClassName"].ToString();
				tNode.ChildNodes.Add(tmsonnode);
				this.bingsonnodes(dtnode, dt["ClassID"].ToString(), tmsonnode);
			}
		}

		protected string QueryCondition()
		{
			string goodcode = this.txtQuery.Value.Trim();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1");
			if (goodcode != "")
			{
				strSql.AppendFormat(" and (GoodsCode = '{0}' or Name = '{1}' or NameCode = '{2}')", goodcode, goodcode, goodcode);
			}
			if (this.GoodsClassID != "" && this.GoodsClassID != "0")
			{
				strSql.AppendFormat(" and GoodsClassID in ({0}) ", PubFunction.GetClassID(Convert.ToInt32(this.GoodsClassID)));
			}
			return strSql.ToString();
		}

		protected void btnUserSearch_Click(object sender, EventArgs e)
		{
			this.tvGoodsClass.Nodes[0].Select();
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void tvGoodsClass_SelectedNodeChanged(object sender, EventArgs e)
		{
			string intGoodsClass = this.tvGoodsClass.SelectedValue;
			string strSql = this.QueryCondition();
			if (intGoodsClass != "0" && intGoodsClass != "")
			{
				strSql += string.Format(" and GoodsClassID in ({0}) ", PubFunction.GetClassID(Convert.ToInt32(intGoodsClass)));
			}
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.Get_ParameterList(strSql);
		}
	}
}
