using Chain.BLL;
using Chain.Model;
using ChainStock.Controls;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.Report
{
	public class RptGoodsExpense : PageBase
	{
		protected HtmlForm frmGoodsExpense;

		protected Literal ltlTitle;

		protected HtmlInputText txtgoodcode;

		protected HtmlSelect sltShopChart;

		protected HtmlInputHidden HDsltshop;

		protected HtmlInputText txtMemStartTime;

		protected HtmlInputText txtMemEndTime;

		protected HtmlGenericControl chart;

		protected Button btnGoodsExpenseExcel;

		protected HtmlInputText txtGoodsName;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected HtmlSelect sltGoodsType;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden txtGoodsClass;

		protected Button Button1;

		protected Repeater rptGoods;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.txtStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShopChart, true);
				this.txtMemStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtMemEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				if (PubFunction.curParameter.dataAuthority != 0)
				{
					if (this._UserShopID > 1)
					{
						this.sltShop.Value = this._UserShopID.ToString();
						this.sltShop.Attributes.Add("disabled", "disabled");
					}
				}
				this.Get_ParameterList(this.QueryCondition());
			}
		}

		private void Get_ParameterList(string strSql)
		{
			string strStartTime = this.txtStartTime.Value;
			string strEntTime = this.txtEndTime.Value;
			if (strStartTime != "")
			{
				strSql = strSql + " and  OrderCreateTime>='" + strStartTime + "'";
			}
			if (strEntTime != "")
			{
				strSql = strSql + " and OrderCreateTime<'" + PubFunction.TimeEndDay(strEntTime) + "'";
			}
			strSql += " AND  OrderLog.OrderType != 4 AND OrderLog.OrderType != 5 ";
			Chain.BLL.Goods bllGoods = new Chain.BLL.Goods();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "OrderLog.OrderShopID", strSql);
			DataTable dt = bllGoods.GetGoodsExpense(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.rptGoods.DataSource = dt;
			this.rptGoods.DataBind();
			PageBase.BindSerialRepeater(this.rptGoods, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.Get_ParameterList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			string strQueryGoods = this.txtGoodsName.Value;
			string strGoodsType = this.sltGoodsType.Value;
			string strShopID = this.sltShop.Value;
			string strGoodsClass = (base.Request["sltGoodsClass"] == null) ? "" : base.Request["sltGoodsClass"].Trim();
			this.txtGoodsClass.Value = strGoodsClass;
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" 1=1  ");
			if (strQueryGoods != "")
			{
				strSql.AppendFormat("and (GoodsCode like '%{0}%' or Name like '%{0}%' or NameCode like '%{0}%')", strQueryGoods);
			}
			if (strGoodsType != "")
			{
				strSql.AppendFormat("and GoodsType={0}", int.Parse(strGoodsType));
			}
			if (strGoodsClass != "")
			{
				Chain.Model.GoodsClass model = new Chain.BLL.GoodsClass().GetModel(int.Parse(strGoodsClass));
				if (model.ParentID == 0)
				{
					string strClass = int.Parse(strGoodsClass) + ",";
					DataTable dt = new Chain.BLL.GoodsClass().GetList("ParentID=" + int.Parse(strGoodsClass)).Tables[0];
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						strClass = strClass + dt.Rows[i]["ClassID"] + ",";
					}
					int length = strClass.Length;
					strClass = strClass.Substring(0, length - 1);
					strSql.AppendFormat(" and GoodsClassID in ({0})", strClass);
				}
				else
				{
					strSql.AppendFormat(" and GoodsClassID={0}", int.Parse(strGoodsClass));
				}
			}
			if (strShopID != "")
			{
				strSql.AppendFormat(" and OrderShopID={0}", int.Parse(strShopID));
			}
			return strSql.ToString();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Get_ParameterList(this.QueryCondition());
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.Get_ParameterList(this.QueryCondition());
		}

		protected string GetGoodsType(int intGoodsType, float strNumber)
		{
			string strGoodsType = "";
			switch (intGoodsType)
			{
			case 0:
				strGoodsType = "普通商品";
				break;
			case 1:
				strGoodsType = "服务项目";
				break;
			}
			if (strNumber < 0f)
			{
				strGoodsType = "会员计次";
			}
			return strGoodsType;
		}

		protected void btnGoodsExpenseExcel_Click(object sender, EventArgs e)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			string strWhere = " and " + this.QueryCondition();
			string strStartTime = this.txtStartTime.Value;
			string strEntTime = this.txtEndTime.Value;
			if (strStartTime != "")
			{
				strSql = strSql + " and  OrderCreateTime>='" + strStartTime + "'";
				strWhere = strWhere + " and  OrderCreateTime>='" + strEntTime + "'";
			}
			if (strEntTime != "")
			{
				DateTime strEntTimes = Convert.ToDateTime(strEntTime).AddDays(1.0);
				object obj = strSql;
				strSql = string.Concat(new object[]
				{
					obj,
					" and OrderCreateTime<'",
					strEntTimes,
					"'"
				});
				obj = strWhere;
				strWhere = string.Concat(new object[]
				{
					obj,
					" and OrderCreateTime<'",
					strEntTimes,
					"'"
				});
			}
			strSql += " and OrderLog.OrderType != 4 AND OrderLog.OrderType != 5 AND OrderLog.OrderType != 3";
			strSql = PubFunction.GetShopAuthority(this._UserShopID, "OrderLog.OrderShopID", strSql);
			Chain.BLL.Goods bllGoods = new Chain.BLL.Goods();
			DataTable dt = bllGoods.GetGoodsExpense(10000000, 1, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.GoodsExpensExcel(dt, this._UserName, strWhere);
		}
	}
}
