using Chain.BLL;
using ChainStock.Controls;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.SystemManage
{
	public class ShopUserList : PageBase
	{
		protected HtmlForm frmUser;

		protected Literal ltlTitle;

		protected HtmlInputPassword txtPwdOne;

		protected HtmlInputPassword txtPwdTwo;

		protected HtmlInputText txtUserAccount;

		protected HtmlInputText txtUserName;

		protected HtmlInputHidden txtUserID;

		protected HtmlInputPassword txtPwd;

		protected HtmlInputHidden txtPassword;

		protected HtmlInputPassword txtRepwd;

		protected HtmlInputText txtUserNumber;

		protected HtmlInputText txtUserTel;

		protected HtmlSelect sltShopInfo;

		protected HtmlInputText txtShopId;

		protected HtmlSelect sltGroupID;

		protected HtmlInputText txtGroupID;

		protected HtmlInputRadioButton radChooseYes;

		protected HtmlInputRadioButton radChooseNo;

		protected HtmlTextArea txtUserRemark;

		protected HtmlInputButton btnSysUserAdd;

		protected HtmlInputText txtUserType;

		protected HtmlSelect sltShop;

		protected HtmlInputText txtSearchAccount;

		protected HtmlSelect sltUserGroupID;

		protected Button btnUserSearch;

		protected Repeater gvSysUserList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.txtUserType.Value = "3";
				PubFunction.BindShopSelectByShopType(this._UserShopID, this.sltShop, true, 3);
				PubFunction.BindAuthoritySelelctByGroupType(this._UserGroupID, this.sltUserGroupID, true, 3);
				PubFunction.BindShopSelectByShopType(this._UserShopID, this.sltShopInfo, false, 3);
				this.GetSysUserList(this.Condition());
				this.txtShopId.Value = this._UserShopID.ToString();
				this.txtGroupID.Value = this._UserGroupID.ToString();
			}
		}

		private void GetSysUserList(string strSql)
		{
			SysGroup group = new SysGroup();
			SysUser user = new SysUser();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " and SysShop.ShopType=3  and SysUser.UserShopID = SysShop.ShopID and SysUser.UserGroupID = SysGroup.GroupID ";
			switch (PubFunction.curParameter.istry)
			{
			case 0:
				strSql += "and SysUser.UserID>1";
				break;
			case 2:
				strSql += "and SysUser.UserID>1";
				break;
			}
			if (this._UserGroupID != 1)
			{
				object obj = strSql;
				strSql = string.Concat(new object[]
				{
					obj,
					" and ((sysUser.UserID=",
					this._UserID,
					") or (UserGroupID in (select GroupID from SysGroup where ParentGroupID=0 or ParentIDStr like '%/",
					int.Parse(this.txtUserType.Value),
					"/%')))"
				});
			}
			else
			{
				object obj = strSql;
				strSql = string.Concat(new object[]
				{
					obj,
					" and UserGroupID in (select GroupID from SysGroup where  ParentGroupID=0 or ParentIDStr like '%/",
					int.Parse(this.txtUserType.Value),
					"/%' or GroupID=",
					int.Parse(this.txtUserType.Value),
					")"
				});
			}
			DataTable db = user.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
			{
				PubFunction.GetMemListShopAuthority(this._UserShopID, "UserShopID", strSql)
			}).Tables[0];
			this.NetPagerParameter.RecordCount = Counts;
			this.NetPagerParameter.CustomInfoHTML = string.Format("<div class=\"results\"><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[]
			{
				this.NetPagerParameter.CurrentPageIndex,
				this.NetPagerParameter.PageCount,
				this.NetPagerParameter.RecordCount,
				this.NetPagerParameter.PageSize
			});
			this.gvSysUserList.DataSource = db;
			this.gvSysUserList.DataBind();
			PageBase.BindSerialRepeater(this.gvSysUserList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.GetSysUserList(this.Condition());
		}

		protected string Condition()
		{
			string strShopID = this.sltShop.Value;
			string strGroupID = this.sltUserGroupID.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" 1=1 ");
			if (strShopID != "")
			{
				strSql.AppendFormat(" and UserShopID={0}", strShopID);
			}
			if (strGroupID != "")
			{
				strSql.AppendFormat(" and UserGroupID ={0}", strGroupID);
			}
			if (this.txtSearchAccount.Value != "")
			{
				strSql.AppendFormat(" and UserAccount like '%{0}%' ", this.txtSearchAccount.Value);
			}
			return strSql.ToString();
		}

		protected void btnUserSearch_Click1(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.GetSysUserList(this.Condition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.GetSysUserList(this.Condition());
		}

		protected void gvSysUserList_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				if (dr["UserID"].ToString() == "1")
				{
					HtmlAnchor hyUserDelete = (HtmlAnchor)e.Item.FindControl("hyUserDel");
					hyUserDelete.Attributes.Add("style", "display:none");
				}
			}
		}
	}
}
