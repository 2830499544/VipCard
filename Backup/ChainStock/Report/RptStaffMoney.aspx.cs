using Chain.BLL;
using ChainStock.Controls;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.Report
{
	public class RptStaffMoney : PageBase
	{
		private StaffMoney bllStaffMoney = new StaffMoney();

		private Staff bllStaff = new Staff();

		protected HtmlForm frmRptStaffMoney;

		protected Literal ltlTitle;

		protected HtmlInputText txtMemStartTime;

		protected HtmlInputText txtMemEndTime;

		protected HtmlSelect sltStaffShopID;

		protected Button btnRptStaffMoneyExcel;

		protected HtmlInputText txtQueryStaff;

		protected HtmlInputText txtStaffStartTime;

		protected HtmlInputText txtStaffEndTime;

		protected HtmlSelect sltStaffClassID;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected Button btnRptStaffMoneyQuery;

		protected Label lblStaffMoney;

		protected Repeater rptStaffMoneyList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch2;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindShopSelect(this._UserShopID, this.sltStaffShopID, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				PubFunction.BindStaffClass(this._UserShopID, this.sltStaffClassID, true);
				this.txtStaffStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtStaffEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				if (PubFunction.curParameter.dataAuthority != 0)
				{
					if (this._UserShopID > 1)
					{
						this.sltStaffShopID.Value = this._UserShopID.ToString();
						this.sltStaffShopID.Attributes.Add("disabled", "disabled");
					}
				}
				this.txtMemStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtMemEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				this.BindStaffTotalMoney();
				this.BindStaffMoneyList(this.QueryCondition());
			}
		}

		private void BindStaffTotalMoney()
		{
			StaffMoney bllStaffMoney = new StaffMoney();
			string strSql = this.QueryCondition();
			strSql += " and Staff.StaffClassID = StaffClass.ClassID and StaffClass.ClassShopID = SysShop.ShopID and StaffMoney.StaffID = Staff.StaffID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "ClassShopID", strSql);
			decimal dclStaffMoney = bllStaffMoney.GetStaffTotalMoney(strSql);
			this.lblStaffMoney.Text = dclStaffMoney.ToString("0.00");
		}

		private void BindStaffMoneyList(string strSql)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " and Staff.StaffClassID = StaffClass.ClassID and StaffClass.ClassShopID = SysShop.ShopID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "ClassShopID", strSql);
			StringBuilder strSb = new StringBuilder();
			strSb.Append(" 1=1 ");
			if (this.txtStaffStartTime.Value != "")
			{
				strSb.AppendFormat(" and StaffMoney.StaffCreateTime>='{0}'", DateTime.Parse(this.txtStaffStartTime.Value));
			}
			if (this.txtStaffEndTime.Value != "")
			{
				strSb.AppendFormat(" and StaffMoney.StaffCreateTime<='{0}'", DateTime.Parse(this.txtStaffEndTime.Value + " 23:59:59"));
			}
			DataTable dtStaff = this.bllStaff.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, strSb.ToString(), new string[]
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
			this.rptStaffMoneyList.DataSource = dtStaff;
			this.rptStaffMoneyList.DataBind();
			PageBase.BindSerialRepeater(this.rptStaffMoneyList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindStaffTotalMoney();
			this.BindStaffMoneyList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strQueryStaff = PubFunction.RemoveSpace(this.txtQueryStaff.Value);
			string strStaffClass = this.sltStaffClassID.Value;
			string strStaffShop = this.sltStaffShopID.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" 1=1");
			if (strQueryStaff != "")
			{
				strSql.AppendFormat(" and (StaffName like '%{0}%' or StaffNumber = '{0}' or StaffMobile = '{0}')", strQueryStaff);
			}
			if (strStaffClass != "")
			{
				strSql.AppendFormat(" and StaffClassID ={0}", int.Parse(strStaffClass));
			}
			if (strStaffShop != "")
			{
				strSql.AppendFormat(" and ShopID={0}", int.Parse(strStaffShop));
			}
			return strSql.ToString();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindStaffTotalMoney();
			this.BindStaffMoneyList(this.QueryCondition());
		}

		protected void btnRptStaffMoneyQuery_Click(object sender, EventArgs e)
		{
			this.BindStaffTotalMoney();
			this.BindStaffMoneyList(this.QueryCondition());
		}

		protected void rptStaffMoneyList_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				Repeater rptItem = (Repeater)e.Item.FindControl("rptStaffMoneyDetail");
				if (rptItem != null)
				{
					int Count = this.NetPagerParameter.RecordCount;
					string strSql = " StaffID=" + dr["StaffID"].ToString();
					StringBuilder strSb = new StringBuilder();
					strSb.Append(" and 1=1 ");
					if (this.txtStaffStartTime.Value != "")
					{
						strSb.AppendFormat(" and StaffCreateTime>='{0}'", DateTime.Parse(this.txtStaffStartTime.Value));
					}
					if (this.txtStaffEndTime.Value != "")
					{
						strSb.AppendFormat(" and StaffCreateTime<='{0}'", DateTime.Parse(this.txtStaffEndTime.Value + " 23:59:59"));
					}
					strSql += strSb;
					DataTable dt = this.bllStaffMoney.GetListSP(strSql).Tables[0];
					rptItem.DataSource = dt;
					rptItem.DataBind();
					foreach (RepeaterItem rp in rptItem.Items)
					{
						Label lblDetailNum = (Label)rp.FindControl("lblDetailNumber");
						lblDetailNum.Text = (rp.ItemIndex + 1).ToString();
					}
				}
			}
		}

		protected void btnRptStaffMoneyExcel_Click(object sender, EventArgs e)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			StringBuilder strDetail = new StringBuilder();
			strSql += " and Staff.StaffClassID = StaffClass.ClassID and StaffClass.ClassShopID = SysShop.ShopID";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "ClassShopID", strSql);
			StringBuilder strSb = new StringBuilder();
			strSb.Append(" 1=1 ");
			if (this.txtStaffStartTime.Value != "")
			{
				strSb.AppendFormat(" and StaffMoney.StaffCreateTime>='{0}'", DateTime.Parse(this.txtStaffStartTime.Value));
				strDetail.AppendFormat(" and  StaffCreateTime>='{0}'", DateTime.Parse(this.txtStaffStartTime.Value));
			}
			if (this.txtStaffEndTime.Value != "")
			{
				strSb.AppendFormat(" and StaffMoney.StaffCreateTime<='{0}'", DateTime.Parse(this.txtStaffEndTime.Value));
				strDetail.AppendFormat(" and StaffCreateTime<='{0}'", DateTime.Parse(this.txtStaffEndTime.Value));
			}
			DataTable dtStaff = this.bllStaff.GetListSP(100000, 1, out Counts, strSb.ToString(), new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.StaffMoney(dtStaff, this._UserName, strDetail.ToString());
		}

		protected string GetStaffCommission(int commissionType)
		{
			string result;
			switch (commissionType)
			{
			case 0:
				result = "商品消费提成";
				break;
			case 1:
				result = "会员登记提成";
				break;
			case 2:
				result = "会员冲次提成";
				break;
			case 3:
				result = "会员冲时提成";
				break;
			case 4:
				result = "会员冲次消费提成";
				break;
			case 5:
				result = "会员冲时消费提成";
				break;
			default:
				result = "未知类型";
				break;
			}
			return result;
		}
	}
}
