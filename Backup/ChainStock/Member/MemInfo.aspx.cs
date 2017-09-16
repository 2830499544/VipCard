using Chain.BLL;
using ChainStock.Controls;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class MemInfo : PageBase
	{
		private OrderLog bllOrderLog = new OrderLog();

		private OrderDetail bllOrderDetail = new OrderDetail();

		protected HtmlForm frmMemInfo;

		protected Literal ltlTitle;

		protected MemberSearch ucMemberSearch;

		protected HtmlInputButton btnEditMem;

		protected HtmlInputButton btnExpense;

		protected HtmlInputButton btGoodsExpense;

		protected HtmlInputButton btTimeExpense;

		protected HtmlInputButton btConsumeMemCount;

		protected HtmlInputButton btnRechargeMoney;

		protected HtmlInputButton btnRechargCount;

		protected HtmlInputButton btnExchangeGift;

		protected HtmlInputButton btnSendSMS;

		protected Label txtMemCard;

		protected Label txtMemName;

		protected Label txtMemCardNumber;

		protected Label txtMemMobile;

		protected Label txtMemMoney;

		protected Label txtMemPoint;

		protected Label txtMemLevel;

		protected Label txtMemState;

		protected Label txtMemShop;

		protected Label txtMemPastTime;

		protected Label txtMemBirthday;

		protected Label txtMemSex;

		protected Label txtMemIdentityCard;

		protected Label txtMemEmail;

		protected Label txtMemCreateTime;

		protected Label txtMemUserName;

		protected Label txtTelephone;

		protected Label txtMemRecommendCard;

		protected Label txtMemAddress;

		protected HtmlTextArea txtMemRemark;

		protected HtmlInputHidden MemCard;

		protected HtmlInputCheckBox chkIsPast;

		protected HtmlGenericControl MemInfoCustomField;

		protected HtmlImage imgMemPhoto;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["MemCard"] != null)
				{
					this.MemCard.Value = base.Request.QueryString["MemCard"];
				}
				this.chkIsPast.Checked = PubFunction.curParameter.bolPastTime;
				PubFunction.BindMemInfoCustomFields(this.MemInfoCustomField, "Mem", "MemInfo_Custom_");
				this.SetQuickBtn();
				this.ucMemberSearch.BolSenseICCard = this.curParameter.bolSenseiccard;
				this.ucMemberSearch.BolContactICCard = this.curParameter.bolContacticcard;
			}
		}

		private void SetQuickBtn()
		{
			DataTable dt = PubFunction.GetGroupAuthority(this._UserGroupID);
			this.btnEditMem.Visible = PubFunction.GetControlVisit(dt, int.Parse(this.btnEditMem.Attributes["cls"]), 3);
			this.btnExpense.Visible = PubFunction.GetPageVisit(dt, int.Parse(this.btnExpense.Attributes["cls"]));
			this.btGoodsExpense.Visible = PubFunction.GetPageVisit(dt, int.Parse(this.btGoodsExpense.Attributes["cls"]));
			this.btTimeExpense.Visible = PubFunction.GetPageVisit(dt, int.Parse(this.btTimeExpense.Attributes["cls"]));
			this.btConsumeMemCount.Visible = PubFunction.GetPageVisit(dt, int.Parse(this.btConsumeMemCount.Attributes["cls"]));
			this.btnRechargeMoney.Visible = PubFunction.GetPageVisit(dt, int.Parse(this.btnRechargeMoney.Attributes["cls"]));
			this.btnRechargCount.Visible = PubFunction.GetPageVisit(dt, int.Parse(this.btnRechargCount.Attributes["cls"]));
			this.btnExchangeGift.Visible = PubFunction.GetPageVisit(dt, int.Parse(this.btnExchangeGift.Attributes["cls"]));
			this.btnSendSMS.Visible = PubFunction.GetPageVisit(dt, int.Parse(this.btnSendSMS.Attributes["cls"]));
			this.btnSendSMS.Visible = PubFunction.curParameter.bolSms;
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
			return strGoodsType;
		}

		protected string GetOrderType(int intOrderType)
		{
			string strOrderType = "";
			switch (intOrderType)
			{
			case 0:
				strOrderType = "快速消费";
				break;
			case 1:
				strOrderType = "计时消费";
				break;
			case 2:
				strOrderType = "商品消费";
				break;
			}
			return strOrderType;
		}
	}
}
