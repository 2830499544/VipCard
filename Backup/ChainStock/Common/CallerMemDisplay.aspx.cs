using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Common
{
	public class CallerMemDisplay : Page
	{
		protected Literal litObject;

		protected HtmlForm form1;

		protected HtmlGenericControl spTel;

		protected Label lblTel;

		protected HtmlGenericControl lblIsMem;

		protected Label lblTelState;

		protected Label lblTime;

		protected HtmlInputHidden txtFMemID;

		protected HtmlInputText txtFMemCard;

		protected HtmlInputText txtFMemName;

		protected HtmlInputText txtFMemMoney;

		protected HtmlInputText txtFMemPoint;

		protected HtmlInputText txtFMemLevelName;

		protected HtmlInputText txtFMemShopName;

		protected HtmlInputText txtFMemMobile;

		protected HtmlInputText txtFMemState;

		protected HtmlInputText txtFMemBirthday;

		protected HtmlInputText txtFMemSex;

		protected HtmlInputText txtFMemIdentityCard;

		protected HtmlInputText txtFMemPastTime;

		protected HtmlInputText txtFMemCreateTime;

		protected HtmlInputText txtFMemUserName;

		protected HtmlInputText txtFMemEmail;

		protected HtmlInputText txtFMemAddress;

		protected HtmlTextArea txTelRemark;

		private Chain.Model.SysArea areaModel = new Chain.Model.SysArea();

		private Chain.BLL.SysArea areaBLL = new Chain.BLL.SysArea();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["Mobile"] != "")
			{
				this.lblTel.Text = base.Request["Mobile"].ToString();
				if (int.Parse(base.Request["IsMem"]) == 0)
				{
					this.spTel.InnerHtml = "会员来电";
					this.GetMemInfoByMobile(this.lblTel.Text);
				}
			}
			if (int.Parse(base.Request["IsMem"]) != 0)
			{
				this.spTel.InnerHtml = "非会员来电";
				this.lblIsMem.InnerText = "该号码不属于会员";
			}
		}

		public void GetAddressName(string id, ref string address)
		{
			if (!string.IsNullOrEmpty(id))
			{
				this.areaModel = this.areaBLL.GetModel(int.Parse(id));
				address = this.areaModel.Name + address;
				if (0 != this.areaModel.PID)
				{
					this.GetAddressName(this.areaModel.PID.ToString(), ref address);
				}
			}
		}

		public void GetMemInfoByMobile(string mobile)
		{
			Chain.Model.Mem modelMem = new Chain.BLL.Mem().GetMemInfoByMobile(mobile);
			if (modelMem != null)
			{
				this.txtFMemID.Value = modelMem.MemID.ToString();
				this.txtFMemCard.Value = modelMem.MemCard;
				this.txtFMemName.Value = modelMem.MemName;
				this.txtFMemMoney.Value = modelMem.MemMoney.ToString();
				this.txtFMemPoint.Value = modelMem.MemPoint.ToString();
				this.txtFMemLevelName.Value = PubFunction.LevelIDToName(modelMem.MemLevelID);
				this.txtFMemShopName.Value = PubFunction.ShopIDToName(modelMem.MemShopID);
				this.txtFMemMobile.Value = modelMem.MemMobile;
				this.txtFMemState.Value = PubFunction.StateToName(modelMem.MemState);
				this.txtFMemBirthday.Value = modelMem.MemBirthday.ToShortDateString();
				this.txtFMemSex.Value = PubFunction.SexToName(modelMem.MemSex);
				this.txtFMemIdentityCard.Value = modelMem.MemIdentityCard;
				this.txtFMemPastTime.Value = modelMem.MemPastTime.ToShortDateString();
				this.txtFMemEmail.Value = modelMem.MemEmail;
				string address = "";
				if (!string.IsNullOrEmpty(modelMem.MemVillage))
				{
					this.GetAddressName(modelMem.MemVillage, ref address);
				}
				else if (!string.IsNullOrEmpty(modelMem.MemCounty))
				{
					this.GetAddressName(modelMem.MemCounty, ref address);
				}
				else if (!string.IsNullOrEmpty(modelMem.MemCity))
				{
					this.GetAddressName(modelMem.MemCity, ref address);
				}
				else if (!string.IsNullOrEmpty(modelMem.MemProvince))
				{
					this.GetAddressName(modelMem.MemProvince, ref address);
				}
				if (string.IsNullOrEmpty(modelMem.MemAddress))
				{
					modelMem.MemAddress = "无";
				}
				this.txtFMemAddress.Value = address + modelMem.MemAddress;
				this.txtFMemCreateTime.Value = modelMem.MemCreateTime.ToShortDateString();
				this.txtFMemUserName.Value = PubFunction.UserIDTOName(modelMem.MemUserID);
			}
		}
	}
}
