using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.member
{
	public class getmoney : Page
	{
		protected HtmlGenericControl spNoUseCount;

		protected Repeater rptWinList;

		protected HtmlGenericControl spStartTime;

		protected HtmlGenericControl spEndTime;

		protected HtmlGenericControl spDesc;

		protected HtmlInputHidden txtMemID;

		protected HtmlInputHidden txtImageUrl;

		protected HtmlInputHidden txtGetMoneyID;

		protected HtmlInputHidden MemCount;

		protected HtmlInputHidden MaxCount;

		protected HtmlInputHidden MoneyRate;

		protected HtmlInputHidden MemtotalCount;

		protected HtmlInputHidden TotalMoney;

		protected HtmlInputHidden GiveMoney;

		protected HtmlInputHidden StartMoney;

		protected HtmlInputHidden EndMoney;

		protected HtmlInputHidden MoneyType;

		protected HtmlInputHidden FixedMoney;

		protected HtmlInputHidden IsWin;

		protected HtmlInputHidden IsSuccess;

		protected HtmlInputHidden OpenID;

		protected HtmlInputHidden IsOwn;

		protected HtmlInputHidden txtMsg;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["MemID"] != null)
				{
					int memID = int.Parse(this.Session["MemID"].ToString());
					this.txtMemID.Value = memID.ToString();
					if (base.Request["MoneyID"] != null)
					{
						string moneyID = base.Request["MoneyID"].ToString();
						this.BindWeiXinMoney(int.Parse(base.Request["MoneyID"]));
						this.txtGetMoneyID.Value = base.Request["MoneyID"];
					}
					string openid = new Chain.BLL.Mem().GetWeiXinMemCardbyMemID(memID);
					this.OpenID.Value = openid;
					if (base.Request["Win"] != null)
					{
						Chain.BLL.WeiXinMoney bllWeiXinMoney = new Chain.BLL.WeiXinMoney();
						Chain.Model.WeiXinMoney modelWeiXinMoney = bllWeiXinMoney.GetModel(int.Parse(this.txtGetMoneyID.Value));
						decimal givemoney = new Chain.BLL.WeiXinGiveMoney().GetMoneySum(" MoneyID=" + modelWeiXinMoney.MoneyID);
						decimal totalmoney = modelWeiXinMoney.TotalMoney;
						if (totalmoney - givemoney < 1m)
						{
							this.IsWin.Value = "-1";
							this.txtMsg.Value = "红包已抢完！";
						}
						else
						{
							int intWin = this.isWin(decimal.Parse(this.MoneyRate.Value), int.Parse(this.MemtotalCount.Value));
							this.IsWin.Value = intWin.ToString();
							if (intWin == 0)
							{
								Chain.BLL.WeiXinGiveMoney bllWeiXinGiveMoney = new Chain.BLL.WeiXinGiveMoney();
								bllWeiXinGiveMoney.Add(new Chain.Model.WeiXinGiveMoney
								{
									MemID = int.Parse(this.txtMemID.Value),
									MoneyID = int.Parse(this.txtGetMoneyID.Value),
									GiveMoney = 0m,
									GiveTime = DateTime.Now,
									IsWin = 0
								});
							}
						}
					}
					if (base.Request["IsSuccess"] != null)
					{
						this.IsSuccess.Value = base.Request["IsSuccess"];
					}
					this.rptWinListBind();
				}
				else
				{
					base.Response.Redirect("login.aspx");
				}
			}
		}

		private void rptWinListBind()
		{
			this.rptWinList.DataSource = new Chain.BLL.WeiXinGiveMoney().GetList(10, " WeiXinGiveMoney.MemID=Mem.MemID and IsWin=1 and WeiXinGiveMoney.MoneyID=" + this.txtGetMoneyID.Value, "WeiXinGiveMoney.GiveTime");
			this.rptWinList.DataBind();
		}

		private void BindWeiXinMoney(int moneyID)
		{
			Chain.BLL.WeiXinMoney bllWeiXinMoney = new Chain.BLL.WeiXinMoney();
			Chain.Model.WeiXinMoney modelWeiXinMoney = bllWeiXinMoney.GetModel(moneyID);
			this.spEndTime.InnerHtml = modelWeiXinMoney.EndTime.ToString("yyyy.MM.dd HH:mm");
			this.spStartTime.InnerHtml = modelWeiXinMoney.StartTime.ToString("yyyy.MM.dd HH:mm");
			int count = new Chain.BLL.WeiXinMoneyMem().GetRecordCount("MoneyID=" + modelWeiXinMoney.MoneyID);
			this.MemtotalCount.Value = count.ToString();
			DataTable dt;
			if (modelWeiXinMoney.QuerySql != null)
			{
				dt = new Chain.BLL.Mem().GetList(modelWeiXinMoney.QuerySql).Tables[0];
			}
			else
			{
				dt = new Chain.BLL.Mem().GetList("").Tables[0];
			}
			this.IsOwn.Value = "0";
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (this.txtMemID.Value == dt.Rows[i]["MemID"].ToString())
				{
					this.IsOwn.Value = "1";
					break;
				}
			}
			int maxcount = modelWeiXinMoney.MaxCount;
			int memcount = new Chain.BLL.WeiXinGiveMoney().GetRecordCount(string.Concat(new object[]
			{
				" MemID=",
				int.Parse(this.txtMemID.Value),
				" and MoneyID=",
				modelWeiXinMoney.MoneyID
			}));
			this.MemCount.Value = (maxcount - memcount).ToString();
			this.spNoUseCount.InnerHtml = this.MemCount.Value;
			this.MaxCount.Value = modelWeiXinMoney.MaxCount.ToString();
			this.MoneyRate.Value = modelWeiXinMoney.MoneyRate.ToString();
			this.TotalMoney.Value = modelWeiXinMoney.TotalMoney.ToString();
			this.StartMoney.Value = modelWeiXinMoney.StartMoney.ToString();
			this.EndMoney.Value = modelWeiXinMoney.EndMoney.ToString();
			this.MoneyType.Value = modelWeiXinMoney.MoneyType.ToString();
			this.FixedMoney.Value = modelWeiXinMoney.FixedMoney.ToString();
			this.txtImageUrl.Value = modelWeiXinMoney.ImageUrl.ToString();
			this.spDesc.InnerHtml = modelWeiXinMoney.MoneyDesc.ToString();
			decimal money = new Chain.BLL.WeiXinGiveMoney().GetMoneySum(" MoneyID=" + modelWeiXinMoney.MoneyID);
			this.GiveMoney.Value = money.ToString();
		}

		public int isWin(decimal rate, int totalCount)
		{
			double winCount = Math.Floor(double.Parse((rate * 100m).ToString()));
			Random rd = new Random();
			int r = rd.Next(0, 10000);
			int result;
			if ((double)r < winCount)
			{
				result = 1;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		public string BindPhoto(object photo)
		{
			string strPhoto;
			if (photo != null && photo.ToString() != "")
			{
				strPhoto = photo.ToString();
			}
			else
			{
				strPhoto = "../website/images/head.png";
			}
			return strPhoto;
		}

		public string BindTime(object time)
		{
			string strTime = "";
			if (time != null)
			{
				strTime = DateTime.Parse(time.ToString()).ToString("MM月dd日 HH:mm");
			}
			return strTime;
		}

		public string BindMobile(object mobile)
		{
			string strMobile = "";
			if (mobile != null)
			{
				strMobile = mobile.ToString();
				if (strMobile != "")
				{
					strMobile = strMobile.Substring(0, 3) + "****" + strMobile.Substring(6, 4);
				}
			}
			return strMobile;
		}
	}
}
