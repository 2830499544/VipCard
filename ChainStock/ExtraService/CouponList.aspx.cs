using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.ExtraService
{
	public class CouponList : PageBase
	{
		private static Random r = new Random();

		protected HtmlHead Head1;

		protected HtmlForm frmCouponList;

		protected Literal ltlTitle;

		protected Literal txtCouponTitle;

		protected HtmlGenericControl spCouponID;

		protected Literal txtCouponType;

		protected HtmlGenericControl spNumber;

		protected Literal txtCouponNumber;

		protected Literal txtCouponPredictNu;

		protected Literal txtCouponDayNum;

		protected Literal txtCouponMinMoney;

		protected Literal txtCouponYF;

		protected Literal txtCouponSY;

		protected Literal txtCouponStart;

		protected HtmlGenericControl lblCouponYX;

		protected Literal txtCouponEnd;

		protected Button btnCouponExcel;

		protected HtmlInputText txtQueryCoupon;

		protected HtmlSelect sltSendType;

		protected HtmlSelect sltUserType;

		protected HtmlInputText txtSendStartTime;

		protected HtmlInputText txtSendEndTime;

		protected HtmlInputText txtUseStartTime;

		protected HtmlInputText txtUseEndTime;

		protected Button btSerch;

		protected Repeater gvCouponList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindCouponData();
			}
		}

		public void BindCouponData()
		{
			int Gid = int.Parse(base.Request["Gid"]);
			this.spCouponID.InnerText = Gid.ToString();
			Chain.BLL.Coupon coupon = new Chain.BLL.Coupon();
			Chain.Model.Coupon model = coupon.GetModel(Gid);
			this.txtCouponTitle.Text = model.CouponTitle;
			this.txtCouponNumber.Text = model.CouponNumber.ToString();
			this.txtCouponDayNum.Text = model.CouponDayNum.ToString();
			this.txtCouponMinMoney.Text = model.CouponMinMoney.ToString("0.00");
			this.txtCouponPredictNu.Text = model.CouponPredictNu.ToString();
			this.txtCouponSY.Text = model.CouponSY.ToString();
			this.txtCouponYF.Text = model.CouponYF.ToString();
			if (model.CouponType > 0)
			{
				this.txtCouponType.Text = "折扣券";
				this.spNumber.InnerText = "折扣比例：";
			}
			else
			{
				this.txtCouponType.Text = "代金券";
				this.spNumber.InnerText = "优惠金额：";
			}
			if (model.CouponEffective > 0)
			{
				this.lblCouponYX.Visible = true;
				this.txtCouponStart.Text = Convert.ToDateTime(model.CouponStart).ToShortDateString();
				this.txtCouponEnd.Text = Convert.ToDateTime(model.CouponEnd).ToShortDateString();
				this.txtCouponEnd.Visible = true;
			}
			else
			{
				this.txtCouponStart.Text = "永久有效";
			}
			this.CouponGetList(this.QueryCondition());
		}

		public void CouponGetList(string strSql)
		{
			Chain.BLL.CouponList bllcoupon = new Chain.BLL.CouponList();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql = strSql + " and CouPonID=" + this.spCouponID.InnerText;
			DataTable db = bllcoupon.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			this.NetPagerParameter.RecordCount = Counts;
			this.gvCouponList.DataSource = db;
			this.gvCouponList.DataBind();
			PageBase.BindSerialRepeater(this.gvCouponList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void btnCouponExcel_Click(object sender, EventArgs e)
		{
			Chain.BLL.CouponList bllcoupon = new Chain.BLL.CouponList();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql = strSql + " and CouPonID=" + this.spCouponID.InnerText;
			DataTable db = bllcoupon.GetListSP(1000000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			int CouponID = int.Parse(this.spCouponID.InnerText);
			Chain.BLL.Coupon coupon = new Chain.BLL.Coupon();
			Chain.Model.Coupon model = coupon.GetModel(CouponID);
			DataExcelInfo.Coupon(db, this._UserName, model);
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.CouponGetList(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.CouponGetList(this.QueryCondition());
		}

		protected void btSerch_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.CouponGetList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strQueryCoupon = this.txtQueryCoupon.Value;
			string strSendType = this.sltSendType.Value;
			string strUseType = this.sltUserType.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" 1=1 ");
			if (strQueryCoupon != "")
			{
				strSql.AppendFormat(" and (CouponTitle like '%{0}%' or CouPon like '%{0}%' )", strQueryCoupon);
			}
			if (strSendType != "")
			{
				strSql.AppendFormat(" and CouPonYF={0}", strSendType);
			}
			if (strUseType != "")
			{
				strSql.AppendFormat(" and CouPonSY={0}", strUseType);
			}
			if (this.txtSendStartTime.Value != "")
			{
				strSql.AppendFormat(" and ConPonSendTime>='{0}'", this.txtSendStartTime.Value);
			}
			if (this.txtSendEndTime.Value != "")
			{
				strSql.AppendFormat(" and  ConPonSendTime<='{0}'", this.txtSendEndTime.Value);
			}
			if (this.txtUseStartTime.Value != "")
			{
				strSql.AppendFormat(" and ConPonUseTime>='{0}'", this.txtUseStartTime.Value);
			}
			if (this.txtUseEndTime.Value != "")
			{
				strSql.AppendFormat(" and ConPonUseTime<='{0}'", this.txtUseEndTime.Value);
			}
			return strSql.ToString();
		}
	}
}
