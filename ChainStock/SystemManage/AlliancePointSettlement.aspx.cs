using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.SystemManage
{
	public class AlliancePointSettlement : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmShopSettlement;

		protected HtmlInputHidden hdShopID;

		protected Literal ltlTitle;

		protected HtmlInputHidden txtShopType;

		protected HtmlSelect sltShop;

		protected HtmlSelect sltIsFinish;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected Button btnRptMemQuery;

		protected HtmlTextArea txtRemark;

		protected Repeater gvShopSettlement;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindUserShop();
				this.txtShopType.Value = "2";
				this.txtStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				this.Get_ParameterList(this.QueryCondition());
			}
		}

		public void BindUserShop()
		{
			Chain.BLL.SysShop bllSysShop = new Chain.BLL.SysShop();
			DataTable dtSysShop = new DataTable();
			if (this._UserShopID == 1)
			{
				dtSysShop = bllSysShop.GetList(" ShopType=2 ").Tables[0];
			}
			else
			{
				dtSysShop = bllSysShop.GetList(string.Format("  ShopType=2 and FatherShopID = {0} OR ShopID = {1}", this._UserShopID, this._UserShopID)).Tables[0];
			}
			this.sltShop.Items.Add(new ListItem("====请选择====", ""));
			foreach (DataRow dr in dtSysShop.Rows)
			{
				this.sltShop.Items.Add(new ListItem(dr["ShopName"].ToString(), dr["ShopID"].ToString()));
			}
		}

		protected void btnRptMemQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.Get_ParameterList(this.QueryCondition());
		}

		private void Get_ParameterList(string strSql)
		{
			Chain.BLL.SysShopPointSettlement bllSysShopPointSettlement = new Chain.BLL.SysShopPointSettlement();
			bllSysShopPointSettlement.UpDateSettlement();
			int Counts = this.NetPagerParameter.RecordCount;
			string join = " on SysShopPointSettlement.OutShopID = SysShop.ShopID ";
			DataTable db = bllSysShopPointSettlement.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, join, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			this.NetPagerParameter.RecordCount = Counts;
			this.NetPagerParameter.CustomInfoHTML = string.Format("<div><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[]
			{
				this.NetPagerParameter.CurrentPageIndex,
				this.NetPagerParameter.PageCount,
				this.NetPagerParameter.RecordCount,
				this.NetPagerParameter.PageSize
			});
			this.gvShopSettlement.DataSource = db;
			this.gvShopSettlement.DataBind();
			PageBase.BindSerialRepeater(this.gvShopSettlement, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.Get_ParameterList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1  and SysShop.ShopType=2");
			if (this.sltIsFinish.Value != "")
			{
				strSql.AppendFormat(" and IsFinish = {0}", this.sltIsFinish.Value);
			}
			if (this.sltShop.Value != "")
			{
				Chain.Model.SysShop shop = new Chain.BLL.SysShop().GetModel(Convert.ToInt32(this.sltShop.Value));
				if (shop.ShopID == 1)
				{
					strSql.Append("");
				}
				else if (shop.IsAllianceProgram)
				{
					strSql.AppendFormat(" AND OutShopID IN (SELECT ShopID FROM SysShop WHERE FatherShopID = {0} or ShopID = {1} )", this.sltShop.Value, this.sltShop.Value);
				}
				else
				{
					strSql.AppendFormat(" and OutShopID = '{0}'", this.sltShop.Value);
				}
			}
			else if (this._UserShopID == 1)
			{
				strSql.Append("");
			}
			else
			{
				strSql.AppendFormat(" AND OutShopID IN (SELECT ShopID FROM SysShop WHERE FatherShopID = {0} or ShopID = {1} )", this._UserShopID, this._UserShopID);
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat("and StartTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat("and EndTime<='{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			return strSql.ToString();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void gvShopSettlement_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				HtmlControl hyShopSettlement = (HtmlControl)e.Item.FindControl("hyShopSettlement");
				HtmlControl hyShopLook = (HtmlControl)e.Item.FindControl("hyShopLook");
				if (this._UserShopID != 1)
				{
					hyShopSettlement.Attributes.Add("style", "display:none;");
				}
				else
				{
					if (Convert.ToInt32(dr["OutShopID"]) == this._UserShopID)
					{
						hyShopSettlement.Attributes.Add("style", "display:none;");
					}
					if (Convert.ToBoolean(dr["IsFinish"]))
					{
						hyShopSettlement.Attributes.Add("style", "display:none;");
					}
					else
					{
						hyShopLook.Attributes.Add("style", "display:none;");
					}
				}
			}
		}
	}
}
