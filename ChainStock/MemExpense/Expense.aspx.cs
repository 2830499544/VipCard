using Chain.BLL;
using Chain.Model;
using ChainStock.Controls;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.MemExpense
{
	public class Expense : PageBase
	{
		protected HtmlForm frmExpense;

		protected HtmlInputHidden txtProject;

		protected HtmlInputHidden txtEndTime;

		protected HtmlInputHidden PointNum;

		protected HtmlInputHidden JSXF;

		protected Literal ltlTitle;

		protected MemberSearch ucMemberSearch;

		protected Pay ucPay;

		protected HtmlInputHidden MemCard;

		protected HtmlInputCheckBox chkAllowPwd;

		protected Label lblPrintTitle;

		protected Label lblPrintFoot;

		protected HtmlGenericControl lblOrderAccount;

		protected HtmlGenericControl lblOrderCreateTime;

		protected HtmlGenericControl lblOrderUSer;

		protected HtmlTableRow trTimeExpense;

		protected HtmlGenericControl spTimeInfo;

		protected HtmlInputText txtExpMoney;

		protected HtmlInputText txtDiscountMoney;

		protected HtmlInputText txtExpPoint;

		protected HtmlTextArea txtExpRemark;

		protected HtmlGenericControl lblIsSMS;

		protected HtmlInputCheckBox chkSMS;

		protected HtmlInputCheckBox chkIsSMS;

		protected HtmlInputCheckBox chkPrint;

		protected HtmlInputHidden txtIsTimeExpense;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				Chain.BLL.OrderTime bllTime = new Chain.BLL.OrderTime();
				Chain.Model.OrderTime model = new Chain.Model.OrderTime();
				this.txtEndTime.Value = DateTime.Now.ToString();
				if (base.Request.QueryString["OrderCode"] != null)
				{
					DataTable dt = bllTime.GetList(" OrderTimeCode='" + base.Request.QueryString["OrderCode"].ToString() + "'").Tables[0];
					model = bllTime.GetModel(int.Parse(dt.Rows[0]["OrderTimeID"].ToString()));
					TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(model.OrderMarchTime));
					this.txtProject.Value = model.OrderProjectID.ToString();
					int allminute = 0;
					try
					{
						Chain.Model.Timingrules mdTimingrules = new Chain.BLL.Timingrules().GetModel(Convert.ToInt32(dt.Rows[0]["OrderRulesID"]));
						int minute = ts.Minutes + (ts.Hours + ts.Days * 24) * 60;
						allminute = minute;
						int micount = minute / mdTimingrules.RulesInterval;
						int passtime = minute % mdTimingrules.RulesInterval;
						if (micount > 0)
						{
							if (passtime > mdTimingrules.RulesExceedTime)
							{
								micount++;
							}
						}
						else
						{
							micount = 1;
						}
						this.txtExpMoney.Value = (mdTimingrules.RulesUnitPrice * micount).ToString("F2");
						this.txtDiscountMoney.Value = this.txtExpMoney.Value;
						this.JSXF.Value = "1";
						this.txtExpMoney.Attributes["readonly"] = "readonly";
						this.txtDiscountMoney.Attributes["readonly"] = "readonly";
						this.txtExpPoint.Attributes["readonly"] = "readonly";
					}
					catch
					{
					}
					if (bool.Parse(base.Request.QueryString["IsMem"]))
					{
						if (base.Request.QueryString["MemCard"] != null)
						{
							this.MemCard.Value = base.Request.QueryString["MemCard"];
						}
					}
					else
					{
						this.MemCard.Value = "0";
					}
					this.lblOrderAccount.InnerText = base.Request.QueryString["OrderCode"];
					this.txtExpPoint.Value = "0";
					this.spTimeInfo.InnerHtml = string.Concat(new object[]
					{
						"开始时间：",
						model.OrderMarchTime,
						"  结束时间：",
						DateTime.Now.ToString(),
						"<br>消费时长：",
						ts.Hours + ts.Days * 24,
						"小时 ",
						ts.Minutes,
						"分钟 ",
						ts.Seconds,
						"秒<br>总共：",
						allminute,
						"分钟"
					});
					this.txtIsTimeExpense.Value = "1";
					this.chkSMS.Checked = (this.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByTimingConsumption);
					this.lblIsSMS.Style.Add("display", (this.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByTimingConsumption) ? "\"\"" : "none");
				}
				else
				{
					this.trTimeExpense.Attributes.Add("style", "display:none;");
					if (base.Request.QueryString["MemCard"] != null)
					{
						this.MemCard.Value = base.Request.QueryString["MemCard"];
					}
					this.txtIsTimeExpense.Value = "0";
					this.lblOrderAccount.InnerText = this.curParameter.strExpensePrefix + DateTime.Now.ToString("yyMMddHHmmssffff");
					this.chkSMS.Checked = (this.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByFastConsumption);
					this.lblIsSMS.Style.Add("display", (this.curParameter.bolMoneySms && this.curParameter.bolAutoSendSMSByFastConsumption) ? "\"\"" : "none");
					this.ucMemberSearch.BolSenseICCard = this.curParameter.bolSenseiccard;
					this.ucMemberSearch.BolContactICCard = this.curParameter.bolContacticcard;
				}
				this.lblOrderCreateTime.InnerText = DateTime.Now.ToString();
				this.lblOrderUSer.InnerText = PubFunction.UserIDTOName(this._UserID);
				PubFunction.Get_PrintTitle(ref this.lblPrintTitle, ref this.lblPrintFoot, this._UserShopID);
				this.chkIsSMS.Checked = this.curParameter.bolSms;
				this.chkPrint.Checked = this.curParameter.bolAutoPrint;
				this.chkAllowPwd.Checked = this.curParameter.bolPwd;
				this.PointNum.Value = PubFunction.GetPointNum("KSXF");
			}
		}
	}
}
