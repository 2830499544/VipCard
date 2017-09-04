using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.member
{
	public class memExpenseRecord : Page
	{
		protected Repeater rptExchange;

		protected HtmlAnchor moreExpense;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["userid"] != null)
				{
					Chain.BLL.SysUser bllUser = new Chain.BLL.SysUser();
					Chain.Model.SysUser modelUser = bllUser.GetModel(int.Parse(this.Session["userid"].ToString()));
					Chain.BLL.OrderLog bllExchange = new Chain.BLL.OrderLog();
					DataTable dt;
					if (modelUser.UserShopID != 1)
					{
						dt = bllExchange.GetList(100, " OrderLog.OrderShopID= " + modelUser.UserShopID, " OrderCreateTime desc").Tables[0];
					}
					else
					{
						dt = bllExchange.GetList(100, "1=1", "OrderCreateTime desc").Tables[0];
					}
					this.rptExchange.DataSource = dt;
					this.rptExchange.DataBind();
					if (dt.Rows.Count < 10 || base.Request.QueryString["type"] == "all")
					{
						this.moreExpense.Attributes.Add("style", "display:none");
					}
					else
					{
						this.moreExpense.Attributes.Add("style", "");
					}
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		public string GetMemName(object memid)
		{
			string memname = "";
			string result;
			if (memid != null)
			{
				if (memid.ToString() != "0")
				{
					Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetModel(int.Parse(memid.ToString()));
					result = modelMem.MemName;
					return result;
				}
				memname = "散客";
			}
			result = memname;
			return result;
		}

		protected void rptOrder_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				Repeater rptItem = (Repeater)e.Item.FindControl("rptOrderDetail");
				if (rptItem != null)
				{
					string strSql = "    OrderDetail.OrderID=" + dr["OrderID"].ToString();
					Chain.BLL.OrderDetail bllOrderDetail = new Chain.BLL.OrderDetail();
					DataTable dt = bllOrderDetail.GetOrderDetail(strSql).Tables[0];
					rptItem.DataSource = dt;
					rptItem.DataBind();
				}
			}
		}
	}
}
