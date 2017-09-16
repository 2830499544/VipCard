using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class TimingCategory : PageBase
	{
		private Chain.BLL.TimingCategory bllTimingCategory = new Chain.BLL.TimingCategory();

		protected HtmlForm form1;

		protected HtmlSelect sltCategoryFather;

		protected HtmlTextArea txtCategoryrRemark;

		protected Literal ltlTitle;

		protected HtmlInputButton btnTimingCategory;

		protected Repeater gvTimingCategory;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				DataTable dtTimingCategory = this.bllTimingCategory.GetNifo(string.Format("CategoryShopID = {0}", this._UserShopID)).Tables[0];
				this.GetMemList(dtTimingCategory);
				this.bindSlt(dtTimingCategory.Select("CategoryFatherID = 0"));
			}
		}

		private void bindSlt(DataRow[] drs)
		{
			this.sltCategoryFather.Items.Add(new ListItem("根类别", "0"));
			for (int i = 0; i < drs.Length; i++)
			{
				DataRow dr = drs[i];
				this.sltCategoryFather.Items.Add(new ListItem(dr["CategoryName"].ToString(), dr["CategoryID"].ToString()));
			}
		}

		private void GetMemList(DataTable dtTimingCategory)
		{
			DataTable dtTree = this.GetTreeList(dtTimingCategory);
			this.gvTimingCategory.DataSource = dtTree;
			this.gvTimingCategory.DataBind();
		}

		protected DataTable GetTreeList(DataTable dtSource)
		{
			DataTable dt = new DataTable();
			for (int i = 0; i < dtSource.Columns.Count; i++)
			{
				dt.Columns.Add(new DataColumn(dtSource.Columns[i].ColumnName));
			}
			DataRow[] dr = dtSource.Select(" CategoryFatherID=0");
			for (int i = 0; i < dr.Length; i++)
			{
				dt.Rows.Add(dt.NewRow());
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					dt.Rows[dt.Rows.Count - 1][j] = dr[i][j].ToString();
				}
				string strClassID = dr[i]["CategoryID"].ToString();
				this.CreateTreeItem(dtSource, dt, strClassID, 1);
			}
			return dt;
		}

		protected void CreateTreeItem(DataTable dtSource, DataTable dt, string strClassID, int level)
		{
			DataRow[] dr = dtSource.Select(" CategoryFatherID=" + strClassID);
			for (int i = 0; i < dr.Length; i++)
			{
				dt.Rows.Add(dt.NewRow());
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					string temp = dr[i][j].ToString();
					if (dt.Columns[j].ColumnName == "CategoryName")
					{
						temp = new string('-', level * 4).ToString() + temp;
					}
					dt.Rows[dt.Rows.Count - 1][j] = temp;
				}
				string strCurrentID = dr[i]["CategoryID"].ToString();
				this.CreateTreeItem(dtSource, dt, strCurrentID, level + 1);
			}
		}
	}
}
