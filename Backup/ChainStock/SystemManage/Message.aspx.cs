using Chain.BLL;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.SystemManage
{
	public class Message : PageBase
	{
		protected HtmlForm frmMessage;

		protected Literal ltlTitle;

		protected HtmlInputText txtQueryMem;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected HtmlSelect sltShop;

		protected Button btQueryMemSearch;

		protected Repeater gvMessageList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				this.Get_ParameterList(this.QueryCondition());
			}
		}

		protected void btQueryMemSearch_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.Get_ParameterList(this.QueryCondition());
		}

		private void Get_ParameterList(string strSql)
		{
			Chain.BLL.Message bllMessage = new Chain.BLL.Message();
			strSql += " and Message.MessageMemID = Mem.MemID and Mem.MemShopID = SysShop.ShopID  ";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "MemShopID", strSql);
			strSql += " group by MemCard,MemName,MemMobile,ShopName,MemID ";
			int Counts = this.NetPagerParameter.RecordCount;
			DataTable db = bllMessage.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			this.NetPagerParameter.RecordCount = Counts;
			this.NetPagerParameter.CustomInfoHTML = string.Format("<div class=\"results\"><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[]
			{
				this.NetPagerParameter.CurrentPageIndex,
				this.NetPagerParameter.PageCount,
				this.NetPagerParameter.RecordCount,
				this.NetPagerParameter.PageSize
			});
			this.gvMessageList.DataSource = db;
			this.gvMessageList.DataBind();
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Get_ParameterList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value;
			string strMemShopID = this.sltShop.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1 and MemID >0 ");
			if (strQueryMem != "")
			{
				strSql.AppendFormat("and (MemCard ='{0}' or MemName like '%{0}%' or MemMobile='{0}')", strQueryMem);
			}
			if (strMemShopID != "")
			{
				strSql.AppendFormat("and MemShopID={0}", int.Parse(strMemShopID));
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat(" and MessageTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat(" and MessageTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			return strSql.ToString();
		}

		protected void gvMessageList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				if (e.Row.Cells[6].Text == "0")
				{
					e.Row.Cells[6].Text = "未回复";
					e.Row.Cells[6].ForeColor = Color.Red;
				}
				else
				{
					e.Row.Cells[6].Text = "已回复";
				}
			}
		}
	}
}
