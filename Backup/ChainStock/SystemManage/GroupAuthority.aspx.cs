using Chain.BLL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class GroupAuthority : PageBase
	{
		protected HtmlForm frmGroupAuthority;

		protected Literal ltlTitle;

		protected HtmlInputText txtGroupName;

		protected HtmlGenericControl lblIsPublic;

		protected HtmlInputCheckBox chkIsPublic;

		protected HtmlInputHidden txtGroupID;

		protected HtmlInputText txtGroupType;

		protected HtmlSelect sltParentGroup;

		protected HtmlInputText txtGroupRemark;

		protected Repeater gdGroupPermission;

		protected Button btnAuthority;

		protected Button btnAuthorityCancel;

		protected HiddenField HidGid;

		protected HiddenField HidAcction;

		private int add = 0;

		private Chain.Model.SysGroup group = new Chain.Model.SysGroup();

		private Chain.BLL.SysGroup bllGroup = new Chain.BLL.SysGroup();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["GroupType"] != null)
				{
					this.txtGroupType.Value = base.Request.QueryString["GroupType"];
				}
				if (base.Request.QueryString["Gid"] != null)
				{
					this.HidGid.Value = base.Request.QueryString["Gid"];
					this.Set_Caption();
					this.BindGroupList();
					this.Get_MenuTree();
					this.Set_GroupPageAcctionPer();
				}
				else
				{
					base.Response.Write("<script>history.back();</script>");
				}
			}
		}

		public void BindGroupList()
		{
			this.group = this.bllGroup.GetModelByGroupType(Convert.ToInt32(this.txtGroupType.Value));
			if (this.group != null)
			{
				this.sltParentGroup.Items.Add(new ListItem(this.group.GroupName, this.group.GroupID.ToString()));
			}
			string strSql = string.Format("(IsPublic = 1  and (GroupType=" + this.txtGroupType.Value + " ))", new object[0]);
			DataTable dt = this.bllGroup.GetList(strSql).Tables[0];
			DataTable dtTree = dt.Clone();
			this.bllGroup.BuildGroupTree(dt, this._UserGroupID, 0, ref dtTree);
			foreach (DataRow item in dtTree.Rows)
			{
				this.sltParentGroup.Items.Add(new ListItem(item["GroupName"].ToString(), item["GroupID"].ToString()));
			}
			if (base.Request.QueryString["ParentGroupID"] != null && base.Request.QueryString["ParentGroupID"] != "")
			{
				int parentGroupID = int.Parse(base.Request.QueryString["ParentGroupID"]);
				this.sltParentGroup.Value = parentGroupID.ToString();
			}
			else
			{
				this.sltParentGroup.Value = this._UserGroupID.ToString();
			}
			if (this.HidGid.Value != "0")
			{
				this.sltParentGroup.Disabled = true;
			}
		}

		public void Set_Caption()
		{
			if (int.Parse(this.HidGid.Value) > 0)
			{
				Chain.Model.SysGroup group = new Chain.BLL.SysGroup().GetModel(int.Parse(this.HidGid.Value));
				this.txtGroupID.Value = group.GroupID.ToString();
				this.txtGroupName.Value = group.GroupName;
				this.txtGroupRemark.Value = group.GroupRemark;
				this.chkIsPublic.Checked = group.IsPublic;
				this.chkIsPublic.Disabled = true;
			}
			if (base.Request.QueryString["ParentGroupID"] != null && base.Request.QueryString["ParentGroupID"] != "")
			{
				int parentGroupID = int.Parse(base.Request.QueryString["ParentGroupID"]);
				Chain.Model.SysGroup group = new Chain.BLL.SysGroup().GetModel(parentGroupID);
				if (group != null && !group.IsPublic)
				{
					this.chkIsPublic.Checked = false;
					this.chkIsPublic.Disabled = true;
					this.lblIsPublic.Visible = false;
				}
			}
		}

		private void Get_MenuTree()
		{
			Chain.BLL.SysModule bll = new Chain.BLL.SysModule();
			string strSql = " ModuleVisible='True' ";
			strSql = strSql + " and ModuleID in (select ModuleID from SysGroupAuthority where GroupID=" + ((this.sltParentGroup.Value == "0") ? "1" : this.sltParentGroup.Value) + " and ActionValue=1) ";
			DataTable db = bll.GetAllList(strSql).Tables[0];
			this.gdGroupPermission.DataSource = this.Create_TreeMenuTable(db);
			this.gdGroupPermission.DataBind();
		}

		private DataTable Create_TreeMenuTable(DataTable db)
		{
			DataTable treedb = db.Clone();
			DataRow[] dr = db.Select("ModuleParentID=0", "ModuleOrder");
			for (int i = 0; i < dr.Length; i++)
			{
				treedb.Rows.Add(dr[i].ItemArray);
				this.Create_TreeMneuTable(db, ref treedb, int.Parse(dr[i]["ModuleID"].ToString()));
			}
			return treedb;
		}

		public void Create_TreeMneuTable(DataTable db, ref DataTable treedb, int MenuID)
		{
			DataRow[] dr = db.Select("ModuleParentID=" + MenuID, "ModuleOrder");
			for (int i = 0; i < dr.Length; i++)
			{
				dr[i]["ModuleCaption"] = "     |---" + dr[i]["ModuleCaption"].ToString();
				treedb.Rows.Add(dr[i].ItemArray);
			}
		}

		protected void gdGroupPermission_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				string Pid = dr["ModuleID"].ToString();
				Chain.BLL.SysModuleAction bll = new Chain.BLL.SysModuleAction();
				string strSql = string.Format(" actionid in (select actionid from SysGroupAuthority where GroupId={0} and ModuleId={1} and ActionValue=1)", (this.sltParentGroup.Value == "0") ? "1" : this.sltParentGroup.Value, int.Parse(Pid));
				DataTable db = bll.GetList(strSql).Tables[0];
				CheckBoxList Permission = (CheckBoxList)e.Item.FindControl("ChkListPerm");
				Permission.RepeatLayout = RepeatLayout.Table;
				Permission.RepeatDirection = RepeatDirection.Horizontal;
				Permission.CssClass = "CssrepeatTable";
				Permission.ForeColor = Color.Black;
				Permission.DataSource = db;
				Permission.DataTextField = "ActionCaption";
				Permission.DataValueField = "ActionID";
				Permission.DataBind();
			}
		}

		private void Set_GroupPageAcctionPer()
		{
			DataTable db = PubFunction.GetUserAuthority(int.Parse(this.HidGid.Value));
			for (int i = 0; i < this.gdGroupPermission.Items.Count; i++)
			{
				CheckBoxList Permission = (CheckBoxList)this.gdGroupPermission.Items[i].FindControl("ChkListPerm");
				Label lblMenuID = (Label)this.gdGroupPermission.Items[i].FindControl("lblMenuID");
				DataRow[] dr = db.Select("ModuleID=" + lblMenuID.Text);
				DataRow[] array = dr;
				for (int j = 0; j < array.Length; j++)
				{
					DataRow drw = array[j];
					foreach (ListItem it in Permission.Items)
					{
						if (it.Value == drw["ActionID"].ToString())
						{
							Permission.Attributes.Add("class", "chk");
							it.Selected = Convert.ToBoolean(drw["ActionValue"].ToString());
						}
					}
				}
			}
		}

		protected void btnAuthority_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.txtGroupName.Value.Trim() != "" & this.txtGroupRemark.Value.Trim() != "")
				{
					Chain.Model.SysLog modelLog = new Chain.Model.SysLog();
					modelLog.LogShopID = this._UserShopID;
					modelLog.LogUserID = this._UserID;
					modelLog.LogCreateTime = DateTime.Now;
					modelLog.LogActionID = 11;
					modelLog.LogType = "系统角色";
					this.group.GroupType = Convert.ToInt32(this.txtGroupType.Value);
					this.group.GroupName = this.txtGroupName.Value.Trim();
					this.group.ParentGroupID = int.Parse(this.sltParentGroup.Value);
					this.group.IsPublic = this.chkIsPublic.Checked;
					if (this.group.ParentGroupID == 0)
					{
						this.group.ParentGroupID = 0;
						this.group.ParentIDStr = "";
					}
					else
					{
						Chain.Model.SysGroup parentGroup = this.bllGroup.GetModel(this.group.ParentGroupID);
						if (parentGroup != null)
						{
							if (string.IsNullOrEmpty(parentGroup.ParentIDStr))
							{
								parentGroup.ParentIDStr = "/";
							}
							this.group.ParentIDStr = parentGroup.ParentIDStr + parentGroup.GroupID + "/";
						}
					}
					this.group.GroupRemark = this.txtGroupRemark.Value.Trim();
					if (int.Parse(this.HidGid.Value) > 0)
					{
						switch (PubFunction.curParameter.istry)
						{
						case 0:
							if (int.Parse(this.txtGroupID.Value) != 1)
							{
								this.group.GroupID = int.Parse(this.txtGroupID.Value);
								this.group.CreateUserID = this._UserID;
								modelLog.LogDetail = "修改角色成功，角色名称：" + this.group.GroupName + ",说明：" + this.group.GroupRemark;
								this.bllGroup.Update(this.group);
								this.add = int.Parse(this.HidGid.Value);
								new Chain.BLL.SysLog().Add(modelLog);
							}
							break;
						case 1:
							this.group.GroupID = int.Parse(this.txtGroupID.Value);
							this.group.CreateUserID = this._UserID;
							modelLog.LogDetail = "修改角色成功，角色名称：" + this.group.GroupName + ",说明：" + this.group.GroupRemark;
							this.bllGroup.Update(this.group);
							this.add = int.Parse(this.HidGid.Value);
							new Chain.BLL.SysLog().Add(modelLog);
							break;
						case 2:
							if (int.Parse(this.txtGroupID.Value) != 1)
							{
								this.group.GroupID = int.Parse(this.txtGroupID.Value);
								this.group.CreateUserID = this._UserID;
								modelLog.LogDetail = "修改角色成功，角色名称：" + this.group.GroupName + ",说明：" + this.group.GroupRemark;
								this.bllGroup.Update(this.group);
								this.add = int.Parse(this.HidGid.Value);
								new Chain.BLL.SysLog().Add(modelLog);
							}
							break;
						}
					}
					else if (this.txtGroupID.Value == "")
					{
						this.group.CreateUserID = this._UserID;
						modelLog.LogDetail = "新增角色成功，角色名称：" + this.group.GroupName + ",说明：" + this.group.GroupRemark;
						this.add = this.bllGroup.Add(this.group);
						new Chain.BLL.SysLog().Add(modelLog);
					}
					switch (PubFunction.curParameter.istry)
					{
					case 0:
						if (this.txtGroupID.Value == "1")
						{
							base.OutputWarn("此版本为试用版，不可更改超级管理员。");
						}
						else if (this.SetGroupAuthority(this.add))
						{
							base.OutputWarn("保存成功！");
						}
						break;
					case 1:
						if (this.SetGroupAuthority(this.add))
						{
							base.OutputWarn("保存成功！");
						}
						break;
					case 2:
						if (this.txtGroupID.Value == "1")
						{
							base.OutputWarn("此版本为试用版，不可更改超级管理员。");
						}
						else if (this.SetGroupAuthority(this.add))
						{
							base.OutputWarn("保存成功！");
						}
						break;
					}
					List<Chain.Model.SysGroup> list = new Chain.BLL.SysGroup().GetModelList("");
					foreach (Chain.Model.SysGroup item in list)
					{
						PubFunction.UpdateGroupAuthority(item.GroupID);
					}
				}
				else
				{
					base.OutputWarn("数据不完整，请重新输入！");
				}
			}
			catch
			{
				base.OutputWarn("系统错误,请与系统管理员联系！");
			}
		}

		public bool SetGroupAuthority(int add)
		{
			int intIndex = -1;
			Chain.BLL.SysGroupAuthority bllGroupAuthority = new Chain.BLL.SysGroupAuthority();
			Chain.Model.SysGroupAuthority modelGroupAuthority = new Chain.Model.SysGroupAuthority();
			bllGroupAuthority.DeleteList(int.Parse(this.HidGid.Value));
			DataTable dtGroup = this.bllGroup.GetList(" GroupID=" + add).Tables[0];
			for (int i = 0; i < this.gdGroupPermission.Items.Count; i++)
			{
				CheckBoxList Permission = (CheckBoxList)this.gdGroupPermission.Items[i].FindControl("ChkListPerm");
				Label lblMenuID = (Label)this.gdGroupPermission.Items[i].FindControl("lblMenuID");
				foreach (ListItem it in Permission.Items)
				{
					modelGroupAuthority.GroupID = new int?(int.Parse(dtGroup.Rows[0]["GroupID"].ToString()));
					modelGroupAuthority.ModuleID = new int?(int.Parse(lblMenuID.Text));
					modelGroupAuthority.ActionValue = it.Selected;
					modelGroupAuthority.ActionID = new int?(int.Parse(it.Value));
					intIndex = bllGroupAuthority.Add(modelGroupAuthority);
				}
			}
			bllGroupAuthority.CheckChildGroup(add);
			PubFunction.UpdateGroupAuthority(this._UserGroupID);
			return intIndex > 0;
		}

		protected void btnAuthorityCancel_Click(object sender, EventArgs e)
		{
			base.Response.Write("<Script Language='JavaScript'>window.location.href = '../SystemManage/GroupList.aspx?PID=32';</script>");
		}
	}
}
