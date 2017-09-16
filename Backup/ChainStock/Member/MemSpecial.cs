using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.Member
{
	public class MemSpecial : PageBase
	{
		protected HtmlForm frmSpecial;

		protected HtmlInputHidden PointNum;

		protected Literal ltlTitle;

		protected HtmlInputText txtSpecialName;

		protected HtmlInputRadioButton rdDate;

		protected HtmlInputRadioButton rdWeek;

		protected HtmlInputRadioButton rdMonth;

		protected HtmlInputRadioButton rdBirthday;

		protected HtmlTableRow trDate;

		protected HtmlInputText txtStartTime;

		protected HtmlInputText txtEndTime;

		protected HtmlTableRow trWeek;

		protected HtmlInputText txtWeek;

		protected HtmlTableRow trMonth;

		protected HtmlInputText txtMonth;

		protected HtmlGenericControl lblSpecialUSer;

		protected HtmlInputHidden SpecialUSerID;

		protected HtmlInputText txtMoney;

		protected HtmlInputText txtGiveMoney;

		protected HtmlTextArea txtSpecialRemark;

		protected Label lblPrintTitle;

		protected Label lblPrintFoot;

		protected HtmlInputHidden txtSpecialID;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.lblSpecialUSer.InnerText = PubFunction.UserIDTOName(this._UserID);
			this.SpecialUSerID.Value = this._UserID.ToString();
			if (base.Request.QueryString["SpecialID"] != null)
			{
				this.GetSpecialToEdit(int.Parse(base.Request.QueryString["SpecialID"]));
				this.txtSpecialID.Value = base.Request.QueryString["SpecialID"];
				this.ltlTitle.Text = "主页   >   优惠活动管理   >   编辑优惠活动 ";
			}
			else
			{
				this.ltlTitle.Text = "主页   >   优惠活动管理   >   新增优惠活动 ";
			}
		}

		public void GetSpecialToEdit(int SpecialID)
		{
			Chain.Model.Special models = new Chain.Model.Special();
			Chain.BLL.Special blls = new Chain.BLL.Special();
			DataTable da = blls.GetItemAll(SpecialID).Tables[0];
			this.txtSpecialName.Value = da.Rows[0]["SpecialName"].ToString();
			this.txtGiveMoney.Value = Convert.ToDouble(da.Rows[0]["SpecialGive"]).ToString("0.00");
			this.txtMoney.Value = Convert.ToDouble(da.Rows[0]["SpecialRecharge"]).ToString("0.00");
			this.txtSpecialRemark.Value = da.Rows[0]["Sremark"].ToString();
			this.lblSpecialUSer.InnerText = PubFunction.UserIDTOName(Convert.ToInt32(da.Rows[0]["SpecialUser"]));
			string text = da.Rows[0]["Type"].ToString();
			if (text != null)
			{
				if (!(text == "1"))
				{
					if (!(text == "2"))
					{
						if (!(text == "3"))
						{
							if (text == "4")
							{
								this.rdBirthday.Checked = true;
								this.trDate.Style.Add("display", "none");
								this.trWeek.Style.Add("display", "none");
								this.trMonth.Style.Add("display", "none");
							}
						}
						else
						{
							this.rdMonth.Checked = true;
							this.txtMonth.Value = da.Rows[0]["Month"].ToString();
							this.trDate.Style.Add("display", "none");
							this.trWeek.Style.Add("display", "none");
							this.trMonth.Style.Remove("display");
						}
					}
					else
					{
						this.rdWeek.Checked = true;
						this.txtWeek.Value = da.Rows[0]["Week"].ToString();
						this.trDate.Style.Add("display", "none");
						this.trWeek.Style.Remove("display");
						this.trMonth.Style.Add("display", "none");
					}
				}
				else
				{
					this.rdDate.Checked = true;
					this.txtStartTime.Value = da.Rows[0]["StartTime"].ToString();
					this.txtEndTime.Value = da.Rows[0]["EndTime"].ToString();
					this.trDate.Style.Remove("display");
					this.trWeek.Style.Add("display", "none");
					this.trMonth.Style.Add("display", "none");
				}
			}
		}
	}
}
