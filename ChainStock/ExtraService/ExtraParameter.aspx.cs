using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.ExtraService
{
	public class ExtraParameter : PageBase
	{
		protected HtmlForm frmExtraParameter;

		protected Literal ltlTitle;

		protected HtmlInputCheckBox chkSms;

		protected HtmlInputCheckBox chkMoneySms;

		protected HtmlInputCheckBox chkIsSmsShopName;

		protected TextBox txtSmsShopName;

		protected TextBox txtNotificationSMS;

		protected Label lblNotificationSMSPwd;

		protected TextBox txtNotificationSMSPwd;

		protected Label lblNotificationSmsOverNumber;

		protected HtmlInputCheckBox chkMarketingSMS;

		protected TextBox txtMarketingSMS;

		protected Label lblMarketingSMSPwd;

		protected TextBox txtMarketingSMSPwd;

		protected Label lblMarketingSMSOverNumber;

		protected HtmlInputCheckBox chkTel;

		protected HtmlInputCheckBox chkTelNoMember;

		protected HtmlInputCheckBox chkMMS;

		protected TextBox txtMMSSeries;

		protected TextBox txtMMSSeriesPwd;

		protected Label lblMMSSerialPwd;

		protected Label lblMMSOverNumber;

		protected HtmlInputCheckBox ckbIsAutoSendSMSByMemRegister;

		protected HtmlInputCheckBox ckbIsAutoSendMMSByMemRegister;

		protected HtmlInputCheckBox ckbIsAutoSendSMSByMemRecharge;

		protected HtmlInputCheckBox ckbIsAutoSendSMSByMemWithdraw;

		protected HtmlInputCheckBox ckbIsAutoSendSMSByMemGiftExchange;

		protected HtmlInputCheckBox ckbIsAutoSendSMSByMemPointChange;

		protected HtmlInputCheckBox ckbIsAutoSendSMSByCommodityConsumption;

		protected HtmlInputCheckBox ckbIsAutoSendSMSByFastConsumption;

		protected HtmlInputCheckBox ckbIsAutoSendSMSByMemRedTimes;

		protected HtmlInputCheckBox ckbIsAutoSendSMSByTimingConsumption;

		protected HtmlInputCheckBox ckbIsAutoSendSMSByStorageTiming;

		protected Button btnExtraParameter;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindParameter();
			}
		}

		protected void BindParameter()
		{
			this.chkSms.Checked = this.curParameter.bolSms;
			this.chkMoneySms.Checked = this.curParameter.bolMoneySms;
			this.chkIsSmsShopName.Checked = this.curParameter.bolIsSmsShopName;
			this.txtSmsShopName.Text = this.curParameter.strSmsShopName;
			this.txtNotificationSMS.Text = this.curParameter.strNotificationSMS;
			this.lblNotificationSMSPwd.Text = this.curParameter.strNotificationSMSPwd;
			this.chkTel.Checked = this.curParameter.bolTel;
			this.chkTelNoMember.Checked = this.curParameter.bolTelNoMember;
			this.chkMMS.Checked = this.curParameter.bolMMS;
			this.txtMMSSeries.Text = this.curParameter.strMMSSeries;
			this.lblMMSSerialPwd.Text = this.curParameter.strMMSSerialPwd;
			this.ckbIsAutoSendSMSByMemRegister.Checked = this.curParameter.bolAutoSendSMSByMemRegister;
			this.ckbIsAutoSendMMSByMemRegister.Checked = this.curParameter.bolAutoSendMMSByMemRegister;
			this.ckbIsAutoSendSMSByMemRecharge.Checked = this.curParameter.bolAutoSendSMSByMemRecharge;
			this.ckbIsAutoSendSMSByMemWithdraw.Checked = this.curParameter.bolAutoSendSMSByMemWithdraw;
			this.ckbIsAutoSendSMSByMemGiftExchange.Checked = this.curParameter.bolAutoSendSMSByMemGiftExchange;
			this.ckbIsAutoSendSMSByMemPointChange.Checked = this.curParameter.bolAutoSendSMSByMemPointChange;
			this.ckbIsAutoSendSMSByCommodityConsumption.Checked = this.curParameter.bolAutoSendSMSByCommodityConsumption;
			this.ckbIsAutoSendSMSByFastConsumption.Checked = this.curParameter.bolAutoSendSMSByFastConsumption;
			this.ckbIsAutoSendSMSByMemRedTimes.Checked = this.curParameter.bolAutoSendSMSByMemRedTimes;
			this.ckbIsAutoSendSMSByTimingConsumption.Checked = this.curParameter.bolAutoSendSMSByTimingConsumption;
			this.ckbIsAutoSendSMSByStorageTiming.Checked = this.curParameter.IsAutoSendSMSByStorageTiming;
			this.chkMarketingSMS.Checked = this.curParameter.bolMarketingSMS;
			this.txtMarketingSMS.Text = this.curParameter.strMarketingSmsSeries;
			this.lblMarketingSMSPwd.Text = this.curParameter.strMarketingSmsSerialPwd;
		}

		protected void btnExtraParameter_Click(object sender, EventArgs e)
		{
			Chain.Model.SysParameter modelParameter = new Chain.Model.SysParameter();
			modelParameter.ParameterID = 1;
			modelParameter.Sms = this.chkSms.Checked;
			modelParameter.MoneySms = this.chkMoneySms.Checked;
			modelParameter.IsSmsShopName = this.chkIsSmsShopName.Checked;
			modelParameter.SmsShopName = this.txtSmsShopName.Text.Trim();
			modelParameter.SmsSeries = this.txtNotificationSMS.Text.Trim();
			modelParameter.SmsSerialPwd = ((this.txtNotificationSMSPwd.Text.Trim() == "") ? this.curParameter.strNotificationSMSPwd : this.txtNotificationSMSPwd.Text.Trim());
			modelParameter.MMS = this.chkMMS.Checked;
			modelParameter.MMSSeries = this.txtMMSSeries.Text.Trim();
			modelParameter.MMSSerialPwd = ((this.txtMMSSeriesPwd.Text.Trim() == "") ? this.curParameter.strMMSSerialPwd : this.txtMMSSeriesPwd.Text.Trim());
			modelParameter.Tel = this.chkTel.Checked;
			modelParameter.TelNoMember = this.chkTelNoMember.Checked;
			modelParameter.IsAutoSendSMSByMemRegister = this.ckbIsAutoSendSMSByMemRegister.Checked;
			modelParameter.IsAutoSendMMSByMemRegister = this.ckbIsAutoSendMMSByMemRegister.Checked;
			modelParameter.IsAutoSendSMSByMemRecharge = this.ckbIsAutoSendSMSByMemRecharge.Checked;
			modelParameter.IsAutoSendSMSByMemWithdraw = this.ckbIsAutoSendSMSByMemWithdraw.Checked;
			modelParameter.IsAutoSendSMSByMemGiftExchange = this.ckbIsAutoSendSMSByMemGiftExchange.Checked;
			modelParameter.IsAutoSendSMSByMemPointChange = this.ckbIsAutoSendSMSByMemPointChange.Checked;
			modelParameter.IsAutoSendSMSByCommodityConsumption = this.ckbIsAutoSendSMSByCommodityConsumption.Checked;
			modelParameter.IsAutoSendSMSByFastConsumption = this.ckbIsAutoSendSMSByFastConsumption.Checked;
			modelParameter.IsAutoSendSMSByMemRedTimes = this.ckbIsAutoSendSMSByMemRedTimes.Checked;
			modelParameter.IsAutoSendSMSByTimingConsumption = this.ckbIsAutoSendSMSByTimingConsumption.Checked;
			modelParameter.IsAutoSendSMSByStorageTiming = this.ckbIsAutoSendSMSByStorageTiming.Checked;
			modelParameter.MarketingSMS = this.chkMarketingSMS.Checked;
			modelParameter.MarketingSmsSeries = this.txtMarketingSMS.Text.Trim();
			modelParameter.MarketingSmsSerialPwd = ((this.txtMarketingSMSPwd.Text.Trim() == "") ? this.curParameter.strMarketingSmsSerialPwd : this.txtMarketingSMSPwd.Text.Trim());
			Chain.BLL.SysParameter bllUpdateParameter = new Chain.BLL.SysParameter();
			if (bllUpdateParameter.UpdateExtraParameter(modelParameter))
			{
				PubFunction pub = new PubFunction();
				PubFunction.curParameter = pub.LoadSysParameter();
				this.curParameter = PubFunction.curParameter;
				PubFunction.SaveSysLog(this._UserID, 3, "增值服务-参数设置", "增值服务的参数设置成功", this._UserShopID, DateTime.Now, PubFunction.ipAdress);
				base.OutputWarn("保存成功！");
			}
			else
			{
				base.OutputWarn("系统异常，未保存数据，请再次点击保存！");
			}
		}
	}
}
