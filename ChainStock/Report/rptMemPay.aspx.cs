using Chain.BLL;
using Chain.Model;
using ChainStock.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

namespace ChainStock.Report
{
	public class rptMemPay : PageBase
	{
		protected HtmlHead Head1;

		protected HtmlForm form1;

		protected Literal ltlTitle;

		protected HtmlInputText txtQueryMem;

		protected Button btnListQuery;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltShop;

		protected HtmlSelect sltMemPoint;

		protected HtmlInputText txtMemPoint;

		protected HtmlSelect sltMemUserID;

		protected HtmlSelect sltMemBirthday;

		protected HtmlSelect sltMemMoney;

		protected HtmlInputText txtMemMoney;

		protected HtmlSelect sltMemState;

		protected HtmlSelect sltMemPastTime;

		protected HtmlSelect sltConsumeMoney;

		protected HtmlInputText txtConsumeMoney;

		protected HtmlSelect sltCustomField;

		protected Literal ltCustom;

		protected HtmlInputText txtCustomField;

		protected HtmlSelect sltMemConsume;

		protected HtmlInputText txtMemStartTime;

		protected HtmlInputText txtMemEndTime;

		protected HtmlInputText txtMemRecommendCard;

		protected ChainStock.Controls.SysArea SysArea1;

		protected Repeater gvMemList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch1;

		private readonly Chain.BLL.Mem member = new Chain.BLL.Mem();

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ltCustom.Text = "<input type='text' id='text' name='text' class='border_radius'/>";
			PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
			PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
			PubFunction.BindCustomField(this._UserShopID, this.sltCustomField, true, 1);
			PubFunction.BindUserSelect(this._UserShopID, this.sltMemUserID, true, false);
			if (PubFunction.curParameter.dataAuthority == 1 && this._UserShopID > 1)
			{
				this.sltShop.Value = this._UserShopID.ToString();
			}
			if (!base.IsPostBack)
			{
				this.Query();
			}
		}

