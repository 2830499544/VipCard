using Chain.BLL;
using Chain.Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.SystemManage
{
	public class SysParameter : PageBase
	{
		protected HtmlForm frmSysParameter;

		protected Literal ltlTitle;

		protected HtmlGenericControl tab8;

		protected HtmlInputCheckBox ckbRegNullPwd;

		protected HtmlInputCheckBox chkPwd;

		protected HtmlInputCheckBox chkIsNeedPwd;

		protected HtmlInputCheckBox chkMoneyAndPoint;

		protected HtmlInputCheckBox chkAutoLevel;

		protected CheckBox chkDegradeLevel;

		protected HtmlInputCheckBox chkPastTime;

		protected HtmlInputCheckBox chkPointLevel;

		protected HtmlInputCheckBox chkIsMustSlotCard;

		protected TextBox txtRecommendPoint;

		protected TextBox txtDrawMoneyPercent;

		protected TextBox txtPointPeriod;

		protected TextBox txtStockCount;

		protected TextBox txtUnitList;

		protected TextBox txtPointDrawPercent;

		protected TextBox txtPointDiscountPercent;

		protected TextBox txtPointUsePercent;

		protected TextBox txtAllianceRebatePercent;

		protected TextBox txtCardShopRebatePercent;

		protected TextBox txtGiveMemMoneyRate;

		protected TextBox txtGoodsInPrefix;

		protected TextBox txtGoodsAllotPrefix;

		protected TextBox txtExpensePrefix;

		protected TextBox txtGoodsExpensePrefix;

		protected TextBox txtTimeExpensePrefix;

		protected TextBox txtStorageTimingPrefix;

		protected TextBox txtMemCountPrefix;

		protected TextBox txtMemRechargePrefix;

		protected TextBox txtMemDrawMoneyPrefix;

		protected TextBox txtGiftExchangePrefix;

		protected TextBox txtMemPointChangePrefix;

		protected TextBox txtMemCountExpensePrefix;

		protected HtmlInputCheckBox chkPayCard;

		protected HtmlInputCheckBox chkPayCash;

		protected HtmlInputCheckBox chkPayBink;

		protected HtmlInputCheckBox chkPayPoint;

		protected HtmlInputCheckBox chkPayCoupon;

		protected HtmlInputCheckBox chkMemRegisterStaff;

		protected HtmlInputCheckBox chkStaff;

		protected HtmlInputRadioButton rdStaff;

		protected HtmlInputRadioButton rdGoods;

		protected HtmlInputCheckBox chkAutoPrint;

		protected HtmlInputCheckBox PrintPreview;

		protected HtmlInputCheckBox A4ShiZhi;

		protected HtmlInputCheckBox SanLianShiZhi;

		protected HtmlInputCheckBox POs58ShiZhi;

		protected HtmlInputCheckBox POs80ShiZhi;

		protected HtmlInputCheckBox chkAccordPrint;

		protected TextBox txtPrintTitle;

		protected TextBox txtPrintFootNote;

		protected TextBox Txthycz;

		protected TextBox Txtjfbd;

		protected TextBox Txtjfdh;

		protected TextBox Txtsprk;

		protected TextBox Txtspxf;

		protected TextBox Txtjcxf;

		protected TextBox Txtksxf;

		protected TextBox Txthycc;

		protected TextBox Txthycs;

		protected TextBox Txtxfjl;

		protected TextBox AccountsToMoney;

		protected TextBox Txthyczbb;

		protected TextBox Txtjfbdbb;

		protected TextBox Txtjfdhbb;

		protected TextBox Txtckrkmx;

		protected HtmlInputCheckBox chkEmail;

		protected HtmlInputCheckBox chkMoneyEmail;

		protected TextBox txtEmailUserName;

		protected TextBox txtEmailAdress;

		protected TextBox txtEmailPwd;

		protected Label lblEmailPwd;

		protected TextBox txtEmailSMTP;

		protected TextBox txtEnterpriseEmailPort;

		protected TextBox txtEnterpriseEmailDisplayName;

		protected HtmlInputCheckBox chkEnterpriseEmailEnableSSL;

		protected HtmlInputCheckBox chkEnterpriseEmailUseDefaultCredentials;

		protected TextBox txtSellerAccount;

		protected TextBox txtPartnerID;

		protected TextBox txtPartnerKey;

		protected Label lblPartnerKey;

		protected HtmlInputCheckBox chkIsAutoSendSMSByMemPast;

		protected TextBox txtAutoSendSMSByMemPastForDay;

		protected HtmlInputCheckBox chksAutoSendSMSByMemBirthday;

		protected TextBox txtAutoSendSMSByMemBirthdayForDay;

		protected HtmlInputCheckBox CKautoBackup;

		protected TextBox TBautoBackupDay;

		protected HtmlInputCheckBox chkIsStartWeiXin;

		protected HtmlTableRow lblIsStartTiming;

		protected HtmlInputCheckBox chkIsStartTimingProject;

		protected HtmlTableRow lblIsStartMemCount;

		protected HtmlInputCheckBox chkIsStartMemCount;

		protected HtmlTableRow lblIsSettlement;

		protected HtmlInputCheckBox chkIsSettlement;

		protected HtmlTableRow lblIsStartSendCard;

		protected HtmlInputCheckBox chkIsSendCard;

		protected HtmlTableRow lblIsStartShopSmsMenage;

		protected HtmlInputCheckBox chkShopSmsManage;

		protected HtmlTableRow lblIsStartShopPointMenage;

		protected HtmlInputCheckBox chkShopPointManage;

		protected CheckBox chkSenseiccard;

		protected CheckBox chkContacticcard;

		protected HtmlImage imgMainPhoto;

		protected HtmlInputHidden txtMainPhoto;

		protected HtmlImage imgIndexLogo;

		protected HtmlInputHidden txtIndexLogo;

		protected HtmlImage imgMainLogin;

		protected HtmlInputHidden txtMainLogin;

		protected HtmlImage imgSelfPhoto;

		protected HtmlInputHidden txtSelfPhoto;

		protected HtmlImage imgSelfLogin;

		protected HtmlInputHidden txtSelfLogin;

		protected Button Button1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this._UserID == 1)
				{
				}
				this.BindParameter();
				if (!PubFunction.curParameter.UsingUnion)
				{
					this.chkIsSendCard.Checked = false;
					this.lblIsStartSendCard.Attributes.Add("style", "display:none");
					this.chkShopSmsManage.Checked = false;
					this.lblIsStartShopSmsMenage.Attributes.Add("style", "display:none");
					this.chkShopPointManage.Checked = false;
					this.lblIsStartShopPointMenage.Attributes.Add("style", "display:none");
					this.chkIsSettlement.Checked = false;
					this.lblIsSettlement.Attributes.Add("style", "display:none");
				}
			}
		}

		protected void BindParameter()
		{
			this.chkPwd.Checked = this.curParameter.bolPwd;
			this.ckbRegNullPwd.Checked = this.curParameter.RegNullPwd;
			this.chkMoneyAndPoint.Checked = this.curParameter.bolMoneyAndPoint;
			this.chkAutoLevel.Checked = this.curParameter.bolAutoLevel;
			this.chkDegradeLevel.Checked = this.curParameter.bolDegradeLevel;
			this.chkPastTime.Checked = this.curParameter.bolPastTime;
			this.txtGiveMemMoneyRate.Text = this.curParameter.dclGiveMemMoneyRate.ToString();
			this.txtRecommendPoint.Text = this.curParameter.intRecommendPoint.ToString();
			this.txtDrawMoneyPercent.Text = this.curParameter.DrawMoneyPercent.ToString();
			this.txtPointPeriod.Text = this.curParameter.intPointPeriod.ToString();
			this.txtStockCount.Text = this.curParameter.intStockCount.ToString();
			this.txtExpensePrefix.Text = this.curParameter.strExpensePrefix;
			this.txtGoodsExpensePrefix.Text = this.curParameter.strGoodsExpensePrefix;
			this.txtTimeExpensePrefix.Text = this.curParameter.strTimeExpensePrefix;
			this.txtGoodsAllotPrefix.Text = this.curParameter.strGoodsAllotPrefix;
			this.txtGoodsInPrefix.Text = this.curParameter.strGoodsInPrefix;
			this.txtMemCountPrefix.Text = this.curParameter.strMemCountPrefix;
			this.txtMemRechargePrefix.Text = this.curParameter.strMemRechargePrefix;
			this.txtMemDrawMoneyPrefix.Text = this.curParameter.strMemDrawMoneyPrefix;
			this.txtMemPointChangePrefix.Text = this.curParameter.strMemPointChangePrefix;
			this.txtGiftExchangePrefix.Text = this.curParameter.strGiftExchangePrefix;
			this.txtStorageTimingPrefix.Text = this.curParameter.StorageTimingPrefix;
			this.chkAutoPrint.Checked = this.curParameter.bolAutoPrint;
			this.chkAccordPrint.Checked = this.curParameter.bolAccordPrint;
			this.txtUnitList.Text = this.curParameter.strUnitList;
			this.chkPayCard.Checked = this.curParameter.bolIsPayCard;
			this.chkPayCash.Checked = this.curParameter.bolIsPayCash;
			this.chkPayBink.Checked = this.curParameter.bolIsPayBink;
			this.chkPayCoupon.Checked = this.curParameter.bolIsPayCoupon;
			this.chkPayPoint.Checked = this.curParameter.bolIsPayPoint;
			this.txtPrintTitle.Text = this.curParameter.strPrintTitle;
			this.txtPrintFootNote.Text = this.curParameter.strPrintFootNote;
			this.chkStaff.Checked = this.curParameter.bolStaff;
			this.chkSenseiccard.Checked = this.curParameter.bolSenseiccard;
			this.chkContacticcard.Checked = this.curParameter.bolContacticcard;
			this.txtPointDrawPercent.Text = this.curParameter.PointDrawPercent.ToString();
			this.txtAllianceRebatePercent.Text = this.curParameter.AllianceRebatePercent.ToString();
			this.txtCardShopRebatePercent.Text = this.curParameter.CardShopRebatePercent.ToString();
			if (this.curParameter.PrintPreview == 1)
			{
				this.PrintPreview.Checked = true;
			}
			else
			{
				this.PrintPreview.Checked = false;
			}
			if (this.curParameter.PrintPaperType == 0)
			{
				this.A4ShiZhi.Checked = true;
			}
			if (this.curParameter.PrintPaperType == 1)
			{
				this.SanLianShiZhi.Checked = true;
			}
			if (this.curParameter.PrintPaperType == 2)
			{
				this.POs58ShiZhi.Checked = true;
			}
			if (this.curParameter.PrintPaperType == 3)
			{
				this.POs80ShiZhi.Checked = true;
			}
			if (this.curParameter.AutoBackupDB)
			{
				this.CKautoBackup.Checked = true;
			}
			else
			{
				this.CKautoBackup.Checked = false;
			}
			this.TBautoBackupDay.Text = this.curParameter.AutoBackupDay.ToString();
			if (this.curParameter.bolStaffType)
			{
				this.rdStaff.Checked = true;
			}
			else
			{
				this.rdGoods.Checked = true;
			}
			this.chkPointLevel.Checked = this.curParameter.chkPointLevel;
			this.chkEmail.Checked = this.curParameter.bolIsEmail;
			this.chkMoneyEmail.Checked = this.curParameter.bolIsEmailNotice;
			this.txtEmailAdress.Text = this.curParameter.EmailAdress;
			this.lblEmailPwd.Text = this.curParameter.EmailPwd;
			this.txtEmailSMTP.Text = this.curParameter.EmailSMTP;
			this.txtEnterpriseEmailPort.Text = this.curParameter.EnterpriseEmailPort.ToString();
			this.txtEnterpriseEmailDisplayName.Text = this.curParameter.EnterpriseEmailDisplayName;
			this.chkEnterpriseEmailEnableSSL.Checked = this.curParameter.EnterpriseEmailEnableSSL;
			this.chkEnterpriseEmailUseDefaultCredentials.Checked = this.curParameter.EnterpriseEmailUseDefaultCredentials;
			this.txtSellerAccount.Text = this.curParameter.SellerAccount;
			this.txtPartnerID.Text = this.curParameter.PartnerID;
			this.lblPartnerKey.Text = this.curParameter.PartnerKey;
			this.chkIsNeedPwd.Checked = this.curParameter.IsEditPwdNeedOldPwd;
			this.chkMemRegisterStaff.Checked = this.curParameter.bolIsMemRegisterStaff;
			this.chkIsMustSlotCard.Checked = this.curParameter.IsMustSlotCard;
			this.txtMemCountExpensePrefix.Text = this.curParameter.strMemCountExpensePrefix;
			this.txtPointUsePercent.Text = this.curParameter.PointUsePercent.ToString();
			this.txtPointDiscountPercent.Text = this.curParameter.PointDiscountPercent.ToString();
			this.chkIsAutoSendSMSByMemPast.Checked = this.curParameter.bolIsAutoSendSMSByMemPast;
			this.txtAutoSendSMSByMemPastForDay.Text = this.curParameter.intAutoSendSMSByMemPastForDay.ToString();
			this.chksAutoSendSMSByMemBirthday.Checked = this.curParameter.bolIsAutoSendSMSByMemBirthday;
			this.txtAutoSendSMSByMemBirthdayForDay.Text = this.curParameter.intAutoSendSMSByMemBirthdayForDay.ToString();
			if (!PubFunction.curParameter.EnableGoods)
			{
				this.lblIsStartTiming.Visible = false;
				this.lblIsStartMemCount.Visible = false;
			}
			this.chkIsStartWeiXin.Checked = this.curParameter.bolIsStartWeiXin;
			this.chkIsStartTimingProject.Checked = this.curParameter.bolIsStartTimingProject;
			this.chkIsStartMemCount.Checked = this.curParameter.bolIsStartMemCount;
			this.txtEmailUserName.Text = this.curParameter.strEmailUserName;
			this.chkIsSendCard.Checked = this.curParameter.bolIsSendCard;
			this.chkShopSmsManage.Checked = this.curParameter.bolShopSmsManage;
			this.chkShopPointManage.Checked = this.curParameter.bolShopPointManage;
			this.chkIsSettlement.Checked = this.curParameter.bolIsSettlement;
			string strNumber = PubFunction.curParameter.PointNumStr;
			string[] Number = strNumber.Split(new char[]
			{
				'$'
			});
			this.Txthycz.Text = Number[0];
			this.Txtjfbd.Text = Number[1];
			this.Txtjfdh.Text = Number[2];
			this.Txtsprk.Text = Number[3];
			this.Txtspxf.Text = Number[4];
			this.Txtjcxf.Text = Number[5];
			this.Txtksxf.Text = Number[6];
			this.Txthycc.Text = Number[7];
			this.Txthycs.Text = Number[8];
			this.Txtxfjl.Text = Number[9];
			this.Txthyczbb.Text = Number[10];
			this.Txtjfbdbb.Text = Number[11];
			this.Txtjfdhbb.Text = Number[12];
			this.Txtckrkmx.Text = Number[13];
			this.AccountsToMoney.Text = Number[14];
		}

		protected void btnSysParameter_Click(object sender, EventArgs e)
		{
			string pointnum = "";
			pointnum = pointnum + this.Txthycz.Text + "$";
			pointnum = pointnum + this.Txtjfbd.Text + "$";
			pointnum = pointnum + this.Txtjfdh.Text + "$";
			pointnum = pointnum + this.Txtsprk.Text + "$";
			pointnum = pointnum + this.Txtspxf.Text + "$";
			pointnum = pointnum + this.Txtjcxf.Text + "$";
			pointnum = pointnum + this.Txtksxf.Text + "$";
			pointnum = pointnum + this.Txthycc.Text + "$";
			pointnum = pointnum + this.Txthycs.Text + "$";
			pointnum = pointnum + this.Txtxfjl.Text + "$";
			pointnum = pointnum + this.Txthyczbb.Text + "$";
			pointnum = pointnum + this.Txtjfbdbb.Text + "$";
			pointnum = pointnum + this.Txtjfdhbb.Text + "$";
			pointnum = pointnum + this.Txtckrkmx.Text + "$";
			pointnum = pointnum + this.AccountsToMoney.Text + "$";
			Chain.BLL.SysParameter bllUpdateParameter = new Chain.BLL.SysParameter();
			Chain.Model.SysParameter modelParameter = bllUpdateParameter.GetModel(1);
			modelParameter.ParameterID = 1;
			modelParameter.Pwd = this.chkPwd.Checked;
			modelParameter.RegNullPwd = this.ckbRegNullPwd.Checked;
			modelParameter.MoneyAndPoint = this.chkMoneyAndPoint.Checked;
			modelParameter.AutoLevel = this.chkAutoLevel.Checked;
			modelParameter.DegradeLevel = this.chkDegradeLevel.Checked;
			modelParameter.GiveMemMoneyRate = decimal.Parse(this.txtGiveMemMoneyRate.Text);
			modelParameter.PastTime = this.chkPastTime.Checked;
			string strRecommendPoint = this.txtRecommendPoint.Text.Trim();
			decimal dclDrawMoneyPercent = 0m;
			int result = 0;
			if (!int.TryParse(this.txtStockCount.Text.Trim(), out result))
			{
				base.OutputWarn("库存报警数量只能输入正整数！");
			}
			else if (!int.TryParse(this.txtPointPeriod.Text.Trim(), out result))
			{
				base.OutputWarn("积分到期清零周期只能输入正整数！");
			}
			else if (!decimal.TryParse(this.txtDrawMoneyPercent.Text.Trim(), out dclDrawMoneyPercent))
			{
				base.OutputWarn("账户提现折损率的取值范围只能是0至1之间的数字！");
			}
			else
			{
				Regex regex = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
				if (this.txtEmailAdress.Text != "" && !regex.IsMatch(this.txtEmailAdress.Text))
				{
					if (this.chkEmail.Checked)
					{
						base.OutputWarn("邮箱格式不正确，请重新输入！");
						return;
					}
					this.txtEmailAdress.Text = "";
				}
				if (this.txtEmailAdress.Text != "" && (this.txtEmailPwd.Text == "" || this.txtEmailSMTP.Text == "" || this.txtEnterpriseEmailPort.Text == ""))
				{
					if (this.chkEmail.Checked)
					{
						base.OutputWarn("邮箱账号已经输入，请完善邮箱设置！");
						return;
					}
				}
				if (!int.TryParse(this.txtEnterpriseEmailPort.Text.Trim(), out result))
				{
					if (this.chkEmail.Checked)
					{
						base.OutputWarn("邮件端口只能输入正整数！");
						return;
					}
					this.txtEnterpriseEmailPort.Text = "25";
				}
				if (dclDrawMoneyPercent <= 0m || dclDrawMoneyPercent > 1m)
				{
					base.OutputWarn("账户提现折损率的取值范围只能是0至1之间的数字！");
				}
				else if (!this.chkPayCard.Checked && (!this.chkPayCash.Checked & !this.chkPayBink.Checked & !this.chkPayCoupon.Checked))
				{
					base.OutputWarn("*必须选择付费方式,否则无法结算!");
				}
				else
				{
					modelParameter.DrawMoneyPercent = dclDrawMoneyPercent;
					modelParameter.RecommendPoint = ((strRecommendPoint != "") ? int.Parse(strRecommendPoint) : 0);
					modelParameter.PointPeriod = Convert.ToInt32(this.txtPointPeriod.Text);
					modelParameter.StockCount = Convert.ToInt32(this.txtStockCount.Text);
					modelParameter.ExpensePrefix = this.txtExpensePrefix.Text;
					modelParameter.GoodsExpensePrefix = this.txtGoodsExpensePrefix.Text;
					modelParameter.TimeExpensePrefix = this.txtTimeExpensePrefix.Text;
					modelParameter.MemCountPrefix = this.txtMemCountPrefix.Text;
					modelParameter.MemRechargePrefix = this.txtMemRechargePrefix.Text;
					modelParameter.GoodsInPrefix = this.txtGoodsInPrefix.Text;
					modelParameter.GoodsAllotPrefix = this.txtGoodsAllotPrefix.Text;
					modelParameter.MemDrawMoneyPrefix = this.txtMemDrawMoneyPrefix.Text;
					modelParameter.MemPointChangePrefix = this.txtMemPointChangePrefix.Text;
					modelParameter.GiftExchangePrefix = this.txtGiftExchangePrefix.Text;
					modelParameter.AllianceRebatePercent = decimal.Parse(this.txtAllianceRebatePercent.Text);
					modelParameter.CardShopRebatePercent = decimal.Parse(this.txtCardShopRebatePercent.Text);
					modelParameter.PointDrawPercent = decimal.Parse(this.txtPointDrawPercent.Text);
					modelParameter.Senseiccard = this.chkSenseiccard.Checked;
					modelParameter.Contacticcard = this.chkContacticcard.Checked;
					modelParameter.PointNumStr = pointnum;
					if (this.PrintPreview.Checked)
					{
						modelParameter.PrintPreview = 1;
					}
					else
					{
						modelParameter.PrintPreview = 0;
					}
					if (this.A4ShiZhi.Checked)
					{
						modelParameter.PrintPaperType = 0;
					}
					if (this.SanLianShiZhi.Checked)
					{
						modelParameter.PrintPaperType = 1;
					}
					if (this.POs58ShiZhi.Checked)
					{
						modelParameter.PrintPaperType = 2;
					}
					if (this.POs80ShiZhi.Checked)
					{
						modelParameter.PrintPaperType = 3;
					}
					if (this.CKautoBackup.Checked)
					{
						modelParameter.AutoBackupDB = true;
					}
					else
					{
						modelParameter.AutoBackupDB = false;
					}
					modelParameter.AutoBackupDay = Convert.ToInt32(this.TBautoBackupDay.Text);
					modelParameter.IsPayCard = this.chkPayCard.Checked;
					modelParameter.IsPayCash = this.chkPayCash.Checked;
					modelParameter.IsPayBink = this.chkPayBink.Checked;
					modelParameter.IsPayCoupon = this.chkPayCoupon.Checked;
					modelParameter.IsPayPoint = this.chkPayPoint.Checked;
					modelParameter.AutoPrint = this.chkAutoPrint.Checked;
					modelParameter.AccordPrint = this.chkAccordPrint.Checked;
					modelParameter.PrintTitle = this.txtPrintTitle.Text;
					modelParameter.PrintFootNote = this.txtPrintFootNote.Text;
					modelParameter.IsStaff = this.chkStaff.Checked;
					modelParameter.IsEmail = this.chkEmail.Checked;
					modelParameter.IsEmailNotice = this.chkMoneyEmail.Checked;
					modelParameter.EmailAdress = this.txtEmailAdress.Text;
					modelParameter.EmailPwd = this.txtEmailPwd.Text;
					modelParameter.EmailSMTP = this.txtEmailSMTP.Text;
					modelParameter.EnterpriseEmailPort = int.Parse(this.txtEnterpriseEmailPort.Text);
					modelParameter.EnterpriseEmailEnableSSL = this.chkEnterpriseEmailEnableSSL.Checked;
					modelParameter.EnterpriseEmailUseDefaultCredentials = this.chkEnterpriseEmailUseDefaultCredentials.Checked;
					modelParameter.EnterpriseEmailDisplayName = this.txtEnterpriseEmailDisplayName.Text;
					modelParameter.IsMustSlotCard = this.chkIsMustSlotCard.Checked;
					modelParameter.UnitList = ((this.txtUnitList.Text == "") ? "个" : this.txtUnitList.Text);
					if (this.chkStaff.Checked)
					{
						modelParameter.StaffType = this.rdStaff.Checked;
					}
					modelParameter.PointLevel = this.chkPointLevel.Checked;
					modelParameter.SellerAccount = this.txtSellerAccount.Text;
					modelParameter.PartnerID = this.txtPartnerID.Text;
					modelParameter.PartnerKey = this.txtPartnerKey.Text;
					modelParameter.IsEditPwdNeedOldPwd = this.chkIsNeedPwd.Checked;
					modelParameter.IsMemRegisterStaff = this.chkMemRegisterStaff.Checked;
					modelParameter.StorageTimingPrefix = this.txtStorageTimingPrefix.Text.Trim();
					modelParameter.MemCountExpensePrefix = this.txtMemCountExpensePrefix.Text.Trim();
					modelParameter.IsAutoSendSMSByMemPast = this.chkIsAutoSendSMSByMemPast.Checked;
					this.txtAutoSendSMSByMemPastForDay.Text = ((this.txtAutoSendSMSByMemPastForDay.Text.Trim() == "") ? "0" : this.txtAutoSendSMSByMemPastForDay.Text);
					if (modelParameter.IsAutoSendSMSByMemPast)
					{
						if (!int.TryParse(this.txtAutoSendSMSByMemPastForDay.Text.Trim(), out result) || Convert.ToInt32(this.txtAutoSendSMSByMemPastForDay.Text.Trim()) < 0 || Convert.ToInt32(this.txtAutoSendSMSByMemPastForDay.Text.Trim()) > 30)
						{
							base.OutputWarn("会员到期提醒提前天数只能是0到30之间的正整数！");
							return;
						}
						modelParameter.AutoSendSMSByMemPastForDay = Convert.ToInt32(this.txtAutoSendSMSByMemPastForDay.Text.Trim());
					}
					else
					{
						modelParameter.AutoSendSMSByMemPastForDay = 0;
					}
					modelParameter.IsAutoSendSMSByMemBirthday = this.chksAutoSendSMSByMemBirthday.Checked;
					this.txtAutoSendSMSByMemBirthdayForDay.Text = ((this.txtAutoSendSMSByMemBirthdayForDay.Text.Trim() == "") ? "0" : this.txtAutoSendSMSByMemBirthdayForDay.Text);
					if (modelParameter.IsAutoSendSMSByMemBirthday)
					{
						if (!int.TryParse(this.txtAutoSendSMSByMemBirthdayForDay.Text.Trim(), out result) || Convert.ToInt32(this.txtAutoSendSMSByMemBirthdayForDay.Text.Trim()) < 0 || Convert.ToInt32(this.txtAutoSendSMSByMemBirthdayForDay.Text.Trim()) > 30)
						{
							base.OutputWarn("会员生日提醒提前天数只能是0到30之间的正整数！");
							return;
						}
						modelParameter.AutoSendSMSByMemBirthdayForDay = Convert.ToInt32(this.txtAutoSendSMSByMemBirthdayForDay.Text.Trim());
					}
					else
					{
						modelParameter.AutoSendSMSByMemBirthdayForDay = 0;
					}
					modelParameter.IsStartWeiXin = this.chkIsStartWeiXin.Checked;
					modelParameter.IsStartTimingProject = this.chkIsStartTimingProject.Checked;
					modelParameter.IsStartMemCount = this.chkIsStartMemCount.Checked;
					modelParameter.EmailUserName = this.txtEmailUserName.Text;
					modelParameter.PointUsePercent = decimal.Parse(this.txtPointUsePercent.Text);
					modelParameter.PointDiscountPercent = decimal.Parse(this.txtPointDiscountPercent.Text);
					modelParameter.IsSendCard = this.chkIsSendCard.Checked;
					modelParameter.ShopSmsManage = this.chkShopSmsManage.Checked;
					modelParameter.ShopPointManage = this.chkShopPointManage.Checked;
					modelParameter.ShopSettlement = this.chkIsSettlement.Checked;
					if (!PubFunction.curParameter.EnableGoods)
					{
						modelParameter.IsStartTimingProject = (modelParameter.IsStartMemCount = false);
					}
					if (!this.SwitchingMode(modelParameter))
					{
						base.OutputWarn("保存失败！");
					}
					else
					{
						List<Chain.Model.SysGroup> list = new Chain.BLL.SysGroup().GetModelList("");
						foreach (Chain.Model.SysGroup item in list)
						{
							PubFunction.UpdateGroupAuthority(item.GroupID);
						}
						if (bllUpdateParameter.Update(modelParameter))
						{
							PubFunction pub = new PubFunction();
							PubFunction.curParameter = pub.LoadSysParameter();
							this.curParameter = PubFunction.curParameter;
							PubFunction.SaveSysLog(this._UserID, 3, "系统参数设置", "系统参数设置", this._UserShopID, DateTime.Now, PubFunction.ipAdress);
							base.OutputWarn("保存成功！");
						}
						else
						{
							base.OutputWarn("系统异常，未保存数据，请再次点击保存！");
						}
					}
				}
			}
		}

		public bool SwitchingMode(Chain.Model.SysParameter modelParameter)
		{
			string strStartGood = "66,67,69,76,87,91,113,114,118";
			string strStartWeiXin = "98,120,121,122,123,124,125,126,127,128,129,130,131";
			string strStartTimingProject = "87,110,111,113,114";
			string strStartMemCount = "66,76,118";
			string strStartGoodBar = "60";
			string strStartWeiXinBar = "119";
			List<bool> ststus = new List<bool>
			{
				PubFunction.curParameter.EnableGoods,
				PubFunction.curParameter.EnableGoods,
				modelParameter.IsStartWeiXin,
				modelParameter.IsStartWeiXin,
				modelParameter.IsStartTimingProject,
				modelParameter.IsStartMemCount
			};
			List<string> moduleIDs = new List<string>
			{
				strStartGoodBar,
				strStartGood,
				strStartWeiXinBar,
				strStartWeiXin,
				strStartTimingProject,
				strStartMemCount
			};
			Chain.BLL.SysParameter bllUpdateParameter = new Chain.BLL.SysParameter();
			return bllUpdateParameter.SwitchingMode(ststus, moduleIDs);
		}
	}
}
