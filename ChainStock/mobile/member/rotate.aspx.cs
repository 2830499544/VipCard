using Chain.BLL;
using Chain.Model;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChainStock.mobile.member
{
	public class rotate : Page
	{
		private Chain.BLL.SysRotatePrizeLog bllSysRotatePrizeLog = new Chain.BLL.SysRotatePrizeLog();

		private Chain.BLL.SysRotate bllSysRotate = new Chain.BLL.SysRotate();

		protected HtmlGenericControl spOnePrizeName;

		protected HtmlGenericControl spOneName;

		protected HtmlGenericControl spTwoPrizeName;

		protected HtmlGenericControl spTwoName;

		protected HtmlGenericControl spThreePrizeName;

		protected HtmlGenericControl spThreeName;

		protected HtmlGenericControl spFourPrizeName;

		protected HtmlGenericControl spFourName;

		protected HtmlGenericControl spFivePrizeName;

		protected HtmlGenericControl spFiveName;

		protected HtmlGenericControl spSixPrizeName;

		protected HtmlGenericControl spSixName;

		protected HtmlGenericControl spNoUseCount;

		protected Repeater rptWinList;

		protected HtmlGenericControl spStartTime;

		protected HtmlGenericControl spEndTime;

		protected HtmlGenericControl spRotateDesc;

		protected HtmlGenericControl Span1;

		protected HtmlGenericControl spGetPrizeLevel;

		protected HtmlGenericControl spGetPrizeName;

		protected HtmlGenericControl spCode;

		protected HtmlGenericControl spWinArray;

		protected HtmlGenericControl spRoateArray;

		protected HtmlGenericControl spRotateID;

		protected HtmlGenericControl spRotateCount;

		protected HtmlGenericControl spPersonTotalCount;

		protected HtmlGenericControl spPersonTotalCountWin;

		protected HtmlGenericControl spPersonDayCount;

		protected HtmlGenericControl spPersonDayCountWin;

		protected HtmlInputHidden txtMemID;

		protected HtmlGenericControl spIsOne;

		protected HtmlGenericControl spIsTwo;

		protected HtmlGenericControl spIsThree;

		protected HtmlGenericControl spIsFour;

		protected HtmlGenericControl spIsFive;

		protected HtmlGenericControl spIsSix;

		protected HtmlGenericControl spOneRate;

		protected HtmlGenericControl spTwoRate;

		protected HtmlGenericControl spThreeRate;

		protected HtmlGenericControl spFourRate;

		protected HtmlGenericControl spFiveRate;

		protected HtmlGenericControl spSixRate;

		protected HtmlInputHidden txtMemTotalCount;

		protected HtmlInputHidden txtRotateID;

		protected HtmlInputHidden txtMsg;

		protected HtmlGenericControl spImageUrl;

		protected HtmlGenericControl spOnePrizeCount;

		protected HtmlGenericControl spTwoPrizeCount;

		protected HtmlGenericControl spThreePrizeCount;

		protected HtmlGenericControl spFourPrizeCount;

		protected HtmlGenericControl spFivePrizeCount;

		protected HtmlGenericControl spSixPrizeCount;

		protected HtmlGenericControl spOnePrizeWinCount;

		protected HtmlGenericControl spTwoPrizeWinCount;

		protected HtmlGenericControl spThreePrizeWinCount;

		protected HtmlGenericControl spFourPrizeWinCount;

		protected HtmlGenericControl spFivePrizeWinCount;

		protected HtmlGenericControl spSixPrizeWinCount;

		protected HtmlGenericControl spIsWinOne;

		protected HtmlGenericControl spWinCount;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.Session["MemID"] != null)
			{
				int MemID = int.Parse(this.Session["MemID"].ToString());
				this.txtMemID.Value = MemID.ToString();
				if (base.Request["RotateID"] != null)
				{
					this.txtRotateID.Value = base.Request["RotateID"].ToString();
					this.BinRotateInfo(int.Parse(base.Request["RotateID"].ToString()), MemID);
				}
				this.rptWinListBind();
			}
			else
			{
				base.Response.Redirect("login.aspx");
			}
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

		private void rptWinListBind()
		{
			this.rptWinList.DataSource = new Chain.BLL.SysRotatePrizeLog().GetList(10, "SysRotate.RotateID=SysRotatePrizeLog.RotateID and  SysRotatePrizeLog.MemID=Mem.MemID and PrizeLevel<>'未中奖' and SysRotatePrizeLog.RotateID=" + this.txtRotateID.Value, "SysRotatePrizeLog.CreateTime");
			this.rptWinList.DataBind();
		}

		private void BinRotateInfo(int RotateID, int MemID)
		{
			Chain.Model.SysRotate modelSysRotate = this.bllSysRotate.GetModel(RotateID);
			if (modelSysRotate.RotateID != 0)
			{
				if (modelSysRotate.ImageUrl != null && modelSysRotate.ImageUrl.ToString() != "")
				{
					this.spImageUrl.InnerHtml = modelSysRotate.ImageUrl;
				}
				this.spPersonTotalCount.InnerHtml = modelSysRotate.PersonTotalCount.ToString();
				this.spPersonDayCount.InnerHtml = modelSysRotate.PersonDayCount.ToString();
				this.spRotateCount.InnerHtml = modelSysRotate.RotateCount.ToString();
				this.spStartTime.InnerHtml = modelSysRotate.StartTime.ToString("yyyy-MM-dd");
				this.spEndTime.InnerHtml = modelSysRotate.EndTime.ToString("yyyy-MM-dd");
				this.spRotateDesc.InnerHtml = modelSysRotate.RotateRemark.ToString();
				this.spOneName.InnerHtml = modelSysRotate.OneName;
				this.spTwoName.InnerHtml = modelSysRotate.TwoName;
				this.spThreeName.InnerHtml = modelSysRotate.ThreeName;
				this.spFourName.InnerHtml = modelSysRotate.FourName;
				this.spFiveName.InnerHtml = modelSysRotate.FiveName;
				this.spSixName.InnerHtml = modelSysRotate.SixName;
				this.spOnePrizeName.InnerHtml = modelSysRotate.OnePrizeName;
				this.spTwoPrizeName.InnerHtml = modelSysRotate.TwoPrizeName;
				this.spThreePrizeName.InnerHtml = modelSysRotate.ThreePrizeName;
				this.spFourPrizeName.InnerHtml = modelSysRotate.FourPrizeName;
				this.spFivePrizeName.InnerHtml = modelSysRotate.FivePrizeName;
				this.spSixPrizeName.InnerHtml = modelSysRotate.SixPrizeName;
				this.spOneRate.InnerHtml = modelSysRotate.OneRate.ToString();
				this.spTwoRate.InnerHtml = modelSysRotate.TwoRate.ToString();
				this.spThreeRate.InnerHtml = modelSysRotate.ThreeRate.ToString();
				this.spFourRate.InnerHtml = modelSysRotate.FourRate.ToString();
				this.spFiveRate.InnerHtml = modelSysRotate.FiveRate.ToString();
				this.spSixRate.InnerHtml = modelSysRotate.SixRate.ToString();
				this.spIsWinOne.InnerHtml = modelSysRotate.IsWinOne.ToString();
				Chain.BLL.SysRotateCount bllSysRotateCount = new Chain.BLL.SysRotateCount();
				DataTable dt = bllSysRotateCount.GetList(" RotateID=" + RotateID).Tables[0];
				this.txtMemTotalCount.Value = "0";
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string starttime = DateTime.Parse(dt.Rows[i]["StartTime"].ToString()).ToString("yyyy-MM-dd");
					string endtime = DateTime.Parse(dt.Rows[i]["endTime"].ToString()).ToString("yyyy-MM-dd");
					int count = int.Parse(dt.Rows[i]["RotateCount"].ToString());
					int memID = int.Parse(this.txtMemID.Value);
					decimal costamount = decimal.Parse(dt.Rows[i]["CostAmount"].ToString());
					decimal amount = bllSysRotateCount.GetMemCountCostAmount(starttime, endtime, MemID);
					decimal amount2 = bllSysRotateCount.GetMemOrderLogCostAmount(starttime, endtime, MemID);
					decimal amount3 = bllSysRotateCount.GetMemStorageTimingCostAmount(starttime, endtime, MemID);
					decimal amount4 = amount + amount2 + amount3;
					if (amount4 >= costamount)
					{
						this.txtMemTotalCount.Value = count.ToString();
						break;
					}
				}
				Chain.BLL.SysRotatePrizeLog bllSysRotatePrizeLog = new Chain.BLL.SysRotatePrizeLog();
				int getcount = bllSysRotatePrizeLog.GetRecordCount(string.Concat(new object[]
				{
					"MemID=",
					int.Parse(this.txtMemID.Value),
					"  and RotateID=",
					int.Parse(this.txtRotateID.Value)
				}));
				int hasWinCount = bllSysRotatePrizeLog.GetRecordCount(string.Concat(new object[]
				{
					"MemID=",
					int.Parse(this.txtMemID.Value),
					" and PrizeLevel<>'未中奖' and RotateID=",
					int.Parse(this.txtRotateID.Value)
				}));
				this.spWinCount.InnerHtml = hasWinCount.ToString();
				if (this.txtMemTotalCount.Value != "0")
				{
					this.spNoUseCount.InnerHtml = (int.Parse(this.txtMemTotalCount.Value) - getcount).ToString();
				}
				if (modelSysRotate.OneMobile != null && modelSysRotate.OneMobile.ToString() != "")
				{
					string[] strOneMobileList = modelSysRotate.OneMobile.Trim(new char[]
					{
						','
					}).Split(new char[]
					{
						','
					});
					for (int a = 0; a < strOneMobileList.Length; a++)
					{
						int strMemID = new Chain.BLL.Mem().GetMemIDByMobile(strOneMobileList[a]);
						if (strMemID == MemID)
						{
							this.spIsOne.InnerHtml = "1";
							break;
						}
					}
				}
				if (modelSysRotate.TwoMobile != null && modelSysRotate.TwoMobile.ToString() != "")
				{
					string[] strTwoMobileList = modelSysRotate.TwoMobile.Trim(new char[]
					{
						','
					}).Split(new char[]
					{
						','
					});
					for (int b = 0; b < strTwoMobileList.Length; b++)
					{
						int strMemID = new Chain.BLL.Mem().GetMemIDByMobile(strTwoMobileList[b]);
						if (strMemID == MemID)
						{
							this.spIsTwo.InnerHtml = "1";
							break;
						}
					}
				}
				if (modelSysRotate.ThreeMobile != null && modelSysRotate.ThreeMobile.ToString() != "")
				{
					string[] strThreeMobileList = modelSysRotate.ThreeMobile.Trim(new char[]
					{
						','
					}).Split(new char[]
					{
						','
					});
					for (int c = 0; c < strThreeMobileList.Length; c++)
					{
						int strMemID = new Chain.BLL.Mem().GetMemIDByMobile(strThreeMobileList[c]);
						if (strMemID == MemID)
						{
							this.spIsThree.InnerHtml = "1";
							break;
						}
					}
				}
				if (modelSysRotate.FourMobile != null && modelSysRotate.FourMobile.ToString() != "")
				{
					string[] strFourMobileList = modelSysRotate.FourMobile.Trim(new char[]
					{
						','
					}).Split(new char[]
					{
						','
					});
					for (int d = 0; d < strFourMobileList.Length; d++)
					{
						int strMemID = new Chain.BLL.Mem().GetMemIDByMobile(strFourMobileList[d]);
						if (strMemID == MemID)
						{
							this.spIsFour.InnerHtml = "1";
							break;
						}
					}
				}
				if (modelSysRotate.FiveMobile != null && modelSysRotate.FiveMobile.ToString() != "")
				{
					string[] strFiveMobileList = modelSysRotate.FiveMobile.Trim(new char[]
					{
						','
					}).Split(new char[]
					{
						','
					});
					for (int e = 0; e < strFiveMobileList.Length; e++)
					{
						int strMemID = new Chain.BLL.Mem().GetMemIDByMobile(strFiveMobileList[e]);
						if (strMemID == MemID)
						{
							this.spIsFive.InnerHtml = "1";
							break;
						}
					}
				}
				if (modelSysRotate.SixMobile != null && modelSysRotate.SixMobile.ToString() != "")
				{
					string[] strSixMobileList = modelSysRotate.SixMobile.Trim(new char[]
					{
						','
					}).Split(new char[]
					{
						','
					});
					for (int f = 0; f < strSixMobileList.Length; f++)
					{
						int strMemID = new Chain.BLL.Mem().GetMemIDByMobile(strSixMobileList[f]);
						if (strMemID == MemID)
						{
							this.spIsSix.InnerHtml = "1";
							break;
						}
					}
				}
			}
		}
	}
}