		private void Query()
		{
			List<string> condition = this.QueryCondition();
			int Counts = this.NetPagerParameter.RecordCount;
			using (DataTable dtMem = this.member.getMemPayList(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, condition.ToArray()))
			{
				this.NetPagerParameter.RecordCount = Counts;
				this.NetPagerParameter.CustomInfoHTML = string.Format("<div class=\"results\"><span>当前第{0}/{1}页 共{2}条记录 每页{3}条</span></div>", new object[]
				{
					this.NetPagerParameter.CurrentPageIndex,
					this.NetPagerParameter.PageCount,
					this.NetPagerParameter.RecordCount,
					this.NetPagerParameter.PageSize
				});
				this.gvMemList.DataSource = dtMem;
				this.gvMemList.DataBind();
			}
			PageBase.BindSerialRepeater(this.gvMemList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
		}

		private List<string> QueryCondition()
		{
			List<string> condition = new List<string>();
			condition.Add(PubFunction.GetShopAuthority(this._UserShopID, "MemShopID", " 1=1 "));
			string strQueryMem = this.txtQueryMem.Value;
			string strMemLevelID = this.sltMemLevelID.Value;
			string strMemShopID = this.sltShop.Value;
			string strMemPastTime = this.sltMemPastTime.Value;
			string strMemCustom = this.sltCustomField.Value;
			string strCustomField = (!string.IsNullOrEmpty(base.Request["txtCustomField"])) ? base.Request["txtCustomField"].ToString() : "";
			string strMemState = this.sltMemState.Value;
			string strMemUserID = this.sltMemUserID.Value;
			string strMemStartTime = this.txtMemStartTime.Value;
			string strMemEndTime = this.txtMemEndTime.Value;
			string strMemBirthday = this.sltMemBirthday.Value;
			string strMemConsume = this.sltMemConsume.Value;
			string strPointSymbols = this.sltMemPoint.Value;
			string strPoint = (this.txtMemPoint.Value.Trim() != "") ? this.txtMemPoint.Value.Trim() : "0";
			string strMoneySymbols = this.sltMemMoney.Value;
			string strMemMoney = (this.txtMemMoney.Value.Trim() != "") ? this.txtMemMoney.Value.Trim() : "0";
			string strConsumeMoneySymbols = this.sltConsumeMoney.Value;
			string strConsumeMoney = (this.txtConsumeMoney.Value.Trim() != "") ? this.txtConsumeMoney.Value.Trim() : "0";
			string strProvince = this.SysArea1.sltProvince.Value;
			string strCity = (base.Request["SysArea1$sltCity"] != null) ? base.Request["SysArea1$sltCity"].ToString() : "";
			string strCounty = (base.Request["SysArea1$sltCounty"] != null) ? base.Request["SysArea1$sltCounty"].ToString() : "";
			string strVillage = (base.Request["SysArea1$sltVillage"] != null) ? base.Request["SysArea1$sltVillage"].ToString() : "";
			string strRecommend = this.txtMemRecommendCard.Value;
			if (strQueryMem != "")
			{
				condition.Add(string.Format(" (MemCard ='{0}' or MemCardNumber='{0}' or MemName like '%{0}%' or MemMobile='{0}')", strQueryMem));
			}
			if (strMemLevelID != "")
			{
				condition.Add(string.Format(" Mem.MemLevelID={0}", strMemLevelID));
			}
			if (strMemShopID != "")
			{
				condition.Add(string.Format(" MemShopID={0}", strMemShopID));
			}
			if (strMemPastTime != "")
			{
				if (strMemPastTime == "0")
				{
					condition.Add(string.Format("MemPastTime<'{0}'", DateTime.Now.ToShortDateString()));
				}
				else if (strMemPastTime == "1")
				{
					condition.Add(string.Format("MemPastTime BETWEEN '{0}' AND '{1}'", DateTime.Now.AddDays(1.0).ToShortDateString(), DateTime.Now.AddDays(7.0).ToShortDateString()));
				}
				else if (strMemPastTime == "2")
				{
					condition.Add(string.Format("MemPastTime BETWEEN '{0}' AND '{1}'", DateTime.Now.AddDays(1.0).ToShortDateString(), DateTime.Now.AddDays(30.0).ToShortDateString()));
				}
			}
			if (strMemConsume != "")
			{
				condition.Add("MemConsumeLastTime>'1901-01-01'");
				if (strMemConsume == "0")
				{
					condition.Add("MemConsumeLastTime<DATEADD(M,-3,GETDATE())");
				}
				else if (strMemConsume == "1")
				{
					condition.Add("MemConsumeLastTime<DATEADD(M,-6,GETDATE())");
				}
				else if (strMemConsume == "2")
				{
					condition.Add("MemConsumeLastTime<DATEADD(Y,-1,GETDATE())");
				}
			}
			if (strMemBirthday != "")
			{
				condition.Add("MemBirthday>'1900-01-02'");
				string text = strMemBirthday;
				if (text != null)
				{
					if (!(text == "0"))
					{
						if (!(text == "1"))
						{
							if (text == "2")
							{
								condition.Add(string.Format("MONTH(MemBirthday)={0}", DateTime.Now.Month));
							}
						}
						else
						{
							condition.Add(string.Format("DATEDIFF(WEEK,'{0}-'+CONVERT(VARCHAR(5),GETDATE(),110),GETDATE())=0", DateTime.Now.Year));
						}
					}
					else
					{
						condition.Add(string.Format("MONTH(MemBirthday)={0} AND DAY(MemBirthday)={1}", DateTime.Now.Month, DateTime.Now.Day));
					}
				}
			}
			if (strMemCustom != "" && strCustomField != "")
			{
				condition.Add(string.Format(" {0} like '%{1}%'", strMemCustom, strCustomField));
			}
			if (strMemState != "")
			{
				condition.Add(string.Format("MemState={0} ", int.Parse(strMemState)));
			}
			if (strMemUserID != "")
			{
				condition.Add(string.Format("MemUserID={0} ", int.Parse(strMemUserID)));
			}
			if (strMemStartTime != "")
			{
				condition.Add(string.Format("MemCreateTime>='{0}' ", strMemStartTime));
			}
			if (strMemEndTime != "")
			{
				condition.Add(string.Format("MemCreateTime<'{0}'", DateTime.Now.AddDays(1.0).ToShortDateString()));
			}
			if (int.Parse(strPoint) != 0)
			{
				condition.Add(string.Format("MemPoint {1} {0} ", strPoint, strPointSymbols));
			}
			if (decimal.Parse(strMemMoney) != 0m)
			{
				condition.Add(string.Format("MemMoney {1} {0} ", strMemMoney, strMoneySymbols));
			}
			if (decimal.Parse(strConsumeMoney) != 0m)
			{
				condition.Add(string.Format("MemConsumeMoney {1} {0} ", strConsumeMoney, strConsumeMoneySymbols));
			}
			if (strProvince != "")
			{
				condition.Add(string.Format("MemProvince='{0}'", strProvince));
			}
			if (strCity != "" || strProvince != "")
			{
				this.SysArea1.sltCity.Items.Clear();
				PubFunction.BindSysArea(this.SysArea1.sltCity, int.Parse(strProvince));
				this.SysArea1.sltCity.Value = strCity;
				if (strCity != "")
				{
					condition.Add(string.Format("MemCity='{0}'", strCity));
				}
			}
			if (strCounty != "" || strCity != "")
			{
				this.SysArea1.sltCounty.Items.Clear();
				PubFunction.BindSysArea(this.SysArea1.sltCounty, int.Parse(strCity));
				this.SysArea1.sltCounty.Value = strCounty;
				if (strCounty != "")
				{
					condition.Add(string.Format("MemCounty='{0}'", strCounty));
				}
			}
			if (strVillage != "" || strCounty != "")
			{
				this.SysArea1.sltVillage.Items.Clear();
				PubFunction.BindSysArea(this.SysArea1.sltVillage, int.Parse(strCounty));
				this.SysArea1.sltVillage.Value = strVillage;
				if (strVillage != "")
				{
					condition.Add(string.Format("MemVillage='{0}'", strVillage));
				}
			}
			if (strRecommend != "")
			{
				Chain.Model.Mem memModel = new Chain.BLL.Mem().GetModelByMemCard(strRecommend);
				if (memModel != null)
				{
					condition.Add(string.Format("MemRecommendID={0}", memModel.MemID));
				}
				else
				{
					condition.Add("1=2");
				}
			}
			this.BindCustomSelect(this.sltCustomField, strMemCustom, strCustomField, this._UserShopID);
			return condition;
		}

		public void BindCustomSelect(HtmlSelect select, string strMemCustom, string strCustomField, int intShopID)
		{
			StringBuilder sbHtml = new StringBuilder();
			if (select.Value != "")
			{
				select.Items.Clear();
				PubFunction.BindCustomField(intShopID, select, true, 1);
				select.Value = strMemCustom;
			}
			if (strCustomField != "")
			{
				string strCustomType = select.Items.FindByValue(select.Value).Attributes["CustomFieldType"].ToString();
				string strCustomInfo = select.Items.FindByValue(select.Value).Attributes["CustomFieldInfo"].ToString();
				string text = strCustomType;
				if (text != null)
				{
					if (!(text == "text"))
					{
						if (!(text == "select"))
						{
							if (text == "date")
							{
								sbHtml.Append("<input type='text' id='txtCustomField' class='Wdate border_radius' name='txtCustomField' value='" + strCustomField + "'  onfocus=\"WdatePicker({ skin: 'ext', isShowClear: false, readOnly: true });\"/>");
							}
						}
						else
						{
							string[] strInfo = strCustomInfo.Split("|".ToCharArray());
							sbHtml.Append("<select id='txtCustomField' name ='txtCustomField' class='selectWidth' >");
							sbHtml.Append("<option value='无'>===== 请选择 =====</option>");
							for (int i = 0; i < strInfo.Length; i++)
							{
								if (strInfo[i] == strCustomField)
								{
									sbHtml.Append(string.Concat(new string[]
									{
										"<option value='",
										strInfo[i],
										"' selected>",
										strInfo[i],
										"</option>"
									}));
								}
								else
								{
									sbHtml.Append(string.Concat(new string[]
									{
										"<option value='",
										strInfo[i],
										"'>",
										strInfo[i],
										"</option>"
									}));
								}
							}
							sbHtml.Append("</select>");
						}
					}
					else
					{
						sbHtml.Append("<input type='text' id='txtCustomField' name ='txtCustomField' value='" + strCustomField + "' class='input_txt border_radius'/>");
					}
				}
				this.ltCustom.Text = sbHtml.ToString();
			}
		}

		protected void btnQuery_Click(object sender, EventArgs e)
		{
			this.Query();
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.Query();
		}

		protected void gvMemList_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.Query();
		}

		protected string GetMemState(int memState)
		{
			string strState = "";
			switch (memState)
			{
			case 0:
				strState = "正常";
				break;
			case 1:
				strState = "锁定";
				break;
			case 2:
				strState = "挂失";
				break;
			}
			return strState;
		}
	}
}
