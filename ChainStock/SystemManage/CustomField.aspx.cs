using Chain.BLL;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class CustomField : PageBase
	{
		protected HtmlForm frmCustomField;

		protected HtmlInputHidden txtCustomID;

		protected Literal ltlTitle;

		protected HtmlInputText txtCustomName;

		protected HtmlInputText txtCustomCode;

		protected HtmlTableCell trCustomFieleType;

		protected HtmlInputRadioButton radText;

		protected HtmlInputRadioButton radDate;

		protected HtmlInputRadioButton radSelect;

		protected HtmlTableRow trCustomSelectData;

		protected HtmlInputText txtCustomSelectData;

		protected HtmlInputButton btnCustomFieldAdd;

		protected Button btnIsShowAll;

		protected Repeater gvCustomFieldList;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.GetCustomFieldList();
			}
		}

		private void GetCustomFieldList()
		{
			MemCustomField bllMemCustomField = new MemCustomField();
			DataTable dt = bllMemCustomField.GetAllCustom().Tables[0];
			this.gvCustomFieldList.DataSource = dt;
			this.gvCustomFieldList.DataBind();
		}

		protected void ckbCustomFieldShow_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox chk = (CheckBox)sender;
			RepeaterItem dcf = (RepeaterItem)chk.Parent;
			string customId = ((Literal)dcf.FindControl("ltrID")).Text;
			if (chk.Checked)
			{
				new MemCustomField().UpdateCustomFieldShow(customId, true);
			}
			else
			{
				new MemCustomField().UpdateCustomFieldShow(customId, false);
			}
		}

		protected void btnIsShowAll_Click(object sender, EventArgs e)
		{
			new MemCustomField().UpdateCustomFieldShow("", true);
			this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "message", "<Script Language='JavaScript' defer>art.dialog({ time: 1.5, content: '自定义属性已全部显示在列表中！', close: function () { location.href = location.href; }});</script>");
		}

		public string GetCustomFieldType(string strType)
		{
			string strResult = "选择项";
			if (strType == "text")
			{
				strResult = "文本";
			}
			else if (strType == "date")
			{
				strResult = "日期";
			}
			return strResult;
		}

		protected void gvCustomFieldList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				if (e.Row.Cells[3].Text == "text")
				{
					e.Row.Cells[3].Text = "文本";
				}
				else if (e.Row.Cells[3].Text == "date")
				{
					e.Row.Cells[3].Text = "日期";
				}
				else
				{
					e.Row.Cells[3].Text = "选择项";
				}
				if (e.Row.Cells[5].Text == "1")
				{
					e.Row.Cells[5].Text = "会员";
					e.Row.Cells[5].ForeColor = Color.Red;
				}
				else
				{
					e.Row.Cells[5].Text = "产品";
					e.Row.Cells[5].ForeColor = Color.Blue;
				}
			}
		}
	}
}
