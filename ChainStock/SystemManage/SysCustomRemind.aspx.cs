using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class SysCustomRemind : PageBase
	{
		protected HtmlForm frmCustomRemind;

		protected Literal ltlTitle;

		protected HtmlGenericControl lblCustomRemindUSer;

		protected HtmlInputText txtCustomRemindTime;

		protected HtmlInputText txtCustomRemindTitle;

		protected HtmlInputHidden txtCustomRemindID;

		protected CheckBoxList cblCustomReminder;

		protected HtmlTextArea txtCustomRemindDetail;

		protected HtmlInputSubmit btnCustomRemindSave;

		protected HtmlInputButton btnCustomRemindAdd;

		protected Repeater gvCustomRemind;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["CID"] != null)
				{
					this.GetCustomRemindInfo();
				}
				this.BindCBLCustomRemind();
				this.GetCustomRemindList();
			}
			this.lblCustomRemindUSer.InnerText = this._UserName;
		}

		private void BindCBLCustomRemind()
		{
			DataTable dt = new Chain.BLL.SysUser().GetList("").Tables[0];
			this.cblCustomReminder.DataSource = dt;
			this.cblCustomReminder.DataTextField = "UserName";
			this.cblCustomReminder.DataValueField = "UserID";
			this.cblCustomReminder.DataBind();
			foreach (ListItem li in this.cblCustomReminder.Items)
			{
				li.Attributes.Add("alt", li.Value);
			}
		}

		private void GetCustomRemindList()
		{
			Chain.BLL.SysCustomRemind bllCustomRemind = new Chain.BLL.SysCustomRemind();
			DataTable dtCustomRemind = bllCustomRemind.GetList("").Tables[0];
			this.gvCustomRemind.DataSource = dtCustomRemind;
			this.gvCustomRemind.DataBind();
		}

		private void GetCustomRemindInfo()
		{
			int intRemind = int.Parse(base.Request.QueryString["CID"]);
			Chain.Model.SysCustomRemind modeRemind = new Chain.BLL.SysCustomRemind().GetModel(intRemind);
			this.txtCustomRemindID.Value = modeRemind.CustomRemindID.ToString();
			this.txtCustomRemindTitle.Value = modeRemind.CustomRemindTitle;
			this.txtCustomRemindTime.Value = modeRemind.CustomRemindTime.ToShortDateString();
			this.txtCustomRemindDetail.Value = modeRemind.CustomRemindDetail;
			string strReminder = modeRemind.CustomReminder;
			string[] str = strReminder.Split(new char[]
			{
				','
			});
			string[] array = str;
			for (int i = 0; i < array.Length; i++)
			{
				string p = array[i];
				this.cblCustomReminder.Items.FindByValue(p).Selected = true;
			}
		}
	}
}
