using Chain.BLL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.PointManage
{
	public class RptCustomerRecommend : Page
	{
		protected HtmlHead Head1;

		protected HtmlForm form1;

		protected TreeView TreeView1;

		protected Label lblMemCard;

		protected Label lblMemName;

		protected Label lblListin;

		protected Label lblListout;

		protected Repeater rptPointDetails;

		protected HiddenField hidLevel;

		private Chain.BLL.PointLog bll = new Chain.BLL.PointLog();

		private Chain.BLL.PointRate bllPointRate = new Chain.BLL.PointRate();

		private Chain.BLL.Mem MemRecommend = new Chain.BLL.Mem();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.GetCustomerRecommend();
			}
		}

		public void GetCustomerRecommend()
		{
			this.hidLevel.Value = base.Request["level"].ToString();
			this.TreeView1.Nodes.Clear();
			Chain.Model.Mem model = this.MemRecommend.GetModelByMemCard(base.Request["MemCard"]);
			TreeNode tr = new TreeNode("[顶层会员]" + model.MemName, model.MemID.ToString());
			tr.ToolTip = model.MemCard;
			this.GetSubRecommend(model.MemID, ref tr);
			this.TreeView1.Nodes.Add(tr);
			this.TreeView1.ExpandAll();
			this.TreeView1.Nodes[0].Select();
			this.TreeView1_SelectedNodeChanged(null, null);
		}

		public void GetSubRecommend(int MemRecommendID, ref TreeNode trn)
		{
			DataTable db = this.MemRecommend.getMemRecommendList(MemRecommendID);
			foreach (DataRow dr in db.Rows)
			{
				TreeNode trnm = new TreeNode(dr["MemName"].ToString(), dr["MemID"].ToString());
				trnm.ToolTip = dr["MemCard"].ToString();
				trn.ChildNodes.Add(trnm);
				trnm.Text = "[" + trnm.Depth.ToString() + "层会员]" + trnm.Text;
				if (trnm.Depth < int.Parse(this.hidLevel.Value))
				{
					this.GetSubRecommend(int.Parse(dr["MemID"].ToString()), ref trnm);
				}
			}
		}

		protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
		{
			this.ShowRptPointDetails();
		}

		public void ShowRptPointDetails()
		{
			this.lblMemCard.Text = this.TreeView1.SelectedNode.Text;
			this.lblMemName.Text = this.TreeView1.SelectedNode.ToolTip;
			List<int> listmem = new List<int>();
			this.GetNodeLevel(this.TreeView1.SelectedNode, ref listmem);
			string strMemlist = string.Join(",", listmem.ConvertAll<string>((int m) => m.ToString()).ToArray());
			string str = "";
			if (strMemlist.Length > 0)
			{
				str = " and PointGiveMemID in (" + strMemlist + ")";
				DataTable dbin = this.bllPointRate.GetMemDetailByMemID(int.Parse(this.TreeView1.SelectedValue), str).Tables[0];
				this.rptPointDetails.DataSource = dbin;
				this.rptPointDetails.DataBind();
				this.lblListin.Text = dbin.Compute("sum(POINTNUMBER)", "").ToString();
				str = " and PointGiveMemID not in (" + strMemlist + ")";
				DataTable db = this.bllPointRate.GetMemDetailByMemID(int.Parse(this.TreeView1.SelectedValue), str).Tables[0];
				string listout = db.Compute("Sum(POINTNUMBER)", "").ToString();
				this.lblListout.Text = ((listout.ToString() == "") ? "0" : listout.ToString());
			}
			else
			{
				this.lblListin.Text = "0";
				this.rptPointDetails.DataSource = new DataTable();
				this.rptPointDetails.DataBind();
				if (this.TreeView1.SelectedNode.ChildNodes.Count == 0)
				{
					DataTable db = this.bllPointRate.GetMemDetailByMemID(int.Parse(this.TreeView1.SelectedValue), str).Tables[0];
					string listout = db.Compute("Sum(POINTNUMBER)", "").ToString();
					this.lblListout.Text = ((listout.ToString() == "") ? "0" : listout.ToString());
				}
			}
			foreach (RepeaterItem rp in this.rptPointDetails.Items)
			{
				Label lblNum = (Label)rp.FindControl("lblDetails");
				lblNum.Text = (rp.ItemIndex + 1).ToString();
			}
		}

		public void GetNodeLevel(TreeNode tn, ref List<int> listmem)
		{
			TreeNodeCollection trc = tn.ChildNodes;
			foreach (TreeNode trn in trc)
			{
				listmem.Add(int.Parse(trn.Value));
				this.GetNodeLevel(trn, ref listmem);
			}
		}
	}
}
