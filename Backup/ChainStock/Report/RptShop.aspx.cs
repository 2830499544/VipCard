using Chain.BLL;
using ChainStock.Controls;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Report
{
	public class RptShop : PageBase
	{
		private decimal sumOrderTotalMoney = 0m;

		private decimal sumOrderDiscountMoney = 0m;

		private decimal sumSRechargeMoney = 0m;

		private decimal sumFRechargeMoney = 0m;

		private int sumOrderPoint = 0;

		private int sumUsePoint = 0;

		private int sumPointNumber = 0;

		private int MemCount = 0;

		private decimal MemMoney = 0m;

		protected HtmlForm frmRptShop;

		protected Literal ltlTitle;

		protected Button btnShopExcel;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected Button btnShopSearch;

		protected Repeater gvRptShop;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				this.txtStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				if (PubFunction.curParameter.dataAuthority != 0)
				{
					if (this._UserShopID > 1)
					{
						this.sltShop.Value = this._UserShopID.ToString();
					}
				}
				DataTable dt = this.Get_ParameterList(this.QueryCondition());
				this.BindList(dt);
			}
		}

		protected void btnShopSearch_Click(object sender, EventArgs e)
		{
			DataTable dt = this.Get_ParameterList(this.QueryCondition());
			this.BindList(dt);
		}

		private DataTable Get_ParameterList(string strSql)
		{
			SysShop bllShop = new SysShop();
			string strStartTime;
			if (this.txtStartTime.Value == "")
			{
				strStartTime = DateTime.Now.AddYears(-50).ToString("yyyy-MM-dd");
			}
			else
			{
				strStartTime = Convert.ToDateTime(this.txtStartTime.Value).AddSeconds(1.0).ToString();
			}
			string strEndTime;
			if (this.txtEndTime.Value == "")
			{
				strEndTime = DateTime.Now.AddYears(50).ToString("yyyy-MM-dd");
			}
			else
			{
				strEndTime = Convert.ToDateTime(this.txtEndTime.Value).AddDays(1.0).AddSeconds(-1.0).ToString();
			}
			return bllShop.getTotalShop(strStartTime, strEndTime, new string[]
			{
				strSql
			}).Tables[0];
		}

		protected string QueryCondition()
		{
			string strMemShopID = this.sltShop.Value;
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" SysShop.ShopID>0 and SysShop.ShopState=0 and SysShop.ShopType=3 ");
			if (strMemShopID != "")
			{
				string strwhere = PubFunction.GetMemListShopAuthority(int.Parse(strMemShopID), "SysShop.ShopID", " 1=1 ");
				strSql.AppendFormat(" and {0} ", strwhere);
			}
			else
			{
				string strwhere = PubFunction.GetMemListShopAuthority(this._UserShopID, "SysShop.ShopID", " 1=1 ");
				strSql.AppendFormat(" and {0} ", strwhere);
			}
			return strSql.ToString();
		}

		private void BindList(DataTable dt)
		{
			this.gvRptShop.DataSource = dt;
			this.gvRptShop.DataBind();
			PageBase.BindSerialRepeater(this.gvRptShop, 0);
		}

		protected void btnShopExcel_Click(object sender, EventArgs e)
		{
			DataTable dt = this.Get_ParameterList(this.QueryCondition());
			DataExcelInfo.ShopReportExcel(dt, this._UserName);
		}

		protected void gvRptShop_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				string OrderTotalMoney = DataBinder.Eval(e.Item.DataItem, "sumOrderTotalMoney").ToString().Trim();
				if (!OrderTotalMoney.Equals(""))
				{
					this.sumOrderTotalMoney += Convert.ToDecimal(OrderTotalMoney);
				}
				string OrderDiscountMoney = DataBinder.Eval(e.Item.DataItem, "sumOrderDiscountMoney").ToString().Trim();
				if (!OrderDiscountMoney.Equals(""))
				{
					this.sumOrderDiscountMoney += Convert.ToDecimal(OrderDiscountMoney);
				}
				string SRechargeMoney = DataBinder.Eval(e.Item.DataItem, "sumSRechargeMoney").ToString().Trim();
				if (!SRechargeMoney.Equals(""))
				{
					this.sumSRechargeMoney += Convert.ToDecimal(SRechargeMoney);
				}
				string FRechargeMoney = DataBinder.Eval(e.Item.DataItem, "sumFRechargeMoney").ToString().Trim();
				if (!FRechargeMoney.Equals(""))
				{
					this.sumFRechargeMoney += Convert.ToDecimal(FRechargeMoney);
				}
				string OrderPoint = DataBinder.Eval(e.Item.DataItem, "sumOrderPoint").ToString().Trim();
				if (!OrderPoint.Equals(""))
				{
					this.sumOrderPoint += Convert.ToInt32(OrderPoint);
				}
				string UsePoint = DataBinder.Eval(e.Item.DataItem, "sumUsePoint").ToString().Trim();
				if (!UsePoint.Equals(""))
				{
					this.sumUsePoint += Convert.ToInt32(UsePoint);
				}
				string PointNumber = DataBinder.Eval(e.Item.DataItem, "sumPointNumber").ToString().Trim();
				if (!PointNumber.Equals(""))
				{
					this.sumPointNumber += Convert.ToInt32(PointNumber);
				}
				string Count = DataBinder.Eval(e.Item.DataItem, "MemCount").ToString().Trim();
				if (!Count.Equals(""))
				{
					this.MemCount += Convert.ToInt32(Count);
				}
				string Money = DataBinder.Eval(e.Item.DataItem, "MemMoney").ToString().Trim();
				if (!Money.Equals(""))
				{
					this.MemMoney += Convert.ToDecimal(Money);
				}
			}
			if (e.Item.ItemType == ListItemType.Footer)
			{
				if (e.Item.FindControl("lblSumOrderTotalMoney") != null)
				{
					Label lblSumOrderTotalMoney = (Label)e.Item.FindControl("lblSumOrderTotalMoney");
					lblSumOrderTotalMoney.Text = this.sumOrderTotalMoney.ToString("0.00");
				}
				if (e.Item.FindControl("lblSumOrderDiscountMoney") != null)
				{
					Label lblSumOrderDiscountMoney = (Label)e.Item.FindControl("lblSumOrderDiscountMoney");
					lblSumOrderDiscountMoney.Text = this.sumOrderDiscountMoney.ToString("0.00");
				}
				if (e.Item.FindControl("lblSumSRechargeMoney") != null)
				{
					Label lblSumSRechargeMoney = (Label)e.Item.FindControl("lblSumSRechargeMoney");
					lblSumSRechargeMoney.Text = this.sumSRechargeMoney.ToString("0.00");
				}
				if (e.Item.FindControl("lblSumFRechargeMoney") != null)
				{
					Label lblSumFRechargeMoney = (Label)e.Item.FindControl("lblSumFRechargeMoney");
					lblSumFRechargeMoney.Text = this.sumFRechargeMoney.ToString("0.00");
				}
				if (e.Item.FindControl("lblSumOrderPoint") != null)
				{
					Label lblSumOrderPoint = (Label)e.Item.FindControl("lblSumOrderPoint");
					lblSumOrderPoint.Text = this.sumOrderPoint.ToString();
				}
				if (e.Item.FindControl("lblSumUsePoint") != null)
				{
					Label lblSumUsePoint = (Label)e.Item.FindControl("lblSumUsePoint");
					lblSumUsePoint.Text = this.sumUsePoint.ToString();
				}
				if (e.Item.FindControl("lblSumPointNumber") != null)
				{
					Label lblSumPointNumber = (Label)e.Item.FindControl("lblSumPointNumber");
					lblSumPointNumber.Text = this.sumPointNumber.ToString();
				}
				if (e.Item.FindControl("lblMemCount") != null)
				{
					Label lblMemCount = (Label)e.Item.FindControl("lblMemCount");
					lblMemCount.Text = this.MemCount.ToString();
				}
				if (e.Item.FindControl("lblMemMoney") != null)
				{
					Label lblMemMoney = (Label)e.Item.FindControl("lblMemMoney");
					lblMemMoney.Text = this.MemMoney.ToString();
				}
			}
		}
	}
}
