using Chain.BLL;
using Chain.Common;
using Chain.Model;
using ChainStock.Controls;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class MemberRegister : PageBase
	{
		protected HtmlForm frmMemRegister;

		protected HtmlInputHidden txtMemID;

		protected Literal ltlTitle;

		protected HtmlInputText txtMemCard;

		protected HtmlInputButton btnQRCode;

		protected HtmlInputButton btnSendSenseICCard;

		protected HtmlInputButton btnContactICCard;

		protected HtmlInputText txtMemName;

		protected HtmlTableRow trMemPassword;

		protected HtmlGenericControl sppwd1;

		protected HtmlInputPassword txtMemPassword;

		protected HtmlInputCheckBox RegNullPwd;

		protected HtmlGenericControl sppwd2;

		protected HtmlInputPassword txtMemPasswordCheck;

		protected HtmlInputText txtMemMobile;

		protected HtmlSelect sltMemState;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

		protected HtmlInputText txtMemBirthday;

		protected HtmlSelect sltMemSex;

		protected HtmlInputText txtMemPoint;

		protected HtmlInputText txtMemMoney;

		protected HtmlInputText txtMemEmail;

		protected HtmlInputText txtTelephone;

		protected HtmlInputText txtMemIdentityCard;

		protected HtmlInputText txtMemCreateTime;

		protected HtmlSelect sltMemUserID;

		protected HtmlInputText txtMemRecommendCard;

		protected HtmlGenericControl txtMemRecommendMsg;

		protected HtmlInputHidden txtMemRecommendID;

		protected HtmlInputHidden txtMemRecommendName;

		protected HtmlInputText txtMemPastTime;

		protected HtmlInputCheckBox chkMemIsPast;

		protected HtmlInputCheckBox chkIsIsPast;

		protected HtmlTableRow Tr1;

		protected HtmlTableCell tdStaff;

		protected HtmlTableCell tddStaff;

		protected HtmlSelect sltStaff;

		protected HtmlInputText txtRegisterStaffMoney;

		protected HtmlInputCheckBox chkRegisterStaff;

		protected HtmlTableCell tdCardNumber;

		protected HtmlTableCell tddCardNumber;

		protected HtmlInputText txtCardNumber;

		protected ChainStock.Controls.SysArea ucSysArea;

		protected HtmlInputText txtMemAddress;

		protected HtmlTextArea txtMemRemark;

		protected HtmlGenericControl tbCustomField;

		protected HtmlInputCheckBox chkSMS;

		protected HtmlInputCheckBox chkMMS;

		protected HtmlInputCheckBox chkIsSMS;

		protected HtmlImage imgMemPhoto;

		protected HtmlInputHidden txtMemPhoto;

		protected HtmlGenericControl isSendRWM;

		protected HtmlGenericControl trTitle;

		protected HtmlGenericControl trQrCode;

		protected HtmlImage imgQRCode;

		protected HtmlInputHidden hidImgSrc;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, false);
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, false);
				PubFunction.BindUserSelect(this._UserShopID, this.sltMemUserID, false, false);
				PubFunction.BindStaff(this._UserShopID, this.sltStaff, true);
				this.chkSMS.Checked = (this.curParameter.bolMoneySms && PubFunction.curParameter.bolAutoSendSMSByMemRegister);
				this.chkIsSMS.Checked = this.curParameter.bolSms;
				this.sltShop.Value = this._UserShopID.ToString();
				this.sltShop.Attributes.Add("disabled", "disabled");
				this.chkMMS.Checked = (this.curParameter.bolMMS && PubFunction.curParameter.bolAutoSendMMSByMemRegister);
				if (PubFunction.curParameter.RegNullPwd)
				{
					this.sppwd1.Attributes.Add("style", "display:none;");
					this.sppwd2.Attributes.Add("style", "display:none;");
				}
				this.RegNullPwd.Checked = PubFunction.curParameter.RegNullPwd;
				this.txtMemPassword.Value = "";
				this.txtMemPasswordCheck.Value = "";
				this.chkIsIsPast.Checked = PubFunction.curParameter.bolPastTime;
				this.chkRegisterStaff.Checked = PubFunction.curParameter.bolIsMemRegisterStaff;
				if (!PubFunction.curParameter.bolIsMemRegisterStaff)
				{
					this.tdStaff.Visible = false;
					this.tddStaff.Visible = false;
				}
				if (!PubFunction.curParameter.bolMoneyAndPoint)
				{
					this.txtMemPoint.Attributes.Add("readonly", "true");
					this.txtMemPoint.Style.Add("background-color", "#eee");
					this.txtMemMoney.Attributes.Add("readonly", "true");
					this.txtMemMoney.Style.Add("background-color", "#eee");
				}
				if (base.Request.QueryString["MemID"] != null)
				{
					this.GetMemToEdit(int.Parse(base.Request.QueryString["MemID"]));
					this.trMemPassword.Visible = false;
					this.ucSysArea.NeedBind = false;
					this.btnQRCode.Attributes.Add("style", "display:none");
					this.btnSendSenseICCard.Style.Add("display", "none");
					this.btnContactICCard.Style.Add("display", "none");
				}
				else
				{
					PubFunction.BindAddCustomFields(this.tbCustomField, "Mem");
					this.txtMemPoint.Value = "0";
					this.txtMemMoney.Value = "0";
					this.txtMemCreateTime.Value = DateTime.Now.ToString("yyyy-MM-dd");
					this.sltMemUserID.Value = this._UserID.ToString();
					this.btnQRCode.Style.Add("display", (this.curParameter.bolMMS && this.curParameter.bolAutoSendMMSByMemRegister) ? "\"\"" : "none");
					if (!this.curParameter.bolSenseiccard)
					{
						this.btnSendSenseICCard.Style.Add("display", "none");
					}
					if (!this.curParameter.bolContacticcard)
					{
						this.btnContactICCard.Style.Add("display", "none");
					}
				}
				this.isSendRWM.Style.Add("display", (this.curParameter.bolMMS && this.curParameter.bolAutoSendMMSByMemRegister) ? "\"\"" : "none");
			}
		}

		protected void GetMemToEdit(int memID)
		{
			Chain.Model.Mem modelMem = new Chain.Model.Mem();
			Chain.BLL.Mem bllMem = new Chain.BLL.Mem();
			modelMem = bllMem.GetModel(memID);
			DataTable dtMem = bllMem.GetItemAll(memID).Tables[0];
			this.txtMemID.Value = modelMem.MemID.ToString();
			this.txtMemCard.Value = modelMem.MemCard;
			this.txtMemCard.Attributes.Add("readonly", "true");
			this.txtMemName.Value = modelMem.MemName;
			this.txtCardNumber.Value = modelMem.MemCardNumber;
			if (modelMem.MemSex)
			{
				this.sltMemSex.SelectedIndex = 0;
			}
			else
			{
				this.sltMemSex.SelectedIndex = 1;
			}
			this.txtMemIdentityCard.Value = modelMem.MemIdentityCard;
			string strMemBirthday = modelMem.MemBirthday.ToString();
			if (strMemBirthday.IndexOf("1900") == -1 && strMemBirthday.IndexOf("0001") == -1)
			{
				this.txtMemBirthday.Value = modelMem.MemBirthday.ToShortDateString();
			}
			else
			{
				this.txtMemBirthday.Value = "";
			}
			if (modelMem.MemPoint.ToString() != "")
			{
				this.txtMemPoint.Value = modelMem.MemPoint.ToString();
			}
			else
			{
				this.txtMemPoint.Value = "0";
			}
			this.sltShop.Items.Add(new ListItem(new Chain.BLL.SysShop().GetModel(modelMem.MemShopID).ShopName, modelMem.MemShopID.ToString()));
			if (modelMem.MemMoney.ToString() != "")
			{
				this.txtMemMoney.Value = Math.Round(modelMem.MemMoney, 2).ToString();
			}
			else
			{
				this.txtMemMoney.Value = "0";
			}
			this.txtMemMobile.Value = modelMem.MemMobile;
			this.txtMemEmail.Value = modelMem.MemEmail;
			this.txtMemAddress.Value = modelMem.MemAddress;
			this.sltMemState.Value = modelMem.MemState.ToString();
			this.sltMemLevelID.Value = modelMem.MemLevelID.ToString();
			this.sltShop.Value = modelMem.MemShopID.ToString();
			this.sltShop.Attributes.Add("disabled", "disabled");
			this.txtMemRecommendID.Value = modelMem.MemRecommendID.ToString();
			if (modelMem.MemRecommendID != 0)
			{
				Chain.Model.Mem modelRecommendMem = new Chain.Model.Mem();
				modelRecommendMem = bllMem.GetModel(modelMem.MemRecommendID);
				this.txtMemRecommendCard.Value = modelRecommendMem.MemCard;
			}
			this.txtMemCreateTime.Value = modelMem.MemCreateTime.ToShortDateString();
			string strMemPastTime = modelMem.MemPastTime.ToString();
			if (strMemPastTime.IndexOf("2900") == -1 && strMemPastTime.IndexOf("0001") == -1)
			{
				this.txtMemPastTime.Value = modelMem.MemPastTime.ToShortDateString();
				this.chkMemIsPast.Checked = false;
			}
			else
			{
				this.txtMemPastTime.Value = "";
				this.chkMemIsPast.Checked = true;
			}
			if (modelMem.MemPhoto != "")
			{
				this.imgMemPhoto.Src = base.GetWebRoot() + modelMem.MemPhoto;
				this.txtMemPhoto.Value = modelMem.MemPhoto;
			}
			else
			{
				this.imgMemPhoto.Src = "../images/member/nophoto.gif";
				this.txtMemPhoto.Value = "";
			}
			this.txtMemRemark.Value = StringPlus.HtmlDecode(modelMem.MemRemark);
			this.sltMemUserID.Value = modelMem.MemUserID.ToString();
			this.txtTelephone.Value = modelMem.MemTelePhone;
			if (modelMem.MemQRCode != "")
			{
				this.trTitle.Visible = true;
				this.trQrCode.Visible = true;
				this.imgQRCode.Src = modelMem.MemQRCode;
			}
			else
			{
				this.trTitle.Visible = false;
				this.trQrCode.Visible = false;
			}
			if (modelMem.MemProvince != "")
			{
				this.ucSysArea.sltProvince.Items.Clear();
				PubFunction.BindSysArea(this.ucSysArea.sltProvince, 0);
				this.ucSysArea.sltProvince.Value = modelMem.MemProvince;
			}
			if (modelMem.MemCity != "" && modelMem.MemCity != null && modelMem.MemProvince != "" && modelMem.MemProvince != null)
			{
				this.ucSysArea.sltCity.Items.Clear();
				PubFunction.BindSysArea(this.ucSysArea.sltCity, int.Parse(modelMem.MemProvince));
				this.ucSysArea.sltCity.Value = modelMem.MemCity;
			}
			if (modelMem.MemCounty != "" && modelMem.MemCounty != null && modelMem.MemCity != "" && modelMem.MemCity != null)
			{
				this.ucSysArea.sltCounty.Items.Clear();
				PubFunction.BindSysArea(this.ucSysArea.sltCounty, int.Parse(modelMem.MemCity));
				this.ucSysArea.sltCounty.Value = modelMem.MemCounty;
			}
			if (modelMem.MemVillage != "" && modelMem.MemVillage != null && modelMem.MemCounty != "" && modelMem.MemCounty != null)
			{
				this.ucSysArea.sltVillage.Items.Clear();
				PubFunction.BindSysArea(this.ucSysArea.sltVillage, int.Parse(modelMem.MemCounty));
				this.ucSysArea.sltVillage.Value = modelMem.MemVillage;
			}
			PubFunction.BindEditCustomFields(this.tbCustomField, "Mem", dtMem.Rows[0]);
		}
	}
}
