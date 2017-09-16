using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock
{
	public class StartPage : PageBase
	{
		protected HtmlForm form1;

		protected HtmlInputHidden txtMemStartTime;

		protected HtmlInputHidden txtMemEndTime;

		protected HtmlInputHidden sltShop;

		protected Literal lblShopName;

		protected Literal lbGroupName;

		protected Literal ltlMemToday;

		protected Literal ltlMemYesterday;

		protected Literal lblrMoneyToday;

		protected Literal lblrMoneyYesterday;

		protected Literal lblGetMoneyToday;

		protected Literal lblGetMoneyYesterday;

		protected HtmlGenericControl lblPoint;

		protected Literal lblPointCount;

		protected HtmlGenericControl lblSms;

		protected Literal lblSmsCount;

		protected Literal lblShopMem;

		protected Literal lblShopPhone;

		protected Literal lblShopAddress;

		protected Repeater rpNotice;

		protected Repeater gvMemBirthday;

		protected Repeater gvMemPastTime;

		protected Repeater gvdMemPontReset;

		protected Repeater gvGoods;

		protected Repeater gvCustomRemind;

		private string strSql = "";

		private string strOrder = "";

		private int shopID = 0;

		private int UserGroupID = 0;

		private int userid = 0;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.shopID = this._UserShopID;
			this.UserGroupID = this._UserGroupID;
			this.userid = this._UserID;
			this.txtMemStartTime.Value = DateTime.Now.AddDays(-20.0).ToString("yyyy-MM-dd");
			this.txtMemEndTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
			this.strOrder = " SysNoticeTime desc";
			this.sltShop.Value = this.shopID.ToString();
			Chain.BLL.SysNotice bllNotice = new Chain.BLL.SysNotice();
			DataTable dtNotice = bllNotice.GetList(5, this.strSql, this.strOrder).Tables[0];
			this.rpNotice.DataSource = dtNotice;
			this.rpNotice.DataBind();
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			Chain.BLL.OrderLog bllLog = new Chain.BLL.OrderLog();
			Chain.BLL.SysShop bllShop = new Chain.BLL.SysShop();
			Chain.Model.SysShop modelShop = bllShop.GetModel(this.shopID);
			this.lblShopName.Text = modelShop.ShopName;
			this.lblShopAddress.Text = modelShop.ShopAddress;
			this.lblShopMem.Text = modelShop.ShopContactMan;
			this.lblShopPhone.Text = modelShop.ShopTelephone;
			if (this.shopID != 1 && PubFunction.curParameter.bolShopPointManage)
			{
				this.lblPointCount.Text = modelShop.PointCount.ToString();
			}
			else
			{
				this.lblPoint.Visible = false;
			}
			if (this.shopID != 1 && PubFunction.curParameter.bolShopSmsManage)
			{
				this.lblSmsCount.Text = modelShop.SmsCount.ToString();
			}
			else
			{
				this.lblSms.Visible = false;
			}
			Chain.Model.SysGroup modelSysGroup = new Chain.BLL.SysGroup().GetModel(this.UserGroupID);
			this.lbGroupName.Text = modelSysGroup.GroupName;
			Chain.BLL.MemRecharge bllMemRecharge = new Chain.BLL.MemRecharge();
			Chain.BLL.OrderLog bllOrderLog = new Chain.BLL.OrderLog();
			Chain.BLL.MemCount bllMemCount = new Chain.BLL.MemCount();
			string strMemToday = "CONVERT(varchar(10),MemCreateTime,120) = CONVERT(varchar(10),GETDATE(),120) AND MemID > 0";
			string strMemYesterday = "CONVERT(varchar(10),MemCreateTime,120) = CONVERT(varchar(10), DATEADD(day,-1,GETDATE()),120) AND MemID > 0";
			string strMoneyToday = "CONVERT(varchar(10),RechargeCreateTime,120) = CONVERT(varchar(10),GETDATE(),120)";
			string strMoneyYesterday = "CONVERT(varchar(10),RechargeCreateTime,120) = CONVERT(varchar(10), DATEADD(day,-1,GETDATE()),120)";
			string strGetMoneyToday = "CONVERT(varchar(10),OrderCreateTime,120) = CONVERT(varchar(10),GETDATE(),120)";
			string strGetMoneyYesterday = "CONVERT(varchar(10),OrderCreateTime,120) = CONVERT(varchar(10), DATEADD(day,-1,GETDATE()),120)";
			string strGetCountMoneyToday = "CONVERT(varchar(10),CountCreateTime,120) = CONVERT(varchar(10),GETDATE(),120)";
			string strGetCountMoneyYesterday = "CONVERT(varchar(10),CountCreateTime,120) = CONVERT(varchar(10), DATEADD(day,-1,GETDATE()),120)";
			if (modelShop.ShopID > 1)
			{
				strMemToday += string.Format(" AND MemShopID = {0}", this.shopID);
				strMemYesterday += string.Format(" AND MemShopID = {0}", this.shopID);
				strMoneyToday += string.Format(" AND RechargeShopID = {0}", this.shopID);
				strMoneyYesterday += string.Format(" AND RechargeShopID = {0}", this.shopID);
				strGetMoneyToday += string.Format(" AND OrderShopID = {0}", this.shopID);
				strGetMoneyYesterday += string.Format(" AND OrderShopID = {0}", this.shopID);
				strGetCountMoneyToday += string.Format(" AND CountShopID = {0}", this.shopID);
				strGetCountMoneyYesterday += string.Format(" AND CountShopID = {0}", this.shopID);
			}
			this.ltlMemToday.Text = bllMem.GetRecordCount(strMemToday).ToString();
			this.ltlMemYesterday.Text = bllMem.GetRecordCount(strMemYesterday).ToString();
			this.lblrMoneyToday.Text = bllMemRecharge.GetRecMoney(strMoneyToday).ToString("F2");
			this.lblrMoneyYesterday.Text = bllMemRecharge.GetRecMoney(strMoneyYesterday).ToString("F2");
			this.lblGetMoneyToday.Text = (bllOrderLog.GetTotalCash(strGetMoneyToday) + bllMemCount.GetTotalCash(strGetCountMoneyToday) + Convert.ToDecimal(this.lblrMoneyToday.Text)).ToString("F2");
			this.lblGetMoneyYesterday.Text = (bllOrderLog.GetTotalCash(strGetMoneyYesterday) + bllMemCount.GetTotalCash(strGetCountMoneyYesterday) + Convert.ToDecimal(this.lblrMoneyYesterday.Text)).ToString("F2");
			this.GetSysRemind();
		}

		private void GetSysRemind()
		{
			int count = 5;
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			DataTable dtBirhtday = bllMem.GetBirthdayList(0, this.shopID, count).Tables[0];
			this.gvMemBirthday.DataSource = dtBirhtday;
			this.gvMemBirthday.DataBind();
			StartPage.RepeaterBindSerial(this.gvMemBirthday, 0);
			DataTable dtMemPastTime = bllMem.GetMemPastTime(" and DATEDIFF(day,getdate(),MemPastTime) = 0 ", this.shopID, count).Tables[0];
			this.gvMemPastTime.DataSource = dtMemPastTime;
			this.gvMemPastTime.DataBind();
			StartPage.RepeaterBindSerial(this.gvMemPastTime, 0);
			DataTable dtMemPointRest = bllMem.GetMemPointReset(string.Format("MemPoint>0 and DATEDIFF(day,isnull(MemConsumeLastTime,MemCreateTime),getdate()) >= '{0}' and MemShopID = '{1}' ", PubFunction.curParameter.intPointPeriod, this.shopID), 0, count).Tables[0];
			this.gvdMemPontReset.DataSource = dtMemPointRest;
			this.gvdMemPontReset.DataBind();
			StartPage.RepeaterBindSerial(this.gvdMemPontReset, 0);
			DataTable dtGoods = new Chain.BLL.Goods().GetStockRemind(string.Format("Number < = '{0}' and GoodsType = '0' and ShopID = '{1}'", PubFunction.curParameter.intStockCount, this.shopID), count).Tables[0];
			this.gvGoods.DataSource = dtGoods;
			this.gvGoods.DataBind();
			StartPage.RepeaterBindSerial(this.gvGoods, 0);
			DataTable dtCustomRemind = new Chain.BLL.SysCustomRemind().GetList("CustomReminder like '%" + PubFunction.UserIDTOName(this.userid) + "%' and DATEDIFF(day,CustomRemindTime,getdate())<=0 ", count).Tables[0];
			this.gvCustomRemind.DataSource = dtCustomRemind;
			this.gvCustomRemind.DataBind();
			StartPage.RepeaterBindSerial(this.gvCustomRemind, 0);
		}

		public static void RepeaterBindSerial(Repeater rpt, int StartIndex)
		{
			if (rpt != null)
			{
				if (StartIndex > -1)
				{
					foreach (RepeaterItem rpi in rpt.Items)
					{
						Literal lblNum = (Literal)rpi.FindControl("lblNumber");
						lblNum.Text = (StartIndex + rpi.ItemIndex + 1).ToString();
					}
				}
			}
		}
	}
}
