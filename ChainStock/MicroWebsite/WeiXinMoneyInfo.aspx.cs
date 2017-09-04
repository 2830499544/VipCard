using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.MicroWebsite
{
	public class WeiXinMoneyInfo : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm frmSetLevel;

		protected Literal ltlTitle;

		protected HtmlInputText txtMoneyTitle;

		protected HtmlInputHidden txtMoneyID;

		protected HtmlInputText txtMoneyDesc;

		protected HtmlTextArea txtMoneyWish;

		protected HtmlImage imgMoneyPhoto;

		protected HtmlInputHidden txtMoneyPhoto;

		protected HtmlInputText txtMoneyRegion;

		protected HtmlInputText txtEndTime;

		protected HtmlInputText txtTotalMoney;

		protected HtmlInputRadioButton radMoneyTypeOne;

		protected HtmlInputRadioButton radMoneyTypeTwo;

		protected HtmlGenericControl trMoneyTypeOne;

		protected HtmlInputText txtStartMoney;

		protected HtmlInputText txtEndMoney;

		protected HtmlGenericControl trMoneyTypeTwo;

		protected HtmlInputText txtFixedMoney;

		protected HtmlInputText txtMaxCount;

		protected HtmlInputText txtMoneyRate;

		protected HtmlInputHidden txtQuerySql;

		protected HtmlTextArea txtMemList;

		protected HtmlTextArea txtMemIDList;

		protected HtmlGenericControl lblStartTime;

		protected HtmlInputRadioButton radStartTypeOne;

		protected HtmlInputRadioButton radStartTypeTwo;

		protected HtmlInputText txtStartTime;

		protected Button btnMoneySave;

		private Chain.BLL.GoodsClass bllGoodsClass = new Chain.BLL.GoodsClass();

		private Chain.BLL.MemLevel bllMemLevel = new Chain.BLL.MemLevel();

		private Chain.BLL.GoodsClassDiscount bllGoodsClassDiscount = new Chain.BLL.GoodsClassDiscount();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["MoneyID"] != null)
				{
					this.txtMoneyID.Value = base.Request.QueryString["MoneyID"];
					this.BindData(int.Parse(this.txtMoneyID.Value));
				}
				else
				{
					this.radMoneyTypeOne.Checked = true;
					this.BindData();
				}
			}
		}

		private void BindData(int MoneyID)
		{
			Chain.BLL.WeiXinMoney bllWeiXinMoney = new Chain.BLL.WeiXinMoney();
			Chain.Model.WeiXinMoney modelWeiXinMoney = bllWeiXinMoney.GetModel(MoneyID);
			this.txtMoneyTitle.Value = modelWeiXinMoney.MoneyTitle;
			this.txtMoneyDesc.Value = modelWeiXinMoney.MoneyDesc;
			this.txtMoneyWish.Value = modelWeiXinMoney.MoneyWish;
			this.imgMoneyPhoto.Src = modelWeiXinMoney.ImageUrl;
			this.txtMoneyPhoto.Value = modelWeiXinMoney.ImageUrl;
			this.txtEndTime.Value = modelWeiXinMoney.EndTime.ToString("yyyy-MM-dd HH:mm");
			this.txtStartTime.Value = modelWeiXinMoney.StartTime.ToString("yyyy-MM-dd HH:mm");
			this.txtTotalMoney.Value = modelWeiXinMoney.TotalMoney.ToString();
			if (modelWeiXinMoney.MoneyType == 1)
			{
				this.radMoneyTypeOne.Checked = true;
				this.radMoneyTypeTwo.Checked = false;
				this.txtStartMoney.Value = modelWeiXinMoney.StartMoney.ToString();
				this.txtEndMoney.Value = modelWeiXinMoney.EndMoney.ToString();
			}
			else
			{
				this.radMoneyTypeOne.Checked = false;
				this.radMoneyTypeTwo.Checked = true;
				this.txtFixedMoney.Value = modelWeiXinMoney.FixedMoney.ToString();
			}
			this.txtMaxCount.Value = modelWeiXinMoney.MaxCount.ToString();
			this.txtMoneyRate.Value = modelWeiXinMoney.MoneyRate.ToString();
			this.txtMoneyRegion.Value = modelWeiXinMoney.MoneyRegion;
			this.txtQuerySql.Value = modelWeiXinMoney.QuerySql;
			DataTable dt = new Chain.BLL.Mem().GetList(this.txtQuerySql.Value).Tables[0];
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				HtmlTextArea expr_1EA = this.txtMemList;
				string value = expr_1EA.Value;
				expr_1EA.Value = string.Concat(new string[]
				{
					value,
					dt.Rows[i]["MemCard"].ToString(),
					"[",
					dt.Rows[i]["MemName"].ToString(),
					"];"
				});
				this.txtMemIDList.Value = dt.Rows[i]["MemID"].ToString();
			}
			this.lblStartTime.Visible = false;
		}

		private void BindData()
		{
			if (this.Session["QuerySql"] != null)
			{
				this.txtQuerySql.Value = this.Session["QuerySql"].ToString();
				DataTable dt = new Chain.BLL.Mem().GetList(this.txtQuerySql.Value).Tables[0];
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					HtmlTextArea expr_6C = this.txtMemList;
					string value = expr_6C.Value;
					expr_6C.Value = string.Concat(new string[]
					{
						value,
						dt.Rows[i]["MemCard"].ToString(),
						"[",
						dt.Rows[i]["MemName"].ToString(),
						"];"
					});
					HtmlTextArea expr_E3 = this.txtMemIDList;
					expr_E3.Value = expr_E3.Value + dt.Rows[i]["MemID"].ToString() + ",";
				}
			}
		}

		protected void btnMoneySave_Click(object sender, EventArgs e)
		{
			Chain.Model.WeiXinMoney modelWeiXinMoney = new Chain.Model.WeiXinMoney();
			Chain.BLL.WeiXinMoney bllWeiXinMoney = new Chain.BLL.WeiXinMoney();
			int MoneyID = 0;
			DateTime StartTime;
			if (this.radStartTypeOne.Checked)
			{
				StartTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
			}
			else
			{
				StartTime = DateTime.Parse(DateTime.Parse(this.txtStartTime.Value).ToString("yyyy-MM-dd HH:mm"));
			}
			if (this.txtMoneyID.Value != "")
			{
				MoneyID = int.Parse(this.txtMoneyID.Value);
				StartTime = DateTime.Parse(DateTime.Parse(this.txtStartTime.Value).ToString("yyyy-MM-dd HH:mm"));
			}
			string ImageUrl = this.txtMoneyPhoto.Value;
			string MoneyDesc = this.txtMoneyDesc.Value;
			string MoneyTitle = this.txtMoneyTitle.Value;
			string MoneyWish = this.txtMoneyWish.Value;
			DateTime EndTime = DateTime.Parse(DateTime.Parse(this.txtEndTime.Value).ToString("yyyy-MM-dd HH:mm"));
			decimal TotalMoney = decimal.Parse(this.txtTotalMoney.Value);
			decimal StartMoney = 0m;
			decimal EndMoney = 0m;
			decimal FixedMoney = 0m;
			int MoneyType;
			if (this.radMoneyTypeOne.Checked)
			{
				StartMoney = decimal.Parse(this.txtStartMoney.Value);
				EndMoney = decimal.Parse(this.txtEndMoney.Value);
				MoneyType = 1;
			}
			else
			{
				FixedMoney = decimal.Parse(this.txtFixedMoney.Value);
				MoneyType = 2;
			}
			int MaxCount = int.Parse(this.txtMaxCount.Value);
			decimal MoneyRate = decimal.Parse(this.txtMoneyRate.Value);
			string MemIDList = this.txtMemIDList.Value.TrimEnd(new char[]
			{
				','
			});
			string[] strMemIDList = MemIDList.Split(new char[]
			{
				','
			});
			modelWeiXinMoney.MoneyTitle = MoneyTitle;
			modelWeiXinMoney.MoneyWish = MoneyWish;
			modelWeiXinMoney.MoneyDesc = MoneyDesc;
			modelWeiXinMoney.ImageUrl = ImageUrl;
			modelWeiXinMoney.StartTime = StartTime;
			modelWeiXinMoney.EndTime = EndTime;
			modelWeiXinMoney.TotalMoney = TotalMoney;
			modelWeiXinMoney.MoneyType = MoneyType;
			modelWeiXinMoney.StartMoney = StartMoney;
			modelWeiXinMoney.EndMoney = EndMoney;
			modelWeiXinMoney.FixedMoney = FixedMoney;
			modelWeiXinMoney.MaxCount = MaxCount;
			modelWeiXinMoney.MoneyRate = MoneyRate;
			modelWeiXinMoney.GiveMoney = 0m;
			modelWeiXinMoney.MoneyRegion = this.txtMoneyRegion.Value;
			modelWeiXinMoney.QuerySql = this.txtQuerySql.Value;
			if (MoneyID == 0)
			{
				modelWeiXinMoney.CreateTime = DateTime.Now;
				modelWeiXinMoney.CreateUserID = this._UserID;
				int intMoneyID = bllWeiXinMoney.Add(modelWeiXinMoney);
				Chain.BLL.WeiXinMoneyMem bllWeiXinMoneyMem = new Chain.BLL.WeiXinMoneyMem();
				for (int i = 0; i < strMemIDList.Length; i++)
				{
					bllWeiXinMoneyMem.Add(new Chain.Model.WeiXinMoneyMem
					{
						MemID = int.Parse(strMemIDList[i]),
						MoneyID = intMoneyID
					});
				}
				base.MessagePageShowError("保存成功！");
				base.Response.Redirect("WeiXinMoneyList.aspx");
			}
			else
			{
				modelWeiXinMoney.MoneyID = MoneyID;
				if (bllWeiXinMoney.Update(modelWeiXinMoney) > 0)
				{
					base.MessagePageShowError("修改成功！");
					base.Response.Redirect("WeiXinMoneyList.aspx");
				}
				else
				{
					base.MessagePageShowError("修改失败！");
				}
			}
		}
	}
}
