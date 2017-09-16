using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.member
{
	public class getCoupon : Page
	{
		protected HtmlInputHidden txtMemID;

		protected Repeater rptCoupon;

		protected HtmlGenericControl divMsg;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.Session["MemID"] != null)
			{
				int MemID = int.Parse(this.Session["MemID"].ToString());
				this.txtMemID.Value = MemID.ToString();
				Chain.BLL.Coupon bllCoupon = new Chain.BLL.Coupon();
				DataTable dt = bllCoupon.GetList("  IsGet=1 and ID not in(select CouPonID  from CouponList where CouPonMID= " + MemID + ")").Tables[0];
				this.rptCoupon.DataSource = dt;
				this.rptCoupon.DataBind();
				if (dt.Rows.Count == 0)
				{
					this.divMsg.Visible = true;
				}
			}
			else
			{
				base.Response.Redirect("login.aspx");
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
