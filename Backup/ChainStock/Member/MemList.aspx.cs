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

namespace ChainStock.Member
{
	public class MemList : PageBase
	{
		protected HtmlForm form1;

		protected Literal ltlTitle;

		protected HtmlInputText txtQueryMem;

		protected Button btnMemListQuery;

		protected Button btnOut;

		protected HtmlInputButton btnMemRecharge;

		protected HtmlInputButton btnMemChangeCard;

		protected HtmlInputButton btnMemDelay;

		protected HtmlInputButton btnMemLock;

		protected HtmlInputButton btnMemChangePwd;

		protected HtmlInputButton btnMemDrawMoney;

		protected HtmlInputButton btnSenndSMS;

		protected HtmlInputButton btnMemPointReset;

		protected HtmlInputButton btnAllMemPointReset;

		protected HtmlInputButton btnCoupon;

		protected Button btnMoney;

		protected CheckBox chkSms;

		protected HtmlSelect sltCoupon;

		protected Label lblCouponTime;

		protected Label lblCouponMinMoney;

		protected Label lblCouponDayNum;

		protected Label lblCouponYF;

		protected Label lblCouponNu;

		protected Label lblChoosedMem;

		protected Label lblLockMem;

		protected Label lblNoMobile;

		protected Label lblSendNumber;

		protected Button btnSendCoupon;

		protected Button btnCloseCoupon;

		protected HtmlSelect sltMemLevelID;

		protected HtmlSelect sltShop;

		protected HtmlInputHidden HDsltshop;

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

		protected HtmlSelect sltMemConsume;

		protected HtmlInputText txtMemStartTime;

		protected HtmlInputText txtMemEndTime;

		protected HtmlInputText txtMemRecommendCard;

		protected ChainStock.Controls.SysArea SysArea1;

		protected HtmlSelect selMemWeiXinCard;

		protected HtmlSelect selMemAttention;

		protected HtmlInputCheckBox chkAll;

		protected Literal ltlHeader;

		protected Repeater gvMemList;

		protected DropDownList drpPageSize;

		protected AspNetPager NetPagerParameter;

