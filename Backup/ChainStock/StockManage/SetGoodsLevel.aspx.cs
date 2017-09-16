using Chain.BLL;
using ChainStock.Controls;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.StockManage
{
	public class SetGoodsLevel : PageBase
	{
		private int _curShopID;

		protected HtmlForm frmGoodsClass;

		protected HtmlInputHidden share;

		protected Literal ltlTitle;

		protected HtmlInputButton Button1;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected Button btnGoodsClassQuery;

		protected Repeater rpGoodsClass;

		protected QuickSearch QuickSearch1;

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
				this.BindGoodsClassList(this.CurShopID);
				this.share.Value = PubFunction.GetControlVisit(PubFunction.GetUserAuthority(this._UserGroupID), 120, 73).ToString().ToLower();
			}
		}

		protected void BindGoodsClassList(int ShopID)
		{
			PubFunction.BindShopSelect(this._UserShopID, this.sltShop, ShopID, this._UserShopID != 1);
			GoodsClass gdClass = new GoodsClass();
			DataTable dtClass = gdClass.GetListByShopID(ShopID).Tables[0];
			DataTable dtResult = this.GetTreeList(dtClass);
			this.rpGoodsClass.DataSource = dtResult;
			this.rpGoodsClass.DataBind();
		}

		protected DataTable GetTreeList(DataTable dtSource)
		{
			DataTable dt = new DataTable();
			for (int i = 0; i < dtSource.Columns.Count; i++)
			{
				dt.Columns.Add(new DataColumn(dtSource.Columns[i].ColumnName));
			}
			DataRow[] dr = dtSource.Select(" ParentID=0");
			for (int i = 0; i < dr.Length; i++)
			{
				dt.Rows.Add(dt.NewRow());
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					dt.Rows[dt.Rows.Count - 1][j] = dr[i][j].ToString();
				}
				string strClassID = dr[i]["ClassID"].ToString();
				this.CreateTreeItem(dtSource, dt, strClassID, 1);
			}
			return dt;
		}

		protected void CreateTreeItem(DataTable dtSource, DataTable dt, string strClassID, int level)
		{
			DataRow[] dr = dtSource.Select(" ParentID=" + strClassID);
			for (int i = 0; i < dr.Length; i++)
			{
				dt.Rows.Add(dt.NewRow());
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					string temp = dr[i][j].ToString();
					if (dt.Columns[j].ColumnName == "ClassName")
					{
						temp = new string('-', level * 4).ToString() + temp;
					}
					dt.Rows[dt.Rows.Count - 1][j] = temp;
				}
				string strCurrentID = dr[i]["ClassID"].ToString();
				this.CreateTreeItem(dtSource, dt, strCurrentID, level + 1);
			}
		}

		protected void btnGoodsClassQuery_Click(object sender, EventArgs e)
		{
			this.BindGoodsClassList(int.Parse(this.sltShop.Value));
		}
	}
}
