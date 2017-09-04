using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.StockManage
{
	public class GoodsClassDiscountList : PageBase
	{
		protected HtmlForm frmSetLevel;

		protected Image imgTitle;

		protected Label lblFrmTitle;

		protected HtmlInputText txtClassDiscountPercent;

		protected HtmlInputText txtClassPointPercent;

		protected HtmlInputSubmit btnSetGoodsClassDiscount;

		protected GridView gvMemLevel;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				string tempClassID = base.Request.QueryString["ClassID"];
				int classID = 0;
				if (!string.IsNullOrEmpty(tempClassID))
				{
					int.TryParse(tempClassID, out classID);
				}
				this.Bind(classID);
			}
		}

		protected void Bind(int classID)
		{
			this.gvMemLevel.DataSource = new GoodsClassDiscount().GetListByClassID(classID);
			this.gvMemLevel.DataBind();
			PageBase.BindSerialGridView(this.gvMemLevel, false, 0);
		}

		protected void gvMemLevel_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			int CellsCount = this.gvMemLevel.Columns.Count - 1;
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				DataRowView dr = (DataRowView)e.Row.DataItem;
				double DiscountPercent = double.Parse(dr["ClassDiscountPercent"].ToString()) * 100.0;
				double PointPercent = 0.0;
				if (dr["ClassPointPercent"].ToString() != "0")
				{
					PointPercent = 1.0 / double.Parse(dr["ClassPointPercent"].ToString());
				}
				PointPercent = Math.Round(PointPercent, 0);
				e.Row.Cells[3].Text = DiscountPercent.ToString() + '%';
				e.Row.Cells[4].Text = PointPercent.ToString();
				if (Convert.ToBoolean(dr["LevellLock"]))
				{
					e.Row.Cells[6].Text = "<span style='color: Red;'>等级锁定</span>";
				}
				else
				{
					e.Row.Cells[6].Text = "正常升级";
				}
			}
		}
	}
}
