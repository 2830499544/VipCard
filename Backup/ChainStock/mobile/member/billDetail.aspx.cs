using Chain.BLL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ChainStock.mobile.member
{
	public class billDetail : Page
	{
		protected HtmlGenericControl spOrderType;

		protected HtmlGenericControl spOrderDiscountMoney;

		protected HtmlGenericControl spPayTypeInfo;

		protected HtmlGenericControl spPayType;

		protected HtmlGenericControl spGoodsName;

		protected HtmlGenericControl spOrderPointInfo;

		protected HtmlGenericControl spOrderPoint;

		protected HtmlGenericControl spOrderCreateTime;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					Mem bllMem = new Mem();
					string OrderAccount = "";
					if (base.Request.QueryString["OrderAccount"] != null)
					{
						OrderAccount = base.Request.QueryString["OrderAccount"];
					}
					DataTable dt = bllMem.GetMemBillList(string.Concat(new object[]
					{
						" OrderMemID=",
						MemID,
						" and OrderAccount=",
						OrderAccount
					})).Tables[0];
					if (dt.Rows.Count > 0)
					{
						this.spOrderCreateTime.InnerHtml = DateTime.Parse(dt.Rows[0]["OrderCreateTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
						if (decimal.Parse(dt.Rows[0]["OrderDiscountMoney"].ToString()) > 0m)
						{
							this.spOrderDiscountMoney.InnerHtml = "+" + decimal.Parse(dt.Rows[0]["OrderDiscountMoney"].ToString()).ToString("#0.00");
						}
						else
						{
							this.spOrderDiscountMoney.InnerHtml = decimal.Parse(dt.Rows[0]["OrderDiscountMoney"].ToString()).ToString("#0.00");
						}
						string OrderName = dt.Rows[0]["OrderName"].ToString();
						this.spOrderPoint.InnerHtml = dt.Rows[0]["OrderPoint"].ToString();
						this.spOrderType.InnerHtml = dt.Rows[0]["OrderType"].ToString();
						this.spGoodsName.InnerHtml = dt.Rows[0]["GoodsName"].ToString();
						this.spPayType.InnerHtml = dt.Rows[0]["PayType"].ToString();
						if (OrderName == "商品退单")
						{
							this.spOrderPointInfo.InnerHtml = "扣除积分";
							this.spPayTypeInfo.InnerHtml = "退款方式";
						}
						if (OrderName == "会员充次")
						{
							this.spPayTypeInfo.InnerHtml = "充次方式";
						}
						if (OrderName == "会员充时")
						{
							this.spPayTypeInfo.InnerHtml = "充时方式";
						}
						if (OrderName == "会员充值")
						{
							this.spPayTypeInfo.InnerHtml = "充值方式";
						}
					}
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}
	}
}
