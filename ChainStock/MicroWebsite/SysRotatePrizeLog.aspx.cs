using Chain.BLL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.MicroWebsite
{
	public class SysRotatePrizeLog : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmMicroExpHistory;

		protected HtmlInputHidden txtUser;

		protected HtmlInputHidden txtShopid;

		protected Literal ltlTitle;

		protected Button btnExpenseExcel;

		protected Repeater gvSysRotatePrizeLog;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected HtmlInputCheckBox chkSMS;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.txtUser.Value = this._UserName;
				this.txtShopid.Value = this._UserShopID.ToString();
				this.BindStatus();
				this.BindExpenseHistory(this.QueryCondition());
				this.chkSMS.Checked = (this.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByCommodityConsumption);
			}
		}

		protected string BindStatus(object status)
		{
			string result = "";
			string text = status.ToString();
			if (text != null)
			{
				if (!(text == "0"))
				{
					if (text == "1")
					{
						result = "已领取";
					}
				}
				else
				{
					result = "待领取";
				}
			}
			return result;
		}

		protected string BindPrizeName(object prizeLevel, object rotateID)
		{
			string result = "";
			Chain.BLL.SysRotate bllSysRotate = new Chain.BLL.SysRotate();
			Chain.Model.SysRotate modelSysRotate = bllSysRotate.GetModel(int.Parse(rotateID.ToString()));
			if (modelSysRotate != null)
			{
				string text = prizeLevel.ToString();
				if (text != null)
				{
					if (!(text == "一等奖"))
					{
						if (!(text == "二等奖"))
						{
							if (!(text == "三等奖"))
							{
								if (!(text == "四等奖"))
								{
									if (!(text == "五等奖"))
									{
										if (text == "六等奖")
										{
											result = modelSysRotate.SixPrizeName;
										}
									}
									else
									{
										result = modelSysRotate.FivePrizeName;
									}
								}
								else
								{
									result = modelSysRotate.FourPrizeName;
								}
							}
							else
							{
								result = modelSysRotate.ThreePrizeName;
							}
						}
						else
						{
							result = modelSysRotate.TwoPrizeName;
						}
					}
					else
					{
						result = modelSysRotate.OnePrizeName;
					}
				}
			}
			return result;
		}

		private void BindExpenseHistory(string strSql)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " and Mem.MemShopID=SysShop.ShopID and SysRotatePrizeLog.MemID = Mem.MemID and SysRotatePrizeLog.RotateID=SysRotate.RotateID and Mem.MemLevelID=MemLevel.LevelID and SysRotatePrizeLog.PrizeLevel<>'未中奖'";
			DataTable dtExpenseHistory = new Chain.BLL.SysRotatePrizeLog().GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
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
			this.gvSysRotatePrizeLog.DataSource = dtExpenseHistory;
			this.gvSysRotatePrizeLog.DataBind();
			PageBase.BindSerialRepeater(this.gvSysRotatePrizeLog, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		protected string QueryCondition()
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("1=1");
			if (base.Request.QueryString["RotateID"] != null)
			{
				strSql.AppendFormat(" and SysRotatePrizeLog.RotateID={0} ", int.Parse(base.Request.QueryString["RotateID"]));
			}
			if (base.Request.QueryString["MemID"] != null)
			{
				strSql.AppendFormat(" and SysRotatePrizeLog.MemID={0} ", int.Parse(base.Request.QueryString["MemID"]));
			}
			return strSql.ToString();
		}

		protected string GetMemCard(string strMemCard)
		{
			string memCard;
			if (strMemCard == "0")
			{
				memCard = "无卡号";
			}
			else
			{
				memCard = strMemCard;
			}
			return memCard;
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.BindExpenseHistory(this.QueryCondition());
		}

		protected void btnRptExpenseQuery_Click(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.BindExpenseHistory(this.QueryCondition());
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.BindExpenseHistory(this.QueryCondition());
		}

		protected void rptExpenseHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
		}

		protected void btnExpenseExcel_Click(object sender, EventArgs e)
		{
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += " and Mem.MemShopID=SysShop.ShopID  and SysRotatePrizeLog.MemID = Mem.MemID and SysRotatePrizeLog.RotateID=SysRotate.RotateID and Mem.MemLevelID=MemLevel.LevelID and SysRotatePrizeLog.PrizeLevel<>'未中奖'";
			DataTable dtExpenseHistory = new Chain.BLL.SysRotatePrizeLog().GetListSP(100000, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
			{
				strSql
			}).Tables[0];
			DataExcelInfo.SysRotatePrizeLogExcel(dtExpenseHistory, this._UserName);
		}

		private void BindStatus()
		{
			List<ListItem> list = new List<ListItem>();
			list.Add(new ListItem("==== 请选择 ==== ", "0"));
			list.Add(new ListItem("待领取", "0"));
			list.Add(new ListItem("已领取", "1"));
		}
	}
}
