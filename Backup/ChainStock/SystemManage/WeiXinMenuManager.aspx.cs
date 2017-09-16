using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class WeiXinMenuManager : PageBase
	{
		protected HtmlForm frmWeiXinMenuManager;

		protected Literal ltlTitle;

		protected HtmlInputButton btnCreateMenu;

		protected HtmlInputButton btnAddMenu;

		protected Repeater rpt_MenuManager;

		protected HtmlSelect sltMenuType;

		protected HtmlSelect sltReply;

		private WeiXinMenu WeiXinMenuBll = new WeiXinMenu();

		private int menuFirst = 0;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				DataSet ds = this.WeiXinMenuBll.GetMenuAllInfo2();
				this.menuFirst = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
				this.rpt_MenuManager.DataSource = ds.Tables[0];
				this.rpt_MenuManager.DataBind();
				this.IsShowAddMenuButtom(ds.Tables[0].Rows.Count);
				this.BindMenuFirst();
				this.BindReplyRule();
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

		protected string MenuDel(object objLevel, object objMenuID, object objMenuName)
		{
			string returnStr = string.Empty;
			return string.Concat(new object[]
			{
				"<a href=\"javascript:menuDel(",
				objMenuID,
				",'",
				objMenuName,
				"')\"><img src=\"../images/Gift/del.png\" alt=\"删除\" title=\"删除\" /></a>"
			});
		}

		protected string MenuEdit(object objMenuID, object parentMenuID, object objMenuName, object objparentMenuID)
		{
			string returnStr = string.Empty;
			if (parentMenuID.ToString() == "0")
			{
				returnStr = string.Concat(new object[]
				{
					"<a href=\"javascript:updateMenuFirst(",
					objMenuID,
					",'",
					objMenuName,
					"')\"><img src=\"../images/Gift/eit.png\" alt=\"编辑\" title=\"编辑\" /></a>"
				});
			}
			else
			{
				returnStr = string.Concat(new object[]
				{
					"<a href=\"javascript:updateMenuSecound(",
					objMenuID,
					",'",
					objMenuName,
					"')\"><img src=\"../images/Gift/eit.png\" alt=\"编辑\" title=\"编辑\" /></a>"
				});
			}
			return returnStr;
		}

		private void IsShowAddMenuButtom(int menuCount)
		{
			if (menuCount >= 18)
			{
				this.btnAddMenu.Disabled = true;
			}
		}

		private void BindMenuFirst()
		{
			DataTable dt = this.WeiXinMenuBll.GetMenuParentInfo().Tables[0];
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (Convert.ToInt32(dt.Rows[i]["childNum"]) < 5)
				{
					this.sltMenuType.Items.Add(new ListItem(dt.Rows[i]["MenuName"].ToString(), dt.Rows[i]["MenuID"].ToString()));
				}
			}
		}

		private void BindReplyRule()
		{
			DataTable dt = new Chain.BLL.WeiXinRule().GetList("").Tables[0];
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				this.sltReply.Items.Add(new ListItem(dt.Rows[i]["RuleDesc"].ToString(), dt.Rows[i]["RuleNUmber"].ToString()));
			}
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
