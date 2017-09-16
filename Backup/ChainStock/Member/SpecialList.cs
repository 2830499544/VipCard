using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.Member
{
	public class SpecialList : PageBase
	{
		protected HtmlForm form1;

		protected Literal ltlTitle;

		protected HtmlInputButton btnSpecialListAdd;

		protected HtmlInputText txtUserType;

		protected Repeater gvSpecialList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.GetSpecialList();
		}

		private void GetSpecialList()
		{
			Special blls = new Special();
			string strSql = string.Empty;
			strSql += "1=1";
			strSql += " and  Special.SpecialUser = SysUser.UserID ";
			strSql = strSql + "and  SpecialUser=" + this._UserShopID;
			int Counts = this.NetPagerParameter.RecordCount;
			DataTable dtMem = blls.GetListsp(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
			{
				PubFunction.GetMemListShopAuthority(this._UserShopID, "SpecialID", strSql)
			}).Tables[0];
			this.NetPagerParameter.RecordCount = Counts;
			this.NetPagerParameter.CustomInfoHTML = string.Format("<div class=\"results\"><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[]
			{
				this.NetPagerParameter.CurrentPageIndex,
				this.NetPagerParameter.PageCount,
				this.NetPagerParameter.RecordCount,
				this.NetPagerParameter.PageSize
			});
			this.gvSpecialList.DataSource = dtMem;
			this.gvSpecialList.DataBind();
			PageBase.BindSerialRepeater(this.gvSpecialList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.GetSpecialList();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.GetSpecialList();
		}

		protected void btnShopSelect_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.GetSpecialList();
		}

		protected string GetType(string type)
		{
			string strName = "";
			if (type != null)
			{
				if (!(type == "1"))
				{
					if (!(type == "2"))
					{
						if (!(type == "3"))
						{
							if (type == "4")
							{
								strName = "会员生日";
							}
						}
						else
						{
							strName = "每月";
						}
					}
					else
					{
						strName = "每周";
					}
				}
				else
				{
					strName = "固定期限";
				}
			}
			return strName;
		}
	}
}
