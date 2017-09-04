using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.PointManage
{
	public class SetGiftLevel : PageBase
	{
		protected HtmlForm frmGiftLevel;

		protected Literal ltlTitle;

		protected HtmlInputButton Button1;

		protected HtmlSelect sltGiftClass;

		protected HtmlInputText txtClassName;

		protected HtmlTextArea txtGiftClassRemark;

		protected Repeater rpGiftClass;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.bindSlt();
				this.BindGiftClassList();
			}
		}

		private void bindSlt()
		{
			GiftClass gfClass = new GiftClass();
			DataTable dtClass = gfClass.GetList(" GiftParentID=0").Tables[0];
			this.sltGiftClass.Items.Add(new ListItem("根类别", "0"));
			foreach (DataRow dr in dtClass.Rows)
			{
				this.sltGiftClass.Items.Add(new ListItem(dr["GiftClassName"].ToString(), dr["GiftClassID"].ToString()));
			}
		}

		protected void BindGiftClassList()
		{
			GiftClass gfClass = new GiftClass();
			DataTable dtClass = gfClass.GetList("").Tables[0];
			DataTable dtResult = this.GetTreeList(dtClass);
			this.rpGiftClass.DataSource = dtResult;
			this.rpGiftClass.DataBind();
		}

		protected DataTable GetTreeList(DataTable dtSource)
		{
			DataTable dt = new DataTable();
			for (int i = 0; i < dtSource.Columns.Count; i++)
			{
				dt.Columns.Add(new DataColumn(dtSource.Columns[i].ColumnName));
			}
			DataRow[] dr = dtSource.Select(" GiftParentID=0");
			for (int i = 0; i < dr.Length; i++)
			{
				dt.Rows.Add(dt.NewRow());
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					dt.Rows[dt.Rows.Count - 1][j] = dr[i][j].ToString();
				}
				string strClassID = dr[i]["GiftClassID"].ToString();
				this.CreateTreeItem(dtSource, dt, strClassID, 1);
			}
			return dt;
		}

		protected void CreateTreeItem(DataTable dtSource, DataTable dt, string strClassID, int level)
		{
			DataRow[] dr = dtSource.Select(" GiftParentID=" + strClassID);
			for (int i = 0; i < dr.Length; i++)
			{
				dt.Rows.Add(dt.NewRow());
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					string temp = dr[i][j].ToString();
					dt.Rows[dt.Rows.Count - 1][j] = temp;
				}
				string strCurrentID = dr[i]["GiftClassID"].ToString();
				this.CreateTreeItem(dtSource, dt, strCurrentID, level + 1);
			}
		}

		protected bool IsShow(string strControl, int module)
		{
			return PubFunction.GetControlVisit(PubFunction.GetGroupAuthority(this._UserGroupID), strControl, module);
		}
	}
}
