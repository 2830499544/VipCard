using Chain.BLL;
using Chain.Model;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using WxPayAPI;

namespace ChainStock.mobile.member
{
	public class givemoney : Page
	{
		protected HtmlGenericControl spResultInfo;

		protected HtmlGenericControl moneyInfo;

		protected HtmlGenericControl spGetMoney;

		protected HtmlInputHidden txtMoneyID;

		protected HtmlInputHidden txtMemID;

		protected HtmlInputHidden MemCount;

		protected HtmlInputHidden MaxCount;

		protected HtmlInputHidden MoneyRate;

		protected HtmlInputHidden MemtotalCount;

		protected HtmlInputHidden TotalMoney;

		protected HtmlInputHidden StartMoney;

		protected HtmlInputHidden EndMoney;

		protected HtmlInputHidden MoneyType;

		protected HtmlInputHidden FixedMoney;

		protected HtmlInputHidden IsWin;

		protected HtmlInputHidden txtLinkUrl;

		protected HtmlInputHidden txtgetmoney;

		protected HtmlInputHidden txtRemainMoney;

		protected HtmlInputHidden txtRemainCount;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int memID = int.Parse(this.Session["MemID"].ToString());
					this.txtMemID.Value = memID.ToString();
					if (base.Request.QueryString["MoneyID"] != null)
					{
						this.txtMoneyID.Value = base.Request.QueryString["MoneyID"].ToString();
						this.BindWeiXinMoney(int.Parse(this.txtMoneyID.Value));
					}
					if (int.Parse(this.MemCount.Value) > 0)
					{
						string openid = new Chain.BLL.Mem().GetModel(memID).MemWeiXinCard;
						if (string.IsNullOrEmpty(openid))
						{
							base.Response.Write("<span style='color:#FF0000;font-size:20px'>页面传参出错,请返回重试</span>");
						}
						else
						{
							double getmoney = double.Parse(this.txtgetmoney.Value);
							Chain.BLL.WeiXinMoney bllWeiXinMoney = new Chain.BLL.WeiXinMoney();
							Chain.Model.WeiXinMoney modelWeiXinMoney = bllWeiXinMoney.GetModel(int.Parse(this.txtMoneyID.Value));
							string mch_billno = DateTime.Now.ToString("yyMMddHHmmssffff");
							int total_amount = int.Parse((getmoney * 100.0).ToString(""));
							string wishing = modelWeiXinMoney.MoneyWish;
							string remark = modelWeiXinMoney.MoneyDesc;
							string act_name = modelWeiXinMoney.MoneyTitle;
							string re_openid = openid;
							WxPayData data = new WxPayData();
							data.SetValue("mch_billno", mch_billno);
							data.SetValue("mch_id", PubFunction.curParameter.strMchid);
							data.SetValue("wxappid", PubFunction.curParameter.strWeiXinAppID);
							data.SetValue("send_name", "智络");
							data.SetValue("re_openid", re_openid);
							data.SetValue("total_amount", total_amount);
							data.SetValue("total_num", 1);
							data.SetValue("wishing", wishing);
							data.SetValue("client_ip", PubFunction.ipAdress);
							data.SetValue("act_name", act_name);
							data.SetValue("remark", remark);
							WxPayData result = WxPayApi.Sendredpack(data, PubFunction.curParameter.strMchKey, "cert\\apiclient_cert.p12", PubFunction.curParameter.strMchid, 10);
							if (!result.IsSet("return_code") || result.GetValue("return_code").ToString() == "FAIL")
							{
								string arg_2DD_0 = result.IsSet("return_msg") ? result.GetValue("return_msg").ToString() : "";
								this.spGetMoney.InnerHtml = "";
								this.spResultInfo.InnerHtml = "红包接口调用失败！" + result.GetValue("return_msg").ToString();
							}
							else if (result.GetValue("return_code").ToString() == "SUCCESS" && result.GetValue("result_code").ToString() == "SUCCESS")
							{
								if (int.Parse(this.MemCount.Value) > 0)
								{
									Chain.BLL.WeiXinGiveMoney bllWeiXinGiveMoney = new Chain.BLL.WeiXinGiveMoney();
									bllWeiXinGiveMoney.Add(new Chain.Model.WeiXinGiveMoney
									{
										MemID = int.Parse(this.txtMemID.Value),
										MoneyID = int.Parse(this.txtMoneyID.Value),
										GiveMoney = decimal.Parse(getmoney.ToString()),
										GiveTime = DateTime.Now,
										IsWin = 1
									});
									bllWeiXinMoney.UpdateGiveMoney(int.Parse(this.txtMoneyID.Value), decimal.Parse(getmoney.ToString()));
									this.spResultInfo.InnerHtml = "红包发放成功！";
									this.spGetMoney.InnerHtml = getmoney.ToString("#0.00");
									this.moneyInfo.Visible = true;
								}
								else
								{
									this.spGetMoney.InnerHtml = "";
									this.spResultInfo.InnerHtml = "您的红包领取次数已用完！";
								}
							}
							else
							{
								this.spGetMoney.InnerHtml = "";
								this.spResultInfo.InnerHtml = "红包发放失败！" + result.GetValue("return_msg").ToString();
							}
						}
					}
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		private void BindWeiXinMoney(int MoneyID)
		{
			Chain.Model.WeiXinMoney modelWeiXinMoney = new Chain.BLL.WeiXinMoney().GetModel(MoneyID);
			int maxcount = modelWeiXinMoney.MaxCount;
			int memcount = new Chain.BLL.WeiXinGiveMoney().GetRecordCount(string.Concat(new object[]
			{
				" MemID=",
				int.Parse(this.txtMemID.Value),
				" and MoneyID=",
				modelWeiXinMoney.MoneyID
			}));
			this.MemCount.Value = (maxcount - memcount).ToString();
			this.txtMoneyID.Value = modelWeiXinMoney.MoneyID.ToString();
			this.MaxCount.Value = modelWeiXinMoney.MaxCount.ToString();
			this.MoneyRate.Value = modelWeiXinMoney.MoneyRate.ToString();
			this.TotalMoney.Value = modelWeiXinMoney.TotalMoney.ToString();
			this.StartMoney.Value = modelWeiXinMoney.StartMoney.ToString();
			this.EndMoney.Value = modelWeiXinMoney.EndMoney.ToString();
			this.MoneyType.Value = modelWeiXinMoney.MoneyType.ToString();
			this.FixedMoney.Value = modelWeiXinMoney.FixedMoney.ToString();
			int count = new Chain.BLL.WeiXinMoneyMem().GetRecordCount("MoneyID=" + modelWeiXinMoney.MoneyID);
			int giveCount = new Chain.BLL.WeiXinGiveMoney().GetRecordCount("MoneyID=" + modelWeiXinMoney.MoneyID);
			this.MemtotalCount.Value = count.ToString();
			this.txtRemainMoney.Value = (modelWeiXinMoney.TotalMoney - modelWeiXinMoney.GiveMoney).ToString();
			this.txtRemainCount.Value = Math.Floor(double.Parse(((count - giveCount) * modelWeiXinMoney.MoneyRate / 100m).ToString())).ToString();
			double min = double.Parse(modelWeiXinMoney.StartMoney.ToString());
			double max = double.Parse(modelWeiXinMoney.EndMoney.ToString());
			double fixedmoney = double.Parse(modelWeiXinMoney.FixedMoney.ToString());
			double getmoney;
			if (modelWeiXinMoney.MoneyType == 1)
			{
				getmoney = this.GetRandomMoney(max, min);
			}
			else
			{
				getmoney = this.GetFixedMoney(fixedmoney);
			}
			this.txtgetmoney.Value = getmoney.ToString();
		}

		public double GetFixedMoney(double fixedmoney)
		{
			double RemainMoney = double.Parse(this.txtRemainMoney.Value);
			double money = fixedmoney;
			if (RemainMoney < money)
			{
				money = RemainMoney;
			}
			return money;
		}

		public double GetRandomMoney(double max, double min)
		{
			double RemainMoney = double.Parse(this.txtRemainMoney.Value);
			Random r = new Random();
			double money = r.NextDouble() * max;
			money = ((money <= min) ? min : money);
			money = Math.Floor(money * 100.0) / 100.0;
			if (RemainMoney < money)
			{
				money = RemainMoney;
			}
			return money;
		}
	}
}
