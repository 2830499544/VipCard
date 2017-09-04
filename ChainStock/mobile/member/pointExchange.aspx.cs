using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.member
{
	public class pointExchange : Page
	{
		protected HtmlInputText txtKey;

		protected HtmlGenericControl spMemPointTotal;

		protected HtmlAnchor allclass;

		protected Repeater rptClass;

		protected Repeater rptGift;

		protected HtmlAnchor moreGift;

		protected HtmlInputHidden txtMemID;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					this.BindMemInfo(MemID);
					this.txtMemID.Value = MemID.ToString();
					this.rptClassBind();
					this.rptGiftBind();
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		private void rptGiftBind()
		{
			Chain.BLL.PointGift bllGift = new Chain.BLL.PointGift();
			string sql = " GiftStockNumber>0 ";
			if (base.Request.QueryString["ClassID"] != null)
			{
				sql = sql + " and GiftClassID=" + base.Request.QueryString["ClassID"];
			}
			if (base.Request.QueryString["Key"] != null)
			{
				sql = sql + " and GiftName like '%" + base.Request.QueryString["Key"] + "%'";
				this.txtKey.Value = base.Request.QueryString["Key"].ToString();
			}
			DataTable dt;
			if (base.Request.QueryString["type"] == "all")
			{
				dt = bllGift.GetList(sql).Tables[0];
			}
			else
			{
				dt = bllGift.GetList(10, sql, "GiftID").Tables[0];
			}
			this.rptGift.DataSource = dt;
			this.rptGift.DataBind();
			if (dt.Rows.Count < 10 || base.Request.QueryString["type"] == "all")
			{
				this.moreGift.Attributes.Add("style", "display:none");
			}
		}

		protected void rptClass_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				HtmlControl classname = (HtmlControl)e.Item.FindControl("classname");
				this.allclass.Attributes.Remove("class");
				if (base.Request.QueryString["ClassID"] != null)
				{
					if (dr["GiftClassID"].ToString() == base.Request.QueryString["ClassID"])
					{
						classname.Attributes.Add("class", "active");
					}
					else
					{
						classname.Attributes.Remove("class");
					}
				}
				else
				{
					this.allclass.Attributes.Add("class", "active");
				}
			}
		}

		private void rptClassBind()
		{
			Chain.BLL.GiftClass bllClass = new Chain.BLL.GiftClass();
			DataTable dt = bllClass.GetList("1=1").Tables[0];
			this.rptClass.DataSource = bllClass.GetList("1=1").Tables[0];
			this.rptClass.DataBind();
		}

		private void BindMemInfo(int MemID)
		{
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			Chain.Model.Mem modelMem = bllMem.GetModel(MemID);
			int exchangePoint = new Chain.BLL.GiftExchange().GetMemExchangePoint("MemID=" + MemID + " and ExchangeStatus=1");
			this.spMemPointTotal.InnerHtml = (modelMem.MemPoint - exchangePoint).ToString();
		}
	}
}
