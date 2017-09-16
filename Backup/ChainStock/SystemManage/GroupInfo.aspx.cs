using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class GroupInfo : PageBase
	{
		protected HtmlForm frmGroup;

		protected Image imgTitle;

		protected Label lblFrmTitle;

		protected HtmlInputText txtGroupName;

		protected HtmlInputHidden txtGroupID;

		protected HtmlInputText txtGroupRemark;

		protected Button btnGroupSave;

		protected HiddenField HidGid;

		protected HiddenField HidAcction;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["GID"] != null)
				{
					this.HidGid.Value = base.Request.QueryString["GID"];
				}
				this.HidAcction.Value = base.Request.QueryString["Acction"];
				this.Set_Caption();
				this.Set_GroupInfo();
			}
		}

		public void Set_Caption()
		{
			if (this.HidAcction.Value == "Add")
			{
				this.lblFrmTitle.Text = this.lblFrmTitle.Text + "--  角色设置";
			}
		}

		public void Set_GroupInfo()
		{
			if (this.HidAcction.Value == "Edit")
			{
				Chain.Model.SysGroup group = new Chain.BLL.SysGroup().GetModel(int.Parse(this.HidGid.Value));
				this.lblFrmTitle.Text = this.lblFrmTitle.Text + "--" + group.GroupName + "  角色编辑";
				this.txtGroupID.Value = group.GroupID.ToString();
				this.txtGroupName.Value = group.GroupName;
				this.txtGroupRemark.Value = group.GroupRemark;
			}
		}

		protected void btnGroupSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.txtGroupName.Value != "" & this.txtGroupRemark.Value != "")
				{
					Chain.Model.SysGroup group = new Chain.Model.SysGroup();
					group.GroupID = int.Parse(this.txtGroupID.Value);
					group.GroupName = this.txtGroupName.Value;
					group.GroupRemark = this.txtGroupRemark.Value;
					Chain.BLL.SysGroup bllGroup = new Chain.BLL.SysGroup();
					bllGroup.Update(group);
					base.Response.Write("<Script Language='JavaScript'>if ( window.confirm('保存成功！ 是否要进行系统角色管理？')) {  window.location.href='../SystemManage/GroupList.aspx?PID=32' } else {window.location.href= location.href };</script>");
				}
				else
				{
					base.Response.Write("<Script Language='JavaScript'>window.alert('数据不完整，请重新输入！');</script>");
				}
			}
			catch
			{
				base.Response.Write("<Script Language='JavaScript'>window.alert('系统错误 请与系统管理员联系！');</script>");
			}
		}
	}
}
