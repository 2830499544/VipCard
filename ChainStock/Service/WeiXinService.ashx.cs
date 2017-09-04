using Chain.BLL;
using Chain.Common;
using Chain.Model;
using System;
using System.Data;
using System.Reflection;
using System.Web;

namespace ChainStock.Service
{
	public class WeiXinService : IHttpHandler
	{
		private HttpRequest Request;

		private HttpResponse Response;

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			context.Response.Buffer = true;
			context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0);
			context.Response.AddHeader("pragma", "no-cache");
			context.Response.AddHeader("cache-control", "");
			context.Response.CacheControl = "no-cache";
			context.Response.ContentType = "text/plain";
			this.Request = context.Request;
			this.Response = context.Response;
			string method = this.Request["Method"].ToString();
			MethodInfo methodInfo = base.GetType().GetMethod(method);
			methodInfo.Invoke(this, null);
		}

		public void UpdateMemInfo()
		{
			int flag = 0;
			try
			{
				string MemWeiXinCard = this.Request["MemWeiXinCard"];
				string MemName = this.Request["MemName"];
				bool MemSex = !(this.Request["MemSex"] == "0");
				string tempMemBirthday = this.Request["MemBirthday"];
				string MemCard = this.Request["MemCard"];
				DateTime MemBirthday;
				if (DateTime.TryParse(tempMemBirthday, out MemBirthday))
				{
					Chain.BLL.Mem memBll = new Chain.BLL.Mem();
					Chain.Model.Mem memModel = memBll.GetModelByMemCard(MemCard);
					memModel.MemName = MemName;
					memModel.MemSex = MemSex;
					memModel.MemBirthday = MemBirthday;
					flag = memBll.Update(memModel);
				}
				else
				{
					flag = -1;
				}
			}
			catch
			{
				flag = 0;
			}
			this.Response.Write(flag);
		}

		public void GetListForPointGift()
		{
			string flag = "";
			try
			{
				int pageIndex = (this.Request["pageIndex"] == null) ? 2 : int.Parse(this.Request["pageIndex"]);
				int pageSize = (this.Request["pageSize"] == null) ? 5 : int.Parse(this.Request["pageSize"]);
				string strWhere = string.Empty;
				if (PubFunction.curParameter.bolGiftShare)
				{
					strWhere = "PointGift.GiftClassID=GiftClass.GiftClassID and PointGift.GiftShopID=SysShop.ShopID";
				}
				else
				{
					strWhere = "PointGift.GiftClassID=GiftClass.GiftClassID and PointGift.GiftShopID=SysShop.ShopID and SysShop.ShopID=1";
				}
				int count;
				DataTable dt = new Chain.BLL.PointGift().GetListSP(pageSize, pageIndex, false, out count, new string[]
				{
					strWhere
				}).Tables[0];
				flag = JsonPlus.ToJson(dt, "GiftID,GiftPhoto,GiftName,GiftExchangePoint");
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		public void ConvertGift()
		{
			int flag = 0;
			try
			{
				string MemWeiXinCard = this.Request["MemWeiXinCard"];
				int GiftID = int.Parse(this.Request["GiftID"]);
				int Num = int.Parse(this.Request["Num"]);
				string memAddress = this.Request["memAddress"];
				string telNumber = this.Request["telNumber"];
				int SumExchangePoint = int.Parse(this.Request["SumExchangePoint"]);
				Chain.Model.GiftExchange giftExchange = new Chain.Model.GiftExchange();
				Chain.Model.Mem mem = new Chain.BLL.Mem().GetMemByWeiXinCard(MemWeiXinCard);
				giftExchange.MemID = mem.MemID;
				giftExchange.ExchangeTelePhone = telNumber;
				giftExchange.ExchangeAddress = memAddress;
				giftExchange.ExchangeAccount = DateTime.Now.ToString("yyMMddhhmmssffff");
				giftExchange.ExchangeAllNumber = Num;
				giftExchange.ExchangeAllPoint = SumExchangePoint;
				giftExchange.ApplicationTime = DateTime.Now;
				giftExchange.ApplicationRemark = "";
				giftExchange.ExchangeType = 3;
				giftExchange.ExchangeStatus = 1;
				flag = new Chain.BLL.GiftExchange().Add(giftExchange);
				if (flag > 0)
				{
					Chain.Model.GiftExchangeDetail giftExchangeDetailModel = new Chain.Model.GiftExchangeDetail();
					giftExchangeDetailModel.ExchangeID = flag;
					giftExchangeDetailModel.ExchangeGiftID = GiftID;
					giftExchangeDetailModel.ExchangeNumber = Num;
					giftExchangeDetailModel.ExchangePoint = new Chain.BLL.PointGift().GetModel(GiftID).GiftExchangePoint * Num;
					flag = new Chain.BLL.GiftExchangeDetail().Add(giftExchangeDetailModel);
				}
			}
			catch
			{
				flag = 0;
			}
			this.Response.Write(flag);
		}

		public void GetRecharge()
		{
			string flag = "";
			try
			{
				int pageIndex = (this.Request["pageIndex"] == null) ? 2 : int.Parse(this.Request["pageIndex"]);
				int pageSize = (this.Request["pageSize"] == null) ? 5 : int.Parse(this.Request["pageSize"]);
				string MemWeiXinCard = this.Request["MemWeiXinCard"];
				string strWhere = "MemWeiXinCard='" + MemWeiXinCard + "' and MemRecharge.RechargeShopID = SysShop.ShopID and MemRecharge.RechargeMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and MemRecharge.RechargeUserID = SysUser.UserID";
				int count;
				DataTable dt = new Chain.BLL.MemRecharge().GetListSP(pageSize, pageIndex, out count, new string[]
				{
					strWhere
				}).Tables[0];
				flag = JsonPlus.ToJson(dt, "RechargeType,RechargeAccount,RechargeMoney,RechargeOrdMoney,RechargeGive,RechargeCreateTime");
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		public void GetExpense()
		{
			string flag = "";
			try
			{
				int pageIndex = (this.Request["pageIndex"] == null) ? 2 : int.Parse(this.Request["pageIndex"]);
				int pageSize = (this.Request["pageSize"] == null) ? 5 : int.Parse(this.Request["pageSize"]);
				string MemWeiXinCard = this.Request["MemWeiXinCard"];
				string strWhere = "MemWeiXinCard='" + MemWeiXinCard + "'";
				int count;
				DataTable dt = new Chain.BLL.Mem().GetMemExpenseToWeiXin(pageSize, pageIndex, out count, new string[]
				{
					strWhere
				}).Tables[0];
				flag = JsonPlus.ToJson(dt, "OrderAccount,OrderTotalMoney,OrderDiscountMoney,OrderPayCoupon,OrderPoint,OrderCreateTime");
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		public void GetPointChange()
		{
			string flag = "";
			try
			{
				int pageIndex = (this.Request["pageIndex"] == null) ? 2 : int.Parse(this.Request["pageIndex"]);
				int pageSize = (this.Request["pageSize"] == null) ? 5 : int.Parse(this.Request["pageSize"]);
				string MemWeiXinCard = this.Request["MemWeiXinCard"];
				string strWhere = "MemWeiXinCard='" + MemWeiXinCard + "' and PointLog.PointShopID = SysShop.ShopID and PointLog.PointMemID = Mem.MemID and Mem.MemLevelID=MemLevel.LevelID and PointLog.PointUserID = SysUser.UserID";
				int count;
				DataTable dt = new Chain.BLL.PointLog().GetListSP(pageSize, pageIndex, out count, new string[]
				{
					strWhere
				}).Tables[0];
				flag = JsonPlus.ToJson(dt, "PointNumber,PointRemark,PointCreateTime");
			}
			catch
			{
			}
			this.Response.Write(flag);
		}

		public void GetConvertHistory()
		{
			string flag = "";
			try
			{
				int pageIndex = (this.Request["pageIndex"] == null) ? 2 : int.Parse(this.Request["pageIndex"]);
				int pageSize = (this.Request["pageSize"] == null) ? 5 : int.Parse(this.Request["pageSize"]);
				string MemWeiXinCard = this.Request["MemWeiXinCard"];
				string strWhere = "GiftExchange.MemID=Mem.MemID and MemWeiXinCard='" + MemWeiXinCard + "'";
				int count;
				DataTable dt = new Chain.BLL.GiftExchange().GetListGiftExchangeSP(pageSize, pageIndex, out count, new string[]
				{
					strWhere
				}).Tables[0];
				flag = JsonPlus.ToJson(dt, "ExchangeAccount,ExchangeAllNumber,ExchangeAllPoint,ExchangeStatus,ExchangeRemark,ApplicationTime,ExchangeID");
			}
			catch
			{
			}
			this.Response.Write(flag);
		}
	}
}
