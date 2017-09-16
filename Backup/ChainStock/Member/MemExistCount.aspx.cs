using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class MemExistCount : PageBase
	{
		protected HtmlForm frmMemExistCount;

		protected Image imgTitle;

		protected Label lblFrmTitle;

		protected GridView gvMemCountList;

		private int intMemID = 0;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["MemID"] != "")
			{
				this.intMemID = int.Parse(base.Request["MemID"]);
				Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
				Chain.Model.Mem modelMem = new Chain.Model.Mem();
				modelMem = bllMem.GetModel(this.intMemID);
				if (modelMem.MemName != "")
				{
					Label expr_75 = this.lblFrmTitle;
					expr_75.Text = expr_75.Text + "---" + modelMem.MemName;
				}
			}
			this.BindMemCountList();
		}

		private void BindMemCountList()
		{
			if (this.intMemID != 0)
			{
				string strSql = " CountDetailMemID=" + this.intMemID;
				Chain.BLL.MemCountDetail bllMemCount = new Chain.BLL.MemCountDetail();
				DataTable db = bllMemCount.GetQueryMemCountList(strSql).Tables[0];
				this.gvMemCountList.DataSource = db;
				this.gvMemCountList.DataBind();
				PageBase.BindSerialGridView(this.gvMemCountList, false, 0);
				PageBase.BindNullSGridView(this.gvMemCountList);
			}
		}
	}
}
