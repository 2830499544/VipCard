using Chain.BLL;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.ExtraService
{
	public class CouponManage : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmCouponList;

		protected Literal ltlTitle;

		protected HtmlInputText txtCouponTitle;

		protected HtmlTextArea txtCouponContent;

		protected HtmlInputButton btnCouponAdd;

		protected Repeater gvCouponList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.CouponGetList();
			}
		}

		public void CouponGetList()
		{
			Coupon coupon = new Coupon();
			int Counts = this.NetPagerParameter.RecordCount;
			string strWhere = " 1=1 ";
			if (this._UserShopID != 1)
			{
				strWhere = strWhere + " and " + PubFunction.GetMemListShopAuthority(this._UserShopID, "CouponShopID", strWhere);
			}
			DataTable db = coupon.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
			{
				strWhere
			}).Tables[0];
			this.NetPagerParameter.RecordCount = Counts;
			this.gvCouponList.DataSource = db;
			this.gvCouponList.DataBind();
			PageBase.BindSerialRepeater(this.gvCouponList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.CouponGetList();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.CouponGetList();
		}

		public string GetCouponEffective(string strType, string strStartTime, string strEndTime)
		{
			string strResult = string.Empty;
			if (strType == "0")
			{
				strResult = "<span style='color:red;'>永久有效</span>";
			}
			else
			{
				strResult = Convert.ToDateTime(strStartTime).ToShortDateString() + "至" + Convert.ToDateTime(strEndTime).ToShortDateString();
			}
			return strResult;
		}
	}
}
