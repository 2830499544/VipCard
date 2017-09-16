using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class GroupList : PageBase
	{
		protected HtmlForm frmGroup;

		protected Literal ltlTitle;

		protected HtmlInputButton btnGroupAdd;

		protected Repeater gvGroupList;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.GetGroupList();
			}
		}

		private void GetGroupList()
		{
			SysGroup group = new SysGroup();
			string strSql = " 1=1";
			strSql += string.Format(" and (IsPublic = 'true' or CreateUserID in (select UserID from sysUser where UserShopID={0}))", this._UserShopID);
			DataTable dt = group.GetList(strSql).Tables[0];
			DataTable dtTree = dt.Clone();
			group.BuildGroupTree(dt, 0, 0, ref dtTree);
			this.gvGroupList.DataSource = dtTree;
			this.gvGroupList.DataBind();
			PageBase.BindSerialRepeater(this.gvGroupList, 0);
		}

		protected void gvGroupList_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				HtmlAnchor hyGroupEdit = (HtmlAnchor)e.Item.FindControl("hyGroupPermission");
				HtmlAnchor hyGroupDelete = (HtmlAnchor)e.Item.FindControl("hyGroupel");
				if (dr["ParentIDStr"].ToString().IndexOf("/" + this._UserGroupID + "/") < 0)
				{
					hyGroupEdit.Visible = false;
					hyGroupDelete.Visible = false;
				}
				if (!bool.Parse(dr["IsPublic"].ToString()) && int.Parse(dr["CreateUserID"].ToString()) != this._UserID)
				{
					hyGroupEdit.Visible = false;
					hyGroupDelete.Visible = false;
				}
				if (dr["GroupID"].ToString() == "1")
				{
					hyGroupEdit.Attributes.Add("style", "display:none");
					hyGroupDelete.Attributes.Add("style", "display:none");
				}
			}
		}
	}
}
