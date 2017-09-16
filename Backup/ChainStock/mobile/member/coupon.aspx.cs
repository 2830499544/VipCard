using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.member
{
	public class coupon : Page
	{
		protected Repeater rptCoupon;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int MemID = int.Parse(this.Session["MemID"].ToString());
					Chain.BLL.CouponList bllCoupon = new Chain.BLL.CouponList();
					this.rptCoupon.DataSource = bllCoupon.GetCouponDetailNew(" Coupon.ID=CouponList.CouPonID and CouPonMID= " + MemID);
					this.rptCoupon.DataBind();
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		public string GetCouponTime(object id)
		{
			string result = "";
			Chain.BLL.Coupon bllcoupon = new Chain.BLL.Coupon();
			if (id != null)
			{
				Chain.Model.Coupon modelCoupon = bllcoupon.GetModel(int.Parse(id.ToString()));
				if (modelCoupon.CouponEffective == 1)
				{
					result = DateTime.Parse(modelCoupon.CouponStart.ToString()).ToString("yyyy.MM.dd") + "--" + DateTime.Parse(modelCoupon.CouponEnd.ToString()).ToString("yyyy.MM.dd");
				}
				else
				{
					result = "永久有效";
				}
			}
			return result;
		}
	}
}
