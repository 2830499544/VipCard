using Chain.BLL;
using ChainStock.Controls;
using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Report
{
	public class RptMemRecharge : PageBase
	{
		protected HtmlForm frmRptMemRecharge;

		protected HtmlInputHidden PointNum;

		protected HtmlInputHidden Export;

		protected HtmlInputHidden Revokable;

		protected Literal ltlTitle;

		protected HtmlInputText txtMemStartTime;

		protected HtmlInputText txtMemEndTime;

		protected HtmlSelect sltShopChart;

		protected Button btnMemRechargeExcel;

		protected Label lblczMoney;

		protected Label lbzsMoney;

		protected HtmlInputText txtQueryMem;

		protected HtmlInputText txtRechargeAccount;

		protected HtmlInputText txtRemark;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltRecharge;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected HtmlSelect sltMoney;

		protected HtmlInputText txtMoney;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected Button btnRptMemRechargeQuery;

		protected HtmlInputText txtMRMem;

		protected HtmlSelect sltMRType;

		protected HtmlSelect sltMRShop;

		protected HtmlInputText txtMRStart;

		protected HtmlInputText txtMREnd;

		protected HtmlSelect sltOrderBy;

		protected HtmlSelect sltSRType;

		protected HtmlSelect sltSRShop;

		protected HtmlSelect sltSROrderBy;

		protected HtmlInputText txtSRStart;

		protected HtmlInputText txtSREnd;

		protected Label lblPrintTitle;

		protected Label lblPrintFoot;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltMRShop, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltSRShop, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShopChart, true);
				this.txtMoney.Value = "0";
				this.txtStartTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtMRStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtMREnd.Value = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtSRStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
				this.txtSREnd.Value = DateTime.Now.ToString("yyyy-MM-dd");
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
				this.BindRechargeMoney();
				PubFunction.AgainPrint(ref this.lblPrintTitle, ref this.lblPrintFoot, this._UserShopID);
				this.PointNum.Value = PubFunction.GetPointNum("JFBDBB");
				this.Revokable.Value = PubFunction.GetControlVisit(this._UserAuthority, "gvRptMemRecharge_hyRevoke", 21).ToString();
			}
		}

		private void BindRechargeMoney()
		{
			MemRecharge bllMemRecharge = new MemRecharge();
			string sbWhere = this.QueryCondition();
			sbWhere += "AND RechargeIsApprove=1 and MemRecharge.RechargeShopID = SysShop.ShopID and MemRecharge.RechargeMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and MemRecharge.RechargeUserID = SysUser.UserID";
			sbWhere = PubFunction.GetShopAuthority(this._UserShopID, "RechargeShopID", sbWhere);
			decimal czMoney = bllMemRecharge.GetRechargeMoney(sbWhere + string.Format(" and MemRecharge.RechargeCreateTime >= '{0}' and MemRecharge.RechargeCreateTime < '{1}'", this.txtStartTime.Value, PubFunction.TimeEndDay(this.txtEndTime.Value)));
			this.lblczMoney.Text = czMoney.ToString("f2");
			decimal zsMoney = bllMemRecharge.GetGiveMoney(sbWhere + string.Format(" and MemRecharge.RechargeCreateTime >= '{0}' and MemRecharge.RechargeCreateTime <  '{1}'", this.txtStartTime.Value, PubFunction.TimeEndDay(this.txtEndTime.Value)));
			this.lbzsMoney.Text = zsMoney.ToString("f2");
		}

		protected void btnMemRechargeExcel_Click(object sender, EventArgs e)
		{
			MemRecharge bllMemRecharge = new MemRecharge();
			string tag = this.Export.Value;
			string startTime = DateTime.Now.ToString("yyyy-MM") + "-01";
			string endTime = DateTime.Now.ToString();
			DateTime date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM") + "-01");
			if (DateTime.TryParse(this.txtMRStart.Value, out date))
			{
				startTime = date.ToString();
			}
			date = DateTime.Now;
			if (DateTime.TryParse(this.txtMREnd.Value, out date))
			{
				endTime = date.ToString();
			}
			string text = tag;
			if (text != null)
			{
				if (!(text == "R"))
				{
					if (!(text == "MR"))
					{
						if (text == "SR")
						{
							StringBuilder strSqlSR = new StringBuilder();
							strSqlSR.Append(string.Format(" RechargeCreateTime>='{0}' AND RechargeCreateTime< '{1}' ", startTime, PubFunction.TimeEndDay(this.txtEndTime.Value)));
							if (this.sltSRShop.Value != "")
							{
								strSqlSR.AppendFormat("and ShopID={0}", int.Parse(this.sltSRShop.Value));
							}
							if (this.sltSRType.Value != "")
							{
								strSqlSR.AppendFormat("and RechargeType={0} ", int.Parse(this.sltSRType.Value));
							}
							string orderBy = "RechargeMemID";
							bool isAsc = true;
							text = this.sltSROrderBy.Value;
							switch (text)
							{
							case "":
								orderBy = "RechargeShopID";
								isAsc = false;
								break;
							case "1":
								orderBy = "TotalMoney";
								isAsc = true;
								break;
							case "2":
								orderBy = "TotalMoney";
								isAsc = false;
								break;
							case "3":
								orderBy = "RechargeTotalMoney";
								isAsc = true;
								break;
							case "4":
								orderBy = "RechargeTotalMoney";
								isAsc = false;
								break;
							case "5":
								orderBy = "RechargeTotalGive";
								isAsc = true;
								break;
							case "6":
								orderBy = "RechargeTotalGive";
								isAsc = false;
								break;
							case "7":
								orderBy = "RechargeCount";
								isAsc = true;
								break;
							case "8":
								orderBy = "RechargeCount";
								isAsc = false;
								break;
							}
							int recordCounts;
							DataTable db = bllMemRecharge.GetListSP2(10000, 1, orderBy, isAsc, out recordCounts, new string[]
							{
								PubFunction.GetShopAuthority(this._UserShopID, "ShopID", strSqlSR.ToString())
							}).Tables[0];
							DataExcelInfo.ShopRechargeReportExcel(db, this._UserName);
						}
					}
					else
					{
						StringBuilder strSqlMR = new StringBuilder();
						strSqlMR.Append(string.Format(" RechargeCreateTime>='{0}' AND RechargeCreateTime<='{1}' ", startTime, PubFunction.TimeEndDay(this.txtEndTime.Value)));
						if (this.txtMRMem.Value != "")
						{
							strSqlMR.AppendFormat("and (MemCard = '{0}' or MemName like '%{0}%' or MemMobile = '{0}' or MemCardNumber = '{0}' )", this.txtMRMem.Value);
						}
						if (this.sltMRShop.Value != "")
						{
							strSqlMR.AppendFormat("and MemShopID={0}", int.Parse(this.sltMRShop.Value));
						}
						if (this.sltMRType.Value != "")
						{
							strSqlMR.AppendFormat("and RechargeType={0} ", int.Parse(this.sltMRType.Value));
						}
						string orderBy = "RechargeMemID";
						bool isAsc = true;
						text = this.sltOrderBy.Value;
						switch (text)
						{
						case "":
							orderBy = "RechargeMemID";
							isAsc = false;
							break;
						case "1":
							orderBy = "MemMoney";
							isAsc = true;
							break;
						case "2":
							orderBy = "MemMoney";
							isAsc = false;
							break;
						case "3":
							orderBy = "RechargeCount";
							isAsc = true;
							break;
						case "4":
							orderBy = "RechargeCount";
							isAsc = false;
							break;
						case "5":
							orderBy = "LastRechargeTime";
							isAsc = true;
							break;
						case "6":
							orderBy = "LastRechargeTime";
							isAsc = false;
							break;
						case "7":
							orderBy = "TotalMoney";
							isAsc = true;
							break;
						case "8":
							orderBy = "TotalMoney";
							isAsc = false;
							break;
						case "9":
							orderBy = "RechargeTotalMoney";
							isAsc = true;
							break;
						case "10":
							orderBy = "RechargeTotalMoney";
							isAsc = false;
							break;
						case "11":
							orderBy = "RechargeTotalGive";
							isAsc = true;
							break;
						case "12":
							orderBy = "RechargeTotalGive";
							isAsc = false;
							break;
						}
						int recordCounts;
						DataTable db = bllMemRecharge.GetListSP1(10000, 1, orderBy, isAsc, out recordCounts, new string[]
						{
							PubFunction.GetShopAuthority(this._UserShopID, "MemShopID", strSqlMR.ToString())
						}).Tables[0];
						DataExcelInfo.MemRechargeReportExcel(db, this._UserName);
					}
				}
				else
				{
					StringBuilder strSqlR = new StringBuilder();
					strSqlR.Append("1=1");
					if (this.txtQueryMem.Value != "")
					{
						strSqlR.AppendFormat("and (MemCard = '{0}' or MemName like '%{0}%' or MemMobile = '{0}' or MemCardNumber = '{0}' )", this.txtQueryMem.Value);
					}
					if (this.sltMemLevelID.Value != "")
					{
						strSqlR.AppendFormat("and MemLevelID={0}", int.Parse(this.sltMemLevelID.Value));
					}
					if (this.sltShop.Value != "")
					{
						strSqlR.AppendFormat("and RechargeShopID={0}", int.Parse(this.sltShop.Value));
					}
					if (int.Parse(this.txtMoney.Value) != 0)
					{
						strSqlR.AppendFormat("and RechargeMoney" + this.sltMoney.Value + "{0}", this.txtMoney.Value);
					}
					if (this.txtStartTime.Value != "")
					{
						strSqlR.AppendFormat("and RechargeCreateTime>='{0}' ", this.txtStartTime.Value);
					}
					if (this.txtEndTime.Value != "")
					{
						strSqlR.AppendFormat("and RechargeCreateTime< '{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
					}
					if (this.sltRecharge.Value != "")
					{
						strSqlR.AppendFormat("and RechargeType={0} ", int.Parse(this.sltRecharge.Value));
					}
					if (this.txtRemark.Value != "")
					{
						strSqlR.AppendFormat(" and RechargeRemark like '%{0}%' ", this.txtRemark.Value);
					}
					if (this.txtRechargeAccount.Value != "")
					{
						strSqlR.AppendFormat(" and RechargeAccount='{0}'", this.txtRechargeAccount.Value);
					}
					strSqlR.Append(" and MemRecharge.RechargeShopID = SysShop.ShopID and MemRecharge.RechargeMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and MemRecharge.RechargeUserID = SysUser.UserID");
					int recordCounts;
					DataTable db = bllMemRecharge.GetListSP(10000, 1, out recordCounts, new string[]
					{
						PubFunction.GetShopAuthority(this._UserShopID, "RechargeShopID", strSqlR.ToString())
					}).Tables[0];
					DataExcelInfo.RechargeReportExcel(db, this._UserName);
				}
			}
		}

		protected string QueryCondition()
		{
			string strQueryMem = this.txtQueryMem.Value;
			string strMemLevelID = this.sltMemLevelID.Value;
			string strMemShopID = this.sltShop.Value;
			string strMoneySymbols = this.sltMoney.Value;
			string strMoney = (this.txtMoney.Value.Trim() != "") ? this.txtMoney.Value.Trim() : "0";
			decimal dclMoney = decimal.Parse(strMoney);
			string strRechargeType = this.sltRecharge.Value;
			string strRechargeAccount = PubFunction.RemoveSpace(this.txtRechargeAccount.Value);
			string strRemark = PubFunction.RemoveSpace(this.txtRemark.Value);
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1");
			if (strQueryMem != "")
			{
				strSql.AppendFormat("and (MemCard = '{0}' or MemName like '%{0}%' or MemMobile = '{0}' or MemCardNumber = '{0}' )", strQueryMem);
			}
			if (strMemLevelID != "")
			{
				strSql.AppendFormat("and Mem.MemLevelID={0}", int.Parse(strMemLevelID));
			}
			if (strMemShopID != "")
			{
				strSql.AppendFormat("and RechargeShopID={0}", int.Parse(strMemShopID));
			}
			if (dclMoney != 0m)
			{
				strSql.AppendFormat("and RechargeMoney" + strMoneySymbols + "{0}", dclMoney);
			}
			if (this.txtStartTime.Value != "")
			{
				strSql.AppendFormat("and RechargeCreateTime>='{0}' ", this.txtStartTime.Value);
			}
			if (this.txtEndTime.Value != "")
			{
				strSql.AppendFormat("and RechargeCreateTime< '{0}'", PubFunction.TimeEndDay(this.txtEndTime.Value));
			}
			if (strRechargeType != "")
			{
				strSql.AppendFormat("and RechargeType={0} ", int.Parse(strRechargeType));
			}
			if (strRemark != "")
			{
				strSql.AppendFormat(" and RechargeRemark like '%{0}%' ", strRemark);
			}
			if (strRechargeAccount != "")
			{
				strSql.AppendFormat(" and RechargeAccount='{0}'", strRechargeAccount);
			}
			return strSql.ToString();
		}
	}
}