		protected QuickSearch QuickSearch1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.Session["QuerySql"] = null;
				this.ltCustom.Text = "<input type='text' id='text' name='text' class='border_radius'/>";
				PubFunction.BindMemLevelSelect(this.sltMemLevelID, true);
				PubFunction.BindShopSelect(this._UserShopID, this.sltShop, true);
				PubFunction.BindCustomField(this._UserShopID, this.sltCustomField, true, 1);
				PubFunction.BindUserSelect(this._UserShopID, this.sltMemUserID, true, false);
				if (PubFunction.curParameter.dataAuthority == 1)
				{
					if (this._UserShopID > 1)
					{
						this.sltShop.Value = this._UserShopID.ToString();
					}
				}
				this.GetMemList(this.QueryCondition());
				this.SetQuickBtn();
				this.chkSms.Checked = this.curParameter.bolSms;
				PubFunction.BindCouponSelect(this.sltCoupon, false);
				this.btnAllMemPointReset.Visible = false;
			}
		}

		protected void btnMoney_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("../MicroWebsite/WeiXinMoneyInfo.aspx");
		}

		private void SetQuickBtn()
		{
			DataTable dt = PubFunction.GetGroupAuthority(this._UserGroupID);
			this.btnMemRecharge.Visible = PubFunction.GetPageVisit(dt, int.Parse(this.btnMemRecharge.Attributes["cls"]));
			this.btnMemChangeCard.Visible = PubFunction.GetPageVisit(dt, int.Parse(this.btnMemChangeCard.Attributes["cls"]));
			this.btnMemDelay.Visible = PubFunction.GetPageVisit(dt, int.Parse(this.btnMemDelay.Attributes["cls"]));
			this.btnMemLock.Visible = PubFunction.GetPageVisit(dt, int.Parse(this.btnMemLock.Attributes["cls"]));
			this.btnMemChangePwd.Visible = PubFunction.GetPageVisit(dt, int.Parse(this.btnMemChangePwd.Attributes["cls"]));
			this.btnMemDrawMoney.Visible = PubFunction.GetPageVisit(dt, int.Parse(this.btnMemDrawMoney.Attributes["cls"]));
			this.btnSenndSMS.Visible = PubFunction.GetPageVisit(dt, int.Parse(this.btnSenndSMS.Attributes["cls"]));
			if (this.btnMemChangePwd.Visible)
			{
				this.btnMemChangePwd.Visible = PubFunction.curParameter.bolPwd;
			}
			if (this.btnMemDelay.Visible)
			{
				this.btnMemDelay.Visible = PubFunction.curParameter.bolPastTime;
			}
			if (this.btnSenndSMS.Visible)
			{
				this.btnSenndSMS.Visible = PubFunction.curParameter.bolSms;
			}
		}

		private void GetMemList(string strSql)
		{
			Chain.BLL.Mem member = new Chain.BLL.Mem();
			int Counts = this.NetPagerParameter.RecordCount;
			strSql += " and Mem.MemShopID = SysShop.ShopID and Mem.MemLevelID = MemLevel.LevelID and Mem.MemUserID = SysUser.UserID ";
			strSql += " and Mem.MemShopID =SysShopMemLevel.ShopID and SysShopMemLevel.MemLevelID=MemLevel.LevelID ";
			DataTable dtMem = member.GetListSP(this.NetPagerParameter.PageSize, this.NetPagerParameter.CurrentPageIndex, out Counts, new string[]
			{
				PubFunction.GetMemListShopAuthority(this._UserShopID, "MemShopID", strSql)
			}).Tables[0];
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
			PageBase.BindSerialRepeater(this.gvMemList, this.NetPagerParameter.PageSize * (this.NetPagerParameter.CurrentPageIndex - 1));
			Chain.BLL.MemCustomField bllCustom = new Chain.BLL.MemCustomField();
			List<Chain.Model.MemCustomField> fieldlist = bllCustom.GetModelList("CustomType=1 and CustomFieldIsShow=1");
			if (fieldlist.Count > 0)
			{
				StringBuilder strHeader = new StringBuilder();
				StringBuilder strHtml = new StringBuilder();
				for (int i = 0; i < this.gvMemList.Items.Count; i++)
				{
					Literal ltlMemID = (Literal)this.gvMemList.Items[i].FindControl("ltlMemID");
					Literal ltlHtml = (Literal)this.gvMemList.Items[i].FindControl("ltlHtml");
					int MemID = Convert.ToInt32(ltlMemID.Text);
					DataRow[] drMem = dtMem.Select(string.Format(" MemID = {0}", MemID));
					strHtml.Length = 0;
					foreach (Chain.Model.MemCustomField mdCustomField in fieldlist)
					{
						if (i == 0)
						{
							strHeader.AppendFormat("<th>{0}</th>", mdCustomField.CustomFieldName);
						}
						strHtml.AppendFormat("<td>{0}</td>", drMem[0][mdCustomField.CustomField]);
					}
					ltlHtml.Text = strHtml.ToString();
				}
				this.ltlHeader.Text = strHeader.ToString();
			}
		}

		protected void NetPagerParameter_PageChanging(object src, PageChangingEventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = e.NewPageIndex;
			this.GetMemList(this.QueryCondition());
		}

		protected void btnMemListQuery_Click(object sender, EventArgs e)
		{
			string strPoint = this.txtMemPoint.Value.Trim();
			string strMemMoney = this.txtMemMoney.Value.Trim();
			string strConsumeMoney = this.txtConsumeMoney.Value.Trim();
			if (strPoint != "")
			{
				try
				{
					if (int.Parse(strPoint) < 0)
					{
						base.OutputWarn("会员积分必须输入大于等于0的整数！");
						return;
					}
				}
				catch
				{
					base.OutputWarn("会员积分必须输入大于等于0的整数！");
					return;
				}
			}
			if (strMemMoney != "")
			{
				try
				{
					if (decimal.Parse(strMemMoney) < 0m)
					{
						base.OutputWarn("会员余额必须输入大于等于0数字");
						return;
					}
				}
				catch
				{
					base.OutputWarn("会员余额必须输入大于等于0数字");
					return;
				}
			}
			if (strConsumeMoney != "")
			{
				try
				{
					if (decimal.Parse(strConsumeMoney) < 0m)
					{
						base.OutputWarn("会员余额必须输入大于等于0数字");
						return;
					}
				}
				catch
				{
					base.OutputWarn("会员余额必须输入大于等于0数字");
					return;
				}
			}
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.GetMemList(this.QueryCondition());
		}

		protected string QueryCondition()
		{
			int intMemWeiXinCard = Convert.ToInt32(this.selMemWeiXinCard.Value);
			int intMemAttention = Convert.ToInt32(this.selMemAttention.Value);
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
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" MemID >0 ");
			if (intMemWeiXinCard == 1)
			{
				strSql.AppendFormat("and Mem.MemWeiXinCard is not null and Mem.MemWeiXinCard!=''", new object[0]);
			}
			if (intMemWeiXinCard == 2)
			{
				strSql.AppendFormat("and (Mem.MemWeiXinCard is  null or Mem.MemWeiXinCard='')", new object[0]);
			}
			if (intMemAttention != 0)
			{
				strSql.AppendFormat("and Mem.MemAttention={0}", intMemAttention);
			}
			if (strQueryMem != "")
			{
				Chain.BLL.SubMem bllSubMem = new Chain.BLL.SubMem();
				DataTable dtSubMem = bllSubMem.GetList(" SubCardNumber='" + strQueryMem + "' and IsUsed ='true' ").Tables[0];
				if (dtSubMem != null)
				{
					if (dtSubMem.Rows.Count > 0)
					{
						strQueryMem = dtSubMem.Rows[0]["MemCard"].ToString();
					}
				}
				strSql.AppendFormat("and (MemCard ='{0}' or MemCardNumber='{0}' or MemName like '%{0}%' or MemMobile='{0}')", strQueryMem);
			}
			if (strMemLevelID != "")
			{
				strSql.AppendFormat("and Mem.MemLevelID={0}", int.Parse(strMemLevelID));
			}
			if (strMemShopID != "")
			{
				strSql.AppendFormat("and MemShopID={0}", int.Parse(strMemShopID));
			}
			if (strMemPastTime != "")
			{
				if (strMemPastTime == "0")
				{
					strSql.AppendFormat(" and getdate()>MemPastTime ", new object[0]);
				}
				else if (strMemPastTime == "1")
				{
					strSql.AppendFormat(" and datediff(day,getdate(),MemPastTime)<=7 and datediff(day,getdate(),MemPastTime)>0", new object[0]);
				}
				else if (strMemPastTime == "2")
				{
					strSql.AppendFormat(" and datediff(day,getdate(),MemPastTime)<=30 and datediff(day,getdate(),MemPastTime)>0 ", new object[0]);
				}
			}
			if (strMemConsume != "")
			{
				if (strMemConsume == "0")
				{
					strSql.AppendFormat(" and datediff(day,MemConsumeLastTime,getdate())>91 ", new object[0]);
				}
				else if (strMemConsume == "1")
				{
					strSql.AppendFormat(" and datediff(day,MemConsumeLastTime,getdate())>182 ", new object[0]);
				}
				else if (strMemConsume == "2")
				{
					strSql.AppendFormat(" and datediff(day,MemConsumeLastTime,getdate())>365 ", new object[0]);
				}
			}
			if (strMemBirthday != "")
			{
				string text = strMemBirthday;
				if (text != null)
				{
					if (!(text == "0"))
					{
						if (!(text == "1"))
						{
							if (text == "2")
							{
								string strMonth = DateTime.Now.ToString("MM/yy").Substring(0, 2);
								strSql.AppendFormat(" and convert(varchar(2),MemBirthday,107)={0} ", strMonth);
							}
						}
						else
						{
							string timeNow = DateTime.Now.ToShortDateString().Substring(0, 1);
							strSql.AppendFormat(" and datediff(week,(convert(varchar(5),getdate(),120)+convert(varchar(5),MemBirthday,110)),getdate())=0 ", new object[0]);
						}
					}
					else
					{
						strSql.AppendFormat(" and month(MemBirthday)=month(getdate()) and day(MemBirthday)-day(getdate())=0 ", new object[0]);
					}
				}
			}
			if (strMemCustom != "" && strCustomField != "")
			{
				strSql.AppendFormat(" and {0} like '%{1}%'", strMemCustom, strCustomField);
			}
			if (strMemState != "")
			{
				strSql.AppendFormat(" and MemState={0} ", int.Parse(strMemState));
			}
			if (strMemUserID != "")
			{
				strSql.AppendFormat(" and MemUserID={0} ", int.Parse(strMemUserID));
			}
			if (strMemStartTime != "")
			{
				strSql.AppendFormat(" and MemCreateTime>='{0}' ", strMemStartTime);
			}
			if (strMemEndTime != "")
			{
				strMemEndTime = Convert.ToDateTime(strMemEndTime).AddDays(1.0).ToString();
				strSql.AppendFormat(" and MemCreateTime<'{0}' ", strMemEndTime);
			}
			if (int.Parse(strPoint) != 0)
			{
				strSql.AppendFormat(" and MemPoint" + strPointSymbols + "{0} ", int.Parse(strPoint));
			}
			if (decimal.Parse(strMemMoney) != 0m)
			{
				strSql.AppendFormat(" and MemMoney" + strMoneySymbols + "{0} ", decimal.Parse(strMemMoney));
			}
			if (decimal.Parse(strConsumeMoney) != 0m)
			{
				strSql.AppendFormat(" and MemConsumeMoney" + strConsumeMoneySymbols + "{0} ", decimal.Parse(strConsumeMoney));
			}
			if (strProvince != "")
			{
				strSql.AppendFormat(" and MemProvince='{0}'", strProvince);
			}
			if (strCity != "" || strProvince != "")
			{
				this.SysArea1.sltCity.Items.Clear();
				PubFunction.BindSysArea(this.SysArea1.sltCity, int.Parse(strProvince));
				this.SysArea1.sltCity.Value = strCity;
				if (strCity != "")
				{
					strSql.AppendFormat(" and MemCity='{0}'", strCity);
				}
			}
			if (strCounty != "" || strCity != "")
			{
				this.SysArea1.sltCounty.Items.Clear();
				PubFunction.BindSysArea(this.SysArea1.sltCounty, int.Parse(strCity));
				this.SysArea1.sltCounty.Value = strCounty;
				if (strCounty != "")
				{
					strSql.AppendFormat(" and MemCounty='{0}'", strCounty);
				}
			}
			if (strVillage != "" || strCounty != "")
			{
				this.SysArea1.sltVillage.Items.Clear();
				PubFunction.BindSysArea(this.SysArea1.sltVillage, int.Parse(strCounty));
				this.SysArea1.sltVillage.Value = strVillage;
				if (strVillage != "")
				{
					strSql.AppendFormat(" and MemVillage='{0}'", strVillage);
				}
			}
			if (strRecommend != "")
			{
				Chain.Model.Mem memModel = new Chain.BLL.Mem().GetModelByMemCard(strRecommend);
				if (memModel != null)
				{
					strSql.AppendFormat(" and MemRecommendID=" + memModel.MemID, new object[0]);
				}
				else
				{
					strSql.Append(" and 1=2 ");
				}
			}
			this.BindCustomSelect(this.sltCustomField, strMemCustom, strCustomField, this._UserShopID);
			this.Session["QuerySql"] = strSql.ToString();
			return strSql.ToString();
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

		protected void BtnMemExcel_Click(object sender, EventArgs e)
		{
			Chain.BLL.Mem bllMember = new Chain.BLL.Mem();
			int Counts = this.NetPagerParameter.RecordCount;
			string strSql = this.QueryCondition();
			strSql += "and Mem.MemShopID = SysShop.ShopID and Mem.MemLevelID = MemLevel.LevelID and Mem.MemUserID = SysUser.UserID";
			strSql += " and Mem.MemShopID =SysShopMemLevel.ShopID and SysShopMemLevel.MemLevelID=MemLevel.LevelID ";
			DataTable db = bllMember.GetListSP(100000, 1, out Counts, new string[]
			{
				PubFunction.GetMemListShopAuthority(this._UserShopID, "MemShopID", strSql)
			}).Tables[0];
			DataExcelInfo.MemReportExcel(db, this._UserName);
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

		protected string GetMemBirthday(DateTime birthday)
		{
			string result;
			if (birthday.Year == 1900 && birthday.Month == 1 && birthday.Day == 1)
			{
				result = "";
			}
			else
			{
				result = birthday.ToString("yyyy-MM-dd");
			}
			return result;
		}

		protected void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.NetPagerParameter.CurrentPageIndex = 1;
			this.NetPagerParameter.PageSize = int.Parse(this.drpPageSize.SelectedValue);
			this.GetMemList(this.QueryCondition());
		}

		protected void gvMemList_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView dr = (DataRowView)e.Item.DataItem;
				HtmlInputCheckBox chkItem = (HtmlInputCheckBox)e.Item.FindControl("chkItem");
				chkItem.Attributes.Add("memID", dr["MemID"].ToString());
				chkItem.Attributes.Add("memcard", dr["MemCard"].ToString());
				chkItem.Attributes.Add("memname", dr["MemName"].ToString());
				chkItem.Attributes.Add("mobiles", dr["MemMobile"].ToString());
				chkItem.Attributes.Add("state", dr["MemState"].ToString());
				HtmlControl hyEdit = (HtmlControl)e.Item.FindControl("hyEdit");
				Chain.Model.SysShop modelShop = new Chain.BLL.SysShop().GetModel(this._UserShopID);
				if (modelShop.ShopType == 3)
				{
					hyEdit.Attributes.Add("style", "display:block;");
				}
				else
				{
					hyEdit.Attributes.Add("style", "display:none;");
				}
			}
		}
	}
}
