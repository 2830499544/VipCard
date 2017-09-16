using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainPoint.SystemManage
{
	public class WeiXinRuleList : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmWeiXinRuleList;

		protected HtmlInputText txtSystemDomain;

		protected Literal ltlTitle;

		protected Repeater gvTextRule;

		protected HtmlTextArea txtSendContent;

		protected Repeater r_NewsRule;

		protected HtmlTextArea txtNewsDesc;

		protected Repeater r_menu;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.GetSysRemind();
				this.txtSystemDomain.Value = PubFunction.curParameter.strDoMain;
			}
		}

		private void GetSysRemind()
		{
			this.gvTextRule.DataSource = new WeiXinRule().GetList("RuleNUmber !='1' and RuleNUmber !='2' and RuleNewsType='text' order by RuleID desc ").Tables[0];
			this.gvTextRule.DataBind();
			this.r_NewsRule.DataSource = new WeiXinNews().GetParent();
			this.r_NewsRule.DataBind();
			this.r_menu.DataSource = new WeiXinMenu().GetMenuAllInfo2().Tables[0];
			this.r_menu.DataBind();
		}

		protected void r_NewsRule_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView item = (DataRowView)e.Item.DataItem;
				if (item != null)
				{
					if (int.Parse(item["NewsThisCount"].ToString()) >= 10)
					{
						((HtmlAnchor)e.Item.FindControl("add")).Attributes.Add("disabled", "disabled");
					}
				}
				Repeater r_NewsDetail = (Repeater)e.Item.FindControl("r_NewsDetail");
				if (r_NewsDetail != null)
				{
					int RuleID = Convert.ToInt32(((DataRowView)e.Item.DataItem)["RuleID"]);
					DataTable dt = new WeiXinNews().GetList("NewsRuleID=" + RuleID).Tables[0];
					r_NewsDetail.DataSource = dt;
					r_NewsDetail.DataBind();
				}
			}
		}

		protected string GetMenuTypeName(object objMenuType)
		{
			string result;
			switch (Convert.ToInt32(objMenuType))
			{
			case 0:
				result = "菜单类型";
				break;
			case 1:
				result = "按钮类型";
				break;
			case 2:
				result = "链接类型";
				break;
			default:
				result = "未知类型";
				break;
			}
			return result;
		}

		protected string GetMenuName(object objMenuName, object objparentMenuID)
		{
			string MenuName = string.Empty;
			if (objparentMenuID.ToString() != "0")
			{
				MenuName = "----" + objMenuName;
			}
			else
			{
				MenuName = objMenuName.ToString();
			}
			return MenuName;
		}
	}
}
