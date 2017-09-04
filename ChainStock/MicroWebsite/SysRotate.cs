using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainPoint.MicroWebsite
{
	public class SysRotate : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmWeiXinRuleList;

		protected Literal ltlTitle;

		protected HtmlInputText txtRotateName;

		protected HtmlInputText txtRotateID;

		protected HtmlTextArea txtRotateRemark;

		protected HtmlImage imgRotatePhoto;

		protected HtmlInputHidden txtRotatePhoto;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected HtmlInputText txtRotateRegion;

		protected HtmlInputCheckBox cbkIsWinOne;

		protected HtmlInputText txtOneName;

		protected HtmlInputText txtOnePrizeName;

		protected HtmlInputText txtOnePrizeCount;

		protected HtmlInputText txtOneWinCount;

		protected HtmlInputText txtOneRate;

		protected HtmlInputText txtOneMobile;

		protected HtmlInputText txtTwoName;

		protected HtmlInputText txtTwoPrizeName;

		protected HtmlInputText txtTwoPrizeCount;

		protected HtmlInputText txtTwoWinCount;

		protected HtmlInputText txtTwoRate;

		protected HtmlInputText txtTwoMobile;

		protected HtmlInputText txtThreeName;

		protected HtmlInputText txtThreePrizeName;

		protected HtmlInputText txtThreePrizeCount;

		protected HtmlInputText txtThreeWinCount;

		protected HtmlInputText txtThreeRate;

		protected HtmlInputText txtThreeMobile;

		protected HtmlInputText txtFourName;

		protected HtmlInputText txtFourPrizeName;

		protected HtmlInputText txtFourPrizeCount;

		protected HtmlInputText txtFourWinCount;

		protected HtmlInputText txtFourRate;

		protected HtmlInputText txtFourMobile;

		protected HtmlInputText txtFiveName;

		protected HtmlInputText txtFivePrizeName;

		protected HtmlInputText txtFivePrizeCount;

		protected HtmlInputText txtFiveWinCount;

		protected HtmlInputText txtFiveRate;

		protected HtmlInputText txtFiveMobile;

		protected HtmlInputText txtSixName;

		protected HtmlInputText txtSixPrizeName;

		protected HtmlInputText txtSixPrizeCount;

		protected HtmlInputText txtSixWinCount;

		protected HtmlInputText txtSixRate;

		protected HtmlInputText txtSixMobile;

		protected Button btnMoneySave;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.txtStartTime.Value = DateTime.Now.ToShortDateString();
				this.txtEndTime.Value = DateTime.Now.ToShortDateString();
				this.GetSysRotate();
			}
		}

		private void GetSysRotate()
		{
			if (base.Request.QueryString["rotateID"] != null)
			{
				int rotateID = int.Parse(base.Request.QueryString["rotateID"]);
				Chain.BLL.SysRotate bllSysRotate = new Chain.BLL.SysRotate();
				Chain.Model.SysRotate modelSysRotate = bllSysRotate.GetModel(rotateID);
				this.txtRotateName.Value = modelSysRotate.RotateName;
				this.txtRotateID.Value = modelSysRotate.RotateID.ToString();
				this.txtRotateRemark.Value = modelSysRotate.RotateRemark;
				this.txtStartTime.Value = modelSysRotate.StartTime.ToString("yyyy-MM-dd HH:mm");
				this.txtEndTime.Value = modelSysRotate.EndTime.ToString("yyyy-MM-dd HH:mm");
				this.txtOnePrizeCount.Value = modelSysRotate.OnePrizeCount.ToString();
				this.txtOnePrizeName.Value = modelSysRotate.OnePrizeName;
				this.txtTwoPrizeCount.Value = modelSysRotate.TwoPrizeCount.ToString();
				this.txtTwoPrizeName.Value = modelSysRotate.TwoPrizeName;
				this.txtThreePrizeCount.Value = modelSysRotate.ThreePrizeCount.ToString();
				this.txtThreePrizeName.Value = modelSysRotate.ThreePrizeName;
				this.txtFourPrizeCount.Value = modelSysRotate.FourPrizeCount.ToString();
				this.txtFourPrizeName.Value = modelSysRotate.FourPrizeName;
				this.txtFivePrizeCount.Value = modelSysRotate.FivePrizeCount.ToString();
				this.txtFivePrizeName.Value = modelSysRotate.FivePrizeName;
				this.txtSixPrizeCount.Value = modelSysRotate.SixPrizeCount.ToString();
				this.txtSixPrizeName.Value = modelSysRotate.SixPrizeName;
				this.txtOneRate.Value = modelSysRotate.OneRate.ToString();
				this.txtTwoRate.Value = modelSysRotate.TwoRate.ToString();
				this.txtThreeRate.Value = modelSysRotate.ThreeRate.ToString();
				this.txtFourRate.Value = modelSysRotate.FourRate.ToString();
				this.txtFiveRate.Value = modelSysRotate.FiveRate.ToString();
				this.txtSixRate.Value = modelSysRotate.SixRate.ToString();
				this.txtRotateRegion.Value = modelSysRotate.RotateRegion;
				this.imgRotatePhoto.Src = modelSysRotate.ImageUrl;
				this.txtRotatePhoto.Value = modelSysRotate.ImageUrl;
				this.txtOneMobile.Value = modelSysRotate.OneMobile;
				this.txtTwoMobile.Value = modelSysRotate.TwoMobile;
				this.txtThreeMobile.Value = modelSysRotate.ThreeMobile;
				this.txtFourMobile.Value = modelSysRotate.FourMobile;
				this.txtFiveMobile.Value = modelSysRotate.FiveMobile;
				this.txtSixMobile.Value = modelSysRotate.SixMobile;
				this.txtOneWinCount.Value = modelSysRotate.OnePrizeWinCount.ToString();
				this.txtTwoWinCount.Value = modelSysRotate.TwoPrizeWinCount.ToString();
				this.txtThreeWinCount.Value = modelSysRotate.ThreePrizeWinCount.ToString();
				this.txtFourWinCount.Value = modelSysRotate.FourPrizeWinCount.ToString();
				this.txtFiveWinCount.Value = modelSysRotate.FivePrizeWinCount.ToString();
				this.txtSixWinCount.Value = modelSysRotate.SixPrizeWinCount.ToString();
				this.txtOneName.Value = modelSysRotate.OneName;
				this.txtTwoName.Value = modelSysRotate.TwoName;
				this.txtThreeName.Value = modelSysRotate.ThreeName;
				this.txtFourName.Value = modelSysRotate.FourName;
				this.txtFiveName.Value = modelSysRotate.FiveName;
				this.txtSixName.Value = modelSysRotate.SixName;
				if (modelSysRotate.IsWinOne == 1)
				{
					this.cbkIsWinOne.Checked = true;
				}
				else
				{
					this.cbkIsWinOne.Checked = false;
				}
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Chain.BLL.SysRotate blSysRotate = new Chain.BLL.SysRotate();
			Chain.Model.SysRotate modelSysRotate = new Chain.Model.SysRotate();
			int intUserID = this._UserID;
			modelSysRotate.RotateName = this.txtRotateName.Value;
			modelSysRotate.RotateRemark = this.txtRotateRemark.Value;
			modelSysRotate.StartTime = DateTime.Parse(DateTime.Parse(this.txtStartTime.Value).ToString("yyyy-MM-dd HH:mm"));
			modelSysRotate.EndTime = DateTime.Parse(DateTime.Parse(this.txtEndTime.Value).ToString("yyyy-MM-dd HH:mm"));
			modelSysRotate.RotateCount = 0;
			modelSysRotate.PersonTotalCount = 0;
			modelSysRotate.PersonDayCount = 0;
			modelSysRotate.OnePrizeName = this.txtOnePrizeName.Value;
			modelSysRotate.OnePrizeCount = int.Parse(this.txtOnePrizeCount.Value);
			modelSysRotate.TwoPrizeName = this.txtTwoPrizeName.Value;
			modelSysRotate.TwoPrizeCount = int.Parse(this.txtTwoPrizeCount.Value);
			modelSysRotate.ThreePrizeName = this.txtThreePrizeName.Value;
			modelSysRotate.ThreePrizeCount = int.Parse(this.txtThreePrizeCount.Value);
			modelSysRotate.FourPrizeName = this.txtFourPrizeName.Value;
			modelSysRotate.FourPrizeCount = int.Parse(this.txtFourPrizeCount.Value);
			modelSysRotate.FivePrizeName = this.txtFivePrizeName.Value;
			modelSysRotate.FivePrizeCount = int.Parse(this.txtFivePrizeCount.Value);
			modelSysRotate.SixPrizeName = this.txtSixPrizeName.Value;
			modelSysRotate.SixPrizeCount = int.Parse(this.txtSixPrizeCount.Value);
			modelSysRotate.OneRate = decimal.Parse(this.txtOneRate.Value);
			modelSysRotate.TwoRate = decimal.Parse(this.txtTwoRate.Value);
			modelSysRotate.ThreeRate = decimal.Parse(this.txtThreeRate.Value);
			modelSysRotate.FourRate = decimal.Parse(this.txtFourRate.Value);
			modelSysRotate.FiveRate = decimal.Parse(this.txtFiveRate.Value);
			modelSysRotate.SixRate = decimal.Parse(this.txtSixRate.Value);
			modelSysRotate.RotateRegion = this.txtRotateRegion.Value;
			modelSysRotate.CreateTime = DateTime.Now;
			modelSysRotate.CreateUserID = intUserID;
			modelSysRotate.OnePrizeWinCount = 0;
			modelSysRotate.TwoPrizeWinCount = 0;
			modelSysRotate.ThreePrizeWinCount = 0;
			modelSysRotate.FourPrizeWinCount = 0;
			modelSysRotate.FivePrizeWinCount = 0;
			modelSysRotate.SixPrizeWinCount = 0;
			modelSysRotate.ImageUrl = this.txtRotatePhoto.Value;
			modelSysRotate.OneMobile = this.txtOneMobile.Value;
			modelSysRotate.TwoMobile = this.txtTwoMobile.Value;
			modelSysRotate.ThreeMobile = this.txtThreeMobile.Value;
			modelSysRotate.FourMobile = this.txtFourMobile.Value;
			modelSysRotate.FiveMobile = this.txtFiveMobile.Value;
			modelSysRotate.SixMobile = this.txtSixMobile.Value;
			modelSysRotate.OnePrizeWinCount = int.Parse(this.txtOneWinCount.Value);
			modelSysRotate.TwoPrizeWinCount = int.Parse(this.txtTwoWinCount.Value);
			modelSysRotate.ThreePrizeWinCount = int.Parse(this.txtThreeWinCount.Value);
			modelSysRotate.FourPrizeWinCount = int.Parse(this.txtFourWinCount.Value);
			modelSysRotate.FivePrizeWinCount = int.Parse(this.txtFiveWinCount.Value);
			modelSysRotate.SixPrizeWinCount = int.Parse(this.txtSixWinCount.Value);
			modelSysRotate.OneName = this.txtOneName.Value;
			modelSysRotate.TwoName = this.txtTwoName.Value;
			modelSysRotate.ThreeName = this.txtThreeName.Value;
			modelSysRotate.FourName = this.txtFourName.Value;
			modelSysRotate.FiveName = this.txtFiveName.Value;
			modelSysRotate.SixName = this.txtSixName.Value;
			if (this.cbkIsWinOne.Checked)
			{
				modelSysRotate.IsWinOne = 1;
			}
			else
			{
				modelSysRotate.IsWinOne = 0;
			}
			int flag;
			if (this.txtRotateID.Value != "")
			{
				modelSysRotate.RotateID = int.Parse(this.txtRotateID.Value);
				flag = blSysRotate.Update(modelSysRotate);
			}
			else
			{
				flag = blSysRotate.Add(modelSysRotate);
			}
			if (flag > 0)
			{
				base.MessagePageShowError("保存成功！");
				base.Response.Redirect("SysRotateList.aspx?PID=144");
			}
			else
			{
				base.MessagePageShowError("保存失败！");
			}
		}
	}
}
